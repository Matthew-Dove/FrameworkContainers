using System;
using System.Threading.Tasks;
using ContainerExpressions.Containers;
using FrameworkContainers.Network.HttpCollective.Models;

namespace FrameworkContainers.Network.HttpCollective;

/// <summary>Dependency inversion alterative to the static class.</summary>
public interface IHttpClient
{
    HttpMaybe Maybe { get; }
    HttpResponse Response { get; }

    Http245 Post245(string body, Either<string, Uri> url, string contentType, params Header[] headers);
    Http245 Post245(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers);
    HttpBody Post(string body, Either<string, Uri> url, string contentType, params Header[] headers);
    HttpBody Post(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers);
    HttpStatus PostStatus(string body, Either<string, Uri> url, string contentType, params Header[] headers);
    HttpStatus PostStatus(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers);
    HttpStatus PostJsonStatus<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers);
    HttpStatus PostJsonStatus<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers);
    TResponse PostJson<TRequest, TResponse>(TRequest model, Either<string, Uri> url, params Header[] headers);
    TResponse PostJson<TRequest, TResponse>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers);

    Http245 Put245(string body, Either<string, Uri> url, string contentType, params Header[] headers);
    Http245 Put245(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers);
    HttpBody Put(string body, Either<string, Uri> url, string contentType, params Header[] headers);
    HttpBody Put(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers);
    HttpStatus PutStatus(string body, Either<string, Uri> url, string contentType, params Header[] headers);
    HttpStatus PutStatus(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers);
    HttpStatus PutJsonStatus<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers);
    HttpStatus PutJsonStatus<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers);
    TResponse PutJson<TRequest, TResponse>(TRequest model, Either<string, Uri> url, params Header[] headers);
    TResponse PutJson<TRequest, TResponse>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers);

    Http245 Patch245(string body, Either<string, Uri> url, string contentType, params Header[] headers);
    Http245 Patch245(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers);
    HttpBody Patch(string body, Either<string, Uri> url, string contentType, params Header[] headers);
    HttpBody Patch(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers);
    HttpStatus PatchStatus(string body, Either<string, Uri> url, string contentType, params Header[] headers);
    HttpStatus PatchStatus(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers);
    HttpStatus PatchJsonStatus<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers);
    HttpStatus PatchJsonStatus<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers);
    TResponse PatchJson<TRequest, TResponse>(TRequest model, Either<string, Uri> url, params Header[] headers);
    TResponse PatchJson<TRequest, TResponse>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers);

    Http245 Get245(string body, Either<string, Uri> url, string contentType, params Header[] headers);
    Http245 Get245(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers);
    HttpBody Get(Either<string, Uri> url, params Header[] headers);
    HttpBody Get(Either<string, Uri> url, HttpOptions options, params Header[] headers);
    HttpStatus GetStatus(Either<string, Uri> url, params Header[] headers);
    HttpStatus GetStatus(Either<string, Uri> url, HttpOptions options, params Header[] headers);
    TResponse GetJson<TResponse>(Either<string, Uri> url, params Header[] headers);
    TResponse GetJson<TResponse>(Either<string, Uri> url, HttpOptions options, params Header[] headers);

    HttpStatus DeleteStatus(Either<string, Uri> url, params Header[] headers);
    HttpStatus DeleteStatus(Either<string, Uri> url, HttpOptions options, params Header[] headers);

    Task<HttpBody> PostAsync(string body, Either<string, Uri> url, string contentType, params Header[] headers);
    Task<HttpBody> PostAsync(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers);
    Task<HttpStatus> PostStatusAsync(string body, Either<string, Uri> url, string contentType, params Header[] headers);
    Task<HttpStatus> PostStatusAsync(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers);
    Task<HttpStatus> PostJsonStatusAsync<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers);
    Task<HttpStatus> PostJsonStatusAsync<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers);
    Task<TResponse> PostJsonAsync<TRequest, TResponse>(TRequest model, Either<string, Uri> url, params Header[] headers);
    Task<TResponse> PostJsonAsync<TRequest, TResponse>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers);

    Task<HttpBody> PutAsync(string body, Either<string, Uri> url, string contentType, params Header[] headers);
    Task<HttpBody> PutAsync(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers);
    Task<HttpStatus> PutStatusAsync(string body, Either<string, Uri> url, string contentType, params Header[] headers);
    Task<HttpStatus> PutStatusAsync(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers);
    Task<HttpStatus> PutJsonStatusAsync<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers);
    Task<HttpStatus> PutJsonStatusAsync<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers);
    Task<TResponse> PutJsonAsync<TRequest, TResponse>(TRequest model, Either<string, Uri> url, params Header[] headers);
    Task<TResponse> PutJsonAsync<TRequest, TResponse>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers);

    Task<HttpBody> PatchAsync(string body, Either<string, Uri> url, string contentType, params Header[] headers);
    Task<HttpBody> PatchAsync(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers);
    Task<HttpStatus> PatchStatusAsync(string body, Either<string, Uri> url, string contentType, params Header[] headers);
    Task<HttpStatus> PatchStatusAsync(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers);
    Task<HttpStatus> PatchJsonStatusAsync<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers);
    Task<HttpStatus> PatchJsonStatusAsync<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers);
    Task<TResponse> PatchJsonAsync<TRequest, TResponse>(TRequest model, Either<string, Uri> url, params Header[] headers);
    Task<TResponse> PatchJsonAsync<TRequest, TResponse>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers);

    Task<HttpBody> GetAsync(Either<string, Uri> url, params Header[] headers);
    Task<HttpBody> GetAsync(Either<string, Uri> url, HttpOptions options, params Header[] headers);
    Task<HttpStatus> GetStatusAsync(Either<string, Uri> url, params Header[] headers);
    Task<HttpStatus> GetStatusAsync(Either<string, Uri> url, HttpOptions options, params Header[] headers);
    Task<TResponse> GetJsonAsync<TResponse>(Either<string, Uri> url, params Header[] headers);
    Task<TResponse> GetJsonAsync<TResponse>(Either<string, Uri> url, HttpOptions options, params Header[] headers);

    Task<HttpStatus> DeleteStatusAsync(Either<string, Uri> url, params Header[] headers);
    Task<HttpStatus> DeleteStatusAsync(Either<string, Uri> url, HttpOptions options, params Header[] headers);
}

