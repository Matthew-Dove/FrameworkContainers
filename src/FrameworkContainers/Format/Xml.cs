using FrameworkContainers.Models;
using FrameworkContainers.Models.Exceptions;
using System;
using System.IO;
using System.Text;
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
                ExtensibleMarkupLanguage.DeserializeError(ex, typeof(T), xml);
            }
            return default;
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
                ExtensibleMarkupLanguage.SerializeError(ex, model);
            }
            return default;
        }
    }




    /* -------------------------- TODO --------------------------

    > Update `Http` to use static throwing methods (and `HttpMaybe`).
    > Add encoding options for `Xml.ToModel<T>()`.
    > Split XMLOptions to read (deserialize) / write (serialize) configs.
    > Update `Response` to accept the new XML configs.
    > Update `Maybe` to accept the new XML configs.
    > Update `Client` to accept the new XML configs.

    ** -------------------------- TODO -------------------------- */




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
        /// <param name="xml">The XML string to deserialize from.</param>
        /// <returns>An object instance.</returns>
        public static T XmlToModel<T>(string xml)
        {
            using (var stream = new StringStream(xml))
            using (var reader = XmlDictionaryReader.CreateTextReader(stream, Encoding.Unicode, new XmlDictionaryReaderQuotas { MaxDepth = 32 }, OnClose))
            {
                var serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(reader);
            }
        }

        private static void OnClose(XmlDictionaryReader _) { }
    }

    /// <summary>Overrides the base "StringWriter" class to accept different character encoding types.</summary>
    internal sealed class StringWriterWithEncoding : StringWriter
    {
        /// <summary>Overrides the default encoding type (UTF-16).</summary>
        public override Encoding Encoding => _encoding ?? base.Encoding;
        private readonly Encoding _encoding;

        public StringWriterWithEncoding() { }

        /// <summary>Constructor that accepts a character encoding type.</summary>
        /// <param name="encoding">The character encoding type</param>
        public StringWriterWithEncoding(Encoding encoding) => _encoding = encoding;
    }

    /// <summary>
    /// Converts a string to a stream, without allocating the string a second time.
    /// <para>If you get into trouble, you can use this simplifed one below; but it will double allocate the string value.</para>
    /// <para>StringStream : MemoryStream { public StringStream(string s) : base(Encoding.Unicode.GetBytes(s), false) { } }</para>
    /// </summary>
    internal sealed class StringStream : Stream
    {
        public override bool CanRead { get; } = true;
        public override bool CanSeek { get; } = true;
        public override bool CanWrite { get; } = false;
        public override long Length { get { return _byteLength; } }
        public override long Position { get { return _position; } set { _position = (int)value; } }

        private readonly string _str;
        private readonly long _byteLength;
        private int _position;

        public StringStream(string str)
        {
            _str = str;
            _byteLength = _str.Length * 2;
            _position = 0;
        }

        // This stream is readonly.
        public override void Write(byte[] buffer, int offset, int count) { }
        public override void Flush() { }
        public override void SetLength(long value) { }

        public override int Read(byte[] buffer, int offset, int count)
        {
            int bytesRead = 0;
            while (bytesRead < count)
            {
                if (_position >= _byteLength) return bytesRead;
                char c = _str[_position / 2];
                buffer[offset + bytesRead] = (byte)((_position % 2 == 0) ? c & 0xFF : (c >> 8) & 0xFF);
                Position++; bytesRead++;
            }
            return bytesRead;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            switch (origin)
            {
                case SeekOrigin.Begin: Position = offset; break;
                case SeekOrigin.Current: Position = Position + offset; break;
                case SeekOrigin.End: Position = _byteLength + offset; break;
            }
            return Position;
        }
    }
}
