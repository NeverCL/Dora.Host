using Dora.Agent.Handle;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dora.Host.Core.Message
{
    public class ServerMessenger : Messenger
    {
        static List<string> agents = new List<string>();

        public static ServerMessenger Instance = new ServerMessenger();

        private ServerMessenger()
        {
            
        }

        public void Start()
        {
            SubscribeAsync(ServerChannel, (c, v) =>
            {
                Handle(v);
            }, false);
        }

        private void Handle(string value)
        {
            var context = JsonConvert.DeserializeObject<Context>(value);
            if (context.MessageType == MessageType.Agent)
            {
                agents.Add(context.FromChannel.ToString());
                return;
            }

            if (context.MessageType == MessageType.Client)
            {
                context.Data = agents;
                var msg = JsonConvert.SerializeObject(context);
                PublishAsync(context.FromChannel, msg);
                return;
            }

            if (context.MessageType == MessageType.ClientMessage)
            {
                PublishAsync(context.ToChannel, value);
                return;
            }

            if (context.MessageType == MessageType.AgentMessage)
            {
                PublishAsync(context.ToChannel, value);
                return;
            }
        }
    }
}
