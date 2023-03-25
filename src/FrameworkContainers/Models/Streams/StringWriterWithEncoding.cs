using System.IO;
using System.Text;

namespace FrameworkContainers.Models.Streams
{
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
}
