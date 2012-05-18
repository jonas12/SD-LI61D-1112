using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharedTypes
{  
    [Serializable]
    public class Request
    {
        public string reqID;
        public string operacao;
        public double  op1;
        public double op2;
    }
    [Serializable]
    public class Response
    {
        public string reqID;
        public double res;
      
    }
}
