using Application.Abstracts;
using WebApi.Helpers;

namespace WebApi.RegisterServices
{
    public static class RegisterHelpers
    {
        public static IServiceCollection AddHelpersService(this IServiceCollection services)
        {
            services.AddScoped<IUserLogin, UserLogin>();
            return services;
        }
    }
}
