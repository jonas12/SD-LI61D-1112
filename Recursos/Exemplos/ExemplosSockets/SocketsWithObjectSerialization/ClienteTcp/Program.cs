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

namespace ClienteTcp
{
    class Program {
        static void Main(string[] args) {            
            try {
                Console.Write("Qual o nome da máquina com o processo servidor? ");
                string svcName=Console.ReadLine();
                IPHostEntry serverHost = Dns.GetHostEntry(svcName);
                foreach (IPAddress ip in serverHost.AddressList)
                    Console.WriteLine(ip.ToString()+" AddressFamily=" + ip.AddressFamily.ToString());
                IPAddress svcIP = serverHost.AddressList[0];
                Console.WriteLine(svcName + ":" + svcIP.ToString());
                IPEndPoint svcEndPoint = new IPEndPoint(svcIP, 5000);
               
                Socket tcpSock =
                       new Socket(svcIP.AddressFamily,SocketType.Stream,ProtocolType.Tcp);
                    try {
                        tcpSock.Connect(svcEndPoint);
                        
                        try {
                            Console.WriteLine();
                            NetworkStream networkStream = new NetworkStream(tcpSock);

                            Produto prod = new Produto() { Codigo = 25, Nome = "Designação do Produto" };
                            Console.WriteLine("Vai enviar produto:" + prod.Codigo + ":" + prod.Nome);
                            IFormatter formatter = new BinaryFormatter();
                            formatter.Serialize(networkStream, prod);

                            Produto prod2 = (Produto)formatter.Deserialize(networkStream);
                            Console.WriteLine("Recebido Novo produto:"+prod2.Codigo + ":" + prod2.Nome);
                                                        
                        }catch (SocketException ex) {
                            Console.WriteLine("Message exchange failed: {0}", ex.Message);
                        }catch (IOException ex) {
                            Console.WriteLine("Message exchange failed: {0}", ex.Message);
                        }finally {
                            if (tcpSock != null) tcpSock.Close();
                        }
                    } catch (SocketException) {
                        if (tcpSock != null) tcpSock.Close();
                        Console.WriteLine("Failed to connect to the server.");
                    }
            }catch (SocketException ex){
                Console.WriteLine("Could not resolve server DNS name: {0}", ex.Message);
            }
        }//main
    }//Program
}//namespace
