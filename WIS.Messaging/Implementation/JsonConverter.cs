using System.Reflection;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace WIS.Messaging.Implementation;

public sealed class JsonValueConverter : JsonConverter<JsonValue>
{
    private static readonly MethodInfo _fromString =
        typeof(JsonValue).GetMethod("FromString", BindingFlags.Public | BindingFlags.Static, null, new[] { typeof(string) }, null);

    public override JsonValue? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var doc = JsonDocument.ParseValue(ref reader);
        var raw = doc.RootElement.GetRawText();
        if (_fromString == null) throw new JsonException("JsonValue.FromString not found");
        return (JsonValue)_fromString.Invoke(null, new object[] { raw })!;
    }

    public override void Write(Utf8JsonWriter writer, JsonValue value, JsonSerializerOptions options)
    {
        if (value is null) { writer.WriteNullValue(); return; }
        var raw = value.ToString();
        using var doc = JsonDocument.Parse(raw);
        doc.RootElement.WriteTo(writer);
    }
}