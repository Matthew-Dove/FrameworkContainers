using System.Text.Json.Serialization;
using System.Text.Json;
using System;
using ContainerExpressions.Containers;

namespace FrameworkContainers.Format.JsonCollective.Models.Converters
{
    /// <summary>
    /// <para>Allows parsing of Smart Enums when the json source is a number, or a string.</para>
    /// <para>If the source is a string, which is null, or empty - the default value of 0 will be set; or an empty enum range when 0 is not a defined value.</para>
    /// <para>If an incorrect string, or integer value is sent; an exception is thrown.</para>
    /// <para>The string json value input is expected to be CSV (case insensitive), the output smart enum value(s) will be lowercase.</para>
    /// </summary>
    public sealed class SmartEnumConverter<T> : JsonConverter<EnumRange<T>> where T : SmartEnum
    {
        public override EnumRange<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => SmartEnumConverter.Read<T>(ref reader);
        public override void Write(Utf8JsonWriter writer, EnumRange<T> value, JsonSerializerOptions options) => SmartEnumConverter.Write<T>(writer, value, FormatOptions.Lowercase);
    }

    /// <summary>
    /// <para>Allows parsing of Smart Enums when the json source is a number, or a string.</para>
    /// <para>If the source is a string, which is null, or empty - the default value of 0 will be set; or an empty enum range when 0 is not a defined value.</para>
    /// <para>If an incorrect string, or integer value is sent; an exception is thrown.</para>
    /// <para>The string json value input is expected to be CSV (case insensitive), the output smart enum value(s) will be lowercase.</para>
    /// </summary>
    public sealed class SmartEnumConverterLowerCase<T> : JsonConverter<EnumRange<T>> where T : SmartEnum
    {
        public override EnumRange<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => SmartEnumConverter.Read<T>(ref reader);
        public override void Write(Utf8JsonWriter writer, EnumRange<T> value, JsonSerializerOptions options) => SmartEnumConverter.Write<T>(writer, value, FormatOptions.Lowercase);
    }

    /// <summary>
    /// <para>Allows parsing of Smart Enums when the json source is a number, or a string.</para>
    /// <para>If the source is a string, which is null, or empty - the default value of 0 will be set; or an empty enum range when 0 is not a defined value.</para>
    /// <para>If an incorrect string, or integer value is sent; an exception is thrown.</para>
    /// <para>The string json value input is expected to be CSV (case insensitive), the output smart enum value(s) will be UPPERCASE.</para>
    /// </summary>
    public sealed class SmartEnumConverterUpperCase<T> : JsonConverter<EnumRange<T>> where T : SmartEnum
    {
        public override EnumRange<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => SmartEnumConverter.Read<T>(ref reader);
        public override void Write(Utf8JsonWriter writer, EnumRange<T> value, JsonSerializerOptions options) => SmartEnumConverter.Write<T>(writer, value, FormatOptions.Uppercase);
    }

    /// <summary>
    /// <para>Allows parsing of Smart Enums when the json source is a number, or a string.</para>
    /// <para>If the source is a string, which is null, or empty - the default value of 0 will be set; or an empty enum range when 0 is not a defined value.</para>
    /// <para>If an incorrect string, or integer value is sent; an exception is thrown.</para>
    /// <para>The string json value input is expected to be CSV (case insensitive), the output smart enum value(s) will be OriginalCase.</para>
    /// </summary>
    public sealed class SmartEnumConverterOriginalCase<T> : JsonConverter<EnumRange<T>> where T : SmartEnum
    {
        public override EnumRange<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => SmartEnumConverter.Read<T>(ref reader);
        public override void Write(Utf8JsonWriter writer, EnumRange<T> value, JsonSerializerOptions options) => SmartEnumConverter.Write<T>(writer, value, FormatOptions.Original);
    }

    file abstract class SmartEnumConverter
    {
        public static EnumRange<T> Read<T>(ref Utf8JsonReader reader) where T : SmartEnum
        {
            EnumRange<T> result = default;

            if (reader.TokenType == JsonTokenType.String)
            {
                var value = reader.GetString();
                if (!string.IsNullOrEmpty(value))
                {
                    if (SmartEnum<T>.TryParse(value, out var range)) result = range;
                    else new JsonException($"Unable to convert \"{reader.GetString()}\" to EnumRange<{typeof(T).Name}>.").ThrowError();
                }
            }
            else if (reader.TokenType == JsonTokenType.Number)
            {
                if (reader.TryGetInt32(out var value) && value >= 0 && SmartEnum<T>.TryParse(value, out var range)) result = range;
                else new JsonException($"Unable to convert \"{reader.GetString()}\" to EnumRange<{typeof(T).Name}>.").ThrowError();
            }

            if (result.Objects == null || result.Objects.Length == 0) result = None<T>.Range;
            return result;
        }

        public static void Write<T>(Utf8JsonWriter writer, EnumRange<T> value, FormatOptions format) where T : SmartEnum
        {
            string names = null;
            if (value.Objects != null && value.Objects.Length != 0) names = value.ToString(format);
            writer.WriteStringValue(names);
        }
    }

    file sealed class None<T> where T : SmartEnum
    {
        internal static readonly EnumRange<T> Range = SmartEnum<T>.FromValue(0).GetValueOrDefault(new EnumRange<T>());
    }
}
