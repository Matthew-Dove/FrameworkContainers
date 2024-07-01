using ContainerExpressions.Containers;
using FrameworkContainers.Format.JsonCollective.Models;
using System;

namespace FrameworkContainers.Format.JsonCollective
{
    /// <summary>Access to JSON serialize, and deserialize methods that return the result in a Response container.</summary>
    public sealed class JsonResponse
    {
        internal static readonly JsonResponse Instance = new JsonResponse();

        private JsonResponse() { }

        public Response<T> ToModel<T>(string json) => ToModel<T>(json, JsonOptions.Default);

        public Response<T> ToModel<T>(string json, JsonOptions options)
        {
            var response = new Response<T>();

            try
            {
                var model = JavaScriptObjectNotation.JsonToModel<T>(json, options);
                response = response.With(model);
            }
            catch (Exception ex)
            {
                ex.LogError($"Error deserializing format to type: \"{typeof(T).FullName}\".");
            }

            return response;
        }

        public Response<string> FromModel<T>(T model) => FromModel(model, JsonOptions.Default);

        public Response<string> FromModel<T>(T model, JsonOptions options)
        {
            var response = new Response<string>();

            try
            {
                var json = JavaScriptObjectNotation.ModelToJson<T>(model, options);
                response = response.With(json);
            }
            catch (Exception ex)
            {
                ex.LogError($"Error serializing format to type: \"{typeof(T).FullName}\".");
            }

            return response;
        }
    }

    /// <summary>Access to JSON serialize, and deserialize methods that return the result in a Response container (for a single type).</summary>
    public sealed class JsonResponse<T>
    {
        internal static readonly JsonResponse<T> Instance = new JsonResponse<T>();

        private JsonResponse() { }

        public Response<T> ToModel(string json) => JsonResponse.Instance.ToModel<T>(json);

        public Response<T> ToModel(string json, JsonOptions options) => JsonResponse.Instance.ToModel<T>(json, options);

        public Response<string> FromModel(T model) => JsonResponse.Instance.FromModel<T>(model);

        public Response<string> FromModel(T model, JsonOptions options) => JsonResponse.Instance.FromModel<T>(model, options);
    }
}
