using System;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using ClassHeaders;


    [WebService(Description = "Exemplo SoapHeaders", Namespace = "http://deetc.isel.ipl.pt/SD/")]
    public class WSLogin : System.Web.Services.WebService
    {
        public SoapHdLogin CurrentUser;
        public SoapHdMoreInfo MoreInfo = new SoapHdMoreInfo();

        public OtherHeader oh;
        

        [WebMethod, SoapHeader("CurrentUser", Direction = SoapHeaderDirection.InOut)]
        [SoapHeader("MoreInfo", Direction = SoapHeaderDirection.Out)]
        public string Login(string msg)
        {
            System.Threading.Thread.Sleep(2 * 1000); // simular que operação demora 2 seg.
            CurrentUser.ts.dtOut = DateTime.Now;
            MoreInfo.info = "Informação out do WS";
            if (CurrentUser.UserName == "ISEL" && CurrentUser.TokenKey == "12345")
                return "Login Efectuado " + msg;
            else
            {
                CurrentUser.UserName = "??????"; CurrentUser.TokenKey = "-----";
                return "Login não Efectuado " + msg;
            }
        }

        [WebMethod, SoapHeader("oh")]
        
        public string OtherMeth()
        {
            return "other method";
        }
    }

