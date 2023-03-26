using ContainerExpressions.Containers;
using FrameworkContainers.Models;
using FrameworkContainers.Models.Exceptions;
using System;

namespace FrameworkContainers.Format
{
    /// <summary>Access to JSON serialize, and deserialize methods that return the result in a Maybe container.</summary>
    public sealed class JsonMaybe
    {
        internal static readonly JsonMaybe Instance = new JsonMaybe();

        private JsonMaybe() { }

        public Maybe<T> ToModel<T>(string json) => ToModel<T>(json, JsonOptions.Performant);

        public Maybe<T> ToModel<T>(string json, JsonOptions options)
        {
            var maybe = new Maybe<T>();

            try
            {
                var model = JavaScriptObjectNotation.JsonToModel<T>(json, options);
                maybe = maybe.With(model);
            }
            catch (Exception ex)
            {
                maybe = maybe.With(new FormatDeserializeException(Constants.Format.DESERIALIZE_ERROR_MESSAGE, ex, FormatRange.Json, typeof(T), json));
            }

            return maybe;
        }

        public Maybe<string> FromModel<T>(T model) => FromModel(model, JsonOptions.Performant);

        public Maybe<string> FromModel<T>(T model, JsonOptions options)
        {
            var maybe = new Maybe<string>();

            try
            {
                var json = JavaScriptObjectNotation.ModelToJson<T>(model, options);
                maybe = maybe.With(json);
            }
            catch (Exception ex)
            {
                maybe = maybe.With(new FormatSerializeException(Constants.Format.SERIALIZE_ERROR_MESSAGE, ex, FormatRange.Json, model));
            }

            return maybe;
        }
    }

    /// <summary>Access to JSON serialize, and deserialize methods that return the result in a Maybe container (for a single type).</summary>
    public sealed class JsonMaybe<T>
    {
        internal static readonly JsonMaybe<T> Instance = new JsonMaybe<T>();

        private JsonMaybe() { }

        public Maybe<T> ToModel(string json) => JsonMaybe.Instance.ToModel<T>(json);

        public Maybe<T> ToModel(string json, JsonOptions options) => JsonMaybe.Instance.ToModel<T>(json, options);

        public Maybe<string> FromModel(T model) => JsonMaybe.Instance.FromModel<T>(model);

        public Maybe<string> FromModel(T model, JsonOptions options) => JsonMaybe.Instance.FromModel<T>(model, options);
    }
}
