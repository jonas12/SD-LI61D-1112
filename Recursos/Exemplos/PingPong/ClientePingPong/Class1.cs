using System;
using System.Collections;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using PingPong;
namespace ClientePingPong
{
    class PingPongImpl : MarshalByRefObject, IPingPong {
        private bool estping;
        public bool EstPing {get {return estping;} set {estping=value;}}
        public void Ping(IPingPong myself) {
            
            Console.WriteLine("PING"+myself.EstPing);
            
        }
        public void Pong() {
            Console.WriteLine("PONG");
        }
    }

    class Cliente	{
		
        static void Main( )	{

            BinaryServerFormatterSinkProvider serverProv = new BinaryServerFormatterSinkProvider();
            serverProv.TypeFilterLevel = System.Runtime.Serialization.Formatters.TypeFilterLevel.Full;
            BinaryClientFormatterSinkProvider clientProv = new BinaryClientFormatterSinkProvider();

            //////SoapServerFormatterSinkProvider serverProv = new SoapServerFormatterSinkProvider();
            //////serverProv.TypeFilterLevel = System.Runtime.Serialization.Formatters.TypeFilterLevel.Full;
            //////SoapClientFormatterSinkProvider clientProv = new SoapClientFormatterSinkProvider();
            IDictionary props = new Hashtable();
            props["port"] = 0;
            HttpChannel ch = new HttpChannel(props, clientProv, serverProv);
            //HttpChannel ch = new HttpChannel(0);
            ChannelServices.RegisterChannel(ch, false);
            IServer svc = (IServer)Activator.GetObject(
                typeof(IServer),
                "http://localhost:1234/RemoteServer.soap");


            //RemotingConfiguration.Configure("ClientePingPong.exe.config", false);
            //WellKnownClientTypeEntry[] entries =
            //    RemotingConfiguration.GetRegisteredWellKnownClientTypes();
            //IServer svc = (IServer)Activator.GetObject(
            //    entries[0].ObjectType,
            //    entries[0].ObjectUrl); 



            //Console.ReadLine();
            IPingPong pp = new PingPongImpl(); pp.EstPing=true;
            try
            {
                for (int i = 0; i < 10; i++)
                    svc.FazPingPong(pp);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message+"\n"+ex.StackTrace);
            }
            Console.WriteLine("Prima Enter");
            Console.ReadLine();
        }
    }
}
