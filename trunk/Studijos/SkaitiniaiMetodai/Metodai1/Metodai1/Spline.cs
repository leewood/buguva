using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Metodai1
{
    public delegate double MathFunction(double x);

    class Spline
    {
        public Spline()
        {
            X = new List<double>();
            Y = new List<double>();
        }

        public List<double> X
        {
            get;
            set;
        }

        public List<double> Y
        {
            get;
            set;
        }

        private List<double> g = new List<double>();
        private List<double> h = new List<double>();
        private List<double> f = new List<double>();

        public double G(int index)
        {
            return g[index] / 2;
        }

        public double HSolved(int index)
        {
            return (g[index + 1] - g[index]) / (6 * H(index));
        }

        public double e(int index)
        {
            return (Y[index + 1] - Y[index]) / H(index) - H(index) * ((g[index + 1] / 2 + g[index]) / 3);
        }

        public double F(int index1, int index2)
        {
            if (index1 + 1 < f.Count)
            {
                return f[index1];
            }
            else
            {
                double result = (Y[index2] - Y[index1]) / H(index1);
                if (index1 == f.Count)
                {
                    f.Add(result);
                }
                return result;
            }
        }

        public string functionLine(int i)
        {
            double xi_2 = X[i] * X[i];
            double xi_3 = xi_2 * X[i];
            double koef1 = Y[i] - X[i] * e(i) + xi_2 * G(i) - xi_3 * HSolved(i);
            double koef2 = e(i) - 2 * G(i) * X[i] + 3 * HSolved(i) * xi_2;
            double koef3 = G(i) - 3 * HSolved(i) * X[i];
            double koef4 = HSolved(i);

            string result = "S(x) = " + koef1.ToString("0.000000") + ((koef2 >= 0) ? "+" : "") + koef2.ToString("0.000000") + "*x" +
                            ((koef3 >= 0) ? "+" : "") + koef3.ToString("0.000000") + "*x^2" + ((koef4 >= 0) ? "+" : "") + koef4.ToString("0.000000") + "*x^3";
            result += "   " + X[i].ToString("0.00") + " <= x < " + X[i + 1].ToString("0.00") + "\n";
            return result;
        }

        public void findG()
        {
            TriMatrix matrix = new TriMatrix();
            matrix[0, 0] = 0;
            matrix[1, 0] = 1;
            matrix[2, 0] = 0;
            matrix[0] = 0;
            for (int i = 0; i < X.Count - 2; i++)
            {
                matrix[0, i + 1] = H(i);
                matrix[1, i + 1] = 2 * (H(i) + H(i + 1));
                matrix[2, i + 1] = H(i + 1);
                matrix[i + 1] = 6 * (F(i + 1, i + 2) - F(i, i + 1));
            }
            matrix[0, X.Count - 1] = 0;
            matrix[1, X.Count - 1] = 1;
            matrix[0, X.Count - 1] = 0;
            matrix[X.Count - 1] = 0;
            g = matrix.Solve();
        }

        public static Spline FromBitNumber(int number)
        {
            List<double> xValues = new List<double>();
            List<double> yValues = new List<double>();
            for (int i = 0; i < 6; i++)
            {
                double x = number % 2;
                number = number / 2;
                double y = x * 209;
                xValues.Add(5 - i);
                yValues.Add(y);
            }
            Spline result = new Spline();

            result.X = new List<double>();
            result.Y = new List<double>();
            for (int i = 0; i < 6; i++)
            {
                result.X.Add(xValues[5 - i]);
                result.Y.Add(yValues[5 - i]);
            }
            return result;
        }


        public static Spline FromGivenTable(string table)
        {
            string[] lines = table.Split(new char[] { '\n' });
            string[] x = lines[0].Split(new char[] { ' ' });
            string[] y = lines[1].Split(new char[] { ' ' });            
            Spline result = new Spline();
            result.X = new List<double>();
            result.Y = new List<double>();
            for (int i = 0; i < x.Length; i++)
            {
                double curX = double.Parse(x[i]);
                double curY = double.Parse(y[i]);
                result.X.Add(curX);
                result.Y.Add(curY);                
            }
            return result;
        }


        public static Spline FromFunction(MathFunction function, double a, double b, int pointCount)
        {
            double step = (b - a) / (double)pointCount;
            double curX = a;
            Spline result = new Spline();
            result.X = new List<double>();
            result.Y = new List<double>();
            for (int i = 0; i < pointCount + 1; i++)
            {
                double curY = function(curX);
                result.X.Add(curX);
                result.Y.Add(curY);
                curX += step;
            }
            return result;
        }

        public double H(int index)
        {
            if (index + 1 < h.Count)
            {
                return h[index];
            }
            else
            {
                double result = X[index + 1] - X[index];
                if (index == h.Count)
                {
                    h.Add(result);
                }
                return result;
            }
        }

        public void findSpline(System.Windows.Forms.RichTextBox output)
        {
            output.Clear();
            printValuesTable(output);
            findG();
            for (int i = 0; i < X.Count; i++)
            {
                output.AppendText("g" + i.ToString() + " = " + g[i].ToString("0.00000") + "; ");
            }
            output.AppendText("\n\n");
            for (int i = 0; i < X.Count - 1; i++)
            {
                output.AppendText("i = " + i.ToString() + " : e" + i.ToString() + " = " + e(i).ToString("0.000000") +
                                  "; G" + i.ToString() + " = " + G(i).ToString("0.000000") + "; H" + i.ToString() + " = " + HSolved(i).ToString("0.000000") + "\n");                                                  
            }

            output.AppendText("\n\n");
            for (int i = 0; i < X.Count - 1; i++)
            {
                output.AppendText(functionLine(i));
            }

        }


        public void printValuesTable(System.Windows.Forms.RichTextBox output)
        {
            string forMatlab = "S := spline([";
            output.AppendText("\n");
            output.AppendText("--------------------------------------------\n");
            output.AppendText("X = ");
            for (int i = 0; i < X.Count; i++)
            {
                output.AppendText(String.Format("{0, 9}|", X[i].ToString("0.00000")));
                forMatlab += X[i].ToString("0.00000").Replace(",", ".");
                if (i < X.Count - 1)
                {
                    forMatlab += ",";
                }
                else
                {
                    forMatlab += "],[";
                }
            }
            output.AppendText("\n");
            output.AppendText("Y = ");
            for (int i = 0; i < Y.Count; i++)
            {
                output.AppendText(String.Format("{0, 9}|", Y[i].ToString("0.00000")));
                forMatlab += Y[i].ToString("0.00000").Replace(",", ".");
                if (i < Y.Count - 1)
                {
                    forMatlab += ",";
                }
                else
                {
                    forMatlab += "],x,cubic);\nplot(S, x = " + X[0].ToString("0") + ".." + X[X.Count - 1].ToString("0") + ", thickness = 2);\n";
                }
            }
            output.AppendText("\n");
            output.AppendText("--------------------------------------------\n");
            output.AppendText("For MAPLE:\n");
            output.AppendText(forMatlab);
            output.AppendText("\n");
        }

        public double approxY(double x)
        {
            if ((x < X[0]) || (x > X[X.Count - 1]))
            {
                throw new Exception("Išeina už rėžių");
            }
            else
            {
                int i = 0;
                while (X[i] < x)
                {
                    i++;
                }
                if (i == 0)
                {
                    i = 1;
                }
                i = i - 1;
                double xMinus = x - X[i];
                double result = Y[i] + xMinus * (e(i) + xMinus * (G(i) + HSolved(i) * xMinus));
                return result;
            }
            return 0;
        }
    }
}
