using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KTUzd.Models;

namespace KTUzd.Solution
{
    /// <summary>
    /// Faktorizacijos algoritmo realizacija
    /// </summary>
    public class FactoringAlgorithm
    {
        /// <summary>
        /// Vienas faktorizacijos žingsnis, t.y. vieno polinomo skaidymas pagal duotą kosetą
        /// </summary>
        /// <param name="coset">Kosetas</param>
        /// <param name="poly">Polinomas</param>
        /// <returns>Išskaidymas</returns>
        public static Polynomial[] OneStepFactorization(CyclotomicCoset coset, Polynomial poly)
        {
            List<Polynomial> result = new List<Polynomial>();
            Polynomial cosetPoly = new Polynomial(coset);
            cosetPoly.Q = poly.Q;
            for (int s = 0; s < poly.Q; s++)
            {
                Polynomial member = GreatestCommonDivisor.FindGCD(poly, cosetPoly + s);
                member.Q = poly.Q;
                if (member.PolynomialDegree > 0)
                {
                    result.Add(member);
                }
            }
            return result.ToArray();
        }

        /// <summary>
        /// Atlieka pilną polinomo faktorizaciją
        /// </summary>
        /// <param name="poly">Polinomas</param>
        /// <returns>Išskaidymo rezultatas</returns>
        public static Polynomial[] FullFactorization(Polynomial poly)
        {
            List<Polynomial> result = new List<Polynomial>() { poly };
            poly.UpdateCosets();
            int currentCoset = 0;
            // kol dar yra galima faktorizuoti ir dar yra nepanaudotų kosetų
            while ((currentCoset < poly.CosetsSet.Count) && (result.Count < poly.IrreducibleFactorsCount))
            {
                List<Polynomial> newResult = new List<Polynomial>();
                // bėgame per kiekvieną skaidinį ir bandome jį dar skaidyti.
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
