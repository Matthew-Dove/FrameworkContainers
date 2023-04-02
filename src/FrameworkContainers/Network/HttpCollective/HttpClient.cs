using System;
using System.Threading.Tasks;
using ContainerExpressions.Containers;
using FrameworkContainers.Network.HttpCollective.Models;

namespace FrameworkContainers.Network.HttpCollective
{
    /// <summary>Dependency inversion alterative to the static class.</summary>
    public interface IHttpClient
    {
        HttpMaybe Maybe { get; }
        HttpResponse Response { get; }

        string Post(string body, Either<string, Uri> url, string contentType, params Header[] headers);
        string Post(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers);
        HttpStatus PostJson<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers);
        HttpStatus PostJson<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers);
        TResponse PostJson<TRequest, TResponse>(TRequest model, Either<string, Uri> url, params Header[] headers);
        TResponse PostJson<TRequest, TResponse>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers);

        string Put(string body, Either<string, Uri> url, string contentType, params Header[] headers);
        string Put(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers);
        HttpStatus PutJson<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers);
        HttpStatus PutJson<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers);
        TResponse PutJson<TRequest, TResponse>(TRequest model, Either<string, Uri> url, params Header[] headers);
        TResponse PutJson<TRequest, TResponse>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers);

        string Get(Either<string, Uri> url, params Header[] headers);
        string Get(Either<string, Uri> url, HttpOptions options, params Header[] headers);
        TResponse GetJson<TResponse>(Either<string, Uri> url, params Header[] headers);
        TResponse GetJson<TResponse>(Either<string, Uri> url, HttpOptions options, params Header[] headers);

        string Delete(Either<string, Uri> url, params Header[] headers);
        string Delete(Either<string, Uri> url, HttpOptions options, params Header[] headers);
        TResponse DeleteJson<TResponse>(Either<string, Uri> url, params Header[] headers);
        TResponse DeleteJson<TResponse>(Either<string, Uri> url, HttpOptions options, params Header[] headers);

        string Patch(string body, Either<string, Uri> url, string contentType, params Header[] headers);
        string Patch(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers);
        HttpStatus PatchJson<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers);
        HttpStatus PatchJson<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers);
        TResponse PatchJson<TRequest, TResponse>(TRequest model, Either<string, Uri> url, params Header[] headers);
        TResponse PatchJson<TRequest, TResponse>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers);

        Task<string> PostAsync(string body, Either<string, Uri> url, string contentType, params Header[] headers);
        Task<string> PostAsync(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers);
        Task<HttpStatus> PostJsonAsync<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers);
        Task<HttpStatus> PostJsonAsync<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers);
        Task<TResponse> PostJsonAsync<TRequest, TResponse>(TRequest model, Either<string, Uri> url, params Header[] headers);
        Task<TResponse> PostJsonAsync<TRequest, TResponse>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers);

        Task<string> PutAsync(string body, Either<string, Uri> url, string contentType, params Header[] headers);
        Task<string> PutAsync(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers);
        Task<HttpStatus> PutJsonAsync<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers);
        Task<HttpStatus> PutJsonAsync<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers);
        Task<TResponse> PutJsonAsync<TRequest, TResponse>(TRequest model, Either<string, Uri> url, params Header[] headers);
        Task<TResponse> PutJsonAsync<TRequest, TResponse>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers);

        Task<string> GetAsync(Either<string, Uri> url, params Header[] headers);
        Task<string> GetAsync(Either<string, Uri> url, HttpOptions options, params Header[] headers);
        Task<TResponse> GetJsonAsync<TResponse>(Either<string, Uri> url, params Header[] headers);
        Task<TResponse> GetJsonAsync<TResponse>(Either<string, Uri> url, HttpOptions options, params Header[] headers);

        Task<string> DeleteAsync(Either<string, Uri> url, params Header[] headers);
        Task<string> DeleteAsync(Either<string, Uri> url, HttpOptions options, params Header[] headers);
        Task<TResponse> DeleteJsonAsync<TResponse>(Either<string, Uri> url, params Header[] headers);
        Task<TResponse> DeleteJsonAsync<TResponse>(Either<string, Uri> url, HttpOptions options, params Header[] headers);

        Task<string> PatchAsync(string body, Either<string, Uri> url, string contentType, params Header[] headers);
        Task<string> PatchAsync(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers);
        Task<HttpStatus> PatchJsonAsync<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers);
        Task<HttpStatus> PatchJsonAsync<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers);
        Task<TResponse> PatchJsonAsync<TRequest, TResponse>(TRequest model, Either<string, Uri> url, params Header[] headers);
        Task<TResponse> PatchJsonAsync<TRequest, TResponse>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers);
    }

    public sealed class HttpClient : IHttpClient
    {
        public HttpMaybe Maybe => Http.Maybe;
        public HttpResponse Response => Http.Response;

        public string Post(string body, Either<string, Uri> url, string contentType, params Header[] headers) => Http.Post(body, url, contentType, headers);
        public string Post(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers) => Http.Post(body, url, contentType, options, headers);
        public HttpStatus PostJson<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers) => Http.PostJson<TRequest>(model, url, headers);
        public HttpStatus PostJson<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers) => Http.PostJson<TRequest>(model, url, options, headers);
        public TResponse PostJson<TRequest, TResponse>(TRequest model, Either<string, Uri> url, params Header[] headers) => Http.PostJson<TRequest, TResponse>(model, url, headers);
        public TResponse PostJson<TRequest, TResponse>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers) => Http.PostJson<TRequest, TResponse>(model, url, options, headers);

        public string Put(string body, Either<string, Uri> url, string contentType, params Header[] headers) => Http.Put(body, url, contentType, headers);
        public string Put(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers) => Http.Put(body, url, contentType, options, headers);
        public HttpStatus PutJson<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers) => Http.PutJson<TRequest>(model, url, headers);
        public HttpStatus PutJson<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers) => Http.PutJson<TRequest>(model, url, options, headers);
        public TResponse PutJson<TRequest, TResponse>(TRequest model, Either<string, Uri> url, params Header[] headers) => Http.PutJson<TRequest, TResponse>(model, url, headers);
        public TResponse PutJson<TRequest, TResponse>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers) => Http.PutJson<TRequest, TResponse>(model, url, options, headers);

        public string Get(Either<string, Uri> url, params Header[] headers) => Http.Get(url, headers);
        public string Get(Either<string, Uri> url, HttpOptions options, params Header[] headers) => Http.Get(url, options, headers);
        public TResponse GetJson<TResponse>(Either<string, Uri> url, params Header[] headers) => Http.GetJson<TResponse>(url, headers);
        public TResponse GetJson<TResponse>(Either<string, Uri> url, HttpOptions options, params Header[] headers) => Http.GetJson<TResponse>(url, options, headers);

        public string Delete(Either<string, Uri> url, params Header[] headers) => Http.Delete(url, headers);
        public string Delete(Either<string, Uri> url, HttpOptions options, params Header[] headers) => Http.Delete(url, options, headers);
        public TResponse DeleteJson<TResponse>(Either<string, Uri> url, params Header[] headers) => Http.DeleteJson<TResponse>(url, headers);
        public TResponse DeleteJson<TResponse>(Either<string, Uri> url, HttpOptions options, params Header[] headers) => Http.DeleteJson<TResponse>(url, options, headers);

        public string Patch(string body, Either<string, Uri> url, string contentType, params Header[] headers) => Http.Patch(body, url, contentType, headers);
        public string Patch(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers) => Http.Patch(body, url, contentType, options, headers);
        public HttpStatus PatchJson<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers) => Http.PatchJson<TRequest>(model, url, headers);
        public HttpStatus PatchJson<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers) => Http.PatchJson<TRequest>(model, url, options, headers);
        public TResponse PatchJson<TRequest, TResponse>(TRequest model, Either<string, Uri> url, params Header[] headers) => Http.PatchJson<TRequest, TResponse>(model, url, headers);
        public TResponse PatchJson<TRequest, TResponse>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers) => Http.PatchJson<TRequest, TResponse>(model, url, options, headers);

        public Task<string> PostAsync(string body, Either<string, Uri> url, string contentType, params Header[] headers) => Http.PostAsync(body, url, contentType, headers);
        public Task<string> PostAsync(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers) => Http.PostAsync(body, url, contentType, options, headers);
        public Task<HttpStatus> PostJsonAsync<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers) => Http.PostJsonAsync<TRequest>(model, url, headers);
        public Task<HttpStatus> PostJsonAsync<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers) => Http.PostJsonAsync<TRequest>(model, url, options, headers);
        public Task<TResponse> PostJsonAsync<TRequest, TResponse>(TRequest model, Either<string, Uri> url, params Header[] headers) => Http.PostJsonAsync<TRequest, TResponse>(model, url, headers);
        public Task<TResponse> PostJsonAsync<TRequest, TResponse>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers) => Http.PostJsonAsync<TRequest, TResponse>(model, url, options, headers);

        public Task<string> PutAsync(string body, Either<string, Uri> url, string contentType, params Header[] headers) => Http.PutAsync(body, url, contentType, headers);
        public Task<string> PutAsync(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers) => Http.PutAsync(body, url, contentType, options, headers);
        public Task<HttpStatus> PutJsonAsync<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers) => Http.PutJsonAsync<TRequest>(model, url, headers);
        public Task<HttpStatus> PutJsonAsync<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers) => Http.PutJsonAsync<TRequest>(model, url, options, headers);
        public Task<TResponse> PutJsonAsync<TRequest, TResponse>(TRequest model, Either<string, Uri> url, params Header[] headers) => Http.PutJsonAsync<TRequest, TResponse>(model, url, headers);
        public Task<TResponse> PutJsonAsync<TRequest, TResponse>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers) => Http.PutJsonAsync<TRequest, TResponse>(model, url, options, headers);

        public Task<string> GetAsync(Either<string, Uri> url, params Header[] headers) => Http.GetAsync(url, headers);
        public Task<string> GetAsync(Either<string, Uri> url, HttpOptions options, params Header[] headers) => Http.GetAsync(url, options, headers);
        public Task<TResponse> GetJsonAsync<TResponse>(Either<string, Uri> url, params Header[] headers) => Http.GetJsonAsync<TResponse>(url, headers);
        public Task<TResponse> GetJsonAsync<TResponse>(Either<string, Uri> url, HttpOptions options, params Header[] headers) => Http.GetJsonAsync<TResponse>(url, options, headers);

        public Task<string> DeleteAsync(Either<string, Uri> url, params Header[] headers) => Http.DeleteAsync(url, headers);
        public Task<string> DeleteAsync(Either<string, Uri> url, HttpOptions options, params Header[] headers) => Http.DeleteAsync(url, options, headers);
        public Task<TResponse> DeleteJsonAsync<TResponse>(Either<string, Uri> url, params Header[] headers) => Http.DeleteJsonAsync<TResponse>(url, headers);
        public Task<TResponse> DeleteJsonAsync<TResponse>(Either<string, Uri> url, HttpOptions options, params Header[] headers) => Http.DeleteJsonAsync<TResponse>(url, options, headers);

        public Task<string> PatchAsync(string body, Either<string, Uri> url, string contentType, params Header[] headers) => Http.PatchAsync(body, url, contentType, headers);
        public Task<string> PatchAsync(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers) => Http.PatchAsync(body, url, contentType, options, headers);
        public Task<HttpStatus> PatchJsonAsync<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers) => Http.PatchJsonAsync<TRequest>(model, url, headers);
        public Task<HttpStatus> PatchJsonAsync<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers) => Http.PatchJsonAsync<TRequest>(model, url, options, headers);
        public Task<TResponse> PatchJsonAsync<TRequest, TResponse>(TRequest model, Either<string, Uri> url, params Header[] headers) => Http.PatchJsonAsync<TRequest, TResponse>(model, url, headers);
        public Task<TResponse> PatchJsonAsync<TRequest, TResponse>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers) => Http.PatchJsonAsync<TRequest, TResponse>(model, url, options, headers);
    }

    /// <summary>Dependency inversion alterative to the static class (for a single type).</summary>
    

    /// <summary>Dependency inversion alterative to the static class (for a two types).</summary>


}
