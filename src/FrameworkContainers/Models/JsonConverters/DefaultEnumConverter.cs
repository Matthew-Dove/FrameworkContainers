using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FrameworkContainers.Models.JsonConverters
{
    /// <summary>
    /// <para>Allows parsing of enums when the json source is a number, or a string.</para>
    /// <para>The string value can be null, or empty resulting in the 0th enum field being selected.</para>
    /// <para>Otherwise the value will attempt to be parsed to the enum.</para>
    /// </summary>
    /// <typeparam name="TEnum">The type of enum that is being converted.</typeparam>
    public sealed class DefaultEnumConverter<TEnum> : JsonConverter<TEnum> where TEnum : struct
    {
        public override TEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => Read(ref reader);

        private static TEnum Read(ref Utf8JsonReader reader)
        {
            TEnum result = default;
            if (reader.TokenType == JsonTokenType.Number || reader.TokenType == JsonTokenType.String)
            {
                var value = reader.TokenType == JsonTokenType.Number ? reader.GetInt32().ToString() : reader.GetString();
                if (!string.IsNullOrEmpty(value) && !Enum.TryParse(value, true, out result)) throw new JsonException($"Unable to convert \"{value}\" to Enum \"{typeof(TEnum).FullName}\".");
            }
            return result;
        }

        public override void Write(Utf8JsonWriter writer, TEnum value, JsonSerializerOptions options) => writer.WriteStringValue(value.ToString());
    }
}
