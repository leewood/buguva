using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Metodai4
{
    class Trapecijos: IMetodas
    {
        public double calc(FunctionDel function, double a, double b)
        {
            return (b - a) * (function(a) + function(b)) / (double)2;
        }

        

        public double calcByN(FunctionDel function, double a, double b, int N)
        {
            double deltaX = (b - a) / (double)N;
            double x1 = a;
            double x2 = x1 + deltaX;
            double result = 0;
            for (int i = 0; i < N; i++)
            {
                result += calc(function, x1, x2);
                x1 = x2;
                x2 += deltaX;
            }
            return result;
        }

        public double calcByN2(FunctionDel function, double a, double b, int N)
        {
            double result = function(a) + function(b);
            double deltaX = (b - a) / (double)N;
            double curX = a + deltaX;
            for (int i = 0; i < N - 1; i++)
            {
                result += 2 * function(curX);
                curX += deltaX;
            }
            result = result * deltaX;
            return result;
        }


        #region IMetodas Members


        public int tikslumoKlase()
        {
            return 2;
        }

        public double calcByEpsilon(FunctionDel function, double a, double b, double eps)
        {
            int N = RungesPaklaida.GeriausiasN(this, a, b, eps, function);
            return calcByN(function, a, b, N);
        }

        #endregion
    }
}
