using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

using ConsClient.SD;

namespace ConsClient
{
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class Receiver : IDuplexServiceCallback
    {

        public void newAnnounce(string msg)
        {
            Console.WriteLine(msg);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {

            Receiver rec = new Receiver();
            DuplexServiceClient svc = new DuplexServiceClient(new InstanceContext(rec));
            Console.WriteLine(svc.init());

            for (; ; )
            {
                Console.Write("msg to send?");
                string msg = Console.ReadLine();
                if (string.Compare(msg, "exit") == 0) break;
                svc.SendMsg(msg);
            }
            svc.exit();
        }
    }
}
