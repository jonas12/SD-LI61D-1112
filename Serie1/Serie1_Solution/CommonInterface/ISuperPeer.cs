using System.Collections.Generic;

namespace CommonInterface
{
    public interface ISuperPeer : IPeer
    {
        List<IPeer> OnlinePeers { get; set; }
        List<ISuperPeer> SuperPeers { get; set; }
        void RegisterPeer(IPeer p);
        void UnRegisterPeer(IPeer p);
        List<IPeer> GetPeers();
    }
}
