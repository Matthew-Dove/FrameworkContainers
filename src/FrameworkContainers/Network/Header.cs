using System;

namespace FrameworkContainers.Network
{
    public readonly struct Header
    {
        public string Key { get; }
        public string Value { get; }

        public Header(string key, string value)
        {
            if (string.IsNullOrEmpty(key)) throw new ArgumentOutOfRangeException(nameof(key));
            if (string.IsNullOrEmpty(value)) throw new ArgumentOutOfRangeException(nameof(value));

            Key = key;
            Value = value;
        }
    }
}
