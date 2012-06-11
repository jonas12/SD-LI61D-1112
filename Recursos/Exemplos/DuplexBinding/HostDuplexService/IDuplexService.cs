using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace HostDuplexService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IPubSubService" in both code and config file together.
    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IClientReceiverCallback))]
    public interface IDuplexService
    {
        [OperationContract(IsInitiating=true)] // inicia session no servidor
        string init();
        [OperationContract(IsTerminating=true)] // termina session no servidor
        void exit();
        [OperationContract]
        void SendMsg(string msg);
    }

    // CallBack interface in client 
    public interface IClientReceiverCallback
    {
        [OperationContract(IsOneWay = true)]
        void newAnnounce(string msg);
    }
}
