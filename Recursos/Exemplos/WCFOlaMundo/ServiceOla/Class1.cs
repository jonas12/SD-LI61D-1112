using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Runtime.Serialization;
using Contratos;

namespace Service
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Single)]
    //[ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    //[ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class ServiceOla : IServiceOla
    {
        private Guid id;
        private string session=null;

        private int state = 0;
        public void changeState(int val) { state = val; }
        public int getState() { return state; }

     public ServiceOla()
     {
        id = Guid.NewGuid();
        Console.WriteLine("Object {0} has been created.", id);
        if (OperationContext.Current == null)
            Console.WriteLine("Context sem sessão");
        else
        {
           session = OperationContext.Current.SessionId;
           Console.WriteLine("For session {0}", session);
        }
     }

        public string olaSimples(string nome)
        {
            return "Ola " + nome;
        }

        public string olaPessoa(Pessoa pes)
        {
            return "Ola " + pes.FirstName + " " + pes.LastName;
        }

        public Pessoa getPessoa(string firstName, string lastName)
        {
            Pessoa pes = new Pessoa();
            pes.FirstName = firstName;  pes.LastName = lastName;
            return pes;
        }

        public string[] teste(string[] str)
        {
            return str;
        }

        public byte[] getDataMTOM(byte[] arg)
        {
            byte[] buf = new byte[arg.Length];
            for (int i = 0; i < arg.Length; i++)
                buf[i] = 0x55; //(byte)(i % 256);
            return buf;
        }

        public int division(int a, int b)
        {
            if (b == 0) throw new DivideByZeroException();
            return a / b;
        }


        ~ServiceOla()
        {
          Console.WriteLine("Object {0} has been destroyed.", id);
          if (session == null)
              Console.WriteLine("Sem Sessão");
          else Console.WriteLine("For session {0}", session);
        }

 
    }
}
