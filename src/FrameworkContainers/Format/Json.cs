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
        public static readonly JsonMaybe Maybe = new JsonMaybe();

        /// <summary>Access to JSON serialize, and deserialize methods that return the result in a Response container.</summary>
        public static readonly JsonResponse Response = new JsonResponse();

        public static T ToModel<T>(string json) => ToModel<T>(json, JsonOptions.Performant);

        public static T ToModel<T>(string json, JsonOptions options)
        {
            try
            {
                return JsonSerializer.Deserialize<T>(json, options.SerializerSettings);
            }
            catch (Exception ex)
            {
                throw new FormatDeserializeException(Constants.Format.DESERIALIZE_ERROR_MESSAGE, ex, FormatRange.Json, typeof(T), json);
            }
        }

        public static string FromModel<T>(T model) => FromModel(model, JsonOptions.Performant);

        public static string FromModel<T>(T model, JsonOptions options)
        {
            try
            {
                return JsonSerializer.Serialize<T>(model, options.SerializerSettings);
            }
            catch (Exception ex)
            {
                throw new FormatSerializeException(Constants.Format.SERIALIZE_ERROR_MESSAGE, ex, FormatRange.Json, model);
            }
        }
    }
}
