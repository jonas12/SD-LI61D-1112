using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Xml.Serialization;

namespace CommonInterface.Utils
{
    public static class PeerHelpers
    {
        public static readonly string ROOT_ELEMENT = "Articles";
        public static void ToXml(this IPeer p, string fileName)
        {
            TextWriter writer = new StreamWriter(fileName);
            XmlSerializer serializer = new XmlSerializer(typeof(List<Article>), new XmlRootAttribute(ROOT_ELEMENT));

            serializer.Serialize(writer, p.Articles);

            writer.Close();
        }

        public static void FromXml(this IPeer p, string fileName)
        {
            TextReader reader = new StreamReader(fileName);
            XmlSerializer serializer = new XmlSerializer(typeof(List<Article>), new XmlRootAttribute(ROOT_ELEMENT));

            p.Articles = (List<Article>) serializer.Deserialize(reader);

            reader.Close();
        }

        public static bool IsAlive(this IPeer p)
        {
            try
            {
                p.IsAlive();
            }
            catch (WebException)
            {
                return false;
            }

            return true;
        }
    }
}
