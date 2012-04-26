using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;

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
                        Socket clientSock = tcpSock.Accept();
                        Console.WriteLine("Accepted connection from: {0}", clientSock.RemoteEndPoint.ToString());
                        StreamReader reader = null; StreamWriter writer = null;
                        try  {
                            
                            NetworkStream networkStream = new NetworkStream(clientSock);
                            reader = new StreamReader(networkStream);
                            string clientMsg = reader.ReadLine();
                            Console.WriteLine("Server received message: {0}", clientMsg);

                            writer = new StreamWriter(networkStream);
                            string msgResult = "Ola Cliente com msg="+clientMsg.ToUpper();
                            writer.WriteLine(msgResult);
                            writer.Flush();
                            Console.WriteLine("Server sent message: {0}", msgResult);
                        }
                        catch (SocketException ex) {
                            Console.WriteLine("Message exchange failed: {0}", ex.Message);
                        }
                        finally {
                            if (reader != null) reader.Close();
                            if (writer != null) writer.Close();
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
