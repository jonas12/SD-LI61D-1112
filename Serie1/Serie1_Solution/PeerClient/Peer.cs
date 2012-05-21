using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
            
            if(SuperPeer == null || !SuperPeer.IsAlive())
            {
                throw new NotRegisteredToSuperPeerException();
            }

            title = title.ToLower();

            Article article = Articles.Find(a => a.Title.ToLower().Equals(title));
            
            if(article.IsDefault())
            {
                foreach (IPeer p in OnlinePeers)
                {
                    try
                    {
                        article = p.GetArticleBy(title);

                        if (!article.IsDefault())
                            return article;
                    }
                    catch (WebException)
                    {
                        OnlinePeers.Remove(p);
                    }
                }

                List<IPeer> peers = (List<IPeer>) OnlinePeers.Except(SuperPeer.GetPeers());

                OnlinePeers.AddRange(peers);

                foreach (IPeer p in peers)
                {
                    try
                    {
                        article = p.GetArticleBy(title);

                        if (!article.IsDefault())
                            return article;
                    }
                    catch (WebException)
                    {
                        OnlinePeers.Remove(p);
                    }
                }

                return default(Article);
            }

            return article;
        }

        public void BindToSuperPeer(ISuperPeer p)
        {
            SuperPeer = p;
        }

        public void UnbindFromSuperPeer()
        {
            throw new NotImplementedException();
        }

        public void Ping() { }
    }
}
