using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Metodai3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*
            RelaksacijosMetodas met = new RelaksacijosMetodas();
            met.output = listView1;
            met.A = new List<List<double>>()
            {
                new List<double>() {4, -1, -1},
                new List<double>() {6, 8, 0},
                new List<double>() {-5, 0, 12}
            };
            met.B = new List<double>() { -2, 45, 80 };
            met.calc(new List<double>() {0, 0, 0 }, 1.2, 0.0001);
             */

            JungtiniuGradientu jung = new JungtiniuGradientu();
            jung.output = listView1;
            /*
            jung.calc(
                new List<List<double>>() 
                {
                    new List<double>() { 2, 1, 0.95},
                    new List<double>() { 1, 2, 1},
                    new List<double>() { 0.95, 1, 2}
                },
                new List<double>() { 3.95, 4, 3.95 },
                new List<double>() { 0, 0, 0 }, 0.0001
                );
             */
            jung.calc(calcInitData().Values, GetB().Values[0], new List<double>() { 0, 0, 0, 0 }, 0.000000000000001);
        }

        private Matrix calcInitData()
        {
            Matrix C = new Matrix();
            C.Values = new List<List<double>>() 
            {
                new List<double>() { 0.01, 0, 0, 1 },
                new List<double>() { 0, 0.01, 0, 0 },
                new List<double>() { 0, 0, 0.01, 0 },
                new List<double>() { 0, 0, 0, 0.01 }
            };
            Matrix D = new Matrix();
            D.Values = new List<List<double>>()
            {
                new List<double>() { 1.342, 0.202, -0.599, 0.432 },
                new List<double>() { 0.202, 1.342, 0.202, -0.599 },
                new List<double>() { -0.599, 0.202, 1.342, 0.202 },
                new List<double>() { 0.432, -0.599, 0.202, 1.342 }
            };
            double k = 52;
            Matrix A = D.Add(C.MulNumber(k));
            return A;
        }

        private Matrix GetB()
        {
            Matrix B = new Matrix();
            B.Values = new List<List<double>>
            {
                new List<double>() {1.941, -0.230, -1.941, 0.230}
            };
            return B;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            double w = 1.5;
            try
            {
                w = double.Parse(textBox1.Text);
            }
            catch (Exception)
            {
            }
            RelaksacijosMetodas met = new RelaksacijosMetodas();
            met.output = listView2;
            met.A = calcInitData().Values;
            met.B = GetB().Values[0];
            /*
            met.A = new List<List<double>>()
            {
                new List<double>() {4, -1, -1},
                new List<double>() {6, 8, 0},
                new List<double>() {-5, 0, 12}
            };
            met.B = new List<double>() { -2, 45, 80 };
             */
            met.calc(new List<double>() { 0, 0, 0, 0 }, w, 0.000001);
        }
    }
}
