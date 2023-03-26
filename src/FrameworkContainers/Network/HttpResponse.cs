using ContainerExpressions.Containers;
using FrameworkContainers.Format;
using FrameworkContainers.Models;
using FrameworkContainers.Models.Exceptions;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace FrameworkContainers.Network
{
    public sealed class HttpResponse
    {
        internal static readonly HttpResponse Instance = new HttpResponse();

        private HttpResponse() { }

        private static Response<T> Error<T>(HttpException ex) { ex.LogValue(ex.ToString()); return new Response<T>(); }

        private static Func<string, Response<T>> Send<T>(string httpMethod, string url, HttpOptions options, Header[] headers)
        {
            return body => HypertextTransferProtocol
                .Send(body, url, Constants.Http.JSON_CONTENT, options, Http.AddJsonAccept(headers), httpMethod)
                .Match(Json.Response.ToModel<T>, Error<T>);
        }

        private static Func<string, Task<Response<T>>> SendAsync<T>(HttpMethod httpMethod, string url, HttpOptions options, Header[] headers)
        {
            return body => HypertextTransferProtocol
                .SendAsync(body, url, Constants.Http.JSON_CONTENT, options, Http.AddJsonAccept(headers), httpMethod)
                .ContinueWith(static x => x.Result.Match(Json.Response.ToModel<T>, Error<T>));
        }

        public Response<string> Post(string body, string url, string contentType, params Header[] headers)
        {
            return Post(body, url, contentType, HttpOptions.Default, headers);
        }

        public Response<string> Post(string body, string url, string contentType, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.Send(body, url, contentType, options, headers, Constants.Http.POST).Match(Response.Create, Error<string>);
        }

        public Response<TResponse> PostJson<TRequest, TResponse>(TRequest model, string url, params Header[] headers)
        {
            return PostJson<TRequest, TResponse>(model, url, HttpOptions.Default, headers);
        }

        public Response<TResponse> PostJson<TRequest, TResponse>(TRequest model, string url, HttpOptions options, params Header[] headers)
        {
            return Json.Response.FromModel(model).Bind(Send<TResponse>(Constants.Http.POST, url, options, headers));
        }

        public Response<string> Put(string body, string url, string contentType, params Header[] headers)
        {
            return Put(body, url, contentType, HttpOptions.Default, headers);
        }

        public Response<string> Put(string body, string url, string contentType, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.Send(body, url, contentType, options, headers, Constants.Http.PUT).Match(Response.Create, Error<string>);
        }

        public Response<TResponse> PutJson<TRequest, TResponse>(TRequest model, string url, params Header[] headers)
        {
            return PutJson<TRequest, TResponse>(model, url, HttpOptions.Default, headers);
        }

        public Response<TResponse> PutJson<TRequest, TResponse>(TRequest model, string url, HttpOptions options, params Header[] headers)
        {
            return Json.Response.FromModel(model).Bind(Send<TResponse>(Constants.Http.PUT, url, options, headers));
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
            return HypertextTransferProtocol.Send(string.Empty, url, string.Empty, options, Http.AddJsonAccept(headers), Constants.Http.GET).Match(Json.Response.ToModel<TResponse>, Error<TResponse>);
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
            return HypertextTransferProtocol.Send(string.Empty, url, string.Empty, options, Http.AddJsonAccept(headers), Constants.Http.DELETE).Match(Json.Response.ToModel<TResponse>, Error<TResponse>);
        }

        public Task<Response<string>> PostAsync(string body, string url, string contentType, params Header[] headers)
        {
            return PostAsync(body, url, contentType, HttpOptions.Default, headers);
        }

        public Task<Response<string>> PostAsync(string body, string url, string contentType, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.SendAsync(body, url, contentType, options, headers, HttpMethod.Post).ContinueWith(static x => x.Result.Match(Response.Create, Error<string>));
        }

        public Task<Response<TResponse>> PostJsonAsync<TRequest, TResponse>(TRequest model, string url, params Header[] headers)
        {
            return PostJsonAsync<TRequest, TResponse>(model, url, HttpOptions.Default, headers);
        }

        public Task<Response<TResponse>> PostJsonAsync<TRequest, TResponse>(TRequest model, string url, HttpOptions options, params Header[] headers)
        {
            return Json.Response.FromModel(model).BindAsync(SendAsync<TResponse>(HttpMethod.Post, url, options, headers));
        }

        public Task<Response<string>> PutAsync(string body, string url, string contentType, params Header[] headers)
        {
            return PutAsync(body, url, contentType, HttpOptions.Default, headers);
        }

        public Task<Response<string>> PutAsync(string body, string url, string contentType, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.SendAsync(body, url, contentType, options, headers, HttpMethod.Put).ContinueWith(static x => x.Result.Match(Response.Create, Error<string>));
        }

        public Task<Response<TResponse>> PutJsonAsync<TRequest, TResponse>(TRequest model, string url, params Header[] headers)
        {
            return PutJsonAsync<TRequest, TResponse>(model, url, HttpOptions.Default, headers);
        }

        public Task<Response<TResponse>> PutJsonAsync<TRequest, TResponse>(TRequest model, string url, HttpOptions options, params Header[] headers)
        {
            return Json.Response.FromModel(model).BindAsync(SendAsync<TResponse>(HttpMethod.Put, url, options, headers));
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
            return HypertextTransferProtocol.SendAsync(string.Empty, url, string.Empty, options, Http.AddJsonAccept(headers), HttpMethod.Get).ContinueWith(static x => x.Result.Match(Json.Response.ToModel<TResponse>, Error<TResponse>));
        }

        public Task<Response<string>> DeleteAsync(string url, params Header[] headers)
        {
            return DeleteAsync(url, HttpOptions.Default, headers);
        }

        public Task<Response<string>> DeleteAsync(string url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.SendAsync(string.Empty, url, string.Empty, options, headers, HttpMethod.Delete).ContinueWith(static x => x.Result.Match(Response.Create, Error<string>));
        }

        public Task<Response<TResponse>> DeleteAsync<TResponse>(string url, params Header[] headers)
        {
            return DeleteAsync<TResponse>(url, HttpOptions.Default, headers);
        }

        public Task<Response<TResponse>> DeleteAsync<TResponse>(string url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.SendAsync(string.Empty, url, string.Empty, options, Http.AddJsonAccept(headers), HttpMethod.Delete).ContinueWith(static x => x.Result.Match(Json.Response.ToModel<TResponse>, Error<TResponse>));
        }
    }
}
