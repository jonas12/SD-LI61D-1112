using System;

namespace IRemCalc
{ 
    
    public interface ICalc {

         int add(int a, int b); 
         int sub(int a, int b);
         int mult(int a, int b);
         float div(int a, int b);
         int Val{ get; set; }

    }
}
