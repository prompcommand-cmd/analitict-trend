using AnaliticTrend.Infrastructure.Seeders;
using Core.Entities;
using Infrastructure.Abstracts;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Persistance
{
    public class AppDbContext : DbContext
    {
        private readonly ISecretManager _secretManager;
        private const string contextName = ContextName.APP;

        [ActivatorUtilitiesConstructor]
        public AppDbContext(
            DbContextOptions<AppDbContext> options,
            IServiceProvider serviceProvider)
            : base(options)
        {
            _secretManager = serviceProvider.GetRequiredService<ISecretManager>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_secretManager.IsEnableSecretManager(contextName))
            {
                var conn = _secretManager.GetConnectionString(contextName);

                optionsBuilder
                    .UseSqlite(conn, opt => opt.CommandTimeout(90))
                    .LogTo(Console.WriteLine, LogLevel.Error);
            }
            else
            {
                base.OnConfiguring(optionsBuilder);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<OrderRecord> OrderRecord { get; set; }
    }
}
