using FrameworkContainers.Format.XmlCollective.Models;
using FrameworkContainers.Models.Streams;
using FrameworkContainers.Models;
using System.Xml.Serialization;
using System.Xml;

namespace FrameworkContainers.Format.XmlCollective
{
    internal static class ExtensibleMarkupLanguage
    {
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
