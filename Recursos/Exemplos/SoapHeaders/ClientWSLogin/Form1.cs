using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using ClientWSLogin.deetc.sd;

namespace ClientWSLogin
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WSLogin wslprx = new WSLogin();
            //wslprx.Credentials = CredentialCache.DefaultCredentials;
           
            SoapHdLogin soaphdl = new SoapHdLogin();
           
            soaphdl.UserName = textBox1.Text; soaphdl.TokenKey = textBox2.Text;
            soaphdl.MustUnderstand = true;
            soaphdl.ts = new TimeStamp();
            soaphdl.ts.dtIn = DateTime.Now;
            wslprx.SoapHdLoginValue = soaphdl;
            
            try
            {
                textBox3.Text = wslprx.Login(textBox1.Text);
                textBox1.Text = wslprx.SoapHdLoginValue.UserName;
                textBox2.Text = wslprx.SoapHdLoginValue.TokenKey;
                textBox5.Text = wslprx.SoapHdLoginValue.ts.dtIn.ToString();
                textBox6.Text = wslprx.SoapHdLoginValue.ts.dtOut.ToString();
                // Note que o SoapHDMoreInfo só tem valor após a chamada
                textBox4.Text = wslprx.SoapHdMoreInfoValue.info;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        
    }
}
