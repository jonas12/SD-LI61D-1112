﻿using System;
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
        public IDictionary<int,IPeer> OnlinePeers { get; set; }
        public List<Article> Articles { get; set; }

        public Peer()
        {
            Articles = new List<Article>();
            OnlinePeers = new Dictionary<int,IPeer>();
            Id = DateTime.Now.Ticks.GetHashCode();
        }

        public Article GetArticleBy(string title, bool checkPeers)
        {
            Console.WriteLine(Id + " - Getting article - " + title);
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

            IList<KeyValuePair<int, IPeer>> peers = new List<KeyValuePair<int,IPeer>>();

            try
            {
                IPeerListCtx plc = new PeerListCtx();
                IPeerRequestContext ctx = new PeerRequestContext(plc);
                ctx.CheckAndAdd(SuperPeer.Id);//so faz add pois e o inicio da chain de getpeers

                PeerHelpers.ConcatAndReturnDif(ref peers, OnlinePeers, SuperPeer.GetPeers(ctx),this);
            }
            catch (WebException)
            {
                throw new NotRegisteredToSuperPeerException();
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

            return default(Article);
        }

        public void BindToSuperPeer(ISuperPeer sp)
        {
            Console.WriteLine(Id + " - Peer Binding to sp - " + sp.Id);
            SuperPeer = sp;
        }

        public void UnbindFromSuperPeer()
        {
            SuperPeer.UnRegisterPeer(Id);
        }

        public void Ping() { }
    }
}
