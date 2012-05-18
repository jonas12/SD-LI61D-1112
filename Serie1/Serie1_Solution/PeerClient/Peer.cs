using System;
using System.Collections.Generic;
using CommonInterface;

namespace PeerClient
{
    public class Peer : MarshalByRefObject, IPeer
    {
        public Peer()
        {
            Articles = new List<Article>();    
        }

        public List<Article> Articles { get; set; }

        public Article GetArticleBy(string title)
        {
            throw new NotImplementedException();
        }
    }
}
