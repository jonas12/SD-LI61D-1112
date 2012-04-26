using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassCalc
{
    public delegate void DelDivisionByZero(int dividendo, int divisor);
    
    public class Calc
    {
        public Calc()
        {
            // nada a fazer
        }

        public event DelDivisionByZero Evtdbz = null;

        public int add(int op1, int op2)
        {
            return op1 + op2;
        }
        public int sub(int op1, int op2)
        {
            return op1 - op2;
        }
        public int mult(int op1, int op2)
        {
            return op1 * op2;
        }
        public int div(int op1, int op2)
        {
            try
            {
                return op1 / op2;
                
            }
            catch (DivideByZeroException e)
            {
                if (Evtdbz != null) Evtdbz(op1, op2);
                throw (new DivideByZeroException("Divisão por Zero !", e));

            }
        }

    }
}
