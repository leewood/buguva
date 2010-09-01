using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Metodai2
{
    class KirstiniuMetodas
    {
        public double X1Start
        {
            get;
            set;
        }

        public double X2Start
        {
            get;
            set;
        }

        public double Epsilon
        {
            get;
            set;
        }

        Funkcija funkcija;

        public int MaksimalusN
        {
            get;
            set;
        }

        public KirstiniuMetodas(Funkcija funkcija, double x1, double x2, double eps, int maksN)
        {
            this.funkcija = funkcija;
            this.X1Start = x1;
            this.X2Start = x2;
            this.Epsilon = eps;
            this.MaksimalusN = maksN;
        }

        public double calcApprox(RichTextBox output, ListView output2)
        {
            if (funkcija != null)
            {                
                int i = 0;
                double x1 = X1Start;
                double x2 = X2Start;
                while (true)
                {
                    double x3 = x2 - funkcija(x2) * (x2 - x1) / (funkcija(x2) - funkcija(x1));
                    output.AppendText(String.Format("X_{0} = {1}\n", i + 2, x3));
                    output2.Items.Add(new ListViewItem(new string[] { i.ToString(), x1.ToString(), x2.ToString(), x3.ToString(), funkcija(x1).ToString(), funkcija(x2).ToString(), Math.Abs(x3 - x2).ToString(), (funkcija(x2) - funkcija(x1)).ToString()}));
                    if (Math.Abs(x3 - x2) < Epsilon)
                    {
                        output.AppendText(String.Format(" |X_{0} - X_{1}| < {2}; Exiting\n", i + 2, i + 1, Epsilon));
                        output.AppendText(String.Format("Rezultatas: {0}\n", x3));
                        return x3;
                    }
                    x1 = x2;
                    x2 = x3;
                    i++;
                    if (i > MaksimalusN)
                    {
                        output.AppendText(String.Format(" i > {0}; Exiting\n", MaksimalusN));
                        output.AppendText(String.Format("Rezultatas: {0}\n", x3));
                        return x3;
                    }
                }
            }
            else
            {
                output.AppendText("Nenurodėte funkcijos");
                return 0;
            }
        }

    }
}
