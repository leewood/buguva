using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Metodai4
{
    class RungesPaklaida
    {
        public static double GautiPaklaida(IMetodas metodas, double a, double b, int N, FunctionDel funkcija)
        {
            return Math.Abs(metodas.calcByN(funkcija, a, b, N) - metodas.calcByN(funkcija, a, b, N / 2)) / (Math.Pow(2, metodas.tikslumoKlase()) - 1);
        }

        public static int GeriausiasN(IMetodas metodas, double a, double b, double eps, FunctionDel funkcija)
        {
            int N = 1;
            while (GautiPaklaida(metodas, a, b, N, funkcija) > eps)
            {
                N *= 2;
            }
            return N;
        }
    }
}