public sealed class HttpClient : IHttpClient
{
    public HttpMaybe Maybe => Http.Maybe;
    public HttpResponse Response => Http.Response;

    public Http245 Post245(string body, Either<string, Uri> url, string contentType, params Header[] headers) => Http.Post245(body, url, contentType, headers);
    public Http245 Post245(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers) => Http.Post245(body, url, contentType, options, headers);
    public HttpBody Post(string body, Either<string, Uri> url, string contentType, params Header[] headers) => Http.Post(body, url, contentType, headers);
    public HttpBody Post(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers) => Http.Post(body, url, contentType, options, headers);
    public HttpStatus PostStatus(string body, Either<string, Uri> url, string contentType, params Header[] headers) => Http.PostStatus(body, url, contentType, headers);
    public HttpStatus PostStatus(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers) => Http.PostStatus(body, url, contentType, options, headers);
    public HttpStatus PostJsonStatus<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers) => Http.PostJsonStatus<TRequest>(model, url, headers);
    public HttpStatus PostJsonStatus<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers) => Http.PostJsonStatus<TRequest>(model, url, options, headers);
    public TResponse PostJson<TRequest, TResponse>(TRequest model, Either<string, Uri> url, params Header[] headers) => Http.PostJson<TRequest, TResponse>(model, url, headers);
    public TResponse PostJson<TRequest, TResponse>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers) => Http.PostJson<TRequest, TResponse>(model, url, options, headers);

    public Http245 Put245(string body, Either<string, Uri> url, string contentType, params Header[] headers) => Http.Put245(body, url, contentType, headers);
    public Http245 Put245(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers) => Http.Put245(body, url, contentType, options, headers);
    public HttpBody Put(string body, Either<string, Uri> url, string contentType, params Header[] headers) => Http.Put(body, url, contentType, headers);
    public HttpBody Put(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers) => Http.Put(body, url, contentType, options, headers);
    public HttpStatus PutStatus(string body, Either<string, Uri> url, string contentType, params Header[] headers) => Http.PutStatus(body, url, contentType, headers);
    public HttpStatus PutStatus(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers) => Http.PutStatus(body, url, contentType, options, headers);
    public HttpStatus PutJsonStatus<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers) => Http.PutJsonStatus<TRequest>(model, url, headers);
    public HttpStatus PutJsonStatus<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers) => Http.PutJsonStatus<TRequest>(model, url, options, headers);
    public TResponse PutJson<TRequest, TResponse>(TRequest model, Either<string, Uri> url, params Header[] headers) => Http.PutJson<TRequest, TResponse>(model, url, headers);
    public TResponse PutJson<TRequest, TResponse>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers) => Http.PutJson<TRequest, TResponse>(model, url, options, headers);

    public Http245 Patch245(string body, Either<string, Uri> url, string contentType, params Header[] headers) => Http.Patch245(body, url, contentType, headers);
    public Http245 Patch245(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers) => Http.Patch245(body, url, contentType, options, headers);
    public HttpBody Patch(string body, Either<string, Uri> url, string contentType, params Header[] headers) => Http.Patch(body, url, contentType, headers);
    public HttpBody Patch(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers) => Http.Patch(body, url, contentType, options, headers);
    public HttpStatus PatchStatus(string body, Either<string, Uri> url, string contentType, params Header[] headers) => Http.PatchStatus(body, url, contentType, headers);
    public HttpStatus PatchStatus(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers) => Http.PatchStatus(body, url, contentType, options, headers);
    public HttpStatus PatchJsonStatus<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers) => Http.PatchJsonStatus<TRequest>(model, url, headers);
    public HttpStatus PatchJsonStatus<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers) => Http.PatchJsonStatus<TRequest>(model, url, options, headers);
    public TResponse PatchJson<TRequest, TResponse>(TRequest model, Either<string, Uri> url, params Header[] headers) => Http.PatchJson<TRequest, TResponse>(model, url, headers);
    public TResponse PatchJson<TRequest, TResponse>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers) => Http.PatchJson<TRequest, TResponse>(model, url, options, headers);

    public Http245 Get245(string body, Either<string, Uri> url, string contentType, params Header[] headers) => Http.Get245(body, url, contentType, headers);
    public Http245 Get245(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers) => Http.Get245(body, url, contentType, options, headers);
    public HttpBody Get(Either<string, Uri> url, params Header[] headers) => Http.Get(url, headers);
    public HttpBody Get(Either<string, Uri> url, HttpOptions options, params Header[] headers) => Http.Get(url, options, headers);
    public HttpStatus GetStatus(Either<string, Uri> url, params Header[] headers) => Http.GetStatus(url, headers);
    public HttpStatus GetStatus(Either<string, Uri> url, HttpOptions options, params Header[] headers) => Http.GetStatus(url, options, headers);
    public TResponse GetJson<TResponse>(Either<string, Uri> url, params Header[] headers) => Http.GetJson<TResponse>(url, headers);
    public TResponse GetJson<TResponse>(Either<string, Uri> url, HttpOptions options, params Header[] headers) => Http.GetJson<TResponse>(url, options, headers);

    public HttpStatus DeleteStatus(Either<string, Uri> url, params Header[] headers) => Http.DeleteStatus(url, headers);
    public HttpStatus DeleteStatus(Either<string, Uri> url, HttpOptions options, params Header[] headers) => Http.DeleteStatus(url, options, headers);

    public Task<HttpBody> PostAsync(string body, Either<string, Uri> url, string contentType, params Header[] headers) => Http.PostAsync(body, url, contentType, headers);
    public Task<HttpBody> PostAsync(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers) => Http.PostAsync(body, url, contentType, options, headers);
    public Task<HttpStatus> PostStatusAsync(string body, Either<string, Uri> url, string contentType, params Header[] headers) => Http.PostStatusAsync(body, url, contentType, headers);
    public Task<HttpStatus> PostStatusAsync(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers) => Http.PostStatusAsync(body, url, contentType, options, headers);
    public Task<HttpStatus> PostJsonStatusAsync<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers) => Http.PostJsonStatusAsync<TRequest>(model, url, headers);
    public Task<HttpStatus> PostJsonStatusAsync<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers) => Http.PostJsonStatusAsync<TRequest>(model, url, options, headers);
    public Task<TResponse> PostJsonAsync<TRequest, TResponse>(TRequest model, Either<string, Uri> url, params Header[] headers) => Http.PostJsonAsync<TRequest, TResponse>(model, url, headers);
    public Task<TResponse> PostJsonAsync<TRequest, TResponse>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers) => Http.PostJsonAsync<TRequest, TResponse>(model, url, options, headers);

    public Task<HttpBody> PutAsync(string body, Either<string, Uri> url, string contentType, params Header[] headers) => Http.PutAsync(body, url, contentType, headers);
    public Task<HttpBody> PutAsync(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers) => Http.PutAsync(body, url, contentType, options, headers);
    public Task<HttpStatus> PutStatusAsync(string body, Either<string, Uri> url, string contentType, params Header[] headers) => Http.PutStatusAsync(body, url, contentType, headers);
    public Task<HttpStatus> PutStatusAsync(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers) => Http.PutStatusAsync(body, url, contentType, options, headers);
    public Task<HttpStatus> PutJsonStatusAsync<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers) => Http.PutJsonStatusAsync<TRequest>(model, url, headers);
    public Task<HttpStatus> PutJsonStatusAsync<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers) => Http.PutJsonStatusAsync<TRequest>(model, url, options, headers);
    public Task<TResponse> PutJsonAsync<TRequest, TResponse>(TRequest model, Either<string, Uri> url, params Header[] headers) => Http.PutJsonAsync<TRequest, TResponse>(model, url, headers);
    public Task<TResponse> PutJsonAsync<TRequest, TResponse>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers) => Http.PutJsonAsync<TRequest, TResponse>(model, url, options, headers);

    public Task<HttpBody> PatchAsync(string body, Either<string, Uri> url, string contentType, params Header[] headers) => Http.PatchAsync(body, url, contentType, headers);
    public Task<HttpBody> PatchAsync(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers) => Http.PatchAsync(body, url, contentType, options, headers);
    public Task<HttpStatus> PatchStatusAsync(string body, Either<string, Uri> url, string contentType, params Header[] headers) => Http.PatchStatusAsync(body, url, contentType, headers);
    public Task<HttpStatus> PatchStatusAsync(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers) => Http.PatchStatusAsync(body, url, contentType, options, headers);
    public Task<HttpStatus> PatchJsonStatusAsync<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers) => Http.PatchJsonStatusAsync<TRequest>(model, url, headers);
    public Task<HttpStatus> PatchJsonStatusAsync<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers) => Http.PatchJsonStatusAsync<TRequest>(model, url, options, headers);
    public Task<TResponse> PatchJsonAsync<TRequest, TResponse>(TRequest model, Either<string, Uri> url, params Header[] headers) => Http.PatchJsonAsync<TRequest, TResponse>(model, url, headers);
    public Task<TResponse> PatchJsonAsync<TRequest, TResponse>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers) => Http.PatchJsonAsync<TRequest, TResponse>(model, url, options, headers);

    public Task<HttpBody> GetAsync(Either<string, Uri> url, params Header[] headers) => Http.GetAsync(url, headers);
    public Task<HttpBody> GetAsync(Either<string, Uri> url, HttpOptions options, params Header[] headers) => Http.GetAsync(url, options, headers);
    public Task<HttpStatus> GetStatusAsync(Either<string, Uri> url, params Header[] headers) => Http.GetStatusAsync(url, headers);
    public Task<HttpStatus> GetStatusAsync(Either<string, Uri> url, HttpOptions options, params Header[] headers) => Http.GetStatusAsync(url, options, headers);
    public Task<TResponse> GetJsonAsync<TResponse>(Either<string, Uri> url, params Header[] headers) => Http.GetJsonAsync<TResponse>(url, headers);
    public Task<TResponse> GetJsonAsync<TResponse>(Either<string, Uri> url, HttpOptions options, params Header[] headers) => Http.GetJsonAsync<TResponse>(url, options, headers);

    public Task<HttpStatus> DeleteStatusAsync(Either<string, Uri> url, params Header[] headers) => Http.DeleteStatusAsync(url, headers);
    public Task<HttpStatus> DeleteStatusAsync(Either<string, Uri> url, HttpOptions options, params Header[] headers) => Http.DeleteStatusAsync(url, options, headers);
}

