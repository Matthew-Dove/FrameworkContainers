using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace FrameworkContainers.Network.HttpCollective.Models
{
    internal readonly struct HttpResult<T>
    {
        public bool IsValid { get; }
        public T Value { get; }
        public Task<T> Task { get; }

        public HttpResult(Task<T> task)
        {
            IsValid = false;
            Value = default;
            Task = task;

            if (!task.IsCanceled && !task.IsFaulted)
            {
                IsValid = true;
                Value = task.Result;
            }
        }

        public static HttpResult<T> Create(Task<T> task) => new HttpResult<T>(task);

        public static implicit operator T(HttpResult<T> http) => http.Value;
        public static implicit operator bool(HttpResult<T> http) => http.IsValid;
        public static implicit operator Exception(HttpResult<T> http) => http.Task.IsFaulted ? http.Task.Exception : null;
    }

    internal readonly struct HttpResult : IDisposable
    {
        public bool IsValid { get; }
        public HttpResponseMessage Value { get; }
        public Task<HttpResponseMessage> Task { get; }

        public HttpResult(Task<HttpResponseMessage> task)
        {
            IsValid = false;
            Value = default;
            Task = task;

            if (!task.IsCanceled && !task.IsFaulted)
            {
                IsValid = true;
                Value = task.Result;
            }
        }

        public static HttpResult Create(Task<HttpResponseMessage> task) => new HttpResult(task);

        public static implicit operator HttpResponseMessage(HttpResult http) => http.Value;
        public static implicit operator bool(HttpResult http) => http.IsValid;
        public static implicit operator Exception(HttpResult http) => http.Task.IsFaulted ? http.Task.Exception : null;

        public void Dispose() => Value?.Dispose();
    }

    internal static class HttpResultExtensions
    {
        public static T GetValueOr<T>(this HttpResult<T> http, T @default) => http ? http : @default;

        public static async Task<string> TryGetBody(this HttpResult http)
        {
            if (http.Value?.Content == null) return string.Empty;
            try { return await http.Value.Content.ReadAsStringAsync().ConfigureAwait(false) ?? string.Empty; }
            catch (Exception) { return string.Empty; }
        }
    }
}
