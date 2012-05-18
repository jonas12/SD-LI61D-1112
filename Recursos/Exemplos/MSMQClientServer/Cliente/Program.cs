using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Messaging;
using SharedTypes;
using System.Threading;

namespace Cliente
{
    class Program
    {
        private static string qserver = @".\Private$\QServer";
        private static string qresp = @".\Private$\QResponse";

        static MessageQueue createNewResponseQueue(string qpath)
        {
            MessageQueue mq = null;
            if (MessageQueue.Exists(qpath))
            {
                    // Connect if the message queue already exists.
                    mq = new MessageQueue(qpath);
                    mq.Label = qpath;
                    Console.WriteLine("Connected to existing response message queue: " + qpath);
             }
             else
             {
                    // Create a new message queue.
                    mq = MessageQueue.Create(qpath);
                    mq.Label = qpath;
                    Console.WriteLine("Created response message queue: " + qpath);
             }

            return mq;
        }

        static void Main(string[] args)
        {
            try
            {
                MessageQueue mq = null;
                if (!MessageQueue.Exists(qserver))
                {
                    Console.WriteLine("Can´t connect to Server message queue " + qserver);
                }
                else
                {
                    Console.Write("Qual o seu ID? "); string cID = Console.ReadLine();
                    MessageQueue qresponse=createNewResponseQueue(qresp + cID);
                    // Connect to server message queue that already exists to sen a message
                    mq = new MessageQueue(qserver);

                    for (int i = 0; i < 100; i++)
                    {
                        Request req = new Request();
                        req.operacao = "mult"; req.op1 = 10; req.op2 = (double)i; req.reqID = i.ToString();
                        Message msg = new Message();
                        msg.Body = req;
                        msg.ResponseQueue = qresponse;
                        mq.Send(msg);
                    }
                    for (int i = 0; i < 100; i++)
                    {
                        // get the response
                        Message resMsg = qresponse.Receive();
        
                        resMsg.Formatter = new System.Messaging.XmlMessageFormatter(new Type[] { typeof(Response) });
                        Response res = (Response)resMsg.Body;
                        Console.WriteLine("Resposta ao pedido " + res.reqID + "=" + res.res);
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetType() + ": " + ex.Message);
            }
        }
    }
}
