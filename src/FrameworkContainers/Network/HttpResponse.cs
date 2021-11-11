using ContainerExpressions.Containers;
using FrameworkContainers.Format;
using System.Net.Http;
using System.Threading.Tasks;

namespace FrameworkContainers.Network
{
    public sealed class HttpResponse
    {
        internal HttpResponse() { }

        public Response<string> Post(string body, string url, string contentType, params Header[] headers)
        {
            return Post(body, url, contentType, HttpOptions.Default, headers);
        }

        public Response<string> Post(string body, string url, string contentType, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.Send(body, url, contentType, options, headers, "POST").Match(x => Response.Create(x), ex => { ex.LogValue(ex.ToString()); return new Response<string>(); });
        }

        public Response<TResponse> PostJson<TRequest, TResponse>(TRequest model, string url, params Header[] headers)
        {
            return PostJson<TRequest, TResponse>(model, url, HttpOptions.Default, headers);
        }

        public Response<TResponse> PostJson<TRequest, TResponse>(TRequest model, string url, HttpOptions options, params Header[] headers)
        {
            return Json.Response.FromModel(model)
            .Bind(json =>
                HypertextTransferProtocol.Send(json, url, "application/json", options, Http.AddJsonAccept(headers), "POST")
                .Match(x => Json.Response.ToModel<TResponse>(x), ex => { ex.LogValue(ex.ToString()); return new Response<TResponse>(); })
            );
        }

        public Response<string> Put(string body, string url, string contentType, params Header[] headers)
        {
            return Put(body, url, contentType, HttpOptions.Default, headers);
        }

        public Response<string> Put(string body, string url, string contentType, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.Send(body, url, contentType, options, headers, "PUT").Match(x => Response.Create(x), ex => { ex.LogValue(ex.ToString()); return new Response<string>(); });
        }

        public Response<TResponse> PutJson<TRequest, TResponse>(TRequest model, string url, params Header[] headers)
        {
            return PutJson<TRequest, TResponse>(model, url, HttpOptions.Default, headers);
        }

        public Response<TResponse> PutJson<TRequest, TResponse>(TRequest model, string url, HttpOptions options, params Header[] headers)
        {
            return Json.Response.FromModel(model)
            .Bind(json =>
                HypertextTransferProtocol.Send(json, url, "application/json", options, Http.AddJsonAccept(headers), "PUT")
                .Match(x => Json.Response.ToModel<TResponse>(x), ex => { ex.LogValue(ex.ToString()); return new Response<TResponse>(); })
            );
        }

        public Response<string> Get(string url, params Header[] headers)
        {
            return Get(url, HttpOptions.Default, headers);
        }

        public Response<string> Get(string url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.Send(string.Empty, url, string.Empty, options, headers, "GET").Match(x => Response.Create(x), ex => { ex.LogValue(ex.ToString()); return new Response<string>(); });
        }

        public Response<TResponse> GetJson<TResponse>(string url, params Header[] headers)
        {
            return GetJson<TResponse>(url, HttpOptions.Default, headers);
        }

        public Response<TResponse> GetJson<TResponse>(string url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.Send(string.Empty, url, string.Empty, options, Http.AddJsonAccept(headers), "GET").Match(x => Json.Response.ToModel<TResponse>(x), ex => { ex.LogValue(ex.ToString()); return new Response<TResponse>(); });
        }

        public Response<string> Delete(string url, params Header[] headers)
        {
            return Delete(url, HttpOptions.Default, headers);
        }

        public Response<string> Delete(string url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.Send(string.Empty, url, string.Empty, options, headers, "DELETE").Match(x => Response.Create(x), ex => { ex.LogValue(ex.ToString()); return new Response<string>(); });
        }

        public Response<TResponse> DeleteJson<TResponse>(string url, params Header[] headers)
        {
            return DeleteJson<TResponse>(url, HttpOptions.Default, headers);
        }

        public Response<TResponse> DeleteJson<TResponse>(string url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.Send(string.Empty, url, string.Empty, options, Http.AddJsonAccept(headers), "DELETE").Match(x => Json.Response.ToModel<TResponse>(x), ex => { ex.LogValue(ex.ToString()); return new Response<TResponse>(); });
        }

        public Task<Response<string>> PostAsync(string body, string url, string contentType, params Header[] headers)
        {
            return PostAsync(body, url, contentType, HttpOptions.Default, headers);
        }

