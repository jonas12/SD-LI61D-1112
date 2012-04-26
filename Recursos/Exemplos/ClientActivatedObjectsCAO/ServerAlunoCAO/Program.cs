using System;
using System.Runtime.Remoting; 
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels.Tcp;
using Alunos;
using System.Collections;
namespace ServerAluno
{
    class ServerMain
    {
        static void Main()
        { 

            HttpChannel ch = new HttpChannel(1234);
            // Registar o canal
            ChannelServices.RegisterChannel(ch,false);

            // Registar nome do servidor
            //RemotingConfiguration.ApplicationName="ServerCAO";
            // Registar o type Aluno como Client Activated Object (CAO)
            RemotingConfiguration.RegisterActivatedServiceType(
                      typeof(Aluno)
            ); 
            
            // Espera pedidos
            Console.WriteLine("Server: Espera pedidos...Prima Enter para terminar\n");

            Console.ReadLine();
        }
    }
}
