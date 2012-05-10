using System;
using System.Runtime.Remoting;

using IRemCalc;
using System.Collections;
namespace CalcCliente
{

    class Cliente
    {

        static void Main()
        {
            string configfile = "Client.exe.config";
            RemotingConfiguration.Configure(configfile, false);

            WellKnownClientTypeEntry[] entries = RemotingConfiguration.GetRegisteredWellKnownClientTypes();
            Console.WriteLine(entries[0].TypeName + " " + entries[0].ObjectType + " " + entries[0].ObjectUrl);

            ICalc robj = (ICalc)Activator.GetObject(entries[0].ObjectType, entries[0].ObjectUrl);

            if (RemotingServices.IsTransparentProxy(robj)) Console.WriteLine("robj é remoto");

            try
            {
                Console.WriteLine("5+8={0}", robj.add(5, 8));
                Console.WriteLine("4*7={0}", robj.mult(4, 7));
             
                Console.WriteLine("5/2={0}", robj.div(5, 0));
            }
            catch (DivideByZeroException e)
            {
                Console.WriteLine("Divisão por zero: {0}", e.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unhandled Exception: {0}", ex.Message);
            }
            Console.ReadLine();
        }
    }
}

