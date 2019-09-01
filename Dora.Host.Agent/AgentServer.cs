using Dora.Agent.Handle;
using Dora.Host.Agent.Handle.Camera;
using Dora.Host.Agent.Handle.Win32;
using Dora.Host.Core.Agent;
using Dora.Host.Core.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dora.Host.Agent
{
    public class AgentServer : BaseHandle
    {
        private readonly AgentMessenger message;

        public AgentServer() : this(AgentMessenger.Instance)
        {

        }

        public AgentServer(AgentMessenger message)
        {
            this.message = message;
        }

        internal async Task Start()
        {
            await message.SubscribeAsync(async context => await ReceiveAsync(context));
            await SendAsync(new Context { MessageType = MessageType.Agent, FromChannel = AgentMessenger.AgentChannel });
        }

        public override async Task SendAsync(Context context)
        {
            await message.PublishAsync(context);
        }

        public override async Task ReceiveAsync(Context context)
        {
            IAgentHandle handle;
            switch (context.HandleType)
            {
                case HandleType.Win32_CloseScreen:
                    handle = new Win32Handle();
                    break;
                case HandleType.Aforge_Camera:
                    handle = new CameraHandle();
                    break;
                default:
                    handle = null;
                    break;
            }
            await handle.ReceiveAsync(context);
            await handle.SendAsync(context);
        }
    }
}
