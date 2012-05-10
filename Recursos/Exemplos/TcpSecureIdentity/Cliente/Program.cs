using System;
using System.Collections.Generic;
using System.Text;
using Interface;
using System.Runtime.Remoting;


namespace Cliente
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
               System.Runtime.Remoting.RemotingConfiguration.Configure("Cliente.exe.config", true);

                  //IRemObject robj = (IRemObject) Activator.GetObject(
                  //  typeof(IRemObject),
                  //  "tcp://localhost:9002/RemObject.rem");
               WellKnownClientTypeEntry[] entries = RemotingConfiguration.GetRegisteredWellKnownClientTypes();
               IRemObject robj = (IRemObject)Activator.GetObject(entries[0].ObjectType, entries[0].ObjectUrl);

                Console.WriteLine(robj.forAll("luis"));
                Console.WriteLine(robj.only4Admins("luis"));

                System.Console.ReadLine();
             }
            catch (Exception ex)
            {
                System.Console.WriteLine("Exception: " + ex.Message);
                System.Console.ReadLine();
            }

        }
    }
}
