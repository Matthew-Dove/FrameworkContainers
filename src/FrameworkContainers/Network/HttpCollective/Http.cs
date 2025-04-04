using ContainerExpressions.Containers;
using FrameworkContainers.Format.JsonCollective;
using FrameworkContainers.Format.JsonCollective.Models;
using FrameworkContainers.Models;
using FrameworkContainers.Network.HttpCollective.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace FrameworkContainers.Network.HttpCollective;

public static class Http
{
    public static readonly HttpResponse Response = HttpResponse.Instance;
    public static readonly HttpMaybe Maybe = HttpMaybe.Instance;

    /// <summary>Tear down the underlying HTTP Client, and dispose of any resources (only call this when your application is shutting down).</summary>
    public static void TearDown() => HypertextTransferProtocol.TearDown();

    private static HttpStatus IdentityValueStatus<E>(Task<Either<string, E>> t) where E : Exception => t.Result.Match(HttpStatus.Create, Throw<HttpStatus>);

    private static HttpStatus IdentityValueStatus<E>(Task<Either<HttpStatus, E>> t) where E : Exception => t.Result.Match(Identity, Throw<HttpStatus>);

    private static HttpBody IdentityValueBody<E>(Task<Either<string, E>> t) where E : Exception => t.Result.Match(HttpBody.Create, Throw<HttpBody>);

    private static T IdentityValue<T, E>(Task<Either<T, E>> t) where E : Exception => t.Result.Match(Identity, Throw<T>);

    private static Func<string, T> Parse<T>(JsonOptions options) { return json => Json.ToModel<T>(json, options); }

    public static Http245 Post245(string body, Either<string, Uri> url, string contentType, params Header[] headers)
    {
        return Post245(body, url, contentType, HttpOptions.Default, headers);
    }

    public static Http245 Post245(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .Send245(body, url.Match(static x => new Uri(x), Identity), contentType, options, headers, Constants.Http.POST)
            .Match(Identity, Throw<Http245>);
    }

    public static HttpBody Post(string body, Either<string, Uri> url, string contentType, params Header[] headers)
    {
        return Post(body, url, contentType, HttpOptions.Default, headers);
    }

    public static HttpBody Post(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .Send(body, url.Match(static x => new Uri(x), Identity), contentType, options, headers, Constants.Http.POST)
            .Match(HttpBody.Create, Throw<HttpBody>);
    }

    public static HttpStatus PostStatus(string body, Either<string, Uri> url, string contentType, params Header[] headers)
    {
        return PostStatus(body, url, contentType, HttpOptions.Default, headers);
    }

    public static HttpStatus PostStatus(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .Send(body, url.Match(static x => new Uri(x), Identity), contentType, new HttpOptions(options, retrieveHttpStatus: true), headers, Constants.Http.POST)
            .Match(HttpStatus.Create, Throw<HttpStatus>);
    }

