using System;
using System.Collections.Generic;
using CommonInterface;

namespace SuperPeerImpl
{
    public class SuperPeer : MarshalByRefObject, ISuperPeer
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

        public IEnumerable<IPeer> Peers
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public IEnumerable<ISuperPeer> SuperPeers
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public void RegisterPeer(IPeer p)
        {
            Console.WriteLine("Peer Registered");
        }

        public void UnRegisterPeer(IPeer p)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IPeer> GetPeers()
        {
            throw new NotImplementedException();
        }
    }
}
