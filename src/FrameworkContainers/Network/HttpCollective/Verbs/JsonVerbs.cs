using System.Threading.Tasks;
using System;
using ContainerExpressions.Containers;
using FrameworkContainers.Network.HttpCollective.Models;
using FrameworkContainers.Models;

namespace FrameworkContainers.Network.HttpCollective.Verbs
{
    public readonly struct Get
    {
        private readonly Task<Response<HttpBody>> _verb;
        public Get(Either<string, Uri> url, params Header[] headers) : this(url, HttpOptions.Default, headers) { }
        public Get(Either<string, Uri> url, HttpOptions options, params Header[] headers) => _verb = Http.Response.GetAsync(url, options, headers);
        public VerbAwaiter<Response<HttpBody>> GetAwaiter() => new (_verb);
    }

    public readonly struct Post
    {
        private readonly Task<Response<HttpBody>> _verb;
        public Post(string request, Either<string, Uri> url, params Header[] headers) : this(request, url, HttpOptions.Default, headers) { }
        public Post(string request, Either<string, Uri> url, HttpOptions options, params Header[] headers) => _verb = Http.Response.PostAsync(request, url, Constants.Http.JSON_CONTENT, options, headers);
        public VerbAwaiter<Response<HttpBody>> GetAwaiter() => new (_verb);
    }

    public readonly struct Put
    {
        private readonly Task<Response<HttpBody>> _verb;
        public Put(string request, Either<string, Uri> url, params Header[] headers) : this(request, url, HttpOptions.Default, headers) { }
        public Put(string request, Either<string, Uri> url, HttpOptions options, params Header[] headers) => _verb = Http.Response.PutAsync(request, url, Constants.Http.JSON_CONTENT, options, headers);
        public VerbAwaiter<Response<HttpBody>> GetAwaiter() => new (_verb);
    }

    public readonly struct Patch
    {
        private readonly Task<Response<HttpBody>> _verb;
        public Patch(string request, Either<string, Uri> url, params Header[] headers) : this(request, url, HttpOptions.Default, headers) { }
        public Patch(string request, Either<string, Uri> url, HttpOptions options, params Header[] headers) => _verb = Http.Response.PatchAsync(request, url, Constants.Http.JSON_CONTENT, options, headers);
        public VerbAwaiter<Response<HttpBody>> GetAwaiter() => new (_verb);
    }

    public readonly struct DeleteStatus
    {
        private readonly Task<Response<HttpStatus>> _verb;
        public DeleteStatus(Either<string, Uri> url, params Header[] headers) : this(url, HttpOptions.Default, headers) { }
        public DeleteStatus(Either<string, Uri> url, HttpOptions options, params Header[] headers) => _verb = Http.Response.DeleteStatusAsync(url, options, headers);
        public VerbAwaiter<Response<HttpStatus>> GetAwaiter() => new (_verb);
    }

    public readonly struct PostStatus
    {
        private readonly Task<Response<HttpStatus>> _verb;
        public PostStatus(string request, Either<string, Uri> url, params Header[] headers) : this(request, url, HttpOptions.Default, headers) { }
        public PostStatus(string request, Either<string, Uri> url, HttpOptions options, params Header[] headers) => _verb = Http.Response.PostStatusAsync(request, url, Constants.Http.JSON_CONTENT, options, headers);
        public VerbAwaiter<Response<HttpStatus>> GetAwaiter() => new (_verb);
    }

    public readonly struct PutStatus
    {
        private readonly Task<Response<HttpStatus>> _verb;
        public PutStatus(string request, Either<string, Uri> url, params Header[] headers) : this(request, url, HttpOptions.Default, headers) { }
        public PutStatus(string request, Either<string, Uri> url, HttpOptions options, params Header[] headers) => _verb = Http.Response.PutStatusAsync(request, url, Constants.Http.JSON_CONTENT, options, headers);
        public VerbAwaiter<Response<HttpStatus>> GetAwaiter() => new (_verb);
    }

    public readonly struct PatchStatus
    {
        private readonly Task<Response<HttpStatus>> _verb;
        public PatchStatus(string request, Either<string, Uri> url, params Header[] headers) : this(request, url, HttpOptions.Default, headers) { }
        public PatchStatus(string request, Either<string, Uri> url, HttpOptions options, params Header[] headers) => _verb = Http.Response.PatchStatusAsync(request, url, Constants.Http.JSON_CONTENT, options, headers);
        public VerbAwaiter<Response<HttpStatus>> GetAwaiter() => new (_verb);
    }
}
