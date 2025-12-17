namespace Application.Models.User
{
    public class UserLoginDto
    {
        public UserLoginDto(string? userId, string? userName, string email, string? jwtToken)
        {
            UserId = userId;
            UserName = userName;
            Email = email;
            JwtToken = jwtToken;
        }
        public string? UserId { get; init; }
        public string? UserName { get; init; }
        public string? Email { get; init; }
        public string? JwtToken { get; init; }
    }
}
