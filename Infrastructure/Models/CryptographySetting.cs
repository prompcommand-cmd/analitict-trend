namespace Infrastructure.Models
{
    public class CryptographySetting
    {
        public const string section = "CryptographySettings";
        public string Provider { get; set; }
        public string Key { get; set; }
    }
}