/// <summary>Dependency inversion alterative to the static class (for a single type).</summary>
public interface IHttpClient<T>
{
    HttpMaybe<T> Maybe { get; }
    HttpResponse<T> Response { get; }

    HttpStatus PostJsonStatus(T model, Either<string, Uri> url, params Header[] headers);
    HttpStatus PostJsonStatus(T model, Either<string, Uri> url, HttpOptions options, params Header[] headers);
    T PostJson<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers);
    T PostJson<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers);

    HttpStatus PutJsonStatus(T model, Either<string, Uri> url, params Header[] headers);
    HttpStatus PutJsonStatus(T model, Either<string, Uri> url, HttpOptions options, params Header[] headers);
    T PutJson<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers);
    T PutJson<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers);

    HttpStatus PatchJsonStatus(T model, Either<string, Uri> url, params Header[] headers);
    HttpStatus PatchJsonStatus(T model, Either<string, Uri> url, HttpOptions options, params Header[] headers);
    T PatchJson<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers);
    T PatchJson<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers);

    T GetJson(Either<string, Uri> url, params Header[] headers);
    T GetJson(Either<string, Uri> url, HttpOptions options, params Header[] headers);

    Task<HttpStatus> PostJsonStatusAsync(T model, Either<string, Uri> url, params Header[] headers);
    Task<HttpStatus> PostJsonStatusAsync(T model, Either<string, Uri> url, HttpOptions options, params Header[] headers);
    Task<T> PostJsonAsync<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers);
    Task<T> PostJsonAsync<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers);

    Task<HttpStatus> PutJsonStatusAsync(T model, Either<string, Uri> url, params Header[] headers);
    Task<HttpStatus> PutJsonStatusAsync(T model, Either<string, Uri> url, HttpOptions options, params Header[] headers);
    Task<T> PutJsonAsync<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers);
    Task<T> PutJsonAsync<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers);

    Task<HttpStatus> PatchJsonStatusAsync(T model, Either<string, Uri> url, params Header[] headers);
    Task<HttpStatus> PatchJsonStatusAsync(T model, Either<string, Uri> url, HttpOptions options, params Header[] headers);
    Task<T> PatchJsonAsync<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers);
    Task<T> PatchJsonAsync<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers);

    Task<T> GetJsonAsync(Either<string, Uri> url, params Header[] headers);
    Task<T> GetJsonAsync(Either<string, Uri> url, HttpOptions options, params Header[] headers);
}

