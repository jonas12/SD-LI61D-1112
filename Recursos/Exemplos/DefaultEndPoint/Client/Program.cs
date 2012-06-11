using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Client.ServiceReference1;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            IHelloService svc = new HelloServiceClient();
            Console.WriteLine(svc.SayHello("luis"));
            Console.ReadLine();
        }
    }
}
