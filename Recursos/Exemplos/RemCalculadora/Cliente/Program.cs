using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels.Tcp;
using ICalculadora;

namespace Cliente
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                HttpChannel ch = new HttpChannel(0);
                //TcpChannel ch = new TcpChannel(0);
                ChannelServices.RegisterChannel(ch, false);
                ICalc robj = (ICalc)Activator.GetObject(
                                       typeof(ICalc),
                "http://localhost:1234/RemoteCalcServer.soap");

                Console.WriteLine("Vai invokar o metodo getPerson()");
                Person p = robj.getPerson();
                Console.WriteLine("Nome da Pessoa:"+p.nome);
                Console.WriteLine("Resultado da Soma: {0}\n", robj.add(15, 5));
                //Console.ReadLine();
                Console.WriteLine("Resultado da Multiplicação: {0}\n", robj.mult(15, 5));
                Console.WriteLine("valor=" + robj.getValue());
                Console.Write("Introduza um numero inteiro: ");

                string input = Console.ReadLine();
                robj.setValue(int.Parse(input));
                Console.WriteLine("Prima enter");
                Console.ReadLine();
                Console.WriteLine("valor=" + robj.getValue());

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message+"\n"+ex.StackTrace);
            }

            Console.ReadLine();
        }
    }
}
