using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassHeaders
{
    public class TimeStamp
    {
        public DateTime dtIn;
        public DateTime dtOut;

    }

    public class SoapHdLogin : System.Web.Services.Protocols.SoapHeader
    {
        public string UserName;
        public string TokenKey;
        public TimeStamp ts;

    }

    public class SoapHdMoreInfo : System.Web.Services.Protocols.SoapHeader
    {
        public string info;
    }

    public class OtherHeader : System.Web.Services.Protocols.SoapHeader
    {
        public string[] tabstr;
    }
}
