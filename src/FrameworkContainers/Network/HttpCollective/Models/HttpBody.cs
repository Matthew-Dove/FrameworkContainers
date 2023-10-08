using ContainerExpressions.Containers;

namespace FrameworkContainers.Network.HttpCollective.Models
{
    /// <summary>The plaintext returned from the upstream server.</summary>
    public sealed class HttpBody : Alias<string>
    {
        internal HttpBody(string value) : base(string.IsNullOrEmpty(value) ? string.Empty : value) { }
        internal static HttpBody Create(string value) => new HttpBody(value);
    }
}
