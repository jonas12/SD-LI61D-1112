using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DefaultEndPoint
{
    public class HelloService : IHelloService
    {
        public string SayHello(string user)
        {
            return "Hello " + user;
        }
    }
}
