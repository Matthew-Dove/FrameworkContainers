﻿using ContainerExpressions.Containers;
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
                else new JsonException($"Unable to convert \"{reader.GetString()}\" to DateTime.").ThrowError();
            }

            return result;
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options) => writer.WriteStringValue(value);

        public override int GetHashCode() => base.GetHashCode();
        public override bool Equals(object obj) => obj is DefaultDateTimeConverter c && c is not null;
    }
}
