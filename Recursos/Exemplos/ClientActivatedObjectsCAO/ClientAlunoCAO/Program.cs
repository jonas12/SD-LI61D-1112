using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels.Tcp;
using Alunos;
using System.Collections;
using System.Runtime.Remoting.Proxies;
namespace ClientAluno
{
    class ClientMain
    {
        
        static void Main()
        {

            HttpChannel ch = new HttpChannel(0);
            ChannelServices.RegisterChannel(ch,false);

            Console.WriteLine("vai registar o type Aluno");
            RemotingConfiguration.RegisterActivatedClientType(
                   typeof(Aluno),
                   "http://localhost:1234"
                //ou caso se especifique o nome do serviço
                //"http://localhost:1234/ServerCAO"
               );

           Aluno maria = new Aluno("Maria");
            Console.WriteLine("vai criar Aluno Jose");
            Aluno jose = new Aluno();
            Console.WriteLine("objecto aluno jose é transparente proxy ? {0}",
                                    RemotingServices.IsTransparentProxy(jose));
            Console.WriteLine("criou aluno jose");
            Console.WriteLine(maria.AlunoHello());
            Console.WriteLine(jose.AlunoHello() == null ?"null":"deve ter retornado hello");
            jose.Nome = "José";
            Console.WriteLine(jose.AlunoHello());
            Console.ReadLine();
        }
    }
}
