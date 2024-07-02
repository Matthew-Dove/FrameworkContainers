using ContainerExpressions.Containers;
using FrameworkContainers.Network.HttpCollective.Models;
using System.Threading.Tasks;
using System;

namespace FrameworkContainers.Network.HttpCollective.Verbs
{
    public readonly struct Get<TResponse>
    {
        private readonly Task<Response<TResponse>> _verb;
        public Get(Either<string, Uri> url, params Header[] headers) : this(url, HttpOptions.Default, headers) { }
        public Get(Either<string, Uri> url, HttpOptions options, params Header[] headers) => _verb = Http.Response.GetJsonAsync<TResponse>(url, options, headers);
        public VerbAwaiter<Response<TResponse>> GetAwaiter() => new (_verb);
    }

    public readonly struct Post<TRequest, TResponse>
    {
        private readonly Task<Response<TResponse>> _verb;
        public Post(TRequest request, Either<string, Uri> url, params Header[] headers) : this(request, url, HttpOptions.Default, headers) { }
        public Post(TRequest request, Either<string, Uri> url, HttpOptions options, params Header[] headers) => _verb = Http.Response.PostJsonAsync<TRequest, TResponse>(request, url, options, headers);
        public VerbAwaiter<Response<TResponse>> GetAwaiter() => new (_verb);
    }

    public readonly struct Put<TRequest, TResponse>
    {
        private readonly Task<Response<TResponse>> _verb;
        public Put(TRequest request, Either<string, Uri> url, params Header[] headers) : this(request, url, HttpOptions.Default, headers) { }
        public Put(TRequest request, Either<string, Uri> url, HttpOptions options, params Header[] headers) => _verb = Http.Response.PutJsonAsync<TRequest, TResponse>(request, url, options, headers);
        public VerbAwaiter<Response<TResponse>> GetAwaiter() => new (_verb);
    }

    public readonly struct Patch<TRequest, TResponse>
    {
        private readonly Task<Response<TResponse>> _verb;
        public Patch(TRequest request, Either<string, Uri> url, params Header[] headers) : this(request, url, HttpOptions.Default, headers) { }
        public Patch(TRequest request, Either<string, Uri> url, HttpOptions options, params Header[] headers) => _verb = Http.Response.PatchJsonAsync<TRequest, TResponse>(request, url, options, headers);
        public VerbAwaiter<Response<TResponse>> GetAwaiter() => new (_verb);
    }

    public readonly struct PostStatus<TRequest>
    {
        private readonly Task<Response<HttpStatus>> _verb;
        public PostStatus(TRequest request, Either<string, Uri> url, params Header[] headers) : this(request, url, HttpOptions.Default, headers) { }
        public PostStatus(TRequest request, Either<string, Uri> url, HttpOptions options, params Header[] headers) => _verb = Http.Response.PostJsonStatusAsync(request, url, options, headers);
        public VerbAwaiter<Response<HttpStatus>> GetAwaiter() => new (_verb);
    }

    public readonly struct PutStatus<TRequest>
    {
        private readonly Task<Response<HttpStatus>> _verb;
        public PutStatus(TRequest request, Either<string, Uri> url, params Header[] headers) : this(request, url, HttpOptions.Default, headers) { }
        public PutStatus(TRequest request, Either<string, Uri> url, HttpOptions options, params Header[] headers) => _verb = Http.Response.PutJsonStatusAsync(request, url, options, headers);
        public VerbAwaiter<Response<HttpStatus>> GetAwaiter() => new (_verb);
    }

    public readonly struct PatchStatus<TRequest>
    {
        private readonly Task<Response<HttpStatus>> _verb;
        public PatchStatus(TRequest request, Either<string, Uri> url, params Header[] headers) : this(request, url, HttpOptions.Default, headers) { }
        public PatchStatus(TRequest request, Either<string, Uri> url, HttpOptions options, params Header[] headers) => _verb = Http.Response.PatchJsonStatusAsync(request, url, options, headers);
        public VerbAwaiter<Response<HttpStatus>> GetAwaiter() => new (_verb);
    }
}
