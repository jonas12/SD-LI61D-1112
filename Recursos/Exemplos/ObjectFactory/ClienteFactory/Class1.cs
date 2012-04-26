using System;
using System.Collections;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using InterfaceFactory;

namespace ClienteFactory {

	class ClientRemAlunos	{
		
		static void Main( )	{
         
            //Criar o Canal Http
			HttpChannel ch = new HttpChannel(0);
            // Registar o canal
            ChannelServices.RegisterChannel(ch, false);

            Console.WriteLine("Vai criar Alunos Factory\n");
            IRemAlunoFactory fact = (IRemAlunoFactory) Activator.GetObject(
                                     typeof(IRemAlunoFactory),
                                     "http://localhost:1234/RemoteAlunoFactory.soap");
            Console.WriteLine("Vai criar Aluno sem nome\n");
            IRemAluno maria = fact.GetNewInstanceAluno();
            maria.SetNome("Maria");
            Console.WriteLine("Vai criar Aluno com nome\n");
            IRemAluno jose = fact.GetNewInstanceAluno("jose");

            Console.WriteLine(maria.AlunoHello());
            jose.Nome="mudei o nome ao jose"; Console.WriteLine(jose.AlunoHello());
            Console.ReadLine();
		}
	}
}
