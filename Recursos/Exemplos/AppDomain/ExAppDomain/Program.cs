using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExAppDomain
{
    class Program
    {
        static void Main(string[] args)
        {
            // Obtem uma referência para o current domain
            AppDomain myDomain = AppDomain.CurrentDomain;
            // Mostra algumas informações sobre o AppDomain
            Console.WriteLine("Informações sobre o AppDomain corrente ...");
            Console.WriteLine("  Hash Code = {0}", myDomain.GetHashCode());
            Console.WriteLine("  Friendly Name = {0}", myDomain.FriendlyName);
            Console.WriteLine("  App Base = {0}", myDomain.BaseDirectory);
           
            // Cria um novo AppDomain e atribui-lhe um nome "Calculadora"
            AppDomain mathDomain = AppDomain.CreateDomain("Calculadora");
            // Indica ao novo Appdomain para executar o Assembly WinCalc.exe.
            // Assume que o Assembly Wincalc.exe e seus dependentes estão na
            // mesma directoria que a desta aplicação.	
		   
            mathDomain.ExecuteAssembly("WinCalc.exe");
            Console.WriteLine("Terminou a aplicação em execução noutro Domínio");
            Console.ReadLine();
        }
    }
}
