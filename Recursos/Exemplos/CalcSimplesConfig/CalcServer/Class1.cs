using System;
using System.Runtime.Remoting;
namespace CalcServer {
    class RemCalc {
		
        static void Main( ) {
            string configfile = "CalcServer.exe.config";
            RemotingConfiguration.Configure(configfile, false);
        

            Console.WriteLine("Inicio do Server Calc Espera pedidos");
            Console.ReadLine();       
        }
    }
}

