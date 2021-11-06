using ContainerExpressions.Containers;
using System;
using System.Text.Json;

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
                var model = JsonSerializer.Deserialize<T>(json, options.SerializerSettings);
                response = response.With(model);
            }
            catch (Exception ex)
            {
                ex.LogValue($"Error deserializing json to type {typeof(T).FullName}: {ex}");
            }

            return response;
        }

        public Response<string> FromModel<T>(T model) => FromModel(model, JsonOptions.Performant);

        public Response<string> FromModel<T>(T model, JsonOptions options)
        {
            var response = new Response<string>();

            try
            {
                var json = JsonSerializer.Serialize<T>(model, options.SerializerSettings);
                response = response.With(json);
            }
            catch (Exception ex)
            {
                ex.LogValue($"Error deserializing json to type {typeof(T).FullName}: {ex}");
            }

            return response;
        }
    }
}
