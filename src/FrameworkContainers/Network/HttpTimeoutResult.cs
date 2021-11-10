using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace FrameworkContainers.Network
{
    internal readonly struct HttpTimeoutResult : IDisposable
    {
        public bool IsComplete { get; }
        public HttpResponseMessage Message { get; }
        public bool IsTimeout { get; }
        public bool IsFaulted { get; }

        public HttpTimeoutResult(Task<HttpResponseMessage> task)
        {
            IsComplete = false;
            Message = null;
            IsTimeout = task.IsCanceled;
            IsFaulted = task.IsFaulted;

            if (!task.IsCanceled && !task.IsFaulted)
            {
                IsComplete = true;
                Message = task.Result;
            }
        }

        public async Task<string> TryGetBody()
        {
            if (Message?.Content == null) return String.Empty;
            try { return await Message.Content.ReadAsStringAsync() ?? string.Empty; }
            catch (Exception) { return string.Empty; }
        }

        public void Dispose() => Message?.Dispose();
    }
}
