using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Metodai3
{
    class RelaksacijosMetodas
    {
        public List<double> B = new List<double>();
        public List<List<double>> A = new List<List<double>>();
        public ListView output = null;

        List<List<double>> x = new List<List<double>>();
        int n = 0;
        double w = 0; 

        public double calcZeidelX(int k, int i)
        {
            double sum1 = 0;
            for (int j = 1; j <= i - 1; j++)
            {
                sum1 += A[i - 1][j - 1] * x[k][j - 1];
            }
            double sum2 = 0;
            for (int j = i + 1; j <= n; j++)
            {
                sum2 += A[i - 1][j - 1] * x[k - 1][j - 1];
            }
            return (B[i - 1] - sum1 - sum2) / A[i - 1][i - 1];
        }

        public double calcX(int k, int i)
        {
            return w * calcZeidelX(k, i) + (1 - w) * x[k - 1][i - 1];
        }

        public double calcLine()
        {
            int k = x.Count;
            x.Add(new List<double>());
            double minDif = -1;
            double diffModule = 0;
            List<string> resO = new List<string>();
            List<string> differ = new List<string>();
            string vertorS = "(";
            string sep = "";
            resO.Add(k.ToString());
            for (int i = 1; i <= n; i++)
            {
                double res = calcX(k, i);
                x[k].Add(res);
                resO.Add(res.ToString(Format));
                double diff = Math.Abs(x[k - 1][i - 1] - x[k][i - 1]);
                differ.Add(diff.ToString(Format));
                vertorS += sep + diff.ToString(Format);
                sep = ", ";
                if ((minDif < 0) || (minDif < diff))
                {
                    minDif = diff;
                }
                diffModule += diff * diff;
            }
            vertorS += ")";
            resO.AddRange(differ);
            resO.Add(vertorS);
            resO.Add(minDif.ToString(Format));
            resO.Add(diffModule.ToString(Format2));
            if (output != null)
            {
                output.Items.Add(new ListViewItem(resO.ToArray()));
            }
            return (diffModule > minDif * minDif) ? diffModule : minDif * minDif;
        }

        public int places = 0;

        public int numberAfterComma(double pres)
        {
            double current = 1;
            int n = 0;
            while (current > pres)
            {
                current /= 10;
                n++;
            }
            return n + 1;
        }

        private string _format = null;

        private string _format2 = null;
        public string Format2
        {
            get
            {
                if (_format2 == null)
                {
                    _format2 = "0.";
                    for (int i = 0; i < places * 2 + 2; i++)
                    {
                        _format2 += "0";
                    }

                }
                return _format2;
            }
        }


        public string Format
        {
            get
            {
                if (_format == null)
                {
                    _format = "0.";
                    for (int i = 0; i < places + 1; i++)
                    {
                        _format += "0";
                    }
                    
                }
                return _format;
            }
        }


        public void calc(List<double> initX, double w, double presc)
        {
            x = new List<List<double>>();            
            places = numberAfterComma(presc);
            x.Add(initX);
            n = initX.Count;
            if (output != null)
            {
                output.Columns.Clear();
                ColumnHeader ih = new ColumnHeader();
                ih.Text = "i = ";
                output.Columns.Add(ih);
                for (int i = 0; i < n; i++)
                {
                    ColumnHeader header = new ColumnHeader();
                    header.Text = "x" + (i + 1).ToString() + " = ";
                    output.Columns.Add(header);
                }
                for (int i = 0; i < n; i++)
                {
                    ColumnHeader header = new ColumnHeader();
                    header.Text = "dx" + (i + 1).ToString() + " = ";
                    output.Columns.Add(header);
                }
                ih = new ColumnHeader();
                ih.Text = "dx = ";
                output.Columns.Add(ih);
                ih = new ColumnHeader();
                ih.Text = "max(dx)";
                output.Columns.Add(ih);
                ih = new ColumnHeader();
                ih.Text = "|dx| = ";
                output.Columns.Add(ih);
                output.Items.Clear();
            }
            this.w = w;
            double diff = 0;
            do
            {
                diff = calcLine();
            } while (diff >= presc * presc);
            calcLine();            
        }


    }
}
