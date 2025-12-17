namespace WebApi.Helpers
{
    public static class LogIdGenerator
    {
        public static string GenerateTimestampedRandomId()
        {
            var now = DateTime.UtcNow.ToLocalTime();
            var dateTimePart = $"{now:yyMMddHHmm}";
            var randomPart = new Random().Next(100, 1000).ToString("D3");

            return dateTimePart + randomPart;
        }
    }
}
