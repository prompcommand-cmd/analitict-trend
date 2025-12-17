using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using AnaliticTrend.Infrastructure.Models;

namespace AnaliticTrend.Infrastructure.Persistance
{
    public class AppDbContextFactory
        : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite("Data Source=AnaliticTrend.db")
                .Options;

            return new AppDbContext(
                options,
                new DesignTimeServiceProvider());
        }
    }
}
