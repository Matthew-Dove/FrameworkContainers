using ContainerExpressions.Containers;
using FrameworkContainers.Format.JsonCollective;
using FrameworkContainers.Format.JsonCollective.Models;
using FrameworkContainers.Models;
using FrameworkContainers.Network.HttpCollective.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace FrameworkContainers.Network.HttpCollective;

public interface IHttpMaybe
{
    Maybe<Http245> Post245(string body, Either<string, Uri> url, string contentType, params Header[] headers);
    Maybe<Http245> Post245(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers);
    Maybe<HttpBody> Post(string body, Either<string, Uri> url, string contentType, params Header[] headers);
    Maybe<HttpBody> Post(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers);
    Maybe<HttpStatus> PostStatus(string body, Either<string, Uri> url, string contentType, params Header[] headers);
    Maybe<HttpStatus> PostStatus(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers);
    Maybe<HttpStatus> PostJsonStatus<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers);
    Maybe<HttpStatus> PostJsonStatus<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers);
    Maybe<TResponse> PostJson<TRequest, TResponse>(TRequest model, Either<string, Uri> url, params Header[] headers);
    Maybe<TResponse> PostJson<TRequest, TResponse>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers);

    Maybe<Http245> Put245(string body, Either<string, Uri> url, string contentType, params Header[] headers);
    Maybe<Http245> Put245(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers);
    Maybe<HttpBody> Put(string body, Either<string, Uri> url, string contentType, params Header[] headers);
    Maybe<HttpBody> Put(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers);
    Maybe<HttpStatus> PutStatus(string body, Either<string, Uri> url, string contentType, params Header[] headers);
    Maybe<HttpStatus> PutStatus(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers);
    Maybe<HttpStatus> PutJsonStatus<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers);
    Maybe<HttpStatus> PutJsonStatus<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers);
    Maybe<TResponse> PutJson<TRequest, TResponse>(TRequest model, Either<string, Uri> url, params Header[] headers);
    Maybe<TResponse> PutJson<TRequest, TResponse>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers);

    Maybe<Http245> Patch245(string body, Either<string, Uri> url, string contentType, params Header[] headers);
    Maybe<Http245> Patch245(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers);
    Maybe<HttpBody> Patch(string body, Either<string, Uri> url, string contentType, params Header[] headers);
    Maybe<HttpBody> Patch(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers);
    Maybe<HttpStatus> PatchStatus(string body, Either<string, Uri> url, string contentType, params Header[] headers);
    Maybe<HttpStatus> PatchStatus(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers);
    Maybe<HttpStatus> PatchJsonStatus<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers);
    Maybe<HttpStatus> PatchJsonStatus<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers);
    Maybe<TResponse> PatchJson<TRequest, TResponse>(TRequest model, Either<string, Uri> url, params Header[] headers);
    Maybe<TResponse> PatchJson<TRequest, TResponse>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers);

    Maybe<Http245> Get245(Either<string, Uri> url, params Header[] headers);
    Maybe<Http245> Get245(Either<string, Uri> url, HttpOptions options, params Header[] headers);
    Maybe<HttpBody> Get(Either<string, Uri> url, params Header[] headers);
    Maybe<HttpBody> Get(Either<string, Uri> url, HttpOptions options, params Header[] headers);
    Maybe<HttpStatus> GetStatus(Either<string, Uri> url, params Header[] headers);
    Maybe<HttpStatus> GetStatus(Either<string, Uri> url, HttpOptions options, params Header[] headers);
    Maybe<TResponse> GetJson<TResponse>(Either<string, Uri> url, params Header[] headers);
    Maybe<TResponse> GetJson<TResponse>(Either<string, Uri> url, HttpOptions options, params Header[] headers);

    Maybe<HttpStatus> DeleteStatus(Either<string, Uri> url, params Header[] headers);
    Maybe<HttpStatus> DeleteStatus(Either<string, Uri> url, HttpOptions options, params Header[] headers);

    Task<Maybe<Http245>> Post245Async(string body, Either<string, Uri> url, string contentType, params Header[] headers);
    Task<Maybe<Http245>> Post245Async(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers);
    Task<Maybe<HttpBody>> PostAsync(string body, Either<string, Uri> url, string contentType, params Header[] headers);
    Task<Maybe<HttpBody>> PostAsync(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers);
    Task<Maybe<HttpStatus>> PostStatusAsync(string body, Either<string, Uri> url, string contentType, params Header[] headers);
    Task<Maybe<HttpStatus>> PostStatusAsync(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers);
    Task<Maybe<HttpStatus>> PostJsonStatusAsync<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers);
    Task<Maybe<HttpStatus>> PostJsonStatusAsync<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers);
    Task<Maybe<TResponse>> PostJsonAsync<TRequest, TResponse>(TRequest model, Either<string, Uri> url, params Header[] headers);
    Task<Maybe<TResponse>> PostJsonAsync<TRequest, TResponse>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers);

    Task<Maybe<Http245>> Put245Async(string body, Either<string, Uri> url, string contentType, params Header[] headers);
    Task<Maybe<Http245>> Put245Async(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers);
    Task<Maybe<HttpBody>> PutAsync(string body, Either<string, Uri> url, string contentType, params Header[] headers);
    Task<Maybe<HttpBody>> PutAsync(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers);
    Task<Maybe<HttpStatus>> PutStatusAsync(string body, Either<string, Uri> url, string contentType, params Header[] headers);
    Task<Maybe<HttpStatus>> PutStatusAsync(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers);
    Task<Maybe<HttpStatus>> PutJsonStatusAsync<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers);
    Task<Maybe<HttpStatus>> PutJsonStatusAsync<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers);
    Task<Maybe<TResponse>> PutJsonAsync<TRequest, TResponse>(TRequest model, Either<string, Uri> url, params Header[] headers);
    Task<Maybe<TResponse>> PutJsonAsync<TRequest, TResponse>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers);

    Task<Maybe<Http245>> Patch245Async(string body, Either<string, Uri> url, string contentType, params Header[] headers);
    Task<Maybe<Http245>> Patch245Async(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers);
    Task<Maybe<HttpBody>> PatchAsync(string body, Either<string, Uri> url, string contentType, params Header[] headers);
    Task<Maybe<HttpBody>> PatchAsync(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers);
    Task<Maybe<HttpStatus>> PatchStatusAsync(string body, Either<string, Uri> url, string contentType, params Header[] headers);
    Task<Maybe<HttpStatus>> PatchStatusAsync(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers);
    Task<Maybe<HttpStatus>> PatchJsonStatusAsync<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers);
    Task<Maybe<HttpStatus>> PatchJsonStatusAsync<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers);
    Task<Maybe<TResponse>> PatchJsonAsync<TRequest, TResponse>(TRequest model, Either<string, Uri> url, params Header[] headers);
    Task<Maybe<TResponse>> PatchJsonAsync<TRequest, TResponse>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers);

    Task<Maybe<Http245>> Get245Async(Either<string, Uri> url, params Header[] headers);
    Task<Maybe<Http245>> Get245Async(Either<string, Uri> url, HttpOptions options, params Header[] headers);
    Task<Maybe<HttpBody>> GetAsync(Either<string, Uri> url, params Header[] headers);
    Task<Maybe<HttpBody>> GetAsync(Either<string, Uri> url, HttpOptions options, params Header[] headers);
    Task<Maybe<HttpStatus>> GetStatusAsync(Either<string, Uri> url, params Header[] headers);
    Task<Maybe<HttpStatus>> GetStatusAsync(Either<string, Uri> url, HttpOptions options, params Header[] headers);
    Task<Maybe<TResponse>> GetJsonAsync<TResponse>(Either<string, Uri> url, params Header[] headers);
    Task<Maybe<TResponse>> GetJsonAsync<TResponse>(Either<string, Uri> url, HttpOptions options, params Header[] headers);

    Task<Maybe<HttpStatus>> DeleteStatusAsync(Either<string, Uri> url, params Header[] headers);
    Task<Maybe<HttpStatus>> DeleteStatusAsync(Either<string, Uri> url, HttpOptions options, params Header[] headers);
}

