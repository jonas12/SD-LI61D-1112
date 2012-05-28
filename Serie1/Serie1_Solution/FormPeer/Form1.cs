using System;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using System.Threading;
using System.Windows.Forms;
using CommonInterface;
using CommonInterface.Exceptions;
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

        private bool IsSuperPeer
        {
            get { return p is ISuperPeer; }
        }

        delegate void SetTextCallback(string text);

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

                artcPrint.AppendText(String.Format("Connected to {0}\n", (url ?? clients[0].ObjectUrl)));
            }

            catch (WebException)
            {
                spLoctxt.Text = DefaultText;
                SetTextOnRequests("That SuperPeer is offline.");
                return;
            } 
        }

        private void UnregisterPeer()
        {
            try
            {
                p.UnbindFromSuperPeer();
            }
            finally 
            {
                registerBtn.Enabled = true;
                unregisterBtn.Enabled = false;
                isConnected = false;
                artcPrint.AppendText("Unregistered\n");
            }
        }

        private void UnregisterSuperPeer()
        {
            string url = spLoctxt.Text;
            ISuperPeer sp = (ISuperPeer)Activator.GetObject(typeof(ISuperPeer), url);
            try
            {
                ((ISuperPeer)p).UnbindFromSuperPeer(sp.Id);
                sp.UnbindFromSuperPeer(p.Id);
            }
            finally
            {
                artcPrint.AppendText("Unregistered\n");
            }
        }

        private void ToSuperPeer()
        {
            SuperPeer sp = new SuperPeer
            {
                Articles = p.Articles,
                OnlinePeers = p.OnlinePeers
                
            };
            sp.OnMethodCalled += CallOnPeerMethodCall;
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(SuperPeer), "SuperPeer.soap", WellKnownObjectMode.Singleton);
            WellKnownServiceTypeEntry[] services = RemotingConfiguration.GetRegisteredWellKnownServiceTypes();
            RemotingServices.Marshal(sp, "SuperPeer.soap");

            if(isConnected)
            {
                WellKnownClientTypeEntry[] clients = RemotingConfiguration.GetRegisteredWellKnownClientTypes();
                ISuperPeer otherSuperPeer = (ISuperPeer)Activator.GetObject(typeof(ISuperPeer), clients[0].ObjectUrl);
                try
                {
                    otherSuperPeer.UnRegisterPeer(p.Id);
                }
                catch (WebException)
                {
                    p = sp;
                    return;
                }
                
                sp.BindToSuperPeer(otherSuperPeer);
                otherSuperPeer.BindToSuperPeer(sp);
            }

            p = sp;
            this.Text = "SuperPeer - "+ GetLocalUrl() + services[0].ObjectUri;
        }

        private static string GetLocalUrl()
        {
            var httpChannel = (HttpChannel)ChannelServices.GetChannel("http");
            var channelDataStore = (ChannelDataStore)(httpChannel).ChannelData;
            return channelDataStore.ChannelUris[0] + "/";
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Closing(object sender, EventArgs e)
        {
           if(!IsSuperPeer && isConnected )
           {
               UnregisterPeer();
           }else if(IsSuperPeer)
           {
               p.UnbindFromSuperPeer();
           }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            p = new Peer();
            p.OnMethodCalled += CallOnPeerMethodCall;

            p.FromXml(FILE_NAME);
            RemotingConfiguration.Configure(CONFIG_FILE_NAME, false);

            TryRegisterPeer(null);
        }

        private void registerBtn_Click(object sender, EventArgs e)
        {
            string url = spLoctxt.Text;
            if (String.IsNullOrEmpty(url) || url.Equals(DefaultText))
                url = null;

            if(!IsSuperPeer)
                TryRegisterPeer(url);
            else if(url!=null)
                BindNewSuperPeer(url);
        }

        private void BindNewSuperPeer(string url)
        {
            ISuperPeer sp = (ISuperPeer)Activator.GetObject(typeof(ISuperPeer), url);

            try
            {
                sp.BindToSuperPeer(p as SuperPeer);
                p.BindToSuperPeer(sp);

                artcPrint.AppendText(String.Format("Connected to {0}\n", url));
            }

            catch (WebException)
            {
                spLoctxt.Text = DefaultText;
                SetTextOnRequests("That SuperPeer is offline.");
                return;
            } 
        }

        private void unregisterBtn_Click(object sender, EventArgs e)
        {
            if (!IsSuperPeer)
                UnregisterPeer();
            else
                UnregisterSuperPeer();
        }

        private void artclsrchBtn_Click(object sender, EventArgs e)
        {
            string artname = artclNametxt.Text;
            Article a = default(Article);
            try
            {
               a= p.GetArticleBy(artname, true);    
            }catch(EmptyTitleException)
            {
                artcPrint.AppendText("File name cannot be empty!\n");
                return;
            }catch(NotRegisteredToSuperPeerException)
            {
                artcPrint.AppendText("SuperPeer is offline. Please register a SuperPeer.\n");
                registerBtn.Enabled = true;
                unregisterBtn.Enabled = false;
                isConnected = false;
                return;
            }

            if(!a.IsDefault())
                PrintArticle(a);
            else
                artcPrint.AppendText("File not found!\n");
        }

        public void CallOnPeerMethodCall(object sender, MethodCallEventArgs args)
        {
            SetTextOnRequests(args.Name+" called by: "+args.Id);
        }

        private void spbcBtn_Click(object sender, EventArgs e)
        {
            unregisterBtn.Enabled = true;
            registerBtn.Enabled = true;
            ToSuperPeer();
        }

        private void SetTextOnRequests(string text)
        {
            if(incoming.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetTextOnRequests);
                Invoke(d, new object[] { text });
            }
            else
            {
                incoming.AppendText("Called: " + text + "\n");    
            }
        }

    }
}
