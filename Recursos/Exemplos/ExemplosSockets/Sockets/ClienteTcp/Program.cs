using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace ClienteTcp
{
    class Program {
        static void Main(string[] args) {            
            try {
                Console.Write("Qual o nome da máquina com o servidor? ");
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
                        StreamWriter writer = null;
                        StreamReader reader = null;
                        try {
                            NetworkStream networkStream = new NetworkStream(tcpSock);
                            writer = new StreamWriter(networkStream);
                            string msg = "Ola Server:";
                            writer.WriteLine(msg);
                            writer.Flush();
                            Console.WriteLine("Client sent message: {0}", msg);

                            reader = new StreamReader(networkStream);
                            string msgResult = reader.ReadLine();
                            Console.WriteLine("Client received message: {0}", msgResult);
                        }catch (SocketException ex) {
                            Console.WriteLine("Message exchange failed: {0}", ex.Message);
                        }catch (IOException ex) {
                            Console.WriteLine("Message exchange failed: {0}", ex.Message);
                        }finally {
                            if (reader != null) reader.Close();
                            if (writer != null) writer.Close();
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
