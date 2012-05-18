using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Messaging;
using SharedTypes;
using System.Threading;

namespace Server
{
    class Program
    {
        private static string qname=@".\Private$\QServer";

        static Response DoTask(Request req)
        {
            Response res = new Response();
            res.reqID = req.reqID;
            switch (req.operacao)
            {
                case "add": res.res = req.op1 + req.op2; break;
                case "sub": res.res = req.op1 - req.op2; break;
                case "mult": res.res = req.op1 *req.op2; break;
                case "div": res.res = req.op1 / req.op2; break;
            }
            //simulate a slow task
            Thread.Sleep(4 * 1000);
            return res;
        }

        static void Main(string[] args)
        {
            try
            {
                MessageQueue mq = null;
                if (MessageQueue.Exists(qname))
                {
                    // Connect if the message queue already exists.
                    mq = new MessageQueue(qname);
                    Console.WriteLine("Connected to existing message queue: " + qname);
                }
                else
                {
                   // Create a new message queue.
                   mq = MessageQueue.Create(qname);
                   Console.WriteLine("Created message queue: " + qname);
                }

                //Loop: Wait message; Process message; Send Response
                for (;;)
                {
                    Message reqMsg = mq.Receive();
                    reqMsg.Formatter = new System.Messaging.XmlMessageFormatter(new Type[] { typeof(Request) });
                    Request req = (Request)reqMsg.Body;
                    Console.WriteLine(req.operacao +" "+ req.op1 +" "+ req.op2);
                    Console.WriteLine("Response Queue: "+reqMsg.ResponseQueue.Label);
                    Response res=DoTask(req);
                    Message resMsg = new Message();
                    resMsg.Body = res;
                    reqMsg.ResponseQueue.Send(resMsg);
                }


            }
            catch (Exception e)
            {
                Console.WriteLine(e.GetType() + ": " + e.Message);
            }




        }
    }
}
