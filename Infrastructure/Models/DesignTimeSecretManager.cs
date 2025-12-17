using Infrastructure.Abstracts;

namespace AnaliticTrend.Infrastructure.Models
{
    class DesignTimeSecretManager : ISecretManager
    {
        public bool IsEnableSecretManager(string contextName) => false;

        public string GetConnectionString(string contextName)
            => "Data Source=AnaliticTrend.db";
    }
}
