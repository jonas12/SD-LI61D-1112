using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;

using System.Text;

namespace HostService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ServiceCtx" in both code and config file together.
    public class ServiceCtx : IServiceCtx
    {
        public string DoWork()
        {
            string clientName=null;
            string accessID=null;
            MessageProperties msgProp = OperationContext.Current.IncomingMessageProperties;
            ContextMessageProperty ctxProperty = msgProp[ContextMessageProperty.Name] as ContextMessageProperty;
            if (ctxProperty.Context.ContainsKey("clientName"))
            {
                clientName=ctxProperty.Context["clientName"];
                Console.WriteLine("client name: " + clientName);

            }
            if (ctxProperty.Context.ContainsKey("AccessID"))
            {
                accessID = ctxProperty.Context["AccessID"];
                Console.WriteLine("  Access ID:" + accessID);
            }

            return clientName + ":" + accessID;
        }
    }
}
