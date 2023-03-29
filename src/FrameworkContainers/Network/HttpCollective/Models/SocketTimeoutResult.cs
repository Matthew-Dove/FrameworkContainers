using System.Threading.Tasks;

namespace FrameworkContainers.Network.HttpCollective.Models
{
    internal readonly struct SocketTimeoutResult
    {
        public bool IsComplete { get; }
        public string Body { get; }
        public bool IsTimeout { get; }
        public bool IsFaulted { get; }

        public SocketTimeoutResult(Task<string> task)
        {
            IsComplete = false;
            Body = null;
            IsTimeout = task.IsCanceled;
            IsFaulted = task.IsFaulted;

            if (!task.IsCanceled && !task.IsFaulted)
            {
                IsComplete = true;
                Body = task.Result;
            }
        }
    }
}
