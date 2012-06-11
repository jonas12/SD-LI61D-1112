using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace DefaultEndPoint
{
    [ServiceContract]
    public interface IHelloService
    {
        [OperationContract]
        string SayHello(string user);
    }
}