public sealed class HttpMaybe : IHttpMaybe
{
    internal static readonly IHttpMaybe Instance = new HttpMaybe();

    private static Maybe<HttpStatus> IdentityMaybeStatus<E>(Task<Either<string, E>> t) where E : Exception => t.Result.Match(static x => Maybe.Create(HttpStatus.Create(x)), Maybe.CreateError<HttpStatus>);

    private static Maybe<HttpBody> IdentityMaybeBody<E>(Task<Either<string, E>> t) where E : Exception => t.Result.Match(static x => Maybe.Create(HttpBody.Create(x)), Maybe.CreateError<HttpBody>);

    private static Maybe<T> IdentityMaybe<T, E>(Task<Either<T, E>> t) where E : Exception => t.Result.Match(Maybe.Create<T>, Maybe.CreateError<T>);

    private static Func<string, Maybe<T>> Parse<T>(JsonOptions options) { return json => Json.Maybe.ToModel<T>(json, options).Match(Maybe.Create<T>, Maybe.CreateError<T>); }

    private static Func<string, Maybe<HttpStatus>> Send(string httpMethod, Either<string, Uri> url, HttpOptions options, Header[] headers)
    {
        return body => HypertextTransferProtocol
            .Send(body, url.Match(static x => new Uri(x), Identity), Constants.Http.JSON_CONTENT, new HttpOptions(options, retrieveHttpStatus: true), headers, httpMethod)
            .Match(static x => new Maybe<HttpStatus>(new HttpStatus(x)), Maybe.CreateError<HttpStatus>);
    }

