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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                TriMatrix matrix = TriMatrix.Load(richTextBox1.Text);
                matrix.LoadResults(richTextBox2.Text);
                if (!matrix.CheckForCorrectness())
                {
                    throw new Exception(matrix.ErrorText);
                }
                List<double> solving = matrix.Solve();
                richTextBox3.Clear();
                richTextBox3.AppendText("Result:\n");
                for (int i = 0; i < solving.Count; i++)
                {
                    richTextBox3.AppendText("x" + i.ToString() + " = " + solving[i].ToString("0.000000") + "\n");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        Spline tab2;

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int number = int.Parse(textBox1.Text);
                tab2 = Spline.FromBitNumber(number);
                tab2.findSpline(richTextBox4);
                button3.Enabled = true;
                textBox2.Enabled = true;
            }
            catch (FormatException)
            {
                MessageBox.Show("Įvedėte blogą studento numerį sąraše");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                double x = double.Parse(textBox2.Text);
                double y = tab2.approxY(x);
                richTextBox4.AppendText("F(" + x.ToString("0.00000") + ") = " + y.ToString("0.000000") + "\n");
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

        Spline tab3;


        double f50(double x)
        {
            double xSqr = x * x;
            return (2 * xSqr + 6) / (xSqr - 2 * x + 5);
        }

        double f52(double x)
        {
            double result = (1 + x) * Math.Sin(x);
            return result;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            double a, b;
            MathFunction function;
            if (radioButton1.Checked)
            {
                a = -3;
                b = 2;
                function = f50;
            }
            else
            {
                a = 0;
                b = 4;
                function = f52;
            }
            tab3 = Spline.FromFunction(function, a, b, 10);
            tab3.findSpline(richTextBox5);
            button4.Enabled = true;
            textBox3.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
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

        Spline tab4;

        private void button7_Click(object sender, EventArgs e)
        {


            tab4 = Spline.FromGivenTable(richTextBox7.Text);
            tab4.findSpline(richTextBox6);
            button6.Enabled = true;
            textBox4.Enabled = true;

        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                double x = double.Parse(textBox4.Text);
                double y = tab4.approxY(x);
                richTextBox6.AppendText("F(" + x.ToString("0.00000") + ") = " + y.ToString("0.000000") + "\n");
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
