using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FrameworkContainers.Format.JsonCollective.Models.Converters
{
    /// <summary>
    /// <para>Allows parsing of DateTime when the json source is null or empty.</para>
    /// <para>When the source has no value, a default will be used instead.</para>
    /// <para>Otherwise the value will be parsed as a normal DateTime.</para>
    /// </summary>
    public sealed class DefaultDateTimeConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => Read(ref reader);

        private static DateTime Read(ref Utf8JsonReader reader)
        {
            DateTime result = default;

            if (reader.TokenType == JsonTokenType.String)
            {
                if (reader.TryGetDateTime(out DateTime value)) result = value;
                else if (string.IsNullOrEmpty(reader.GetString())) { }
                else JsonConverterError($"Unable to convert \"{reader.GetString()}\" to DateTime.");
            }

            return result;
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options) => writer.WriteStringValue(value);
    }
}
