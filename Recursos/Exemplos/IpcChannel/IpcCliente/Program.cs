using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Ipc;
using IRemObject;

namespace IpcCliente
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Configuração do Ipc Channel...");
               
                IpcChannel ipcCh = new IpcChannel();
                //IpcClientChannel ipcCh = new IpcClientChannel();
                ChannelServices.RegisterChannel(ipcCh, false);

                Console.WriteLine("Obter proxy para remote object...");
                IRemOla robj = (IRemOla)Activator.GetObject(
                     typeof(IRemObject.IRemOla),
                     "ipc://MyIpcChannel/RemObject.rem");

                Console.Write("Qual o seu nome? ");
                string nome= Console.ReadLine();
                Console.WriteLine(robj.ola(nome));
               

            }catch (Exception ex)  {
                Console.WriteLine("General Exception: " + ex.Message);
            }
            Console.ReadLine();
        }

    }
}
