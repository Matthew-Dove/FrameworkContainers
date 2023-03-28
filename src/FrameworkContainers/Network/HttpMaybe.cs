using ContainerExpressions.Containers;
using FrameworkContainers.Format;
using FrameworkContainers.Models;
using FrameworkContainers.Models.Exceptions;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace FrameworkContainers.Network
{
    public sealed class HttpMaybe
    {
        internal static readonly HttpMaybe Instance = new HttpMaybe();

        private HttpMaybe() { }

        private static Func<string, Maybe<T>> Parse<T>(JsonOptions options) { return json => Json.Maybe.ToModel<T>(json, options).Match(Maybe.Create<T>, Maybe.Create<T>); }

        private static Func<Task<Either<string, HttpException>>, Maybe<T>> ParseAsync<T>(JsonOptions options) { return x => x.Result.Match(Parse<T>(options), Maybe.Create<T>); }

        private static Func<string, Maybe<T>> Send<T>(string httpMethod, string url, HttpOptions options, Header[] headers)
        {
            return body => HypertextTransferProtocol
                .Send(body, url, Constants.Http.JSON_CONTENT, options, Http.AddJsonHeaders(headers), httpMethod)
                .Match(Parse<T>(options), Maybe.Create<T>);
        }

        private static Func<string, Task<Maybe<T>>> SendAsync<T>(HttpMethod httpMethod, string url, HttpOptions options, Header[] headers)
        {
            return body => HypertextTransferProtocol
                .SendAsync(body, url, Constants.Http.JSON_CONTENT, options, Http.AddJsonHeaders(headers), httpMethod)
                .ContinueWith(ParseAsync<T>(options));
        }

        public Maybe<string> Post(string body, string url, string contentType, params Header[] headers)
        {
            return Post(body, url, contentType, HttpOptions.Default, headers);
        }

        public Maybe<string> Post(string body, string url, string contentType, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.Send(body, url, contentType, options, headers, Constants.Http.POST).Match(Maybe.Create<string>, Maybe.Create<string>);
        }

        public Maybe<TResponse> PostJson<TRequest, TResponse>(TRequest model, string url, params Header[] headers)
        {
            return PostJson<TRequest, TResponse>(model, url, HttpOptions.Default, headers);
        }

        public Maybe<TResponse> PostJson<TRequest, TResponse>(TRequest model, string url, HttpOptions options, params Header[] headers)
        {
            return Json.Maybe.FromModel(model, options).Match(
                Send<TResponse>(Constants.Http.POST, url, options, headers),
                Maybe.Create<TResponse>
            );
        }

        public Maybe<string> Put(string body, string url, string contentType, params Header[] headers)
        {
            return Put(body, url, contentType, HttpOptions.Default, headers);
        }

        public Maybe<string> Put(string body, string url, string contentType, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.Send(body, url, contentType, options, headers, Constants.Http.PUT).Match(Maybe.Create<string>, Maybe.Create<string>);
        }

        public Maybe<TResponse> PutJson<TRequest, TResponse>(TRequest model, string url, params Header[] headers)
        {
            return PutJson<TRequest, TResponse>(model, url, HttpOptions.Default, headers);
        }

        public Maybe<TResponse> PutJson<TRequest, TResponse>(TRequest model, string url, HttpOptions options, params Header[] headers)
        {
            return Json.Maybe.FromModel(model, options).Match(
                Send<TResponse>(Constants.Http.PUT, url, options, headers),
                Maybe.Create<TResponse>
            );
        }

        public Maybe<string> Get(string url, params Header[] headers)
        {
            return Get(url, HttpOptions.Default, headers);
        }

        public Maybe<string> Get(string url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.Send(string.Empty, url, string.Empty, options, headers, Constants.Http.GET).Match(Maybe.Create<string>, Maybe.Create<string>);
        }

        public Maybe<TResponse> GetJson<TResponse>(string url, params Header[] headers)
        {
            return GetJson<TResponse>(url, HttpOptions.Default, headers);
        }

        public Maybe<TResponse> GetJson<TResponse>(string url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol
                .Send(string.Empty, url, string.Empty, options, Http.AddJsonHeaders(headers), Constants.Http.GET)
                .Match(Parse<TResponse>(options), Maybe.Create<TResponse>);
        }

        public Maybe<string> Delete(string url, params Header[] headers)
        {
            return Delete(url, HttpOptions.Default, headers);
        }

        public Maybe<string> Delete(string url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.Send(string.Empty, url, string.Empty, options, headers, Constants.Http.DELETE).Match(Maybe.Create<string>, Maybe.Create<string>);
        }

        public Maybe<TResponse> DeleteJson<TResponse>(string url, params Header[] headers)
        {
            return DeleteJson<TResponse>(url, HttpOptions.Default, headers);
        }

        public Maybe<TResponse> DeleteJson<TResponse>(string url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol
                .Send(string.Empty, url, string.Empty, options, Http.AddJsonHeaders(headers), Constants.Http.DELETE)
                .Match(Parse<TResponse>(options), Maybe.Create<TResponse>);
        }

        public Maybe<string> Patch(string body, string url, string contentType, params Header[] headers)
        {
            return Patch(body, url, contentType, HttpOptions.Default, headers);
        }

        public Maybe<string> Patch(string body, string url, string contentType, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.Send(body, url, contentType, options, headers, Constants.Http.PATCH).Match(Maybe.Create<string>, Maybe.Create<string>);
        }

        public Maybe<TResponse> PatchJson<TRequest, TResponse>(TRequest model, string url, params Header[] headers)
        {
            return PatchJson<TRequest, TResponse>(model, url, HttpOptions.Default, headers);
        }

        public Maybe<TResponse> PatchJson<TRequest, TResponse>(TRequest model, string url, HttpOptions options, params Header[] headers)
        {
            return Json.Maybe.FromModel(model, options).Match(
                Send<TResponse>(Constants.Http.PATCH, url, options, headers),
                Maybe.Create<TResponse>
            );
        }

        public Task<Maybe<string>> PostAsync(string body, string url, string contentType, params Header[] headers)
        {
            return PostAsync(body, url, contentType, HttpOptions.Default, headers);
        }

        public Task<Maybe<string>> PostAsync(string body, string url, string contentType, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.SendAsync(body, url, contentType, options, headers, HttpMethod.Post).ContinueWith(static x => x.Result.Match(Maybe.Create<string>, Maybe.Create<string>));
        }

        public Task<Maybe<TResponse>> PostJsonAsync<TRequest, TResponse>(TRequest model, string url, params Header[] headers)
        {
            return PostJsonAsync<TRequest, TResponse>(model, url, HttpOptions.Default, headers);
        }

        public Task<Maybe<TResponse>> PostJsonAsync<TRequest, TResponse>(TRequest model, string url, HttpOptions options, params Header[] headers)
        {
            return Json.Maybe.FromModel(model, options).MatchAsync(
                SendAsync<TResponse>(HttpMethod.Post, url, options, headers),
                Maybe.Create<TResponse>
            );
        }

        public Task<Maybe<string>> PutAsync(string body, string url, string contentType, params Header[] headers)
        {
            return PutAsync(body, url, contentType, HttpOptions.Default, headers);
        }

        public Task<Maybe<string>> PutAsync(string body, string url, string contentType, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.SendAsync(body, url, contentType, options, headers, HttpMethod.Put).ContinueWith(static x => x.Result.Match(Maybe.Create<string>, Maybe.Create<string>));
        }

        public Task<Maybe<TResponse>> PutJsonAsync<TRequest, TResponse>(TRequest model, string url, params Header[] headers)
        {
            return PutJsonAsync<TRequest, TResponse>(model, url, HttpOptions.Default, headers);
        }

        public Task<Maybe<TResponse>> PutJsonAsync<TRequest, TResponse>(TRequest model, string url, HttpOptions options, params Header[] headers)
        {
            return Json.Maybe.FromModel(model, options).MatchAsync(
                SendAsync<TResponse>(HttpMethod.Put, url, options, headers),
                Maybe.Create<TResponse>
            );
        }

        public Task<Maybe<string>> GetAsync(string url, params Header[] headers)
        {
            return GetAsync(url, HttpOptions.Default, headers);
        }

        public Task<Maybe<string>> GetAsync(string url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.SendAsync(string.Empty, url, string.Empty, options, headers, HttpMethod.Get).ContinueWith(static x => x.Result.Match(Maybe.Create<string>, Maybe.Create<string>));
        }

        public Task<Maybe<TResponse>> GetJsonAsync<TResponse>(string url, params Header[] headers)
        {
            return GetJsonAsync<TResponse>(url, HttpOptions.Default, headers);
        }

        public Task<Maybe<TResponse>> GetJsonAsync<TResponse>(string url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.SendAsync(string.Empty, url, string.Empty, options, Http.AddJsonHeaders(headers), HttpMethod.Get).ContinueWith(ParseAsync<TResponse>(options));
        }

        public Task<Maybe<string>> DeleteAsync(string url, params Header[] headers)
        {
            return DeleteAsync(url, HttpOptions.Default, headers);
        }

        public Task<Maybe<string>> DeleteAsync(string url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.SendAsync(string.Empty, url, string.Empty, options, headers, HttpMethod.Delete).ContinueWith(static x => x.Result.Match(Maybe.Create<string>, Maybe.Create<string>));
        }

        public Task<Maybe<TResponse>> DeleteJsonAsync<TResponse>(string url, params Header[] headers)
        {
            return DeleteJsonAsync<TResponse>(url, HttpOptions.Default, headers);
        }

        public Task<Maybe<TResponse>> DeleteJsonAsync<TResponse>(string url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.SendAsync(string.Empty, url, string.Empty, options, Http.AddJsonHeaders(headers), HttpMethod.Delete).ContinueWith(ParseAsync<TResponse>(options));
        }

        public Task<Maybe<string>> PatchAsync(string body, string url, string contentType, params Header[] headers)
        {
            return PatchAsync(body, url, contentType, HttpOptions.Default, headers);
        }

        public Task<Maybe<string>> PatchAsync(string body, string url, string contentType, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.SendAsync(body, url, contentType, options, headers, HypertextTransferProtocol.Patch).ContinueWith(static x => x.Result.Match(Maybe.Create<string>, Maybe.Create<string>));
        }

        public Task<Maybe<TResponse>> PatchJsonAsync<TRequest, TResponse>(TRequest model, string url, params Header[] headers)
        {
            return PatchJsonAsync<TRequest, TResponse>(model, url, HttpOptions.Default, headers);
        }

        public Task<Maybe<TResponse>> PatchJsonAsync<TRequest, TResponse>(TRequest model, string url, HttpOptions options, params Header[] headers)
        {
            return Json.Maybe.FromModel(model, options).MatchAsync(
                SendAsync<TResponse>(HypertextTransferProtocol.Patch, url, options, headers),
                Maybe.Create<TResponse>
            );
        }
    }
}
