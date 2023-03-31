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
    public sealed class HttpResponse
    {
        internal static readonly HttpResponse Instance = new HttpResponse();

        private HttpResponse() { }

        private static Func<string, Response<T>> Parse<T>(JsonOptions options) { return json => Json.Response.ToModel<T>(json, options); }

        private static Func<Task<Either<string, HttpException>>, Response<T>> ParseAsync<T>(JsonOptions options) { return x => x.Result.Match(Parse<T>(options), Error<T>); }

        private static Response<T> Error<T>(HttpException ex) { ex.LogValue(ex.ToString()); return new Response<T>(); }

        private static Func<string, Response<HttpStatus>> Send(string httpMethod, string url, HttpOptions options, Header[] headers)
        {
            return body => HypertextTransferProtocol
                .Send(body, url, Constants.Http.JSON_CONTENT, new HttpOptions(options, retrieveHttpStatus: true), Http.AddJsonHeaders(headers), httpMethod)
                .Match(static x => new Response<HttpStatus>(new HttpStatus(x)), Error<HttpStatus>);
        }

        private static Func<string, Response<T>> Send<T>(string httpMethod, string url, HttpOptions options, Header[] headers)
        {
            return body => HypertextTransferProtocol
                .Send(body, url, Constants.Http.JSON_CONTENT, options, Http.AddJsonHeaders(headers), httpMethod)
                .Match(Parse<T>(options), Error<T>);
        }

        private static Func<string, Task<Response<HttpStatus>>> SendAsync(HttpMethod httpMethod, string url, HttpOptions options, Header[] headers)
        {
            return body => HypertextTransferProtocol
                .SendAsync(body, url, Constants.Http.JSON_CONTENT, options, Http.AddJsonHeaders(headers), httpMethod)
                .ContinueWith(static t => t.Result.Match(static x => new Response<HttpStatus>(new HttpStatus(x)), Error<HttpStatus>));
        }

        private static Func<string, Task<Response<T>>> SendAsync<T>(HttpMethod httpMethod, string url, HttpOptions options, Header[] headers)
        {
            return body => HypertextTransferProtocol
                .SendAsync(body, url, Constants.Http.JSON_CONTENT, options, Http.AddJsonHeaders(headers), httpMethod)
                .ContinueWith(ParseAsync<T>(options));
        }

        public Response<string> Post(string body, string url, string contentType, params Header[] headers)
        {
            return Post(body, url, contentType, HttpOptions.Default, headers);
        }

        public Response<string> Post(string body, string url, string contentType, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.Send(body, url, contentType, options, headers, Constants.Http.POST).Match(Response.Create, Error<string>);
        }

        public Response<HttpStatus> PostJson<TRequest>(TRequest model, string url, params Header[] headers)
        {
            return PostJson<TRequest>(model, url, HttpOptions.Default, headers);
        }

        public Response<HttpStatus> PostJson<TRequest>(TRequest model, string url, HttpOptions options, params Header[] headers)
        {
            return Json.Response.FromModel(model, options).Bind(Send(Constants.Http.POST, url, options, headers));
        }

        public Response<TResponse> PostJson<TRequest, TResponse>(TRequest model, string url, params Header[] headers)
        {
            return PostJson<TRequest, TResponse>(model, url, HttpOptions.Default, headers);
        }

        public Response<TResponse> PostJson<TRequest, TResponse>(TRequest model, string url, HttpOptions options, params Header[] headers)
        {
            return Json.Response.FromModel(model, options).Bind(Send<TResponse>(Constants.Http.POST, url, options, headers));
        }

        public Response<string> Put(string body, string url, string contentType, params Header[] headers)
        {
            return Put(body, url, contentType, HttpOptions.Default, headers);
        }

        public Response<string> Put(string body, string url, string contentType, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.Send(body, url, contentType, options, headers, Constants.Http.PUT).Match(Response.Create, Error<string>);
        }

        public Response<HttpStatus> PutJson<TRequest>(TRequest model, string url, params Header[] headers)
        {
            return PutJson<TRequest>(model, url, HttpOptions.Default, headers);
        }

        public Response<HttpStatus> PutJson<TRequest>(TRequest model, string url, HttpOptions options, params Header[] headers)
        {
            return Json.Response.FromModel(model, options).Bind(Send(Constants.Http.PUT, url, options, headers));
        }

        public Response<TResponse> PutJson<TRequest, TResponse>(TRequest model, string url, params Header[] headers)
        {
            return PutJson<TRequest, TResponse>(model, url, HttpOptions.Default, headers);
        }

        public Response<TResponse> PutJson<TRequest, TResponse>(TRequest model, string url, HttpOptions options, params Header[] headers)
        {
            return Json.Response.FromModel(model, options).Bind(Send<TResponse>(Constants.Http.PUT, url, options, headers));
        }

        public Response<string> Get(string url, params Header[] headers)
        {
            return Get(url, HttpOptions.Default, headers);
        }

        public Response<string> Get(string url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.Send(string.Empty, url, string.Empty, options, headers, Constants.Http.GET).Match(Response.Create, Error<string>);
        }

        public Response<TResponse> GetJson<TResponse>(string url, params Header[] headers)
        {
            return GetJson<TResponse>(url, HttpOptions.Default, headers);
        }

        public Response<TResponse> GetJson<TResponse>(string url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.Send(string.Empty, url, string.Empty, options, Http.AddJsonHeaders(headers), Constants.Http.GET).Match(Parse<TResponse>(options), Error<TResponse>);
        }

        public Response<string> Delete(string url, params Header[] headers)
        {
            return Delete(url, HttpOptions.Default, headers);
        }

        public Response<string> Delete(string url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.Send(string.Empty, url, string.Empty, options, headers, Constants.Http.DELETE).Match(Response.Create, Error<string>);
        }

        public Response<TResponse> DeleteJson<TResponse>(string url, params Header[] headers)
        {
            return DeleteJson<TResponse>(url, HttpOptions.Default, headers);
        }

        public Response<TResponse> DeleteJson<TResponse>(string url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.Send(string.Empty, url, string.Empty, options, Http.AddJsonHeaders(headers), Constants.Http.DELETE).Match(Parse<TResponse>(options), Error<TResponse>);
        }

        public Response<string> Patch(string body, string url, string contentType, params Header[] headers)
        {
            return Patch(body, url, contentType, HttpOptions.Default, headers);
        }

        public Response<string> Patch(string body, string url, string contentType, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.Send(body, url, contentType, options, headers, Constants.Http.PATCH).Match(Response.Create, Error<string>);
        }

        public Response<HttpStatus> PatchJson<TRequest>(TRequest model, string url, params Header[] headers)
        {
            return PatchJson<TRequest>(model, url, HttpOptions.Default, headers);
        }

        public Response<HttpStatus> PatchJson<TRequest>(TRequest model, string url, HttpOptions options, params Header[] headers)
        {
            return Json.Response.FromModel(model, options).Bind(Send(Constants.Http.PATCH, url, options, headers));
        }

        public Response<TResponse> PatchJson<TRequest, TResponse>(TRequest model, string url, params Header[] headers)
        {
            return PatchJson<TRequest, TResponse>(model, url, HttpOptions.Default, headers);
        }

        public Response<TResponse> PatchJson<TRequest, TResponse>(TRequest model, string url, HttpOptions options, params Header[] headers)
        {
            return Json.Response.FromModel(model, options).Bind(Send<TResponse>(Constants.Http.PATCH, url, options, headers));
        }

        public Task<Response<string>> PostAsync(string body, string url, string contentType, params Header[] headers)
        {
            return PostAsync(body, url, contentType, HttpOptions.Default, headers);
        }

        public Task<Response<string>> PostAsync(string body, string url, string contentType, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.SendAsync(body, url, contentType, options, headers, HttpMethod.Post).ContinueWith(static x => x.Result.Match(Response.Create, Error<string>));
        }

        public Task<Response<HttpStatus>> PostJsonAsync<TRequest>(TRequest model, string url, params Header[] headers)
        {
            return PostJsonAsync<TRequest>(model, url, HttpOptions.Default, headers);
        }

        public Task<Response<HttpStatus>> PostJsonAsync<TRequest>(TRequest model, string url, HttpOptions options, params Header[] headers)
        {
            return Json.Response.FromModel(model, options).BindAsync(SendAsync(HttpMethod.Post, url, options, headers));
        }

        public Task<Response<TResponse>> PostJsonAsync<TRequest, TResponse>(TRequest model, string url, params Header[] headers)
        {
            return PostJsonAsync<TRequest, TResponse>(model, url, HttpOptions.Default, headers);
        }

        public Task<Response<TResponse>> PostJsonAsync<TRequest, TResponse>(TRequest model, string url, HttpOptions options, params Header[] headers)
        {
            return Json.Response.FromModel(model, options).BindAsync(SendAsync<TResponse>(HttpMethod.Post, url, options, headers));
        }

        public Task<Response<string>> PutAsync(string body, string url, string contentType, params Header[] headers)
        {
            return PutAsync(body, url, contentType, HttpOptions.Default, headers);
        }

        public Task<Response<string>> PutAsync(string body, string url, string contentType, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.SendAsync(body, url, contentType, options, headers, HttpMethod.Put).ContinueWith(static x => x.Result.Match(Response.Create, Error<string>));
        }

        public Task<Response<HttpStatus>> PutJsonAsync<TRequest>(TRequest model, string url, params Header[] headers)
        {
            return PutJsonAsync<TRequest>(model, url, HttpOptions.Default, headers);
        }

        public Task<Response<HttpStatus>> PutJsonAsync<TRequest>(TRequest model, string url, HttpOptions options, params Header[] headers)
        {
            return Json.Response.FromModel(model, options).BindAsync(SendAsync(HttpMethod.Put, url, options, headers));
        }

        public Task<Response<TResponse>> PutJsonAsync<TRequest, TResponse>(TRequest model, string url, params Header[] headers)
        {
            return PutJsonAsync<TRequest, TResponse>(model, url, HttpOptions.Default, headers);
        }

        public Task<Response<TResponse>> PutJsonAsync<TRequest, TResponse>(TRequest model, string url, HttpOptions options, params Header[] headers)
        {
            return Json.Response.FromModel(model, options).BindAsync(SendAsync<TResponse>(HttpMethod.Put, url, options, headers));
        }

        public Task<Response<string>> GetAsync(string url, params Header[] headers)
        {
            return GetAsync(url, HttpOptions.Default, headers);
        }

        public Task<Response<string>> GetAsync(string url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.SendAsync(string.Empty, url, string.Empty, options, headers, HttpMethod.Get).ContinueWith(static x => x.Result.Match(Response.Create, Error<string>));
        }

        public Task<Response<TResponse>> GetJsonAsync<TResponse>(string url, params Header[] headers)
        {
            return GetJsonAsync<TResponse>(url, HttpOptions.Default, headers);
        }

        public Task<Response<TResponse>> GetJsonAsync<TResponse>(string url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.SendAsync(string.Empty, url, string.Empty, options, Http.AddJsonHeaders(headers), HttpMethod.Get).ContinueWith(ParseAsync<TResponse>(options));
        }

        public Task<Response<string>> DeleteAsync(string url, params Header[] headers)
        {
            return DeleteAsync(url, HttpOptions.Default, headers);
        }

        public Task<Response<string>> DeleteAsync(string url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.SendAsync(string.Empty, url, string.Empty, options, headers, HttpMethod.Delete).ContinueWith(static x => x.Result.Match(Response.Create, Error<string>));
        }

        public Task<Response<TResponse>> DeleteJsonAsync<TResponse>(string url, params Header[] headers)
        {
            return DeleteJsonAsync<TResponse>(url, HttpOptions.Default, headers);
        }

        public Task<Response<TResponse>> DeleteJsonAsync<TResponse>(string url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.SendAsync(string.Empty, url, string.Empty, options, Http.AddJsonHeaders(headers), HttpMethod.Delete).ContinueWith(ParseAsync<TResponse>(options));
        }

        public Task<Response<string>> PatchAsync(string body, string url, string contentType, params Header[] headers)
        {
            return PatchAsync(body, url, contentType, HttpOptions.Default, headers);
        }

        public Task<Response<string>> PatchAsync(string body, string url, string contentType, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.SendAsync(body, url, contentType, options, headers, HypertextTransferProtocol.Patch).ContinueWith(static x => x.Result.Match(Response.Create, Error<string>));
        }

        public Task<Response<HttpStatus>> PatchJsonAsync<TRequest>(TRequest model, string url, params Header[] headers)
        {
            return PutJsonAsync<TRequest>(model, url, HttpOptions.Default, headers);
        }

        public Task<Response<HttpStatus>> PatchJsonAsync<TRequest>(TRequest model, string url, HttpOptions options, params Header[] headers)
        {
            return Json.Response.FromModel(model, options).BindAsync(SendAsync(HypertextTransferProtocol.Patch, url, options, headers));
        }

        public Task<Response<TResponse>> PatchJsonAsync<TRequest, TResponse>(TRequest model, string url, params Header[] headers)
        {
            return PutJsonAsync<TRequest, TResponse>(model, url, HttpOptions.Default, headers);
        }

        public Task<Response<TResponse>> PatchJsonAsync<TRequest, TResponse>(TRequest model, string url, HttpOptions options, params Header[] headers)
        {
            return Json.Response.FromModel(model, options).BindAsync(SendAsync<TResponse>(HypertextTransferProtocol.Patch, url, options, headers));
        }
    }

    public sealed class HttpResponse<T>
    {
        internal static readonly HttpResponse<T> Instance = new HttpResponse<T>();

        private HttpResponse() { }

        public Response<HttpStatus> PostJson(T model, string url, params Header[] headers) => HttpResponse.Instance.PostJson<T>(model, url, headers);

        public Response<HttpStatus> PostJson(T model, string url, HttpOptions options, params Header[] headers) => HttpResponse.Instance.PostJson<T>(model, url, options, headers);

        public Response<T> PostJson<TRequest>(TRequest model, string url, params Header[] headers) => HttpResponse.Instance.PostJson<TRequest, T>(model, url, headers);

        public Response<T> PostJson<TRequest>(TRequest model, string url, HttpOptions options, params Header[] headers) => HttpResponse.Instance.PostJson<TRequest, T>(model, url, options, headers);

        public Response<HttpStatus> PutJson(T model, string url, params Header[] headers) => HttpResponse.Instance.PutJson<T>(model, url, headers);

        public Response<HttpStatus> PutJson(T model, string url, HttpOptions options, params Header[] headers) => HttpResponse.Instance.PutJson<T>(model, url, options, headers);

        public Response<T> PutJson<TRequest>(TRequest model, string url, params Header[] headers) => HttpResponse.Instance.PutJson<TRequest, T>(model, url, headers);

        public Response<T> PutJson<TRequest>(TRequest model, string url, HttpOptions options, params Header[] headers) => HttpResponse.Instance.PutJson<TRequest, T>(model, url, options, headers);

        public Response<T> GetJson(string url, params Header[] headers) => HttpResponse.Instance.GetJson<T>(url, headers);

        public Response<T> GetJson(string url, HttpOptions options, params Header[] headers) => HttpResponse.Instance.GetJson<T>(url, headers);

        public Response<T> DeleteJson(string url, params Header[] headers) => HttpResponse.Instance.DeleteJson<T>(url, headers);

        public Response<T> DeleteJson(string url, HttpOptions options, params Header[] headers) => HttpResponse.Instance.DeleteJson<T>(url, options, headers);

        public Response<HttpStatus> PatchJson(T model, string url, params Header[] headers) => HttpResponse.Instance.PatchJson<T>(model, url, headers);

        public Response<HttpStatus> PatchJson(T model, string url, HttpOptions options, params Header[] headers) => HttpResponse.Instance.PatchJson<T>(model, url, options, headers);

        public Response<T> PatchJson<TRequest>(TRequest model, string url, params Header[] headers) => HttpResponse.Instance.PatchJson<TRequest, T>(model, url, headers);

        public Response<T> PatchJson<TRequest>(TRequest model, string url, HttpOptions options, params Header[] headers) => HttpResponse.Instance.PatchJson<TRequest, T>(model, url, options, headers);

        public Task<Response<HttpStatus>> PostJsonAsync(T model, string url, params Header[] headers) => HttpResponse.Instance.PostJsonAsync<T>(model, url, headers);

        public Task<Response<HttpStatus>> PostJsonAsync(T model, string url, HttpOptions options, params Header[] headers) => HttpResponse.Instance.PostJsonAsync<T>(model, url, options, headers);

        public Task<Response<T>> PostJsonAsync<TRequest>(TRequest model, string url, params Header[] headers) => HttpResponse.Instance.PostJsonAsync<TRequest, T>(model, url, headers);

        public Task<Response<T>> PostJsonAsync<TRequest>(TRequest model, string url, HttpOptions options, params Header[] headers) => HttpResponse.Instance.PostJsonAsync<TRequest, T>(model, url, options, headers);

        public Task<Response<HttpStatus>> PutJsonAsync(T model, string url, params Header[] headers) => HttpResponse.Instance.PutJsonAsync<T>(model, url, headers);

        public Task<Response<HttpStatus>> PutJsonAsync(T model, string url, HttpOptions options, params Header[] headers) => HttpResponse.Instance.PutJsonAsync<T>(model, url, options, headers);

        public Task<Response<TResponse>> PutJsonAsync<TResponse>(T model, string url, params Header[] headers) => HttpResponse.Instance.PutJsonAsync<T, TResponse>(model, url, headers);

        public Task<Response<T>> PutJsonAsync<TRequest>(TRequest model, string url, HttpOptions options, params Header[] headers) => HttpResponse.Instance.PutJsonAsync<TRequest, T>(model, url, options, headers);

        public Task<Response<T>> GetJsonAsync(string url, params Header[] headers) => HttpResponse.Instance.GetJsonAsync<T>(url, headers);

        public Task<Response<T>> GetJsonAsync(string url, HttpOptions options, params Header[] headers) => HttpResponse.Instance.GetJsonAsync<T>(url, options, headers);

        public Task<Response<T>> DeleteJsonAsync(string url, params Header[] headers) => HttpResponse.Instance.DeleteJsonAsync<T>(url, headers);

        public Task<Response<T>> DeleteJsonAsync(string url, HttpOptions options, params Header[] headers) => HttpResponse.Instance.DeleteJsonAsync<T>(url, options, headers);

        public Task<Response<HttpStatus>> PatchJsonAsync(T model, string url, params Header[] headers) => HttpResponse.Instance.PatchJsonAsync<T>(model, url, headers);

        public Task<Response<HttpStatus>> PatchJsonAsync(T model, string url, HttpOptions options, params Header[] headers) => HttpResponse.Instance.PatchJsonAsync<T>(model, url, options, headers);

        public Task<Response<T>> PatchJsonAsync<TRequest>(TRequest model, string url, params Header[] headers) => HttpResponse.Instance.PatchJsonAsync<TRequest, T>(model, url, headers);

        public Task<Response<T>> PatchJsonAsync<TRequest>(TRequest model, string url, HttpOptions options, params Header[] headers) => HttpResponse.Instance.PatchJsonAsync<TRequest, T>(model, url, options, headers);
    }

    public sealed class HttpResponse<TRequest, TResponse>
    {
        internal static readonly HttpResponse<TRequest, TResponse> Instance = new HttpResponse<TRequest, TResponse>();

        private HttpResponse() { }

        public Response<TResponse> PostJson(TRequest model, string url, params Header[] headers) => HttpResponse.Instance.PostJson<TRequest, TResponse>(model, url, headers);

        public Response<TResponse> PostJson(TRequest model, string url, HttpOptions options, params Header[] headers) => HttpResponse.Instance.PostJson<TRequest, TResponse>(model, url, options, headers);

        public Response<TResponse> PutJson(TRequest model, string url, params Header[] headers) => HttpResponse.Instance.PutJson<TRequest, TResponse>(model, url, headers);

        public Response<TResponse> PutJson(TRequest model, string url, HttpOptions options, params Header[] headers) => HttpResponse.Instance.PutJson<TRequest, TResponse>(model, url, options, headers);

        public Response<TResponse> PatchJson(TRequest model, string url, params Header[] headers) => HttpResponse.Instance.PatchJson<TRequest, TResponse>(model, url, headers);

        public Response<TResponse> PatchJson(TRequest model, string url, HttpOptions options, params Header[] headers) => HttpResponse.Instance.PatchJson<TRequest, TResponse>(model, url, options, headers);

        public Task<Response<TResponse>> PostJsonAsync(TRequest model, string url, params Header[] headers) => HttpResponse.Instance.PostJsonAsync<TRequest, TResponse>(model, url, headers);

        public Task<Response<TResponse>> PostJsonAsync(TRequest model, string url, HttpOptions options, params Header[] headers) => HttpResponse.Instance.PostJsonAsync<TRequest, TResponse>(model, url, options, headers);

        public Task<Response<TResponse>> PutJsonAsync(TRequest model, string url, params Header[] headers) => HttpResponse.Instance.PutJsonAsync<TRequest, TResponse>(model, url, headers);

        public Task<Response<TResponse>> PutJsonAsync(TRequest model, string url, HttpOptions options, params Header[] headers) => HttpResponse.Instance.PutJsonAsync<TRequest, TResponse>(model, url, options, headers);

        public Task<Response<TResponse>> PatchJsonAsync(TRequest model, string url, params Header[] headers) => HttpResponse.Instance.PatchJsonAsync<TRequest, TResponse>(model, url, headers);

        public Task<Response<TResponse>> PatchJsonAsync(TRequest model, string url, HttpOptions options, params Header[] headers) => HttpResponse.Instance.PatchJsonAsync<TRequest, TResponse>(model, url, options, headers);
    }
}
