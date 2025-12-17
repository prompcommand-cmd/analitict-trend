using Application.Abstracts;
using Application.Models.User;

namespace WebApi.Helpers
{
    public class UserLogin : IUserLogin
    {
        private string? _userId;
        private string? _userName;
        private string? _email;
        private string? _jwtToken;

        public UserLoginDto User() => new UserLoginDto(_userId, _userName, _email, _jwtToken);

        public void SetUserLogin(string? userId, string? userName, string? email, string? jwtToken)
        {
            _userId = userId;
            _userName = userName;
            _email = email;
            _jwtToken = jwtToken;
        }

        public string? UserId => _userId;
        public string? UserName => _userName;
        public string? Email => _email;
        public string? JwtToken => _jwtToken;
    }
}
