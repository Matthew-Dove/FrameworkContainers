using FrameworkContainers.Format.JsonCollective.Models;
using FrameworkContainers.Models;
using System.Text.Json;

namespace FrameworkContainers.Network.HttpCollective.Models
{
    public readonly struct HttpOptions
    {
        internal static HttpOptions Default => new HttpOptions(timeoutSeconds: Constants.Http.TIMEOUT_SECONDS, json: JsonOptions.Default);

        /// <summary>The time to wait (in seconds) before giving up on a HTTP response.</summary>
        public int TimeoutSeconds { get; }

        /// <summary>Json options to use when serializing / deserializing is required.</summary>
        public JsonOptions Json { get; }

        /// <summary>When true, the http status is returned; instead of the response body.</summary>
        internal bool RetrieveHttpStatus { get; }

        public HttpOptions(int timeoutSeconds = default, JsonOptions json = null)
        {
            if (timeoutSeconds < 0 || timeoutSeconds >= 14400) ArgumentOutOfRangeException(nameof(timeoutSeconds));

            TimeoutSeconds = timeoutSeconds == default ? Constants.Http.TIMEOUT_SECONDS : timeoutSeconds;
            Json = json ?? JsonOptions.Default;
            RetrieveHttpStatus = false;
        }

        internal HttpOptions(HttpOptions options, bool retrieveHttpStatus)
        {
            TimeoutSeconds = options;
            Json = options;
            RetrieveHttpStatus = retrieveHttpStatus;
        }

        // Cast from HttpOptions.
        public static implicit operator JsonSerializerOptions(HttpOptions options) => options.Json.SerializerSettings;
        public static implicit operator JsonOptions(HttpOptions options) => options.Json;
        public static implicit operator int(HttpOptions options) => options.TimeoutSeconds;
        public static implicit operator bool(HttpOptions options) => options.RetrieveHttpStatus;

        // Cast to HttpOptions.
        public static implicit operator HttpOptions(JsonOptions options) => new HttpOptions(Constants.Http.TIMEOUT_SECONDS, options);
        public static implicit operator HttpOptions(int seconds) => new HttpOptions(seconds, JsonOptions.Default);
    }
}
