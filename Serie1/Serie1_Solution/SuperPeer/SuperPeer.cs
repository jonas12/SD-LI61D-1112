using System;
using System.Collections.Generic;
using System.Linq;
using CommonInterface;
using CommonInterface.Exceptions;
using CommonInterface.Utils;

namespace SuperPeerClient
{
    public class SuperPeer : MarshalByRefObject, ISuperPeer
    {
        public SuperPeer()
        {
            Articles = new List<Article>();
            OnlinePeers = new List<IPeer>();
            SuperPeers = new List<ISuperPeer>();
        }

        public List<Article> Articles { get; set; }

        public Article GetArticleBy(string title)
        {
            if(title == null)
            {
                throw new ArgumentNullException();
            }

            title = title.ToLower();

            Article article = Articles.Find(a => a.Title.ToLower().Equals(title));
            
            if(article.IsDefault())
            {
                if (OnlinePeers.Any(p => !(article = p.GetArticleBy(title)).IsDefault()))
                {
                    return article;
                }

                foreach (ISuperPeer sp in SuperPeers)
                {
                    
                }
            }

            return article;
        }

        public List<IPeer> OnlinePeers { get; set; }

        public List<ISuperPeer> SuperPeers { get; set; }

        public void RegisterPeer(IPeer p)
        {
            if (OnlinePeers.Contains(p))
            {
                throw new PeerAlreadyRegisteredException();
            }

            p.
            OnlinePeers.Add(p);
        }

        public void UnRegisterPeer(IPeer p)
        {
            if(!OnlinePeers.Remove(p))
            {
                throw new PeerNotFoundException();
            }
        }

        public void BindToSuperPeer(ISuperPeer p)
        {
            SuperPeers.Add(p);
        }

        public List<IPeer> GetPeers()
        {
            List<IPeer> peers = new List<IPeer>();

            peers.AddRange(OnlinePeers);

            foreach (SuperPeer superPeer in SuperPeers)
            {
               peers.AddRange(superPeer.GetPeers());
            }

            return peers;
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }
    }
}
