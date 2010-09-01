using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Metodai4
{
    class RungesKuto3Lygis
    {
        
        public double O1;
        public double O2;
        public double O3;
        public double A2;
        public double A3;
        public double B21;
        public double B31;
        public double B32;
        public double H;
        public double MaxT = 1;

        private List<double> U = new List<double>();

        public ListView output = null;

        public string forMatlab()
        {
            string forMatlab = "S := [";
            for (int i = 0; i < U.Count; i++)
            {
                forMatlab += "[" + (i * H).ToString("0.00000").Replace(",", ".") + ", " + U[i].ToString("0.00000").Replace(",", ".") + "]";
                if (i < U.Count - 1)
                {
                    forMatlab += ",";
                }
                else
                {
                    forMatlab += "];\nplot(S, x = 0..1, style=POINT);\n";
                }
            }
            return forMatlab;
        }

        double result = 0;

        public void calcLine(int N, Function2Arg funkcija, double t)
        {            
                double k1 = funkcija(t, U[N - 1]);
                double k2 = funkcija(t + A2 * H, U[N - 1] + H * B21 * k1);
                double k3 = funkcija(t + A3 * H, U[N - 1] + H * (B31 * k1 + B32 * k2));
                double Un = U[N - 1] + H * (O1 * k1 + O2 * k2 + O3 * k3);
                List<string> text = new List<string>();
                text.Add(t.ToString());
                text.Add(k1.ToString());
                text.Add(k2.ToString());
                text.Add(k3.ToString());
                text.Add(U[N - 1].ToString());
                if (output != null)
                {
                    output.Items.Add(new ListViewItem(text.ToArray()));
                }
                U.Add(Un);
                if (t == MaxT)
                {
                    result = Un;
                }
        }

        public double calc(double h, Function2Arg funkcija)
        {
            int N = ((int)Math.Round(MaxT / h)) + 1;
            U.Clear();
            U.Add(1);
            H = h;
            if (output != null)
            {
                output.Columns.Clear();
                ColumnHeader header = new ColumnHeader();
                header.Text = "t = ";
                output.Columns.Add(header);
                header = new ColumnHeader();
                header.Text = "k1 = ";
                output.Columns.Add(header);
                header = new ColumnHeader();
                header.Text = "k2 = ";
                output.Columns.Add(header);
                header = new ColumnHeader();
                header.Text = "k3 = ";
                output.Columns.Add(header);
                header = new ColumnHeader();
                header.Text = "U = ";
                output.Columns.Add(header);
                output.Items.Clear();
            }
            double t = 0;
            //double t = (N - 1) * H;
            int i = 0;            
            while (t < MaxT)
            {
                calcLine(i + 1, funkcija, t);
                
                if ((t < MaxT) && (t + H > MaxT))
                {
                    t = MaxT;            
                }
                else
                {
                    t += H;
                }                                
                t = Math.Round(t, 6);
                i++;
            }
            calcLine(i + 1, funkcija, t);
            return U[U.Count - 2];
        }

        public double paklaida(double h, Function2Arg funkcija)
        {
            RungesKuto3Lygis temp = new RungesKuto3Lygis();
            temp.O1 = O1;
            temp.O2 = O2;
            temp.O3 = O2;
            temp.A2 = A2;
            temp.A3 = A3;
            temp.B21 = B21;
            temp.B31 = B31;
            temp.B32 = B32;
            temp.MaxT = MaxT;
            temp.H = h * 2;
            RungesKuto3Lygis temp2 = new RungesKuto3Lygis();
            temp2.O1 = O1;
            temp2.O2 = O2;
            temp2.O3 = O2;
            temp2.A2 = A2;
            temp2.A3 = A3;
            temp2.B21 = B21;
            temp2.B31 = B31;
            temp2.B32 = B32;
            temp2.MaxT = MaxT;
            temp2.H = h;
            return Math.Abs(temp2.calc(h, funkcija) - temp.calc(h * 2, funkcija)) / 7;
        }

    }
}
