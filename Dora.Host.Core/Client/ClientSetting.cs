using System;
using System.Collections.Generic;
using System.Text;

namespace Dora.Host.Core.Client
{
    public class ClientSetting
    {
        public static ClientSetting Instance = new ClientSetting();
        public string ToChannel { get; set; }
    }
}
