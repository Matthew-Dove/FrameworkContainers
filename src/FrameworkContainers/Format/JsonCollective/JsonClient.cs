using FrameworkContainers.Format.JsonCollective.Models;

namespace FrameworkContainers.Format.JsonCollective
{
    /// <summary>Dependency injection alterative to the static class.</summary>
    public interface IJsonClient
    {
        JsonMaybe Maybe { get; }
        JsonResponse Response { get; }
        T ToModel<T>(string json);
        T ToModel<T>(string json, JsonOptions options);
        string FromModel<T>(T model);
        string FromModel<T>(T model, JsonOptions options);
    }

    public sealed class JsonClient : IJsonClient
    {
        public JsonMaybe Maybe => Json.Maybe;
        public JsonResponse Response => Json.Response;

        public T ToModel<T>(string json) => Json.ToModel<T>(json);
        public T ToModel<T>(string json, JsonOptions options) => Json.ToModel<T>(json, options);

        public string FromModel<T>(T model) => Json.FromModel(model);
        public string FromModel<T>(T model, JsonOptions options) => Json.FromModel(model, options);
    }

    /// <summary>Dependency injection alterative to the static class (for a single type).</summary>
    public interface IJsonClient<T>
    {
        JsonMaybe<T> Maybe { get; }
        JsonResponse<T> Response { get; }
        T ToModel(string json);
        T ToModel(string json, JsonOptions options);
        string FromModel(T model);
        string FromModel(T model, JsonOptions options);
    }

    public sealed class JsonClient<T> : IJsonClient<T>
    {
        public JsonMaybe<T> Maybe => JsonMaybe<T>.Instance;
        public JsonResponse<T> Response => JsonResponse<T>.Instance;

        public T ToModel(string json) => Json.ToModel<T>(json);
        public T ToModel(string json, JsonOptions options) => Json.ToModel<T>(json, options);

        public string FromModel(T model) => Json.FromModel(model);
        public string FromModel(T model, JsonOptions options) => Json.FromModel(model, options);
    }
}