    private static Func<string, Maybe<T>> Send<T>(string httpMethod, Either<string, Uri> url, HttpOptions options, Header[] headers)
    {
        return body => HypertextTransferProtocol
            .Send(body, url.Match(static x => new Uri(x), Identity), Constants.Http.JSON_CONTENT, options, headers, httpMethod)
            .Match(Parse<T>(options), Maybe.CreateError<T>);
    }

    public Maybe<Http245> Post245(string body, Either<string, Uri> url, string contentType, params Header[] headers)
    {
        return Post245(body, url, contentType, HttpOptions.Default, headers);
    }

    public Maybe<Http245> Post245(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .Send245(body, url.Match(static x => new Uri(x), Identity), contentType, options, headers, Constants.Http.POST)
            .Match(Maybe.Create, Maybe.CreateError<Http245>);
    }

    public Maybe<HttpBody> Post(string body, Either<string, Uri> url, string contentType, params Header[] headers)
    {
        return Post(body, url, contentType, HttpOptions.Default, headers);
    }

    public Maybe<HttpBody> Post(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .Send(body, url.Match(static x => new Uri(x), Identity), contentType, options, headers, Constants.Http.POST)
            .Match(static x => Maybe.Create(HttpBody.Create(x)), Maybe.CreateError<HttpBody>);
    }

    public Maybe<HttpStatus> PostStatus(string body, Either<string, Uri> url, string contentType, params Header[] headers)
    {
        return PostStatus(body, url, contentType, HttpOptions.Default, headers);
    }

    public Maybe<HttpStatus> PostStatus(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .Send(body, url.Match(static x => new Uri(x), Identity), contentType, new HttpOptions(options, retrieveHttpStatus: true), headers, Constants.Http.POST)
            .Match(static x => Maybe.Create(HttpStatus.Create(x)), Maybe.CreateError<HttpStatus>);
    }

