using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Metodai1
{
    class ApproxValue
    {
        public ApproxValue(AbstractMathFunction function, double min, double max, int pointCount)
        {
            this.function = function;
            Min = min;
            Max = max;
            PointCount = pointCount;
            findSpline();
        }

        private List<double> X = new List<double>();
        private List<double> Y = new List<double>();
        private List<double> g = new List<double>();
        private double step;
        private bool splineFound = false;

        public double Min
        {
            get;
            set;
        }

        public double Max
        {
            get;
            set;
        }

        public int PointCount
        {
            get;
            set;
        }

        private AbstractMathFunction function = null;
        
        private void findSpline()
        {
            double a = function.getMinPossibleValueInInterval(Min, Max);
            double b = function.getMaxPossibleValueInInterval(Min, Max);
            step = (b - a) / (double)PointCount;
            double curX = a;
            X = new List<double>();
            Y = new List<double>();
            for (int i = 0; i < PointCount + 1; i++)
            {
                double curY = function.getFunctionValue(curX);
                X.Add(curX);
                Y.Add(curY);
                curX += step;
            }
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
            splineFound = true;
        }


        public double approxY(double x)
        {
            if (!splineFound)
            {
                findSpline();
            }
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
                double Hi = (g[i + 1] - g[i]) / (6 * step);
                double Ei = (Y[i + 1] - Y[i]) / step - step * ((g[i + 1] / 2 + g[i]) / 3);
                double result = Y[i] + xMinus * (Ei + xMinus * ((g[i] / 2) + Hi * xMinus));
                return result;
            }            
        }
    }
}
