using System.Text;

namespace FrameworkContainers.Format
{
    /// <summary>Serialize options for the XmlSerializer.</summary>
    public readonly struct XmlWriteOptions
    {
        internal static readonly XmlWriteOptions Default = new XmlWriteOptions(true, true, Encoding.Unicode);

        /// <summary>Whether or not to remove the default XML namespaces from the output.</summary>
        public bool RemoveDefaultXmlNamespaces { get; }

        /// <summary>Whether or not to omit the XML declaration from the output.</summary>
        public bool OmitXmlDeclaration { get; }

        /// <summary>The character encoding to use.</summary>
        public Encoding Encoding { get; }

        /// <summary>Serialize options for the XmlSerializer.</summary>
        /// <param name="removeDefaultXmlNamespaces">Whether or not to remove the default XML namespaces from the output.</param>
        /// <param name="omitXmlDeclaration">Whether or not to omit the XML declaration from the output.</param>
        /// <param name="encoding">The character encoding to use.</param>
        public XmlWriteOptions(bool removeDefaultXmlNamespaces, bool omitXmlDeclaration, Encoding encoding)
        {
            RemoveDefaultXmlNamespaces = removeDefaultXmlNamespaces;
            OmitXmlDeclaration = omitXmlDeclaration;
            Encoding = encoding;
        }
    }

    /// <summary>Deserialize options for the XmlSerializer.</summary>
    public readonly struct XmlReadOptions
    {
        internal static readonly XmlReadOptions Default = new XmlReadOptions(Encoding.Unicode);

        /// <summary>The character encoding to use.</summary>
        public Encoding Encoding { get; }

        /// <summary>Deserialize options for the XmlSerializer.</summary>
        /// <param name="encoding">The character encoding to use.</param>
        public XmlReadOptions(Encoding encoding)
        {
            Encoding = encoding;
        }
    }
}
