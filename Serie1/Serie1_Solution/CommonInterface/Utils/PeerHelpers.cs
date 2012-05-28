using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
                p.Ping();
            }
            catch (WebException)
            {
                return false;
            }

            return true;
        }

        // isto pode lançar excepção (webException) caso um peer se disconecte depois do getpeers ser chamado
        //tratar mais tarde
        public static void ConcatAndReturnDif(ref IList<KeyValuePair<int,IPeer>> list,IDictionary<int, IPeer> onlinePeers, IList<IPeer> getPeers, IPeer peer)
        {
            IEnumerable<KeyValuePair<int, IPeer>> list2 = getPeers.Where(p => !onlinePeers.ContainsKey(p.Id) && p.Id != peer.Id)
                                                                    .Select(p => new KeyValuePair<int, IPeer>(p.Id, p));
            foreach (var kvp in list2)
            {
                list.Add(kvp);
                peer.OnlinePeers.Add(kvp);
            }

        }
    }
}
