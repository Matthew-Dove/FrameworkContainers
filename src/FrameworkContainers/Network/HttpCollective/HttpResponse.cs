﻿using ContainerExpressions.Containers;
using FrameworkContainers.Format.JsonCollective;
using FrameworkContainers.Format.JsonCollective.Models;
using FrameworkContainers.Models;
using FrameworkContainers.Network.HttpCollective.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace FrameworkContainers.Network.HttpCollective;

public interface IHttpResponse
{
    Response<Http245> Post245(string body, Either<string, Uri> url, string contentType, params Header[] headers);
    Response<Http245> Post245(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers);
    Response<HttpBody> Post(string body, Either<string, Uri> url, string contentType, params Header[] headers);
    Response<HttpBody> Post(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers);
    Response<HttpStatus> PostStatus(string body, Either<string, Uri> url, string contentType, params Header[] headers);
    Response<HttpStatus> PostStatus(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers);
    Response<HttpStatus> PostJsonStatus<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers);
    Response<HttpStatus> PostJsonStatus<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers);
    Response<TResponse> PostJson<TRequest, TResponse>(TRequest model, Either<string, Uri> url, params Header[] headers);
    Response<TResponse> PostJson<TRequest, TResponse>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers);

    Response<Http245> Put245(string body, Either<string, Uri> url, string contentType, params Header[] headers);
    Response<Http245> Put245(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers);
    Response<HttpBody> Put(string body, Either<string, Uri> url, string contentType, params Header[] headers);
    Response<HttpBody> Put(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers);
    Response<HttpStatus> PutStatus(string body, Either<string, Uri> url, string contentType, params Header[] headers);
    Response<HttpStatus> PutStatus(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers);
    Response<HttpStatus> PutJsonStatus<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers);
    Response<HttpStatus> PutJsonStatus<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers);
    Response<TResponse> PutJson<TRequest, TResponse>(TRequest model, Either<string, Uri> url, params Header[] headers);
    Response<TResponse> PutJson<TRequest, TResponse>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers);

    Response<Http245> Patch245(string body, Either<string, Uri> url, string contentType, params Header[] headers);
    Response<Http245> Patch245(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers);
    Response<HttpBody> Patch(string body, Either<string, Uri> url, string contentType, params Header[] headers);
    Response<HttpBody> Patch(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers);
    Response<HttpStatus> PatchStatus(string body, Either<string, Uri> url, string contentType, params Header[] headers);
    Response<HttpStatus> PatchStatus(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers);
    Response<HttpStatus> PatchJsonStatus<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers);
    Response<HttpStatus> PatchJsonStatus<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers);
    Response<TResponse> PatchJson<TRequest, TResponse>(TRequest model, Either<string, Uri> url, params Header[] headers);
    Response<TResponse> PatchJson<TRequest, TResponse>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers);

    Response<Http245> Get245(Either<string, Uri> url, params Header[] headers);
    Response<Http245> Get245(Either<string, Uri> url, HttpOptions options, params Header[] headers);
    Response<HttpBody> Get(Either<string, Uri> url, params Header[] headers);
    Response<HttpBody> Get(Either<string, Uri> url, HttpOptions options, params Header[] headers);
    Response<HttpStatus> GetStatus(Either<string, Uri> url, params Header[] headers);
    Response<HttpStatus> GetStatus(Either<string, Uri> url, HttpOptions options, params Header[] headers);
    Response<TResponse> GetJson<TResponse>(Either<string, Uri> url, params Header[] headers);
    Response<TResponse> GetJson<TResponse>(Either<string, Uri> url, HttpOptions options, params Header[] headers);

    Response<HttpStatus> DeleteStatus(Either<string, Uri> url, params Header[] headers);
    Response<HttpStatus> DeleteStatus(Either<string, Uri> url, HttpOptions options, params Header[] headers);

    Task<Response<Http245>> Post245Async(string body, Either<string, Uri> url, string contentType, params Header[] headers);
    Task<Response<Http245>> Post245Async(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers);
    Task<Response<HttpBody>> PostAsync(string body, Either<string, Uri> url, string contentType, params Header[] headers);
    Task<Response<HttpBody>> PostAsync(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers);
    Task<Response<HttpStatus>> PostStatusAsync(string body, Either<string, Uri> url, string contentType, params Header[] headers);
    Task<Response<HttpStatus>> PostStatusAsync(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers);
    Task<Response<HttpStatus>> PostJsonStatusAsync<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers);
    Task<Response<HttpStatus>> PostJsonStatusAsync<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers);
    Task<Response<TResponse>> PostJsonAsync<TRequest, TResponse>(TRequest model, Either<string, Uri> url, params Header[] headers);
    Task<Response<TResponse>> PostJsonAsync<TRequest, TResponse>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers);

    Task<Response<Http245>> Put245Async(string body, Either<string, Uri> url, string contentType, params Header[] headers);
    Task<Response<Http245>> Put245Async(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers);
    Task<Response<HttpBody>> PutAsync(string body, Either<string, Uri> url, string contentType, params Header[] headers);
    Task<Response<HttpBody>> PutAsync(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers);
    Task<Response<HttpStatus>> PutStatusAsync(string body, Either<string, Uri> url, string contentType, params Header[] headers);
    Task<Response<HttpStatus>> PutStatusAsync(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers);
    Task<Response<HttpStatus>> PutJsonStatusAsync<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers);
    Task<Response<HttpStatus>> PutJsonStatusAsync<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers);
    Task<Response<TResponse>> PutJsonAsync<TRequest, TResponse>(TRequest model, Either<string, Uri> url, params Header[] headers);
    Task<Response<TResponse>> PutJsonAsync<TRequest, TResponse>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers);

    Task<Response<Http245>> Patch245Async(string body, Either<string, Uri> url, string contentType, params Header[] headers);
    Task<Response<Http245>> Patch245Async(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers);
    Task<Response<HttpBody>> PatchAsync(string body, Either<string, Uri> url, string contentType, params Header[] headers);
    Task<Response<HttpBody>> PatchAsync(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers);
    Task<Response<HttpStatus>> PatchStatusAsync(string body, Either<string, Uri> url, string contentType, params Header[] headers);
    Task<Response<HttpStatus>> PatchStatusAsync(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers);
    Task<Response<HttpStatus>> PatchJsonStatusAsync<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers);
    Task<Response<HttpStatus>> PatchJsonStatusAsync<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers);
    Task<Response<TResponse>> PatchJsonAsync<TRequest, TResponse>(TRequest model, Either<string, Uri> url, params Header[] headers);
    Task<Response<TResponse>> PatchJsonAsync<TRequest, TResponse>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers);

    Task<Response<Http245>> Get245Async(Either<string, Uri> url, params Header[] headers);
    Task<Response<Http245>> Get245Async(Either<string, Uri> url, HttpOptions options, params Header[] headers);
    Task<Response<HttpBody>> GetAsync(Either<string, Uri> url, params Header[] headers);
    Task<Response<HttpBody>> GetAsync(Either<string, Uri> url, HttpOptions options, params Header[] headers);
    Task<Response<HttpStatus>> GetStatusAsync(Either<string, Uri> url, params Header[] headers);
    Task<Response<HttpStatus>> GetStatusAsync(Either<string, Uri> url, HttpOptions options, params Header[] headers);
    Task<Response<TResponse>> GetJsonAsync<TResponse>(Either<string, Uri> url, params Header[] headers);
    Task<Response<TResponse>> GetJsonAsync<TResponse>(Either<string, Uri> url, HttpOptions options, params Header[] headers);

    Task<Response<HttpStatus>> DeleteStatusAsync(Either<string, Uri> url, params Header[] headers);
    Task<Response<HttpStatus>> DeleteStatusAsync(Either<string, Uri> url, HttpOptions options, params Header[] headers);
}

