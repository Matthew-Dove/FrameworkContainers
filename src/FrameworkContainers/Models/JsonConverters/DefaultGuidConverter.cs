using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FrameworkContainers.Models.JsonConverters
{
    /// <summary>
    /// <para>Allows parsing of Guids when the json source is null or empty.</para>
    /// <para>When the source has no value, a default will be used instead.</para>
    /// <para>Otherwise the value will be parsed as a normal guid.</para>
    /// </summary>
    public sealed class DefaultGuidConverter : JsonConverter<Guid>
    {
        public override Guid Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => Read(ref reader);

        private static Guid Read(ref Utf8JsonReader reader)
        {
            Guid result = default;

            if (reader.TokenType == JsonTokenType.String)
            {
                if (reader.TryGetGuid(out Guid value)) result = value;
                else if (string.IsNullOrEmpty(reader.GetString())) { }
                else throw new JsonException($"Unable to convert \"{reader.GetString()}\" to Guid.");
            }

            return result;
        }

        public override void Write(Utf8JsonWriter writer, Guid value, JsonSerializerOptions options) => writer.WriteStringValue(value);
    }
}
