using ContainerExpressions.Containers;
using FrameworkContainers.Network.HttpCollective.Models;
using System;
using System.Threading.Tasks;

namespace FrameworkContainers.Network.HttpCollective.Extensions;

public static class HttpExtensions
{
    public static Hes<TRequest> Send<TRequest>(this TRequest request, Either<string, Uri> url, params Header[] headers) => Send(request, url, HttpOptions.Default, headers);
    public static Hes<TRequest> Send<TRequest>(this TRequest request, Either<string, Uri> url, HttpOptions options, params Header[] headers)
    {
        return new Hes<TRequest>(request, url, options, headers);
    }

    public static Task<Response<TResponse>> Get<TResponse>(this string url, params Header[] headers) => Get<TResponse>(new Uri(url), HttpOptions.Default, headers);
    public static Task<Response<TResponse>> Get<TResponse>(this string url, HttpOptions options, params Header[] headers) => Get<TResponse>(new Uri(url), options, headers);
    public static Task<Response<TResponse>> Get<TResponse>(this Uri url, params Header[] headers) => Get<TResponse>(url, HttpOptions.Default, headers);
    public static Task<Response<TResponse>> Get<TResponse>(this Uri url, HttpOptions options, params Header[] headers)
    {
        return Http.Response.GetJsonAsync<TResponse>(url, options, headers);
    }

    public static Task<Response<HttpStatus>> DeleteStatus(this string url, params Header[] headers) => DeleteStatus(new Uri(url), HttpOptions.Default, headers);
    public static Task<Response<HttpStatus>> DeleteStatus(this string url, HttpOptions options, params Header[] headers) => DeleteStatus(new Uri(url), options, headers);
    public static Task<Response<HttpStatus>> DeleteStatus(this Uri url, params Header[] headers) => DeleteStatus(url, HttpOptions.Default, headers);
    public static Task<Response<HttpStatus>> DeleteStatus(this Uri url, HttpOptions options, params Header[] headers)
    {
        return Http.Response.DeleteStatusAsync(url,options, headers);
    }
}

public readonly struct Hes<TRequest>
{
    private readonly TRequest _request;
    private readonly Either<string, Uri> _url;
    private readonly HttpOptions _options;
    private readonly Header[] _headers;

    internal Hes(TRequest request, Either<string, Uri> url, HttpOptions options, params Header[] headers)
    {
        _request = request;
        _url = url;
        _options = options;
        _headers = headers;
    }

    public Task<Response<TResponse>> Post<TResponse>() => Http.Response.PostJsonAsync<TRequest, TResponse>(_request, _url, _options, _headers);
    public Task<Response<TResponse>> Put<TResponse>() => Http.Response.PutJsonAsync<TRequest, TResponse>(_request, _url, _options, _headers);
    public Task<Response<TResponse>> Patch<TResponse>() => Http.Response.PatchJsonAsync<TRequest, TResponse>(_request, _url, _options, _headers);
}
