using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Runtime.Remoting;
using System.Text;
using System.Windows.Forms;
using CommonInterface;
using CommonInterface.Utils;

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
            RemotingConfiguration.Configure(CONFIG_FILE_NAME, false);
            WellKnownClientTypeEntry[] clients = RemotingConfiguration.GetRegisteredWellKnownClientTypes();

            ISuperPeer sp = (ISuperPeer)Activator.GetObject(typeof(ISuperPeer), url ?? clients[0].ObjectUrl);

            try
            {
                sp.RegisterPeer(p);
                spLoctxt.Text = clients[0].ObjectUrl;
                registerBtn.Enabled = false;
                unregisterBtn.Enabled = true;
                isConnected = true;
                if(url!=null)
                    RemotingConfiguration.RegisterWellKnownClientType(typeof(ISuperPeer),url);
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
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            p = new Peer();
            p.FromXml(FILE_NAME);
            
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
            if (!isConnected)
                return;
            string artname = artclNametxt.Text;
            Article a = p.GetArticleBy(artname);
            PrintArticle(a);
        }
    }
}
