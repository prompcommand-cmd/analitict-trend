namespace AnaliticTrend.Application.Models
{
    public class JwtInternalSetting
    {
        public const string section = "JwtSettings";
        public string? Key { get; set; }
        public int ExpireInSeconds { get; set; }
        public int ExpireInMinutes { get; set; }
        public int ExpireInHours { get; set; }
        public string? Audience { get; set; }
    }
}
