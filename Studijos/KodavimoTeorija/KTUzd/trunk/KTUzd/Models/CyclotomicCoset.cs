using System.Collections.Generic;

namespace KTUzd.Models
{
    /// <summary>
    /// Ciklinį kosetą (angl. cyclotomic coset apibrėžianti klasė
    /// </summary>
    public class CyclotomicCoset
    {
        /// <summary>
        /// Koseto nariai
        /// </summary>
        public List<int> Items { get; set; }
        public int N { get; private set; }
        public int Q { get; private set; }
        public int S { get; private set; }
        public int I { get; private set; }

        public CyclotomicCoset() {}
        public CyclotomicCoset(List<int> values)
        {
            Items = values;
        }

        /// <summary>
        /// Apskaičiuoja ciklinį kosetą pagal duotas reikšmes
        /// </summary>
        /// <param name="n">Polinomo laipsnis</param>
        /// <param name="q">Baigtinio kūno parametras q</param>
        /// <param name="i">Parametras I</param>
        /// <returns></returns>
        public static CyclotomicCoset Calculate(int n, int q, int i)
        {
            int s = 1;
            var result = new CyclotomicCoset { N = n, Q = q, I = i, Items = new List<int> { i } };

            int qGrade = q;
            while ((i * qGrade) % n != i)
            {
                if ((i * qGrade) % n == 0)
                {
                    result.Items.Clear();
                    result.S = 0;
                    return result;
                }
                result.Items.Add((qGrade * i) % n);
                qGrade *= q;
                s++;
            }
            result.S = s;
            return result;
        }

        /// <summary>
        /// Palygina dvi kosetų sekas
        /// </summary>
        /// <param name="list1">Pirma seka</param>
        /// <param name="list2">Antra seka</param>
        /// <returns>Ar lygu</returns>
        private static bool ListEqual(ICollection<int> list1, IList<int> list2)
        {
            if (list1.Count != list2.Count)
            {
                return false;
            }
            for (int i = 0; i < list1.Count; i++)
            {
                if (!list1.Contains(list2[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj is CyclotomicCoset)
            {
                return ListEqual(Items, ((CyclotomicCoset)obj).Items);
            }
            if (obj is IList<int>)
            {
                return ListEqual(Items, (List<int>)obj);
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = (Items != null ? Items.GetHashCode() : 0);
                result = (result * 397) ^ N;
                result = (result * 397) ^ Q;
                result = (result * 397) ^ S;
                result = (result * 397) ^ I;
                return result;
            }
        }

        /// <summary>
        /// Patikrina ar kosetų seka turi tokį elementą
        /// </summary>
        /// <param name="set">Kosetų seka</param>
        /// <param name="coset">Kosetas</param>
        /// <returns>Ar turi</returns>
        public static bool Contains(List<CyclotomicCoset> set, CyclotomicCoset coset)
        {
            foreach (var checkWith in set)
            {
                if (checkWith.Equals(coset))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Apskaičiuoja visų su polinomu susijusių kosetų seką
        /// </summary>
        /// <param name="n">Polinomo laipsnis</param>
        /// <param name="q">Parametras q</param>
        /// <returns>Ieškoma seka</returns>
        public static List<CyclotomicCoset> GetCyclotomicCosetsSet(int n, int q)
        {
            var result = new List<CyclotomicCoset>();
            for (int i = 0; i < n; i++)
            {
                var coset = Calculate(n, q, i);
                if (!Contains(result, coset))
                {
                    result.Add(coset);
                }
            }
            return result;
        }
    }

}
