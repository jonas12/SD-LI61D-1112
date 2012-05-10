using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels;
using IRemObject;
using System.Threading;

namespace Client
{

    class Client
    {

        static void Main()
        {
            

            HttpChannel ch = new HttpChannel();
            ChannelServices.RegisterChannel(ch,false);
            INumber robj = (INumber)Activator.GetObject(
                typeof(INumber),
                "http://localhost:1234/RemoteNumber.soap");
            Console.WriteLine("Client.Main(): Referencia para objecto remoto adquirida");

            Console.WriteLine("Client.Main(): Vai colocar novo valor=42");
            DateTime start = System.DateTime.Now;
            robj.setValue(42);
            int soma = robj.add(42, 100);
            Console.WriteLine("Client.Main(): Soma: {0}", soma);
            Console.WriteLine("Client.Main(): Vai ler o valor");
            int tmp = robj.getValue();
            Console.WriteLine("Client.Main(): Novo valor no server: {0}", tmp);
            DateTime end = System.DateTime.Now;
            TimeSpan texec = end.Subtract(start);
            Console.WriteLine("Client.Main(): Tempo de Execução: {0}s:{1}ms", texec.Seconds, texec.Milliseconds);
            
            Console.ReadLine();
        }
    }
}
