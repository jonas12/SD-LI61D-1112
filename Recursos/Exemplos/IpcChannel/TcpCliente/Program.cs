using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using IRemObject;

namespace TcpCliente
{
    class Program
    {
        static void Main(string[] args)
        {
            try {
           
                Console.WriteLine("Configuração do Tcp Channel...");
                TcpChannel ch = new TcpChannel(0);
          
                ChannelServices.RegisterChannel(ch, false);

                Console.WriteLine("Obter proxy para remote object...");
                IRemOla robj = (IRemOla)Activator.GetObject(
                     typeof(IRemObject.IRemOla),
                     "tcp://localhost:1234/RemObject.rem");

                Console.Write("Qual o seu nome? ");
                string nome= Console.ReadLine();
                Console.WriteLine(robj.ola(nome));

                
            }catch (Exception ex)  {
                Console.WriteLine("General Exception: " + ex.Message);
            }
            Console.ReadLine();
        }


      }
}
