using ContainerExpressions.Containers;
using FrameworkContainers.Models.Exceptions;
using System;
using System.Text.Json;

namespace FrameworkContainers.Format
{
    /// <summary>Access to JSON serialize, and deserialize methods that return the result in a Maybe container.</summary>
    public sealed class JsonMaybe
    {
        internal const string DESERIALIZE_ERROR_MESSAGE = "Error deserializing format to model.";
        internal const string SERIALIZE_ERROR_MESSAGE = "Error serializing model to format.";

        internal JsonMaybe() { }

        public Maybe<T, FormatDeserializeException> ToModel<T>(string json) => ToModel<T>(json, JsonOptions.Performant);

        public Maybe<T, FormatDeserializeException> ToModel<T>(string json, JsonOptions options)
        {
            var maybe = new Maybe<T, FormatDeserializeException>();

            try
            {
                var model = JsonSerializer.Deserialize<T>(json, options.SerializerSettings);
                maybe = maybe.With(model);
            }
            catch (Exception ex)
            {
                ex.LogValue($"Error deserializing format to type {typeof(T).FullName}: {ex}");
                maybe = maybe.With(new FormatDeserializeException(DESERIALIZE_ERROR_MESSAGE, ex, FormatRange.Json, typeof(T), json));
            }

            return maybe;
        }

        public Maybe<string, FormatSerializeException> FromModel<T>(T model) => FromModel(model, JsonOptions.Performant);

        public Maybe<string, FormatSerializeException> FromModel<T>(T model, JsonOptions options)
        {
            var maybe = new Maybe<string, FormatSerializeException>();

            try
            {
                var json = JsonSerializer.Serialize<T>(model, options.SerializerSettings);
                maybe = maybe.With(json);
            }
            catch (Exception ex)
            {
                ex.LogValue($"Error serializing format to type {typeof(T).FullName}: {ex}");
                maybe = maybe.With(new FormatSerializeException(SERIALIZE_ERROR_MESSAGE, ex, FormatRange.Json, model));
            }

            return maybe;
        }
    }
}
