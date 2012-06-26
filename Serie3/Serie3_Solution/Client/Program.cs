using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.ServiceModel;
using System.Text;
using Contracts;
using Client.ServiceReference1;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ReadLine();

            var cs = new CentralServiceClient();
            
            Console.WriteLine(cs.Register(@"http://localhost:8000/CService/"));
            ICService s = new CService();
            InstanceContext ctx = new InstanceContext(s);
            ctx.Open();
            
            //using(var s = new ServiceHost(typeof(CService)))
            //{
            //    var cs = new CentralServiceClient();
            //    s.Open();
            //    Console.WriteLine(cs.Register(s.BaseAddresses[0].ToString()));
            //}
            Console.ReadLine();
        }
    }
}
