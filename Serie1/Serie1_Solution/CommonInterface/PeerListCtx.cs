using System;
using System.Collections.Generic;

namespace CommonInterface
{
    public class PeerListCtx : MarshalByRefObject, IPeerListCtx
    {
        private Object locker;
        private IList<String> pl;

        public PeerListCtx()
        {
            pl = new List<String>();
            locker = new Object();
        }
        public bool CheckAndAdd(ISuperPeer sp)
        {
            return false;
        }
    }
}
