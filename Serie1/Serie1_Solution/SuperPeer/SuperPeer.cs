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

        public List<Article> Articles { get; set; }
        public List<IPeer> OnlinePeers { get; set; }
        public List<ISuperPeer> SuperPeers { get; set; }
        public List<IPeer> RegisteredPeers { get; set; }

        public SuperPeer()
        {
            Articles = new List<Article>();
            OnlinePeers = new List<IPeer>();
            SuperPeers = new List<ISuperPeer>();
            RegisteredPeers = new List<IPeer>();
        }

        public Article GetArticleBy(string title, bool checkPeers)
        {
            if (title == null)
            {
                throw new ArgumentNullException();
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
                    UnRegisterPeer(p);
                }
            }

            if (!checkPeers)
                return default(Article);

            List<IPeer> peers = new List<IPeer>();

            IPeerListCtx plc = new PeerListCtx();
            IPeerRequestContext ctx = new PeerRequestContext(plc);
            ctx.CheckAndAdd(this);//so faz add pois e o inicio da chain de getpeers

            foreach (ISuperPeer sp in SuperPeers)
            {
                try
                {
                    ctx.CheckAndAdd(sp);
                    peers = peers = sp.GetPeers(ctx);
                    peers = OnlinePeers.Intersect(peers).Except(peers).ToList();
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
                        article = p.GetArticleBy(title, false);

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
            if (!OnlinePeers.Remove(p))
            {
                throw new PeerNotFoundException();
            }
        }

        public void BindToSuperPeer(ISuperPeer p)
        {
            SuperPeers.Add(p);
        }

        public void UnbindFromSuperPeer()
        {
            throw new NotImplementedException();
        }

        public void Ping() { }

        public List<IPeer> GetPeers(IPeerRequestContext ctx)
        {
            if (ctx.Jumps <= 0)
            {
                return new List<IPeer>();
            }
            ctx.Jumps -= 1;

            List<IPeer> peers = new List<IPeer>();

            peers.AddRange(RegisteredPeers);
            peers.Add(this);

            foreach (SuperPeer superPeer in SuperPeers)
            {
                if (ctx.CheckAndAdd(superPeer))
                    peers.AddRange(superPeer.GetPeers(ctx));
            }

            return peers;
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }
    }
}
