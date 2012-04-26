using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ClassCalc;

namespace WinCalc
{
    public partial class Form1 : Form
    {
        private Calc mycalc;
        private bool withcallback = false;
        public Form1()
        {
            InitializeComponent();
            AppDomain myDomain = AppDomain.CurrentDomain;
            this.Text = myDomain.FriendlyName;
            
            mycalc = new Calc();
            mycalc.Evtdbz += new DelDivisionByZero(this.CallBackDivisonByZero);
            withcallback = true;
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            try
            {
                int op1 = int.Parse(textOp1.Text);
                int op2 = int.Parse(textOp2.Text);
                textRes.Text = mycalc.add(op1, op2).ToString();
            }
            catch (Exception)
            {
                textRes.Text = "operando inválido";
            }
        }

        private void btSub_Click(object sender, EventArgs e)
        {
            try
            {
                int op1 = int.Parse(textOp1.Text);
                int op2 = int.Parse(textOp2.Text);
                textRes.Text = mycalc.sub(op1, op2).ToString();
            }
            catch (Exception)
            {
                textRes.Text = "operando inválido";
            }
        }

        private void btMult_Click(object sender, EventArgs e)
        {
            try
            {
                int op1 = int.Parse(textOp1.Text);
                int op2 = int.Parse(textOp2.Text);
                textRes.Text = mycalc.mult(op1, op2).ToString();
            }
            catch (Exception)
            {
                textRes.Text = "operando inválido";
            }
        }
        private void CallBackDivisonByZero(int Dividendo, int Divisor)
        {
            //System.Windows.Forms.MessageBox.Show("Divisão por Zero","INFO");
            textRes.Text = "Divisão por Zero:" + Dividendo.ToString() + "/" + Divisor.ToString();

        }
        private void btDiv_Click(object sender, EventArgs e)
        {

            try
            {
                int op1 = int.Parse(textOp1.Text);
                int op2 = int.Parse(textOp2.Text);
                textRes.Text = mycalc.div(op1, op2).ToString();
            }
            catch (DivideByZeroException dbze)
            {
                if (!withcallback) textRes.Text = dbze.Message;

            }catch (Exception) {
                textRes.Text = "operando inválido";
            }
        }

        private void btClear_Click(object sender, EventArgs e)
        {
			textOp1.Text="";textOp2.Text="";textRes.Text="";
		}
        

        
    }
}
