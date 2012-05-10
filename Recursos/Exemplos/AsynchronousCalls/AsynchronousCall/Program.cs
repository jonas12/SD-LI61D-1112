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

        delegate void DelSetValue(int newvalue);
        delegate int DelAdd(int a, int b);

        static void Main()
        {
            
            HttpChannel ch = new HttpChannel();
            ChannelServices.RegisterChannel(ch,false);
            INumber robj = (INumber)Activator.GetObject(
                typeof(INumber),
                "http://localhost:1234/RemoteNumber.soap");
            Console.WriteLine("Client.Main(): Referencia para objecto remoto adquirida");

            DateTime start = System.DateTime.Now;
            Console.WriteLine("Client.Main(): Vai colocar novo valor=42");
            
            // Invoca��o Ass�ncrona do m�todo setValue()
            DelSetValue svdel = new DelSetValue(robj.setValue);
            IAsyncResult svasyncres = svdel.BeginInvoke(42, null, null);
            // Invoca��o Ass�ncrona do m�todo add()
            DelAdd adddel = new DelAdd(robj.add);
            IAsyncResult addsyncres = adddel.BeginInvoke(42,100,null, null);

            // Continua a realizar Ac��es

            // Obter os resultados da invoca��o ass�ncrona
            Console.WriteLine("Client.Main(): Obter resultado do SetValue()");
            svdel.EndInvoke(svasyncres);
            Console.WriteLine("Client.Main(): Obter resultado de add()");
            int soma = adddel.EndInvoke(addsyncres);
            Console.WriteLine("Client.Main(): soma: {0}", soma);

            Console.WriteLine("Client.Main(): Vai ler o valor");
            int tmp = robj.getValue();
            Console.WriteLine("Client.Main(): Novo valor no server: {0}", tmp);

            DateTime end = System.DateTime.Now;
            TimeSpan texec = end.Subtract(start);
            Console.WriteLine("Client.Main(): Tempo de Execu��o: {0}s:{1}ms", texec.Seconds, texec.Milliseconds);

            
            Console.ReadLine();
        }
    }
}