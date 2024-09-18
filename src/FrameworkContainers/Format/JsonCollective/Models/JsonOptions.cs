using FrameworkContainers.Format.JsonCollective.Models.Converters;
using FrameworkContainers.Models;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FrameworkContainers.Format.JsonCollective.Models
{
    /// <summary>Json serializer options.</summary>
    public sealed class JsonOptions
    {
        internal static JsonOptions Default { get { return PerformantCamelCase; } }

        internal JsonSerializerOptions SerializerSettings { get; }

        private JsonOptions(JsonSerializerDefaults defaults, JsonNamingPolicy namingPolicy = null, JsonConverter[] converters = null)
        {
            SerializerSettings = new JsonSerializerOptions(defaults)
            {
                PropertyNamingPolicy = namingPolicy,
                Converters = {
                    new JsonStringEnumConverter(namingPolicy)
                },
                MaxDepth = Constants.Serialize.MAX_DEPTH,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };

            for (int i = 0; i < converters?.Length; i++) SerializerSettings.Converters.Add(converters[i]);

            if (defaults == JsonSerializerDefaults.Web)
            {
                SerializerSettings.NumberHandling = JsonNumberHandling.AllowReadingFromString;
                SerializerSettings.AllowTrailingCommas = true;
                SerializerSettings.ReadCommentHandling = JsonCommentHandling.Skip;
                SerializerSettings.PropertyNameCaseInsensitive = true;
                SerializerSettings.WriteIndented = true;
                SerializerSettings.DictionaryKeyPolicy = namingPolicy;
                SerializerSettings.IncludeFields = true;
            }
        }

        /// <summary>Json serializer will be optimized for performance with the model's property case.</summary>
        public static JsonOptions Performant { get; } = new JsonOptions(JsonSerializerDefaults.General);

        /// <summary>Json serializer will be optimized for performance with camelCase (this is the default behaviour).</summary>
        public static JsonOptions PerformantCamelCase { get; } = new JsonOptions(JsonSerializerDefaults.General, JsonNamingPolicy.CamelCase);

        /// <summary>Json serializer will be optimized to "just work" with the model's property case (use this if you want it to work in the widest range of scenarios).</summary>
        public static JsonOptions Permissive { get; } = new JsonOptions(JsonSerializerDefaults.Web);

        /// <summary>Json serializer will be optimized to "just work" with camelCase (use this if you want it to work in the widest range of scenarios).</summary>
        public static JsonOptions PermissiveCamelCase { get; } = new JsonOptions(JsonSerializerDefaults.Web, JsonNamingPolicy.CamelCase);

        /// <summary>
        /// Add custom JSON converters to the JSON serializer.
        /// <para>e.g. JsonOptions.WithConverters(JsonFlags.Performant | JsonFlags.CamelCase, new DefaultDateTimeConverter(), new DefaultIntConverter())</para>
        /// </summary>
        /// <param name="flags">Set behaviour for the JSON serializer.</param>
        /// <param name="converters">Custom JSON type converters to use when serializing, or deserializing.</param>
        public static JsonOptions WithConverters(JsonFlags flags = JsonFlags.Default, params JsonConverter[] converters)
        {
            var isPerformant = flags == JsonFlags.Default || flags.HasFlag(JsonFlags.Performant);
            var isCamelCase = flags == JsonFlags.Default || flags.HasFlag(JsonFlags.CamelCase);

            return new JsonOptions(
                isPerformant ? JsonSerializerDefaults.General : JsonSerializerDefaults.Web,
                isCamelCase ? JsonNamingPolicy.CamelCase : null,
                converters
            );
        }

        public static IList<JsonConverter> GetJsonConverters()
        {
            var converters = new List<JsonConverter>(6);

            converters.Add(new DefaultBoolConverter());
            converters.Add(new DefaultDateTimeConverter());
            converters.Add(new DefaultFloatConverter());
            converters.Add(new DefaultGuidConverter());
            converters.Add(new DefaultIntConverter());
            converters.Add(new DefaultLongConverter());

            return converters;
        }
    }
}
