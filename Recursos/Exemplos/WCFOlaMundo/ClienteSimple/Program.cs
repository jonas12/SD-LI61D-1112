using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Contratos;

namespace ClienteSimple
{
    class Program
    {
        static void Main(string[] args)
        {
            EndpointAddress addr = new EndpointAddress("http://localhost:8080/ServiceOla");
            //BasicHttpBinding bind = new BasicHttpBinding();
            WSHttpBinding bind = new WSHttpBinding();
            IChannelFactory<IServiceOla> cfact = new ChannelFactory<IServiceOla>(bind);
            IServiceOla prx = cfact.CreateChannel(addr);
           
            Console.WriteLine(prx.olaSimples("Mundo"));
            Pessoa pes = new Pessoa();
            pes.FirstName = "Luis";
            pes.LastName = "Assunção";
            Console.WriteLine(prx.olaPessoa(pes));
            pes = prx.getPessoa("Luis", "Assunção");
            Console.WriteLine(pes.FirstName + " " + pes.LastName);
            //Console.WriteLine("state=" + prx.getState());
            //Console.WriteLine("novo valor?");
            //string line = Console.ReadLine();
            //prx.changeState(int.Parse(line));
            //Console.ReadLine();
            //Console.WriteLine("state="+prx.getState());

            Console.ReadLine();

        }
    }
}
