using AnaliticTrend.Application.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AnaliticTrend.WebApi.RegisterServices
{
    public static class RegisterAuthentication
    {
        public static void AddAuthentication(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(o =>
                {
                    o.TokenValidationParameters = IdentityTokenValidationParameter(builder);
                    o.RequireHttpsMetadata = true;
                    o.SaveToken = true;
                })
                .AddIdentityCookies();
        }

        private static TokenValidationParameters IdentityTokenValidationParameter(WebApplicationBuilder builder)
        {
            return new TokenValidationParameters
            {
                ValidAudience = builder.Configuration["JwtSettings:Audience"],
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]!.ToString())),
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.Zero
            };
        }
    }
}
