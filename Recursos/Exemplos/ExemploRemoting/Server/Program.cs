using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interface;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting;

namespace Server
{

    public class Myserver : MarshalByRefObject, IRem
    {
        int IRem.add(int a, int b)
        {
            return a + b;
        }

        string IRem.Ola(string str)
        {
            return "Ola " + str;
        }
    }



    class Program
    {
        static void Main(string[] args)
        {

            TcpChannel ch = new TcpChannel(5000);
            ChannelServices.RegisterChannel(ch, false);

            Myserver svc = new Myserver();
            System.Runtime.Remoting.ObjRef objrefWellKnown = RemotingServices.Marshal((MarshalByRefObject)svc, "RemoteServer");

            Console.ReadLine();


        }
    }
}
