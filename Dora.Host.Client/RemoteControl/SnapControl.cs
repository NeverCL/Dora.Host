using Dora.Agent.Handle;
using Dora.Host.Core.Client;
using Dora.Host.Core.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dora.Host.Client.RemoteControl
{
    public class SnapControl //: IRemoteControl
    {
        public static SnapControl Instance = new SnapControl();

        public SnapControl()
        {

        }

        public async Task SendAsync(Action<Context> action)
        {
            var context = new Context();
            context.FromChannel = context.Id + "";
            context.ToChannel = ClientSetting.Instance.ToChannel;
            context.HandleType = HandleType.Aforge_Camera;
            await ClientMessenger.Instance.SubscribeAsync(context.FromChannel, action);
            await ClientMessenger.Instance.PublishAsync(context);
        }
    }
}
