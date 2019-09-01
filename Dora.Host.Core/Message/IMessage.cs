using Dora.Agent.Handle;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dora.Host.Core.Message
{
    public interface IMessage
    {
        Task SubscribeAsync(string channel, Action<string, string> action);

        Task PublishAsync(string channel, string message);
    }
}
