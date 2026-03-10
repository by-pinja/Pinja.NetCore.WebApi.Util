using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

/// <summary>
/// Correctly deserializes ISO-8601 datetime strings preserving UTC kind (Z suffix → DateTimeKind.Utc).
/// The library version (Pinja.NetCore.WebApi.Util.Time.UtcDateTimeConverter) uses DateTime.Parse without
/// DateTimeStyles.RoundtripKind, which silently converts UTC datetimes to local time on Read.
/// </summary>
public class UtcDateTimeConverter : JsonConverter<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var str = reader.GetString() ?? throw new InvalidOperationException($"Invalid value for {nameof(UtcDateTimeConverter)}");
        return DateTime.Parse(str, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ssZ"));
    }
}
