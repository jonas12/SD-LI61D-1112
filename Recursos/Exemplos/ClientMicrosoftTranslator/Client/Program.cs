using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Client.sd;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            LanguageServiceClient svc = new LanguageServiceClient();
            
            Console.WriteLine("qual a frase a traduzir?");
            string frase = Console.ReadLine();
            
            string str = svc.Translate("F4E6E0444F32B660BED9908E9744594B53D2E864", frase, "pt", "en", "text/html", "general"); 
            
            Console.WriteLine("Tradução:"+str);
            Console.ReadLine();
        }
    }
}