public sealed class HttpClient<T> : IHttpClient<T>
{
    public HttpMaybe<T> Maybe => HttpMaybe<T>.Instance;
    public HttpResponse<T> Response => HttpResponse<T>.Instance;

    public HttpStatus PostJsonStatus(T model, Either<string, Uri> url, params Header[] headers) => Http.PostJsonStatus<T>(model, url, headers);
    public HttpStatus PostJsonStatus(T model, Either<string, Uri> url, HttpOptions options, params Header[] headers) => Http.PostJsonStatus<T>(model, url, options, headers);
    public T PostJson<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers) => Http.PostJson<TRequest, T>(model, url, headers);
    public T PostJson<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers) => Http.PostJson<TRequest, T>(model, url, options, headers);

    public HttpStatus PutJsonStatus(T model, Either<string, Uri> url, params Header[] headers) => Http.PutJsonStatus<T>(model, url, headers);
    public HttpStatus PutJsonStatus(T model, Either<string, Uri> url, HttpOptions options, params Header[] headers) => Http.PutJsonStatus<T>(model, url, options, headers);
    public T PutJson<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers) => Http.PutJson<TRequest, T>(model, url, headers);
    public T PutJson<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers) => Http.PutJson<TRequest, T>(model, url, options, headers);

    public HttpStatus PatchJsonStatus(T model, Either<string, Uri> url, params Header[] headers) => Http.PatchJsonStatus<T>(model, url, headers);
    public HttpStatus PatchJsonStatus(T model, Either<string, Uri> url, HttpOptions options, params Header[] headers) => Http.PatchJsonStatus<T>(model, url, options, headers);
    public T PatchJson<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers) => Http.PatchJson<TRequest, T>(model, url, headers);
    public T PatchJson<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers) => Http.PatchJson<TRequest, T>(model, url, options, headers);

    public T GetJson(Either<string, Uri> url, params Header[] headers) => Http.GetJson<T>(url, headers);
    public T GetJson(Either<string, Uri> url, HttpOptions options, params Header[] headers) => Http.GetJson<T>(url, options, headers);

    public Task<HttpStatus> PostJsonStatusAsync(T model, Either<string, Uri> url, params Header[] headers) => Http.PostJsonStatusAsync<T>(model, url, headers);
    public Task<HttpStatus> PostJsonStatusAsync(T model, Either<string, Uri> url, HttpOptions options, params Header[] headers) => Http.PostJsonStatusAsync<T>(model, url, options, headers);
    public Task<T> PostJsonAsync<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers) => Http.PostJsonAsync<TRequest, T>(model, url, headers);
    public Task<T> PostJsonAsync<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers) => Http.PostJsonAsync<TRequest, T>(model, url, options, headers);

    public Task<HttpStatus> PutJsonStatusAsync(T model, Either<string, Uri> url, params Header[] headers) => Http.PutJsonStatusAsync<T>(model, url, headers);
    public Task<HttpStatus> PutJsonStatusAsync(T model, Either<string, Uri> url, HttpOptions options, params Header[] headers) => Http.PutJsonStatusAsync<T>(model, url, options, headers);
    public Task<T> PutJsonAsync<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers) => Http.PutJsonAsync<TRequest, T>(model, url, headers);
    public Task<T> PutJsonAsync<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers) => Http.PutJsonAsync<TRequest, T>(model, url, options, headers);

    public Task<HttpStatus> PatchJsonStatusAsync(T model, Either<string, Uri> url, params Header[] headers) => Http.PatchJsonStatusAsync<T>(model, url, headers);
    public Task<HttpStatus> PatchJsonStatusAsync(T model, Either<string, Uri> url, HttpOptions options, params Header[] headers) => Http.PatchJsonStatusAsync<T>(model, url, options, headers);
    public Task<T> PatchJsonAsync<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers) => Http.PatchJsonAsync<TRequest, T>(model, url, headers);
    public Task<T> PatchJsonAsync<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers) => Http.PatchJsonAsync<TRequest, T>(model, url, options, headers);

    public Task<T> GetJsonAsync(Either<string, Uri> url, params Header[] headers) => Http.GetJsonAsync<T>(url, headers);
    public Task<T> GetJsonAsync(Either<string, Uri> url, HttpOptions options, params Header[] headers) => Http.GetJsonAsync<T>(url, options, headers);
}

