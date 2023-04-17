using ContainerExpressions.Containers;
using FrameworkContainers.Format.JsonCollective;
using FrameworkContainers.Format.JsonCollective.Models;
using FrameworkContainers.Models;
using FrameworkContainers.Network.HttpCollective.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace FrameworkContainers.Network.HttpCollective
{
    public sealed class HttpMaybe
    {
        internal static readonly HttpMaybe Instance = new HttpMaybe();

        private HttpMaybe() { }

        private static Maybe<HttpStatus> IdentityMaybeStatus<E>(Task<Either<string, E>> t) where E : Exception => t.Result.Match(static x => Maybe.Create(HttpStatus.Create(x)), Maybe.Create<HttpStatus>);

        private static Maybe<T> IdentityMaybe<T, E>(Task<Either<T, E>> t) where E : Exception => t.Result.Match(Maybe.Create<T>, Maybe.Create<T>);

        private static Func<string, Maybe<T>> Parse<T>(JsonOptions options) { return json => Json.Maybe.ToModel<T>(json, options).Match(Maybe.Create<T>, Maybe.Create<T>); }

        private static Func<string, Maybe<HttpStatus>> Send(string httpMethod, Either<string, Uri> url, HttpOptions options, Header[] headers)
        {
            return body => HypertextTransferProtocol
                .Send(body, url.Match(static x => new Uri(x), Identity), Constants.Http.JSON_CONTENT, new HttpOptions(options, retrieveHttpStatus: true), headers, httpMethod)
                .Match(static x => new Maybe<HttpStatus>(new HttpStatus(x)), Maybe.Create<HttpStatus>);
        }

        private static Func<string, Maybe<T>> Send<T>(string httpMethod, Either<string, Uri> url, HttpOptions options, Header[] headers)
        {
            return body => HypertextTransferProtocol
                .Send(body, url.Match(static x => new Uri(x), Identity), Constants.Http.JSON_CONTENT, options, headers, httpMethod)
                .Match(Parse<T>(options), Maybe.Create<T>);
        }

        public Maybe<string> Post(string body, Either<string, Uri> url, string contentType, params Header[] headers)
        {
            return Post(body, url, contentType, HttpOptions.Default, headers);
        }

        public Maybe<string> Post(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol
                .Send(body, url.Match(static x => new Uri(x), Identity), contentType, options, headers, Constants.Http.POST)
                .Match(Maybe.Create<string>, Maybe.Create<string>);
        }

        public Maybe<HttpStatus> PostStatus(string body, Either<string, Uri> url, string contentType, params Header[] headers)
        {
            return PostStatus(body, url, contentType, HttpOptions.Default, headers);
        }

        public Maybe<HttpStatus> PostStatus(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol
                .Send(body, url.Match(static x => new Uri(x), Identity), contentType, new HttpOptions(options, retrieveHttpStatus: true), headers, Constants.Http.POST)
                .Match(static x => Maybe.Create(HttpStatus.Create(x)), Maybe.Create<HttpStatus>);
        }

        public Maybe<HttpStatus> PostJsonStatus<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers)
        {
            return PostJsonStatus<TRequest>(model, url, HttpOptions.Default, headers);
        }

        public Maybe<HttpStatus> PostJsonStatus<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers)
        {
            return Json.Maybe.FromModel(model, options).Match(
                Send(Constants.Http.POST, url, options, headers),
                Maybe.Create<HttpStatus>
            );
        }

        public Maybe<TResponse> PostJson<TRequest, TResponse>(TRequest model, Either<string, Uri> url, params Header[] headers)
        {
            return PostJson<TRequest, TResponse>(model, url, HttpOptions.Default, headers);
        }

        public Maybe<TResponse> PostJson<TRequest, TResponse>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers)
        {
            return Json.Maybe.FromModel(model, options).Match(
                Send<TResponse>(Constants.Http.POST, url, options, headers),
                Maybe.Create<TResponse>
            );
        }

        public Maybe<string> Put(string body, Either<string, Uri> url, string contentType, params Header[] headers)
        {
            return Put(body, url, contentType, HttpOptions.Default, headers);
        }

        public Maybe<string> Put(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol
                .Send(body, url.Match(static x => new Uri(x), Identity), contentType, options, headers, Constants.Http.PUT)
                .Match(Maybe.Create<string>, Maybe.Create<string>);
        }

        public Maybe<HttpStatus> PutStatus(string body, Either<string, Uri> url, string contentType, params Header[] headers)
        {
            return PutStatus(body, url, contentType, HttpOptions.Default, headers);
        }

        public Maybe<HttpStatus> PutStatus(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol
                .Send(body, url.Match(static x => new Uri(x), Identity), contentType, new HttpOptions(options, retrieveHttpStatus: true), headers, Constants.Http.PUT)
                .Match(static x => Maybe.Create(HttpStatus.Create(x)), Maybe.Create<HttpStatus>);
        }

        public Maybe<HttpStatus> PutJsonStatus<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers)
        {
            return PutJsonStatus<TRequest>(model, url, HttpOptions.Default, headers);
        }

        public Maybe<HttpStatus> PutJsonStatus<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers)
        {
            return Json.Maybe.FromModel(model, options).Match(
                Send(Constants.Http.PUT, url, options, headers),
                Maybe.Create<HttpStatus>
            );
        }

        public Maybe<TResponse> PutJson<TRequest, TResponse>(TRequest model, Either<string, Uri> url, params Header[] headers)
        {
            return PutJson<TRequest, TResponse>(model, url, HttpOptions.Default, headers);
        }

        public Maybe<TResponse> PutJson<TRequest, TResponse>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers)
        {
            return Json.Maybe.FromModel(model, options).Match(
                Send<TResponse>(Constants.Http.PUT, url, options, headers),
                Maybe.Create<TResponse>
            );
        }

        public Maybe<string> Patch(string body, Either<string, Uri> url, string contentType, params Header[] headers)
        {
            return Patch(body, url, contentType, HttpOptions.Default, headers);
        }

        public Maybe<string> Patch(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol
                .Send(body, url.Match(static x => new Uri(x), Identity), contentType, options, headers, Constants.Http.PATCH)
                .Match(Maybe.Create<string>, Maybe.Create<string>);
        }

        public Maybe<HttpStatus> PatchStatus(string body, Either<string, Uri> url, string contentType, params Header[] headers)
        {
            return PatchStatus(body, url, contentType, HttpOptions.Default, headers);
        }

        public Maybe<HttpStatus> PatchStatus(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol
                .Send(body, url.Match(static x => new Uri(x), Identity), contentType, new HttpOptions(options, retrieveHttpStatus: true), headers, Constants.Http.PATCH)
                .Match(static x => Maybe.Create(HttpStatus.Create(x)), Maybe.Create<HttpStatus>);
        }

        public Maybe<HttpStatus> PatchStatusJson<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers)
        {
            return PatchStatusJson<TRequest>(model, url, HttpOptions.Default, headers);
        }

        public Maybe<HttpStatus> PatchStatusJson<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers)
        {
            return Json.Maybe.FromModel(model, options).Match(
                Send(Constants.Http.PATCH, url, options, headers),
                Maybe.Create<HttpStatus>
            );
        }

        public Maybe<TResponse> PatchJson<TRequest, TResponse>(TRequest model, Either<string, Uri> url, params Header[] headers)
        {
            return PatchJson<TRequest, TResponse>(model, url, HttpOptions.Default, headers);
        }

        public Maybe<TResponse> PatchJson<TRequest, TResponse>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers)
        {
            return Json.Maybe.FromModel(model, options).Match(
                Send<TResponse>(Constants.Http.PATCH, url, options, headers),
                Maybe.Create<TResponse>
            );
        }

        public Maybe<string> Get(Either<string, Uri> url, params Header[] headers)
        {
            return Get(url, HttpOptions.Default, headers);
        }

        public Maybe<string> Get(Either<string, Uri> url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol
                .Send(string.Empty, url.Match(static x => new Uri(x), Identity), string.Empty, options, headers, Constants.Http.GET)
                .Match(Maybe.Create<string>, Maybe.Create<string>);
        }

        public Maybe<HttpStatus> GetStatus(Either<string, Uri> url, params Header[] headers)
        {
            return GetStatus(url, HttpOptions.Default, headers);
        }

        public Maybe<HttpStatus> GetStatus(Either<string, Uri> url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol
                .Send(string.Empty, url.Match(static x => new Uri(x), Identity), string.Empty, new HttpOptions(options, retrieveHttpStatus: true), headers, Constants.Http.GET)
                .Match(static x => Maybe.Create(HttpStatus.Create(x)), Maybe.Create<HttpStatus>);
        }

        public Maybe<TResponse> GetJson<TResponse>(Either<string, Uri> url, params Header[] headers)
        {
            return GetJson<TResponse>(url, HttpOptions.Default, headers);
        }

        public Maybe<TResponse> GetJson<TResponse>(Either<string, Uri> url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol
                .Send(string.Empty, url.Match(static x => new Uri(x), Identity), string.Empty, options, headers, Constants.Http.GET)
                .Match(Parse<TResponse>(options), Maybe.Create<TResponse>);
        }

        public Maybe<HttpStatus> DeleteStatus(Either<string, Uri> url, params Header[] headers)
        {
            return DeleteStatus(url, HttpOptions.Default, headers);
        }

        public Maybe<HttpStatus> DeleteStatus(Either<string, Uri> url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol
                .Send(string.Empty, url.Match(static x => new Uri(x), Identity), string.Empty, new HttpOptions(options, retrieveHttpStatus: true), headers, Constants.Http.DELETE)
                .Match(static x => Maybe.Create(HttpStatus.Create(x)), Maybe.Create<HttpStatus>);
        }

        public Task<Maybe<string>> PostAsync(string body, Either<string, Uri> url, string contentType, params Header[] headers)
        {
            return PostAsync(body, url, contentType, HttpOptions.Default, headers);
        }

        public Task<Maybe<string>> PostAsync(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol
                .SendAsync(body, url.Match(static x => new Uri(x), Identity), contentType, options, headers, HttpMethod.Post)
                .ContinueWith(IdentityMaybe);
        }

        public Task<Maybe<HttpStatus>> PostStatusAsync(string body, Either<string, Uri> url, string contentType, params Header[] headers)
        {
            return PostStatusAsync(body, url, contentType, HttpOptions.Default, headers);
        }

        public Task<Maybe<HttpStatus>> PostStatusAsync(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol
                .SendAsync(body, url.Match(static x => new Uri(x), Identity), contentType, new HttpOptions(options, retrieveHttpStatus: true), headers, HttpMethod.Post)
                .ContinueWith(IdentityMaybeStatus);
        }

        public Task<Maybe<HttpStatus>> PostJsonStatusAsync<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers)
        {
            return PostJsonStatusAsync<TRequest>(model, url, HttpOptions.Default, headers);
        }

        public Task<Maybe<HttpStatus>> PostJsonStatusAsync<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol
                .SendJsonStatusAsync(model, url.Match(static x => new Uri(x), Identity), new HttpOptions(options, retrieveHttpStatus: true), headers, HttpMethod.Post)
                .ContinueWith(IdentityMaybe);
        }

        public Task<Maybe<TResponse>> PostJsonAsync<TRequest, TResponse>(TRequest model, Either<string, Uri> url, params Header[] headers)
        {
            return PostJsonAsync<TRequest, TResponse>(model, url, HttpOptions.Default, headers);
        }

        public Task<Maybe<TResponse>> PostJsonAsync<TRequest, TResponse>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol
                .SendJsonAsync<TRequest, TResponse>(model, url.Match(static x => new Uri(x), Identity), options, headers, HttpMethod.Post)
                .ContinueWith(IdentityMaybe);
        }

        public Task<Maybe<string>> PutAsync(string body, Either<string, Uri> url, string contentType, params Header[] headers)
        {
            return PutAsync(body, url, contentType, HttpOptions.Default, headers);
        }

        public Task<Maybe<string>> PutAsync(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol
                .SendAsync(body, url.Match(static x => new Uri(x), Identity), contentType, options, headers, HttpMethod.Put)
                .ContinueWith(IdentityMaybe);
        }

        public Task<Maybe<HttpStatus>> PutStatusAsync(string body, Either<string, Uri> url, string contentType, params Header[] headers)
        {
            return PutStatusAsync(body, url, contentType, HttpOptions.Default, headers);
        }

        public Task<Maybe<HttpStatus>> PutStatusAsync(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol
                .SendAsync(body, url.Match(static x => new Uri(x), Identity), contentType, new HttpOptions(options, retrieveHttpStatus: true), headers, HttpMethod.Put)
                .ContinueWith(IdentityMaybeStatus);
        }

        public Task<Maybe<HttpStatus>> PutJsonStatusAsync<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers)
        {
            return PutJsonStatusAsync<TRequest>(model, url, HttpOptions.Default, headers);
        }

        public Task<Maybe<HttpStatus>> PutJsonStatusAsync<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol
                .SendJsonStatusAsync(model, url.Match(static x => new Uri(x), Identity), new HttpOptions(options, retrieveHttpStatus: true), headers, HttpMethod.Put)
                .ContinueWith(IdentityMaybe);
        }

        public Task<Maybe<TResponse>> PutJsonAsync<TRequest, TResponse>(TRequest model, Either<string, Uri> url, params Header[] headers)
        {
            return PutJsonAsync<TRequest, TResponse>(model, url, HttpOptions.Default, headers);
        }

        public Task<Maybe<TResponse>> PutJsonAsync<TRequest, TResponse>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol
                .SendJsonAsync<TRequest, TResponse>(model, url.Match(static x => new Uri(x), Identity), options, headers, HttpMethod.Put)
                .ContinueWith(IdentityMaybe);
        }

        public Task<Maybe<string>> PatchAsync(string body, Either<string, Uri> url, string contentType, params Header[] headers)
        {
            return PatchAsync(body, url, contentType, HttpOptions.Default, headers);
        }

        public Task<Maybe<string>> PatchAsync(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol
                .SendAsync(body, url.Match(static x => new Uri(x), Identity), contentType, options, headers, HypertextTransferProtocol.Patch)
                .ContinueWith(IdentityMaybe);
        }

        public Task<Maybe<HttpStatus>> PatchStatusAsync(string body, Either<string, Uri> url, string contentType, params Header[] headers)
        {
            return PatchStatusAsync(body, url, contentType, HttpOptions.Default, headers);
        }

        public Task<Maybe<HttpStatus>> PatchStatusAsync(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol
                .SendAsync(body, url.Match(static x => new Uri(x), Identity), contentType, new HttpOptions(options, retrieveHttpStatus: true), headers, HypertextTransferProtocol.Patch).
                ContinueWith(IdentityMaybeStatus);
        }

        public Task<Maybe<HttpStatus>> PatchJsonStatusAsync<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers)
        {
            return PatchJsonStatusAsync<TRequest>(model, url, HttpOptions.Default, headers);
        }

        public Task<Maybe<HttpStatus>> PatchJsonStatusAsync<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol
                .SendJsonStatusAsync(model, url.Match(static x => new Uri(x), Identity), new HttpOptions(options, retrieveHttpStatus: true), headers, HypertextTransferProtocol.Patch)
                .ContinueWith(IdentityMaybe);
        }

        public Task<Maybe<TResponse>> PatchJsonAsync<TRequest, TResponse>(TRequest model, Either<string, Uri> url, params Header[] headers)
        {
            return PatchJsonAsync<TRequest, TResponse>(model, url, HttpOptions.Default, headers);
        }

        public Task<Maybe<TResponse>> PatchJsonAsync<TRequest, TResponse>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol
                .SendJsonAsync<TRequest, TResponse>(model, url.Match(static x => new Uri(x), Identity), options, headers, HypertextTransferProtocol.Patch)
                .ContinueWith(IdentityMaybe);
        }

        public Task<Maybe<string>> GetAsync(Either<string, Uri> url, params Header[] headers)
        {
            return GetAsync(url, HttpOptions.Default, headers);
        }

        public Task<Maybe<string>> GetAsync(Either<string, Uri> url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol
                .SendAsync(string.Empty, url.Match(static x => new Uri(x), Identity), string.Empty, options, headers, HttpMethod.Get)
                .ContinueWith(IdentityMaybe);
        }

        public Task<Maybe<HttpStatus>> GetStatusAsync(Either<string, Uri> url, params Header[] headers)
        {
            return GetStatusAsync(url, HttpOptions.Default, headers);
        }

        public Task<Maybe<HttpStatus>> GetStatusAsync(Either<string, Uri> url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol
                .SendAsync(string.Empty, url.Match(static x => new Uri(x), Identity), string.Empty, new HttpOptions(options, retrieveHttpStatus: true), headers, HttpMethod.Get)
                .ContinueWith(IdentityMaybeStatus);
        }

        public Task<Maybe<TResponse>> GetJsonAsync<TResponse>(Either<string, Uri> url, params Header[] headers)
        {
            return GetJsonAsync<TResponse>(url, HttpOptions.Default, headers);
        }

        public Task<Maybe<TResponse>> GetJsonAsync<TResponse>(Either<string, Uri> url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol
                .SendJsonAsync<TResponse>(url.Match(static x => new Uri(x), Identity), options, headers, HttpMethod.Get)
                .ContinueWith(IdentityMaybe);
        }

        public Task<Maybe<HttpStatus>> DeleteStatusAsync(Either<string, Uri> url, params Header[] headers)
        {
            return DeleteStatusAsync(url, HttpOptions.Default, headers);
        }

        public Task<Maybe<HttpStatus>> DeleteStatusAsync(Either<string, Uri> url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol
                .SendAsync(string.Empty, url.Match(static x => new Uri(x), Identity), string.Empty, new HttpOptions(options, retrieveHttpStatus: true), headers, HttpMethod.Delete)
                .ContinueWith(IdentityMaybeStatus);
        }
    }
}
