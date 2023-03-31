using FrameworkContainers.Format.JsonCollective.Models;
using System.Text.Json;

namespace FrameworkContainers.Format.JsonCollective
{
    internal static class JavaScriptObjectNotation
    {
        public static T JsonToModel<T>(string json, JsonOptions options)
        {
            return JsonSerializer.Deserialize<T>(string.IsNullOrEmpty(json) ? "null" : json, options.SerializerSettings);
        }

        public static string ModelToJson<T>(T model, JsonOptions options)
        {
            return JsonSerializer.Serialize<T>(model, options.SerializerSettings);
        }
    }
}
