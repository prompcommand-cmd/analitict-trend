using AnaliticTrend.Application.Abstracts.UseCases.Auth;
using AnaliticTrend.Application.Models.Auth.Request;
using AnaliticTrend.Application.Models.Auth.Response;
using Microsoft.AspNetCore.Mvc;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;
using WebApi.Abstracts;
using WebApi.Models;
using WebApi.Utilities;

namespace AnaliticTrend.WebApi.EndPoints.Auth
{
    public class AuthLoginEndPoint : IEndpoint
    {
        private static string urlFragment = "auth";

        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            RouteGroupBuilder group = app.MapGroup($"{urlFragment}")
                .WithTags("Authentication")
                .AddFluentValidationAutoValidation();

            group.MapPost("login", async Task<IResult> ([FromBody] AuthLoginRequest request,
                IAuthLoginWithEmailUseCase _authLoginWithEmailUseCase) =>
            {
                var result = await _authLoginWithEmailUseCase.LoginWithEmail(request).ConfigureAwait(false);
                return Results.Ok(result.ToStandardResponse());
            })
            .AllowAnonymous()
            .Produces<ResponseBuilder<AuthLoginResponse>>(StatusCodes.Status200OK)
            .Produces<ErrorDetail>(StatusCodes.Status400BadRequest);
        }
    }
}
