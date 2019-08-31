using AForge.Video;
using AForge.Video.DirectShow;
using Dora.Host.Core.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dora.Agent
{
    class Program
    {
        static void Main(string[] args)
        {
            new Server().Start();
        }
    }
}
