using System;
using System.Net;
using System.Runtime.Remoting;
using CommonInterface;
using CommonInterface.Exceptions;
using CommonInterface.Utils;

namespace PeerClient
{
    public class PeerMain
    {
        private static readonly string CONFIG_FILE_NAME = "PeerClient.exe.config";
        private static readonly string FILE_NAME = "Articles.xml";

        private static void PrintArticle(Article a)
        {
            Console.WriteLine("Title: " + a.Title);
            Console.WriteLine("Authors: ");

            foreach (string author in a.Authors)
                Console.WriteLine("\t" + author);

            Console.WriteLine("Year: " + a.PublishYear);
            Console.WriteLine("Summary: " + a.Summary);
        }

        public static void Main(string[] args)
        {
            Console.ReadLine();
            RemotingConfiguration.Configure(CONFIG_FILE_NAME, false);
            WellKnownClientTypeEntry[] clients = RemotingConfiguration.GetRegisteredWellKnownClientTypes();

            ISuperPeer sp = (ISuperPeer) Activator.GetObject(typeof(ISuperPeer), clients[0].ObjectUrl);
            
            Peer p = new Peer();

            try
            {
                sp.RegisterPeer(p);
                Console.WriteLine("Peer Connected");
            }
            catch (WebException e)
            {
                Console.WriteLine("Super Peer is Offline");
                Console.WriteLine(e.StackTrace);
                Console.ReadLine();
                return;
            }
            
            p.FromXml(FILE_NAME); // Bring articles to memory

            string cmd;

            Console.WriteLine("Waiting for input...");

            while (!(cmd = Console.ReadLine()).Equals("exit"))
            {
                if(cmd.Equals("list"))
                {
                    foreach (Article a in p.Articles)
                    {
                        PrintArticle(a);
                        Console.WriteLine();
                    }
                }
                if (cmd.Equals("show"))
                {
                    p.SuperPeer.ShowPeers();
                }

                if(cmd.Equals("sa"))
                {
                    Console.Write("title -> ");
                    string title = Console.ReadLine();

                    try
                    {
                        Article a = p.GetArticleBy(title,true);
                        if(a.IsDefault())
                            Console.WriteLine("not found");
                        else
                            PrintArticle(a);
                    }
                    catch (EmptyTitleException)
                    {
                        Console.WriteLine("The title should not be empty");
                    }

                    catch(NotRegisteredToSuperPeerException)
                    {
                        Console.WriteLine("Peer not registered on Super Peer");                      
                    }
                }

                else
                {
                    Console.WriteLine("Invalid Command");
                }
            }
            
            p.ToXml(FILE_NAME);
            sp.UnRegisterPeer(p.Id);
        }
    }
}
