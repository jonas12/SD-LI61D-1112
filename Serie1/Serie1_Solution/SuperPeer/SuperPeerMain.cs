using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
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
            var url = GetLocalUrl();
            
            Console.WriteLine("URL: "+url);

            ISuperPeer localSP = (ISuperPeer) Activator.GetObject((typeof (ISuperPeer)), url + service[0].ObjectUri);
            localSP.FromXml(FILE_NAME);

            //WellKnownClientTypeEntry[] clients = RemotingConfiguration.GetRegisteredWellKnownClientTypes();
            //ISuperPeer remoteSP = (ISuperPeer)Activator.GetObject(typeof(ISuperPeer), clients[0].ObjectUrl);

            //remoteSP.BindToSuperPeer(localSP);
            //localSP.BindToSuperPeer(remoteSP);

            Console.WriteLine("Waiting for peers");

            Console.ReadLine();
        }

        private static string GetLocalUrl()
        {
            var httpChannel = (HttpChannel) ChannelServices.GetChannel("http");
            var channelDataStore = (ChannelDataStore)(httpChannel).ChannelData;
            return channelDataStore.ChannelUris[0] + "/";
        }
    }
}
