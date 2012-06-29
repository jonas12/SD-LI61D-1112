using System;
using System.ServiceModel;
using Client.ServiceReference1;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var cs = new CService();
                var ctx = new InstanceContext(cs);
                var server = new CentralServiceClient(ctx);

                int lang = ShowSupportedLanguages(server);
                int theme = ShowSupportedThemes(server);

                var id = server.Register(theme, lang);
                Console.Clear();
                Console.WriteLine("User {2} registered with {0} & {1}\n\n", lang, theme, id);
                while (true)
                {
                    Console.WriteLine("Write the message to send, or no message to quit\n");
                    string msg = Console.ReadLine();
                    if (msg != null && msg.Equals(string.Empty))
                        break;
                    server.SubmitMessage(msg);
                }
                server.UnRegister(id);
            }
            catch (EndpointNotFoundException)
            {
                Console.WriteLine("Unable to contact server...");
            }finally
            {
                Console.WriteLine("Press enter to exit...");
                Console.ReadLine();
            }
        }

        private static int ShowSupportedThemes(CentralServiceClient server)
        {
            var themes = server.GetSupportedThemes();
            Console.WriteLine("Choose a theme: ");
            return ShowChoices(themes);
        }

        private static int ShowSupportedLanguages(CentralServiceClient server)
        {
            var langs = server.GetSupportedLanguages();
            Console.WriteLine("Choose a language: ");
            return ShowChoices(langs);
        }

        private static int ShowChoices(string[] options)
        {
            do
            {
                int count = 0;
                foreach (var lang in options)
                {
                    Console.WriteLine("{0} - {1};", count++, lang);
                }
                int choice;
                if (Int32.TryParse(Console.ReadLine(), out choice) && choice >= 0 && choice < options.Length)
                    return choice;
                Console.Clear();
                Console.WriteLine("Error: Choice unsuccessful");
            } while (true);
        }

    }
}
