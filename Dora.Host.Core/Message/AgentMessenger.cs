using Dora.Agent.Handle;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dora.Host.Core.Message
{
    public class AgentMessenger : Messenger
    {
        public static AgentMessenger Instance = new AgentMessenger();

        public static string AgentChannel { get; } = $"Dora.Agent.{Guid.NewGuid()}";

        private AgentMessenger()
        {

        }

        public Task SubscribeAsync(Action<Context> handle)
        {
            return base.SubscribeAsync(AgentChannel, (c, v) =>
            {
                var context = JsonConvert.DeserializeObject<Context>(v);
                handle(context);
            });
        }

        public Task PublishAsync(Context context)
        {
            context.ToChannel = ServerChannel;
            var message = JsonConvert.SerializeObject(context);
            return base.PublishAsync(context.ToChannel, message);
        }
    }
}
