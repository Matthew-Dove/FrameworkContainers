using ContainerExpressions.Containers;
using FrameworkContainers.Models.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FrameworkContainers.Network
{
    public static class Http
    {
        public static readonly HttpResponse Response = new HttpResponse();

        public static readonly HttpMaybe Maybe = new HttpMaybe();

        public static string Post(string body, string url, string contentType, params Header[] headers)
        {
            return Post(body, url, contentType, HttpOptions.Default, headers);
        }

        public static string Post(string body, string url, string contentType, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.Send(body, url, contentType, options, headers, "POST").Match(response => response, ex => throw ex);
        }

        public static string Put(string body, string url, string contentType, params Header[] headers)
        {
            return Put(body, url, contentType, HttpOptions.Default, headers);
        }

        public static string Put(string body, string url, string contentType, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.Send(body, url, contentType, options, headers, "PUT").Match(response => response, ex => throw ex);
        }

        public static string Get(string url, params Header[] headers)
        {
            return Get(url, HttpOptions.Default, headers);
        }

        public static string Get(string url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.Send(string.Empty, url, string.Empty, options, headers, "GET").Match(response => response, ex => throw ex);
        }

        public static string Delete(string url, params Header[] headers)
        {
            return Delete(url, HttpOptions.Default, headers);
        }

        public static string Delete(string url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.Send(string.Empty, url, string.Empty, options, headers, "DELETE").Match(response => response, ex => throw ex);
        }

        public static Task<string> PostAsync(string body, string url, string contentType, params Header[] headers)
        {
            return PostAsync(body, url, contentType, HttpOptions.Default, headers);
        }

        public static Task<string> PostAsync(string body, string url, string contentType, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.SendAsync(body, url, contentType, options, headers, HttpMethod.Post).ContinueWith(x => x.Result.Match(response => response, ex => throw ex));
        }

        public static Task<string> PutAsync(string body, string url, string contentType, params Header[] headers)
        {
            return PutAsync(body, url, contentType, HttpOptions.Default, headers);
        }

        public static Task<string> PutAsync(string body, string url, string contentType, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.SendAsync(body, url, contentType, options, headers, HttpMethod.Put).ContinueWith(x => x.Result.Match(response => response, ex => throw ex));
        }

        public static Task<string> GetAsync(string url, params Header[] headers)
        {
            return GetAsync(url, HttpOptions.Default, headers);
        }

        public static Task<string> GetAsync(string url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.SendAsync(string.Empty, url, string.Empty, options, headers, HttpMethod.Get).ContinueWith(x => x.Result.Match(response => response, ex => throw ex));
        }

        public static Task<string> DeleteAsync(string url, params Header[] headers)
        {
            return DeleteAsync( url, HttpOptions.Default, headers);
        }

        public static Task<string> DeleteAsync(string url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.SendAsync(string.Empty, url, string.Empty, options, headers, HttpMethod.Delete).ContinueWith(x => x.Result.Match(response => response, ex => throw ex));
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

        public static Either<string, HttpException> Send(string body, string url, string contentType, HttpOptions options, Header[] headers, string httpMethod)
        {
            var response = new Either<string, HttpException>(string.Empty);

            try
            {
                var request = WebRequest.Create(url);
                request.Method = httpMethod;
                request.Timeout = options.TimeoutSeconds * 1000;
                foreach (var header in headers) request.Headers.Add(header.Key, header.Value);
                if (("POST".Equals(httpMethod) || "PUT".Equals(httpMethod)) && !string.IsNullOrEmpty(body))
                {
                    var data = Encoding.UTF8.GetBytes(body);
                    request.ContentType = contentType;
                    request.ContentLength = data.Length;
                    using (var requestStream = request.GetRequestStream())
                    {
                        requestStream.Write(data, 0, data.Length);
                    }
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
                response = new HttpException($"Error calling {httpMethod}: [{url}].", statusCode, responseContent, we, responseheaders);
            }
            catch (Exception ex)
            {
                response = new HttpException($"Error calling {httpMethod}: [{url}].", 504, string.Empty, ex, new Header[0]);
            }

            return response;
        }

        public static async Task<Either<string, HttpException>> SendAsync(string body, string url, string contentType, HttpOptions options, Header[] headers, HttpMethod httpMethod)
        {
            var response = new Either<string, HttpException>(string.Empty);

            using (var cts = new CancellationTokenSource(options.TimeoutSeconds * 1000))
            using (var httpRequest = new HttpRequestMessage(httpMethod, url))
            {
                AddDnsRenew(httpRequest.RequestUri);
                foreach (var header in headers) httpRequest.Headers.Add(header.Key, header.Value);
                if ((httpMethod == HttpMethod.Post || httpMethod == HttpMethod.Put) && !string.IsNullOrEmpty(body))
                {
                    httpRequest.Content = new StringContent(body, Encoding.UTF8, contentType);
                }
                using (var httpResponse = await _client.SendAsync(httpRequest, HttpCompletionOption.ResponseHeadersRead, cts.Token).ContinueWith(x => new HttpTimeoutResult(x)))
                {
                    if (httpResponse.IsComplete && httpResponse.Message.IsSuccessStatusCode && httpResponse.Message.Content is object)
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
                        response = new HttpException($"Error calling {httpMethod.Method}: [{url}].", rawStatusCode, rawBody, null, rawHeaders);
                    }
                }
            }

            return response;
        }
    }
}
