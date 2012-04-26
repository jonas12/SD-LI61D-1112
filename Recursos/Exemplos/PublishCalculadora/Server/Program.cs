using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels.Tcp;
using Interface;

namespace Server
{
    class CalcServer : MarshalByRefObject, ICalc
    {
        public CalcServer() { }
        public CalcServer(int initValue)
        {
            IntValue = initValue;
        }

        private int intvalue;
        public int IntValue
        {
            get
            {
                return intvalue;
            }
            set
            {
                intvalue = value;
            }
        }

        public int add(int a, int b)
        {
            return a+b;
        }

        public int mult(int a, int b)
        {
            return a * b;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Inicio do CalcServer com Publicação Objecto");
            HttpChannel ch = new HttpChannel(1234);
            //TcpChannel ch = new TcpChannel(1234);
            ChannelServices.RegisterChannel(ch, false);

            ICalc svc = new CalcServer(200);

            ObjRef objrefWellKnown = RemotingServices.Marshal((MarshalByRefObject)svc, "RemoteServer.soap");

            // aqui  servidor pode continuar a usar o objecto svc, por exemplo monitorizando o seu estado
            while (true)
            {
                Console.WriteLine("intValue=" + svc.IntValue);
                System.Threading.Thread.Sleep(3 * 1000);

            }

            //Console.ReadLine();  
        }
    }
}
