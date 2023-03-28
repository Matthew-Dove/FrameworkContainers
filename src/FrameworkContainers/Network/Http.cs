using ContainerExpressions.Containers;
using FrameworkContainers.Format;
using FrameworkContainers.Models;
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
        public static readonly HttpResponse Response = HttpResponse.Instance;

        public static readonly HttpMaybe Maybe = HttpMaybe.Instance;

        private static Func<string, T> Parse<T>(JsonOptions options) { return json => Json.ToModel<T>(json, options); }

        private static Func<Task<Either<string, HttpException>>, T> ParseAsync<T>(JsonOptions options) { return x => x.Result.Match(Parse<T>(options), static ex => throw ex); }

        public static string Post(string body, string url, string contentType, params Header[] headers)
        {
            return Post(body, url, contentType, HttpOptions.Default, headers);
        }

        public static string Post(string body, string url, string contentType, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.Send(body, url, contentType, options, headers, Constants.Http.POST).Match(static x => x, static ex => throw ex);
        }

        public static TResponse PostJson<TRequest, TResponse>(TRequest model, string url, params Header[] headers)
        {
            return PostJson<TRequest, TResponse>(model, url, HttpOptions.Default, headers);
        }

        public static TResponse PostJson<TRequest, TResponse>(TRequest model, string url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.Send(Json.FromModel(model, options), url, Constants.Http.JSON_CONTENT, options, AddJsonHeaders(headers), Constants.Http.POST).Match(Parse<TResponse>(options), static ex => throw ex);
        }

        public static string Put(string body, string url, string contentType, params Header[] headers)
        {
            return Put(body, url, contentType, HttpOptions.Default, headers);
        }

        public static string Put(string body, string url, string contentType, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.Send(body, url, contentType, options, headers, Constants.Http.PUT).Match(static x => x, static ex => throw ex);
        }

        public static TResponse PutJson<TRequest, TResponse>(TRequest model, string url, params Header[] headers)
        {
            return PutJson<TRequest, TResponse>(model, url, HttpOptions.Default, headers);
        }

        public static TResponse PutJson<TRequest, TResponse>(TRequest model, string url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.Send(Json.FromModel(model, options), url, Constants.Http.JSON_CONTENT, options, AddJsonHeaders(headers), Constants.Http.PUT).Match(Parse<TResponse>(options), static ex => throw ex);
        }

        public static string Get(string url, params Header[] headers)
        {
            return Get(url, HttpOptions.Default, headers);
        }

        public static string Get(string url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.Send(string.Empty, url, string.Empty, options, headers, Constants.Http.GET).Match(static x => x, static ex => throw ex);
        }

        public static TResponse GetJson<TResponse>(string url, params Header[] headers)
        {
            return GetJson<TResponse>(url, HttpOptions.Default, headers);
        }

        public static TResponse GetJson<TResponse>(string url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.Send(string.Empty, url, string.Empty, options, AddJsonHeaders(headers), Constants.Http.GET).Match(Parse<TResponse>(options), static ex => throw ex);
        }

        public static string Delete(string url, params Header[] headers)
        {
            return Delete(url, HttpOptions.Default, headers);
        }

        public static string Delete(string url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.Send(string.Empty, url, string.Empty, options, headers, Constants.Http.DELETE).Match(static x => x, static ex => throw ex);
        }

        public static TResponse DeleteJson<TResponse>(string url, params Header[] headers)
        {
            return DeleteJson<TResponse>(url, HttpOptions.Default, headers);
        }

        public static TResponse DeleteJson<TResponse>(string url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.Send(string.Empty, url, string.Empty, options, AddJsonHeaders(headers), Constants.Http.DELETE).Match(Parse<TResponse>(options), static ex => throw ex);
        }

        public static string Patch(string body, string url, string contentType, params Header[] headers)
        {
            return Patch(body, url, contentType, HttpOptions.Default, headers);
        }

        public static string Patch(string body, string url, string contentType, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.Send(body, url, contentType, options, headers, Constants.Http.PATCH).Match(static x => x, static ex => throw ex);
        }

        public static TResponse PatchJson<TRequest, TResponse>(TRequest model, string url, params Header[] headers)
        {
            return PatchJson<TRequest, TResponse>(model, url, HttpOptions.Default, headers);
        }

        public static TResponse PatchJson<TRequest, TResponse>(TRequest model, string url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.Send(Json.FromModel(model, options), url, Constants.Http.JSON_CONTENT, options, AddJsonHeaders(headers), Constants.Http.PATCH).Match(Parse<TResponse>(options), static ex => throw ex);
        }

        public static Task<string> PostAsync(string body, string url, string contentType, params Header[] headers)
        {
            return PostAsync(body, url, contentType, HttpOptions.Default, headers);
        }

        public static Task<string> PostAsync(string body, string url, string contentType, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.SendAsync(body, url, contentType, options, headers, HttpMethod.Post).ContinueWith(static x => x.Result.Match(static y => y, static ex => throw ex));
        }

        public static Task<TResponse> PostJsonAsync<TRequest, TResponse>(TRequest model, string url, params Header[] headers)
        {
            return PostJsonAsync<TRequest, TResponse>(model, url, HttpOptions.Default, headers);
        }

        public static Task<TResponse> PostJsonAsync<TRequest, TResponse>(TRequest model, string url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.SendAsync(Json.FromModel(model, options), url, Constants.Http.JSON_CONTENT, options, AddJsonHeaders(headers), HttpMethod.Post).ContinueWith(ParseAsync<TResponse>(options));
        }

        public static Task<string> PutAsync(string body, string url, string contentType, params Header[] headers)
        {
            return PutAsync(body, url, contentType, HttpOptions.Default, headers);
        }

        public static Task<string> PutAsync(string body, string url, string contentType, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.SendAsync(body, url, contentType, options, headers, HttpMethod.Put).ContinueWith(static x => x.Result.Match(static y => y, static ex => throw ex));
        }

        public static Task<TResponse> PutJsonAsync<TRequest, TResponse>(TRequest model, string url, params Header[] headers)
        {
            return PutJsonAsync<TRequest, TResponse>(model, url, HttpOptions.Default, headers);
        }

        public static Task<TResponse> PutJsonAsync<TRequest, TResponse>(TRequest model, string url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.SendAsync(Json.FromModel(model, options), url, Constants.Http.JSON_CONTENT, options, AddJsonHeaders(headers), HttpMethod.Put).ContinueWith(ParseAsync<TResponse>(options));
        }

        public static Task<string> GetAsync(string url, params Header[] headers)
        {
            return GetAsync(url, HttpOptions.Default, headers);
        }

        public static Task<string> GetAsync(string url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.SendAsync(string.Empty, url, string.Empty, options, headers, HttpMethod.Get).ContinueWith(static x => x.Result.Match(static y => y, static ex => throw ex));
        }

        public static Task<TResponse> GetJsonAsync<TResponse>(string url, params Header[] headers)
        {
            return GetJsonAsync<TResponse>(url, HttpOptions.Default, headers);
        }

        public static Task<TResponse> GetJsonAsync<TResponse>(string url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.SendAsync(string.Empty, url, string.Empty, options, AddJsonHeaders(headers), HttpMethod.Get).ContinueWith(ParseAsync<TResponse>(options));
        }

        public static Task<string> DeleteAsync(string url, params Header[] headers)
        {
            return DeleteAsync(url, HttpOptions.Default, headers);
        }

        public static Task<string> DeleteAsync(string url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.SendAsync(string.Empty, url, string.Empty, options, headers, HttpMethod.Delete).ContinueWith(static x => x.Result.Match(static y => y, static ex => throw ex));
        }

        public static Task<TResponse> DeleteJsonAsync<TResponse>(string url, params Header[] headers)
        {
            return DeleteJsonAsync<TResponse>(url, HttpOptions.Default, headers);
        }

        public static Task<TResponse> DeleteJsonAsync<TResponse>(string url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.SendAsync(string.Empty, url, string.Empty, options, AddJsonHeaders(headers), HttpMethod.Delete).ContinueWith(ParseAsync<TResponse>(options));
        }

        public static Task<string> PatchAsync(string body, string url, string contentType, params Header[] headers)
        {
            return PatchAsync(body, url, contentType, HttpOptions.Default, headers);
        }

        public static Task<string> PatchAsync(string body, string url, string contentType, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.SendAsync(body, url, contentType, options, headers, HypertextTransferProtocol.Patch).ContinueWith(static x => x.Result.Match(static y => y, static ex => throw ex));
        }

        public static Task<TResponse> PatchJsonAsync<TRequest, TResponse>(TRequest model, string url, params Header[] headers)
        {
            return PatchJsonAsync<TRequest, TResponse>(model, url, HttpOptions.Default, headers);
        }

        public static Task<TResponse> PatchJsonAsync<TRequest, TResponse>(TRequest model, string url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.SendAsync(Json.FromModel(model, options), url, Constants.Http.JSON_CONTENT, options, AddJsonHeaders(headers), HypertextTransferProtocol.Patch).ContinueWith(ParseAsync<TResponse>(options));
        }

        /// <summary>Append the header: "Accept: application/json", if not present.</summary>
        internal static Header[] AddJsonHeaders(Header[] headers)
        {
            foreach (var header in headers) if ("Accept".Equals(header.Key, StringComparison.OrdinalIgnoreCase)) return headers;
            Array.Resize(ref headers, headers.Length + 1);
            headers[headers.Length - 1] = new Header("Accept", Constants.Http.JSON_CONTENT);
            return headers;
        }
    }

    internal static class HypertextTransferProtocol
    {
        internal static readonly HttpMethod Patch = new HttpMethod(Constants.Http.PATCH); // Patch is missing from .net standard 2.0 (is there in 2.1).

        private static readonly int DNS_RENEW_MILLISECONDS = (int)TimeSpan.FromSeconds(300).TotalMilliseconds;
        private readonly static System.Net.Http.HttpClient _client = new System.Net.Http.HttpClient();
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
            var response = new Either<string, HttpException>();

            try
            {
                var request = WebRequest.Create(url);
                request.Method = httpMethod;
                request.Timeout = options * 1000;
                foreach (var header in headers) request.Headers.Add(header.Key, header.Value);
                if (!string.IsNullOrEmpty(body) && (Constants.Http.POST.Equals(httpMethod) || Constants.Http.PUT.Equals(httpMethod) || Constants.Http.PATCH.Equals(httpMethod)))
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
            var response = new Either<string, HttpException>();

            try
            {
                using (var cts = new CancellationTokenSource(options * 1000))
                using (var httpRequest = new HttpRequestMessage(httpMethod, url))
                {
                    AddDnsRenew(httpRequest.RequestUri);
                    foreach (var header in headers) httpRequest.Headers.Add(header.Key, header.Value);
                    if (!string.IsNullOrEmpty(body) && (httpMethod == HttpMethod.Post || httpMethod == HttpMethod.Put || httpMethod == Patch))
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
            }
            catch (Exception ex)
            {
                response = new HttpException($"Error calling {httpMethod.Method}: [{url}].", 504, string.Empty, ex, new Header[0]);
            }

            return response;
        }
    }
}