public sealed class HttpResponse : IHttpResponse
{
    internal static readonly IHttpResponse Instance = new HttpResponse();

    private static Response<HttpStatus> IdentityResponseStatus<E>(Task<Either<string, E>> t) where E : Exception => t.Result.Match(static x => Response.Create(HttpStatus.Create(x)), Error<HttpStatus>);

    private static Response<HttpBody> IdentityResponseBody<E>(Task<Either<string, E>> t) where E : Exception => t.Result.Match(static x => Response.Create(HttpBody.Create(x)), Error<HttpBody>);

    private static Response<T> IdentityResponse<T, E>(Task<Either<T, E>> t) where E : Exception => t.Result.Match(Response.Create<T>, Error<T>);

    private static Func<string, Response<T>> Parse<T>(JsonOptions options) { return json => Json.Response.ToModel<T>(json, options); }

    private static Response<T> Error<T>(Exception ex) { ex.LogError(); return new Response<T>(); }

    private static Func<string, Response<HttpStatus>> Send(string httpMethod, Either<string, Uri> url, HttpOptions options, Header[] headers)
    {
        return body => HypertextTransferProtocol
            .Send(body, url.Match(static x => new Uri(x), Identity), Constants.Http.JSON_CONTENT, new HttpOptions(options, retrieveHttpStatus: true), headers, httpMethod)
            .Match(static x => new Response<HttpStatus>(new HttpStatus(x)), Error<HttpStatus>);
    }

    private static Func<string, Response<T>> Send<T>(string httpMethod, Either<string, Uri> url, HttpOptions options, Header[] headers)
    {
        return body => HypertextTransferProtocol
            .Send(body, url.Match(static x => new Uri(x), Identity), Constants.Http.JSON_CONTENT, options, headers, httpMethod)
            .Match(Parse<T>(options), Error<T>);
    }

    public Response<Http245> Post245(string body, Either<string, Uri> url, string contentType, params Header[] headers)
    {
        return Post245(body, url, contentType, HttpOptions.Default, headers);
    }

