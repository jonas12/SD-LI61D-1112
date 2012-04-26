using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

// More information: http://www.albahari.com/threading/part2.aspx


namespace Locks
{

    class SomeClass
    {
        private int state = 0;
        private object mylock = new object();
        Thread t = null;
        public SomeClass()
	    {
           t = new Thread(new ThreadStart(ThreadCode));			
		   t.Start();
	     }

        public int getState() { return state; }

        public void ThreadCode()
        {
            for (; ; )
            {
                //verificar a diferença quando se usa lock(this)
                lock (mylock)
                {
                    Thread.Sleep(2*1000);
                    Console.WriteLine("Thread of SomeClass is executing");
                    state++;
                    
                }
            }
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            SomeClass sc = new SomeClass();


            lock (sc)
            {
                // do some work
			    Thread.Sleep( 10*1000 );
                Console.WriteLine(sc.getState());

            }
            Console.WriteLine("Out of lock");
            Console.ReadLine();
        }
    }
}
