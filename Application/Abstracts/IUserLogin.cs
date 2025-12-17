using Application.Models.User;

namespace Application.Abstracts
{
    public interface IUserLogin
    {
        UserLoginDto User();
        void SetUserLogin(string? userId, string? userName, string? email, string? jwtToken);
        string UserId { get; }
        string UserName { get; }
        string Email { get; }
        string JwtToken { get; }
    }
}
