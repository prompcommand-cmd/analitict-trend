using Core.Constant;
using Infrastructure.Abstracts;
using Infrastructure.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Runtime.Serialization;
using System.Text.Json;

namespace Infrastructure
{
    public class SecretManager : ISecretManager
    {
        //private readonly SectionDatabaseSettings _sectionDb;
        private readonly ILogger<SecretManager> _logger;
        private readonly Dictionary<string, string> _connectionString = new Dictionary<string, string>();
        private readonly IConfiguration _configuration;

        public SecretManager(
            //SectionDatabaseSettings sectionDb, 
            ILogger<SecretManager> logger, 
            IConfiguration configuration)
        {
            //_sectionDb = sectionDb;
            _logger = logger;
            _connectionString.Add(ContextName.APP, "");
            _configuration = configuration;
            
        }

        public string GetConnectionString(string contextName)
        {
            if (string.IsNullOrWhiteSpace(_connectionString[contextName]))
            {
                try
                {
                    var dbSetting = _configuration.GetSection($"Databases:{contextName}").Get<DatabaseSettingModel>();

                    if (dbSetting?.Provider == DatabaseProvider.POSTGRESQL)
                    {
                        _connectionString[contextName] = 
                            $"Host = {dbSetting?.Host}; " +
                            $"Port = {dbSetting?.Port}; " +
                            "Database = {dbSetting?.database}; " +
                            $"User ID = {dbSetting?.UserName}; " +
                            $"Password = {dbSetting?.Password};";
                    }
                    else if (dbSetting?.Provider == DatabaseProvider.SQLSERVER)
                    {
                        _connectionString[contextName] =
                            $"Server={dbSetting?.Host},{dbSetting?.Port};" +
                            $"Database={dbSetting?.Database};" +
                            $"User Id={dbSetting?.UserName};" +
                            $"Password={dbSetting?.Password};" +
                            $"TrustServerCertificate={dbSetting?.TrustServerCertificate};";
                    }
                    else if (dbSetting?.Provider == DatabaseProvider.SQLITE)
                    {
                        var dbPath = Path.Combine(AppContext.BaseDirectory, $"{dbSetting.Database}.db");

                        _connectionString[contextName] =
                            $"Data Source={dbPath}";
                    }
                    else if (dbSetting?.Provider == DatabaseProvider.MONGODB)
                    {
                        _connectionString[contextName] =
                            $"{dbSetting?.Provider}://{dbSetting?.UserName}:{dbSetting?.Password}@{dbSetting?.Host}:{dbSetting?.Port}/?authSource={dbSetting?.Database}";
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error retrieving connection string for {ContextName}", contextName);
                    throw new SerializationException("Problem in get Db connection object from env.", ex);
                }
            }
            return _connectionString[contextName];
        }

        public bool IsEnableSecretManager(string contextName)
        {
            //return _sectionDb[contextName].isEnable;
            return true;
        }
    }
}
