using ContainerExpressions.Containers;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FrameworkContainers.Format.JsonCollective.Models.Converters
{
    /// <summary>
    /// <para>Allows parsing of Boolean when the json source is 1, 0, null or empty.</para>
    /// <para>When the source has no value, a default will be used instead.</para>
    /// <para>Otherwise the value will be parsed as a normal boolean.</para>
    /// </summary>
    public sealed class DefaultBoolConverter : JsonConverter<bool>
    {
        public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => Read(ref reader);

        private static bool Read(ref Utf8JsonReader reader)
        {
            bool result = reader.TokenType == JsonTokenType.True;

            if (reader.TokenType == JsonTokenType.String)
            {
                var value = reader.GetString();
                if (!string.IsNullOrEmpty(value))
                {
                    if (value.Equals("true", StringComparison.OrdinalIgnoreCase)) result = true;
                    else if (value.Equals("false", StringComparison.OrdinalIgnoreCase)) result = false;
                    else if (value.Equals("1")) result = true;
                    else if (value.Equals("0")) result = false;
                    else new JsonException($"Unable to convert \"{reader.GetString()}\" to Boolean.").ThrowError();
                }
            }
            else if (reader.TokenType == JsonTokenType.Number)
            {
                if (reader.TryGetInt32(out int value))
                {
                    if (value == 0 || value == 1) result = value == 1;
                    else new JsonException($"Unable to convert \"{reader.GetString()}\" to Boolean.").ThrowError();
                }
                else new JsonException($"Unable to convert \"{reader.GetString()}\" to Boolean.").ThrowError();
            }

            return result;
        }

        public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options) => writer.WriteBooleanValue(value);

        public override int GetHashCode() => base.GetHashCode();
        public override bool Equals(object obj) => obj is DefaultBoolConverter c && c is not null;
    }
}