    public static HttpStatus PostJsonStatus<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers)
    {
        return PostJsonStatus<TRequest>(model, url, HttpOptions.Default, headers);
    }

    public static HttpStatus PostJsonStatus<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .Send(Json.FromModel(model, options), url.Match(static x => new Uri(x), Identity), Constants.Http.JSON_CONTENT, new HttpOptions(options, retrieveHttpStatus: true), headers, Constants.Http.POST)
            .Match(HttpStatus.Create, Throw<HttpStatus>);
    }

    public static TResponse PostJson<TRequest, TResponse>(TRequest model, Either<string, Uri> url, params Header[] headers)
    {
        return PostJson<TRequest, TResponse>(model, url, HttpOptions.Default, headers);
    }

    public static TResponse PostJson<TRequest, TResponse>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .Send(Json.FromModel(model, options), url.Match(static x => new Uri(x), Identity), Constants.Http.JSON_CONTENT, options, headers, Constants.Http.POST)
            .Match(Parse<TResponse>(options), Throw<TResponse>);
    }

    public static Http245 Put245(string body, Either<string, Uri> url, string contentType, params Header[] headers)
    {
        return Put245(body, url, contentType, HttpOptions.Default, headers);
    }

    public static Http245 Put245(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .Send245(body, url.Match(static x => new Uri(x), Identity), contentType, options, headers, Constants.Http.PUT)
            .Match(Identity, Throw<Http245>);
    }

    public static HttpBody Put(string body, Either<string, Uri> url, string contentType, params Header[] headers)
    {
        return Put(body, url, contentType, HttpOptions.Default, headers);
    }

    public static HttpBody Put(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .Send(body, url.Match(static x => new Uri(x), Identity), contentType, options, headers, Constants.Http.PUT)
            .Match(HttpBody.Create, Throw<HttpBody>);
    }

    public static HttpStatus PutStatus(string body, Either<string, Uri> url, string contentType, params Header[] headers)
    {
        return PutStatus(body, url, contentType, HttpOptions.Default, headers);
    }

    public static HttpStatus PutStatus(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .Send(body, url.Match(static x => new Uri(x), Identity), contentType, new HttpOptions(options, retrieveHttpStatus: true), headers, Constants.Http.PUT)
            .Match(HttpStatus.Create, Throw<HttpStatus>);
    }

    public static HttpStatus PutJsonStatus<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers)
    {
        return PutJsonStatus<TRequest>(model, url, HttpOptions.Default, headers);
    }

    public static HttpStatus PutJsonStatus<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .Send(Json.FromModel(model, options), url.Match(static x => new Uri(x), Identity), Constants.Http.JSON_CONTENT, new HttpOptions(options, retrieveHttpStatus: true), headers, Constants.Http.PUT)
            .Match(HttpStatus.Create, Throw<HttpStatus>);
    }

    public static TResponse PutJson<TRequest, TResponse>(TRequest model, Either<string, Uri> url, params Header[] headers)
    {
        return PutJson<TRequest, TResponse>(model, url, HttpOptions.Default, headers);
    }

    public static TResponse PutJson<TRequest, TResponse>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .Send(Json.FromModel(model, options), url.Match(static x => new Uri(x), Identity), Constants.Http.JSON_CONTENT, options, headers, Constants.Http.PUT)
            .Match(Parse<TResponse>(options), Throw<TResponse>);
    }

    public static Http245 Patch245(string body, Either<string, Uri> url, string contentType, params Header[] headers)
    {
        return Patch245(body, url, contentType, HttpOptions.Default, headers);
    }

    public static Http245 Patch245(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .Send245(body, url.Match(static x => new Uri(x), Identity), contentType, options, headers, Constants.Http.PATCH)
            .Match(Identity, Throw<Http245>);
    }

    public static HttpBody Patch(string body, Either<string, Uri> url, string contentType, params Header[] headers)
    {
        return Patch(body, url, contentType, HttpOptions.Default, headers);
    }

    public static HttpBody Patch(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .Send(body, url.Match(static x => new Uri(x), Identity), contentType, options, headers, Constants.Http.PATCH)
            .Match(HttpBody.Create, Throw<HttpBody>);
    }

    public static HttpStatus PatchStatus(string body, Either<string, Uri> url, string contentType, params Header[] headers)
    {
        return PatchStatus(body, url, contentType, HttpOptions.Default, headers);
    }

    public static HttpStatus PatchStatus(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .Send(body, url.Match(static x => new Uri(x), Identity), contentType, new HttpOptions(options, retrieveHttpStatus: true), headers, Constants.Http.PATCH)
            .Match(HttpStatus.Create, Throw<HttpStatus>);
    }

    public static HttpStatus PatchJsonStatus<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers)
    {
        return PatchJsonStatus<TRequest>(model, url, HttpOptions.Default, headers);
    }

    public static HttpStatus PatchJsonStatus<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .Send(Json.FromModel(model, options), url.Match(static x => new Uri(x), Identity), Constants.Http.JSON_CONTENT, new HttpOptions(options, retrieveHttpStatus: true), headers, Constants.Http.PATCH)
            .Match(HttpStatus.Create, Throw<HttpStatus>);
    }

    public static TResponse PatchJson<TRequest, TResponse>(TRequest model, Either<string, Uri> url, params Header[] headers)
    {
        return PatchJson<TRequest, TResponse>(model, url, HttpOptions.Default, headers);
    }

    public static TResponse PatchJson<TRequest, TResponse>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .Send(Json.FromModel(model, options), url.Match(static x => new Uri(x), Identity), Constants.Http.JSON_CONTENT, options, headers, Constants.Http.PATCH)
            .Match(Parse<TResponse>(options), Throw<TResponse>);
    }

    public static Http245 Get245(Either<string, Uri> url, params Header[] headers)
    {
        return Get245(url, HttpOptions.Default, headers);
    }

    public static Http245 Get245(Either<string, Uri> url, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .Send245(string.Empty, url.Match(static x => new Uri(x), Identity), string.Empty, options, headers, Constants.Http.GET)
            .Match(Identity, Throw<Http245>);
    }

    public static HttpBody Get(Either<string, Uri> url, params Header[] headers)
    {
        return Get(url, HttpOptions.Default, headers);
    }

    public static HttpBody Get(Either<string, Uri> url, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .Send(string.Empty, url.Match(static x => new Uri(x), Identity), string.Empty, options, headers, Constants.Http.GET)
            .Match(HttpBody.Create, Throw<HttpBody>);
    }

    public static HttpStatus GetStatus(Either<string, Uri> url, params Header[] headers)
    {
        return GetStatus(url, HttpOptions.Default, headers);
    }

    public static HttpStatus GetStatus(Either<string, Uri> url, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .Send(string.Empty, url.Match(static x => new Uri(x), Identity), string.Empty, new HttpOptions(options, retrieveHttpStatus: true), headers, Constants.Http.GET)
            .Match(HttpStatus.Create, Throw<HttpStatus>);
    }

    public static TResponse GetJson<TResponse>(Either<string, Uri> url, params Header[] headers)
    {
        return GetJson<TResponse>(url, HttpOptions.Default, headers);
    }

    public static TResponse GetJson<TResponse>(Either<string, Uri> url, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .Send(string.Empty, url.Match(static x => new Uri(x), Identity), string.Empty, options, headers, Constants.Http.GET)
            .Match(Parse<TResponse>(options), Throw<TResponse>);
    }

    public static HttpStatus DeleteStatus(Either<string, Uri> url, params Header[] headers)
    {
        return DeleteStatus(url, HttpOptions.Default, headers);
    }

    public static HttpStatus DeleteStatus(Either<string, Uri> url, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .Send(string.Empty, url.Match(static x => new Uri(x), Identity), string.Empty, new HttpOptions(options, retrieveHttpStatus: true), headers, Constants.Http.DELETE)
            .Match(HttpStatus.Create, Throw<HttpStatus>);
    }

    public static Task<Http245> Post245Async(string body, Either<string, Uri> url, string contentType, params Header[] headers)
    {
        return Post245Async(body, url, contentType, HttpOptions.Default, headers);
    }

    public static Task<Http245> Post245Async(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .Send245Async(body, url.Match(static x => new Uri(x), Identity), contentType, options, headers, HttpMethod.Post)
            .ContinueWith(IdentityValue);
    }

    public static Task<HttpBody> PostAsync(string body, Either<string, Uri> url, string contentType, params Header[] headers)
    {
        return PostAsync(body, url, contentType, HttpOptions.Default, headers);
    }

    public static Task<HttpBody> PostAsync(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .SendAsync(body, url.Match(static x => new Uri(x), Identity), contentType, options, headers, HttpMethod.Post)
            .ContinueWith(IdentityValueBody);
    }

    public static Task<HttpStatus> PostStatusAsync(string body, Either<string, Uri> url, string contentType, params Header[] headers)
    {
        return PostStatusAsync(body, url, contentType, HttpOptions.Default, headers);
    }

    public static Task<HttpStatus> PostStatusAsync(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .SendAsync(body, url.Match(static x => new Uri(x), Identity), contentType, new HttpOptions(options, retrieveHttpStatus: true), headers, HttpMethod.Post)
            .ContinueWith(IdentityValueStatus);
    }

    public static Task<HttpStatus> PostJsonStatusAsync<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers)
    {
        return PostJsonStatusAsync<TRequest>(model, url, HttpOptions.Default, headers);
    }

    public static Task<HttpStatus> PostJsonStatusAsync<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .SendJsonStatusAsync<TRequest>(model, url.Match(static x => new Uri(x), Identity), new HttpOptions(options, retrieveHttpStatus: true), headers, HttpMethod.Post)
            .ContinueWith(IdentityValueStatus);
    }

    public static Task<TResponse> PostJsonAsync<TRequest, TResponse>(TRequest model, Either<string, Uri> url, params Header[] headers)
    {
        return PostJsonAsync<TRequest, TResponse>(model, url, HttpOptions.Default, headers);
    }

    public static Task<TResponse> PostJsonAsync<TRequest, TResponse>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .SendJsonAsync<TRequest, TResponse>(model, url.Match(static x => new Uri(x), Identity), options, headers, HttpMethod.Post)
            .ContinueWith(IdentityValue);
    }

    public static Task<Http245> Put245Async(string body, Either<string, Uri> url, string contentType, params Header[] headers)
    {
        return Put245Async(body, url, contentType, HttpOptions.Default, headers);
    }

    public static Task<Http245> Put245Async(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .Send245Async(body, url.Match(static x => new Uri(x), Identity), contentType, options, headers, HttpMethod.Put)
            .ContinueWith(IdentityValue);
    }

    public static Task<HttpBody> PutAsync(string body, Either<string, Uri> url, string contentType, params Header[] headers)
    {
        return PutAsync(body, url, contentType, HttpOptions.Default, headers);
    }

    public static Task<HttpBody> PutAsync(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .SendAsync(body, url.Match(static x => new Uri(x), Identity), contentType, options, headers, HttpMethod.Put)
            .ContinueWith(IdentityValueBody);
    }

    public static Task<HttpStatus> PutStatusAsync(string body, Either<string, Uri> url, string contentType, params Header[] headers)
    {
        return PutStatusAsync(body, url, contentType, HttpOptions.Default, headers);
    }

    public static Task<HttpStatus> PutStatusAsync(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .SendAsync(body, url.Match(static x => new Uri(x), Identity), contentType, new HttpOptions(options, retrieveHttpStatus: true), headers, HttpMethod.Put)
            .ContinueWith(IdentityValueStatus);
    }

    public static Task<HttpStatus> PutJsonStatusAsync<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers)
    {
        return PutJsonStatusAsync<TRequest>(model, url, HttpOptions.Default, headers);
    }

    public static Task<HttpStatus> PutJsonStatusAsync<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .SendJsonStatusAsync<TRequest>(model, url.Match(static x => new Uri(x), Identity), new HttpOptions(options, retrieveHttpStatus: true), headers, HttpMethod.Put)
            .ContinueWith(IdentityValueStatus);
    }

    public static Task<TResponse> PutJsonAsync<TRequest, TResponse>(TRequest model, Either<string, Uri> url, params Header[] headers)
    {
        return PutJsonAsync<TRequest, TResponse>(model, url, HttpOptions.Default, headers);
    }

    public static Task<TResponse> PutJsonAsync<TRequest, TResponse>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .SendJsonAsync<TRequest, TResponse>(model, url.Match(static x => new Uri(x), Identity), options, headers, HttpMethod.Put)
            .ContinueWith(IdentityValue);
    }

    public static Task<Http245> Patch245Async(string body, Either<string, Uri> url, string contentType, params Header[] headers)
    {
        return Patch245Async(body, url, contentType, HttpOptions.Default, headers);
    }

    public static Task<Http245> Patch245Async(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .Send245Async(body, url.Match(static x => new Uri(x), Identity), contentType, options, headers, HypertextTransferProtocol.Patch)
            .ContinueWith(IdentityValue);
    }

    public static Task<HttpBody> PatchAsync(string body, Either<string, Uri> url, string contentType, params Header[] headers)
    {
        return PatchAsync(body, url, contentType, HttpOptions.Default, headers);
    }

    public static Task<HttpBody> PatchAsync(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .SendAsync(body, url.Match(static x => new Uri(x), Identity), contentType, options, headers, HypertextTransferProtocol.Patch)
            .ContinueWith(IdentityValueBody);
    }

    public static Task<HttpStatus> PatchStatusAsync(string body, Either<string, Uri> url, string contentType, params Header[] headers)
    {
        return PatchStatusAsync(body, url, contentType, HttpOptions.Default, headers);
    }

    public static Task<HttpStatus> PatchStatusAsync(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .SendAsync(body, url.Match(static x => new Uri(x), Identity), contentType, new HttpOptions(options, retrieveHttpStatus: true), headers, HypertextTransferProtocol.Patch)
            .ContinueWith(IdentityValueStatus);
    }

    public static Task<HttpStatus> PatchJsonStatusAsync<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers)
    {
        return PatchJsonStatusAsync<TRequest>(model, url, HttpOptions.Default, headers);
    }

    public static Task<HttpStatus> PatchJsonStatusAsync<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .SendJsonStatusAsync<TRequest>(model, url.Match(static x => new Uri(x), Identity), new HttpOptions(options, retrieveHttpStatus: true), headers, HypertextTransferProtocol.Patch)
            .ContinueWith(IdentityValueStatus);
    }

    public static Task<TResponse> PatchJsonAsync<TRequest, TResponse>(TRequest model, Either<string, Uri> url, params Header[] headers)
    {
        return PatchJsonAsync<TRequest, TResponse>(model, url, HttpOptions.Default, headers);
    }

    public static Task<TResponse> PatchJsonAsync<TRequest, TResponse>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .SendJsonAsync<TRequest, TResponse>(model, url.Match(static x => new Uri(x), Identity), options, headers, HypertextTransferProtocol.Patch)
            .ContinueWith(IdentityValue);
    }

    public static Task<Http245> Get245Async(Either<string, Uri> url, params Header[] headers)
    {
        return Get245Async(url, HttpOptions.Default, headers);
    }

    public static Task<Http245> Get245Async(Either<string, Uri> url, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .Send245Async(string.Empty, url.Match(static x => new Uri(x), Identity), string.Empty, options, headers, HttpMethod.Get)
            .ContinueWith(IdentityValue);
    }

    public static Task<HttpBody> GetAsync(Either<string, Uri> url, params Header[] headers)
    {
        return GetAsync(url, HttpOptions.Default, headers);
    }

    public static Task<HttpBody> GetAsync(Either<string, Uri> url, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .SendAsync(string.Empty, url.Match(static x => new Uri(x), Identity), string.Empty, options, headers, HttpMethod.Get)
            .ContinueWith(IdentityValueBody);
    }

    public static Task<HttpStatus> GetStatusAsync(Either<string, Uri> url, params Header[] headers)
    {
        return GetStatusAsync(url, HttpOptions.Default, headers);
    }

    public static Task<HttpStatus> GetStatusAsync(Either<string, Uri> url, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .SendAsync(string.Empty, url.Match(static x => new Uri(x), Identity), string.Empty, new HttpOptions(options, retrieveHttpStatus: true), headers, HttpMethod.Get)
            .ContinueWith(IdentityValueStatus);
    }

    public static Task<TResponse> GetJsonAsync<TResponse>(Either<string, Uri> url, params Header[] headers)
    {
        return GetJsonAsync<TResponse>(url, HttpOptions.Default, headers);
    }

    public static Task<TResponse> GetJsonAsync<TResponse>(Either<string, Uri> url, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .SendJsonAsync<TResponse>(url.Match(static x => new Uri(x), Identity), options, headers, HttpMethod.Get)
            .ContinueWith(IdentityValue);
    }

    public static Task<HttpStatus> DeleteStatusAsync(Either<string, Uri> url, params Header[] headers)
    {
        return DeleteStatusAsync(url, HttpOptions.Default, headers);
    }

    public static Task<HttpStatus> DeleteStatusAsync(Either<string, Uri> url, HttpOptions options, params Header[] headers)
    {
        return HypertextTransferProtocol
            .SendAsync(string.Empty, url.Match(static x => new Uri(x), Identity), string.Empty, new HttpOptions(options, retrieveHttpStatus: true), headers, HttpMethod.Delete)
            .ContinueWith(IdentityValueStatus);
    }
}

public static class Http<T>
{
    public static readonly HttpClient<T> Client = new HttpClient<T>();
}

public static class Http<TRequest, TResponse>
{
    public static readonly HttpClient<TRequest, TResponse> Client = new HttpClient<TRequest, TResponse>();
}
