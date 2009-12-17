using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KTUzd.Models;
using KTUzd.Solution;

namespace KTUzd.Interface
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private bool locked = false;
        private int oldP = 0;
        private int oldM = 0;
        private int oldQ = 0;


        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            if (!locked)
            {
                locked = true;
                int add = 1;                
                int v = (int)numericUpDown2.Value;
                if (v < oldP)
                {
                    add = -1;
                }
                int m = (int) numericUpDown3.Value;
                while (!MathTools.IsPrimary(v))
                {
                    v += add;
                }
                numericUpDown2.Value = v;
                oldP = v;
                numericUpDown4.Value = (int)Math.Pow(v, m);
                locked = false;
            }
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            if (!locked)
            {
                locked = true;
                int v = (int)numericUpDown2.Value;
                int p = (int)numericUpDown3.Value;
                numericUpDown4.Value = (int)Math.Pow(v, p);
                locked = false;
            }
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            if (!locked)
            {
                locked = true;

                locked = false;
            }
        }

        private Polynomial poly = new Polynomial();

        public void UpdatePolynomial()
        {
            int n = (int)numericUpDown1.Value;
            int p = (int)numericUpDown2.Value;
            int m = (int)numericUpDown3.Value;
            int q = (int)numericUpDown4.Value;
            poly = new Polynomial(new List<int>() {n, 0}, p, m);
            poly[0] = -1;
            label6.Text = poly.ToString();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            UpdatePolynomial();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UpdatePolynomial();
            var result = FactoringAlgorithm.FullFactorization(poly);
            StringBuilder text = new StringBuilder();
            foreach (var polynomial in result)
            {
                text.Append("(" + polynomial.ToString() + ")");
            }
            label8.Text = text.ToString();
        }
    }
}
