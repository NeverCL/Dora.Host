using Dora.Agent.Handle;
using Dora.Agent.Util;
using Dora.Host.Core.Agent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dora.Host.Agent.Handle.Win32
{
    public class Win32Handle : BaseHandle
    {
        public override Task ReceiveAsync(Context context)
        {
            Win32Util.CloseScreen();
            return base.ReceiveAsync(context);
        }
    }
}