    public Maybe<HttpStatus> PostJsonStatus<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers)
    {
        return PostJsonStatus<TRequest>(model, url, HttpOptions.Default, headers);
    }

    public Maybe<HttpStatus> PostJsonStatus<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers)
    {
        return Json.Maybe.FromModel(model, options).Match(
            Send(Constants.Http.POST, url, options, headers),
            Maybe.CreateError<HttpStatus>
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
            Maybe.CreateError<TResponse>
        );
    }

    public Maybe<Http245> Put245(string body, Either<string, Uri> url, string contentType, params Header[] headers)
    {
        return Put245(body, url, contentType, HttpOptions.Default, headers);
    }

    public Maybe<Http245> Put245(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .Send245(body, url.Match(static x => new Uri(x), Identity), contentType, options, headers, Constants.Http.PUT)
            .Match(Maybe.Create, Maybe.CreateError<Http245>);
    }

    public Maybe<HttpBody> Put(string body, Either<string, Uri> url, string contentType, params Header[] headers)
    {
        return Put(body, url, contentType, HttpOptions.Default, headers);
    }

    public Maybe<HttpBody> Put(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .Send(body, url.Match(static x => new Uri(x), Identity), contentType, options, headers, Constants.Http.PUT)
            .Match(static x => Maybe.Create(HttpBody.Create(x)), Maybe.CreateError<HttpBody>);
    }

    public Maybe<HttpStatus> PutStatus(string body, Either<string, Uri> url, string contentType, params Header[] headers)
    {
        return PutStatus(body, url, contentType, HttpOptions.Default, headers);
    }

    public Maybe<HttpStatus> PutStatus(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .Send(body, url.Match(static x => new Uri(x), Identity), contentType, new HttpOptions(options, retrieveHttpStatus: true), headers, Constants.Http.PUT)
            .Match(static x => Maybe.Create(HttpStatus.Create(x)), Maybe.CreateError<HttpStatus>);
    }

    public Maybe<HttpStatus> PutJsonStatus<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers)
    {
        return PutJsonStatus<TRequest>(model, url, HttpOptions.Default, headers);
    }

    public Maybe<HttpStatus> PutJsonStatus<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers)
    {
        return Json.Maybe.FromModel(model, options).Match(
            Send(Constants.Http.PUT, url, options, headers),
            Maybe.CreateError<HttpStatus>
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
            Maybe.CreateError<TResponse>
        );
    }

    public Maybe<Http245> Patch245(string body, Either<string, Uri> url, string contentType, params Header[] headers)
    {
        return Patch245(body, url, contentType, HttpOptions.Default, headers);
    }

    public Maybe<Http245> Patch245(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .Send245(body, url.Match(static x => new Uri(x), Identity), contentType, options, headers, Constants.Http.PATCH)
            .Match(Maybe.Create, Maybe.CreateError<Http245>);
    }

    public Maybe<HttpBody> Patch(string body, Either<string, Uri> url, string contentType, params Header[] headers)
    {
        return Patch(body, url, contentType, HttpOptions.Default, headers);
    }

    public Maybe<HttpBody> Patch(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .Send(body, url.Match(static x => new Uri(x), Identity), contentType, options, headers, Constants.Http.PATCH)
            .Match(static x => Maybe.Create(HttpBody.Create(x)), Maybe.CreateError<HttpBody>);
    }

    public Maybe<HttpStatus> PatchStatus(string body, Either<string, Uri> url, string contentType, params Header[] headers)
    {
        return PatchStatus(body, url, contentType, HttpOptions.Default, headers);
    }

    public Maybe<HttpStatus> PatchStatus(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .Send(body, url.Match(static x => new Uri(x), Identity), contentType, new HttpOptions(options, retrieveHttpStatus: true), headers, Constants.Http.PATCH)
            .Match(static x => Maybe.Create(HttpStatus.Create(x)), Maybe.CreateError<HttpStatus>);
    }

    public Maybe<HttpStatus> PatchJsonStatus<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers)
    {
        return PatchJsonStatus<TRequest>(model, url, HttpOptions.Default, headers);
    }

    public Maybe<HttpStatus> PatchJsonStatus<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers)
    {
        return Json.Maybe.FromModel(model, options).Match(
            Send(Constants.Http.PATCH, url, options, headers),
            Maybe.CreateError<HttpStatus>
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
            Maybe.CreateError<TResponse>
        );
    }

    public Maybe<Http245> Get245(Either<string, Uri> url, params Header[] headers)
    {
        return Get245(url, HttpOptions.Default, headers);
    }

    public Maybe<Http245> Get245(Either<string, Uri> url, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .Send245(string.Empty, url.Match(static x => new Uri(x), Identity), string.Empty, options, headers, Constants.Http.GET)
            .Match(Maybe.Create, Maybe.CreateError<Http245>);
    }

    public Maybe<HttpBody> Get(Either<string, Uri> url, params Header[] headers)
    {
        return Get(url, HttpOptions.Default, headers);
    }

    public Maybe<HttpBody> Get(Either<string, Uri> url, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .Send(string.Empty, url.Match(static x => new Uri(x), Identity), string.Empty, options, headers, Constants.Http.GET)
            .Match(static x => Maybe.Create(HttpBody.Create(x)), Maybe.CreateError<HttpBody>);
    }

    public Maybe<HttpStatus> GetStatus(Either<string, Uri> url, params Header[] headers)
    {
        return GetStatus(url, HttpOptions.Default, headers);
    }

    public Maybe<HttpStatus> GetStatus(Either<string, Uri> url, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .Send(string.Empty, url.Match(static x => new Uri(x), Identity), string.Empty, new HttpOptions(options, retrieveHttpStatus: true), headers, Constants.Http.GET)
            .Match(static x => Maybe.Create(HttpStatus.Create(x)), Maybe.CreateError<HttpStatus>);
    }

    public Maybe<TResponse> GetJson<TResponse>(Either<string, Uri> url, params Header[] headers)
    {
        return GetJson<TResponse>(url, HttpOptions.Default, headers);
    }

    public Maybe<TResponse> GetJson<TResponse>(Either<string, Uri> url, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .Send(string.Empty, url.Match(static x => new Uri(x), Identity), string.Empty, options, headers, Constants.Http.GET)
            .Match(Parse<TResponse>(options), Maybe.CreateError<TResponse>);
    }

    public Maybe<HttpStatus> DeleteStatus(Either<string, Uri> url, params Header[] headers)
    {
        return DeleteStatus(url, HttpOptions.Default, headers);
    }

    public Maybe<HttpStatus> DeleteStatus(Either<string, Uri> url, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .Send(string.Empty, url.Match(static x => new Uri(x), Identity), string.Empty, new HttpOptions(options, retrieveHttpStatus: true), headers, Constants.Http.DELETE)
            .Match(static x => Maybe.Create(HttpStatus.Create(x)), Maybe.CreateError<HttpStatus>);
    }

    public Task<Maybe<Http245>> Post245Async(string body, Either<string, Uri> url, string contentType, params Header[] headers)
    {
        return Post245Async(body, url, contentType, HttpOptions.Default, headers);
    }

    public Task<Maybe<Http245>> Post245Async(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .Send245Async(body, url.Match(static x => new Uri(x), Identity), contentType, options, headers, HttpMethod.Post)
            .ContinueWith(IdentityMaybe);
    }

    public Task<Maybe<HttpBody>> PostAsync(string body, Either<string, Uri> url, string contentType, params Header[] headers)
    {
        return PostAsync(body, url, contentType, HttpOptions.Default, headers);
    }

    public Task<Maybe<HttpBody>> PostAsync(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .SendAsync(body, url.Match(static x => new Uri(x), Identity), contentType, options, headers, HttpMethod.Post)
            .ContinueWith(IdentityMaybeBody);
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

    public Task<Maybe<Http245>> Put245Async(string body, Either<string, Uri> url, string contentType, params Header[] headers)
    {
        return Put245Async(body, url, contentType, HttpOptions.Default, headers);
    }

    public Task<Maybe<Http245>> Put245Async(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .Send245Async(body, url.Match(static x => new Uri(x), Identity), contentType, options, headers, HttpMethod.Put)
            .ContinueWith(IdentityMaybe);
    }

    public Task<Maybe<HttpBody>> PutAsync(string body, Either<string, Uri> url, string contentType, params Header[] headers)
    {
        return PutAsync(body, url, contentType, HttpOptions.Default, headers);
    }

    public Task<Maybe<HttpBody>> PutAsync(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .SendAsync(body, url.Match(static x => new Uri(x), Identity), contentType, options, headers, HttpMethod.Put)
            .ContinueWith(IdentityMaybeBody);
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

    public Task<Maybe<Http245>> Patch245Async(string body, Either<string, Uri> url, string contentType, params Header[] headers)
    {
        return Patch245Async(body, url, contentType, HttpOptions.Default, headers);
    }

    public Task<Maybe<Http245>> Patch245Async(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .Send245Async(body, url.Match(static x => new Uri(x), Identity), contentType, options, headers, HypertextTransferProtocol.Patch)
            .ContinueWith(IdentityMaybe);
    }

    public Task<Maybe<HttpBody>> PatchAsync(string body, Either<string, Uri> url, string contentType, params Header[] headers)
    {
        return PatchAsync(body, url, contentType, HttpOptions.Default, headers);
    }

    public Task<Maybe<HttpBody>> PatchAsync(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .SendAsync(body, url.Match(static x => new Uri(x), Identity), contentType, options, headers, HypertextTransferProtocol.Patch)
            .ContinueWith(IdentityMaybeBody);
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

    public Task<Maybe<Http245>> Get245Async(Either<string, Uri> url, params Header[] headers)
    {
        return Get245Async(url, HttpOptions.Default, headers);
    }

    public Task<Maybe<Http245>> Get245Async(Either<string, Uri> url, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .Send245Async(string.Empty, url.Match(static x => new Uri(x), Identity), string.Empty, options, headers, HttpMethod.Get)
            .ContinueWith(IdentityMaybe);
    }

    public Task<Maybe<HttpBody>> GetAsync(Either<string, Uri> url, params Header[] headers)
    {
        return GetAsync(url, HttpOptions.Default, headers);
    }

    public Task<Maybe<HttpBody>> GetAsync(Either<string, Uri> url, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .SendAsync(string.Empty, url.Match(static x => new Uri(x), Identity), string.Empty, options, headers, HttpMethod.Get)
            .ContinueWith(IdentityMaybeBody);
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

public interface IHttpMaybe<T>
{
    Maybe<HttpStatus> PostJsonStatus(T model, Either<string, Uri> url, params Header[] headers);
    Maybe<HttpStatus> PostJsonStatus(T model, Either<string, Uri> url, HttpOptions options, params Header[] headers);
    Maybe<T> PostJson<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers);
    Maybe<T> PostJson<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers);

    Maybe<HttpStatus> PutJsonStatus(T model, Either<string, Uri> url, params Header[] headers);
    Maybe<HttpStatus> PutJsonStatus(T model, Either<string, Uri> url, HttpOptions options, params Header[] headers);
    Maybe<T> PutJson<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers);
    Maybe<T> PutJson<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers);

    Maybe<HttpStatus> PatchJsonStatus(T model, Either<string, Uri> url, params Header[] headers);
    Maybe<HttpStatus> PatchJsonStatus(T model, Either<string, Uri> url, HttpOptions options, params Header[] headers);
    Maybe<T> PatchJson<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers);
    Maybe<T> PatchJson<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers);

    Maybe<T> GetJson(Either<string, Uri> url, params Header[] headers);
    Maybe<T> GetJson(Either<string, Uri> url, HttpOptions options, params Header[] headers);

    Task<Maybe<HttpStatus>> PostJsonStatusAsync(T model, Either<string, Uri> url, params Header[] headers);
    Task<Maybe<HttpStatus>> PostJsonStatusAsync(T model, Either<string, Uri> url, HttpOptions options, params Header[] headers);
    Task<Maybe<T>> PostJsonAsync<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers);
    Task<Maybe<T>> PostJsonAsync<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers);

    Task<Maybe<HttpStatus>> PutJsonStatusAsync(T model, Either<string, Uri> url, params Header[] headers);
    Task<Maybe<HttpStatus>> PutJsonStatusAsync(T model, Either<string, Uri> url, HttpOptions options, params Header[] headers);
    Task<Maybe<T>> PutJsonAsync<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers);
    Task<Maybe<T>> PutJsonAsync<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers);

    Task<Maybe<HttpStatus>> PatchJsonStatusAsync(T model, Either<string, Uri> url, params Header[] headers);
    Task<Maybe<HttpStatus>> PatchJsonStatusAsync(T model, Either<string, Uri> url, HttpOptions options, params Header[] headers);
    Task<Maybe<T>> PatchJsonAsync<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers);
    Task<Maybe<T>> PatchJsonAsync<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers);

    Task<Maybe<T>> GetJsonAsync(Either<string, Uri> url, params Header[] headers);
    Task<Maybe<T>> GetJsonAsync(Either<string, Uri> url, HttpOptions options, params Header[] headers);
}

