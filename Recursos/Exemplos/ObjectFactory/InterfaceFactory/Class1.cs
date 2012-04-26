using System;

namespace InterfaceFactory {
	
	public interface IRemAluno {
         string Nome { get; set; }
         void SetNome(string nome);
         string AlunoHello( );
	}
    
    public interface IRemAlunoFactory {
       IRemAluno GetNewInstanceAluno( ); // construtor por defeito
       IRemAluno GetNewInstanceAluno(string nome); // construtor com argumento
    }
}
