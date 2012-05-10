using System;
using System.Collections.Generic;
using System.Text;

namespace Interface
{
    public interface IRemObject
    {
        string forAll(string msg);
        string only4Admins(string msg);
    }
}