    public Response<Http245> Post245(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .Send245(body, url.Match(static x => new Uri(x), Identity), contentType, options, headers, Constants.Http.POST)
            .Match(Response.Create, Error<Http245>);
    }

    public Response<HttpBody> Post(string body, Either<string, Uri> url, string contentType, params Header[] headers)
    {
        return Post(body, url, contentType, HttpOptions.Default, headers);
    }

    public Response<HttpBody> Post(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .Send(body, url.Match(static x => new Uri(x), Identity), contentType, options, headers, Constants.Http.POST)
            .Match(static x => Response.Create(HttpBody.Create(x)), Error<HttpBody>);
    }

    public Response<HttpStatus> PostStatus(string body, Either<string, Uri> url, string contentType, params Header[] headers)
    {
        return PostStatus(body, url, contentType, HttpOptions.Default, headers);
    }

    public Response<HttpStatus> PostStatus(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .Send(body, url.Match(static x => new Uri(x), Identity), contentType, new HttpOptions(options, retrieveHttpStatus: true), headers, Constants.Http.POST)
            .Match(static x => Response.Create(HttpStatus.Create(x)), Error<HttpStatus>);
    }

    public Response<HttpStatus> PostJsonStatus<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers)
    {
        return PostJsonStatus<TRequest>(model, url, HttpOptions.Default, headers);
    }

    public Response<HttpStatus> PostJsonStatus<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers)
    {
        return Json.Response
            .FromModel(model, options)
            .Bind(Send(Constants.Http.POST, url, options, headers));
    }

    public Response<TResponse> PostJson<TRequest, TResponse>(TRequest model, Either<string, Uri> url, params Header[] headers)
    {
        return PostJson<TRequest, TResponse>(model, url, HttpOptions.Default, headers);
    }

    public Response<TResponse> PostJson<TRequest, TResponse>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers)
    {
        return Json.Response
            .FromModel(model, options)
            .Bind(Send<TResponse>(Constants.Http.POST, url, options, headers));
    }

    public Response<Http245> Put245(string body, Either<string, Uri> url, string contentType, params Header[] headers)
    {
        return Put245(body, url, contentType, HttpOptions.Default, headers);
    }

    public Response<Http245> Put245(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .Send245(body, url.Match(static x => new Uri(x), Identity), contentType, options, headers, Constants.Http.PUT)
            .Match(Response.Create, Error<Http245>);
    }

    public Response<HttpBody> Put(string body, Either<string, Uri> url, string contentType, params Header[] headers)
    {
        return Put(body, url, contentType, HttpOptions.Default, headers);
    }

    public Response<HttpBody> Put(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .Send(body, url.Match(static x => new Uri(x), Identity), contentType, options, headers, Constants.Http.PUT)
            .Match(static x => Response.Create(HttpBody.Create(x)), Error<HttpBody>);
    }

    public Response<HttpStatus> PutStatus(string body, Either<string, Uri> url, string contentType, params Header[] headers)
    {
        return PutStatus(body, url, contentType, HttpOptions.Default, headers);
    }

    public Response<HttpStatus> PutStatus(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .Send(body, url.Match(static x => new Uri(x), Identity), contentType, new HttpOptions(options, retrieveHttpStatus: true), headers, Constants.Http.PUT)
            .Match(static x => Response.Create(HttpStatus.Create(x)), Error<HttpStatus>);
    }

    public Response<HttpStatus> PutJsonStatus<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers)
    {
        return PutJsonStatus<TRequest>(model, url, HttpOptions.Default, headers);
    }

    public Response<HttpStatus> PutJsonStatus<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers)
    {
        return Json.Response.
            FromModel(model, options)
            .Bind(Send(Constants.Http.PUT, url, options, headers));
    }

    public Response<TResponse> PutJson<TRequest, TResponse>(TRequest model, Either<string, Uri> url, params Header[] headers)
    {
        return PutJson<TRequest, TResponse>(model, url, HttpOptions.Default, headers);
    }

    public Response<TResponse> PutJson<TRequest, TResponse>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers)
    {
        return Json.Response
            .FromModel(model, options)
            .Bind(Send<TResponse>(Constants.Http.PUT, url, options, headers));
    }

    public Response<Http245> Patch245(string body, Either<string, Uri> url, string contentType, params Header[] headers)
    {
        return Patch245(body, url, contentType, HttpOptions.Default, headers);
    }

    public Response<Http245> Patch245(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .Send245(body, url.Match(static x => new Uri(x), Identity), contentType, options, headers, Constants.Http.PATCH)
            .Match(Response.Create, Error<Http245>);
    }

    public Response<HttpBody> Patch(string body, Either<string, Uri> url, string contentType, params Header[] headers)
    {
        return Patch(body, url, contentType, HttpOptions.Default, headers);
    }

    public Response<HttpBody> Patch(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .Send(body, url.Match(static x => new Uri(x), Identity), contentType, options, headers, Constants.Http.PATCH)
            .Match(static x => Response.Create(HttpBody.Create(x)), Error<HttpBody>);
    }

    public Response<HttpStatus> PatchStatus(string body, Either<string, Uri> url, string contentType, params Header[] headers)
    {
        return PatchStatus(body, url, contentType, HttpOptions.Default, headers);
    }

    public Response<HttpStatus> PatchStatus(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .Send(body, url.Match(static x => new Uri(x), Identity), contentType, new HttpOptions(options, retrieveHttpStatus: true), headers, Constants.Http.PATCH)
            .Match(static x => Response.Create(HttpStatus.Create(x)), Error<HttpStatus>);
    }

    public Response<HttpStatus> PatchJsonStatus<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers)
    {
        return PatchJsonStatus<TRequest>(model, url, HttpOptions.Default, headers);
    }

    public Response<HttpStatus> PatchJsonStatus<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers)
    {
        return Json.Response
            .FromModel(model, options)
            .Bind(Send(Constants.Http.PATCH, url, options, headers));
    }

    public Response<TResponse> PatchJson<TRequest, TResponse>(TRequest model, Either<string, Uri> url, params Header[] headers)
    {
        return PatchJson<TRequest, TResponse>(model, url, HttpOptions.Default, headers);
    }

    public Response<TResponse> PatchJson<TRequest, TResponse>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers)
    {
        return Json.Response
            .FromModel(model, options)
            .Bind(Send<TResponse>(Constants.Http.PATCH, url, options, headers));
    }

    public Response<Http245> Get245(Either<string, Uri> url, params Header[] headers)
    {
        return Get245(url, HttpOptions.Default, headers);
    }

    public Response<Http245> Get245(Either<string, Uri> url, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .Send245(string.Empty, url.Match(static x => new Uri(x), Identity), string.Empty, options, headers, Constants.Http.GET)
            .Match(Response.Create, Error<Http245>);
    }

    public Response<HttpBody> Get(Either<string, Uri> url, params Header[] headers)
    {
        return Get(url, HttpOptions.Default, headers);
    }

    public Response<HttpBody> Get(Either<string, Uri> url, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .Send(string.Empty, url.Match(static x => new Uri(x), Identity), string.Empty, options, headers, Constants.Http.GET)
            .Match(static x => Response.Create(HttpBody.Create(x)), Error<HttpBody>);
    }

    public Response<HttpStatus> GetStatus(Either<string, Uri> url, params Header[] headers)
    {
        return GetStatus(url, HttpOptions.Default, headers);
    }

    public Response<HttpStatus> GetStatus(Either<string, Uri> url, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .Send(string.Empty, url.Match(static x => new Uri(x), Identity), string.Empty, new HttpOptions(options, retrieveHttpStatus: true), headers, Constants.Http.GET)
            .Match(static x => Response.Create(HttpStatus.Create(x)), Error<HttpStatus>);
    }

    public Response<TResponse> GetJson<TResponse>(Either<string, Uri> url, params Header[] headers)
    {
        return GetJson<TResponse>(url, HttpOptions.Default, headers);
    }

    public Response<TResponse> GetJson<TResponse>(Either<string, Uri> url, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .Send(string.Empty, url.Match(static x => new Uri(x), Identity), string.Empty, options, headers, Constants.Http.GET)
            .Match(Parse<TResponse>(options), Error<TResponse>);
    }

    public Response<HttpStatus> DeleteStatus(Either<string, Uri> url, params Header[] headers)
    {
        return DeleteStatus(url, HttpOptions.Default, headers);
    }

    public Response<HttpStatus> DeleteStatus(Either<string, Uri> url, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .Send(string.Empty, url.Match(static x => new Uri(x), Identity), string.Empty, new HttpOptions(options, retrieveHttpStatus: true), headers, Constants.Http.DELETE)
            .Match(static x => Response.Create(HttpStatus.Create(x)), Error<HttpStatus>);
    }

    public Task<Response<Http245>> Post245Async(string body, Either<string, Uri> url, string contentType, params Header[] headers)
    {
        return Post245Async(body, url, contentType, HttpOptions.Default, headers);
    }

    public Task<Response<Http245>> Post245Async(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .Send245Async(body, url.Match(static x => new Uri(x), Identity), contentType, options, headers, HttpMethod.Post)
            .ContinueWith(IdentityResponse);
    }

    public Task<Response<HttpBody>> PostAsync(string body, Either<string, Uri> url, string contentType, params Header[] headers)
    {
        return PostAsync(body, url, contentType, HttpOptions.Default, headers);
    }

    public Task<Response<HttpBody>> PostAsync(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .SendAsync(body, url.Match(static x => new Uri(x), Identity), contentType, options, headers, HttpMethod.Post)
            .ContinueWith(IdentityResponseBody);
    }

    public Task<Response<HttpStatus>> PostStatusAsync(string body, Either<string, Uri> url, string contentType, params Header[] headers)
    {
        return PostStatusAsync(body, url, contentType, HttpOptions.Default, headers);
    }

    public Task<Response<HttpStatus>> PostStatusAsync(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .SendAsync(body, url.Match(static x => new Uri(x), Identity), contentType, new HttpOptions(options, retrieveHttpStatus: true), headers, HttpMethod.Post)
            .ContinueWith(IdentityResponseStatus);
    }

    public Task<Response<HttpStatus>> PostJsonStatusAsync<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers)
    {
        return PostJsonStatusAsync<TRequest>(model, url, HttpOptions.Default, headers);
    }

    public Task<Response<HttpStatus>> PostJsonStatusAsync<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .SendJsonStatusAsync(model, url.Match(static x => new Uri(x), Identity), new HttpOptions(options, retrieveHttpStatus: true), headers, HttpMethod.Post)
            .ContinueWith(IdentityResponse);
    }

    public Task<Response<TResponse>> PostJsonAsync<TRequest, TResponse>(TRequest model, Either<string, Uri> url, params Header[] headers)
    {
        return PostJsonAsync<TRequest, TResponse>(model, url, HttpOptions.Default, headers);
    }

    public Task<Response<TResponse>> PostJsonAsync<TRequest, TResponse>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .SendJsonAsync<TRequest, TResponse>(model, url.Match(static x => new Uri(x), Identity), new HttpOptions(options, retrieveHttpStatus: true), headers, HttpMethod.Post)
            .ContinueWith(IdentityResponse);
    }

    public Task<Response<Http245>> Put245Async(string body, Either<string, Uri> url, string contentType, params Header[] headers)
    {
        return Put245Async(body, url, contentType, HttpOptions.Default, headers);
    }

    public Task<Response<Http245>> Put245Async(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .Send245Async(body, url.Match(static x => new Uri(x), Identity), contentType, options, headers, HttpMethod.Put)
            .ContinueWith(IdentityResponse);
    }

    public Task<Response<HttpBody>> PutAsync(string body, Either<string, Uri> url, string contentType, params Header[] headers)
    {
        return PutAsync(body, url, contentType, HttpOptions.Default, headers);
    }

    public Task<Response<HttpBody>> PutAsync(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .SendAsync(body, url.Match(static x => new Uri(x), Identity), contentType, options, headers, HttpMethod.Put)
            .ContinueWith(IdentityResponseBody);
    }

    public Task<Response<HttpStatus>> PutStatusAsync(string body, Either<string, Uri> url, string contentType, params Header[] headers)
    {
        return PutStatusAsync(body, url, contentType, HttpOptions.Default, headers);
    }

    public Task<Response<HttpStatus>> PutStatusAsync(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .SendAsync(body, url.Match(static x => new Uri(x), Identity), contentType, new HttpOptions(options, retrieveHttpStatus: true), headers, HttpMethod.Put)
            .ContinueWith(IdentityResponseStatus);
    }

    public Task<Response<HttpStatus>> PutJsonStatusAsync<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers)
    {
        return PutJsonStatusAsync<TRequest>(model, url, HttpOptions.Default, headers);
    }

    public Task<Response<HttpStatus>> PutJsonStatusAsync<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .SendJsonStatusAsync(model, url.Match(static x => new Uri(x), Identity), new HttpOptions(options, retrieveHttpStatus: true), headers, HttpMethod.Post)
            .ContinueWith(IdentityResponse);
    }

    public Task<Response<TResponse>> PutJsonAsync<TRequest, TResponse>(TRequest model, Either<string, Uri> url, params Header[] headers)
    {
        return PutJsonAsync<TRequest, TResponse>(model, url, HttpOptions.Default, headers);
    }

    public Task<Response<TResponse>> PutJsonAsync<TRequest, TResponse>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .SendJsonAsync<TRequest, TResponse>(model, url.Match(static x => new Uri(x), Identity), new HttpOptions(options, retrieveHttpStatus: true), headers, HttpMethod.Put)
            .ContinueWith(IdentityResponse);
    }

    public Task<Response<Http245>> Patch245Async(string body, Either<string, Uri> url, string contentType, params Header[] headers)
    {
        return Patch245Async(body, url, contentType, HttpOptions.Default, headers);
    }

    public Task<Response<Http245>> Patch245Async(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .Send245Async(body, url.Match(static x => new Uri(x), Identity), contentType, options, headers, HypertextTransferProtocol.Patch)
            .ContinueWith(IdentityResponse);
    }

    public Task<Response<HttpBody>> PatchAsync(string body, Either<string, Uri> url, string contentType, params Header[] headers)
    {
        return PatchAsync(body, url, contentType, HttpOptions.Default, headers);
    }

    public Task<Response<HttpBody>> PatchAsync(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .SendAsync(body, url.Match(static x => new Uri(x), Identity), contentType, options, headers, HypertextTransferProtocol.Patch)
            .ContinueWith(IdentityResponseBody);
    }

    public Task<Response<HttpStatus>> PatchStatusAsync(string body, Either<string, Uri> url, string contentType, params Header[] headers)
    {
        return PatchStatusAsync(body, url, contentType, HttpOptions.Default, headers);
    }

    public Task<Response<HttpStatus>> PatchStatusAsync(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .SendAsync(body, url.Match(static x => new Uri(x), Identity), contentType, new HttpOptions(options, retrieveHttpStatus: true), headers, HypertextTransferProtocol.Patch)
            .ContinueWith(IdentityResponseStatus);
    }

    public Task<Response<HttpStatus>> PatchJsonStatusAsync<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers)
    {
        return PatchJsonStatusAsync<TRequest>(model, url, HttpOptions.Default, headers);
    }

    public Task<Response<HttpStatus>> PatchJsonStatusAsync<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .SendJsonStatusAsync(model, url.Match(static x => new Uri(x), Identity), new HttpOptions(options, retrieveHttpStatus: true), headers, HypertextTransferProtocol.Patch)
            .ContinueWith(IdentityResponse);
    }

    public Task<Response<TResponse>> PatchJsonAsync<TRequest, TResponse>(TRequest model, Either<string, Uri> url, params Header[] headers)
    {
        return PatchJsonAsync<TRequest, TResponse>(model, url, HttpOptions.Default, headers);
    }

    public Task<Response<TResponse>> PatchJsonAsync<TRequest, TResponse>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .SendJsonAsync<TRequest, TResponse>(model, url.Match(static x => new Uri(x), Identity), new HttpOptions(options, retrieveHttpStatus: true), headers, HttpMethod.Put)
            .ContinueWith(IdentityResponse);
    }

    public Task<Response<Http245>> Get245Async(Either<string, Uri> url, params Header[] headers)
    {
        return Get245Async(url, HttpOptions.Default, headers);
    }

    public Task<Response<Http245>> Get245Async(Either<string, Uri> url, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .Send245Async(string.Empty, url.Match(static x => new Uri(x), Identity), string.Empty, options, headers, HttpMethod.Get)
            .ContinueWith(IdentityResponse);
    }

    public Task<Response<HttpBody>> GetAsync(Either<string, Uri> url, params Header[] headers)
    {
        return GetAsync(url, HttpOptions.Default, headers);
    }

    public Task<Response<HttpBody>> GetAsync(Either<string, Uri> url, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .SendAsync(string.Empty, url.Match(static x => new Uri(x), Identity), string.Empty, options, headers, HttpMethod.Get)
            .ContinueWith(IdentityResponseBody);
    }

    public Task<Response<HttpStatus>> GetStatusAsync(Either<string, Uri> url, params Header[] headers)
    {
        return GetStatusAsync(url, HttpOptions.Default, headers);
    }

    public Task<Response<HttpStatus>> GetStatusAsync(Either<string, Uri> url, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .SendAsync(string.Empty, url.Match(static x => new Uri(x), Identity), string.Empty, new HttpOptions(options, retrieveHttpStatus: true), headers, HttpMethod.Get)
            .ContinueWith(IdentityResponseStatus);
    }

    public Task<Response<TResponse>> GetJsonAsync<TResponse>(Either<string, Uri> url, params Header[] headers)
    {
        return GetJsonAsync<TResponse>(url, HttpOptions.Default, headers);
    }

    public Task<Response<TResponse>> GetJsonAsync<TResponse>(Either<string, Uri> url, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .SendJsonAsync<TResponse>(url.Match(static x => new Uri(x), Identity), new HttpOptions(options, retrieveHttpStatus: true), headers, HttpMethod.Get)
            .ContinueWith(IdentityResponse);
    }

    public Task<Response<HttpStatus>> DeleteStatusAsync(Either<string, Uri> url, params Header[] headers)
    {
        return DeleteStatusAsync(url, HttpOptions.Default, headers);
    }

    public Task<Response<HttpStatus>> DeleteStatusAsync(Either<string, Uri> url, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .SendAsync(string.Empty, url.Match(static x => new Uri(x), Identity), string.Empty, new HttpOptions(options, retrieveHttpStatus: true), headers, HttpMethod.Delete)
            .ContinueWith(IdentityResponseStatus);
    }
}

