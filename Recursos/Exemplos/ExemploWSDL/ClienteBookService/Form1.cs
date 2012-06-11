using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Web.Services.Protocols; // SoapException
using ClienteBookService.SD;

namespace ClienteBookService
{
    public partial class Form1 : Form
    {
        private BookService bserv;
        public Form1()
        {
            InitializeComponent();
            bserv = new BookService();
          
        }

        private void BtgetISBN_Click(object sender, EventArgs e)
        {
            textBox2.Text = bserv.getISBN(textBox1.Text);
        }

        private void BtgetPrice_Click(object sender, EventArgs e)
        {
            try
            {
                textBox3.Text = (bserv.getBookPrice(textBox4.Text)).ToString();
            }
            catch (SoapException ex)
            {
                MessageBox.Show(ex.Message + "\n" +
                         "Code:" + ex.Code + "\n" +
                         "Details:" + ex.Detail.OuterXml + "\n" +
                         "Actor role:" + ex.Actor);
            }
        }

        private void BtsubmitPOrder_Click(object sender, EventArgs e)
        {
            PurchaseOrder po = new PurchaseOrder();
            po.accountName = textBox6.Text; po.accountNumber = int.Parse(textBox7.Text);
            //  preencher outros detalhes
            textBox5.Text = bserv.submitPOrder(po);
        }



    }
}