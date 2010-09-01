using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Metodai3
{
    class JungtiniuGradientu
    {
        Matrix p;
        Matrix z;
        Matrix A;
        Matrix f;
        Matrix x;
        Matrix r;
        public ListView output = null;

        private double GetTeta(int k)
        {
            return Matrix.ScalarVector(z.FromRow(k), p.FromRow(k).Rotate()) / Matrix.ScalarVector(r.FromRow(k), p.FromRow(k).Rotate());
        }

        private double ZSquare(int k)
        {
            return Matrix.ScalarVector(z.FromRow(k), z.FromRow(k).Rotate());
        }

        private double GetBeta(int k)
        {
            return ZSquare(k + 1) / ZSquare(k);
        }

        private void newR(int k)
        {
            Matrix newRM = A.Mul(p.FromRow(k).Rotate()).Rotate();
            r.AddLine(newRM);
        }

        private void newX(int k, double teta)
        {
            Matrix newXM = x.FromRow(k).Sub(p.FromRow(k).MulNumber(teta));
            x.AddLine(newXM);
        }

        private void newZ(int k, double teta)
        {
            Matrix newZM = z.FromRow(k).Sub(r.FromRow(k).MulNumber(teta));
            z.AddLine(newZM);
        }

        private void newP(int k, double beta)
        {
            Matrix newPM = z.FromRow(k + 1).Add(p.FromRow(k).MulNumber(beta));
            p.AddLine(newPM);
        }

        private bool calcLine(int k, double eps)
        {
            newR(k);
            double teta = GetTeta(k);
            newX(k, teta);
            List<string> resO = new List<string>();
            resO.Add(k.ToString());
            for (int i = 0; i < x.Width; i++)
            {
                resO.Add(x[i, k + 1].ToString(Format));
            }           
            newZ(k, teta);
            string zS = "(";
            string separ = "";
            for (int i = 0; i < x.Width; i++)
            {
                resO.Add(z[i, k + 1].ToString(Format));
                zS += separ + z[i, k + 1].ToString(Format);
                separ = ", ";
            }

            double netikt = Matrix.ScalarVector(z.FromRow(k + 1), z.FromRow(k + 1).Rotate());
            zS += ")";
            resO.Add(zS);
            resO.Add(netikt.ToString(Format2));
            if (output != null)
            {
                output.Items.Add(new ListViewItem(resO.ToArray()));
            }

            if (netikt < eps * eps)
            {
                return false;
            }
            double beta = GetBeta(k);
            newP(k, beta);
            return true;
        }

        private void init(List<List<double>> initA, List<double> initF, List<double> initX)
        {
            p = new Matrix();
            z = new Matrix();
            x = new Matrix();
            r = new Matrix();
            x.AddLine(Matrix.FromList(initX));
            f = new Matrix();
            f.AddLine(Matrix.FromList(initF));
            A = new Matrix();
            A.Values = initA;
            z.AddLine(A.Mul(x.FromRow(0).Rotate()).Sub(f.Rotate()).Rotate());
            p = z.Clone();
            if (output != null)
            {
                output.Columns.Clear();
                ColumnHeader ih = new ColumnHeader();
                ih.Text = "i = ";
                output.Columns.Add(ih);
                for (int i = 0; i < initX.Count; i++)
                {
                    ColumnHeader header = new ColumnHeader();
                    header.Text = "x" + (i + 1).ToString() + " = ";
                    output.Columns.Add(header);
                }
                for (int i = 0; i < initX.Count; i++)
                {
                    ColumnHeader header = new ColumnHeader();
                    header.Text = "z" + (i + 1).ToString() + " = ";
                    output.Columns.Add(header);
                }
                ih = new ColumnHeader();
                ih.Text = "z = ";
                output.Columns.Add(ih);
                ih = new ColumnHeader();
                ih.Text = "(z, z) = ";
                output.Columns.Add(ih);
                output.Items.Clear();
            }
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

        public void calc(List<List<double>> initA, List<double> initF, List<double> initX, double eps)
        {            
            int k = 0;
            places = numberAfterComma(eps);
            init(initA, initF, initX);
            if (A.isSimetric())
            {
                while (calcLine(k, eps))
                {
                    k++;
                }
            }
            else
            {
                MessageBox.Show("Klaida, matrica nesimetrine");
            }
        }

    }
}
