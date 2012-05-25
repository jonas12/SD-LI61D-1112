using System.Collections.Generic;

namespace CommonInterface
{
    public interface IPeer
    {
        List<IPeer> OnlinePeers { get; set; }
        List<Article> Articles { get; set; }
        Article GetArticleBy(string title, bool checkPeers);
        void BindToSuperPeer(ISuperPeer p);
        void UnbindFromSuperPeer();
        void Ping();
    }
}
