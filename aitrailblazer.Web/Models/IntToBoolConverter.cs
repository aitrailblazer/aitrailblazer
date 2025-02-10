using System;
using Newtonsoft.Json;

/// <summary>
/// Custom converter to convert integer values (1 and 0) to boolean (true and false),
/// and handle null values gracefully.
/// </summary>
public class IntToBoolConverter : JsonConverter<bool?>
{
    public override bool CanWrite => true;

    public override bool? ReadJson(JsonReader reader, Type objectType, bool? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null)
        {
            return null;
        }
        if (reader.TokenType == JsonToken.Integer)
        {
            int intValue = Convert.ToInt32(reader.Value);
            return intValue != 0;
        }
        if (reader.TokenType == JsonToken.Boolean)
        {
            return (bool)reader.Value;
        }
        throw new JsonSerializationException($"Unexpected token type {reader.TokenType} when parsing boolean.");
    }

    public override void WriteJson(JsonWriter writer, bool? value, JsonSerializer serializer)
    {
        if (value.HasValue)
        {
            writer.WriteValue(value.Value ? 1 : 0);
        }
        else
        {
            writer.WriteNull();
        }
    }
}