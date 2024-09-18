using ContainerExpressions.Containers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace FrameworkContainers.Infrastructure
{
    public static class LoggingExtensions
    {
        public static IServiceProvider AddContainerExpressionsLogging(this IServiceProvider sp)
        {
            var factory = sp.GetService<ILoggerFactory>();
            if (factory is null) return sp;

            var logger = factory.CreateLogger("ContainerExpressions");
            SetLogger(logger);

            return sp;
        }

        private static void SetLogger(ILogger logger)
        {
            Trace.SetFormattedLogger(logger.LogInformation);
            Try.SetFormattedExceptionLogger(logger.LogError);
        }
    }
}
