using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Metodai2
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
                double a = double.Parse(textBox1.Text);
                double b = double.Parse(textBox2.Text);
                double eps = double.Parse(textBox3.Text);
                int maxN = int.Parse(textBox7.Text);
                Pusiaukirtos pus = new Pusiaukirtos(x => x * (1 - x), a, b, eps, maxN);
                pus.calcApprox(richTextBox1, listView1);
            }
            catch (Exception)
            {
                MessageBox.Show("Neteisingi pradiniai duomenys");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                double x1 = double.Parse(textBox4.Text);
                double x2 = double.Parse(textBox5.Text);
                double eps = double.Parse(textBox6.Text);
                int maxN = int.Parse(textBox8.Text);
                KirstiniuMetodas kirs = new KirstiniuMetodas(x => Math.Cos(x) - x, x1, x2, eps, maxN);                
                kirs.calcApprox(richTextBox2, listView2);
            }
            catch (Exception)
            {
                MessageBox.Show("Neteisingi pradiniai duomenys");
            }
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
