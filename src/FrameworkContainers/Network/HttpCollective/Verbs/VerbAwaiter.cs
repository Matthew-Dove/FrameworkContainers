using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System;

namespace FrameworkContainers.Network.HttpCollective.Verbs
{
    public readonly struct VerbAwaiter<T> : ICriticalNotifyCompletion
    {
        public bool IsCompleted => _task.IsCompleted;
        private readonly Task<T> _task;

        public VerbAwaiter(Task<T> task) => _task = task;

        public void OnCompleted(Action continuation) => _task.ContinueWith(_ => continuation());
        public void UnsafeOnCompleted(Action continuation) => OnCompleted(continuation);
        public T GetResult() => _task.GetAwaiter().GetResult();
    }
}
