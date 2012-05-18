using System;
using System.Runtime.Remoting;

namespace SuperPeerClient
{
    public class SuperPeerMain
    {
        private static readonly string CONFIG_FILE_NAME = "SuperPeerClient.exe.config";

        public static void Main(string[] args)
        {
            RemotingConfiguration.Configure(CONFIG_FILE_NAME,false);
            Console.WriteLine("Waiting for peers");
            Console.ReadLine();
        }
    }
}
