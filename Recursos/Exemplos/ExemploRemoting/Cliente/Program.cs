using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interface;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels;

namespace Cliente
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpChannel ch = new TcpChannel(0);
            ChannelServices.RegisterChannel(ch, false);

            IRem proxy = (IRem)Activator.GetObject(typeof(IRem), "tcp://localhost:5000/RemoteServer");
            Console.WriteLine(proxy.Ola("luis"));

            for (int i = 0; i < 10; i++)
                Console.WriteLine(proxy.add(i, 5));


        }
    }
}
