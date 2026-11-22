using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace FrameworkContainers.Network.HttpCollective.Verbs
{
    public readonly struct VerbAwaiter<T> : ICriticalNotifyCompletion
    {
        public bool IsCompleted => _awaiter.IsCompleted;
        private readonly TaskAwaiter<T> _awaiter;

        public VerbAwaiter(Task<T> task) => _awaiter = task.GetAwaiter();

        public void OnCompleted(Action continuation) => _awaiter.OnCompleted(continuation);
        public void UnsafeOnCompleted(Action continuation) => _awaiter.UnsafeOnCompleted(continuation);
        public T GetResult() => _awaiter.GetResult();
    }
}