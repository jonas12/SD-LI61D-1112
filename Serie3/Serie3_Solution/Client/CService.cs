using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using Contracts;

namespace Client
{
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant)]
    public class CService : ICService
    {
        public int Method(string msg)
        {
            Console.WriteLine(msg);
            return 1010;
        }
    }
}
