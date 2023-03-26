using FrameworkContainers.Models;
using FrameworkContainers.Models.Exceptions;
using System;
using System.Text.Json;

namespace FrameworkContainers.Format
{
    /// <summary>Serialize, and deserialize between string, and model.</summary>
    public static class Json
    {
        /// <summary>Access to JSON serialize, and deserialize methods that return the result in a Maybe container.</summary>
        public static readonly JsonMaybe Maybe = JsonMaybe.Instance;

        /// <summary>Access to JSON serialize, and deserialize methods that return the result in a Response container.</summary>
        public static readonly JsonResponse Response = JsonResponse.Instance;

        public static T ToModel<T>(string json) => ToModel<T>(json, JsonOptions.Default);

        public static T ToModel<T>(string json, JsonOptions options)
        {
            try
            {
                return JavaScriptObjectNotation.JsonToModel<T>(json, options);
            }
            catch (Exception ex)
            {
                JavaScriptObjectNotation.DeserializeError(ex, typeof(T), json);
            }
            return default;
        }

        public static string FromModel<T>(T model) => FromModel(model, JsonOptions.Default);

        public static string FromModel<T>(T model, JsonOptions options)
        {
            try
            {
                return JavaScriptObjectNotation.ModelToJson<T>(model, options);
            }
            catch (Exception ex)
            {
                JavaScriptObjectNotation.SerializeError(ex, model);
            }
            return default;
        }
    }

    internal static class JavaScriptObjectNotation
    {
        public static void SerializeError(Exception ex, object model)
        {
            throw new FormatSerializeException(Constants.Format.SERIALIZE_ERROR_MESSAGE, ex, FormatRange.Json, model);
        }

        public static void DeserializeError(Exception ex, Type targetType, string input)
        {
            throw new FormatDeserializeException(Constants.Format.DESERIALIZE_ERROR_MESSAGE, ex, FormatRange.Json, targetType, input);
        }

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
