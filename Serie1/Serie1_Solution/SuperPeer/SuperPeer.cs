using System;
using System.Collections;
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
        public int Id { get; private set; }
        public List<Article> Articles { get; set; }
        public IDictionary<int, IPeer> OnlinePeers { get; set; }
        public IDictionary<int, ISuperPeer> SuperPeers { get; set; }
        public IDictionary<int, IPeer> RegisteredPeers { get; set; }

        public SuperPeer()
        {
            Articles = new List<Article>();
            OnlinePeers = new Dictionary<int, IPeer>();
            SuperPeers = new Dictionary<int, ISuperPeer>();
            RegisteredPeers = new Dictionary<int, IPeer>();
            Id = DateTime.Now.Ticks.GetHashCode();
            this.FromXml("Articles.xml");
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

            foreach (KeyValuePair<int, IPeer> p in OnlinePeers)
            {
                try
                {
                    article = p.Value.GetArticleBy(title, false);

                    if (!article.IsDefault())
                        return article;
                }
                catch (WebException)
                {
                    OnlinePeers.Remove(p.Key);
                }
            }

            if (!checkPeers)
                return default(Article);

            List<KeyValuePair<int, IPeer>> peers = null;

            IPeerListCtx plc = new PeerListCtx();
            IPeerRequestContext ctx = new PeerRequestContext(plc);
            ctx.CheckAndAdd(this.Id);//so faz add pois e o inicio da chain de getpeers

            // KeyValuePair para se poder remover caso o proxy tenha sido fechado
            foreach (KeyValuePair<int, ISuperPeer> sp in SuperPeers)
            {
                try
                {
                    ctx.CheckAndAdd(sp.Key);
                    peers = PeerHelpers.ConcatAndReturnDif(OnlinePeers, sp.Value.GetPeers(ctx),Id);
                }
                catch (WebException)
                {
                    SuperPeers.Remove(sp.Key);
                }

                if (peers != null)
                {
                    foreach (KeyValuePair<int, IPeer> p in peers)
                    {
                        try
                        {
                            article = p.Value.GetArticleBy(title, false);

                            if (!article.IsDefault())
                                return article;
                        }
                        catch (WebException)
                        {
                            OnlinePeers.Remove(p.Key);
                        }
                    }
                }
            }

            return default(Article);
        }

        public void RegisterPeer(IPeer p)
        {
            int id = 0;
            bool added = false;
            try
            {
                id = p.Id;
                Console.WriteLine("Peer Register - " + id + " sp -" + this.Id);
                if (RegisteredPeers.ContainsKey(id))
                {
                    throw new PeerAlreadyRegisteredException();
                }

                RegisteredPeers.Add(id, p);
                added = true;
                p.BindToSuperPeer(this);
            }
            catch (WebException)
            {
                if (added)
                    RegisteredPeers.Remove(id);
            }
        }

        public void UnRegisterPeer(int pId)
        {
            Console.WriteLine(Id + " - Peer Unregister - " + pId);
            if (!RegisteredPeers.Remove(pId))
            {
                throw new PeerNotFoundException();
            }
        }

        public void ShowPeers()
        {
            foreach (var registeredPeer in RegisteredPeers)
            {
                Console.WriteLine(registeredPeer.Key+" - IsAlive? - "+registeredPeer.Value.IsAlive());
            }
        }

        public void BindToSuperPeer(ISuperPeer p)
        {
            int id = 0;
            bool added = false;
            try
            {
                id = p.Id;
                Console.WriteLine("SPBinding - " + id + " sp -" + this.Id);
                if (SuperPeers.ContainsKey(id))
                {
                    throw new PeerAlreadyRegisteredException();
                }

                SuperPeers.Add(id, p);
                added = true;
                p.BindToSuperPeer(this);
            }
            catch (WebException)
            {
                if (added)
                    SuperPeers.Remove(id);
            }
        }

        public void UnbindFromSuperPeer()
        {
            throw new NotImplementedException();
        }

        public void Ping() { Console.WriteLine(Id + " - SP ping"); }

        public IList<IPeer> GetPeers(IPeerRequestContext ctx)
        {
            Console.WriteLine(Id+" - GettingPeers");
            if (ctx.Jumps <= 0)
            {
                return new List<IPeer>();
            }
            ctx.Jumps -= 1;
            List<int> toRemove = new List<int>();
            List<IPeer> list = new List<IPeer> {this};
            foreach (var peer in RegisteredPeers)
            {
                if (!peer.Value.IsAlive())
                    toRemove.Add(peer.Key);
                else
                    list.Add(peer.Value);
            }

            foreach (var i in toRemove)
            {
                RegisteredPeers.Remove(i);
            }

            toRemove = new List<int>();

            foreach (var sp in SuperPeers)
            {
                try
                {
                    ctx.CheckAndAdd(sp.Key);
                    list.AddRange(sp.Value.GetPeers(ctx));
                }
                catch (WebException)
                {
                    toRemove.Add(sp.Key);
                }
            }

            foreach (var i in toRemove)
            {
                SuperPeers.Remove(i);
            }

            return list;
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }
    }
}
