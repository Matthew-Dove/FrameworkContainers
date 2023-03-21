using System.Text;

namespace FrameworkContainers.Format
{
    /// <summary>Options for the XmlSerializer.</summary>
    public readonly struct XmlOptions
    {
        internal static readonly XmlOptions Default = new XmlOptions(true, true, Encoding.Unicode);

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
}