        public Task<Response<string>> PostAsync(string body, string url, string contentType, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.SendAsync(body, url, contentType, options, headers, HttpMethod.Post).ContinueWith(x => x.Result.Match(y => Response.Create(y), ex => { ex.LogValue(ex.ToString()); return new Response<string>(); }));
        }

        public Task<Response<TResponse>> PostJsonAsync<TRequest, TResponse>(TRequest model, string url, params Header[] headers)
        {
            return PostJsonAsync<TRequest, TResponse>(model, url, HttpOptions.Default, headers);
        }

        public Task<Response<TResponse>> PostJsonAsync<TRequest, TResponse>(TRequest model, string url, HttpOptions options, params Header[] headers)
        {
            return Json.Response.FromModel(model)
            .BindAsync(json =>
                HypertextTransferProtocol.SendAsync(json, url, "application/json", options, Http.AddJsonAccept(headers), HttpMethod.Post)
                .ContinueWith(x => x.Result.Match(y => Json.Response.ToModel<TResponse>(y), ex => { ex.LogValue(ex.ToString()); return new Response<TResponse>(); }))
            );
        }

        public Task<Response<string>> PutAsync(string body, string url, string contentType, params Header[] headers)
        {
            return PutAsync(body, url, contentType, HttpOptions.Default, headers);
        }

        public Task<Response<string>> PutAsync(string body, string url, string contentType, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.SendAsync(body, url, contentType, options, headers, HttpMethod.Put).ContinueWith(x => x.Result.Match(y => Response.Create(y), ex => { ex.LogValue(ex.ToString()); return new Response<string>(); }));
        }

        public Task<Response<TResponse>> PutJsonAsync<TRequest, TResponse>(TRequest model, string url, params Header[] headers)
        {
            return PutJsonAsync<TRequest, TResponse>(model, url, HttpOptions.Default, headers);
        }

        public Task<Response<TResponse>> PutJsonAsync<TRequest, TResponse>(TRequest model, string url, HttpOptions options, params Header[] headers)
        {
            return Json.Response.FromModel(model)
            .BindAsync(json =>
                HypertextTransferProtocol.SendAsync(json, url, "application/json", options, Http.AddJsonAccept(headers), HttpMethod.Put)
                .ContinueWith(x => x.Result.Match(y => Json.Response.ToModel<TResponse>(y), ex => { ex.LogValue(ex.ToString()); return new Response<TResponse>(); }))
            );
        }

        public Task<Response<string>> GetAsync(string url, params Header[] headers)
        {
            return GetAsync(url, HttpOptions.Default, headers);
        }

        public Task<Response<string>> GetAsync(string url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.SendAsync(string.Empty, url, string.Empty, options, headers, HttpMethod.Get).ContinueWith(x => x.Result.Match(y => Response.Create(y), ex => { ex.LogValue(ex.ToString()); return new Response<string>(); }));
        }

        public Task<Response<TResponse>> GetJsonAsync<TResponse>(string url, params Header[] headers)
        {
            return GetJsonAsync<TResponse>(url, HttpOptions.Default, headers);
        }

        public Task<Response<TResponse>> GetJsonAsync<TResponse>(string url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.SendAsync(string.Empty, url, string.Empty, options, Http.AddJsonAccept(headers), HttpMethod.Get).ContinueWith(x => x.Result.Match(y => Json.Response.ToModel<TResponse>(y), ex => { ex.LogValue(ex.ToString()); return new Response<TResponse>(); }));
        }

        public Task<Response<string>> DeleteAsync(string url, params Header[] headers)
        {
            return DeleteAsync(url, HttpOptions.Default, headers);
        }

        public Task<Response<string>> DeleteAsync(string url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.SendAsync(string.Empty, url, string.Empty, options, headers, HttpMethod.Delete).ContinueWith(x => x.Result.Match(y => Response.Create(y), ex => { ex.LogValue(ex.ToString()); return new Response<string>(); }));
        }

        public Task<Response<TResponse>> DeleteAsync<TResponse>(string url, params Header[] headers)
        {
            return DeleteAsync<TResponse>(url, HttpOptions.Default, headers);
        }

        public Task<Response<TResponse>> DeleteAsync<TResponse>(string url, HttpOptions options, params Header[] headers)
        {
            return HypertextTransferProtocol.SendAsync(string.Empty, url, string.Empty, options, Http.AddJsonAccept(headers), HttpMethod.Delete).ContinueWith(x => x.Result.Match(y => Json.Response.ToModel<TResponse>(y), ex => { ex.LogValue(ex.ToString()); return new Response<TResponse>(); }));
        }
    }
}
