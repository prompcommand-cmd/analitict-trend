using FluentValidation;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;
using System.Reflection;
using WebApi.Extension;
using WebApi.Middleware;

namespace WebApi.RegisterServices
{
    public static class RegisterExceptions
    {
        public static IServiceCollection AddExceptionService(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.Load("AnaliticTrend.WebApi"));
            services.AddFluentValidationAutoValidation(configuration =>
            {
                configuration.OverrideDefaultResultFactoryWith<FluentValidationCustomResultFactory>();
            });
            services.AddExceptionHandler<ValidationExceptionHandler>();
            services.AddExceptionHandler<GlobalExceptionHandler>();
            services.AddProblemDetails();
            return services;
        }
    }
}
