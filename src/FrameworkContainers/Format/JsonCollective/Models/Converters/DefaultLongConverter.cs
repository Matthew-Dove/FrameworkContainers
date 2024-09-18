using ContainerExpressions.Containers;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FrameworkContainers.Format.JsonCollective.Models.Converters
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
                else new JsonException($"Unable to convert \"{reader.GetString()}\" to long.").ThrowError();
            }
            else if (reader.TokenType == JsonTokenType.String)
            {
                var value = reader.GetString();
                if (!string.IsNullOrEmpty(value))
                {
                    if (long.TryParse(value, out long number)) result = number;
                    else new JsonException($"Unable to convert \"{value}\" to long.").ThrowError();
                }
            }

            return result;
        }

        public override void Write(Utf8JsonWriter writer, long value, JsonSerializerOptions options) => writer.WriteNumberValue(value);

        public override int GetHashCode() => base.GetHashCode();
        public override bool Equals(object obj) => obj is DefaultLongConverter c && c is not null;
    }
}
