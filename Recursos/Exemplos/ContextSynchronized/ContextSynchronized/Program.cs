using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Remoting.Contexts; // Synchronization attribute
using System.Runtime.Remoting;          // RemotingServices class
using System.Threading;

namespace ContextSynchronized
{
    // Uma classe com requisitos de context bound.
    [Synchronization]
    public class MyContextBoundClass : ContextBoundObject
    {
        public void method(MyAgileClass acobj)
        {
            // recebe um objecto agile context como argumento
            Context ctx = Thread.CurrentContext;
            Console.WriteLine("Ctx Bound Method: ContextID do objecto MyContextBoundClass: {0}", ctx.ContextID);
            Console.WriteLine("Ctx Bound Method: vai invocar o método no objecto Agile recebido como argumento");
            Console.WriteLine("Resposta de AgClMeth:"+acobj.AgClMeth("ola"));
          
        }
    }
    // Uma classe context agile
    public class MyAgileClass
    {
        
        public MyAgileClass()
        {
            Console.WriteLine("Construtor de Agile Class");
        }
        public string AgClMeth(string str)
        {
            Console.WriteLine("AgClMeth arg:" + str);
            Context ctx = Thread.CurrentContext;
            Console.WriteLine("AgClMeth: ContextID do objecto MyAgileClass: {0}", ctx.ContextID);
            return str.ToUpper();
        }
    }    

    class Program
    {
        static void Main(string[] args)
        {
            MyContextBoundClass myBound = new MyContextBoundClass();
            MyAgileClass myAgile = new MyAgileClass();

            // Em que contexto estão ?
            Console.WriteLine("mybound está fora do contexto corrente ? {0}",
                 RemotingServices.IsObjectOutOfContext(myBound)); // True
            Console.WriteLine("myAgile está fora do contexto corrente ? {0}",
                 RemotingServices.IsObjectOutOfContext(myAgile)); // False
            Console.WriteLine();
            // São referência directa ou proxies ?
            Console.WriteLine("\nmyBound é um proxy? {0}",
                 RemotingServices.IsTransparentProxy(myBound)); //True
            Console.WriteLine("myAgile é um proxy? {0}",
                RemotingServices.IsTransparentProxy(myAgile)); // False
            Console.WriteLine();
       
            string str = myAgile.AgClMeth("ole"); Console.WriteLine("Resposta de AgClMeth:"+str);
            myBound.method(myAgile);
           
        }
    }
}