public interface IHttpResponse<T>
{
    Response<HttpStatus> PostJsonStatus(T model, Either<string, Uri> url, params Header[] headers);
    Response<HttpStatus> PostJsonStatus(T model, Either<string, Uri> url, HttpOptions options, params Header[] headers);
    Response<T> PostJson<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers);
    Response<T> PostJson<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers);

    Response<HttpStatus> PutJsonStatus(T model, Either<string, Uri> url, params Header[] headers);
    Response<HttpStatus> PutJsonStatus(T model, Either<string, Uri> url, HttpOptions options, params Header[] headers);
    Response<T> PutJson<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers);
    Response<T> PutJson<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers);

    Response<HttpStatus> PatchJsonStatus(T model, Either<string, Uri> url, params Header[] headers);
    Response<HttpStatus> PatchJsonStatus(T model, Either<string, Uri> url, HttpOptions options, params Header[] headers);
    Response<T> PatchJson<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers);
    Response<T> PatchJson<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers);

    Response<T> GetJson(Either<string, Uri> url, params Header[] headers);
    Response<T> GetJson(Either<string, Uri> url, HttpOptions options, params Header[] headers);

    Task<Response<HttpStatus>> PostJsonStatusAsync(T model, Either<string, Uri> url, params Header[] headers);
    Task<Response<HttpStatus>> PostJsonStatusAsync(T model, Either<string, Uri> url, HttpOptions options, params Header[] headers);
    Task<Response<T>> PostJsonAsync<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers);
    Task<Response<T>> PostJsonAsync<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers);

    Task<Response<HttpStatus>> PutJsonStatusAsync(T model, Either<string, Uri> url, params Header[] headers);
    Task<Response<HttpStatus>> PutJsonStatusAsync(T model, Either<string, Uri> url, HttpOptions options, params Header[] headers);
    Task<Response<T>> PutJsonAsync<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers);
    Task<Response<T>> PutJsonAsync<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers);

    Task<Response<HttpStatus>> PatchJsonStatusAsync(T model, Either<string, Uri> url, params Header[] headers);
    Task<Response<HttpStatus>> PatchJsonStatusAsync(T model, Either<string, Uri> url, HttpOptions options, params Header[] headers);
    Task<Response<T>> PatchJsonAsync<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers);
    Task<Response<T>> PatchJsonAsync<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers);

    Task<Response<T>> GetJsonAsync(Either<string, Uri> url, params Header[] headers);
    Task<Response<T>> GetJsonAsync(Either<string, Uri> url, HttpOptions options, params Header[] headers);
}

