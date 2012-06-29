using System;
using System.ServiceModel;
using Contracts;

namespace ClientDummy
{
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant)]
    public class CService : ICService
    {
        public void Method(string msg)
        {
            Console.WriteLine(msg);
        }
    }
}
