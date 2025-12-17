using AnaliticTrend.Application.Models;
using Infrastructure;
using Infrastructure.Abstracts;
using Infrastructure.Models;
using Microsoft.Extensions.Options;

namespace WebApi.RegisterServices
{
    public static class RegisterConfigurations
    {
        public static void AddConfigurations(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<CryptographySetting>(builder.Configuration.GetSection(CryptographySetting.section));
            builder.Services.Configure<JwtInternalSetting>(options => builder.Configuration.GetSection(JwtInternalSetting.section).Bind(options));
            builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<CryptographySetting>>().Value);
            builder.Services.AddSingleton<ISecretManager, SecretManager>();
        }
    }
}
