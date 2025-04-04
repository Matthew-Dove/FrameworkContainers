using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FrameworkContainers.Format.JsonCollective;

namespace Tests.FrameworkContainers.Network
{
    public class HttpServer : IDisposable
    {
        private readonly HttpListener _listener;
        private readonly CancellationTokenSource _cts;
        private readonly string _prefix;

        private Task _serverTask;

        // Actions to handle different http methods.
        public Func<HttpListenerRequest, object> GetHandler { get; set; }
        public Func<HttpListenerRequest, string, object> PostHandler { get; set; }
        public Func<HttpListenerRequest, string, object> PutHandler { get; set; }
        public Func<HttpListenerRequest, string, object> PatchHandler { get; set; }

        public HttpServer(string url)
        {
            _prefix = url;
            _listener = new HttpListener();
            _listener.Prefixes.Add(_prefix);
            _cts = new CancellationTokenSource();
        }

        public void Start()
        {
            _listener.Start();
            _serverTask = Task.Run(() => RunServerAsync(_cts.Token));
        }

        private async Task RunServerAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                try
                {
                    var context = await _listener.GetContextAsync();
                    _ = Task.Run(() => ProcessRequestAsync(context));
                }
                catch
                {
                    if (token.IsCancellationRequested) break; // Expected exception when cancellation is requested
                    throw;
                }
            }
        }

        private async Task ProcessRequestAsync(HttpListenerContext context)
        {
            var request = context.Request;
            var response = context.Response;

            try
            {
                string requestBody = null;
                if (request.HasEntityBody)
                {
                    using var reader = new StreamReader(request.InputStream, request.ContentEncoding);
                    requestBody = await reader.ReadToEndAsync();
                }

                object result = null;
                response.StatusCode = 500;

                // Handle http methods.
                switch (request.HttpMethod.ToUpper())
                {
                    case "GET":
                        result = GetHandler?.Invoke(request);
                        response.StatusCode = 200;
                        break;
                    case "POST":
                        result = PostHandler?.Invoke(request, requestBody);
                        response.StatusCode = 200;
                        break;
                    case "PUT":
                        result = PutHandler?.Invoke(request, requestBody);
                        response.StatusCode = 200;
                        break;
                    case "PATCH":
                        result = PatchHandler?.Invoke(request, requestBody);
                        response.StatusCode = 200;
                        break;
                    case "DELETE":
                        response.StatusCode = 204;
                        break;
                    default:
                        response.StatusCode = 405;
                        break;
                }

                // Send response.
                if (result != null)
                {
                    response.ContentType = "application/json";
                    var json = Json.FromModel(result);
                    var buffer = Encoding.UTF8.GetBytes(json);
                    response.ContentLength64 = buffer.Length;
                    await response.OutputStream.WriteAsync(buffer, 0, buffer.Length);
                }
            }
            finally
            {
                response.Close();
            }
        }

        public void Stop()
        {
            _cts.Cancel();
            _listener.Stop();
        }

        public void Dispose()
        {
            Stop();
            _cts.Dispose();
        }
    }
}