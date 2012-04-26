using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels.Tcp;
using InterfaceFactory;
using System.Collections;
using System.Runtime.Remoting.Lifetime;
using System.Security.Permissions;
using System.Threading;

namespace ServerFactory
{   
    class Aluno : MarshalByRefObject, IRemAluno	
    {
        private string myNome;
        public string Nome { get {return myNome;} set {myNome=value;}}
        public Aluno() 
        {  // construtor por omissão
            Console.WriteLine("Construtor omissão aluno()");
            myNome="Sem Nome";
        }

        public Aluno(string nome) 
        {  // construtor com argumento
            Console.WriteLine("Construtor com nome: Aluno {0}\n", nome);
            myNome = nome;
        }
        // implementação da Interface IRemAluno
        public void setNome(string nome) 
        {
            Nome=nome;
        }
        
        public string alunoHello() 
        {
            Console.WriteLine("execução do método AlunoHello() {0}",myNome);
            // Comentar as duas linhas seguintes, caso o Lease seja null
            ILease lease =(ILease)this.GetLifetimeService();
            Console.WriteLine("Tempo de Lease corrente {0}\n",lease.CurrentLeaseTime);
            return "Hello " + myNome;
        }
        // redefinição do TTL dos objectos Aluno
        public override object InitializeLifetimeService()
        {
            Console.WriteLine("Chamou InitializeLifetimeService()");
            //return null;
            // Se quiser testar a situação de return null -> TTL infinito
            // deve comentar no método alunoHello() a obtenção do Lease corrente
            ILease lease = (ILease)base.InitializeLifetimeService();
            if (lease.CurrentState == LeaseState.Initial)
            {
                lease.InitialLeaseTime = TimeSpan.FromSeconds(20);
                lease.RenewOnCallTime = TimeSpan.FromSeconds(5);
                //lease.SponsorshipTimeout = TimeSpan.FromSeconds(10);
                lease.SponsorshipTimeout = TimeSpan.Zero; // neste caso não contacta o sponsor
                
            }
            return lease;
            //return null;
        }
    }

    public class GenericSponsor : MarshalByRefObject, IGenericSponsor
    {
        public override object InitializeLifetimeService()
        {
            return null; // sponsor com tempo de vida infinito
        }
        public bool IsForRenew = true;
        public void setNotRenew() { IsForRenew = false; } 
        public TimeSpan Renewal(ILease lease)
        {
            Console.WriteLine("{0}: Sponsor chamado", DateTime.Now);
            if (IsForRenew)
            {
                Console.WriteLine("{0}: vai renovar 10 seg.", DateTime.Now);
                return TimeSpan.FromSeconds(10);
            }
            else
            {
                Console.WriteLine("{0}: Não renova mais.", DateTime.Now);
                return TimeSpan.Zero;
            }
        }
    } // end class GenericSponsor

    class RemoteAlunoFactory :MarshalByRefObject, IRemAlunoFactory 
    {
        public override object InitializeLifetimeService() {   
            ILease lease = (ILease)base.InitializeLifetimeService();
            if (lease.CurrentState == LeaseState.Initial) { 
                lease.InitialLeaseTime=TimeSpan.FromSeconds(10);
                lease.RenewOnCallTime=TimeSpan.FromSeconds(2);
                //lease.SponsorshipTimeout=TimeSpan.FromSeconds(2);
                lease.SponsorshipTimeout=TimeSpan.Zero; // neste caso não contacta o sponsor
                
            }
            return lease;
        }


        public RemoteAlunoFactory( ) 
        {
            Console.WriteLine("Construtor do objecto AlunoFactory\n");
        }
        // Implementação da Interface IRemAlunoFactory
        public IRemAluno getNewInstanceAluno( ) 
        {
            Console.WriteLine("Construtor por defeito do objecto Aluno\n");
            return new Aluno();
        }
        public IRemAluno getNewInstanceAluno(string nome) 
        {
            Console.WriteLine("Construtor do objecto Aluno com argumento {0}\n",nome);
            Aluno al = new Aluno(nome);
            Thread.Sleep(5 * 1000);
            return al;  // new Aluno(nome);
        }

        public IGenericSponsor getGenericSponsor()
        {
            return new GenericSponsor();
        }
    }


    class ServerFactoryMain 
    {
        static void Main() 
        {  
            SoapServerFormatterSinkProvider serverProv = new SoapServerFormatterSinkProvider();
            serverProv.TypeFilterLevel = System.Runtime.Serialization.Formatters.TypeFilterLevel.Full;
            SoapClientFormatterSinkProvider clientProv = new SoapClientFormatterSinkProvider();       
            IDictionary props = new Hashtable();
            props["port"] = 1234;
            HttpChannel ch = new HttpChannel(props,clientProv,serverProv);
            //TcpChannel ch = new TcpChannel(props, clientProv, serverProv);
            ChannelServices.RegisterChannel(ch, false); 
           // Definição do tempo de polling do TTL dos objectos 
           LifetimeServices.LeaseManagerPollTime=TimeSpan.FromSeconds(1);
 
            // Registar o type RemoteAlunoFactory como Server Activated Object (SAO)
            RemotingConfiguration.RegisterWellKnownServiceType(
                typeof(RemoteAlunoFactory),
                "RemoteAlunoFactory.soap",
                WellKnownObjectMode.Singleton);
                //WellKnownObjectMode.SingleCall);
            // Espera pedidos
            Console.WriteLine("Server: Espera pedidos...Prima Enter para terminar\n");

            Console.ReadLine();
        }
    }


}
