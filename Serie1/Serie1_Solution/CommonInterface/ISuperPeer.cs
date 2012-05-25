using System.Collections.Generic;

namespace CommonInterface
{
    public interface ISuperPeer : IPeer
    {
        List<ISuperPeer> SuperPeers { get; set; }
        List<IPeer> RegisteredPeers { get; set; }
        void RegisterPeer(IPeer p);
        void UnRegisterPeer(IPeer p);
        List<IPeer> GetPeers(IPeerRequestContext ctx);
    }
}
