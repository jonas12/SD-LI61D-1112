using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels.Tcp;
using InterfaceFactory;
using System.Threading;
using System.Collections;
using System.Runtime.Remoting.Lifetime;
using System.Security.Permissions;


namespace ClienteFactory
{
    public class MySponsor: MarshalByRefObject, IGenericSponsor {

        public override object InitializeLifetimeService()
        {
            return null; // sponsor com tempo de vida infinito
        }

        public bool IsForRenew = true;

        public void setNotRenew() { IsForRenew = false; }

        public TimeSpan Renewal(ILease lease) {
           Console.WriteLine("{0}: Sponsor chamado",DateTime.Now);
           if (IsForRenew) {
               Console.WriteLine("{0}: vai renovar 10 seg.",DateTime.Now);
               return TimeSpan.FromSeconds(10);
           } else {
              Console.WriteLine("{0}: Não renova mais.",DateTime.Now);
              return TimeSpan.Zero;
           }
        }
    } // end class MySponsor

    
    class ClientRemAlunos	
    {
       
        static void Main(string[] args)
        {
            SoapServerFormatterSinkProvider serverProv = new SoapServerFormatterSinkProvider();
            serverProv.TypeFilterLevel = System.Runtime.Serialization.Formatters.TypeFilterLevel.Full;
            SoapClientFormatterSinkProvider clientProv = new SoapClientFormatterSinkProvider(); 
        
            IDictionary props = new Hashtable();
            props["port"] = 0;      
            HttpChannel ch = new HttpChannel(props,clientProv,serverProv);
            // Se usar TcpChannel não esqueça de mudar o URL de activação para "tcp://localhost......"
            //TcpChannel ch = new TcpChannel(props, clientProv, serverProv);
            ChannelServices.RegisterChannel(ch,false); 

            Console.WriteLine("Vai criar Alunos Factory\n");
            IRemAlunoFactory fact = (IRemAlunoFactory) Activator.GetObject(
                typeof(IRemAlunoFactory),
                "http://localhost:1234/RemoteAlunoFactory.soap");
                        
            Console.WriteLine("Vai criar Aluno com nome\n");
            IRemAluno jose = fact.getNewInstanceAluno("jose");

            //Console.ReadLine();
            Console.WriteLine("{0}: Criação do objecto com sponsor.", DateTime.Now);
            //MySponsor sponsorjose = new MySponsor();  // Sponsor client-side !!! problemas nalgumas máquinas
            //IGenericSponsor sponsorjose = fact.getGenericSponsor(); // Sponsor server-side
            //ILease leasejose = (ILease)RemotingServices.GetLifetimeService((MarshalByRefObject)jose);
            //leasejose.Register(sponsorjose);
               
            try {
                  for (int i=0;i<10;i++) {
                     Console.WriteLine(i+"> "+jose.alunoHello());
                     //if (i == 7) sponsorjose.setNotRenew();
                     Thread.Sleep((i+1)*2000);
                  }
            } catch (Exception e) {
                Console.WriteLine("Fim de vida do objecto Aluno:{0}",e.Message); 
            }
            
            Console.ReadLine();
            //leasejose.Unregister(sponsorjose);
        }
    }
}
