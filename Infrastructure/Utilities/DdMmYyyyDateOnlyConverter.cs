using System.Globalization;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace AnaliticTrend.Infrastructure.Utilities
{
    public class DdMmYyyyDateOnlyConverter : JsonConverter<DateOnly>
    {
        public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            => DateOnly.ParseExact(reader.GetString()!, "dd-MM-yyyy", CultureInfo.InvariantCulture);

        public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
            => writer.WriteStringValue(value.ToString("dd-MM-yyyy"));
    }
}
