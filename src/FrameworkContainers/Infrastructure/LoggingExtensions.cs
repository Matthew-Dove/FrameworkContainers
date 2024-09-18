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
            SetLoggers(logger);

            return sp;
        }

        private static void SetLoggers(ILogger logger)
        {
            Trace.SetFormattedLogger(logger.LogInformation);
            Try.SetFormattedExceptionLogger((x, y, z) => SetErrorLogger(logger, x, y, z));
        }

        private static void SetErrorLogger(ILogger logger, Exception ex, string message, object[] args)
        {
            logger.LogError(ex, message, args);
            var metadata = (string)ex.Data[Try.DataKey];
            if (metadata != null) logger.LogError("{ErrorMetaData}", metadata);
        }
    }
}