public sealed class HttpMaybe<T> : IHttpMaybe<T>
{
    internal static readonly IHttpMaybe<T> Instance = new HttpMaybe<T>();

    public Maybe<HttpStatus> PostJsonStatus(T model, Either<string, Uri> url, params Header[] headers) => HttpMaybe.Instance.PostJsonStatus<T>(model, url, headers);
    public Maybe<HttpStatus> PostJsonStatus(T model, Either<string, Uri> url, HttpOptions options, params Header[] headers) => HttpMaybe.Instance.PostJsonStatus<T>(model, url, options, headers);
    public Maybe<T> PostJson<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers) => HttpMaybe.Instance.PostJson<TRequest, T>(model, url, headers);
    public Maybe<T> PostJson<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers) => HttpMaybe.Instance.PostJson<TRequest, T>(model, url, options, headers);

    public Maybe<HttpStatus> PutJsonStatus(T model, Either<string, Uri> url, params Header[] headers) => HttpMaybe.Instance.PutJsonStatus<T>(model, url, headers);
    public Maybe<HttpStatus> PutJsonStatus(T model, Either<string, Uri> url, HttpOptions options, params Header[] headers) => HttpMaybe.Instance.PutJsonStatus<T>(model, url, options, headers);
    public Maybe<T> PutJson<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers) => HttpMaybe.Instance.PutJson<TRequest, T>(model, url, headers);
    public Maybe<T> PutJson<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers) => HttpMaybe.Instance.PutJson<TRequest, T>(model, url, options, headers);

    public Maybe<HttpStatus> PatchJsonStatus(T model, Either<string, Uri> url, params Header[] headers) => HttpMaybe.Instance.PatchJsonStatus<T>(model, url, headers);
    public Maybe<HttpStatus> PatchJsonStatus(T model, Either<string, Uri> url, HttpOptions options, params Header[] headers) => HttpMaybe.Instance.PatchJsonStatus<T>(model, url, options, headers);
    public Maybe<T> PatchJson<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers) => HttpMaybe.Instance.PatchJson<TRequest, T>(model, url, headers);
    public Maybe<T> PatchJson<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers) => HttpMaybe.Instance.PatchJson<TRequest, T>(model, url, options, headers);

    public Maybe<T> GetJson(Either<string, Uri> url, params Header[] headers) => HttpMaybe.Instance.GetJson<T>(url, headers);
    public Maybe<T> GetJson(Either<string, Uri> url, HttpOptions options, params Header[] headers) => HttpMaybe.Instance.GetJson<T>(url, headers);

    public Task<Maybe<HttpStatus>> PostJsonStatusAsync(T model, Either<string, Uri> url, params Header[] headers) => HttpMaybe.Instance.PostJsonStatusAsync<T>(model, url, headers);
    public Task<Maybe<HttpStatus>> PostJsonStatusAsync(T model, Either<string, Uri> url, HttpOptions options, params Header[] headers) => HttpMaybe.Instance.PostJsonStatusAsync<T>(model, url, options, headers);
    public Task<Maybe<T>> PostJsonAsync<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers) => HttpMaybe.Instance.PostJsonAsync<TRequest, T>(model, url, headers);
    public Task<Maybe<T>> PostJsonAsync<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers) => HttpMaybe.Instance.PostJsonAsync<TRequest, T>(model, url, options, headers);

    public Task<Maybe<HttpStatus>> PutJsonStatusAsync(T model, Either<string, Uri> url, params Header[] headers) => HttpMaybe.Instance.PutJsonStatusAsync<T>(model, url, headers);
    public Task<Maybe<HttpStatus>> PutJsonStatusAsync(T model, Either<string, Uri> url, HttpOptions options, params Header[] headers) => HttpMaybe.Instance.PutJsonStatusAsync<T>(model, url, options, headers);
    public Task<Maybe<T>> PutJsonAsync<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers) => HttpMaybe.Instance.PutJsonAsync<TRequest, T>(model, url, headers);
    public Task<Maybe<T>> PutJsonAsync<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers) => HttpMaybe.Instance.PutJsonAsync<TRequest, T>(model, url, options, headers);

    public Task<Maybe<HttpStatus>> PatchJsonStatusAsync(T model, Either<string, Uri> url, params Header[] headers) => HttpMaybe.Instance.PatchJsonStatusAsync<T>(model, url, headers);
    public Task<Maybe<HttpStatus>> PatchJsonStatusAsync(T model, Either<string, Uri> url, HttpOptions options, params Header[] headers) => HttpMaybe.Instance.PatchJsonStatusAsync<T>(model, url, options, headers);
    public Task<Maybe<T>> PatchJsonAsync<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers) => HttpMaybe.Instance.PatchJsonAsync<TRequest, T>(model, url, headers);
    public Task<Maybe<T>> PatchJsonAsync<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers) => HttpMaybe.Instance.PatchJsonAsync<TRequest, T>(model, url, options, headers);

    public Task<Maybe<T>> GetJsonAsync(Either<string, Uri> url, params Header[] headers) => HttpMaybe.Instance.GetJsonAsync<T>(url, headers);
    public Task<Maybe<T>> GetJsonAsync(Either<string, Uri> url, HttpOptions options, params Header[] headers) => HttpMaybe.Instance.GetJsonAsync<T>(url, options, headers);
}

