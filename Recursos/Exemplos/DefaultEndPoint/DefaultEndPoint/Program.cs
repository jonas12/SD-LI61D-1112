using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Channels;


namespace DefaultEndPoint
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(HelloService)))
            {
                host.Open();
                Console.WriteLine("service started at {0}", host.BaseAddresses[0]);
                DumpEndpoint(host.Description.Endpoints);
                Console.ReadLine();
            }
        }

        private static void DumpEndpoint(ServiceEndpointCollection endpoints)
        {
            foreach (ServiceEndpoint sep in endpoints)
            {
                Console.Write("Address:{0}\nBinding:{1}\nContract:{2}\n", sep.Address, sep.Binding.Name, sep.Contract);
                Console.WriteLine("Binding Stack:");

                foreach (BindingElement be in sep.Binding.CreateBindingElements())
                {
                    Console.WriteLine(be.ToString());
                }

            }
        }
    }
}
