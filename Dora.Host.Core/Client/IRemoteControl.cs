using Dora.Agent.Handle;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dora.Host.Core.Client
{
    public interface IRemoteControl
    {
        Task SendAsync(Context context, Action<Context> action);
    }
}
