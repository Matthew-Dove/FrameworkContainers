using FrameworkContainers.Models.Streams;
using FrameworkContainers.Models;
using System.Xml.Serialization;
using System.Xml;
using System.Text;

namespace FrameworkContainers.Format.XmlCollective
{
    internal static class ExtensibleMarkupLanguage
    {
        private static readonly XmlDictionaryReaderQuotas _readerQuotas = new()
        {
            MaxDepth = Constants.Serialize.MAX_DEPTH,
            MaxStringContentLength = Constants.Serialize.MAX_READ_LENGTH
        };
        private static readonly XmlSerializerNamespaces _namespaces = new([XmlQualifiedName.Empty]);
        private static readonly XmlWriterSettings _settings = new() { Indent = true, OmitXmlDeclaration = true };
        private static readonly Encoding _encoding = Encoding.Unicode;

        public static string ModelToXml<T>(T value)
        {
            using var stream = new StringWriterWithEncoding(_encoding);
            using (var writer = XmlWriter.Create(stream, _settings))
            {
                Serializer<T>.Instance.Serialize(writer, value, _namespaces);
            }
            return stream.ToString();
        }

        public static T XmlToModel<T>(string xml)
        {
            using var stream = new StringStream(xml);
            using var reader = XmlDictionaryReader.CreateTextReader(stream, _encoding, _readerQuotas, onClose: null);
            return (T)Serializer<T>.Instance.Deserialize(reader);
        }

        private static class Serializer<T>
        {
            public static readonly XmlSerializer Instance = new(typeof(T));
        }
    }
}
