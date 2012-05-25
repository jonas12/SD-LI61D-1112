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

                List<IPeer> peers = new List<IPeer>();

                try
                {
                    IPeerListCtx plc = new PeerListCtx();
                    IPeerRequestContext ctx = new PeerRequestContext(plc);
                    ctx.CheckAndAdd(SuperPeer);//so faz add pois e o inicio da chain de getpeers

                    peers = (List<IPeer>)OnlinePeers.Except(SuperPeer.GetPeers(ctx));
                }
                catch (WebException)
                {
                    throw new NotRegisteredToSuperPeerException();
                }

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
            SuperPeer.UnRegisterPeer(this);
        }

        public void Ping() { }
    }
}
