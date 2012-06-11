using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

[WebService(Namespace = "http://SD/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class Service : System.Web.Services.WebService
{
    public Service () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public void HelloWorld(int num, string str) {
        System.Threading.Thread.Sleep(6 * 1000); // simular operação com 4 segundos
        if (num == -1) throw new Exception("valor = -1");
        else
        {
            int x = 1; int y = 1000; 
            //int res = y / (num * x);
        }
    }

    [WebMethod]
    public string HelloWorldWithReturn(int num, string str)
    {
        System.Threading.Thread.Sleep(1 * 1000); // simular operação com 4 segundos
        if (num == -1) throw new Exception("valor = -1");
        return num+"Hello World "+str;
    }
}
