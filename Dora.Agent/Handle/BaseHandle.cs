using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dora.Agent.Handle
{
    public class BaseHandle : IHandle
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
