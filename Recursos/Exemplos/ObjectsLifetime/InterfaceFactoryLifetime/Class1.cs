using System;
using System.Runtime.Remoting.Lifetime;

namespace InterfaceFactory 
{
	
    public interface IRemAluno 
    {
        void setNome(string nome);
        string alunoHello( );
    }

    public interface IGenericSponsor : ISponsor
    {
         void setNotRenew(); // controla o Sponsor para não renovar mais 
    }

    public interface IRemAlunoFactory 
    {
        IRemAluno getNewInstanceAluno( ); // construtor Aluno por omissão
        IRemAluno getNewInstanceAluno(string nome); // construtor Aluno com argumento
        IGenericSponsor getGenericSponsor();   // Cria um Sponsor server-side
    }
}