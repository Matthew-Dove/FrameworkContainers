using ContainerExpressions.Containers;
using FrameworkContainers.Models;
using System;
using System.Threading.Tasks;

namespace FrameworkContainers.Network.HttpCollective.Models
{
    /// <summary>
    /// A description of the http status code sent from the upstream server.
    /// <list type="table">
    ///     <listheader>
    ///         <term>Code</term>
    ///         <description>Description</description>
    ///     </listheader>
    ///     <item>
    ///         <term>200</term>
    ///         <description>OK</description>
    ///     </item>
    ///     <item>
    ///         <term>204</term>
    ///         <description>No Content</description>
    ///     </item>
    ///     <item>
    ///         <term>400</term>
    ///         <description>Bad Request</description>
    ///     </item>
    ///     <item>
    ///         <term>401</term>
    ///         <description>Unauthorized</description>
    ///     </item>
    ///     <item>
    ///         <term>500</term>
    ///         <description>Internal Server Error</description>
    ///     </item>
    ///     <item>
    ///         <term>504</term>
    ///         <description>Gateway Timeout</description>
    ///     </item>
    /// </list>
    /// </summary>
    public sealed class HttpStatus : Alias<string>
    {
        public HttpStatus(string value) : base(string.IsNullOrEmpty(value) ? Constants.Http.DEFAULT_HTTP_DESCRIPTION : value) { }
        public static HttpStatus Create(string value) => new HttpStatus(value);
    }

    /// <summary>The plaintext returned from the upstream server.</summary>
    public sealed class HttpBody : Alias<string>
    {
        public HttpBody(string value) : base(string.IsNullOrEmpty(value) ? string.Empty : value) { }
        public static HttpBody Create(string value) => new HttpBody(value);
    }

    /// <summary>The request body sent to the upstream server.</summary>
    public sealed class HttpRequestBody : Alias<string>
    {
        public HttpRequestBody(string value) : base(string.IsNullOrEmpty(value) ? string.Empty : value) { }
        public static HttpRequestBody Create(string value) => new HttpRequestBody(value);
    }

    /// <summary>The response body returned from the upstream server.</summary>
    public sealed class HttpResponseBody : Alias<string>
    {
        public HttpResponseBody(string value) : base(string.IsNullOrEmpty(value) ? string.Empty : value) { }
        public static HttpResponseBody Create(string value) => new HttpResponseBody(value);
    }

    /// <summary>A custom function to use when logging the http request, and response.</summary>
    public sealed class HttpLogger : Alias<Func<HttpRequestBody, HttpResponseBody, ValueTask>>
    {
        public HttpLogger(Func<HttpRequestBody, HttpResponseBody, ValueTask> value) : base(value.ThrowIfNull("Custom http logger cannot be null.")) { }
        public static HttpLogger Create(Func<HttpRequestBody, HttpResponseBody, ValueTask> value) => new HttpLogger(value);
    }
}
