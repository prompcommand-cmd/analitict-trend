using Application.Abstracts;
using Application.Abstracts.Repositories;
using Infrastructure.Repositories;
using System.Reflection;
using WebApi.Utilities;

namespace WebApi.RegisterServices
{
    public static class RegisterRepositories
    {
        public static void MapRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IAppDbRepository<>), typeof(AppDbRepository<>));
            services.AutoRegisterRepositories(Assembly.Load("AnaliticTrend.Application"), typeof(IGenericRepository<>), Assembly.Load("AnaliticTrend.Infrastructure"));
        }
    }
}