public sealed class HttpResponse<T> : IHttpResponse<T>
{
    internal static readonly IHttpResponse<T> Instance = new HttpResponse<T>();

    public Response<HttpStatus> PostJsonStatus(T model, Either<string, Uri> url, params Header[] headers) => HttpResponse.Instance.PostJsonStatus<T>(model, url, headers);
    public Response<HttpStatus> PostJsonStatus(T model, Either<string, Uri> url, HttpOptions options, params Header[] headers) => HttpResponse.Instance.PostJsonStatus<T>(model, url, options, headers);
    public Response<T> PostJson<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers) => HttpResponse.Instance.PostJson<TRequest, T>(model, url, headers);
    public Response<T> PostJson<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers) => HttpResponse.Instance.PostJson<TRequest, T>(model, url, options, headers);

    public Response<HttpStatus> PutJsonStatus(T model, Either<string, Uri> url, params Header[] headers) => HttpResponse.Instance.PutJsonStatus<T>(model, url, headers);
    public Response<HttpStatus> PutJsonStatus(T model, Either<string, Uri> url, HttpOptions options, params Header[] headers) => HttpResponse.Instance.PutJsonStatus<T>(model, url, options, headers);
    public Response<T> PutJson<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers) => HttpResponse.Instance.PutJson<TRequest, T>(model, url, headers);
    public Response<T> PutJson<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers) => HttpResponse.Instance.PutJson<TRequest, T>(model, url, options, headers);

    public Response<HttpStatus> PatchJsonStatus(T model, Either<string, Uri> url, params Header[] headers) => HttpResponse.Instance.PatchJsonStatus<T>(model, url, headers);
    public Response<HttpStatus> PatchJsonStatus(T model, Either<string, Uri> url, HttpOptions options, params Header[] headers) => HttpResponse.Instance.PatchJsonStatus<T>(model, url, options, headers);
    public Response<T> PatchJson<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers) => HttpResponse.Instance.PatchJson<TRequest, T>(model, url, headers);
    public Response<T> PatchJson<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers) => HttpResponse.Instance.PatchJson<TRequest, T>(model, url, options, headers);

    public Response<T> GetJson(Either<string, Uri> url, params Header[] headers) => HttpResponse.Instance.GetJson<T>(url, headers);
    public Response<T> GetJson(Either<string, Uri> url, HttpOptions options, params Header[] headers) => HttpResponse.Instance.GetJson<T>(url, headers);

    public Task<Response<HttpStatus>> PostJsonStatusAsync(T model, Either<string, Uri> url, params Header[] headers) => HttpResponse.Instance.PostJsonStatusAsync<T>(model, url, headers);
    public Task<Response<HttpStatus>> PostJsonStatusAsync(T model, Either<string, Uri> url, HttpOptions options, params Header[] headers) => HttpResponse.Instance.PostJsonStatusAsync<T>(model, url, options, headers);
    public Task<Response<T>> PostJsonAsync<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers) => HttpResponse.Instance.PostJsonAsync<TRequest, T>(model, url, headers);
    public Task<Response<T>> PostJsonAsync<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers) => HttpResponse.Instance.PostJsonAsync<TRequest, T>(model, url, options, headers);

    public Task<Response<HttpStatus>> PutJsonStatusAsync(T model, Either<string, Uri> url, params Header[] headers) => HttpResponse.Instance.PutJsonStatusAsync<T>(model, url, headers);
    public Task<Response<HttpStatus>> PutJsonStatusAsync(T model, Either<string, Uri> url, HttpOptions options, params Header[] headers) => HttpResponse.Instance.PutJsonStatusAsync<T>(model, url, options, headers);
    public Task<Response<T>> PutJsonAsync<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers) => HttpResponse.Instance.PutJsonAsync<TRequest, T>(model, url, headers);
    public Task<Response<T>> PutJsonAsync<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers) => HttpResponse.Instance.PutJsonAsync<TRequest, T>(model, url, options, headers);

    public Task<Response<HttpStatus>> PatchJsonStatusAsync(T model, Either<string, Uri> url, params Header[] headers) => HttpResponse.Instance.PatchJsonStatusAsync<T>(model, url, headers);
    public Task<Response<HttpStatus>> PatchJsonStatusAsync(T model, Either<string, Uri> url, HttpOptions options, params Header[] headers) => HttpResponse.Instance.PatchJsonStatusAsync<T>(model, url, options, headers);
    public Task<Response<T>> PatchJsonAsync<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers) => HttpResponse.Instance.PatchJsonAsync<TRequest, T>(model, url, headers);
    public Task<Response<T>> PatchJsonAsync<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers) => HttpResponse.Instance.PatchJsonAsync<TRequest, T>(model, url, options, headers);

    public Task<Response<T>> GetJsonAsync(Either<string, Uri> url, params Header[] headers) => HttpResponse.Instance.GetJsonAsync<T>(url, headers);
    public Task<Response<T>> GetJsonAsync(Either<string, Uri> url, HttpOptions options, params Header[] headers) => HttpResponse.Instance.GetJsonAsync<T>(url, options, headers);
}

