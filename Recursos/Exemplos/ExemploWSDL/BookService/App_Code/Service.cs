using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Schema;
using System.Collections;


[System.Xml.Serialization.XmlInclude(typeof(PTAddress))]
public class PTAddress
{
    public string nome;
    public string rua;
    public int[] numero;
    public string cidade;
    public string codigopostal;
}
[System.Xml.Serialization.XmlInclude(typeof(Book))]
public class Book
{
    public string isbn;
    public string title;
    public int quantity;
    public float price;
}

[System.Xml.Serialization.XmlInclude(typeof(PurchaseOrder))]
public class PurchaseOrder
{
    public string accountName;
    public int accountNumber;
    public PTAddress paddress;
    public Book itemBook;
    public ArrayList t;
}

public class UmSoapHeader : SoapHeader
{
    public string anything;
    public int[] inteiros;
}

[WebService(Description="Exemplo de WS de cotação de Livros", Namespace="http://DEETC.SES.SD")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
//[System.Web.Services.Protocols.SoapRpcServiceAttribute]
public class BookService : System.Web.Services.WebService
{
    public BookService () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    private bool isbnNotValid(string isbn)
    {
       //return true;
       return false;
    }
    public UmSoapHeader MoreInfo;
    [WebMethod(Description = "Obter um ISBN dado um título: Formato: D-DDD-DDDDD-D")]
    [SoapHeader("MoreInfo", Direction = SoapHeaderDirection.InOut)]
    public string getISBN(string title)
    {
        return "1-123-12345-9";
    }
    [WebMethod(Description="dado um isbn com formato DD-DDDD-D obtem o preço do livro")]
    public float getBookPrice(string isbn)
    {
        
        if (isbnNotValid(isbn))
        {
            XmlDocument doc = new XmlDocument();
            XmlNode node = doc.CreateNode(XmlNodeType.Element, SoapException.DetailElementName.Name, SoapException.DetailElementName.Namespace);
            XmlNode child = doc.CreateNode(XmlNodeType.Element, "error", "http://DEETC.SES.SD");
            child.InnerText = "BookService:Not valid ISBN in getBookPrice operation.";
            node.AppendChild(child);
            throw new SoapException("ISBN not Valid", SoapException.ServerFaultCode, "ActorServer", node);
        }
        return 24.9F;
    }
    [WebMethod]
    public string submitPOrder(PurchaseOrder po)
    {
        return "OK: " + po.accountName + " : " + po.accountNumber;
    }

    [WebMethod]
    public Book[] getbook(string isbn, int qty)
    {
        return new Book[]{new Book()};
    }
    
}
