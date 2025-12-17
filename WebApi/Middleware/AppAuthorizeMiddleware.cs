using Application.Abstracts;
using Infrastructure.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApi.Extension;
using WebApi.Utilities;

namespace WebApi.Middleware
{
    public class AppAuthorizeMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ICryptography _cryptography;

        public AppAuthorizeMiddleware(RequestDelegate next, ICryptography cryptography)
        {
            _next = next;
            _cryptography = cryptography;
        }

        public async Task Invoke(HttpContext context, IUserLogin userLogin)
        {
            var authAttributes = context.GetEndpoint()?.Metadata?.GetOrderedMetadata<AuthAttribute>();
            context.Request.EnableBuffering();

            var endpoint = context.GetEndpoint();
            if (endpoint?.Metadata.GetMetadata<AllowAnonymousAttribute>() != null)
            {
                await _next(context);
                return;
            }

            if (!context.User.Identity.IsAuthenticated)
            {
                throw new UnauthorizedAccessException("Unauthenticated access to the resource");
            }

            if (context.User != null && context.User.Identity.IsAuthenticated)
            {
                var (userId, userName, email) = UserClaimUtility.UserLoginInfo(context, _cryptography);

                context.Items.Add("UserId", userId);
                context.Items.Add("UserName", userName);
                context.Items.Add("Email", email);

                var jwtToken = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                userLogin.SetUserLogin(userId, userName, email, jwtToken);
            }

            if (authAttributes != null && authAttributes.Any())
            {
                var userAccessValue = UserClaimUtility.GetTypeValue(context.User, authAttributes[0].menuCode);
                if (UserClaimUtility.IsAuthorized(userAccessValue, authAttributes[0].actions[0]))
                {
                    await _next(context);
                }
                else
                {
                    var problemDetails = new ProblemDetails
                    {
                        Title = "Error",
                        Detail = "Unauthorized access to the resource",
                        Status = (int)HttpStatusCode.Unauthorized
                    };

                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;

                    await context.Response
                        .WriteAsJsonAsync(problemDetails);
                }
            }
            else
            {
                await _next(context);
            }
        }
    }
}
