using System;
using System.ServiceModel;

namespace ClientDummy
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var s = new ServiceHost(typeof(CService)))
            {
                s.Open();
                Console.WriteLine("service started at {0}", s.BaseAddresses[0]);
                Console.ReadLine();
            }
        }
    }
}
