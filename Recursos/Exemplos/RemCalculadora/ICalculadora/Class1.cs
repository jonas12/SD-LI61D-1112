using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICalculadora
{
    
    [Serializable]
    public class Person
    {
        public string nome;
        public int cod;

    }
    public interface ICalc
    {
        int add(int a, int b);
        int mult(int a, int b);

        //para testes de diferenciar Singleton versus SingleCall
        void setValue(int x);
        int getValue();
        // para ilustrar que na interface podem ser referidas
        // outras classes Data Transfer Object (DTO) que fazem
        // parte do assembly partilhado entre cliente e servidor
        Person getPerson();
        
    }

}