/// <summary>Dependency inversion alterative to the static class (for a two types).</summary>
public interface IHttpClient<TRequest, TResponse>
{
    HttpMaybe<TRequest, TResponse> Maybe { get; }
    HttpResponse<TRequest, TResponse> Response { get; }

    TResponse PostJson(TRequest model, Either<string, Uri> url, params Header[] headers);
    TResponse PostJson(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers);

    TResponse PutJson(TRequest model, Either<string, Uri> url, params Header[] headers);
    TResponse PutJson(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers);

    TResponse PatchJson(TRequest model, Either<string, Uri> url, params Header[] headers);
    TResponse PatchJson(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers);

    Task<TResponse> PostJsonAsync(TRequest model, Either<string, Uri> url, params Header[] headers);
    Task<TResponse> PostJsonAsync(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers);

    Task<TResponse> PutJsonAsync(TRequest model, Either<string, Uri> url, params Header[] headers);
    Task<TResponse> PutJsonAsync(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers);

    Task<TResponse> PatchJsonAsync(TRequest model, Either<string, Uri> url, params Header[] headers);
    Task<TResponse> PatchJsonAsync(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers);
}

public sealed class HttpClient<TRequest, TResponse> : IHttpClient<TRequest, TResponse>
{
    public HttpMaybe<TRequest, TResponse> Maybe => HttpMaybe<TRequest, TResponse>.Instance;
    public HttpResponse<TRequest, TResponse> Response => HttpResponse<TRequest, TResponse>.Instance;

