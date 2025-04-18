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
    public sealed class SmartEnumConverter<TSmartEnum> : JsonConverter<EnumRange<TSmartEnum>> where TSmartEnum : SmartEnum
    {
        public override EnumRange<TSmartEnum> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => SmartEnumConverter.Read<TSmartEnum>(ref reader);
        public override void Write(Utf8JsonWriter writer, EnumRange<TSmartEnum> value, JsonSerializerOptions options) => SmartEnumConverter.Write<TSmartEnum>(writer, value, FormatOptions.Lowercase);
        public override int GetHashCode() => base.GetHashCode();
        public override bool Equals(object obj) => obj is SmartEnumConverter<TSmartEnum> c && c is not null;
        public override bool CanConvert(Type typeToConvert) => typeof(EnumRange<TSmartEnum>).IsAssignableFrom(typeToConvert);
    }

    /// <summary>
    /// <para>Allows parsing of Smart Enums when the json source is a number, or a string.</para>
    /// <para>If the source is a string, which is null, or empty - the default value of 0 will be set; or an empty enum range when 0 is not a defined value.</para>
    /// <para>If an incorrect string, or integer value is sent; an exception is thrown.</para>
    /// <para>The string json value input is expected to be CSV (case insensitive), the output smart enum value(s) will be lowercase.</para>
    /// </summary>
    public sealed class SmartEnumConverterLowerCase<TSmartEnum> : JsonConverter<EnumRange<TSmartEnum>> where TSmartEnum : SmartEnum
    {
        public override EnumRange<TSmartEnum> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => SmartEnumConverter.Read<TSmartEnum>(ref reader);
        public override void Write(Utf8JsonWriter writer, EnumRange<TSmartEnum> value, JsonSerializerOptions options) => SmartEnumConverter.Write<TSmartEnum>(writer, value, FormatOptions.Lowercase);
        public override int GetHashCode() => base.GetHashCode();
        public override bool Equals(object obj) => obj is SmartEnumConverterLowerCase<TSmartEnum> c && c is not null;
        public override bool CanConvert(Type typeToConvert) => typeof(EnumRange<TSmartEnum>).IsAssignableFrom(typeToConvert);
    }

    /// <summary>
    /// <para>Allows parsing of Smart Enums when the json source is a number, or a string.</para>
    /// <para>If the source is a string, which is null, or empty - the default value of 0 will be set; or an empty enum range when 0 is not a defined value.</para>
    /// <para>If an incorrect string, or integer value is sent; an exception is thrown.</para>
    /// <para>The string json value input is expected to be CSV (case insensitive), the output smart enum value(s) will be UPPERCASE.</para>
    /// </summary>
    public sealed class SmartEnumConverterUpperCase<TSmartEnum> : JsonConverter<EnumRange<TSmartEnum>> where TSmartEnum : SmartEnum
    {
        public override EnumRange<TSmartEnum> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => SmartEnumConverter.Read<TSmartEnum>(ref reader);
        public override void Write(Utf8JsonWriter writer, EnumRange<TSmartEnum> value, JsonSerializerOptions options) => SmartEnumConverter.Write<TSmartEnum>(writer, value, FormatOptions.Uppercase);
        public override int GetHashCode() => base.GetHashCode();
        public override bool Equals(object obj) => obj is SmartEnumConverterUpperCase<TSmartEnum> c && c is not null;
        public override bool CanConvert(Type typeToConvert) => typeof(EnumRange<TSmartEnum>).IsAssignableFrom(typeToConvert);
    }

    /// <summary>
    /// <para>Allows parsing of Smart Enums when the json source is a number, or a string.</para>
    /// <para>If the source is a string, which is null, or empty - the default value of 0 will be set; or an empty enum range when 0 is not a defined value.</para>
    /// <para>If an incorrect string, or integer value is sent; an exception is thrown.</para>
    /// <para>The string json value input is expected to be CSV (case insensitive), the output smart enum value(s) will be OriginalCase.</para>
    /// </summary>
    public sealed class SmartEnumConverterOriginalCase<TSmartEnum> : JsonConverter<EnumRange<TSmartEnum>> where TSmartEnum : SmartEnum
    {
        public override EnumRange<TSmartEnum> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => SmartEnumConverter.Read<TSmartEnum>(ref reader);
        public override void Write(Utf8JsonWriter writer, EnumRange<TSmartEnum> value, JsonSerializerOptions options) => SmartEnumConverter.Write<TSmartEnum>(writer, value, FormatOptions.Original);
        public override int GetHashCode() => base.GetHashCode();
        public override bool Equals(object obj) => obj is SmartEnumConverterOriginalCase<TSmartEnum> c && c is not null;
        public override bool CanConvert(Type typeToConvert) => typeof(EnumRange<TSmartEnum>).IsAssignableFrom(typeToConvert);
    }

    file abstract class SmartEnumConverter
    {
        public static EnumRange<TSmartEnum> Read<TSmartEnum>(ref Utf8JsonReader reader) where TSmartEnum : SmartEnum
        {
            EnumRange<TSmartEnum> result = default;

            if (reader.TokenType == JsonTokenType.String)
            {
                var value = reader.GetString();
                if (!string.IsNullOrEmpty(value))
                {
                    if (SmartEnum<TSmartEnum>.TryParse(value, out var range)) result = range;
                    else new JsonException($"Unable to convert \"{reader.GetString()}\" to EnumRange<{typeof(TSmartEnum).Name}>.").ThrowError();
                }
            }
            else if (reader.TokenType == JsonTokenType.Number)
            {
                if (reader.TryGetInt32(out var value) && value >= 0 && SmartEnum<TSmartEnum>.TryParse(value, out var range)) result = range;
                else new JsonException($"Unable to convert \"{reader.GetString()}\" to EnumRange<{typeof(TSmartEnum).Name}>.").ThrowError();
            }

            if (result.Objects == null || result.Objects.Length == 0) result = None<TSmartEnum>.Range;
            return result;
        }

        public static void Write<TSmartEnum>(Utf8JsonWriter writer, EnumRange<TSmartEnum> value, FormatOptions format) where TSmartEnum : SmartEnum
        {
            string names = null;
            if (value.Objects != null && value.Objects.Length != 0) names = value.ToString(format);
            writer.WriteStringValue(names);
        }
    }

    file sealed class None<TSmartEnum> where TSmartEnum : SmartEnum
    {
        internal static readonly EnumRange<TSmartEnum> Range = SmartEnum<TSmartEnum>.FromValue(0).GetValueOrDefault(new EnumRange<TSmartEnum>());
    }
}
