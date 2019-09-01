using Dora.Agent.Handle;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dora.Host.Core.Message
{
    public abstract class Messenger //: IMessage
    {
        private const string RedisConnection = "wdora.com";
        private string AgentChannel = $"Dora.Agent.{Guid.NewGuid()}";
        private readonly MessengerType messengerType;
        private ConnectionMultiplexer redis;
        private readonly ISubscriber sub;
        public const string ServerChannel = "Dora.Server";

        //public static Messenger AgentInstance = new Messenger(MessengerType.Agent);
        //public static Messenger ClientInstance = new Messenger(MessengerType.Client);

        public Messenger()
        {
            redis = ConnectionMultiplexer.Connect(RedisConnection);
            sub = redis.GetSubscriber();
        }

        //public Task SubscribeAsync(Action<string, Context> action, bool isForget = true)
        //{
        //    var channel = messengerType == MessengerType.Agent ? AgentChannel : ServerChannel;
        //    return sub.SubscribeAsync(channel, (c, m) =>
        //    {
        //        var context = JsonConvert.DeserializeObject<Context>(m);
        //        action(c, context);
        //        if (isForget)
        //            sub.Unsubscribe(channel);
        //    });
        //}

        //public Task PublishAsync(Context message)
        //{
        //    var channel = messengerType == MessengerType.Agent ? ServerChannel : AgentChannel;
        //    var msg = JsonConvert.SerializeObject(message);
        //    return sub.PublishAsync(channel, msg);
        //}

        public Task SubscribeAsync(string channel, Action<string, string> handle, bool isForget = true)
        {
            return sub.SubscribeAsync(channel, (c, v) =>
            {
                handle(c, v);
                if (isForget)
                    sub.Unsubscribe(channel);
            });
        }

        public Task PublishAsync(string channel, string message)
        {
            return sub.PublishAsync(channel, message);
        }
    }

    public enum MessengerType
    {
        Agent,
        Server,
        Client
    }
}
