using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels.Tcp;
using Interface;

namespace cliente
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpChannel channel = new HttpChannel(0);
            ChannelServices.RegisterChannel(channel, false);
            ICalc robj = (ICalc)Activator.GetObject(
                typeof(ICalc),
                "http://localhost:1234/RemoteServer.soap");
            Console.WriteLine("Client: Reference to remote Server acquired");

           Console.WriteLine("Client: Original server side value: {0}", robj.IntValue);
            Console.WriteLine("Client: set value to 42");
            robj.IntValue=40;
            Console.WriteLine("Client: New server side value {0}", robj.IntValue);
          
            Console.WriteLine("Client: mult(5,10)={0}", robj.mult(5,10));

            Console.ReadLine();
        }
    }
}
