﻿using ContainerExpressions.Containers;
using FrameworkContainers.Models.Exceptions;
using FrameworkContainers.Models.Network;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkContainers.Network
{
    public static class Http
    {
        public static string Post(string body, string url, string mediaType, params Header[] headers)
        {
            return HypertextTransferProtocol.Post(body, url, mediaType, headers).Match(response => response, ex => throw ex);
        }

        public static Task<string> PostAsync(string body, string url, string mediaType, params Header[] headers)
        {
            return HypertextTransferProtocol.PostAsync(body, url, mediaType, headers).ContinueWith(x => x.Result.Match(response => response, ex => throw ex));
        }
    }

    internal static class HypertextTransferProtocol
    {
        private static readonly int DNS_RENEW_MILLISECONDS = (int)TimeSpan.FromMinutes(5).TotalMilliseconds;
        private readonly static HttpClient _client = new HttpClient();
        private readonly static HashSet<Uri> _uris = new HashSet<Uri>();
        private readonly static object _lock = new object();

        static HypertextTransferProtocol()
        {
            ServicePointManager.DefaultConnectionLimit = int.MaxValue;
            ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;
            ServicePointManager.SecurityProtocol &= ~SecurityProtocolType.Ssl3;
            ServicePointManager.SecurityProtocol &= ~SecurityProtocolType.Tls;
            ServicePointManager.SecurityProtocol &= ~SecurityProtocolType.Tls11;
        }

        private static void AddDnsRenew(Uri uri)
        {
            if (!_uris.Contains(uri))
            {
                lock (_lock)
                {
                    if (!_uris.Contains(uri))
                    {
                        ServicePointManager.FindServicePoint(uri).ConnectionLeaseTimeout = DNS_RENEW_MILLISECONDS;
                        _uris.Add(uri);
                    }
                }
            }
        }

        public static Either<string, HttpException> Post(string body, string url, string mediaType, params Header[] headers)
        {
            var response = new Either<string, HttpException>(string.Empty);

            try
            {
                var data = Encoding.UTF8.GetBytes(body);
                var request = WebRequest.Create(url);
                request.Method = "POST";
                request.ContentType = mediaType;
                request.ContentLength = data.Length;
                foreach (var header in headers) request.Headers.Add(header.Key, header.Value);
                using (var requestStream = request.GetRequestStream())
                {
                    requestStream.Write(data, 0, data.Length);
                }
                using (var webResponse = request.GetResponse())
                using (var responseStream = webResponse.GetResponseStream())
                using (var sr = new StreamReader(responseStream))
                {
                    response = sr.ReadToEnd();
                }
            }
            catch (WebException we) when (we.Response is HttpWebResponse httpResponse)
            {
                var responseheaders = new Header[httpResponse.Headers.Count];
                for (int i = 0; i < httpResponse.Headers.Count; i++) responseheaders[i] = new Header(httpResponse.Headers.Keys[i], httpResponse.Headers[i]);
                var statusCode = (int)httpResponse.StatusCode;
                var responseContent = string.Empty;
                using (var sr = new StreamReader(httpResponse.GetResponseStream())) { responseContent = sr.ReadToEnd(); }
                httpResponse.Dispose();
                response = new HttpException($"Error calling POST: [{url}].", statusCode, responseContent, we, responseheaders);
            }
            catch (Exception ex)
            {
                response = new HttpException($"Error calling POST: [{url}].", 504, string.Empty, ex, new Header[0]);
            }

            return response;
        }

        public static async Task<Either<string, HttpException>> PostAsync(string body, string url, string mediaType, params Header[] headers)
        {
            var response = new Either<string, HttpException>(string.Empty);

            using (var httpRequest = new HttpRequestMessage(HttpMethod.Post, url) { Content = new StringContent(body, Encoding.UTF8, mediaType) })
            {
                AddDnsRenew(httpRequest.RequestUri);
                foreach (var header in headers) httpRequest.Headers.Add(header.Key, header.Value);
                using (var httpResponse = await _client.SendAsync(httpRequest, HttpCompletionOption.ResponseHeadersRead).ContinueWith(x => new HttpTimeoutResult(x)))
                {
                    if (httpResponse.IsComplete && httpResponse.Message.IsSuccessStatusCode && httpResponse.Message.Content is object && httpResponse.Message.Content.Headers.ContentType.MediaType == mediaType)
                    {
                        var raw = await httpResponse.Message.Content.ReadAsStringAsync().ContinueWith(x => new SocketTimeoutResult(x));
                        response = raw.IsComplete ? raw.Body : string.Empty;
                    }
                    else
                    {
                        var rawStatusCode = ((int?)httpResponse.Message?.StatusCode).GetValueOrDefault(504);
                        var rawBody = await httpResponse.TryGetBody();
                        var rawHeaders = new Header[0];
                        if ((httpResponse.Message?.Headers?.Any()).GetValueOrDefault(false))
                        {
                            rawHeaders = httpResponse.Message.Headers.Select(x => new Header(x.Key, x.Value.First())).ToArray();
                        }
                        response = new HttpException($"Error calling POST: [{url}].", rawStatusCode, rawBody, null, rawHeaders);
                    }
                }
            }

            return response;
        }
    }
}
