using System;

namespace CommonInterface
{
    [Serializable]
    public class PeerRequestContext : IPeerRequestContext
    {
        private IPeerListCtx pl;
        public int Jumps { get; set; }
        public bool CheckAndAdd(ISuperPeer sp)
        {
            return pl.CheckAndAdd(sp);
        }

        public PeerRequestContext(IPeerListCtx plc)
        {
            pl = plc;
            Jumps = 5;
        }
    }
}
