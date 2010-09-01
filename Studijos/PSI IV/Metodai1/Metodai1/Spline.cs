using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Metodai1
{

    class Spline
    {
        public Spline(AbstractMathFunction function)
        {
            this.function = function;
        }

        private List<double> X = new List<double>();
        private List<double> Y = new List<double>();
        private List<double> g = new List<double>();
        private double step;

        private AbstractMathFunction function = null;
        
        public void findSpline(System.Windows.Forms.RichTextBox output, double min, double max, int pointCount)
        {
            double a = function.getMinPossibleValueInInterval(min, max);
            double b = function.getMaxPossibleValueInInterval(min, max);
            step = (b - a) / (double)pointCount;
            double curX = a;
            X = new List<double>();
            Y = new List<double>();
            for (int i = 0; i < pointCount + 1; i++)
            {
                double curY = function.getFunctionValue(curX);
                X.Add(curX);
                Y.Add(curY);
                curX += step;
            }
            output.Clear();            
            TriMatrix matrix = new TriMatrix();
            matrix[0, 0] = 0;
            matrix[1, 0] = 1;
            matrix[2, 0] = 0;
            matrix[0] = 0;
            for (int i = 0; i < X.Count - 2; i++)
            {
                matrix[0, i + 1] = step;
                matrix[1, i + 1] = 4 * step;
                matrix[2, i + 1] = step;
                matrix[i + 1] = 6 * (Y[i + 2] - 2 * Y[i + 1] + Y[i]) / step;
            }
            matrix[0, X.Count - 1] = 0;
            matrix[1, X.Count - 1] = 1;
            matrix[0, X.Count - 1] = 0;
            matrix[X.Count - 1] = 0;
            g = matrix.Solve();
            for (int i = 0; i < X.Count; i++)
            {
                output.AppendText("g" + i.ToString() + " = " + g[i].ToString("0.00000") + "; ");
            }
            output.AppendText("\n\n");


            for (int i = 0; i < X.Count - 1; i++)
            {
                double Ei = (Y[i + 1] - Y[i]) / step - step * ((g[i + 1] / 2 + g[i]) / 3);
                double Gi = g[i] / 2;
                double Hi = (g[i + 1] - g[i]) / (6 * step);
                output.AppendText("i = " + i.ToString() + " : e" + i.ToString() + " = " +
                                  Ei.ToString("0.000000") +
                                  "; G" + i.ToString() + " = " +
                                  Gi.ToString("0.000000") + "; H" + i.ToString() + " = " +
                                  Hi.ToString("0.000000") + "\n");
            }

            output.AppendText("\n\n");
        }

        #region <Results Displaying>

        /*
        public string functionLine(int i)
        {
            double xi_2 = X[i] * X[i];
            double xi_3 = xi_2 * X[i];
            double Hi = (g[i + 1] - g[i]) / (6 * step);
            double Ei = (Y[i + 1] - Y[i]) / step - step * ((g[i + 1] / 2 + g[i]) / 3);
            double koef1 = Y[i] - X[i] * Ei + xi_2 * g[i] / 2 - xi_3 * Hi;
            double koef2 = Ei - 2 * (g[i] / 2) * X[i] + 3 * Hi * xi_2;
            double koef3 = g[i] / 2 - 3 * Hi * X[i];
            double koef4 = Hi;

            string result = "S(x) = " + koef1.ToString("0.000000") + ((koef2 >= 0) ? "+" : "") + koef2.ToString("0.000000") + "*x" +
                            ((koef3 >= 0) ? "+" : "") + koef3.ToString("0.000000") + "*x^2" + ((koef4 >= 0) ? "+" : "") + koef4.ToString("0.000000") + "*x^3";
            result += "   " + X[i].ToString("0.00") + " <= x < " + X[i + 1].ToString("0.00") + "\n";
            return result;
        }
        */

        /*
                private void printValuesTable(System.Windows.Forms.RichTextBox output)
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

         */

        #endregion
    }
}
