using System;
using System.Collections;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using PingPong;
namespace Server
{
    class RemoteServer : MarshalByRefObject, IServer {

         public void FazPingPong(IPingPong pp) {
             if (pp.EstPing)
             {
                 Console.WriteLine("vai fazer Ping");
                 pp.Ping(pp);
             }
             else
             {
                 Console.WriteLine("vai fazer Pong");
                 pp.Pong();
             }
             pp.EstPing=!pp.EstPing;
         }
     }

    class ServerMain {
        static void Main() {
            BinaryServerFormatterSinkProvider serverProv = new BinaryServerFormatterSinkProvider();
            serverProv.TypeFilterLevel = System.Runtime.Serialization.Formatters.TypeFilterLevel.Full;
            BinaryClientFormatterSinkProvider clientProv = new BinaryClientFormatterSinkProvider();

            //SoapServerFormatterSinkProvider serverProv = new SoapServerFormatterSinkProvider();
            //serverProv.TypeFilterLevel = System.Runtime.Serialization.Formatters.TypeFilterLevel.Full;
            //SoapClientFormatterSinkProvider clientProv = new SoapClientFormatterSinkProvider();
            IDictionary props = new Hashtable();
            props["port"] = 1234;
            HttpChannel ch = new HttpChannel(props, clientProv, serverProv);
           //HttpChannel ch = new HttpChannel(1234);
            ChannelServices.RegisterChannel(ch, false);  

            RemotingConfiguration.RegisterWellKnownServiceType(
                typeof(RemoteServer),
                "RemoteServer.soap",
                WellKnownObjectMode.Singleton);
            // Espera pedidos
            Console.WriteLine("Server: Espera pedidos...Prima Enter para terminar\n");

            Console.ReadLine();
        }
    }
}
