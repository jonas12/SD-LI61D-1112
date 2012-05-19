using System;
using System.Runtime.Remoting;
using CommonInterface;
using CommonInterface.Utils;

namespace PeerClient
{
    public class PeerMain
    {
        private static readonly string CONFIG_FILE_NAME = "PeerClient.exe.config";
        private static readonly string FILE_NAME = "Articles.xml";

        public static void Main(string[] args)
        {
            RemotingConfiguration.Configure(CONFIG_FILE_NAME, false);
            WellKnownClientTypeEntry[] clients = RemotingConfiguration.GetRegisteredWellKnownClientTypes();
            
            ISuperPeer sp = (ISuperPeer) Activator.GetObject(typeof(ISuperPeer), clients[0].ObjectUrl);

            Peer p = new Peer();
            
            Console.WriteLine("Registering in peer");
            
            sp.RegisterPeer(p);
            Peer p1 = new Peer();

            Article a = new Article();

            a.Title = "A book 1";
            a.Authors = new []{"José Casimiro"};
            a.PublishYear = 2012;
            a.Summary = "A book about something";

            p1.Articles.Add(a);

            a.Title = "A book 2";
            a.Authors = new[] { "José Casimiro" };
            a.PublishYear = 2012;
            a.Summary = "A book about something";

            p1.Articles.Add(a);

            a.Title = "A book 3";
            a.Authors = new[] { "José Casimiro" };
            a.PublishYear = 2012;
            a.Summary = "A book about something";

            p1.Articles.Add(a);
            p1.ToXml(FILE_NAME);

            Peer p2 = new Peer();
            p2.FromXml(FILE_NAME);

            Console.ReadLine();
        }
    }
}
