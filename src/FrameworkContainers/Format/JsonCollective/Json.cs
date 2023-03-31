using FrameworkContainers.Format.JsonCollective.Models;
using FrameworkContainers.Models;
using FrameworkContainers.Models.Exceptions;
using System;

namespace FrameworkContainers.Format.JsonCollective
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
                DeserializeError(ex, typeof(T), json);
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
                SerializeError(ex, model);
            }
            return default;
        }

        private static void SerializeError(Exception ex, object model)
        {
            throw new FormatSerializeException(Constants.Format.SERIALIZE_ERROR_MESSAGE, ex, FormatRange.Json, model);
        }

        private static void DeserializeError(Exception ex, Type targetType, string input)
        {
            throw new FormatDeserializeException(Constants.Format.DESERIALIZE_ERROR_MESSAGE, ex, FormatRange.Json, targetType, input);
        }
    }
}
