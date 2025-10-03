using System.Text.Json;
using System.Text.Json.Serialization;
using System.Globalization;

public class JsonDateTimeConverter : JsonConverter<DateTime>
{
    // Define o fuso horário de destino (Ex: Brasília)
    private static readonly TimeZoneInfo BrazilTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");

    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        // Converte a string JSON de volta para um DateTime UTC
        return reader.GetDateTime().ToUniversalTime();
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        // Pega a data (que deve estar em UTC no banco)
        var utcDate = DateTime.SpecifyKind(value, DateTimeKind.Utc);

        // Converte do UTC para o fuso horário de Brasília
        var brazilDate = TimeZoneInfo.ConvertTimeFromUtc(utcDate, BrazilTimeZone);

        // Escreve a data no formato ISO 8601 com o offset (-03:00)
        // O "K" no final é o especificador que adiciona o fuso horário
        writer.WriteStringValue(brazilDate.ToString("yyyy-MM-dd'T'HH:mm:ss.fffffffK"));
    }
}