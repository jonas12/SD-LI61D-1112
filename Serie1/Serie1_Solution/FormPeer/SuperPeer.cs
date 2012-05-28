using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using CommonInterface;
using CommonInterface.Exceptions;
using CommonInterface.Utils;
using SuperPeerClient;

namespace FormPeer
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
        }

        public Article GetArticleBy(string title, bool checkPeers)
        {
            OnMethodCalled(this, new MethodCallEventArgs { Name = "GetArticle",Id=this.Id });

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


            IPeerListCtx plc = new PeerListCtx();
            IPeerRequestContext ctx = new PeerRequestContext(plc);
            ctx.CheckAndAdd(this.Id);//so faz add pois e o inicio da chain de getpeers

            IList<KeyValuePair<int, IPeer>> peers = new List<KeyValuePair<int, IPeer>>();

            // KeyValuePair para se poder remover caso o proxy tenha sido fechado
            foreach (KeyValuePair<int, ISuperPeer> sp in SuperPeers)
            {
                try
                {
                    if(ctx.CheckAndAdd(sp.Key))
                        PeerHelpers.ConcatAndReturnDif(ref peers, OnlinePeers, sp.Value.GetPeers(ctx, Id),this);
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
            OnMethodCalled(this, new MethodCallEventArgs{Name = "RegisterPeer", Id = p.Id});
            int pid = 0;
            bool added = false;
            try
            {
                pid = p.Id;
                Console.WriteLine(Id+"- Peer:"+ pid + " Registered.");
                if (RegisteredPeers.ContainsKey(pid))
                {
                    throw new PeerAlreadyRegisteredException();
                }

                RegisteredPeers.Add(pid, p);
                added = true;
            }
            catch (WebException)
            {
                if (added)
                    RegisteredPeers.Remove(pid);
            }
        }

        public void UnRegisterPeer(int pId)
        {
            OnMethodCalled(this, new MethodCallEventArgs { Name = "UnRegisterPeer",Id=pId });
            Console.WriteLine(Id + "- Peer:"+pId+" Unregistered.");
            if (!RegisteredPeers.Remove(pId))
            {
                throw new PeerNotFoundException();
            }
        }

        public void UnbindFromSuperPeer(int id)
        {
            OnMethodCalled(this, new MethodCallEventArgs { Name = "UnbindFromSuperPeer", Id = id });
            SuperPeers.Remove(id);
        }

        public void BindToSuperPeer(ISuperPeer sp)
        {
            
            int spid = 0;
            try
            {
                spid = sp.Id;
                OnMethodCalled(this, new MethodCallEventArgs { Name = "BindToSuperPeer", Id = spid });
                Console.WriteLine(Id+"- SPBind:"+spid);
                if (SuperPeers.ContainsKey(spid))
                {
                    throw new PeerAlreadyRegisteredException();
                }

                SuperPeers.Add(spid, sp);
            }
            catch (WebException)
            {
            }
        }

        /// <summary>
        /// Unbinds ALL of the superpeers.
        /// </summary>
        public void UnbindFromSuperPeer()
        {
            foreach (var superPeer in SuperPeers)
            {
                try{
                    superPeer.Value.UnbindFromSuperPeer(Id);
                }catch(WebException)
                {
                }
            }
            SuperPeers = new Dictionary<int, ISuperPeer>();
        }

        public void Ping() { OnMethodCalled(this, new MethodCallEventArgs { Name = "Ping", Id = this.Id }); }
        public event EventHandler<MethodCallEventArgs> OnMethodCalled;

        public IList<IPeer> GetPeers(IPeerRequestContext ctx, int callerId)
        {
            OnMethodCalled(this, new MethodCallEventArgs { Name = "GetPeers", Id=callerId });
            Thread.Sleep(3000);
            if (ctx.Jumps <= 0)
            {
                return new List<IPeer>();
            }
            ctx.Jumps -= 1;
            List<int> toRemove = new List<int>();
            List<IPeer> list = new List<IPeer> {this};
            foreach (var peer in RegisteredPeers)
            {
                if (peer.Key == callerId) continue;
                
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
                    if(ctx.CheckAndAdd(sp.Key))
                        list.AddRange(sp.Value.GetPeers(ctx, Id));
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
