using FrameworkContainers;
using FrameworkContainers.Log;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Tests.FrameworkContainers
{
    [TestClass]
    public class FrameworkContainerTests
    {
        [TestMethod]
        public void NoError_ValidResponse()
        {
            var response = FrameworkContainer.Log.Error(() => { }, null);

            Assert.IsTrue(response);
        }

        [TestMethod]
        public void Error_InvalidResponse()
        {
            var response = FrameworkContainer.Log.Error(() => { throw new Exception(); }, null);

            Assert.IsFalse(response);
        }

        [TestMethod]
        public void Error_MessageIsPassedToLogger()
        {
            Guid messageA = Guid.NewGuid(), messageB = Guid.Empty;
            Action<Exception, string, object[]> logger = (ex, message, args) => { messageB = Guid.Parse(message); };
            LogContainer.SetErrorLogger(logger);

            var response = FrameworkContainer.Log.Error(() => { throw new Exception(); }, messageA.ToString());

            Assert.IsFalse(response);
            Assert.AreEqual(messageA, messageB);
        }

        [TestMethod]
        public void Error_ArgIsPassedToLogger()
        {
            Guid argA = Guid.NewGuid(), argB = Guid.Empty;
            Action<Exception, string, object[]> logger = (ex, message, args) => { argB = (Guid)args[0]; };
            LogContainer.SetErrorLogger(logger);

            var response = FrameworkContainer.Log.Error(() => { throw new Exception(); }, null, argA);

            Assert.IsFalse(response);
            Assert.AreEqual(argA, argB);
        }

        [TestMethod]
        public void NoError_LoogerIsNotCalled()
        {
            Guid messageA = Guid.NewGuid(), messageB = Guid.Empty, argA = Guid.NewGuid(), argB = Guid.Empty;
            Action<Exception, string, object[]> logger = (ex, message, args) => { messageB = messageA; argA = argB; };
            LogContainer.SetErrorLogger(logger);

            var response = FrameworkContainer.Log.Error(() => { }, null);

            Assert.IsTrue(response);
            Assert.AreNotEqual(messageA, messageB);
            Assert.AreNotEqual(argA, argB);
        }

        [TestMethod]
        public void Error_LoogerIsCalled()
        {
            Guid messageA = Guid.NewGuid(), messageB = Guid.Empty, argA = Guid.NewGuid(), argB = Guid.Empty;
            Action<Exception, string, object[]> logger = (ex, message, args) => { messageB = messageA; argA = argB; };
            LogContainer.SetErrorLogger(logger);

            var response = FrameworkContainer.Log.Error(() => { throw new Exception(); }, null);

            Assert.IsFalse(response);
            Assert.AreEqual(messageA, messageB);
            Assert.AreEqual(argA, argB);
        }
    }
}
