using FrameworkContainers.Models.Streams;
using FrameworkContainers.Models;
using System.Xml.Serialization;
using System.Xml;
using System.Text;

namespace FrameworkContainers.Format.XmlCollective
{
    internal static class ExtensibleMarkupLanguage
    {
        private static readonly XmlDictionaryReaderQuotas _readerQuotas = new XmlDictionaryReaderQuotas { MaxDepth = Constants.Serialize.MAX_DEPTH, MaxStringContentLength = Constants.Serialize.MAX_READ_LENGTH };
        private static readonly XmlSerializerNamespaces _namespaces = new([XmlQualifiedName.Empty]);
        private static readonly XmlWriterSettings _settings = new() { Indent = true, OmitXmlDeclaration = true, CheckCharacters = false };
        private static readonly Encoding _encoding = Encoding.UTF8;

        /// <summary>Converts an object to its serialized XML format.</summary>
        /// <typeparam name="T">The type of object we are operating on.</typeparam>
        /// <param name="value">The object we are operating on.</param>
        /// <returns>The XML string representation of the object.</returns>
        public static string ModelToXml<T>(T value)
        {
            using var stream = new StringWriterWithEncoding(_encoding);
            using var writer = XmlWriter.Create(stream, _settings);
            Serializer<T>.Instance.Serialize(writer, value, _namespaces);
            return stream.ToString();
        }

        /// <summary>Creates an object instance from the specified XML string.</summary>
        /// <typeparam name="T">The Type of the object we are operating on.</typeparam>
        /// <param name="xml">The XML string to deserialize from.</param>
        /// <returns>An object instance.</returns>
        public static T XmlToModel<T>(string xml)
        {
            static void OnClose(XmlDictionaryReader _) { }
            using var stream = new StringStream(xml);
            using var reader = XmlDictionaryReader.CreateTextReader(stream, _encoding, _readerQuotas, OnClose);
            return (T)Serializer<T>.Instance.Deserialize(reader);
        }

        private static class Serializer<T>
        {
            public static readonly XmlSerializer Instance = new XmlSerializer(typeof(T));
        }
    }
}