public interface IHttpResponse<TRequest, TResponse>
{
    Response<TResponse> PostJson(TRequest model, Either<string, Uri> url, params Header[] headers);
    Response<TResponse> PostJson(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers);

    Response<TResponse> PutJson(TRequest model, Either<string, Uri> url, params Header[] headers);
    Response<TResponse> PutJson(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers);

    Response<TResponse> PatchJson(TRequest model, Either<string, Uri> url, params Header[] headers);
    Response<TResponse> PatchJson(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers);

    Task<Response<TResponse>> PostJsonAsync(TRequest model, Either<string, Uri> url, params Header[] headers);
    Task<Response<TResponse>> PostJsonAsync(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers);

    Task<Response<TResponse>> PutJsonAsync(TRequest model, Either<string, Uri> url, params Header[] headers);
    Task<Response<TResponse>> PutJsonAsync(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers);

    Task<Response<TResponse>> PatchJsonAsync(TRequest model, Either<string, Uri> url, params Header[] headers);
    Task<Response<TResponse>> PatchJsonAsync(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers);
}

public sealed class HttpResponse<TRequest, TResponse> : IHttpResponse<TRequest, TResponse>
{
    internal static readonly IHttpResponse<TRequest, TResponse> Instance = new HttpResponse<TRequest, TResponse>();

