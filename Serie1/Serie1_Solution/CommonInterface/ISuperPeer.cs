using System.Collections;
using System.Collections.Generic;

namespace CommonInterface
{
    public interface ISuperPeer : IPeer
    {
        IDictionary<int,ISuperPeer> SuperPeers { get; set; }
        IDictionary<int, IPeer> RegisteredPeers { get; set; }
        IList<IPeer> GetPeers(IPeerRequestContext ctx);
        void RegisterPeer(IPeer p);
        void UnRegisterPeer(int pId);
        void ShowPeers();
    }
}
