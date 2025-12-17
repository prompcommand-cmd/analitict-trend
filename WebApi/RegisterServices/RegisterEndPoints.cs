using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;
using WebApi.Abstracts;

namespace WebApi.RegisterServices
{
    public static class RegisterEndPoints
    {
        public static IServiceCollection AddEndpoints(this IServiceCollection services, Assembly assembly, ConfigurationManager configuration)
        {
            ServiceDescriptor[] serviceDescriptors = assembly
            .DefinedTypes
            .Where(type => type is { IsAbstract: false, IsInterface: false } &&
                            type.IsAssignableTo(typeof(IEndpoint)))
            .Select(type => ServiceDescriptor.Transient(typeof(IEndpoint), type))
            .ToArray();

            services.TryAddEnumerable(serviceDescriptors);

            return services;
        }
        public static IApplicationBuilder MapEndpoints(this WebApplication app, RouteGroupBuilder? routeGroupBuilder = null)
        {

            using (IServiceScope scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider.GetServices<IEndpoint>();
                foreach (IEndpoint item in services)
                {
                    item.MapEndpoint(app);
                }
            }

            return app;
        }
    }
}
