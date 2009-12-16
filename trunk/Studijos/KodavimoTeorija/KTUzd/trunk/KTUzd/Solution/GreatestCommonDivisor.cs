using System;
using KTUzd.Models;

namespace KTUzd.Solution
{
    public class GreatestCommonDivisor
    {
        public static Polynomial FindGCD(Polynomial p1, Polynomial p2)
        {
            if ((p1.PolynomialGrade == 0) && (Math.Abs(p1[0]) <= (decimal)0.000001) ||
                (p2.PolynomialGrade == 0) && (Math.Abs(p2[0]) <= (decimal)0.000001))
            {
                var result = p1 + p2;
                result.Q = p1.Q;
                return result;
            }
            return p1 > p2 ? FindGCD((p1 % p2).Modus(p1.Q), p2) : FindGCD(p1, (p2 % p1).Modus(p2.Q));
        }
    }
}
