using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dora.Host.Agent
{
    class Program
    {
        static void Main(string[] args)
        {
            new AgentServer().Start().Wait();
            Console.WriteLine("Start Agent!");
            Console.ReadLine();
        }
    }
}
