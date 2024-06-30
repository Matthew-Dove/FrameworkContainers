using ContainerExpressions.Containers.Extensions;

namespace FrameworkContainers.Network.HttpCollective.Models
{
    public readonly struct Header
    {
        public string Key { get; }
        public string Value { get; }

        public Header(string key, string value)
        {
            Key = key.ThrowIfNullOrEmpty();
            Value = value.ThrowIfNullOrEmpty();
        }
    }
}
