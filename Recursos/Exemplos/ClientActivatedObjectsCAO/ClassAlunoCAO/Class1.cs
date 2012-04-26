using System;

namespace Alunos
{
    public class Aluno : MarshalByRefObject
    {
        private string myNome;
        public string Nome { get { return myNome; } set { myNome = value; } }
        public Aluno()
        {
            Console.WriteLine("Default Constructor()");
            myNome = "Sem Nome";
        }

        public Aluno(string nome)
        {
            Console.WriteLine("Construtor com nome: Aluno {0}", nome);
            myNome = nome;
        }

        public string AlunoHello()
        {
            Console.WriteLine("execução do método AlunoHello() {0}", myNome);
            return "Hello " + myNome;
        }

        
    }
}
