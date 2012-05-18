using System;
using System.Collections.Generic;
using CommonInterface;

namespace PeerImpl
{
    public class Peer : MarshalByRefObject, IPeer
    {
        public IEnumerable<Article> Articles
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public Article GetArticleBy(string title)
        {
            throw new NotImplementedException();
        }
    }
}