    public TResponse PostJson(TRequest model, Either<string, Uri> url, params Header[] headers) => Http.PostJson<TRequest, TResponse>(model, url, headers);
    public TResponse PostJson(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers) => Http.PostJson<TRequest, TResponse>(model, url, options, headers);

    public TResponse PutJson(TRequest model, Either<string, Uri> url, params Header[] headers) => Http.PutJson<TRequest, TResponse>(model, url, headers);
    public TResponse PutJson(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers) => Http.PutJson<TRequest, TResponse>(model, url, options, headers);

    public TResponse PatchJson(TRequest model, Either<string, Uri> url, params Header[] headers) => Http.PatchJson<TRequest, TResponse>(model, url, headers);
    public TResponse PatchJson(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers) => Http.PatchJson<TRequest, TResponse>(model, url, options, headers);

    public Task<TResponse> PostJsonAsync(TRequest model, Either<string, Uri> url, params Header[] headers) => Http.PostJsonAsync<TRequest, TResponse>(model, url, headers);
    public Task<TResponse> PostJsonAsync(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers) => Http.PostJsonAsync<TRequest, TResponse>(model, url, options, headers);

    public Task<TResponse> PutJsonAsync(TRequest model, Either<string, Uri> url, params Header[] headers) => Http.PutJsonAsync<TRequest, TResponse>(model, url, headers);
    public Task<TResponse> PutJsonAsync(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers) => Http.PutJsonAsync<TRequest, TResponse>(model, url, options, headers);

    public Task<TResponse> PatchJsonAsync(TRequest model, Either<string, Uri> url, params Header[] headers) => Http.PatchJsonAsync<TRequest, TResponse>(model, url, headers);
    public Task<TResponse> PatchJsonAsync(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers) => Http.PatchJsonAsync<TRequest, TResponse>(model, url, options, headers);
}