public interface IHttpMaybe<TRequest, TResponse>
{
    Maybe<TResponse> PostJson(TRequest model, Either<string, Uri> url, params Header[] headers);
    Maybe<TResponse> PostJson(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers);

    Maybe<TResponse> PutJson(TRequest model, Either<string, Uri> url, params Header[] headers);
    Maybe<TResponse> PutJson(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers);

    Maybe<TResponse> PatchJson(TRequest model, Either<string, Uri> url, params Header[] headers);
    Maybe<TResponse> PatchJson(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers);

    Task<Maybe<TResponse>> PostJsonAsync(TRequest model, Either<string, Uri> url, params Header[] headers);
    Task<Maybe<TResponse>> PostJsonAsync(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers);

    Task<Maybe<TResponse>> PutJsonAsync(TRequest model, Either<string, Uri> url, params Header[] headers);
    Task<Maybe<TResponse>> PutJsonAsync(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers);

    Task<Maybe<TResponse>> PatchJsonAsync(TRequest model, Either<string, Uri> url, params Header[] headers);
    Task<Maybe<TResponse>> PatchJsonAsync(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers);
}

public sealed class HttpMaybe<TRequest, TResponse> : IHttpMaybe<TRequest, TResponse>
{
    internal static readonly IHttpMaybe<TRequest, TResponse> Instance = new HttpMaybe<TRequest, TResponse>();

