using Infrastructure.Abstracts;
using Microsoft.Extensions.DependencyInjection;

namespace AnaliticTrend.Infrastructure.Models
{
    public sealed class DesignTimeServiceProvider : IServiceProvider
    {
        private readonly ServiceProvider _provider;

        public DesignTimeServiceProvider()
        {
            var services = new ServiceCollection();

            services.AddSingleton<ISecretManager, DesignTimeSecretManager>();

            _provider = services.BuildServiceProvider();
        }

        public object? GetService(Type serviceType)
            => _provider.GetService(serviceType);
    }
}
