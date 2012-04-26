using System;
using IRemCalc;
namespace ClassCalc {
	
    public class Calc: MarshalByRefObject, ICalc {

        private int val;

        public int Val
        {
            get { return val; }
            set { val = value; }
        }


        public Calc()
        {
            Console.WriteLine("Calc construtor");
        }
     
        public int add(int a, int b) {
            return a + b;
        }
        public int sub(int a, int b) {
            return a - b;
        }
        public int mult(int a, int b) {
            return a * b;
        }
        public float div(int a, int b) {
            try {
               return (float)(a/b);
            } 
            catch (DivideByZeroException e){
                throw (new DivideByZeroException("Divisão por Zero !",e));
            }
        }

    }
}
