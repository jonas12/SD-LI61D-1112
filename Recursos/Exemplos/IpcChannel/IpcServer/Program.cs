using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels.Ipc;
using System.Runtime.Remoting.Channels.Http;
using IRemObject;

namespace IpcServer
{
  
        public class RemoteOlaMundo : MarshalByRefObject, IRemOla   {
            
            public string ola(string nome) {
                return "Ola " + nome;
            }
            
        }

        class Program {
            static void Main(string[] args)  {
               try  {
                  Console.WriteLine("Configura��o do Server...");
                  RemotingConfiguration.Configure("IpcServer.exe.config",false);

                  WellKnownServiceTypeEntry[] entries =
                  RemotingConfiguration.GetRegisteredWellKnownServiceTypes();
                  string objUri=entries[0].ObjectUri;
                   //Console.WriteLine(entries[0].ObjectType, entries[0].ObjectUrl);                
                  Console.WriteLine("Verificar os channels configurados:");
                   // Para todos os Channel no ficheiro de configura��o
                  foreach (IChannel channel in ChannelServices.RegisteredChannels)  {
                      Console.WriteLine("Channel registado: " + channel.ChannelName);
                      if (channel is IpcChannel)
                      {
                          if (((IpcChannel)channel).ChannelData != null)
                          {
                              ChannelDataStore dataStore = (ChannelDataStore)((IpcChannel)channel).ChannelData;
                              foreach (string uri in dataStore.ChannelUris)
                              {
                                  Console.WriteLine(@"> encontrou um Channel com URI: " + uri + "/" + objUri);
                              }
                          }
                          else Console.WriteLine("-) Channel Ipc sem dados de defini��o!");
                      }
                      else
                      {
                          if (channel is TcpChannel)
                          {
                              if (((TcpChannel)channel).ChannelData != null)
                              {
                                  ChannelDataStore dataStore = (ChannelDataStore)((TcpChannel)channel).ChannelData;
                                  foreach (string uri in dataStore.ChannelUris)
                                  {
                                      Console.WriteLine(@"> encontrou um Channel com URI: " + uri + "/" + objUri);
                                  }
                              }
                              else Console.WriteLine("-) Channel Tcp sem dados de defini��o!");
                          }
                          else
                          {
                              if (channel is HttpChannel)
                              {
                                  if (((HttpChannel)channel).ChannelData != null)
                                  {
                                      ChannelDataStore dataStore = (ChannelDataStore)((HttpChannel)channel).ChannelData;
                                      foreach (string uri in dataStore.ChannelUris)
                                      {
                                          Console.WriteLine(@"> encontrou um Channel com URI: " + uri + "/" + objUri);
                                      }
                                  }
                                  else Console.WriteLine("-) Channel Http sem dados de defini��o!");

                              }
                              else Console.WriteLine("N�o existem Channels registados");
                          }
                      }
                  } // foreach
                  Console.WriteLine("--- Espera Pedidos...");
                  Console.ReadLine();
               } catch (Exception ex)
               {
                   Console.WriteLine("Error: Na configura��o do IpcServer: " + ex.Message);
                   Console.ReadLine();
               }

        }
    }
}
