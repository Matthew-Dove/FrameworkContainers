using ContainerExpressions.Containers;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FrameworkContainers.Format.JsonCollective.Models.Converters
{
    /// <summary>
    /// <para>Allows parsing of ints when the json source is a number, or a string; null or empty.</para>
    /// <para>The string value can be null, or empty resulting in the default value of 0.</para>
    /// <para>Otherwise the value will be parsed as a normal int.</para>
    /// </summary>
    public sealed class DefaultIntConverter : JsonConverter<int>
    {
        public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => Read(ref reader);

        private static int Read(ref Utf8JsonReader reader)
        {
            int result = default;

            if (reader.TokenType == JsonTokenType.Number)
            {
                if (reader.TryGetInt32(out int value)) result = value;
                else new JsonException($"Unable to convert \"{reader.GetString()}\" to Int32.").ThrowError();
            }
            else if (reader.TokenType == JsonTokenType.String)
            {
                var value = reader.GetString();
                if (!string.IsNullOrEmpty(value))
                {
                    if (int.TryParse(value, out int number)) result = number;
                    else new JsonException($"Unable to convert \"{value}\" to Int32.").ThrowError();
                }
            }

            return result;
        }

        public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options) => writer.WriteNumberValue(value);
    }
}
