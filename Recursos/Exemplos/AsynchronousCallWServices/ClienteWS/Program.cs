using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClienteWS.SD;
using System.ComponentModel;

namespace ClienteWS
{
    class Program
    {
        public static void workDoneCallback(object sender, AsyncCompletedEventArgs args) { 
            
        
            if (args.Error != null)
                Console.WriteLine("\n"+args.Error.Message+"\n");
            else Console.WriteLine("State from caller:"+args.UserState.ToString());
            
        }

        public static void workDoneCallbackWithReturn(object sender, HelloWorldWithReturnCompletedEventArgs args) {

            if (args.Error != null)
                Console.WriteLine("\n"+args.Error.Message+"\n");
            else Console.WriteLine("Result:"+args.Result + ":State from caller:"+(string)args.UserState);
        }
        

        static void Main(string[] args)
        {
            Service prx = new Service();
           
           


            prx.HelloWorldCompleted += new HelloWorldCompletedEventHandler(workDoneCallback); 
            prx.HelloWorldWithReturnCompleted += new HelloWorldWithReturnCompletedEventHandler(workDoneCallbackWithReturn);
            prx.HelloWorldAsync(20,"Luis ","async call: param= 20");
            prx.HelloWorldWithReturnAsync(50, "maria", "async call com retorno");

            for (int i=0; i < 10; i++)
                prx.HelloWorldAsync(i, "Luis ", i);

            for (int i = 0; i < 20; i++)
            {
                Console.WriteLine("estou livre para trabalhar");
                System.Threading.Thread.Sleep(1 *1000);
            }

            Console.ReadLine();
            
        }
    }
}
