using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KTUzd.Models;

namespace KTUzd.Solution
{
    public class FactoringAlgorithm
    {
        public static Polynomial[] OneStepFactorization(CyclotomicCoset coset, Polynomial poly)
        {
            List<Polynomial> result = new List<Polynomial>();
            Polynomial cosetPoly = new Polynomial(coset);
            cosetPoly.Q = poly.Q;
            for (int s = 0; s < poly.Q; s++)
            {
                Polynomial member = GreatestCommonDivisor.FindGCD(poly, cosetPoly + s);
                member.Q = poly.Q;
                if (member.PolynomialGrade > 0)
                {
                    result.Add(member);
                }
            }
            return result.ToArray();
        }

        public static Polynomial[] FullFactorization(Polynomial poly)
        {
            List<Polynomial> result = new List<Polynomial>() { poly };
            poly.UpdateCosets();
            int currentCoset = 0;
            while ((currentCoset < poly.CosetsSet.Count) && (result.Count < poly.IrreducibleFactorsCount))
            {
                List<Polynomial> newResult = new List<Polynomial>();
                for (int i = 0; i < result.Count; i++)
                {
                    newResult.AddRange(OneStepFactorization(poly.CosetsSet[currentCoset], result[i]));
                }
                currentCoset++;
                result = newResult;
            }
            return result.ToArray();
        }
    }
}
