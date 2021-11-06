using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FrameworkContainers.Models.JsonConverters
{
    /// <summary>
    /// <para>Allows parsing of floats when the json source is a number, or a string; null or empty.</para>
    /// <para>The string value can be null, or empty resulting in the default value of 0.</para>
    /// <para>Otherwise the value will be parsed as a normal float.</para>
    /// </summary>
    public sealed class DefaultFloatConverter : JsonConverter<float>
    {
        public override float Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => Read(ref reader);

        private static float Read(ref Utf8JsonReader reader)
        {
            float result = default;

            if (reader.TokenType == JsonTokenType.Number)
            {
                if (reader.TryGetSingle(out float value)) result = value;
                else throw new JsonException($"Unable to convert \"{reader.GetString()}\" to float.");
            }
            else if (reader.TokenType == JsonTokenType.String)
            {
                var value = reader.GetString();
                if (!string.IsNullOrEmpty(value))
                {
                    if (float.TryParse(value, out float number)) result = number;
                    else throw new JsonException($"Unable to convert \"{value}\" to float.");
                }
            }

            return result;
        }

        public override void Write(Utf8JsonWriter writer, float value, JsonSerializerOptions options) => writer.WriteNumberValue(value);
    }
}
