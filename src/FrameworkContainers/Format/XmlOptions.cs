using System.IO;
using System.Text;

namespace FrameworkContainers.Format
{
    /// <summary>Options for the XmlSerializer.</summary>
    public readonly struct XmlOptions
    {
        internal static readonly XmlOptions Default = new XmlOptions(true, true, null);

        /// <summary>Whether or not to remove the default XML namespaces from the output.</summary>
        public bool RemoveDefaultXmlNamespaces { get; }

        /// <summary>Whether or not to omit the XML declaration from the output.</summary>
        public bool OmitXmlDeclaration { get; }

        /// <summary>The character encoding to use.</summary>
        public Encoding Encoding { get; }

        /// <summary>Options for the XmlSerializer.</summary>
        /// <param name="removeDefaultXmlNamespaces">Whether or not to remove the default XML namespaces from the output.</param>
        /// <param name="omitXmlDeclaration">Whether or not to omit the XML declaration from the output.</param>
        /// <param name="encoding">The character encoding to use.</param>
        public XmlOptions(bool removeDefaultXmlNamespaces, bool omitXmlDeclaration, Encoding encoding)
        {
            RemoveDefaultXmlNamespaces = removeDefaultXmlNamespaces;
            OmitXmlDeclaration = omitXmlDeclaration;
            Encoding = encoding;
        }
    }

    /// <summary>Overrides the base "StringWriter" class to accept different character encoding types.</summary>
    public sealed class StringWriterWithEncoding : StringWriter
    {
        /// <summary>Overrides the default encoding type (UTF-16).</summary>
        public override Encoding Encoding => _encoding ?? base.Encoding;
        private readonly Encoding _encoding;

        public StringWriterWithEncoding() { }

        /// <summary>Constructor that accepts a character encoding type.</summary>
        /// <param name="encoding">The character encoding type</param>
        public StringWriterWithEncoding(Encoding encoding) => _encoding = encoding;
    }
}
