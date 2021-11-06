using ContainerExpressions.Containers;
using FrameworkContainers.Models;
using FrameworkContainers.Models.Exceptions;
using System;

namespace FrameworkContainers.Format
{
    /// <summary>Access to XML serialize, and deserialize methods that return the result in a Maybe container.</summary>
    public sealed class XmlMaybe
    {
        internal XmlMaybe() { }

        public Maybe<T, FormatDeserializeException> ToModel<T>(string xml)
        {
            var maybe = new Maybe<T, FormatDeserializeException>();

            try
            {
                var model = ExtensibleMarkupLanguage.XmlToModel<T>(xml);
                maybe = maybe.With(model);
            }
            catch (Exception ex)
            {
                ex.LogValue($"Error deserializing format to type {typeof(T).FullName}: {ex}");
                maybe = maybe.With(new FormatDeserializeException(Constants.Format.DESERIALIZE_ERROR_MESSAGE, ex, FormatRange.Xml, typeof(T), xml));
            }

            return maybe;
        }

        public Maybe<string, FormatSerializeException> FromModel<T>(T model) => FromModel(model, XmlOptions.Default);

        public Maybe<string, FormatSerializeException> FromModel<T>(T model, XmlOptions options)
        {
            var maybe = new Maybe<string, FormatSerializeException>();

            try
            {
                var xml = ExtensibleMarkupLanguage.ModelToXml<T>(model, options);
                maybe = maybe.With(xml);
            }
            catch (Exception ex)
            {
                ex.LogValue($"Error serializing format to type {typeof(T).FullName}: {ex}");
                maybe = maybe.With(new FormatSerializeException(Constants.Format.SERIALIZE_ERROR_MESSAGE, ex, FormatRange.Xml, model));
            }

            return maybe;
        }
    }
}
