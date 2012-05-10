using System;
using System.Collections.Generic;
using System.Text;

namespace IRemObject
{
    public interface INumber
    {
        void setValue(int newvalue);
        int getValue();
        int add(int a, int b);

       
    }

    public interface IPix
    {
        byte adjustPixel(byte pix, int x, int y, int z);
    }
}
