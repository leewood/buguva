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
            AbstractMathFunction function = null;
            if (radioButton1.Checked)
            {
                a = -3;
                b = 2;
                function = new MathFunction51();
            }
            else if (radioButton2.Checked) 
            {
                a = 0;
                b = 4;
                function = new MathFunction52();
            }
            else
            {
                a = -10;
                b = 10;
                function = new MathFunctionLog();
            }

            Spline tab3 = new Spline(function);
            tab3.findSpline(richTextBox5, a, b, 10);
            button4.Enabled = true;
            textBox3.Enabled = true;
        }
        
        private void button4_Click(object sender, EventArgs e)
        {
            double a, b;
            AbstractMathFunction function = null;
            if (radioButton1.Checked)
            {
                a = -3;
                b = 2;
                function = new MathFunction51();
            }
            else if (radioButton2.Checked)
            {
                a = 0;
                b = 4;
                function = new MathFunction52();
            }
            else
            {
                a = -10;
                b = 10;
                function = new MathFunctionLog();
            }

            try
            {
                ApproxValue tab3 = new ApproxValue(function, a, b, 10);
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
