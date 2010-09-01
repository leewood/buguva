using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Metodai4
{
    class GausoLygis3: IMetodas
    {
        #region IMetodas Members

        public double calc(FunctionDel function, double a, double b)
        {
            double alfa = (b - a) / 2;
            double beta = (a + b) / 2;
            FunctionDel function2 = x => alfa * function(alfa * x + beta);
            double val = Math.Sqrt((double)3 / (double)5);
            return (5 * function2(-val) + 8 * function2(0) + 5 * function2(val)) / 9;
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

        public double calcByEpsilon(FunctionDel function, double a, double b, double eps)
        {
            int N = RungesPaklaida.GeriausiasN(this, a, b, eps, function);
            return calcByN(function, a, b, N);
        }

        public int tikslumoKlase()
        {
            return 6;
        }

        #endregion
    }
}
