using System;
using System.Runtime.Remoting;
using ClassCalc;
//using IRemCalc;
namespace CalcCliente {
	
    class Cliente	{
		
        static void Main( )	{
            string configfile = "CalcCliente.exe.config";
          RemotingConfiguration.Configure(configfile,false);
            
          //WellKnownClientTypeEntry[] entries=RemotingConfiguration.GetRegisteredWellKnownClientTypes();
          //Console.WriteLine(entries[0].TypeName+" "+entries[0].ObjectType+" "+entries[0].ObjectUrl);
          //ICalc robj = (ICalc)Activator.GetObject(entries[0].ObjectType,entries[0].ObjectUrl);
            
            Calc robj = new Calc(); // Caso se partilhasse a classe com implementação
            if (RemotingServices.IsTransparentProxy(robj))
                Console.WriteLine("robj é remoto"); 
            Console.WriteLine("5+8={0}",robj.add(5,8));
            //ICalc robj2 = (ICalc)Activator.GetObject(typeof(ICalc), entries[0].ObjectUrl);
            //if (RemotingServices.IsTransparentProxy(robj2))
            //    Console.WriteLine("robj2 é remoto");
            //Console.WriteLine("(5+8)*2={0}", robj2.mult(robj.add(5, 8), 2));
            
            try {
                Console.WriteLine("5/2={0}",robj.div(5,0));
            } catch (DivideByZeroException e){
                Console.WriteLine("Divisão por zero: {0}",e.Message);
            }
            for (int i = 0; i < 100; i++)
            {
                robj.Val = i;
                Console.WriteLine(robj.add(4,7));
                System.Threading.Thread.Sleep(5* 1000);
                Console.WriteLine("Val="+robj.Val);
            }

            Console.ReadLine();
        }
    }
}

