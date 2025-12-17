using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace AnaliticTrend.Infrastructure.Seeders
{
    public static class SeedUser
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    userId = "11111111-1111-1111-1111-111111111111",
                    userName = "admin",
                    email = "admin@example.com",
                    password = "$2y$10$cGUSx1kJIqSuDe1/KImcsu4wODl/B1pxHq2gWTkgWCcWL.QPX4nTW", //admin123
                    createdOn = new DateTimeOffset(2025, 1, 1, 0, 0, 0, TimeSpan.Zero)
                }
            );
        }
    }
}
