using System;
using System.Collections.Generic;
using System.Text;
using ClienteWithProxy.Aula;

namespace ClienteWithProxy
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceOla prx = new ServiceOla();


            Console.WriteLine(prx.olaSimples("Mundo"));
            Pessoa pes = new Pessoa(); pes.FirstName = "Luis"; pes.LastName = "Assun��o";
            Console.WriteLine(prx.olaPessoa(pes));
            pes = prx.getPessoa("Luis", "Assun��o");
            Console.WriteLine(pes.FirstName + " " + pes.LastName);
            Console.ReadLine();
            

        }
    }
}
