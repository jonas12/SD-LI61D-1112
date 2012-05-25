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
        public int Id { get; private set; }
        public ISuperPeer SuperPeer { get; set; }

        public Peer()
        {
            Articles = new List<Article>();
            OnlinePeers = new List<IPeer>();
            Id = DateTime.Now.Ticks.GetHashCode();
        }

        public List<IPeer> OnlinePeers { get; set; }

        public List<Article> Articles { get; set; }

        public Article GetArticleBy(string title, bool checkPeers)
        {
            if (string.IsNullOrEmpty(title))
            {
                throw new EmptyTitleException();
            }

            if (SuperPeer == null || !SuperPeer.IsAlive())
            {
                throw new NotRegisteredToSuperPeerException();
            }

            title = title.ToLower();

            Article article = Articles.Find(a => a.Title.ToLower().Equals(title));

            if (!article.IsDefault())
                return article;

            foreach (IPeer p in OnlinePeers)
            {
                try
                {
                    article = p.GetArticleBy(title, false);

                    if (!article.IsDefault())
                        return article;
                }
                catch (WebException)
                {
                    OnlinePeers.Remove(p);
                }
            }
            if (!checkPeers)
                return default(Article);

            List<IPeer> peers;

            try
            {
                IPeerListCtx plc = new PeerListCtx();
                IPeerRequestContext ctx = new PeerRequestContext(plc);
                ctx.CheckAndAdd(SuperPeer);//so faz add pois e o inicio da chain de getpeers

                peers = SuperPeer.GetPeers(ctx);
                peers = OnlinePeers.Intersect(peers).Except(peers).ToList();
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
                    article = p.GetArticleBy(title, false);

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
