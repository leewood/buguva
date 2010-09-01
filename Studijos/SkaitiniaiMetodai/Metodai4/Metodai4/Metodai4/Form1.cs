using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Metodai4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        double paskutinis = -1;

        private void button1_Click(object sender, EventArgs e)
        {
            double h = 0.1;
            try
            {
                h = double.Parse(textBox1.Text);
            }
            catch (Exception)
            {
            }
            RungesKuto3Lygis ru = new RungesKuto3Lygis();
            ru.A2 = 0.5;
            ru.A3 = 1;
            ru.B21 = 0.5;
            ru.B31 = -1;
            ru.B32 = 2;
            ru.O1 = 1.0 / 6.0;
            ru.O2 = 4.0 / 6.0;
            ru.O3 = ru.O1;

            /*
            ru.A2 = 2/3;
            ru.A3 = 2/3;
            ru.B21 = 2/3;
            ru.B31 = 0;
            ru.B32 = 2/3;
            ru.O1 = 2.0 / 8.0;
            ru.O2 = 3.0 / 8.0;
            ru.O3 = 3.0 / 8.0;
            */
            //ru.MaxT = 3.14;
            
            //Function2Arg f = (x, u) => 1 - Math.Sin(u + 2 * x) + (0.5 * u) / (2 + x);
            Function2Arg f = (x, u) => 1 - Math.Sin(u + 2 * x) + (0.5 * u) / (2 + x);
            //Function2Arg f = (u, x) => 1 - Math.Sin(u + 2 * x) + (0.5 * u) / (2 + x);
            //Function2Arg f = (x, u) => 1;
            //Function2Arg f = (x, u) => 2*x;
            //Function2Arg f = (x, u) => -u + Math.Sin(x);
            if (paklaidos.Count == 0)
            {
                RungesKuto3Lygis ru2 = new RungesKuto3Lygis();
                ru2.A2 = 0.5;
                ru2.A3 = 1;
                ru2.B21 = 0.5;
                ru2.B31 = -1;
                ru2.B32 = 2;
                ru2.O1 = 1.0 / 6.0;
                ru2.O2 = 4.0 / 6.0;
                ru2.O3 = ru.O1;
                double res2 = ru2.calc(h * 2, f);
                paklaidos.Add(res2);
            }
            double eps = ru.paklaida(h, f);
            ru.output = listView2;
            double res = ru.calc(h, f);
            richTextBox1.Text = ru.forMatlab();
            
            
            if (paklaidos.Count > 0)
            {
                eps = Math.Abs(paklaidos[paklaidos.Count - 1] - res) / 7;
            }            
            paklaidos.Add(res);
            listView4.Items.Add(new ListViewItem(new string[] { res.ToString(), eps.ToString(), ((paskutinis >= 0)? (paskutinis / eps).ToString(): "")}));
            paskutinis = eps;
            label3.Text = eps.ToString();
        }

        double ankst = -1;
        private void button2_Click(object sender, EventArgs e)
        {
            GausoLygis3 met = new GausoLygis3();
            int N = 10;
            try
            {
                N = int.Parse(textBox2.Text);
            }
            catch (Exception)
            {
            }
            FunctionDel f = t => Math.Sin(2 * t) * Math.Cos(3 * t);
            double result = met.calcByN(f, 0, Math.PI / 2, N);
            double paklaida = RungesPaklaida.GautiPaklaida(met, 0, Math.PI / 2, N, f);
            string d = (ankst >= 0) ? (ankst / paklaida).ToString() : "";
            ankst = paklaida;
            listView3.Items.Add(new ListViewItem(new string[] { N.ToString(), result.ToString(), paklaida.ToString(),  d}));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Trapecijos met = new Trapecijos();
            int N = 10;
            try
            {
                N = int.Parse(textBox3.Text);
            }
            catch (Exception)
            {
            }
            FunctionDel f = t => Math.Sin(2 * t) * Math.Cos(3 * t);
            double result = met.calcByN(f, 0, Math.PI / 2, N);
            double paklaida = RungesPaklaida.GautiPaklaida(met, 0, Math.PI / 2, N, f);
            listView1.Items.Add(new ListViewItem(new string[] { N.ToString(), result.ToString(), paklaida.ToString() }));
        }

        List<double> paklaidos = new List<double>();

        private void button4_Click(object sender, EventArgs e)
        {
            listView4.Items.Clear();
            paskutinis = -1;
            double h = 0.1;
            try
            {
                h = double.Parse(textBox1.Text);
            }
            catch (Exception)
            {
            }
            for (int k = 0; k < 10; k++)
            {
                RungesKuto3Lygis ru = new RungesKuto3Lygis();
                ru.A2 = 0.5;
                ru.A3 = 1;
                ru.B21 = 0.5;
                ru.B31 = -1;
                ru.B32 = 2;
                ru.O1 = 1.0 / 6.0;
                ru.O2 = 4.0 / 6.0;
                ru.O3 = ru.O1;
                //ru.MaxT = Math.PI;
                Function2Arg f = (x, u) => 1 - Math.Sin(u + 2 * x) + (0.5 * u) / (2 + x);
                //Function2Arg f = (x, u) => -u + Math.Sin(x);
                double eps = ru.paklaida(h, f);
                ru.output = listView2;
                double res = ru.calc(h, f);
                richTextBox1.Text = ru.forMatlab();
                label3.Text = eps.ToString();

                listView4.Items.Add(new ListViewItem(new string[] { res.ToString(), eps.ToString(), ((paskutinis >= 0) ? (paskutinis / eps).ToString() : "") }));
                paskutinis = eps;
                h = h / 2;
            }
        }
    }
}
