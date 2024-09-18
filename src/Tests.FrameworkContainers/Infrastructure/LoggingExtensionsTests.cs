using ContainerExpressions.Containers;
using FrameworkContainers.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Tests.FrameworkContainers.Infrastructure
{
    [TestClass]
    public class LoggingExtensionsTests
    {
        public const string InformationTemplate = "LogInformation @ {DateTime}.";
        public const string ErrorTemplate = "LogError @ {DateTime}.";

        [TestMethod]
        public void MyTestMethod()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddServicesByConvention("Tests.FrameworkContainers");

            services.AddLogging(configure => {
                configure.ClearProviders();
                configure.AddDebug(); // Writes to the Visual Studio debug output window.
            });

            IServiceProvider sp = services.BuildServiceProvider();
            sp.AddContainerExpressionsLogging();

            // Normal service logging.
            var loggingProvider = sp.GetService<IStandardLogging>();
            loggingProvider.LogInformation();
            loggingProvider.LogError();

            // Container Expressions logging.
            420.LogValue(InformationTemplate.WithArgs(DateTime.Now));
            new Exception("Error!").LogError(ErrorTemplate.WithArgs(DateTime.Now));
        }
    }

    public interface IStandardLogging
    {
        void LogInformation();
        void LogError();
    }

    public class StandardLogging : IStandardLogging
    {
        private readonly ILogger<StandardLogging> _log;

        public StandardLogging(ILogger<StandardLogging> log) => _log = log;

        public void LogInformation() => _log.LogInformation(LoggingExtensionsTests.InformationTemplate, DateTime.Now);

        public void LogError() => _log.LogError(new Exception("Error!"), LoggingExtensionsTests.ErrorTemplate, DateTime.Now);
    }
}
