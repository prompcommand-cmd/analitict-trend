using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace WebApi.RegisterServices
{
    public static class RegisterDbContext
    {
        public static void AddDbContextService(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<AppDbContext>(option => option.UseSqlite(""));
        }
    }
}
