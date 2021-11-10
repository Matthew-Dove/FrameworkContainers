using ContainerExpressions.Containers;
using FrameworkContainers.Models.Exceptions;
using System.Net.Http;
using System.Threading.Tasks;

namespace FrameworkContainers.Network
{
    public sealed class HttpMaybe
    {
        public Maybe<string, HttpException> Post(string body, string url, string contentType, params Header[] headers)
        {
            return Post(body, url, contentType, HttpOptions.Default, headers);
        }

        public Maybe<string, HttpException> Post(string body, string url, string contentType, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.Send(body, url, contentType, options, headers, "POST").Match(x => new Maybe<string, HttpException>(x), ex => new Maybe<string, HttpException>(ex));
        }

        public Maybe<string, HttpException> Put(string body, string url, string contentType, params Header[] headers)
        {
            return Put(body, url, contentType, HttpOptions.Default, headers);
        }

        public Maybe<string, HttpException> Put(string body, string url, string contentType, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.Send(body, url, contentType, options, headers, "PUT").Match(x => new Maybe<string, HttpException>(x), ex => new Maybe<string, HttpException>(ex));
        }

        public Maybe<string, HttpException> Get(string url, params Header[] headers)
        {
            return Get(url, HttpOptions.Default, headers);
        }

        public Maybe<string, HttpException> Get(string url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.Send(string.Empty, url, string.Empty, options, headers, "GET").Match(x => new Maybe<string, HttpException>(x), ex => new Maybe<string, HttpException>(ex));
        }

        public Maybe<string, HttpException> Delete(string url, params Header[] headers)
        {
            return Delete(url, HttpOptions.Default, headers);
        }

        public Maybe<string, HttpException> Delete(string url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.Send(string.Empty, url, string.Empty, options, headers, "DELETE").Match(x => new Maybe<string, HttpException>(x), ex => new Maybe<string, HttpException>(ex));
        }

        public Task<Maybe<string, HttpException>> PostAsync(string body, string url, string contentType, params Header[] headers)
        {
            return PostAsync(body, url, contentType, HttpOptions.Default, headers);
        }

        public Task<Maybe<string, HttpException>> PostAsync(string body, string url, string contentType, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.SendAsync(body, url, contentType, options, headers, HttpMethod.Post).ContinueWith(x => x.Result.Match(y => new Maybe<string, HttpException>(y), ex => new Maybe<string, HttpException>(ex)));
        }

        public Task<Maybe<string, HttpException>> PutAsync(string body, string url, string contentType, params Header[] headers)
        {
            return PutAsync(body, url, contentType, HttpOptions.Default, headers);
        }

        public Task<Maybe<string, HttpException>> PutAsync(string body, string url, string contentType, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.SendAsync(body, url, contentType, options, headers, HttpMethod.Put).ContinueWith(x => x.Result.Match(y => new Maybe<string, HttpException>(y), ex => new Maybe<string, HttpException>(ex)));
        }

        public Task<Maybe<string, HttpException>> GetAsync(string url, params Header[] headers)
        {
            return GetAsync(url, HttpOptions.Default, headers);
        }

        public Task<Maybe<string, HttpException>> GetAsync(string url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.SendAsync(string.Empty, url, string.Empty, options, headers, HttpMethod.Get).ContinueWith(x => x.Result.Match(y => new Maybe<string, HttpException>(y), ex => new Maybe<string, HttpException>(ex)));
        }

        public Task<Maybe<string, HttpException>> DeleteAsync(string url, params Header[] headers)
        {
            return DeleteAsync(url, HttpOptions.Default, headers);
        }

        public Task<Maybe<string, HttpException>> DeleteAsync(string url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.SendAsync(string.Empty, url, string.Empty, options, headers, HttpMethod.Delete).ContinueWith(x => x.Result.Match(y => new Maybe<string, HttpException>(y), ex => new Maybe<string, HttpException>(ex)));
        }
    }
}
