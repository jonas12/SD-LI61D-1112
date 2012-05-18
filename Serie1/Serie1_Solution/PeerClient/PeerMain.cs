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
            RemotingConfiguration.Configure(CONFIG_FILE_NAME, false);
            WellKnownClientTypeEntry[] entries = RemotingConfiguration.GetRegisteredWellKnownClientTypes();
            
            ISuperPeer p = (ISuperPeer) Activator.GetObject(typeof(ISuperPeer), entries[0].ObjectUrl);
            
            Console.WriteLine("Registering in peer");

            p.RegisterPeer(null);

            Console.ReadLine();
        }
    }
}
