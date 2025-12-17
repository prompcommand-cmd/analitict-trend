using Application.Abstracts.UseCases;
using System.Reflection;
using WebApi.Utilities;

namespace WebApi.RegisterServices
{
    public static class RegisterUseCases
    {
        public static void MapUseCases(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AutoRegisterUseCases(Assembly.Load("AnaliticTrend.Application"), typeof(IGenericUseCase), Assembly.Load("AnaliticTrend.Application"));
        }
    }
}
