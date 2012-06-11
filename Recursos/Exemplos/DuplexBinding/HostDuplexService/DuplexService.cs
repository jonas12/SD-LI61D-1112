using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace HostDuplexService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "PubSubService" in both code and config file together.
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.Single)]
    public class DuplexService : IDuplexService
    {
        public string init()
        {
            return "Duplex Service is alive";
        }

        
        
        public void SendMsg(string msg)
        {
            string sessionID = OperationContext.Current.SessionId;
            IClientReceiverCallback cli = OperationContext.Current.GetCallbackChannel<IClientReceiverCallback>();
            cli.newAnnounce("New announcement from:" + sessionID + ":" + msg);
        }

        public void exit()
        {
            
        }

        
    }
}
