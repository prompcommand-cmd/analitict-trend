using AnaliticTrend.Application.Models.Auth.Request;
using AnaliticTrend.Application.Models.Auth.Response;
using Application.Abstracts.UseCases;

namespace AnaliticTrend.Application.Abstracts.UseCases.Auth
{
    public interface IAuthLoginWithEmailUseCase : IGenericUseCase
    {
        Task<AuthLoginResponse> LoginWithEmail(AuthLoginRequest request);
    }
}
