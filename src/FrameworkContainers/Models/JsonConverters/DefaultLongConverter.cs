using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FrameworkContainers.Models.JsonConverters
{
    /// <summary>
    /// <para>Allows parsing of longs when the json source is a number, or a string; null or empty.</para>
    /// <para>The string value can be null, or empty resulting in the default value of 0.</para>
    /// <para>Otherwise the value will be parsed as a normal long.</para>
    /// </summary>
    public sealed class DefaultLongConverter : JsonConverter<long>
    {
        public override long Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => Read(ref reader);

        private static long Read(ref Utf8JsonReader reader)
        {
            long result = default;

            if (reader.TokenType == JsonTokenType.Number)
            {
                if (reader.TryGetInt64(out long value)) result = value;
                else throw new JsonException($"Unable to convert \"{reader.GetString()}\" to long.");
            }
            else if (reader.TokenType == JsonTokenType.String)
            {
                var value = reader.GetString();
                if (!string.IsNullOrEmpty(value))
                {
                    if (long.TryParse(value, out long number)) result = number;
                    else throw new JsonException($"Unable to convert \"{value}\" to long.");
                }
            }

            return result;
        }

        public override void Write(Utf8JsonWriter writer, long value, JsonSerializerOptions options) => writer.WriteNumberValue(value);
    }
}
