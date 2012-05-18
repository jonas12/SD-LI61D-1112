using System.Collections.Generic;

namespace CommonInterface
{
    public interface ISuperPeer : IPeer
    {
        IEnumerable<IPeer> Peers { get; set; }
        IEnumerable<ISuperPeer> SuperPeers { get; set; }
        void RegisterPeer(IPeer p);
        void UnRegisterPeer(IPeer p);
        IEnumerable<IPeer> GetPeers();
    }
}
