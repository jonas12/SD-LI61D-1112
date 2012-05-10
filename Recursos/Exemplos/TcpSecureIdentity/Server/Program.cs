using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Messaging;

using System.Security;
using System.Security.Principal;
using System.Security.Permissions;

namespace Server
{ 
    public class MyRemoteObject : MarshalByRefObject, Interface.IRemObject
    {
        
        public string forAll(string msg) {
            Console.WriteLine();
            Console.WriteLine("Execução sem verificar Identitidade");
            return "Remote Object answer:"+msg.ToUpper();
        }

        public string only4Admins(string msg)
        {
            Console.WriteLine();
            Console.WriteLine("Execução com verificação de Identitidade");
            Console.WriteLine("Identidade Corrente: {0}",WindowsIdentity.GetCurrent().Name);
            IPrincipal cliRem = System.Threading.Thread.CurrentPrincipal;   //Cliente Remoto
            Console.WriteLine("Identidade Cliente Remoto: {0}",cliRem.Identity.Name);
            WindowsPrincipal wprinc = new WindowsPrincipal((WindowsIdentity)cliRem.Identity);
            if (!wprinc.IsInRole(@"BUILTIN\Administrators"))
                throw new Exception("Não tem privilegio de Administrator!");
            else
            {
                Console.WriteLine("Remote user is Administrator");
                return "Remote object answer: ola Administrator " + msg;
            }
        }      
    }

    public class RemoteServer
    {
        static void Main(string[] args)
        {
            try
            {
                System.Console.WriteLine("Configuring server...");
                System.Runtime.Remoting.RemotingConfiguration.Configure("Server.exe.config",true);
                System.Console.WriteLine("Server configured, waiting for requests...");
                System.Console.ReadLine();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Error while configuring server: " + ex.Message);
                System.Console.ReadLine();
            }
        }
    }
}
