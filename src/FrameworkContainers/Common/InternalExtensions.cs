using System;
using System.Threading;
using System.Threading.Tasks;

namespace FrameworkContainers.Common
{
    internal static class InternalExtensions
    {
        public static Task<TNewResult> ContinueWithSync<TResult, TNewResult>(this Task<TResult> task, Func<Task<TResult>, TNewResult> continuationFunction)
        {
            return task.ContinueWith(continuationFunction, CancellationToken.None, TaskContinuationOptions.ExecuteSynchronously | TaskContinuationOptions.DenyChildAttach, TaskScheduler.Default);
        }
    }
}
