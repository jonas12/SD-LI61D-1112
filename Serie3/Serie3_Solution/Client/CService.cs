using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using Contracts;

namespace Client
{
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant)]
    public class CService : ServiceReference1.ICentralServiceCallback
    {
        public void Method(string msg)
        {
            Console.WriteLine(msg);
        }

        public void Receive(string msg)
        {
            Console.WriteLine(msg);
        }
    }
}
