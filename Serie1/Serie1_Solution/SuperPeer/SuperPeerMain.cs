using System;
using System.Runtime.Remoting;
using CommonInterface;
using CommonInterface.Utils;

namespace SuperPeerClient
{
    public class SuperPeerMain
    {
        private static readonly string CONFIG_FILE_NAME = "SuperPeerClient.exe.config";
        private static readonly string FILE_NAME = "Articles.xml";

        public static void Main(string[] args)
        {
            RemotingConfiguration.Configure(CONFIG_FILE_NAME, false);
            WellKnownServiceTypeEntry[] service = RemotingConfiguration.GetRegisteredWellKnownServiceTypes();
            
            Console.WriteLine("Waiting for peers");

            Console.ReadLine();
        }
    }
}
