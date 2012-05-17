using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;


namespace WinUser
{
    public partial class Form1 : Form
    {
        // The Spread Connection.
        private SpreadConnection connection=null;
        // A group.
        private SpreadGroup group;
        private SpreadConnection.MessageHandler handler;

        public Form1()
        {
            InitializeComponent();
            this.FormClosed += new FormClosedEventHandler(this.CloseWin);
            comboBox1.SelectedIndex = 0;
        }
        public void CloseWin(object sender, FormClosedEventArgs e)
        {
            //MessageBox.Show("end");
            //if (rtt != null) rtt.Interrupt();
            if (connection != null) connection.Disconnect();
            Environment.Exit(0);
        }
        private delegate void AddTextDel(SpreadMessage msg);
        public void messageReceived(SpreadMessage msg)
        {
            if (this.InvokeRequired)
            {
                AddTextDel del = new AddTextDel(this.messageReceived);
                object[] args = new object[] { msg };
                this.Invoke(del, args);
            }
            else
                this.DisplayMessage(msg);
        }
        private void DisplayMessage(SpreadMessage msg)
        {
            try
            {
                richTextBox1.AppendText("--------------\n");
                string str = null;
                if (msg.IsRegular)
                {
                    str="Received a ";
                    if (msg.IsUnreliable)
                        str+="UNRELIABLE";
                    else if (msg.IsReliable)
                        str+="RELIABLE";
                    else if (msg.IsFifo)
                        str+="FIFO";
                    else if (msg.IsCausal)
                        str+="CAUSAL";
                    else if (msg.IsAgreed)
                        str+="AGREED";
                    else if (msg.IsSafe)
                        str+="SAFE";
                    str+=" message.";
                    richTextBox1.AppendText(str+"\n");
                    //richTextBox1.AppendText("Type is " + msg.Type + ".\n");
                    richTextBox1.AppendText("Sent by  " + msg.Sender + ".");
                    

                    //if (msg.EndianMismatch == true)
                    //    richTextBox1.AppendText("There is an endian mismatch.\n");
                    //else
                    //    richTextBox1.AppendText("There is no endian mismatch.\n");

                    SpreadGroup[] groups = msg.Groups;
                    richTextBox1.AppendText("To " + groups.Length + " groups.\n");

                    byte[] data = msg.Data;
                    richTextBox1.AppendText("The Size is " + data.Length + " bytes.\n");

                    richTextBox1.AppendText("The message is: " + System.Text.Encoding.ASCII.GetString(data)+"\n");
                }
                else if (msg.IsMembership)
                {
                    MembershipInfo info = msg.MembershipInfo;

                    if (info.IsRegularMembership)
                    {
                        SpreadGroup[] groups = msg.Groups;

                        richTextBox1.AppendText("Received a REGULAR membership.\n");
                        richTextBox1.AppendText("For group " + info.Group + ", ID=" + info.GroupID.ToString() + "\n");
                        richTextBox1.AppendText("with " + groups.Length + " members:\n");
                        //richTextBox1.AppendText("Members: "); 
                        for (int i = 0; i < groups.Length; i++)
                            richTextBox1.AppendText("  " + groups[i]);
                        richTextBox1.AppendText("\n");
                        
                        richTextBox1.AppendText("Due to ");
                        if (info.IsCausedByJoin)
                        {
                            richTextBox1.AppendText("the JOIN of " + info.Joined + "."+"\n");
                        }
                        else if (info.IsCausedByLeave)
                        {
                            richTextBox1.AppendText("the LEAVE of " + info.Left + "."+"\n");
                        }
                        else if (info.IsCausedByDisconnect)
                        {
                            richTextBox1.AppendText("the DISCONNECT of " + info.Disconnected + "."+"\n");
                        }
                        else if (info.IsCausedByNetwork)
                        {
                            SpreadGroup[] stayed = info.Stayed;
                            richTextBox1.AppendText("NETWORK change."+"\n");
                            richTextBox1.AppendText("VS set has " + stayed.Length + " members:"+"\n");
                            for (int i = 0; i < stayed.Length; i++)
                                richTextBox1.AppendText("  " + stayed[i]+"\n");
                        }
                    }
                    else if (info.IsTransition)
                    {
                        richTextBox1.AppendText("Received a TRANSITIONAL membership for group " + info.Group+"\n");
                    }
                    else if (info.IsSelfLeave)
                    {
                        richTextBox1.AppendText("Received a SELF-LEAVE message for group " + info.Group+"\n");
                    }
                }
                else if (msg.IsReject)
                {
                    // Received a Reject message 
                    richTextBox1.AppendText("Received a ");
                    if (msg.IsUnreliable)
                        richTextBox1.AppendText("UNRELIABLE");
                    else if (msg.IsReliable)
                        richTextBox1.AppendText("RELIABLE");
                    else if (msg.IsFifo)
                        richTextBox1.AppendText("FIFO");
                    else if (msg.IsCausal)
                        richTextBox1.AppendText("CAUSAL");
                    else if (msg.IsAgreed)
                        richTextBox1.AppendText("AGREED");
                    else if (msg.IsSafe)
                        richTextBox1.AppendText("SAFE");

                    richTextBox1.AppendText(" REJECTED message."+"\n");

                    richTextBox1.AppendText("Sent by  " + msg.Sender + ".");

                    richTextBox1.AppendText("Type is " + msg.Type + "."+"\n");

                    //if (msg.EndianMismatch == true)
                    //    richTextBox1.AppendText("There is an endian mismatch."+"\n");
                    //else
                    //    richTextBox1.AppendText("There is no endian mismatch."+"\n");

                    SpreadGroup[] groups = msg.Groups;
                    richTextBox1.AppendText("To " + groups.Length + " groups."+"\n");

                    byte[] data = msg.Data;
                    richTextBox1.AppendText("The Size is " + data.Length + " bytes."+"\n");

                    richTextBox1.AppendText("The message is: " + System.Text.Encoding.ASCII.GetString(data)+"\n");
                }
                else
                {
                    richTextBox1.AppendText("Message is of unknown type: " + msg.ServiceType+"\n");
                }
                richTextBox1.AppendText("\n");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                
            }
        }
        //public void messageReceived(SpreadMessage message)
        //{
        //    DisplayMessage(message);
        //}
        private Thread rtt=null;
        private void button1_Click(object sender, EventArgs e)
        {
            string address = null;
            int port = 0;
            string user = textBox1.Text;
            handler = new SpreadConnection.MessageHandler(messageReceived);
            try
            {
                connection = new SpreadConnection();
                connection.Connect(address, port, user, false, true);
                rtt = new Thread(new ThreadStart(this.run));
                rtt.Start();
            }
            catch (SpreadException ex)
            {
                MessageBox.Show("There was an error connecting to the daemon."+ex.Message);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can't find the daemon " + address+ex.Message);
                
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                group = new SpreadGroup();
                string grp = comboBox1.Text;
                group.Join(connection, grp);
                richTextBox1.AppendText("Joined " + group + "." + "\n");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // receive messages Thread
        public void run()
        {
            SpreadMessage msg;
            try {
		        while(true) {
                    msg=connection.Receive();
                    messageReceived(msg);
                 }
             } catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SpreadMessage msg;
            try
            {
                msg = new SpreadMessage();
                msg.IsSafe = true;
                msg.AddGroup(comboBox1.Text);
                msg.Data = System.Text.Encoding.ASCII.GetBytes(textBox3.Text);
                // Send it
                connection.Multicast(msg);
            } 
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (connection != null) connection.Disconnect();
            Environment.Exit(0);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SpreadGroup grp = new SpreadGroup(connection, comboBox1.Text);
            
            if (grp != null)
            {
                try
                {
                    grp.Leave();
                    richTextBox1.AppendText("Left " + grp + ".");
                }
                catch (SpreadException se)
                {
                    richTextBox1.AppendText("Not joined to this Group\n");
                }
            }
            else
            {
                richTextBox1.AppendText("No group to leave.");
            }
        }
    }
}
