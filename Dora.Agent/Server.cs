using Dora.Agent.Handle;
using Dora.Agent.Handle.Camera;
using Dora.Agent.Handle.Win32;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Dora.Agent
{
    internal class Server : BaseHandle
    {
        private static string SubscribeChannel;
        private const string PublushChannel = "Dora.Server";
        private const string RedisConnection = "wdora.com";
        private ConnectionMultiplexer redis;
        private ISubscriber sub;
        private IDatabase db;

        internal void Start()
        {
            SubscribeChannel = $"Dora.Agent.{Guid.NewGuid()}";
            redis = ConnectionMultiplexer.Connect(RedisConnection);
            db = redis.GetDatabase();
            sub = redis.GetSubscriber();
            sub.Subscribe(SubscribeChannel, (c, msg) =>
            {
                var context = JsonConvert.DeserializeObject<Context>(msg);
                ReceiveAsync(context);
            });
        }

        public override async Task SendAsync(Context context)
        {
            var msg = JsonConvert.SerializeObject(context);
            await sub.PublishAsync(PublushChannel, msg);
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