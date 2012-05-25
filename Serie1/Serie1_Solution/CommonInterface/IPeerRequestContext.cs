using System.Collections.Generic;

namespace CommonInterface
{
    public interface IPeerRequestContext
    {
        int Jumps { get; set; }
        bool CheckAndAdd(int sp);
    }
}