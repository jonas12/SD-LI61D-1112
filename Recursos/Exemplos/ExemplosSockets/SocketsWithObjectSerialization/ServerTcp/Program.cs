using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using DataTransferObject;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace ServerTcp
{
    class Program
    {
        static void Main(string[] args) {
            Console.Write("Qual o nome da máquina? ");
            string svName = Console.ReadLine();
            IPHostEntry serverHost = Dns.GetHostEntry(svName);
            IPAddress svIP = serverHost.AddressList[0];

            IPEndPoint myEndPoint = new IPEndPoint(svIP, 5000);
          
           
           
            Socket tcpSock=
                   new Socket(myEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            try {
                tcpSock.Bind(myEndPoint);
                
                tcpSock.Listen(int.MaxValue); 
                Console.WriteLine("Server started.");
                while (true) {
                    try  {
                        Console.WriteLine();
                        Socket clientSock = tcpSock.Accept();
                        Console.WriteLine("Accepted connection from: {0}", clientSock.RemoteEndPoint.ToString());
                        
                        try  {
                            NetworkStream networkStream = new NetworkStream(clientSock);
                            
                            IFormatter formatter = new BinaryFormatter();
                            Produto prod = (Produto)formatter.Deserialize(networkStream);
                            Console.WriteLine("Produto recebido:"+prod.Codigo+":"+prod.Nome);
                            
                            prod.Codigo = 555; prod.Nome="Novo codigo e nova designação atribuída pelo servidor";
                            formatter.Serialize(networkStream, prod);
                            
                        }
                        catch (SocketException ex) {
                            Console.WriteLine("Message exchange failed: {0}", ex.Message);
                        }
                        
                    }
                    catch (SocketException ex){
                        Console.WriteLine("Server could not accept connection: {0}", ex.Message);
                    }
                }
            }
            catch (SocketException ex) {
                Console.WriteLine("Failed to start server: {0}", ex.Message);
            }
            finally {
                if (tcpSock != null)  tcpSock.Close();
            }
        }//main
    }//Program
}//namespace
