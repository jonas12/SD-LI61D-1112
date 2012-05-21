using System;
using System.Collections.Generic;
using System.Linq;
using CommonInterface;
using CommonInterface.Exceptions;
using CommonInterface.Utils;

namespace FormPeer
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

            title = title.ToLower();

            Article article = Articles.Find(a => a.Title.ToLower().Equals(title));
            
            if(article.IsDefault())
            {
                if (OnlinePeers.Any(p => !(article = p.GetArticleBy(title)).IsDefault()))
                {
                    return article;
                }

                // Call GetPeers()
            }

            return article;
        }

        public void BindToSuperPeer(ISuperPeer p)
        {
            p.RegisterPeer(p);
            SuperPeer = p;
        }

        public void UnbindFromSuperPeer()
        {
            SuperPeer.UnRegisterPeer(this);
        }

        public void Ping()
        {
            throw new NotImplementedException();
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }
    }
}
