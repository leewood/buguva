using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Metodai1
{

    public abstract class AbstractSpline
    {

        private List<double> X = new List<double>();
        private List<double> Y = new List<double>();
        private List<double> g = new List<double>();
        private double step;
                
        public void findSpline(System.Windows.Forms.RichTextBox output, double min, double max, int pointCount)
        {
            double a = getMinPossibleValueInInterval(min, max);
            double b = getMaxPossibleValueInInterval(min, max);
            step = (b - a) / (double)pointCount;
            double curX = a;
            X = new List<double>();
            Y = new List<double>();
            for (int i = 0; i < pointCount + 1; i++)
            {
                double curY = getFunctionValue(curX);
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


        protected abstract double getMinPossibleValueInInterval(double start, double end);
        protected abstract double getMaxPossibleValueInInterval(double start, double end);
        protected abstract double getFunctionValue(double x);
        
    }
}
