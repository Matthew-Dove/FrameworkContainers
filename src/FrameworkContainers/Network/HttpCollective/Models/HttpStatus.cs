using ContainerExpressions.Containers;
using FrameworkContainers.Models;
using System;

namespace FrameworkContainers.Network.HttpCollective.Models
{
    /// <summary>
    /// A description of the http status code sent from the server.
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
    public sealed class HttpStatus : Alias<string> {
        internal HttpStatus(string value) : base(string.IsNullOrEmpty(value) ? Constants.Http.DEFAULT_HTTP_DESCRIPTION : value) { }
    }

    /// <summary>Use this url alias when you'd like to retrieve the http status code, instead of the server's response body.</summary>
    public sealed class HttpStatusUrl : Alias<Uri> {
        public HttpStatusUrl(string value) : base(new Uri(value)) { }
    }
}
