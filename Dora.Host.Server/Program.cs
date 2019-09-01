using Dora.Host.Core.Message;
using System;

namespace Dora.Host.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting!");
            ServerMessenger.Instance.Start();
            Console.WriteLine("Started!");
            Console.ReadLine();
        }
    }
}
