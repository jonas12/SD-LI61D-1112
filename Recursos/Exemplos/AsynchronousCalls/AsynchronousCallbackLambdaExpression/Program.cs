using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels;
using IRemObject;
using System.Threading;

namespace AsynchronousCallbackLambdaExpression
{
    class Program
    {
        delegate byte DelAdjust(byte b, int x, int y, int z);

        static void Main(string[] args)
        {
            HttpChannel ch = new HttpChannel();
            ChannelServices.RegisterChannel(ch, false);
            IPix robj = (IPix)Activator.GetObject(
                typeof(IPix),
                "http://localhost:1234/Images.soap");

            DelAdjust del = new DelAdjust(robj.adjustPixel);
            byte[, ,] image = new byte[3, 3, 3] {  { {1,2,3}, { 1, 2, 3}, {1,2,3} },
                                                 { {1,2,3}, { 1, 2, 3}, {1,2,3} },
                                                 { {1,2,3}, { 1, 2, 3}, {1,2,3} } };
            for (int x = 0; x < 3; x++)
                for (int y = 0; y < 3; y++)
                    for (int z = 0; z < 3; z++)
                    {
                        IAsyncResult svasyncres = del.BeginInvoke(0x55, x, y, z,
                                          (IAsyncResult ar) =>
                                           //delegate(IAsyncResult ar)
                                          {
                                              Console.WriteLine("chamada do callback:");
                                              int[] co = (int[])ar.AsyncState;
                                              //string estado = (string)ar.AsyncState;
                                              Console.WriteLine("estado: " + co[0] + "," + co[1]+ ","+co[2]);
                                              AsyncResult ar2 = (AsyncResult)ar;
                                              DelAdjust del2 = (DelAdjust)ar2.AsyncDelegate;
                                              try
                                              {
                                                  image[co[0], co[1], co[2]] = del2.EndInvoke(ar2);
                                              }
                                              catch (Exception ex)
                                              {
                                                  Console.WriteLine("Ocorreu exception:" + ex.Message);
                                              }
                                          },
                                          new int[] { x, y, z }
                                      );
                    }//for

            Console.ReadLine();
            for (int x = 0; x < 3; x++)
                for (int y = 0; y < 3; y++)
                    for (int z = 0; z < 3; z++)
                        Console.WriteLine("{0:x}",image[x, y, z]);

            Console.ReadLine();
        }
    }
}

   
