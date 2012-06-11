using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WCFclient.SD;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Channels;

namespace WCFclient
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceCtxClient prx = new ServiceCtxClient();
            IContextManager ctxManager = prx.InnerChannel.GetProperty<IContextManager>();
            IDictionary<string, string> context = ctxManager.GetContext();
            context["clientName"] = "luis";
            context["AccessID"] = "QWERTY";
            ctxManager.SetContext(context);

            Console.WriteLine(prx.DoWork());

            // Não é possível mudar o contexto após o channel ter sido aberto
            //context["clientName"] = "xx";
            //context["AccessID"] = "XXXXX";
            //contextManager.SetContext(context);


            Console.ReadLine();
        }
    }
}
