using KTUzd.Models;

namespace KTUzd.Solution
{
    public class GreatestCommonDivisor
    {
        public static Polynomial FindGCD(Polynomial p1, Polynomial p2)
        {
            if ((p1 == 0) || (p2 == 0))
            {
                return p1 + p2;
            }
            return p1 > p2 ? FindGCD(p1 % p2, p2) : FindGCD(p1, p2 % p1);
        }
    }
}
