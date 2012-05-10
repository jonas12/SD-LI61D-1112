using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels;
using IRemObject;
using System.Threading;

namespace Server
{
    class RemoteNumber : MarshalByRefObject, INumber
    {

        private int number;
        
        public RemoteNumber()
        {
            Console.WriteLine("RemoteNumber.Constructor");
            number = 0;
        }

        public void setValue(int newvalue)
        {
            Console.WriteLine("RemoteNumber.setValue(): old {0} new {1}", number, newvalue);
            // Simulação de processamento lento 
            Console.WriteLine("waiting 5 segundos antes de alterar valor");
            Thread.Sleep(5 * 1000);
            number = newvalue;
            Console.WriteLine("number tem novo valor");
        }
        public int getValue()
        {
            Console.WriteLine("RemoteNumber.getValue(): valor corrente {0}", number);
            return number;
        }

        public int add(int a, int b)
        {
            Console.WriteLine("waiting 5 segundos antes de alterar valor");
            Thread.Sleep(5 * 1000);
            return a + b;
        }
    }

    public class Images : MarshalByRefObject, IPix
    {
        public byte adjustPixel(byte pix, int x, int y, int z)
        {
            return pix;
        }

    }


    class ServerStartup
    {
        static void Main()
        {
            Console.WriteLine("ServerStartup.Main(): Inicio do server");

            HttpChannel ch = new HttpChannel(1234);
            ChannelServices.RegisterChannel(ch,false);

            RemotingConfiguration.RegisterWellKnownServiceType(
                typeof(RemoteNumber),
                "RemoteNumber.soap",
                WellKnownObjectMode.Singleton);
            RemotingConfiguration.RegisterWellKnownServiceType(
                typeof(Images),
                "Images.soap",
                WellKnownObjectMode.Singleton);
            Console.ReadLine();
        }

    }
}