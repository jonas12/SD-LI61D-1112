using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Description;
using Contratos;
using Service;
namespace HostService
{
    class Program
    {
        static void Main(string[] args)
        {
            Uri addr = new Uri("http://localhost:8080/ServiceOla");
            Type servtype = typeof(ServiceOla);
            //BasicHttpBinding bind = new BasicHttpBinding();
            WSHttpBinding bind = new WSHttpBinding();
            bind.MessageEncoding = WSMessageEncoding.Mtom;
            bind.Security.Mode = SecurityMode.None;

            ServiceHost svchost = new ServiceHost(servtype);

            ServiceMetadataBehavior smb = svchost.Description.Behaviors.Find<ServiceMetadataBehavior>();
            if (smb != null)
            {
                smb.HttpGetEnabled = true;
                smb.HttpGetUrl = addr;

            }
            else
            {
                Console.WriteLine("vai criar o ServiceMetadataBehavior");
                smb = new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;
                smb.HttpGetUrl = addr;
                svchost.Description.Behaviors.Add(smb);
            }


            svchost.AddServiceEndpoint(typeof(IServiceOla), bind, addr);
            svchost.Open();
 
            Console.WriteLine("Service Ola hosted. To close hosting, Press Enter ");
            Console.ReadLine();
            svchost.Close();
            Console.WriteLine("Host closed. Press Enter to exit ");
            Console.ReadLine();
        }
    }
}
