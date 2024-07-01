using System.Threading;

namespace FrameworkContainers.Network.HttpCollective.Models
{
    /// <summary>Options for the web client used (i.e. HttpClient).</summary>
    public sealed class WebClientOptions
    {
        internal static readonly WebClientOptions Default = new WebClientOptions(cancellationToken: CancellationToken.None, httpClient: null);

        /// <summary>The cancellation token to cancel an operation.</summary>
        internal CancellationToken CancellationToken { get; }

        /// <summary>A preconfigured HttpClient to use, instead of the internal one.</summary>
        internal System.Net.Http.HttpClient HttpClient { get; }

        public WebClientOptions(CancellationToken cancellationToken) : this(cancellationToken, null) { }

        public WebClientOptions(System.Net.Http.HttpClient httpClient) : this(CancellationToken.None, httpClient) { }

        public WebClientOptions(CancellationToken cancellationToken, System.Net.Http.HttpClient httpClient)
        {
            CancellationToken = cancellationToken; // Set to "none" when not provided.
            HttpClient = httpClient; // Set to "null" when not provided.
        }
    }
}
