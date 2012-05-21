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
        private readonly string CONFIG_FILE_NAME = "PeerClient.exe.config";
        private readonly string FILE_NAME = "Articles.xml";

        private void PrintArticle(Article a)
        {
            var artcl = "Title: {0}\nAuthors: {1}\nYear: {2}\n Summary: {3}\n";

            var authors = a.Authors.Aggregate("", (current, author) => String.Concat(current, "\t" + author));

            artcl = String.Format(artcl, a.Title, authors, a.PublishYear, a.Summary);

            artcPrint.AppendText(artcl);
        }

        private void InitPeer()
        {
            RemotingConfiguration.Configure(CONFIG_FILE_NAME, false);
            WellKnownClientTypeEntry[] clients = RemotingConfiguration.GetRegisteredWellKnownClientTypes();

            ISuperPeer sp = (ISuperPeer)Activator.GetObject(typeof(ISuperPeer), clients[0].ObjectUrl);

            Peer p = new Peer();

            try
            {
                sp.RegisterPeer(p);
                spLoctxt.Text = clients[0].ObjectUrl;
                registerBtn.Enabled = false;
                unregisterBtn.Enabled = true;
            }
            catch (WebException)
            {
                spLoctxt.Text = "Choose a SuperPeer";
                return;
            }
            p.FromXml(FILE_NAME); // Bring articles to memory
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            p = new Peer();
        }

        private void registerBtn_Click(object sender, EventArgs e)
        {
            artcPrint.AppendText("Something Somethig\n");
        }
    }
}
