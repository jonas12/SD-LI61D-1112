using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels.Tcp;
using ICalculadora;

namespace Server
{

    class RemoteCalc : MarshalByRefObject, ICalc
    {
        private object mylock = new object();
        private object plock = new object();
        private int val=50;

        public void setValue(int x)
        {
            lock (mylock)
            {    //para testar partilha e ver efeito do Lock
                System.Threading.Thread.Sleep(30 * 1000);

                val = x;
            }
        }
        public int getValue()
        {
            lock (mylock)
            {
                return val;
            }
        }


        // Construtor por omissão. Pode ou não fazer sentido
        public RemoteCalc()
        {
            Console.WriteLine("Construtor de RemoteCalc");
        }
        public int add(int op1, int op2)
        {
            return op1 + op2;
        }
        public int mult(int op1, int op2)
        {
            return op1 * op2;
        }

        public Person getPerson()
        {
            lock (plock)
            {
                Person p = new Person();
                p.nome = "Jose";
                p.cod = 1234;
                return p;
            }
          

        }
        
        ~RemoteCalc()
        {
            Console.WriteLine("Destrutor de RemoteCalc");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Inicio do CalcServer");
            HttpChannel ch = new HttpChannel(1234);
            //TcpChannel ch = new TcpChannel(1234);
            ChannelServices.RegisterChannel(ch, false);
            RemotingConfiguration.RegisterWellKnownServiceType(
                typeof(RemoteCalc),
                "RemoteCalcServer.soap",
            //WellKnownObjectMode.SingleCall); // cada pedido é servido por um novo objecto
            WellKnownObjectMode.Singleton); // pedidos servidos pelo mesmo objecto
            
            Console.WriteLine("Espera pedidos");
            Console.ReadLine();
        }
    }
}
