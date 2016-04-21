using ContainerExpressions.Containers;
using System;

namespace FrameworkContainers.Log
{
    /// <summary>Aggregator for logging related frameworks.</summary>
    public class LogContainer
    {
        /// <summary>Add your own custom error reporter when errors occur.</summary>
        /// <param name="logger">A stateless void function that takes errors, and reports them in anyway it sees fit, the last element in the object array will be the error message.</param>
        public static void SetErrorLogger(Action<Exception, string, object[]> logger) => _logger = logger;
        private static Action<Exception, string, object[]> _logger = null;

        /// <summary>Safely run a void function, and log any errors that happen.</summary>
        /// <param name="func">The function to run.</param>
        /// <param name="message">A message to be passed along to the logger.</param>
        /// <param name="args">runtime values to be passed along to the logger, the runtime error message is appended to the end of this array.</param>
        /// <returns></returns>
        public Response Error(Action func, string message, params object[] args)
        {
            var response = new Response();

            try
            {
                func();
                response = response.AsValid();
            }
            catch (Exception ex)
            {
                Array.Resize(ref args, args == null ? 1 : args.Length + 1);
                args[args.Length - 1] = ex.GetBaseException().Message;
                _logger?.Invoke(ex, message, args);
            }

            return response;
        }
    }
}
