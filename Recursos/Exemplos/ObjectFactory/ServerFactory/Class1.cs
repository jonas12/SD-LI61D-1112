using System;
using System.Collections;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using InterfaceFactory;

namespace ServerFactory
{
	class Aluno : MarshalByRefObject, IRemAluno	{
        private string myNome;
        public string Nome { get {return myNome;} set {myNome=value;}}
        public Aluno() {  // construtor por omissão
            Console.WriteLine("Construtor omissão aluno()");
            myNome="Sem Nome";
        }

        public Aluno(string nome) {  // construtor com argumento
            Console.WriteLine("Construtor com nome: Aluno {0}", nome);
            myNome = nome;
        }
        // implementação da Interface IRemAluno
        public void SetNome(string nome) {
          Nome=nome;
        }
        public string AlunoHello() {
            Console.WriteLine("execução do método AlunoHello() {0}",myNome);
            return "Hello " + myNome;
        }
	}

    class RemoteAlunoFactory : MarshalByRefObject, IRemAlunoFactory {

       public RemoteAlunoFactory( ) {
          Console.WriteLine("Construtor do objecto AlunoFactory\n");
       }
       // Implementação da Interface IRemAlunoFactory
       public IRemAluno GetNewInstanceAluno( ) {
          Console.WriteLine("Construtor por omissão do objecto Aluno\n");
          return new Aluno();
       }
        public IRemAluno GetNewInstanceAluno(string nome) {
            Console.WriteLine("Construtor do objecto Aluno com argumento {0}\n",nome);
            return new Aluno(nome);
        }
    }


    class ServerFactoryMain {
        static void Main() {
            //Criar o Canal Http
            HttpChannel ch = new HttpChannel(1234);
            // Registar o canal
            ChannelServices.RegisterChannel(ch, false);

            // Registar o type RemoteAlunoFactory como Server Activated Object (SAO)
            RemotingConfiguration.RegisterWellKnownServiceType(
                typeof(RemoteAlunoFactory),
                "RemoteAlunoFactory.soap",
                //WellKnownObjectMode.SingleCall);
                WellKnownObjectMode.Singleton);
            // Espera pedidos
            Console.WriteLine("Server: Espera pedidos...Prima Enter para terminar\n");

            Console.ReadLine();
        }
    }


}
