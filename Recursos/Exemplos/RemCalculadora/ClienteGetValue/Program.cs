using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels.Tcp;
using ICalculadora;

namespace ClienteGetValue
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                HttpChannel ch = new HttpChannel(0);
                //TcpChannel ch = new TcpChannel(0);
                ChannelServices.RegisterChannel(ch, false);
                ICalc robj = (ICalc)Activator.GetObject(
                                       typeof(ICalc),
                "http://localhost:1234/RemoteCalcServer.soap");

                for (int i = 0; i < 100; i++)
                {
                    
                    Console.WriteLine("valor=" + robj.getValue());
                    System.Threading.Thread.Sleep(1 * 1000);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
            }

            Console.ReadLine();
        }
    }
}
