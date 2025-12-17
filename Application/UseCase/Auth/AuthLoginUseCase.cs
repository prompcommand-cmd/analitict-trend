using AnaliticTrend.Application.Abstracts.UseCases.Auth;
using AnaliticTrend.Application.Models.Auth.Request;
using AnaliticTrend.Application.Models.Auth.Response;
using AnaliticTrend.Application.Utilities;
using Application.Abstracts.Repositories;
using Application.Utilities;
using Core.Entities;

namespace AnaliticTrend.Application.UseCase.Auth
{
    public class AuthLoginUseCase : IAuthLoginWithEmailUseCase
    {
        private readonly IAppDbRepository<User> _userRepository;
        private readonly JwtTokenGenerator _jwtTokenGenerator;

        public AuthLoginUseCase(IAppDbRepository<User> userRepository, JwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<AuthLoginResponse> LoginWithEmail(AuthLoginRequest request)
        {
            var user = _userRepository.GetAll().Where(u => u.email == request.email).FirstOrDefault();
            if (user == null)
            {
                throw new Exception("Email doesnt found");
            }

            var verifyPassword = PasswordHasher.VerifyPassword(request.password, user.password);
            if (!verifyPassword)
            {
                throw new Exception("Password is incorrect");
            }

            var accessToken = _jwtTokenGenerator.GenerateToken(user);

            var response = new AuthLoginResponse
            {
                username = user.userName,
                Token = accessToken
            };

            return await Task.FromResult(response);
        }
    }
}
