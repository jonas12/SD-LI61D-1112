using System;
using System.Runtime.Remoting;
using CommonInterface;

namespace PeerClient
{
    public class PeerMain
    {
        private static readonly string CONFIG_FILE_NAME = "PeerClient.exe.config";

        public static void Main(string[] args)
        {
            Console.ReadLine();
            RemotingConfiguration.Configure(CONFIG_FILE_NAME, false);
            WellKnownClientTypeEntry[] clients = RemotingConfiguration.GetRegisteredWellKnownClientTypes();
            
            ISuperPeer sp = (ISuperPeer) Activator.GetObject(typeof(ISuperPeer), clients[0].ObjectUrl);

            Peer p = new Peer();
            
            Console.WriteLine("Registering in peer");
            
            sp.RegisterPeer(p);

            Console.WriteLine("p.Ping() -> " + p.Ping());

            Console.ReadLine();
        }
    }
}
