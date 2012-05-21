using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
                        UnRegisterPeer(p);
                    }
                }

                List<IPeer> peers = new List<IPeer>();

                foreach (ISuperPeer sp in SuperPeers)
                {
                    try
                    {
                        peers = (List<IPeer>)OnlinePeers.Except(sp.GetPeers());
                    }
                    catch (WebException)
                    {
                        SuperPeers.Remove(sp);
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
                }

                return default(Article);
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

        public void Ping(){}

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
