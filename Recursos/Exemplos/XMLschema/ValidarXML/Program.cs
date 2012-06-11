using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Schema;
using System.Xml;
using System.IO;

namespace ValidarXML
{
    class Program
    {
        static bool erro = false;
        public static void ShowParserErrors(object sender, ValidationEventArgs args)
        {
            Console.WriteLine("ERRO: XML fragment não obedece ao schema: {0}", args.Message);
            erro = true;
        }

        static void Main(string[] args)
        {
            try
            {
                //XML fragment para validar
                string xmlFrag =
                 "<address  myns:Pais='port'  myns:PaisCode='351' xmlns='http://SD/PTAddress.xsd' xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'  >" +
                    "<nome>ISEL</nome>" +
                    "<rua>Conselheiro Emidio Navarro</rua>" +
                    "<numero>1</numero>" +
                    "<numero>2</numero>" +
                    "<numero>3</numero>" +
                    //"<numero>4</numero>" +
                    "<cidade>Lisboa</cidade>" +
                    "<codigo-postal>1950</codigo-postal>" +
                 "</address>";

                //Set the settings to xml reader and add the schema
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.Schemas.Add("http://SD/PTAddress.xsd", "..\\..\\XMLSchemaPTAddress.xsd");
                settings.ValidationType = ValidationType.Schema;
                settings.ValidationFlags |= XmlSchemaValidationFlags.AllowXmlAttributes;
                settings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;
                settings.ConformanceLevel = ConformanceLevel.Fragment;
                settings.ValidationEventHandler += new ValidationEventHandler(ShowParserErrors);

                // Create the XmlNamespaceManager.
                XmlNamespaceManager nsmgr = new XmlNamespaceManager(new NameTable());
                nsmgr.AddNamespace("myns", "http://SD/PTAddress.xsd");

                // Create the XmlParserContext.
                XmlParserContext context = new XmlParserContext(null, nsmgr, null, XmlSpace.None);

                XmlReader reader = XmlReader.Create(new StringReader(xmlFrag), settings, context);


                while (reader.Read())
                {
                    //ler toda a string com fragmento xml
                }
                if (!erro) Console.WriteLine("XML fragment VÁLIDO");
            }
            catch (XmlException XmlExp)
            {
                Console.WriteLine(XmlExp.Message);
            }
            catch (XmlSchemaException XmlSchExp)
            {
                Console.WriteLine(XmlSchExp.Message);
            }
            catch (Exception GenExp)
            {
                Console.WriteLine(GenExp.Message);
            }
            finally
            {
                Console.Read();
            }


        }
    }
}
