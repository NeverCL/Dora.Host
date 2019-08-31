using System;
using Dora.Agent.Handle;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dora.Agent.Tests
{
    [TestClass]
    public class CameraHandleTests
    {
        [TestMethod]
        public void TestReceive()
        {
            new CameraHandle().ReceiveAsync(new Context { Data = 0 });
        }
    }
}
