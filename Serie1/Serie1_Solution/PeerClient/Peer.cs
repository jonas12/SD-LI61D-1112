using System;
using System.Collections.Generic;
using System.Linq;
using CommonInterface;
using CommonInterface.Exceptions;
using CommonInterface.Utils;

namespace PeerClient
{
    public class Peer : MarshalByRefObject, IPeer
    {
        public ISuperPeer SuperPeer { get; set; }

        public Peer()
        {
            Articles = new List<Article>(); 
            OnlinePeers = new List<IPeer>();
        }

        public List<IPeer> OnlinePeers { get; set; }

        public List<Article> Articles { get; set; }

        public Article GetArticleBy(string title)
        {
            if(string.IsNullOrEmpty(title))
            {
                throw new EmptyTitleException();
            }

            if(SuperPeer == null)
            {
                throw new NotRegisteredToSuperPeerException();
            }

            title = title.ToLower();

            Article article = Articles.Find(a => a.Title.ToLower().Equals(title));
            
            if(article.IsDefault())
            {
                if (OnlinePeers.Any(p => !(article = p.GetArticleBy(title)).IsDefault()))
                {
                    return article;
                }

                List<IPeer> peers = (List<IPeer>) OnlinePeers.Except(SuperPeer.GetPeers());

                OnlinePeers.AddRange(peers);

                foreach (IPeer p in peers)
                {
                    article = p.GetArticleBy(title);

                    if (!article.IsDefault())
                        return article;
                }
            }

            return article;
        }

        public void BindToSuperPeer(ISuperPeer p)
        {
            SuperPeer = p;
        }
    }
}
