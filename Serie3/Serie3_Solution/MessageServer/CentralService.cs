using System;
using System.Globalization;
using System.Linq;
using System.Security.Policy;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading;
using System.Threading.Tasks;
using Contracts;
using MessageServer.ServiceReference1;

namespace MessageServer
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class CentralService : ICentralService
    {
        private const string AppId = "F4E6E0444F32B660BED9908E9744594B53D2E864";
        private static readonly string[] _supportedLanguages;
        private static readonly string[] _supportedLanguagesNames;
        private static readonly string[] _supportedThemes;


        static CentralService()
        {
            var langService = new LanguageServiceClient();
            _supportedLanguages = langService.GetLanguagesForTranslate(AppId);
            _supportedLanguagesNames = langService.GetLanguageNames(AppId, "en", _supportedLanguages);
            _supportedThemes = new[]
                       {
                           "Horror",
                           "Comedy",
                           "Romance"
                       };
        }
        public int Register(int theme, int language)
        {
            var proxy = OperationContext.Current.GetCallbackChannel<ICService>();
            var supportedLanguagesName = _supportedLanguages[language];
            var supportedTheme = _supportedThemes[theme];

            Console.WriteLine("ThreadID:{0}, Theme: {2}, Language: {1}", Thread.CurrentThread.ManagedThreadId, supportedLanguagesName, supportedTheme);
            return UserRepository.Insert(new User()
                                      {
                                          Callback = proxy,
                                          Language = supportedLanguagesName,
                                          Theme = supportedTheme
                                      });
        }

        public void UnRegister(int clientId)
        {
            UserRepository.Remove(clientId);
        }

        public string[] GetSupportedLanguages()
        {
            return _supportedLanguagesNames;
        }

        public string[] GetSupportedThemes()
        {
            return new[]
                       {
                           "Horror",
                           "Comedy",
                           "Romance"
                       };
        }

        public void SubmitMessage(string msg)
        {
            var proxy = OperationContext.Current.GetCallbackChannel<ICService>();
            var user = UserRepository.GetByService(proxy);

            var users = UserRepository.GetAllMatchingThemeUsers(user);

            var translatorService = new LanguageServiceClient();

            foreach (var ug in users.GroupBy(ur => ur.Language))
            {
                string m = Translate(user.Language, ug.First().Language, msg, translatorService);

                foreach (var u in ug)
                {
                    User u1 = u;
                    Task t = Task.Factory.StartNew(() =>
                                                       {
                                                           try
                                                           {
                                                               u1.Callback.Receive(m);
                                                           }
                                                           catch (Exception)
                                                           {
                                                               UserRepository.Remove(u1.Id);
                                                           }
                                                       });
                }
            }
        }

        private static string Translate(string from, string to, string msg, LanguageServiceClient translatorService)
        {
            try
            {
                return to.Equals(from)
                           ? msg
                           : translatorService.Translate(AppId, msg, from, to, "text/html", "general");
            }
            catch (EndpointNotFoundException)
            {
                return msg;
            }
        }
    }
}

