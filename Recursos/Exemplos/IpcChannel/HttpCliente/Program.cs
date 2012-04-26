using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using IRemObject;

namespace HttpCliente
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Configuração do Http Channel...");
                HttpChannel ch = new HttpChannel(0);
          
                ChannelServices.RegisterChannel(ch, false);

                Console.WriteLine("Obter proxy para remote object...");
                IRemOla robj = (IRemOla)Activator.GetObject(
                     typeof(IRemObject.IRemOla),
                     "http://localhost:1235/RemObject.rem");

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
