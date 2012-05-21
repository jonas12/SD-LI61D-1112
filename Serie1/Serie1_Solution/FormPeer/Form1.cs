using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CommonInterface;

namespace FormPeer
{
    public partial class Form1 : Form
    {
        private IPeer p;

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
            sending.AppendText("Something Somethig\n");
        }
    }
}
