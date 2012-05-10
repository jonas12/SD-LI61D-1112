using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels;
using IRemObject;
using System.Threading;

namespace Client
{

    class Client
    {

        public static DateTime start; 
        delegate void DelSetValue(int newvalue);
        delegate int DelAdd(int a, int b);

        static int resAdd = 0;
        // Async Callback add
        public static void AddCompleted(IAsyncResult ar)
        {
            Console.WriteLine("chamada do callback");
            string estado = (string)ar.AsyncState;
            Console.WriteLine("estado: " + estado);
            AsyncResult ar2 = (AsyncResult)ar;
            DelAdd del = (DelAdd)ar2.AsyncDelegate;
            try
            {

                int res=del.EndInvoke(ar2);
                resAdd = res;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocorreu exception:" + ex.Message);
            }
            DateTime end = System.DateTime.Now;
            TimeSpan texec = end.Subtract(start);
            Console.WriteLine("Client.Main(): Tempo de Execução: {0}s:{1}ms", texec.Seconds, texec.Milliseconds);
        }
        // Async Callback
        public static void setValueCompleted(IAsyncResult ar)
        {
            Console.WriteLine("chamada do callback");
            string estado = (string)ar.AsyncState;
            Console.WriteLine("estado: " + estado);
            AsyncResult ar2 = (AsyncResult)ar;
            DelSetValue del = (DelSetValue)ar2.AsyncDelegate;
            try
            {
               
                del.EndInvoke(ar2);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocorreu exception:"+ex.Message);
            }
            DateTime end = System.DateTime.Now;
            TimeSpan texec = end.Subtract(start);
            Console.WriteLine("Client.Main(): Tempo de Execução: {0}s:{1}ms", texec.Seconds, texec.Milliseconds);
        }

        static void Main()
        {

            HttpChannel ch = new HttpChannel();
            ChannelServices.RegisterChannel(ch,false);
            INumber robj = (INumber)Activator.GetObject(
                typeof(INumber),
                "http://localhost:1234/RemoteNumber.soap");

            Console.WriteLine("Client.Main(): Vai colocar novo valor=100");
            // Invocação Assíncrona do método SetValue
            DelSetValue svdel = new DelSetValue(robj.setValue);

            AsyncCallback mycallback = new AsyncCallback(setValueCompleted);
            start = System.DateTime.Now;
            IAsyncResult svasyncres = svdel.BeginInvoke(100, mycallback, "set value com 100");
            while (!svasyncres.IsCompleted)
            {   // O cliente continua a fazer trabalho até a operação terminar
                Console.WriteLine("espera fim operação");
                Console.WriteLine("novo valor= {0}", robj.getValue());
                Thread.Sleep(1000);
            }
            Console.WriteLine("novo valor= {0}", robj.getValue());


            AsyncCallback myCbadd = new AsyncCallback(AddCompleted);
            DelAdd adddel = new DelAdd(robj.add);
            IAsyncResult svasyncresadd = adddel.BeginInvoke(100, 200, myCbadd, "add 100 com 200");

            while (!svasyncresadd.IsCompleted)
            {   // O cliente continua a fazer trabalho até a operação terminar
                Console.WriteLine("espera fim operação add");
                
                Thread.Sleep(1000);
            }
            Console.WriteLine("RES="+resAdd);


            Console.ReadLine();
        }
    }
}
