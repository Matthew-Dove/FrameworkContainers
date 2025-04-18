using System.Text.Json.Serialization;
using System.Text.Json;
using System;
using ContainerExpressions.Containers;

namespace FrameworkContainers.Format.JsonCollective.Models.Converters
{
    /// <summary>
    /// Convert string values to Option, and vice versa.
    /// <para>The string value must exist in the range of defined options.</para>
    /// <para>If the input is null, the option will be null.</para>
    /// <para>If the input is not null, and not defined; an exception is thrown.</para>
    /// </summary>
    /// <typeparam name="TOption">The subclass that implements Option.</typeparam>
    public sealed class OptionConverter<TOption> : JsonConverter<TOption> where TOption : Option
    {
        public override TOption Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => Read(ref reader);

        private static TOption Read(ref Utf8JsonReader reader)
        {
            TOption result = default;

            if (reader.TokenType == JsonTokenType.String)
            {
                var value = reader.GetString();
                if (!Opt<TOption>.TryParse(value, out result)) new JsonException($"Unable to convert \"{value}\" to Option \"{typeof(TOption).Name}\".").ThrowError();
            }

            return result;
        }

        public override void Write(Utf8JsonWriter writer, TOption value, JsonSerializerOptions options)
        {
            if (value is null) writer.WriteNullValue();
            else writer.WriteStringValue(value.Value);
        }

        public override int GetHashCode() => base.GetHashCode();
        public override bool Equals(object obj) => obj is OptionConverter<TOption> c && c is not null;
    }

    file sealed class Opt<TOption> : Option where TOption : Option
    {
        public static bool TryParse(string value, out TOption option) => TryParse<TOption>(value, out option);
    }
}
