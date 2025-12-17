namespace Infrastructure.Abstracts
{
    public interface ISecretManager
    {
        string GetConnectionString(string contextName);
        bool IsEnableSecretManager(string contextName);
    }
}
