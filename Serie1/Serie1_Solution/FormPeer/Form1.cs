using System;
using System.Linq;
using System.Net;
using System.Runtime.Remoting;
using System.Windows.Forms;
using CommonInterface;
using CommonInterface.Utils;
using SuperPeerClient;

namespace FormPeer
{
    public partial class Form1 : Form
    {
        private IPeer p;
        private readonly string CONFIG_FILE_NAME = "FormPeer.exe.config";
        private readonly string FILE_NAME = "Articles.xml";
        private const string DefaultText = "Choose SuperPeer";
        private bool isConnected = false;

        private void PrintArticle(Article a)
        {
            var artcl = "Title: {0}\nAuthors: {1}\nYear: {2}\n Summary: {3}\n";

            var authors = a.Authors.Aggregate("", (current, author) => String.Concat(current, "\t" + author));

            artcl = String.Format(artcl, a.Title, authors, a.PublishYear, a.Summary);

            artcPrint.AppendText(artcl);
        }

        private void TryRegisterPeer(string url)
        {
            WellKnownClientTypeEntry[] clients = RemotingConfiguration.GetRegisteredWellKnownClientTypes();

            ISuperPeer sp = (ISuperPeer)Activator.GetObject(typeof(ISuperPeer), url ?? clients[0].ObjectUrl);

            try
            {
                p.BindToSuperPeer(sp);
                spLoctxt.Text = clients[0].ObjectUrl;
                registerBtn.Enabled = false;
                unregisterBtn.Enabled = true;
                isConnected = true;

                if(url!=null)
                    RemotingConfiguration.RegisterWellKnownClientType(typeof(ISuperPeer),url);
                artcPrint.AppendText(String.Format("Connected to {0}\n", (url ?? clients[0].ObjectUrl)));
            }

            catch (WebException)
            {
                spLoctxt.Text = DefaultText;
                return;
            } // Bring articles to memory

        }

        private void UnregisterPeer()
        {
            try
            {
                p.UnbindFromSuperPeer();
            }
            finally 
            {
                registerBtn.Enabled = false;
                unregisterBtn.Enabled = true;
                isConnected = false;
                artcPrint.AppendText("Unregistered\n");
            }
        }

        private void ToSuperPeer()
        {
            WellKnownClientTypeEntry[] clients = RemotingConfiguration.GetRegisteredWellKnownClientTypes();
            ISuperPeer otherSuperPeer = (ISuperPeer) Activator.GetObject(typeof(ISuperPeer), clients[0].ObjectUrl);

            SuperPeer sp = new SuperPeer
            {
                Articles = p.Articles,
                OnlinePeers = p.OnlinePeers
            };

            RemotingConfiguration.RegisterWellKnownServiceType(typeof(SuperPeer), "SuperPeer.soap", WellKnownObjectMode.Singleton);

            WellKnownServiceTypeEntry[] services = RemotingConfiguration.GetRegisteredWellKnownServiceTypes();

            RemotingServices.Marshal(sp, "SuperPeer.soap"); 

            try
            {
                otherSuperPeer.UnRegisterPeer(p);
            }
            catch (WebException)
            {
                p = sp;
                return;
            }

            sp.SuperPeers.Add(otherSuperPeer);
            otherSuperPeer.SuperPeers.Add(sp);
            p = sp;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            p = new Peer();
            p.FromXml(FILE_NAME);

            RemotingConfiguration.Configure(CONFIG_FILE_NAME, false);

            TryRegisterPeer(null);
        }

        private void registerBtn_Click(object sender, EventArgs e)
        {
            string url = spLoctxt.Text;
            if (String.IsNullOrEmpty(url) || url.Equals(DefaultText))
                url = null;
            TryRegisterPeer(url);
        }

        private void unregisterBtn_Click(object sender, EventArgs e)
        {
            UnregisterPeer();
        }

        private void artclsrchBtn_Click(object sender, EventArgs e)
        {
            string artname = artclNametxt.Text;

            if (!isConnected || String.IsNullOrEmpty(artname))
                return;

            Article a = p.GetArticleBy(artname, false);

            if(!a.IsDefault())
                PrintArticle(a);
        }

        private void spbcBtn_Click(object sender, EventArgs e)
        {
            ToSuperPeer();
        }
    }
}
