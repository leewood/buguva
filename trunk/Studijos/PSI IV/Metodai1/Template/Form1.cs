using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Metodai1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }



        

        private void button5_Click(object sender, EventArgs e)
        {
            double a, b;
            AbstractSpline tab3 = null;
            if (radioButton1.Checked)
            {
                a = -3;
                b = 2;
                tab3 = new SplineMathFunction51();
            }
            else if (radioButton2.Checked) 
            {
                a = 0;
                b = 4;
                tab3 = new SplineMathFunction52();
            }
            else
            {
                a = -10;
                b = 10;
                tab3 = new SplineMathFunctionLog();
            }

            
            tab3.findSpline(richTextBox5, a, b, 10);
            button4.Enabled = true;
            textBox3.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            double a, b;
            AbstractApproxValue tab3 = null;
            if (radioButton1.Checked)
            {
                a = -3;
                b = 2;
                tab3 = new ApproxValueMathFunction51();
            }
            else if (radioButton2.Checked)
            {
                a = 0;
                b = 4;
                tab3 = new ApproxValueMathFunction52();
            }
            else
            {
                a = -10;
                b = 10;
                tab3 = new ApproxValueMathFunctionLog();
            }
            try
            {
                tab3.Max = b;
                tab3.Min = a;
                tab3.PointCount = 10;
                double x = double.Parse(textBox3.Text);
                double y = tab3.approxY(x);
                richTextBox5.AppendText("F(" + x.ToString("0.00000") + ") = " + y.ToString("0.000000") + "\n");
            }
            catch (FormatException)
            {
                MessageBox.Show("Įvedėte blogą X reikšmę");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
