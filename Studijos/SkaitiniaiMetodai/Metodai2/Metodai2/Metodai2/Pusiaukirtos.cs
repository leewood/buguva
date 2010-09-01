using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Metodai2
{

    public delegate double Funkcija(double x);

    class Main
    {

        void fff()
        {
            //Pusiaukirtos pus = new Pusiaukirtos(x => x * x);
        }

        double f(double x)
        {
            return x;
        }
    }


    class Pusiaukirtos
    {
        Funkcija funkcija;
        double IntervalasA { get; set; }
        double IntervalasB { get; set; }
        double Epsilon { get; set; }
        int MaksimalusN { get; set; }

        

        public Pusiaukirtos(Funkcija funkcija, double a0, double b0, double eps, int maxN)
        {
            this.funkcija = funkcija;
            IntervalasA = a0;
            IntervalasB = b0;
            Epsilon = eps;
            MaksimalusN = maxN;
        }

        public double calcApprox(RichTextBox output, ListView output2)
        {
            if (funkcija != null)
            {
                int n = 0;
                double an = IntervalasA;
                double bn = IntervalasB;
                if (funkcija(an) * funkcija(bn) >= 0)
                {
                    output.AppendText("Klaida, nepatenkinta sąlyga: f(a_0) * f(b_0) < 0");
                    return 0;
                }
                else
                {
                    while (true)
                    {
                        output.AppendText(String.Format("Žingsnis {0}: ", n));
                        double cn = (an + bn) / 2;
                        output.AppendText(String.Format("C_{0} = {1}; ", n, cn));
                        output2.Items.Add(new ListViewItem(new string[] { n.ToString(), an.ToString(), bn.ToString(), cn.ToString(), funkcija(cn).ToString(), Math.Abs(an - bn).ToString()}));
                        if (Math.Abs(an - bn) < 2 * Epsilon)
                        {
                            output.AppendText(String.Format(" |A_{0} - B_{1}| < 2 * {2}|; Exiting\n", n, n, Epsilon));
                            output.AppendText(String.Format("Result = {0}\n", cn));
                            return cn;
                        }
                        if (Math.Abs(funkcija(cn)) < Epsilon)
                        {
                            output.AppendText(String.Format(" f(C_{0}) < 2 * {1}; Exiting\n", n, Epsilon));
                            output.AppendText(String.Format("Result = {0}\n", cn));
                            return cn;
                        }
                        if (funkcija(cn) * funkcija(an) < 0)
                        {
                            bn = cn;
                        }
                        else
                        {
                            an = cn;
                        }
                        output.AppendText(String.Format("A_{0} = {1}; B_{2} = {3}\n", n + 1, an, n + 1, bn));
                        n++;
                        if (n > MaksimalusN)
                        {
                            output.AppendText(String.Format(" N > {0}; Exiting\n", MaksimalusN));
                            output.AppendText(String.Format("Result = {0}\n", cn));
                            return cn;
                        }
                    }
                }
            }
            else
            {
                output.AppendText("Nenurodėte funkcijos\n");
                return 0;
            }
        }


    }
}
