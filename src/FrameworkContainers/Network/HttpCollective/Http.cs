using ContainerExpressions.Containers;
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
    public static class Http
    {
        public static readonly HttpResponse Response = HttpResponse.Instance;

        public static readonly HttpMaybe Maybe = HttpMaybe.Instance;

        private static T IdentityValue<T, E>(Task<Either<T, E>> t) where E : Exception => t.Result.Match(Identity, static ex => throw ex);

        private static Func<string, T> Parse<T>(JsonOptions options) { return json => Json.ToModel<T>(json, options); }

        private static Func<Task<Either<string, HttpException>>, T> ParseAsync<T>(JsonOptions options) { return x => x.Result.Match(Parse<T>(options), static ex => throw ex); }

        public static string Post(string body, Either<string, Uri> url, string contentType, params Header[] headers)
        {
            return Post(body, url, contentType, HttpOptions.Default, headers);
        }

        public static string Post(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.Send(body, url.Match(static x => new Uri(x), Identity), contentType, options, headers, Constants.Http.POST).Match(Identity, static ex => throw ex);
        }

        public static HttpStatus PostJson<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers)
        {
            return PostJson<TRequest>(model, url, HttpOptions.Default, headers);
        }

        public static HttpStatus PostJson<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.Send(Json.FromModel(model, options), url.Match(static x => new Uri(x), Identity), Constants.Http.JSON_CONTENT, new HttpOptions(options, retrieveHttpStatus: true), headers, Constants.Http.POST).Match(static x => new HttpStatus(x), static ex => throw ex);
        }

        public static TResponse PostJson<TRequest, TResponse>(TRequest model, Either<string, Uri> url, params Header[] headers)
        {
            return PostJson<TRequest, TResponse>(model, url, HttpOptions.Default, headers);
        }

        public static TResponse PostJson<TRequest, TResponse>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.Send(Json.FromModel(model, options), url.Match(static x => new Uri(x), Identity), Constants.Http.JSON_CONTENT, options, headers, Constants.Http.POST).Match(Parse<TResponse>(options), static ex => throw ex);
        }

        public static string Put(string body, Either<string, Uri> url, string contentType, params Header[] headers)
        {
            return Put(body, url, contentType, HttpOptions.Default, headers);
        }

        public static string Put(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.Send(body, url.Match(static x => new Uri(x), Identity), contentType, options, headers, Constants.Http.PUT).Match(Identity, static ex => throw ex);
        }

        public static HttpStatus PutJson<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers)
        {
            return PutJson<TRequest>(model, url, HttpOptions.Default, headers);
        }

        public static HttpStatus PutJson<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.Send(Json.FromModel(model, options), url.Match(static x => new Uri(x), Identity), Constants.Http.JSON_CONTENT, new HttpOptions(options, retrieveHttpStatus: true), headers, Constants.Http.PUT).Match(static x => new HttpStatus(x), static ex => throw ex);
        }

        public static TResponse PutJson<TRequest, TResponse>(TRequest model, Either<string, Uri> url, params Header[] headers)
        {
            return PutJson<TRequest, TResponse>(model, url, HttpOptions.Default, headers);
        }

        public static TResponse PutJson<TRequest, TResponse>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.Send(Json.FromModel(model, options), url.Match(static x => new Uri(x), Identity), Constants.Http.JSON_CONTENT, options, headers, Constants.Http.PUT).Match(Parse<TResponse>(options), static ex => throw ex);
        }

        public static string Patch(string body, Either<string, Uri> url, string contentType, params Header[] headers)
        {
            return Patch(body, url, contentType, HttpOptions.Default, headers);
        }

        public static string Patch(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.Send(body, url.Match(static x => new Uri(x), Identity), contentType, options, headers, Constants.Http.PATCH).Match(Identity, static ex => throw ex);
        }

        public static HttpStatus PatchJson<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers)
        {
            return PatchJson<TRequest>(model, url, HttpOptions.Default, headers);
        }

        public static HttpStatus PatchJson<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.Send(Json.FromModel(model, options), url.Match(static x => new Uri(x), Identity), Constants.Http.JSON_CONTENT, new HttpOptions(options, retrieveHttpStatus: true), headers, Constants.Http.PATCH).Match(static x => new HttpStatus(x), static ex => throw ex);
        }

        public static TResponse PatchJson<TRequest, TResponse>(TRequest model, Either<string, Uri> url, params Header[] headers)
        {
            return PatchJson<TRequest, TResponse>(model, url, HttpOptions.Default, headers);
        }

        public static TResponse PatchJson<TRequest, TResponse>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.Send(Json.FromModel(model, options), url.Match(static x => new Uri(x), Identity), Constants.Http.JSON_CONTENT, options, headers, Constants.Http.PATCH).Match(Parse<TResponse>(options), static ex => throw ex);
        }

        public static string Get(Either<string, Uri> url, params Header[] headers)
        {
            return Get(url, HttpOptions.Default, headers);
        }

        public static string Get(Either<string, Uri> url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.Send(string.Empty, url.Match(static x => new Uri(x), Identity), string.Empty, options, headers, Constants.Http.GET).Match(Identity, static ex => throw ex);
        }

        public static TResponse GetJson<TResponse>(Either<string, Uri> url, params Header[] headers)
        {
            return GetJson<TResponse>(url, HttpOptions.Default, headers);
        }

        public static TResponse GetJson<TResponse>(Either<string, Uri> url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.Send(string.Empty, url.Match(static x => new Uri(x), Identity), string.Empty, options, headers, Constants.Http.GET).Match(Parse<TResponse>(options), static ex => throw ex);
        }

        public static string Delete(Either<string, Uri> url, params Header[] headers)
        {
            return Delete(url, HttpOptions.Default, headers);
        }

        public static string Delete(Either<string, Uri> url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.Send(string.Empty, url.Match(static x => new Uri(x), Identity), string.Empty, options, headers, Constants.Http.DELETE).Match(Identity, static ex => throw ex);
        }

        public static TResponse DeleteJson<TResponse>(Either<string, Uri> url, params Header[] headers)
        {
            return DeleteJson<TResponse>(url, HttpOptions.Default, headers);
        }

        public static TResponse DeleteJson<TResponse>(Either<string, Uri> url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.Send(string.Empty, url.Match(static x => new Uri(x), Identity), string.Empty, options, headers, Constants.Http.DELETE).Match(Parse<TResponse>(options), static ex => throw ex);
        }

        public static Task<string> PostAsync(string body, Either<string, Uri> url, string contentType, params Header[] headers)
        {
            return PostAsync(body, url, contentType, HttpOptions.Default, headers);
        }

        public static Task<string> PostAsync(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.SendAsync(body, url.Match(static x => new Uri(x), Identity), contentType, options, headers, HttpMethod.Post).ContinueWith(IdentityValue);
        }

        public static Task<HttpStatus> PostJsonAsync<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers)
        {
            return PostJsonAsync<TRequest>(model, url, HttpOptions.Default, headers);
        }

        public static Task<HttpStatus> PostJsonAsync<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.SendAsync(Json.FromModel(model, options), url.Match(static x => new Uri(x), Identity), Constants.Http.JSON_CONTENT, new HttpOptions(options, retrieveHttpStatus: true), headers, HttpMethod.Post).ContinueWith(static t => t.Result.Match(static x => new HttpStatus(x), static ex => throw ex));
        }

        public static Task<TResponse> PostJsonAsync<TRequest, TResponse>(TRequest model, Either<string, Uri> url, params Header[] headers)
        {
            return PostJsonAsync<TRequest, TResponse>(model, url, HttpOptions.Default, headers);
        }

        public static Task<TResponse> PostJsonAsync<TRequest, TResponse>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.SendAsync(Json.FromModel(model, options), url.Match(static x => new Uri(x), Identity), Constants.Http.JSON_CONTENT, options, headers, HttpMethod.Post).ContinueWith(ParseAsync<TResponse>(options));
        }

        public static Task<string> PutAsync(string body, Either<string, Uri> url, string contentType, params Header[] headers)
        {
            return PutAsync(body, url, contentType, HttpOptions.Default, headers);
        }

        public static Task<string> PutAsync(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.SendAsync(body, url.Match(static x => new Uri(x), Identity), contentType, options, headers, HttpMethod.Put).ContinueWith(IdentityValue);
        }

        public static Task<HttpStatus> PutJsonAsync<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers)
        {
            return PutJsonAsync<TRequest>(model, url, HttpOptions.Default, headers);
        }

        public static Task<HttpStatus> PutJsonAsync<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.SendAsync(Json.FromModel(model, options), url.Match(static x => new Uri(x), Identity), Constants.Http.JSON_CONTENT, new HttpOptions(options, retrieveHttpStatus: true), headers, HttpMethod.Put).ContinueWith(static t => t.Result.Match(static x => new HttpStatus(x), static ex => throw ex));
        }

        public static Task<TResponse> PutJsonAsync<TRequest, TResponse>(TRequest model, Either<string, Uri> url, params Header[] headers)
        {
            return PutJsonAsync<TRequest, TResponse>(model, url, HttpOptions.Default, headers);
        }

        public static Task<TResponse> PutJsonAsync<TRequest, TResponse>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.SendAsync(Json.FromModel(model, options), url.Match(static x => new Uri(x), Identity), Constants.Http.JSON_CONTENT, options, headers, HttpMethod.Put).ContinueWith(ParseAsync<TResponse>(options));
        }

        public static Task<string> PatchAsync(string body, Either<string, Uri> url, string contentType, params Header[] headers)
        {
            return PatchAsync(body, url, contentType, HttpOptions.Default, headers);
        }

        public static Task<string> PatchAsync(string body, Either<string, Uri> url, string contentType, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.SendAsync(body, url.Match(static x => new Uri(x), Identity), contentType, options, headers, HypertextTransferProtocol.Patch).ContinueWith(IdentityValue);
        }

        public static Task<HttpStatus> PatchJsonAsync<TRequest>(TRequest model, Either<string, Uri> url, params Header[] headers)
        {
            return PatchJsonAsync<TRequest>(model, url, HttpOptions.Default, headers);
        }

        public static Task<HttpStatus> PatchJsonAsync<TRequest>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.SendAsync(Json.FromModel(model, options), url.Match(static x => new Uri(x), Identity), Constants.Http.JSON_CONTENT, new HttpOptions(options, retrieveHttpStatus: true), headers, HypertextTransferProtocol.Patch).ContinueWith(static t => t.Result.Match(static x => new HttpStatus(x), static ex => throw ex));
        }

        public static Task<TResponse> PatchJsonAsync<TRequest, TResponse>(TRequest model, Either<string, Uri> url, params Header[] headers)
        {
            return PatchJsonAsync<TRequest, TResponse>(model, url, HttpOptions.Default, headers);
        }

        public static Task<TResponse> PatchJsonAsync<TRequest, TResponse>(TRequest model, Either<string, Uri> url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.SendJsonAsync<TRequest, TResponse>(model, url.Match(static x => new Uri(x), Identity), options, headers, HypertextTransferProtocol.Patch).ContinueWith(IdentityValue);
        }

        public static Task<string> GetAsync(Either<string, Uri> url, params Header[] headers)
        {
            return GetAsync(url, HttpOptions.Default, headers);
        }

        public static Task<string> GetAsync(Either<string, Uri> url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.SendAsync(string.Empty, url.Match(static x => new Uri(x), Identity), string.Empty, options, headers, HttpMethod.Get).ContinueWith(IdentityValue);
        }

        public static Task<TResponse> GetJsonAsync<TResponse>(Either<string, Uri> url, params Header[] headers)
        {
            return GetJsonAsync<TResponse>(url, HttpOptions.Default, headers);
        }

        public static Task<TResponse> GetJsonAsync<TResponse>(Either<string, Uri> url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.SendAsync(string.Empty, url.Match(static x => new Uri(x), Identity), string.Empty, options, headers, HttpMethod.Get).ContinueWith(ParseAsync<TResponse>(options));
        }

        public static Task<string> DeleteAsync(Either<string, Uri> url, params Header[] headers)
        {
            return DeleteAsync(url, HttpOptions.Default, headers);
        }

        public static Task<string> DeleteAsync(Either<string, Uri> url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.SendAsync(string.Empty, url.Match(static x => new Uri(x), Identity), string.Empty, options, headers, HttpMethod.Delete).ContinueWith(IdentityValue);
        }

        public static Task<TResponse> DeleteJsonAsync<TResponse>(Either<string, Uri> url, params Header[] headers)
        {
            return DeleteJsonAsync<TResponse>(url, HttpOptions.Default, headers);
        }

        public static Task<TResponse> DeleteJsonAsync<TResponse>(Either<string, Uri> url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.SendAsync(string.Empty, url.Match(static x => new Uri(x), Identity), string.Empty, options, headers, HttpMethod.Delete).ContinueWith(ParseAsync<TResponse>(options));
        }
    }
}
