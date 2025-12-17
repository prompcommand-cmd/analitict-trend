namespace Infrastructure.Models
{
    public class DatabaseSettingModel
    {
        public bool IsEnable { get; set; }
        public string? Provider { get; set; }
        public string? Host { get; set; }
        public string? Port { get; set; }
        public string? Database { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? ApplicationName { get; set; }
        public bool? TrustServerCertificate { get; set; }
    }
}