    public Response<TResponse> PostJson(TRequest model, Either<string, Uri> url, params Header[] headers) => HttpResponse.Instance.PostJson<TRequest, TResponse>(model, url, headers);
    public Response<TResponse> PostJson(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers) => HttpResponse.Instance.PostJson<TRequest, TResponse>(model, url, options, headers);

    public Response<TResponse> PutJson(TRequest model, Either<string, Uri> url, params Header[] headers) => HttpResponse.Instance.PutJson<TRequest, TResponse>(model, url, headers);
    public Response<TResponse> PutJson(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers) => HttpResponse.Instance.PutJson<TRequest, TResponse>(model, url, options, headers);

    public Response<TResponse> PatchJson(TRequest model, Either<string, Uri> url, params Header[] headers) => HttpResponse.Instance.PatchJson<TRequest, TResponse>(model, url, headers);
    public Response<TResponse> PatchJson(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers) => HttpResponse.Instance.PatchJson<TRequest, TResponse>(model, url, options, headers);

    public Task<Response<TResponse>> PostJsonAsync(TRequest model, Either<string, Uri> url, params Header[] headers) => HttpResponse.Instance.PostJsonAsync<TRequest, TResponse>(model, url, headers);
    public Task<Response<TResponse>> PostJsonAsync(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers) => HttpResponse.Instance.PostJsonAsync<TRequest, TResponse>(model, url, options, headers);

    public Task<Response<TResponse>> PutJsonAsync(TRequest model, Either<string, Uri> url, params Header[] headers) => HttpResponse.Instance.PutJsonAsync<TRequest, TResponse>(model, url, headers);
    public Task<Response<TResponse>> PutJsonAsync(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers) => HttpResponse.Instance.PutJsonAsync<TRequest, TResponse>(model, url, options, headers);

    public Task<Response<TResponse>> PatchJsonAsync(TRequest model, Either<string, Uri> url, params Header[] headers) => HttpResponse.Instance.PatchJsonAsync<TRequest, TResponse>(model, url, headers);
    public Task<Response<TResponse>> PatchJsonAsync(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers) => HttpResponse.Instance.PatchJsonAsync<TRequest, TResponse>(model, url, options, headers);
}
