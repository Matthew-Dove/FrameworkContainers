﻿using ContainerExpressions.Containers;
using FrameworkContainers.Format;
using FrameworkContainers.Models.Exceptions;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace FrameworkContainers.Network
{
    public sealed class HttpMaybe
    {
        internal HttpMaybe() { }

        public Maybe<string, HttpException> Post(string body, string url, string contentType, params Header[] headers)
        {
            return Post(body, url, contentType, HttpOptions.Default, headers);
        }

        public Maybe<string, HttpException> Post(string body, string url, string contentType, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.Send(body, url, contentType, options, headers, "POST").Match(x => new Maybe<string, HttpException>(x), ex => new Maybe<string, HttpException>(ex));
        }

        public Maybe<TResponse> PostJson<TRequest, TResponse>(TRequest model, string url, params Header[] headers)
        {
            return PostJson<TRequest, TResponse>(model, url, HttpOptions.Default, headers);
        }

        public Maybe<TResponse> PostJson<TRequest, TResponse>(TRequest model, string url, HttpOptions options, params Header[] headers)
        {
            return Json.Maybe.FromModel(model).Match(
                json => HypertextTransferProtocol
                    .Send(json, url, "application/json", options, Http.AddJsonAccept(headers), "POST")
                    .Match(x => Json.Maybe.ToModel<TResponse>(x).Match(y => new Maybe<TResponse>(y), ex => new Maybe<TResponse>(ex)), ex => new Maybe<TResponse>(ex)),
                ex => new Maybe<TResponse>(ex)
            );
        }

        public Maybe<string, HttpException> Put(string body, string url, string contentType, params Header[] headers)
        {
            return Put(body, url, contentType, HttpOptions.Default, headers);
        }

        public Maybe<string, HttpException> Put(string body, string url, string contentType, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.Send(body, url, contentType, options, headers, "PUT").Match(x => new Maybe<string, HttpException>(x), ex => new Maybe<string, HttpException>(ex));
        }

        public Maybe<TResponse> PutJson<TRequest, TResponse>(TRequest model, string url, params Header[] headers)
        {
            return PutJson<TRequest, TResponse>(model, url, HttpOptions.Default, headers);
        }

        public Maybe<TResponse> PutJson<TRequest, TResponse>(TRequest model, string url, HttpOptions options, params Header[] headers)
        {
            return Json.Maybe.FromModel(model).Match(
                json => HypertextTransferProtocol
                    .Send(json, url, "application/json", options, Http.AddJsonAccept(headers), "PUT")
                    .Match(x => Json.Maybe.ToModel<TResponse>(x).Match(y => new Maybe<TResponse>(y), ex => new Maybe<TResponse>(ex)), ex => new Maybe<TResponse>(ex)),
                ex => new Maybe<TResponse>(ex)
            );
        }

        public Maybe<string, HttpException> Get(string url, params Header[] headers)
        {
            return Get(url, HttpOptions.Default, headers);
        }

        public Maybe<string, HttpException> Get(string url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.Send(string.Empty, url, string.Empty, options, headers, "GET").Match(x => new Maybe<string, HttpException>(x), ex => new Maybe<string, HttpException>(ex));
        }

        public Maybe<TResponse> GetJson<TResponse>(string url, params Header[] headers)
        {
            return GetJson<TResponse>(url, HttpOptions.Default, headers);
        }

        public Maybe<TResponse> GetJson<TResponse>(string url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol
                .Send(string.Empty, url, string.Empty, options, Http.AddJsonAccept(headers), "GET")
                .Match(x => Json.Maybe.ToModel<TResponse>(x).Match(y => new Maybe<TResponse>(y), ex => new Maybe<TResponse>(ex)), ex => new Maybe<TResponse>(ex));
        }

        public Maybe<string, HttpException> Delete(string url, params Header[] headers)
        {
            return Delete(url, HttpOptions.Default, headers);
        }

        public Maybe<string, HttpException> Delete(string url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.Send(string.Empty, url, string.Empty, options, headers, "DELETE").Match(x => new Maybe<string, HttpException>(x), ex => new Maybe<string, HttpException>(ex));
        }

        public Maybe<TResponse> DeleteJson<TResponse>(string url, params Header[] headers)
        {
            return DeleteJson<TResponse>(url, HttpOptions.Default, headers);
        }

        public Maybe<TResponse> DeleteJson<TResponse>(string url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol
                .Send(string.Empty, url, string.Empty, options, Http.AddJsonAccept(headers), "DELETE")
                .Match(x => Json.Maybe.ToModel<TResponse>(x).Match(y => new Maybe<TResponse>(y), ex => new Maybe<TResponse>(ex)), ex => new Maybe<TResponse>(ex));
        }

        public Task<Maybe<string, HttpException>> PostAsync(string body, string url, string contentType, params Header[] headers)
        {
            return PostAsync(body, url, contentType, HttpOptions.Default, headers);
        }

        public Task<Maybe<string, HttpException>> PostAsync(string body, string url, string contentType, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.SendAsync(body, url, contentType, options, headers, HttpMethod.Post).ContinueWith(x => x.Result.Match(y => new Maybe<string, HttpException>(y), ex => new Maybe<string, HttpException>(ex)));
        }

        public Task<Maybe<TResponse>> PostJsonAsync<TRequest, TResponse>(TRequest model, string url, params Header[] headers)
        {
            return PostJsonAsync<TRequest, TResponse>(model, url, HttpOptions.Default, headers);
        }

        public Task<Maybe<TResponse>> PostJsonAsync<TRequest, TResponse>(TRequest model, string url, HttpOptions options, params Header[] headers)
        {
            return Json.Maybe.FromModel(model).MatchAsync(
                json => HypertextTransferProtocol
                    .SendAsync(json, url, "application/json", options, Http.AddJsonAccept(headers), HttpMethod.Post)
                    .ContinueWith(x => x.Result.Match(y => Json.Maybe.ToModel<TResponse>(y)
                    .Match(z => new Maybe<TResponse>(z), ex => new Maybe<TResponse>(ex)), ex => new Maybe<TResponse>(ex))),
                ex => new Maybe<TResponse>(ex)
            );
        }

        public Task<Maybe<string, HttpException>> PutAsync(string body, string url, string contentType, params Header[] headers)
        {
            return PutAsync(body, url, contentType, HttpOptions.Default, headers);
        }

        public Task<Maybe<string, HttpException>> PutAsync(string body, string url, string contentType, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.SendAsync(body, url, contentType, options, headers, HttpMethod.Put).ContinueWith(x => x.Result.Match(y => new Maybe<string, HttpException>(y), ex => new Maybe<string, HttpException>(ex)));
        }

        public Task<Maybe<TResponse>> PutJsonAsync<TRequest, TResponse>(TRequest model, string url, params Header[] headers)
        {
            return PutJsonAsync<TRequest, TResponse>(model, url, HttpOptions.Default, headers);
        }

        public Task<Maybe<TResponse>> PutJsonAsync<TRequest, TResponse>(TRequest model, string url, HttpOptions options, params Header[] headers)
        {
            return Json.Maybe.FromModel(model).MatchAsync(
                json => HypertextTransferProtocol
                    .SendAsync(json, url, "application/json", options, Http.AddJsonAccept(headers), HttpMethod.Put)
                    .ContinueWith(x => x.Result.Match(y => Json.Maybe.ToModel<TResponse>(y)
                    .Match(z => new Maybe<TResponse>(z), ex => new Maybe<TResponse>(ex)), ex => new Maybe<TResponse>(ex))),
                ex => new Maybe<TResponse>(ex)
            );
        }

        public Task<Maybe<string, HttpException>> GetAsync(string url, params Header[] headers)
        {
            return GetAsync(url, HttpOptions.Default, headers);
        }

        public Task<Maybe<string, HttpException>> GetAsync(string url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.SendAsync(string.Empty, url, string.Empty, options, headers, HttpMethod.Get).ContinueWith(x => x.Result.Match(y => new Maybe<string, HttpException>(y), ex => new Maybe<string, HttpException>(ex)));
        }

        public Task<Maybe<TResponse>> GetJsonAsync<TResponse>(string url, params Header[] headers)
        {
            return GetJsonAsync<TResponse>(url, HttpOptions.Default, headers);
        }

        public Task<Maybe<TResponse>> GetJsonAsync<TResponse>(string url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.SendAsync(string.Empty, url, string.Empty, options, Http.AddJsonAccept(headers), HttpMethod.Get).ContinueWith(x => x.Result.Match(y => Json.Maybe.ToModel<TResponse>(y).Match(z => new Maybe<TResponse>(z), ex => new Maybe<TResponse>(ex)), ex => new Maybe<TResponse>(ex)));
        }

        public Task<Maybe<string, HttpException>> DeleteAsync(string url, params Header[] headers)
        {
            return DeleteAsync(url, HttpOptions.Default, headers);
        }

        public Task<Maybe<string, HttpException>> DeleteAsync(string url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.SendAsync(string.Empty, url, string.Empty, options, headers, HttpMethod.Delete).ContinueWith(x => x.Result.Match(y => new Maybe<string, HttpException>(y), ex => new Maybe<string, HttpException>(ex)));
        }

        public Task<Maybe<TResponse>> DeleteJsonAsync<TResponse>(string url, params Header[] headers)
        {
            return DeleteJsonAsync<TResponse>(url, HttpOptions.Default, headers);
        }

        public Task<Maybe<TResponse>> DeleteJsonAsync<TResponse>(string url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.SendAsync(string.Empty, url, string.Empty, options, Http.AddJsonAccept(headers), HttpMethod.Delete).ContinueWith(x => x.Result.Match(y => Json.Maybe.ToModel<TResponse>(y).Match(z => new Maybe<TResponse>(z), ex => new Maybe<TResponse>(ex)), ex => new Maybe<TResponse>(ex)));
        }
    }
}
