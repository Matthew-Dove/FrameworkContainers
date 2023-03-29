using FrameworkContainers.Format;
using FrameworkContainers.Models;
using System;

namespace FrameworkContainers.Network
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

        public HttpOptions(int timeoutSeconds = 0, JsonOptions json = null)
        {
            if (timeoutSeconds < 0 || timeoutSeconds >= 14400) throw new ArgumentOutOfRangeException(nameof(timeoutSeconds));

            TimeoutSeconds = timeoutSeconds == 0 ? Constants.Http.TIMEOUT_SECONDS : timeoutSeconds;
            Json = json ?? JsonOptions.Default;
            RetrieveHttpStatus = false;
        }

        internal HttpOptions(HttpOptions options, bool retrieveHttpStatus)
        {
            TimeoutSeconds = options;
            Json = options;
            RetrieveHttpStatus = retrieveHttpStatus;
        }

        public static implicit operator JsonOptions(HttpOptions options) => options.Json;
        public static implicit operator int(HttpOptions options) => options.TimeoutSeconds;
        public static implicit operator bool(HttpOptions options) => options.RetrieveHttpStatus;
    }
}
