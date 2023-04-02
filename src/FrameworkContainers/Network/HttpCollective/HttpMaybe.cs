﻿using ContainerExpressions.Containers;
using FrameworkContainers.Format.JsonCollective;
using FrameworkContainers.Format.JsonCollective.Models;
using FrameworkContainers.Models;
using FrameworkContainers.Models.Exceptions;
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

        private static Maybe<T> IdentityMaybe<T, E>(Task<Either<T, E>> t) where E : Exception => t.Result.Match(Maybe.Create<T>, Maybe.Create<T>);

        private static Func<string, Maybe<T>> Parse<T>(JsonOptions options) { return json => Json.Maybe.ToModel<T>(json, options).Match(Maybe.Create<T>, Maybe.Create<T>); }

        private static Func<Task<Either<string, HttpException>>, Maybe<T>> ParseAsync<T>(JsonOptions options) { return x => x.Result.Match(Parse<T>(options), Maybe.Create<T>); }

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

        private static Func<string, Task<Maybe<HttpStatus>>> SendAsync(HttpMethod httpMethod, Either<string, Uri> url, HttpOptions options, Header[] headers)
        {
            return body => HypertextTransferProtocol
                .SendAsync(body, url.Match(static x => new Uri(x), Identity), Constants.Http.JSON_CONTENT, new HttpOptions(options, retrieveHttpStatus: true), headers, httpMethod)
                .ContinueWith(static t => t.Result.Match(static x => new Maybe<HttpStatus>(new HttpStatus(x)), Maybe.Create<HttpStatus>));
        }

        private static Func<string, Task<Maybe<T>>> SendAsync<T>(HttpMethod httpMethod, Either<string, Uri> url, HttpOptions options, Header[] headers)
        {
            return body => HypertextTransferProtocol
                .SendAsync(body, url.Match(static x => new Uri(x), Identity), Constants.Http.JSON_CONTENT, options, headers, httpMethod)
                .ContinueWith(ParseAsync<T>(options));
        }

        public Maybe<string> Post(string body, Either<string, Uri> url, string contentType, params Header[] headers)
        {
            return Post(body, url, contentType, HttpOptions.Default, headers);
        }

        public Maybe<string> Post(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.Send(body, url.Match(static x => new Uri(x), Identity), contentType, options, headers, Constants.Http.POST).Match(Maybe.Create<string>, Maybe.Create<string>);
        }

        public Maybe<HttpStatus> PostJson<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers)
        {
            return PostJson<TRequest>(model, url, HttpOptions.Default, headers);
        }

        public Maybe<HttpStatus> PostJson<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers)
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
            return HypertextTransferProtocol.Send(body, url.Match(static x => new Uri(x), Identity), contentType, options, headers, Constants.Http.PUT).Match(Maybe.Create<string>, Maybe.Create<string>);
        }

        public Maybe<HttpStatus> PutJson<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers)
        {
            return PutJson<TRequest>(model, url, HttpOptions.Default, headers);
        }

        public Maybe<HttpStatus> PutJson<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers)
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
            return HypertextTransferProtocol.Send(body, url.Match(static x => new Uri(x), Identity), contentType, options, headers, Constants.Http.PATCH).Match(Maybe.Create<string>, Maybe.Create<string>);
        }

        public Maybe<HttpStatus> PatchJson<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers)
        {
            return PatchJson<TRequest>(model, url, HttpOptions.Default, headers);
        }

        public Maybe<HttpStatus> PatchJson<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers)
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
            return HypertextTransferProtocol.Send(string.Empty, url.Match(static x => new Uri(x), Identity), string.Empty, options, headers, Constants.Http.GET).Match(Maybe.Create<string>, Maybe.Create<string>);
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

        public Maybe<string> Delete(Either<string, Uri> url, params Header[] headers)
        {
            return Delete(url, HttpOptions.Default, headers);
        }

        public Maybe<string> Delete(Either<string, Uri> url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.Send(string.Empty, url.Match(static x => new Uri(x), Identity), string.Empty, options, headers, Constants.Http.DELETE).Match(Maybe.Create<string>, Maybe.Create<string>);
        }

        public Maybe<TResponse> DeleteJson<TResponse>(Either<string, Uri> url, params Header[] headers)
        {
            return DeleteJson<TResponse>(url, HttpOptions.Default, headers);
        }

        public Maybe<TResponse> DeleteJson<TResponse>(Either<string, Uri> url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol
                .Send(string.Empty, url.Match(static x => new Uri(x), Identity), string.Empty, options, headers, Constants.Http.DELETE)
                .Match(Parse<TResponse>(options), Maybe.Create<TResponse>);
        }

        public Task<Maybe<string>> PostAsync(string body, Either<string, Uri> url, string contentType, params Header[] headers)
        {
            return PostAsync(body, url, contentType, HttpOptions.Default, headers);
        }

        public Task<Maybe<string>> PostAsync(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.SendAsync(body, url.Match(static x => new Uri(x), Identity), contentType, options, headers, HttpMethod.Post).ContinueWith(IdentityMaybe);
        }

        public Task<Maybe<HttpStatus>> PostJsonAsync<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers)
        {
            return PostJsonAsync<TRequest>(model, url, HttpOptions.Default, headers);
        }

        public Task<Maybe<HttpStatus>> PostJsonAsync<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers)
        {
            return Json.Maybe.FromModel(model, options).MatchAsync(
                SendAsync(HttpMethod.Post, url, options, headers),
                Maybe.Create<HttpStatus>
            );
        }

        public Task<Maybe<TResponse>> PostJsonAsync<TRequest, TResponse>(TRequest model, Either<string, Uri> url, params Header[] headers)
        {
            return PostJsonAsync<TRequest, TResponse>(model, url, HttpOptions.Default, headers);
        }

        public Task<Maybe<TResponse>> PostJsonAsync<TRequest, TResponse>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers)
        {
            return Json.Maybe.FromModel(model, options).MatchAsync(
                SendAsync<TResponse>(HttpMethod.Post, url, options, headers),
                Maybe.Create<TResponse>
            );
        }

        public Task<Maybe<string>> PutAsync(string body, Either<string, Uri> url, string contentType, params Header[] headers)
        {
            return PutAsync(body, url, contentType, HttpOptions.Default, headers);
        }

        public Task<Maybe<string>> PutAsync(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.SendAsync(body, url.Match(static x => new Uri(x), Identity), contentType, options, headers, HttpMethod.Put).ContinueWith(IdentityMaybe);
        }

        public Task<Maybe<HttpStatus>> PutJsonAsync<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers)
        {
            return PutJsonAsync<TRequest>(model, url, HttpOptions.Default, headers);
        }

        public Task<Maybe<HttpStatus>> PutJsonAsync<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers)
        {
            return Json.Maybe.FromModel(model, options).MatchAsync(
                SendAsync(HttpMethod.Put, url, options, headers),
                Maybe.Create<HttpStatus>
            );
        }

        public Task<Maybe<TResponse>> PutJsonAsync<TRequest, TResponse>(TRequest model, Either<string, Uri> url, params Header[] headers)
        {
            return PutJsonAsync<TRequest, TResponse>(model, url, HttpOptions.Default, headers);
        }

        public Task<Maybe<TResponse>> PutJsonAsync<TRequest, TResponse>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers)
        {
            return Json.Maybe.FromModel(model, options).MatchAsync(
                SendAsync<TResponse>(HttpMethod.Put, url, options, headers),
                Maybe.Create<TResponse>
            );
        }

        public Task<Maybe<string>> PatchAsync(string body, Either<string, Uri> url, string contentType, params Header[] headers)
        {
            return PatchAsync(body, url, contentType, HttpOptions.Default, headers);
        }

        public Task<Maybe<string>> PatchAsync(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.SendAsync(body, url.Match(static x => new Uri(x), Identity), contentType, options, headers, HypertextTransferProtocol.Patch).ContinueWith(IdentityMaybe);
        }

        public Task<Maybe<HttpStatus>> PatchJsonAsync<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers)
        {
            return PatchJsonAsync<TRequest, HttpStatus>(model, url, HttpOptions.Default, headers);
        }

        public Task<Maybe<HttpStatus>> PatchJsonAsync<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers)
        {
            return Json.Maybe.FromModel(model, options).MatchAsync(
                SendAsync(HypertextTransferProtocol.Patch, url, options, headers),
                Maybe.Create<HttpStatus>
            );
        }

        public Task<Maybe<TResponse>> PatchJsonAsync<TRequest, TResponse>(TRequest model, Either<string, Uri> url, params Header[] headers)
        {
            return PatchJsonAsync<TRequest, TResponse>(model, url, HttpOptions.Default, headers);
        }

        public Task<Maybe<TResponse>> PatchJsonAsync<TRequest, TResponse>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers)
        {
            return Json.Maybe.FromModel(model, options).MatchAsync(
                SendAsync<TResponse>(HypertextTransferProtocol.Patch, url, options, headers),
                Maybe.Create<TResponse>
            );
        }

        public Task<Maybe<string>> GetAsync(Either<string, Uri> url, params Header[] headers)
        {
            return GetAsync(url, HttpOptions.Default, headers);
        }

        public Task<Maybe<string>> GetAsync(Either<string, Uri> url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.SendAsync(string.Empty, url.Match(static x => new Uri(x), Identity), string.Empty, options, headers, HttpMethod.Get).ContinueWith(IdentityMaybe);
        }

        public Task<Maybe<TResponse>> GetJsonAsync<TResponse>(Either<string, Uri> url, params Header[] headers)
        {
            return GetJsonAsync<TResponse>(url, HttpOptions.Default, headers);
        }

        public Task<Maybe<TResponse>> GetJsonAsync<TResponse>(Either<string, Uri> url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.SendAsync(string.Empty, url.Match(static x => new Uri(x), Identity), string.Empty, options, headers, HttpMethod.Get).ContinueWith(ParseAsync<TResponse>(options));
        }

        public Task<Maybe<string>> DeleteAsync(Either<string, Uri> url, params Header[] headers)
        {
            return DeleteAsync(url, HttpOptions.Default, headers);
        }

        public Task<Maybe<string>> DeleteAsync(Either<string, Uri> url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.SendAsync(string.Empty, url.Match(static x => new Uri(x), Identity), string.Empty, options, headers, HttpMethod.Delete).ContinueWith(IdentityMaybe);
        }

        public Task<Maybe<TResponse>> DeleteJsonAsync<TResponse>(Either<string, Uri> url, params Header[] headers)
        {
            return DeleteJsonAsync<TResponse>(url, HttpOptions.Default, headers);
        }

        public Task<Maybe<TResponse>> DeleteJsonAsync<TResponse>(Either<string, Uri> url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.SendAsync(string.Empty, url.Match(static x => new Uri(x), Identity), string.Empty, options, headers, HttpMethod.Delete).ContinueWith(ParseAsync<TResponse>(options));
        }
    }
}
