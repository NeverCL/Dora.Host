using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dora.Agent.Handle;
using Newtonsoft.Json;

namespace Dora.Host.Core.Message
{
    public class ClientMessenger : Messenger
    {
        public static ClientMessenger Instance = new ClientMessenger();

        private ClientMessenger()
        {

        }

        public Task PublishAsync(Context context)
        {
            context.MessageType = MessageType.Client;
            var message = JsonConvert.SerializeObject(context);
            return PublishAsync(ServerChannel, message);
        }

        public Task SubscribeAsync(string channel, Action<Context> action)
        {
            return base.SubscribeAsync(channel, (c, v) =>
            {
                var _ = JsonConvert.DeserializeObject<Context>(v);
                action(_);
            });
        }
    }
}
