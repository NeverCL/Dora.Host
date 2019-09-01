using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dora.Agent.Handle
{
    public class Context
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string FromChannel { get; set; }

        public string ToChannel { get; set; }

        public MessageType MessageType { get; set; }

        public HandleType HandleType { get; set; }

        public object Data { get; set; }
    }

    public enum MessageType
    {
        Client,
        Agent,
        AgentMessage,
        ClientMessage
    }
}
