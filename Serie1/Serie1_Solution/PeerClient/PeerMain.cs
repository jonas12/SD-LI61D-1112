using System;
using System.Collections.Generic;
using System.Runtime.Remoting;
using CommonInterface;
using CommonInterface.Exceptions;
using CommonInterface.Utils;
using PeerClient;

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
            RemotingConfiguration.Configure(CONFIG_FILE_NAME, false);
            WellKnownClientTypeEntry[] clients = RemotingConfiguration.GetRegisteredWellKnownClientTypes();
            
            ISuperPeer sp = (ISuperPeer) Activator.GetObject(typeof(ISuperPeer), clients[0].ObjectUrl);
            
            Peer p = new Peer();

            try
            {
                sp.RegisterPeer(p);
                Console.WriteLine("Peer Connected");
            }
            catch (Exception)
            {
                Console.WriteLine("Super Peer is Offline");
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

                if(cmd.Equals("search_article"))
                {
                    Console.Write("title -> ");
                    string title = Console.ReadLine();

                    try
                    {
                        Article a = p.GetArticleBy(title);
                        PrintArticle(a);
                    }
                    catch (EmptyTitleException)
                    {
                        Console.WriteLine("The title should not be empty");
                    }
                }

                else
                {
                    Console.WriteLine("Invalid Command");
                }
            }
            
            sp.UnRegisterPeer(p);
        }
    }
}
