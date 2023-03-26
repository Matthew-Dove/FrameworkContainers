using FrameworkContainers.Models;
using FrameworkContainers.Models.Exceptions;
using FrameworkContainers.Models.Streams;
using System;
using System.Xml;
using System.Xml.Serialization;

namespace FrameworkContainers.Format
{
    /// <summary>Serialize, and deserialize between string, and model.</summary>
    public static class Xml
    {
        /// <summary>Access to XML serialize, and deserialize methods that return the result in a Maybe container.</summary>
        public static readonly XmlMaybe Maybe = XmlMaybe.Instance;

        /// <summary>Access to XML serialize, and deserialize methods that return the result in a Response container.</summary>
        public static readonly XmlResponse Response = XmlResponse.Instance;

        public static T ToModel<T>(string xml) => ToModel<T>(xml, XmlReadOptions.Default);

        public static T ToModel<T>(string xml, XmlReadOptions options)
        {
            try
            {
                return ExtensibleMarkupLanguage.XmlToModel<T>(xml, options);
            }
            catch (Exception ex)
            {
                ExtensibleMarkupLanguage.DeserializeError(ex, typeof(T), xml);
            }
            return default;
        }

        public static string FromModel<T>(T model) => FromModel(model, XmlWriteOptions.Default);

        public static string FromModel<T>(T model, XmlWriteOptions options)
        {
            try
            {
                return ExtensibleMarkupLanguage.ModelToXml<T>(model, options);
            }
            catch (Exception ex)
            {
                ExtensibleMarkupLanguage.SerializeError(ex, model);
            }
            return default;
        }
    }

    internal static class ExtensibleMarkupLanguage
    {
        public static void SerializeError(Exception ex, object model)
        {
            throw new FormatSerializeException(Constants.Format.SERIALIZE_ERROR_MESSAGE, ex, FormatRange.Xml, model);
        }

        public static void DeserializeError(Exception ex, Type targetType, string input)
        {
            throw new FormatDeserializeException(Constants.Format.DESERIALIZE_ERROR_MESSAGE, ex, FormatRange.Xml, targetType, input);
        }

        /// <summary>Converts an object to its serialized XML format.</summary>
        /// <typeparam name="T">The type of object we are operating on.</typeparam>
        /// <param name="value">The object we are operating on.</param>
        /// <returns>The XML string representation of the object.</returns>
        public static string ModelToXml<T>(T value, XmlWriteOptions options)
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
        /// <param name="xml">The XML string to deserialize from.</param>
        /// <returns>An object instance.</returns>
        public static T XmlToModel<T>(string xml, XmlReadOptions options)
        {
            using (var stream = new StringStream(xml))
            using (var reader = XmlDictionaryReader.CreateTextReader(stream, options.Encoding, _readerQuotas, OnClose))
            {
                var serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(reader);
            }
        }

        private readonly static XmlDictionaryReaderQuotas _readerQuotas = new XmlDictionaryReaderQuotas { MaxDepth = Constants.Serialize.MAX_DEPTH, MaxStringContentLength = Constants.Serialize.MAX_READ_LENGTH };

        private static void OnClose(XmlDictionaryReader _) { }
    }
}
