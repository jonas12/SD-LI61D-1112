using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interface
{
    public interface ICalc
    {
        int IntValue { get; set; }
        int add(int a, int b);
        int mult(int a, int b);
    }
}
