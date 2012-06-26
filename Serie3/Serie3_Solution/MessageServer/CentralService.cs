using System;
using System.Security.Policy;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading;
using Contracts;

namespace MessageServer
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class CentralService : ICentralService
    {
        public int Register(string endpoint)
        {
            Console.WriteLine("Url:{0}, ThreadID:{1}", endpoint, Thread.CurrentThread.ManagedThreadId);
            var proxy = OperationContext.Current.GetCallbackChannel<ICService>();

            var n = proxy.Method("Hello");
            return 0;
        }

        public void UnRegister(int clientId)
        {
            throw new NotImplementedException();
        }
    }
}
