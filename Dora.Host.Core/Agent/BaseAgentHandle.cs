using Dora.Agent.Handle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dora.Host.Core.Agent
{
    public class BaseHandle : IAgentHandle
    {
        public virtual Task ReceiveAsync(Context context)
        {
            return Task.CompletedTask;
        }

        public virtual Task SendAsync(Context context)
        {
            return Task.CompletedTask;
        }
    }
}
