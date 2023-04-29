using FrameworkContainers.Format.JsonCollective.Models;
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
                JsonDeserializeError(ex, typeof(T), json);
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
                JsonSerializeError(ex, model);
            }
            return default;
        }
    }

    public static class Json<T>
    {
        public static readonly JsonClient<T> Client = new JsonClient<T>();
    }
}
