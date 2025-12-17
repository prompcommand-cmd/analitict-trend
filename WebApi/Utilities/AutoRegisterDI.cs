using System.Reflection;

namespace WebApi.Utilities
{
    public static class AutoRegisterDI
    {
        public static void AutoRegisterRepositories(this IServiceCollection services, Assembly interfaceAssembly, Type genericInterface, Assembly classAssembly)
        {
            var getAllInterfaces = interfaceAssembly.GetTypes().Where(type => type.IsInterface);
            var extendedInterfaces = getAllInterfaces.Where(i => i.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == genericInterface));
            foreach (var item in extendedInterfaces)
            {
                var repositoryTypes = classAssembly.GetTypes().Where(type => !type.IsAbstract && !type.IsInterface && item.IsAssignableFrom(type));

                foreach (var repositoryType in repositoryTypes)
                {

                    if (repositoryTypes.Count() != 1)
                    {
                        throw new InvalidOperationException($"Repository '{repositoryType.Name}' must implement only one interface that implements IRepositoryBase<T>.");
                    }

                    services.AddScoped(item, repositoryType);
                }
            }
        }

        public static void AutoRegisterUseCases(this IServiceCollection services, Assembly interfaceAssembly, Type genericInterface, Assembly classAssembly)
        {
            var getAllInterfaces = interfaceAssembly.GetTypes().Where(type => type.IsInterface);
            var extendedInterfaces = getAllInterfaces.Where(i => i.GetInterfaces().Any(x => x.FullName == genericInterface.FullName));
            foreach (var item in extendedInterfaces)
            {
                var useCaseTypes = classAssembly.GetTypes().Where(type => !type.IsAbstract && !type.IsInterface && item.IsAssignableFrom(type));

                foreach (var useCaseType in useCaseTypes)
                {

                    if (useCaseTypes.Count() != 1)
                    {
                        throw new InvalidOperationException($"Use Case '{useCaseType.Name}' must implement only one interface that implements IRepositoryBase<T>.");
                    }

                    services.AddScoped(item, useCaseType);
                }
            }
        }
    }
}
