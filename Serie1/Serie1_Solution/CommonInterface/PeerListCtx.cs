using System;
using System.Collections.Generic;

namespace CommonInterface
{
    public class PeerListCtx : MarshalByRefObject, IPeerListCtx
    {
        private Object locker;
        private IList<int> spK;

        public PeerListCtx()
        {
            spK = new List<int>();
            locker = new Object();
        }
        public bool CheckAndAdd(int sp)
        {
            lock (locker)
            {
                if (spK.Contains(sp))
                    return false;
                spK.Add(sp);
                return true;
            }
        }
    }
}
