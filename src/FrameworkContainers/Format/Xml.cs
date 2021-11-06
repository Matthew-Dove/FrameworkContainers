using FrameworkContainers.Models;
using FrameworkContainers.Models.Exceptions;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace FrameworkContainers.Format
{
    /// <summary>Serialize, and deserialize between string, and model.</summary>
    public static class Xml
    {
        /// <summary>Access to XML serialize, and deserialize methods that return the result in a Maybe container.</summary>
        public static readonly XmlMaybe Maybe = new XmlMaybe();

        /// <summary>Access to XML serialize, and deserialize methods that return the result in a Response container.</summary>
        public static readonly XmlResponse Response = new XmlResponse();

        public static T ToModel<T>(string xml)
        {
            try
            {
                return ExtensibleMarkupLanguage.XmlToModel<T>(xml);
            }
            catch (Exception ex)
            {
                throw new FormatDeserializeException(Constants.Format.DESERIALIZE_ERROR_MESSAGE, ex, FormatRange.Xml, typeof(T), xml);
            }
        }

        public static string FromModel<T>(T model) => FromModel(model, XmlOptions.Default);

        public static string FromModel<T>(T model, XmlOptions options)
        {
            try
            {
                return ExtensibleMarkupLanguage.ModelToXml<T>(model, options);
            }
            catch (Exception ex)
            {
                throw new FormatSerializeException(Constants.Format.SERIALIZE_ERROR_MESSAGE, ex, FormatRange.Xml, model);
            }
        }
    }

    internal static class ExtensibleMarkupLanguage
    {
        /// <summary>Converts an object to its serialized XML format.</summary>
        /// <typeparam name="T">The type of object we are operating on.</typeparam>
        /// <param name="value">The object we are operating on.</param>
        /// <returns>The XML string representation of the object.</returns>
        public static string ModelToXml<T>(T value, XmlOptions options)
        {
            var namespaces = options.RemoveDefaultXmlNamespaces ? new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty }) : null;
            var settings = new XmlWriterSettings
            {
                Indent = true,
                OmitXmlDeclaration = options.OmitXmlDeclaration,
                CheckCharacters = false
            };

            using (var stream = new StringWriterWithEncoding(options.Encoding))
            using (var writer = XmlWriter.Create(stream, settings))
            {
                var serializer = new XmlSerializer(value.GetType());
                serializer.Serialize(writer, value, namespaces);
                return stream.ToString();
            }
        }

        /// <summary>Creates an object instance from the specified XML string.</summary>
        /// <typeparam name="T">The Type of the object we are operating on.</typeparam>
        /// <param name="value">The object we are operating on.</param>
        /// <param name="xml">The XML string to deserialize from.</param>
        /// <returns>An object instance.</returns>
        public static T XmlToModel<T>(string xml)
        {
            using (var reader = new StringReader(xml))
            {
                var serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(reader);
            }
        }
    }
}
