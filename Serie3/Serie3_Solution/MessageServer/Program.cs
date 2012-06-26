using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace MessageServer
{
    class Program
    {
        static void Main(string[] args)
        {
            using(var sHost = new ServiceHost(typeof(CentralService)))
            {
                sHost.Open();
                Console.WriteLine("service started at {0}", sHost.BaseAddresses[0]);
                Console.ReadLine();
            }
        }
    }
}
