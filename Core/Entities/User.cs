namespace Core.Entities
{
    public class User
    {
        public string userId { get; set; }
        public string userName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public DateTimeOffset? createdOn { get; set; } = DateTimeOffset.UtcNow;
    }
}
