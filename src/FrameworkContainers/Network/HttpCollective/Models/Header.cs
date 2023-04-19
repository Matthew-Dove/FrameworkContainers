using System;

namespace FrameworkContainers.Network.HttpCollective.Models
{
    public readonly struct Header
    {
        public string Key { get; }
        public string Value { get; }

        public Header(string key, string value)
        {
            Key = key.ThrowIfNullOrEmpty(nameof(key));
            Value = value.ThrowIfNullOrEmpty(nameof(value));
        }
    }

    file static class HeaderExtensions
    {
        public static string ThrowIfNullOrEmpty(this string target, string name)
        {
            if (string.IsNullOrEmpty(target)) throw new ArgumentOutOfRangeException(name);
            return target;
        }
    }
}
