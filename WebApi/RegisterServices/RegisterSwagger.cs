using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace WebApi.RegisterServices
{
    public static class RegisterSwagger
    {
        public static void AddSwagger(this WebApplicationBuilder builder)
        {

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(o =>
            {
                o.SwaggerDoc("v1", new OpenApiInfo { Title = "AnaliticTrend API", Version = "v1" });
                o.AddSecurityDefinition("BearerToken", setOpenApiSecurityScheme());
                o.AddSecurityRequirement(setOpenApiSecurityRequirement());

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                o.IncludeXmlComments(xmlPath);
                var appXmlPath = Path.Combine(AppContext.BaseDirectory, "AnaliticTrend.Application.xml");
                o.IncludeXmlComments(appXmlPath);
            });
        }

        private static OpenApiSecurityRequirement setOpenApiSecurityRequirement()
        {
            return new OpenApiSecurityRequirement
                {
                     {
                          new OpenApiSecurityScheme
                          {
                               Reference = new OpenApiReference
                               {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "BearerToken"
                               }
                          },
                          new string[] {}
                     }
                };
        }

        private static OpenApiSecurityScheme setOpenApiSecurityScheme()
        {
            return new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
                Name = HeaderNames.Authorization,
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            };
        }

        public static void UseSwaggerMiddleware(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(option =>
            {
                option.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                option.DefaultModelsExpandDepth(-1);
                option.RoutePrefix = string.Empty;
            });
        }
    }
}
