﻿using ContainerExpressions.Containers;
using FrameworkContainers.Format.JsonCollective.Models;
using FrameworkContainers.Models;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace FrameworkContainers.Network.HttpCollective.Models
{
    public readonly struct HttpOptions
    {
        internal static readonly HttpOptions Default = new HttpOptions(timeoutSeconds: Constants.Http.TIMEOUT_SECONDS, json: JsonOptions.Default, webClient: WebClientOptions.Default, log: null);

        /// <summary>When true, the http status is returned; instead of the response body.</summary>
        internal bool RetrieveHttpStatus { get; }

        /// <summary>The time to wait (in seconds) before giving up on a HTTP response.</summary>
        internal int TimeoutSeconds { get; }

        /// <summary>Json options to use when serializing / deserializing is required.</summary>
        internal JsonOptions Json { get; }

        /// <summary>Options to use on the underlying HttpClient (when applicable - i.e. mostly on async functions).</summary>
        internal WebClientOptions WebClient { get; }

        /// <summary>A custom function to use when logging the http request, and response (null when not set).</summary>
        internal Func<HttpRequestBody, HttpResponseBody, ValueTask> Log { get; }

        public HttpOptions(int timeoutSeconds = default, JsonOptions json = null, WebClientOptions webClient = null, HttpLogger log = null)
        {
            var seconds = timeoutSeconds.ThrowIfLessThan(default).ThrowIfGreaterThan(Constants.Http.MAX_TIMEOUT_SECONDS);

            TimeoutSeconds = seconds == default ? Constants.Http.TIMEOUT_SECONDS : seconds;
            Json = json ?? JsonOptions.Default;
            WebClient = webClient ?? WebClientOptions.Default;
            RetrieveHttpStatus = false;
            Log = log?.Value.Match(
                static left => new Func<HttpRequestBody, HttpResponseBody, ValueTask>((request, response) =>
                {
                    Try.Run(() => left(request, response), "Error calling custom http logger.");
                    return new ValueTask();
                }),
                static right => new Func<HttpRequestBody, HttpResponseBody, ValueTask>((request, response) =>
                {
                    return new ValueTask(Try.RunAsync(() => right(request, response), "Error calling custom http logger."));
                })
            );
        }

        internal HttpOptions(HttpOptions options, bool retrieveHttpStatus)
        {
            TimeoutSeconds = options;
            Json = options;
            WebClient = options.WebClient;
            RetrieveHttpStatus = retrieveHttpStatus;
            Log = options.Log;
        }

        // Cast from HttpOptions (for internal use).
        public static implicit operator JsonSerializerOptions(HttpOptions options) => options.Json.SerializerSettings;
        public static implicit operator JsonOptions(HttpOptions options) => options.Json;
        public static implicit operator int(HttpOptions options) => options.TimeoutSeconds;
        public static implicit operator bool(HttpOptions options) => options.RetrieveHttpStatus;

        // Cast to HttpOptions (for external use).
        public static implicit operator HttpOptions(int seconds) => new HttpOptions(seconds, JsonOptions.Default, WebClientOptions.Default, null);
        public static implicit operator HttpOptions(JsonOptions options) => new HttpOptions(Constants.Http.TIMEOUT_SECONDS, options, WebClientOptions.Default, null);
        public static implicit operator HttpOptions(WebClientOptions options) => new HttpOptions(Constants.Http.TIMEOUT_SECONDS, JsonOptions.Default, options, null);
        public static implicit operator HttpOptions(HttpLogger log) => new HttpOptions(Constants.Http.TIMEOUT_SECONDS, JsonOptions.Default, WebClientOptions.Default, log);
    }
}
