using ContainerExpressions.Containers;
using FrameworkContainers.Models.Exceptions;
using FrameworkContainers.Models;
using FrameworkContainers.Network.HttpCollective.Models;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Linq;
using System.Net.Http.Json;
using System.Net.Http.Headers;

namespace FrameworkContainers.Network.HttpCollective;

internal static class HypertextTransferProtocol
{
    internal static readonly HttpMethod Patch = new HttpMethod(Constants.Http.PATCH); // Patch is missing from .net standard 2.0 (is there in 2.1).
    private static readonly MediaTypeHeaderValue _jsonContent = new MediaTypeHeaderValue(Constants.Http.JSON_CONTENT) { CharSet = Encoding.UTF8.WebName };

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

    // Generic sync string send, and receive.
    public static Either<string, HttpException> Send(string body, Uri url, string contentType, HttpOptions options, Header[] headers, string httpMethod)
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
            using (var webResponse = (HttpWebResponse)request.GetResponse())
            using (var responseStream = webResponse.GetResponseStream())
            using (var sr = new StreamReader(responseStream))
            {
                response = sr.ReadToEnd();
                if (options) response = webResponse.StatusDescription;
            }
        }
        catch (WebException we) when (we.Response is HttpWebResponse httpResponse)
        {
            var responseheaders = new Header[httpResponse.Headers.Count];
            for (int i = 0; i < httpResponse.Headers.Count; i++) responseheaders[i] = new Header(httpResponse.Headers.Keys[i], httpResponse.Headers[i]);
            var statusCode = (int)httpResponse.StatusCode;
            var statusDescription = httpResponse.StatusDescription;
            var responseContent = string.Empty;
            using (var sr = new StreamReader(httpResponse.GetResponseStream())) { responseContent = sr.ReadToEnd(); }
            httpResponse.Dispose();
            response = new HttpException($"Error calling {httpMethod}: [{url}].", statusCode, responseContent, we, responseheaders);
            if (options) response = statusDescription;
        }
        catch (Exception ex)
        {
            response = new HttpException($"Error calling {httpMethod}: [{url}].", Constants.Http.DEFAULT_HTTP_CODE, string.Empty, ex, Array.Empty<Header>());
        }

        return response;
    }

    // Special case for the caller to handle 200, 400, and 500 http status codes manually (sync).
    public static Either<Http245, HttpException> Send245(string body, Uri url, string contentType, HttpOptions options, Header[] headers, string httpMethod)
    {
        var response = new Either<Http245, HttpException>();

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
            using (var webResponse = (HttpWebResponse)request.GetResponse())
            using (var responseStream = webResponse.GetResponseStream())
            using (var sr = new StreamReader(responseStream))
            {
                var responseBody = sr.ReadToEnd();
                var responseStatusCode = (int)webResponse.StatusCode;
                var responseheaders = new Header[webResponse.Headers.Count];
                for (int i = 0; i < webResponse.Headers.Count; i++) responseheaders[i] = new Header(webResponse.Headers.Keys[i], webResponse.Headers[i]);
                response = new Http245(responseheaders, responseStatusCode, responseBody);
            }
        }
        catch (WebException we) when (we.Response is HttpWebResponse httpResponse)
        {
            var responseStatusCode = (int)httpResponse.StatusCode;
            var responseBody = default(string);
            using (var sr = new StreamReader(httpResponse.GetResponseStream())) { responseBody = sr.ReadToEnd(); }
            var responseheaders = new Header[httpResponse.Headers.Count];
            for (int i = 0; i < httpResponse.Headers.Count; i++) responseheaders[i] = new Header(httpResponse.Headers.Keys[i], httpResponse.Headers[i]);
            response = new Http245(responseheaders, responseStatusCode, responseBody);
            httpResponse.Dispose();
        }
        catch (Exception ex)
        {
            response = new HttpException($"Error calling {httpMethod}: [{url}].", Constants.Http.DEFAULT_HTTP_CODE, string.Empty, ex, Array.Empty<Header>());
        }

        return response;
    }

    // Generic async string send, and receive.
    public static async Task<Either<string, HttpException>> SendAsync(string body, Uri url, string contentType, HttpOptions options, Header[] headers, HttpMethod httpMethod)
    {
        var response = new Either<string, HttpException>();
        var cts = default(CancellationTokenSource);
        var httpRequest = default(HttpRequestMessage);
        var httpResponse = default(HttpResult);

        try
        {
            var useToken = CancellationToken.None.Equals(options.WebClient.CancellationToken);
            var useClient = options.WebClient.HttpClient == null;
            var token = options.WebClient.CancellationToken;
            var client = useClient ? _client : options.WebClient.HttpClient;

            if (useToken)
            {
                cts = new CancellationTokenSource(options * 1000);
                token = cts.Token;
            }

            AddDnsRenew(url);
            httpRequest = new HttpRequestMessage(httpMethod, url);
            foreach (var header in headers) httpRequest.Headers.Add(header.Key, header.Value);
            if (!string.IsNullOrEmpty(body) && (httpMethod == HttpMethod.Post || httpMethod == HttpMethod.Put || httpMethod == Patch))
            {
                httpRequest.Content = new StringContent(body, Encoding.UTF8, contentType);
            }

            httpResponse = await client.SendAsync(httpRequest, HttpCompletionOption.ResponseHeadersRead, token).ContinueWith(HttpResult.Create).ConfigureAwait(false);

            if (httpResponse.IsValid && httpResponse.Value.IsSuccessStatusCode && httpResponse.Value.Content is object)
            {
                var raw = await httpResponse.Value.Content.ReadAsStringAsync().ContinueWith(HttpResult<string>.Create).ConfigureAwait(false);
                response = raw.GetValueOr(default);
                if (!raw)
                {
                    var rawStatusCode = ((int?)httpResponse.Value?.StatusCode).GetValueOrDefault(Constants.Http.DEFAULT_HTTP_CODE);
                    var rawHeaders = Array.Empty<Header>();
                    if ((httpResponse.Value?.Headers?.Any()).GetValueOrDefault(false))
                    {
                        rawHeaders = httpResponse.Value.Headers.Select(static x => new Header(x.Key, x.Value.First())).ToArray();
                    }
                    response = new HttpException($"Error calling {httpMethod.Method}: [{url}]. Is canceled: {raw.Task.IsCanceled}.", rawStatusCode, string.Empty, raw, rawHeaders);
                }
                if (options) response = httpResponse.Value.ReasonPhrase;
            }
            else
            {
                var rawStatusCode = ((int?)httpResponse.Value?.StatusCode).GetValueOrDefault(Constants.Http.DEFAULT_HTTP_CODE);
                var rawBody = await httpResponse.TryGetBody().ConfigureAwait(false);
                var rawHeaders = Array.Empty<Header>();
                if ((httpResponse.Value?.Headers?.Any()).GetValueOrDefault(false))
                {
                    rawHeaders = httpResponse.Value.Headers.Select(static x => new Header(x.Key, x.Value.First())).ToArray();
                }
                response = new HttpException($"Error calling {httpMethod.Method}: [{url}].", rawStatusCode, rawBody, httpResponse, rawHeaders);
                if (options) response = httpResponse.Value?.ReasonPhrase ?? Constants.Http.DEFAULT_HTTP_DESCRIPTION;
            }
        }
        catch (Exception ex)
        {
            if (ex is AggregateException ae) ex = ae.InnerException;
            response = new HttpException($"Error calling {httpMethod.Method}: [{url}].", Constants.Http.DEFAULT_HTTP_CODE, string.Empty, ex, Array.Empty<Header>());
        }
        finally
        {
            cts?.Dispose();
            httpRequest?.Dispose();
            httpResponse.Dispose();
        }

        return response;
    }

    // Send JSON => Receive HttpStatus -- post | put | patch.
    public static async Task<Either<HttpStatus, HttpException>> SendJsonStatusAsync<TRequest>(TRequest request, Uri url, HttpOptions options, Header[] headers, HttpMethod httpMethod)
    {
        var response = new Either<HttpStatus, HttpException>();
        var cts = default(CancellationTokenSource);
        var httpRequest = default(HttpRequestMessage);
        var httpResponse = default(HttpResult);

        try
        {
            var useToken = CancellationToken.None.Equals(options.WebClient.CancellationToken);
            var useClient = options.WebClient.HttpClient == null;
            var token = options.WebClient.CancellationToken;
            var client = useClient ? _client : options.WebClient.HttpClient;

            if (useToken)
            {
                cts = new CancellationTokenSource(options * 1000);
                token = cts.Token;
            }

            AddDnsRenew(url);
            httpRequest = new HttpRequestMessage(httpMethod, url);
            foreach (var header in headers) httpRequest.Headers.Add(header.Key, header.Value);
            httpRequest.Content = JsonContent.Create<TRequest>(request, _jsonContent, options);

            httpResponse = await client.SendAsync(httpRequest, HttpCompletionOption.ResponseHeadersRead, token).ContinueWith(HttpResult.Create).ConfigureAwait(false);
            response = new HttpStatus(httpResponse.Value?.ReasonPhrase ?? Constants.Http.DEFAULT_HTTP_DESCRIPTION);
        }
        catch (Exception ex)
        {
            if (ex is AggregateException ae) ex = ae.InnerException;
            response = new HttpException($"Error calling {httpMethod.Method}: [{url}].", Constants.Http.DEFAULT_HTTP_CODE, string.Empty, ex, Array.Empty<Header>());
        }
        finally
        {
            cts?.Dispose();
            httpRequest?.Dispose();
            httpResponse.Dispose();
        }

        return response;
    }

    // Send JSON => Receive JSON -- post | put | patch.
    public static async Task<Either<TResponse, HttpException>> SendJsonAsync<TRequest, TResponse>(TRequest request, Uri url, HttpOptions options, Header[] headers, HttpMethod httpMethod)
    {
        var response = new Either<TResponse, HttpException>();
        var cts = default(CancellationTokenSource);
        var httpRequest = default(HttpRequestMessage);
        var httpResponse = default(HttpResult);

        try
        {
            var useToken = CancellationToken.None.Equals(options.WebClient.CancellationToken);
            var useClient = options.WebClient.HttpClient == null;
            var token = options.WebClient.CancellationToken;
            var client = useClient ? _client : options.WebClient.HttpClient;

            if (useToken)
            {
                cts = new CancellationTokenSource(options * 1000);
                token = cts.Token;
            }

            AddDnsRenew(url);
            httpRequest = new HttpRequestMessage(httpMethod, url);
            foreach (var header in headers) httpRequest.Headers.Add(header.Key, header.Value);
            httpRequest.Content = JsonContent.Create<TRequest>(request, _jsonContent, options);

            httpResponse = await client.SendAsync(httpRequest, HttpCompletionOption.ResponseHeadersRead, token).ContinueWith(HttpResult.Create).ConfigureAwait(false);

            if (httpResponse.IsValid && httpResponse.Value.IsSuccessStatusCode && httpResponse.Value.Content is object)
            {
                var raw = await httpResponse.Value.Content.ReadFromJsonAsync<TResponse>(options, cts.Token).ContinueWith(HttpResult<TResponse>.Create).ConfigureAwait(false);
                response = raw.GetValueOr(default);
                if (!raw)
                {
                    var rawStatusCode = ((int?)httpResponse.Value?.StatusCode).GetValueOrDefault(Constants.Http.DEFAULT_HTTP_CODE);
                    var rawHeaders = Array.Empty<Header>();
                    if ((httpResponse.Value?.Headers?.Any()).GetValueOrDefault(false))
                    {
                        rawHeaders = httpResponse.Value.Headers.Select(static x => new Header(x.Key, x.Value.First())).ToArray();
                    }
                    response = new HttpException($"Error calling {httpMethod.Method}: [{url}]. Is canceled: {raw.Task.IsCanceled}.", rawStatusCode, string.Empty, raw, rawHeaders);
                }
            }
            else
            {
                var rawStatusCode = ((int?)httpResponse.Value?.StatusCode).GetValueOrDefault(Constants.Http.DEFAULT_HTTP_CODE);
                var rawBody = await httpResponse.TryGetBody().ConfigureAwait(false);
                var rawHeaders = Array.Empty<Header>();
                if ((httpResponse.Value?.Headers?.Any()).GetValueOrDefault(false))
                {
                    rawHeaders = httpResponse.Value.Headers.Select(static x => new Header(x.Key, x.Value.First())).ToArray();
                }
                response = new HttpException($"Error calling {httpMethod.Method}: [{url}].", rawStatusCode, rawBody, httpResponse, rawHeaders);
            }
        }
        catch (Exception ex)
        {
            if (ex is AggregateException ae) ex = ae.InnerException;
            response = new HttpException($"Error calling {httpMethod.Method}: [{url}].", Constants.Http.DEFAULT_HTTP_CODE, string.Empty, ex, Array.Empty<Header>());
        }
        finally
        {
            cts?.Dispose();
            httpRequest?.Dispose();
            httpResponse.Dispose();
        }

        return response;
    }

    // () => Receive JSON -- get.
    public static async Task<Either<TResponse, HttpException>> SendJsonAsync<TResponse>(Uri url, HttpOptions options, Header[] headers, HttpMethod httpMethod)
    {
        var response = new Either<TResponse, HttpException>();
        var cts = default(CancellationTokenSource);
        var httpRequest = default(HttpRequestMessage);
        var httpResponse = default(HttpResult);

        try
        {
            var useToken = CancellationToken.None.Equals(options.WebClient.CancellationToken);
            var useClient = options.WebClient.HttpClient == null;
            var token = options.WebClient.CancellationToken;
            var client = useClient ? _client : options.WebClient.HttpClient;

            if (useToken)
            {
                cts = new CancellationTokenSource(options * 1000);
                token = cts.Token;
            }

            AddDnsRenew(url);
            httpRequest = new HttpRequestMessage(httpMethod, url);
            foreach (var header in headers) httpRequest.Headers.Add(header.Key, header.Value);

            httpResponse = await client.SendAsync(httpRequest, HttpCompletionOption.ResponseHeadersRead, token).ContinueWith(HttpResult.Create).ConfigureAwait(false);

            if (httpResponse.IsValid && httpResponse.Value.IsSuccessStatusCode && httpResponse.Value.Content is object)
            {
                var raw = await httpResponse.Value.Content.ReadFromJsonAsync<TResponse>(options, cts.Token).ContinueWith(HttpResult<TResponse>.Create).ConfigureAwait(false);
                response = raw.GetValueOr(default);
                if (!raw)
                {
                    var rawStatusCode = ((int?)httpResponse.Value?.StatusCode).GetValueOrDefault(Constants.Http.DEFAULT_HTTP_CODE);
                    var rawHeaders = Array.Empty<Header>();
                    if ((httpResponse.Value?.Headers?.Any()).GetValueOrDefault(false))
                    {
                        rawHeaders = httpResponse.Value.Headers.Select(static x => new Header(x.Key, x.Value.First())).ToArray();
                    }
                    response = new HttpException($"Error calling {httpMethod.Method}: [{url}]. Is canceled: {raw.Task.IsCanceled}.", rawStatusCode, string.Empty, raw, rawHeaders);
                }
            }
            else
            {
                var rawStatusCode = ((int?)httpResponse.Value?.StatusCode).GetValueOrDefault(Constants.Http.DEFAULT_HTTP_CODE);
                var rawBody = await httpResponse.TryGetBody().ConfigureAwait(false);
                var rawHeaders = Array.Empty<Header>();
                if ((httpResponse.Value?.Headers?.Any()).GetValueOrDefault(false))
                {
                    rawHeaders = httpResponse.Value.Headers.Select(static x => new Header(x.Key, x.Value.First())).ToArray();
                }
                response = new HttpException($"Error calling {httpMethod.Method}: [{url}].", rawStatusCode, rawBody, httpResponse, rawHeaders);
            }
        }
        catch (Exception ex)
        {
            if (ex is AggregateException ae) ex = ae.InnerException;
            response = new HttpException($"Error calling {httpMethod.Method}: [{url}].", Constants.Http.DEFAULT_HTTP_CODE, string.Empty, ex, Array.Empty<Header>());
        }
        finally
        {
            cts?.Dispose();
            httpRequest?.Dispose();
            httpResponse.Dispose();
        }

        return response;
    }

    // Special case for the caller to handle 200, 400, and 500 http status codes manually (async).
    public static async Task<Either<Http245, HttpException>> Send245Async(string body, Uri url, string contentType, HttpOptions options, Header[] headers, HttpMethod httpMethod)
    {
        var response = new Either<Http245, HttpException>();
        var cts = default(CancellationTokenSource);
        var httpRequest = default(HttpRequestMessage);
        var httpResponse = default(HttpResult);

        try
        {
            var useToken = CancellationToken.None.Equals(options.WebClient.CancellationToken);
            var useClient = options.WebClient.HttpClient == null;
            var token = options.WebClient.CancellationToken;
            var client = useClient ? _client : options.WebClient.HttpClient;

            if (useToken)
            {
                cts = new CancellationTokenSource(options * 1000);
                token = cts.Token;
            }

            AddDnsRenew(url);
            httpRequest = new HttpRequestMessage(httpMethod, url);
            foreach (var header in headers) httpRequest.Headers.Add(header.Key, header.Value);
            if (!string.IsNullOrEmpty(body) && (httpMethod == HttpMethod.Post || httpMethod == HttpMethod.Put || httpMethod == Patch))
            {
                httpRequest.Content = new StringContent(body, Encoding.UTF8, contentType);
            }

            httpResponse = await client.SendAsync(httpRequest, HttpCompletionOption.ResponseHeadersRead, token).ContinueWith(HttpResult.Create).ConfigureAwait(false);

            if (httpResponse.IsValid && httpResponse.Value.IsSuccessStatusCode && httpResponse.Value.Content is object)
            {
                var raw = await httpResponse.Value.Content.ReadAsStringAsync().ContinueWith(HttpResult<string>.Create).ConfigureAwait(false);
                var rawBody = raw.GetValueOr(default);
                var rawStatusCode = ((int?)httpResponse.Value?.StatusCode).GetValueOrDefault(Constants.Http.DEFAULT_HTTP_CODE);
                var rawHeaders = Array.Empty<Header>();
                if ((httpResponse.Value?.Headers?.Any()).GetValueOrDefault(false))
                {
                    rawHeaders = httpResponse.Value.Headers.Select(static x => new Header(x.Key, x.Value.First())).ToArray();
                }
                response = new Http245(rawHeaders, rawStatusCode, rawBody);
            }
            else
            {
                var rawBody = await httpResponse.TryGetBody().ConfigureAwait(false);
                var rawStatusCode = ((int?)httpResponse.Value?.StatusCode).GetValueOrDefault(Constants.Http.DEFAULT_HTTP_CODE);
                var rawHeaders = Array.Empty<Header>();
                if ((httpResponse.Value?.Headers?.Any()).GetValueOrDefault(false))
                {
                    rawHeaders = httpResponse.Value.Headers.Select(static x => new Header(x.Key, x.Value.First())).ToArray();
                }
                response = new Http245(rawHeaders, rawStatusCode, rawBody);
            }
        }
        catch (Exception ex)
        {
            if (ex is AggregateException ae) ex = ae.InnerException;
            response = new HttpException($"Error calling {httpMethod.Method}: [{url}].", Constants.Http.DEFAULT_HTTP_CODE, string.Empty, ex, Array.Empty<Header>());
        }
        finally
        {
            cts?.Dispose();
            httpRequest?.Dispose();
            httpResponse.Dispose();
        }

        return response;
    }
}
