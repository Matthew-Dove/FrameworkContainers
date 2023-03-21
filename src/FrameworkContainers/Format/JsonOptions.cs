using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FrameworkContainers.Format
{
    /// <summary>Json serializer options.</summary>
    public sealed class JsonOptions
    {
        internal JsonSerializerOptions SerializerSettings { get; }

        private JsonOptions(JsonSerializerDefaults defaults, JsonNamingPolicy namingPolicy = null)
        {
            SerializerSettings = new JsonSerializerOptions(defaults)
            {
                PropertyNamingPolicy = namingPolicy,
                Converters = {
                    new JsonStringEnumConverter(namingPolicy),
                    new JsonStringEnumConverter()
                },
                MaxDepth = 32
            };

            if (defaults == JsonSerializerDefaults.Web)
            {
                SerializerSettings.NumberHandling = JsonNumberHandling.AllowReadingFromString;
                SerializerSettings.AllowTrailingCommas = true;
                SerializerSettings.ReadCommentHandling = JsonCommentHandling.Skip;
                SerializerSettings.PropertyNameCaseInsensitive = true;
                SerializerSettings.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
                SerializerSettings.WriteIndented = true;
                SerializerSettings.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                SerializerSettings.DictionaryKeyPolicy = namingPolicy;
                SerializerSettings.IgnoreReadOnlyFields = true;
                SerializerSettings.IgnoreReadOnlyProperties = true;
                SerializerSettings.IncludeFields = true;
            }
        }

        /// <summary>Json serializer will be optimized for performance (this is the default behaviour).</summary>
        public static JsonOptions Performant { get; } = new JsonOptions(JsonSerializerDefaults.General);

        /// <summary>Json serializer will be optimized for performance.</summary>
        public static JsonOptions PerformantCamelCase { get; } = new JsonOptions(JsonSerializerDefaults.General, JsonNamingPolicy.CamelCase);

        /// <summary>Json serializer will be optimized to "just work" (use this if you want it to work in the widest range of scenarios).</summary>
        public static JsonOptions Permissive { get; } = new JsonOptions(JsonSerializerDefaults.Web);

        /// <summary>Json serializer will be optimized to "just work" (use this if you want it to work in the widest range of scenarios).</summary>
        public static JsonOptions PermissiveCamelCase { get; } = new JsonOptions(JsonSerializerDefaults.Web, JsonNamingPolicy.CamelCase);
    }
}
