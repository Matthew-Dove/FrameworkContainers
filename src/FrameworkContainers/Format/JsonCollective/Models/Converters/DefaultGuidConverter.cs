using ContainerExpressions.Containers.Extensions;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FrameworkContainers.Format.JsonCollective.Models.Converters
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
                else new JsonException($"Unable to convert \"{reader.GetString()}\" to Guid.").ThrowError();
            }

            return result;
        }

        public override void Write(Utf8JsonWriter writer, Guid value, JsonSerializerOptions options) => writer.WriteStringValue(value);
    }
}
