using Dora.Agent.Handle;
using Dora.Agent.Handle.Camera;
using Dora.Agent.Handle.Win32;
using Dora.Host.Core.Message;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Dora.Agent
{
    internal class Server : BaseHandle
    {
        private readonly IMessage message;

        public Server() : this(new Messenger(MessengerType.Agent))
        {

        }

        public Server(IMessage message)
        {
            this.message = message;
        }

        internal void Start()
        {
            message.SubscribeAsync((c, msg) =>
            {
                var context = JsonConvert.DeserializeObject<Context>(msg);
                ReceiveAsync(context);
            });
        }

        public override async Task SendAsync(Context context)
        {
            var msg = JsonConvert.SerializeObject(context);
            await message.PublishAsync(msg);
        }

        public override async Task ReceiveAsync(Context context)
        {
            IHandle handle;
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