using Core.Entities;
using Infrastructure.Persistance;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AnaliticTrend.Infrastructure.Seeders
{
    public static class SeedOrderRecord
    {
        public static void SeedFromJson(AppDbContext db)
        {
            if (db.OrderRecord.Any())
                return;

            var jsonPath = Path.Combine(AppContext.BaseDirectory, "OrderRecord.json");

            if (!File.Exists(jsonPath))
                throw new FileNotFoundException("OrderRecord.json not found", jsonPath);

            var json = File.ReadAllText(jsonPath);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                NumberHandling = JsonNumberHandling.AllowReadingFromString
            };

            var orders = JsonSerializer.Deserialize<List<OrderRecord>>(json, options);

            db.OrderRecord.AddRange(orders!);
            db.SaveChanges();
        }
    }
}