    public Maybe<TResponse> PostJson(TRequest model, Either<string, Uri> url, params Header[] headers) => HttpMaybe.Instance.PostJson<TRequest, TResponse>(model, url, headers);
    public Maybe<TResponse> PostJson(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers) => HttpMaybe.Instance.PostJson<TRequest, TResponse>(model, url, options, headers);

    public Maybe<TResponse> PutJson(TRequest model, Either<string, Uri> url, params Header[] headers) => HttpMaybe.Instance.PutJson<TRequest, TResponse>(model, url, headers);
    public Maybe<TResponse> PutJson(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers) => HttpMaybe.Instance.PutJson<TRequest, TResponse>(model, url, options, headers);

    public Maybe<TResponse> PatchJson(TRequest model, Either<string, Uri> url, params Header[] headers) => HttpMaybe.Instance.PatchJson<TRequest, TResponse>(model, url, headers);
    public Maybe<TResponse> PatchJson(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers) => HttpMaybe.Instance.PatchJson<TRequest, TResponse>(model, url, options, headers);

    public Task<Maybe<TResponse>> PostJsonAsync(TRequest model, Either<string, Uri> url, params Header[] headers) => HttpMaybe.Instance.PostJsonAsync<TRequest, TResponse>(model, url, headers);
    public Task<Maybe<TResponse>> PostJsonAsync(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers) => HttpMaybe.Instance.PostJsonAsync<TRequest, TResponse>(model, url, options, headers);

    public Task<Maybe<TResponse>> PutJsonAsync(TRequest model, Either<string, Uri> url, params Header[] headers) => HttpMaybe.Instance.PutJsonAsync<TRequest, TResponse>(model, url, headers);
    public Task<Maybe<TResponse>> PutJsonAsync(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers) => HttpMaybe.Instance.PutJsonAsync<TRequest, TResponse>(model, url, options, headers);

    public Task<Maybe<TResponse>> PatchJsonAsync(TRequest model, Either<string, Uri> url, params Header[] headers) => HttpMaybe.Instance.PatchJsonAsync<TRequest, TResponse>(model, url, headers);
    public Task<Maybe<TResponse>> PatchJsonAsync(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers) => HttpMaybe.Instance.PatchJsonAsync<TRequest, TResponse>(model, url, options, headers);
}
