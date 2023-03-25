using System.IO;

namespace FrameworkContainers.Models.Streams
{
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
