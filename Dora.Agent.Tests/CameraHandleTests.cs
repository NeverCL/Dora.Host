using System;
using System.Threading.Tasks;
using Dora.Agent.Handle;
using Dora.Agent.Handle.Camera;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dora.Agent.Tests
{
    [TestClass]
    public class CameraHandleTests
    {
        [TestMethod]
        public async Task TestReceive()
        {
            await new CameraHandle().ReceiveAsync(new Context { Data = 0 });
        }
    }
}
