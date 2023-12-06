using System.Text.Json;
using System.Text.Json.Serialization;

namespace Public.DTO;

public class DateOnlyConverter : JsonConverter<DateOnly>
{
    private const string DateFormat = "yyyy/MM/dd";

    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string dateStr = reader.GetString()!;
        
        if (DateTime.TryParseExact(dateStr, DateFormat, null, System.Globalization.DateTimeStyles.None, out var date))
        {
            return DateOnly.FromDateTime(date);
        }

        throw new JsonException($"Invalid date format: {dateStr}. Expected format: {DateFormat}");
    }

    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(DateFormat));
    }
}