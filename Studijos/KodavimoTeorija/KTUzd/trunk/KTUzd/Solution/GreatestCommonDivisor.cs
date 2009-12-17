using System;
using KTUzd.Models;

namespace KTUzd.Solution
{
    /// <summary>
    /// Didžiausio bendro kartotinio radimo algoritmas polinomams
    /// </summary>
    public class GreatestCommonDivisor
    {
        /// <summary>
        /// Randa didžiausią bendrą daliklį tarp polinomų
        /// </summary>
        /// <param name="p1">Pirmas polinomas</param>
        /// <param name="p2">Antras polinomas</param>
        /// <returns></returns>
        public static Polynomial FindGCD(Polynomial p1, Polynomial p2)
        {
            // Jei bent vienas iš narių lygus nuliui baigiame
            if ((p1.PolynomialDegree == 0) && (p1[0] == 0) ||
                (p2.PolynomialDegree == 0) && (p2[0] == 0))
            {
                var result = p1 + p2;
                result.Q = p1.Q;
                return result;
            }
            // kitu atveju randame liekaną ir kviečiame rekursiškai
            return p1 > p2 ? FindGCD((p1 % p2).Modus(p1.Q), p2) : FindGCD(p1, (p2 % p1).Modus(p2.Q));
        }
    }
}
