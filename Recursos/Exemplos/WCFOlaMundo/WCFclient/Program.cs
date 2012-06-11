using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WCFclient.ServiceReference1;

namespace WCFclient
{
    class Program
    {
        static void Main(string[] args)
        {
            //IServiceOla prx = new ServiceOlaClient("WSHttpBinding_IServiceOla", "http://localhost:8081/ServiceOla");
            IServiceOla prx = new ServiceOlaClient();
            byte[] arg = new byte[1024];
            for (int j = 0; j < 1024; j++) arg[j] = 0x55;
            byte[] buf = prx.getDataMTOM(arg);
            foreach (byte b in buf)
                Console.Write("{0:x} ", b);
            Console.WriteLine();



            Pessoa p = prx.getPessoa("Luis", "Assunção");
            Console.WriteLine("Resposta:" + p.FirstName + " " + p.LastName);

            Console.WriteLine("state=" + prx.getState());
            Console.WriteLine("novo valor?");
            string line = Console.ReadLine();
            prx.changeState(int.Parse(line));
            Console.ReadLine();
            Console.WriteLine("state=" + prx.getState());
            Console.ReadLine();
        }
    }
}
