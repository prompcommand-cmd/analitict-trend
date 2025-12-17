using AnaliticTrend.Application.Utilities;
using Infrastructure.Abstracts;
using Infrastructure.Utilities;

namespace WebApi.RegisterServices
{
    public static class RegisterUtilities
    {
        public static void AddUtilities(this WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<ICryptography, Cryptography>();
            builder.Services.AddSingleton<JwtTokenGenerator>();
        }
    }
}
