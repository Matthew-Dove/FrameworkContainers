using FrameworkContainers.Network.HttpCollective;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.FrameworkContainers
{
    [TestClass]
    public class TestInitialize
    {
        [AssemblyInitialize]
        public static void Initialize(TestContext _)
        {

        }

        [AssemblyCleanup]
        public static void Cleanup()
        {
            Http.TearDown();
        }
    }
}
