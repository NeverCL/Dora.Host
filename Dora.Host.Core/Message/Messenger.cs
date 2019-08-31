using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dora.Host.Core.Message
{
    public class Messenger : IMessage
    {
        private const string RedisConnection = "wdora.com";
        private string AgentChannel = $"Dora.Agent.{Guid.NewGuid()}";
        private const string ServerChannel = "Dora.Server";
        private readonly MessengerType messengerType;
        private ConnectionMultiplexer redis;
        private readonly ISubscriber sub;

        public Messenger(MessengerType messengerType)
        {
            this.messengerType = messengerType;
            redis = ConnectionMultiplexer.Connect(RedisConnection);
            sub = redis.GetSubscriber();
        }

        public Task PublishAsync(string message)
        {
            var channel = messengerType == MessengerType.Agent ? ServerChannel : AgentChannel;
            return sub.PublishAsync(channel, message);
        }

        public Task SubscribeAsync(Action<string, string> action)
        {
            var channel = messengerType == MessengerType.Agent ? AgentChannel : ServerChannel;
            return sub.SubscribeAsync(channel, (c, m) => action(c, m));
            //sub.SubscribeAsync(channel, action);
        }
    }

    public enum MessengerType
    {
        Agent,
        Server
    }
}
