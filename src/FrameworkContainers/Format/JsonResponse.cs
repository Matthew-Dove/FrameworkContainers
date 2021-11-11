using ContainerExpressions.Containers;
using System;

namespace FrameworkContainers.Format
{
    /// <summary>Access to JSON serialize, and deserialize methods that return the result in a Response container.</summary>
    public sealed class JsonResponse
    {
        internal JsonResponse() { }

        public Response<T> ToModel<T>(string json) => ToModel<T>(json, JsonOptions.Performant);

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
                ex.LogValue($"Error deserializing format to type {typeof(T).FullName}: {ex}");
            }

            return response;
        }

        public Response<string> FromModel<T>(T model) => FromModel(model, JsonOptions.Performant);

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
                ex.LogValue($"Error deserializing format to type {typeof(T).FullName}: {ex}");
            }

            return response;
        }
    }
}
