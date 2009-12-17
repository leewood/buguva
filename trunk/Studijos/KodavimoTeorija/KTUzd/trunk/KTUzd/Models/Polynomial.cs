using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KTUzd.Models
{
    /// <summary>
    /// Polinomą apibrėžianti klasė. Polinomo objektas turi perrašytus operatorius, todėl sudėtis, 
    /// atimtis, daugyba, dalyba ir liekanos radimas gali būti atliekami naudojant įprastus metematinius
    /// operatorius +, -, *, /, %. Be to atskiro polinomo nario koeficientą galima pasiekti per [] operatorių.
    /// </summary>
    public class Polynomial
    {
        /// <summary>
        /// Sąrašas saugoti polinomo elementams
        /// </summary>
        List<PolynomialElement> _elements = new List<PolynomialElement>();

        #region <Klasės konstruktoriai>

        /// <summary>
        /// Sukonstruoja naują polinomą koeficientus imdamas iš pateikto polinomo
        /// </summary>
        /// <param name="pol">Polinomas pateiktas kaip šablonas</param>
        /// <param name="p">P reikšmė, kurią naudosime</param>
        /// <param name="m">M reikšmė, kurią naudosime</param>
        public Polynomial(Polynomial pol, int p, int m)
        {
            _elements.Clear();
            for (int i = 0; i <= pol.PolynomialDegree; i++)
            {
                this[i] = pol[i];
            }
            Q = (int)Math.Pow(p, m);
            P = p;
            M = m;
        }

        /// <summary>
        /// Sukonstruoja naują polinomą pagal pateiktą tekstinę išraišką
        /// </summary>
        /// <param name="textRepresentation">Tekstinė polinomo išraiška</param>
        /// <param name="p">P parametro reikšmė</param>
        /// <param name="m">M parametro reikšmė</param>
        public Polynomial(string textRepresentation, int p, int m) : this(PolynomialParser.Parse(textRepresentation), p, m) { }

        /// <summary>
        /// Daro tą patį, ką ir ankstesnis konstruktorius, bet kaip m ima 1
        /// </summary>
        /// <param name="text">Tekstinė polinomo išraiška</param>
        /// <param name="q">Q parametro reikšmė</param>
        public Polynomial(string text, int q) : this(text, q, 1) { }

        /// <summary>
        /// Daro tą patį, ką ir ankstesnis konstruktorius, bet palieka parametrą q neužpildytą
        /// </summary>
        /// <param name="text">Tekstinė polinomo išraiška</param>        
        public Polynomial(string text) : this(text, 0) { }

        /// <summary>
        /// Sukonstruoja 0-io laipsnio polinomą pagal duotą skaičių kaip koeficientą
        /// </summary>
        /// <param name="i">nario su 0-iu laipsniu koeficiento reikšmė</param>
        /// <param name="p">P parametro reikšmė</param>
        /// <param name="m">M parametro reikšmė</param>
        public Polynomial(decimal i, int p, int m)
        {
            this[0] = i;
            Q = (int)Math.Pow(p, m);
            P = p;
            M = m;
        }

        /// <summary>
        /// Daro tą patį, ką ir ankstesnis konstruktorius, bet kaip m ima 1
        /// </summary>
        /// <param name="i">nario su 0-iu laipsniu koeficiento reikšmė</param>
        /// <param name="q">Q parametro reikšmė</param>
        public Polynomial(decimal i, int q) : this(i, q, 1) { }

        /// <summary>
        /// Daro tą patį, ką ir ankstesnis konstruktorius, bet palieka parametrą q neužpildytą
        /// </summary>
        /// <param name="i">nario su 0-iu laipsniu koeficiento reikšmė</param>
        public Polynomial(decimal i) : this(i, 0) { }

        /// <summary>
        /// Sukonstruoja nulinį polinomą
        /// </summary>
        public Polynomial() : this(0) { }

        /// <summary>
        /// Sukonstruoja polinomą, pagal duotą laipsnių sąrašą, visur kaip koeficientai imami 1
        /// </summary>
        /// <param name="coset">Laipsnių sąrašas</param>
        /// <param name="p">P parametro reikšmė</param>
        /// <param name="m">M parametro reikšmė</param>
        public Polynomial(List<int> coset, int p, int m) : this(new CyclotomicCoset(coset), p, m) { }

        /// <summary>
        /// Daro tą patį, ką ir ankstesnis konstruktorius, bet kaip m ima 1
        /// </summary>
        /// <param name="coset">Laipsnių sąrašas</param>
        /// <param name="q">Q parametro reikšmė</param>
        public Polynomial(List<int> coset, int q) : this(coset, q, 1) { }

        /// <summary>
        /// Daro tą patį, ką ir ankstesnis konstruktorius, bet palieka parametrą q neužpildytą
        /// </summary>
        /// <param name="coset">Laipsnių sąrašas</param>
        public Polynomial(List<int> coset) : this(coset, 0) { }

        /// <summary>
        /// Sukonstruoja polinomą pagal duotą CyclomaticCoset objektą
        /// </summary>
        /// <param name="coset">CyclomaticCoset objektas apibūdianntis polinomą</param>
        public Polynomial(CyclotomicCoset coset) : this(coset, 0) { }

        /// <summary>
        /// Sukonstruoja polinomą pagal duotą CyclomaticCoset objektą ir Q parametro reikšmę
        /// </summary>
        /// <param name="coset">CyclomaticCoset objektas apibūdianntis polinomą</param>
        /// <param name="q">Q parametro reikšmė</param>
        public Polynomial(CyclotomicCoset coset, int q) : this(coset, q, 1) { }

        /// <summary>
        /// Sukonstruoja polinomą pagal duotą CyclomaticCoset objektą ir P ir M parametrų reikšmes
        /// </summary>
        /// <param name="coset">CyclomaticCoset objektas apibūdianntis polinomą</param>
        /// <param name="p">P parametro reikšmė</param>
        /// <param name="m">M parametro reikšmė</param>
        public Polynomial(CyclotomicCoset coset, int p, int m)
        {
            for (int i = 0; i < coset.Items.Count; i++)
            {
                this[coset.Items[i]] = 1;
            }
            Q = (int)Math.Pow(p, m);
            P = p;
            M = m;
        }

        #endregion

        #region <Įvairūs pagalbiniai metodai>
        /// <summary>
        /// Metodas skirtas polinomo klasės turinį pavaizduoti žmogui suprantamu tekstiniu formatu
        /// </summary>
        /// <returns>
        /// Grąžina polinomą atitinkantį tekstinį formatą.
        /// Rezultato pavyzdys, pvz gali būti x^4 + a^2x^2 + 1. 
        /// Šis užrašas rodo, jog polinomas sudarytas iš 3 elementų, pirmasis yra x pakeltas 
        /// 4 laipsniu, antrasis - x pakeltas 2 laipsniu ir padaugintas primityviojo elemento pakelto
        /// 2 laipsniu. Na o trečiasis - konstanta  1. Kitaip tariant šiame užraše kėlimas
        /// laipsniu vaizduojamas simboliu ^, o primityvusis elementas nurodomas simboliu a.
        /// </returns>
        public override string ToString()
        {
            if (this == 0)
            {
                return "0";
            }
            var b = new StringBuilder();
            bool separ = false;
            foreach (var elem in _elements.OrderByDescending(e => e.Power))
            {
                b.Append(elem.ToString(separ, Q));
                separ = true;
            }
            return b.ToString();
        }



        /// <summary>
        /// Perrašytas masyvo elemento [] operatorius, kad būtų galima pasiekti polinomo 
        /// atskirus elementus naudojant tą pačią notaciją kaip ir pasiekiant įprastų masyvų elementus        
        /// </summary>
        /// <param name="power">
        /// Parametru "power" nurodome, kurio laipsnio polinomo narį norime gauti.
        /// Jei nario su tokiu laipsniu nėra, jis sukuriamas su koeficientu 0.
        /// </param>
        /// <returns>
        /// Gražinamas koeficientas, kurį turi nurodyto laipsnio narys. Jei nario nėra - 0.
        /// Pvz. Jei turime polinomą poly = 2x^3 + x + 3, tai bus tokie rezultatai:
        /// poly[3] = 2; poly[2] = 0; poly[1] = 1, poly[0] = 3.
        /// </returns>
        public decimal this[int power]
        {
            get
            {
                var elem = GetElement(power);
                return elem.Coefficient;
            }
            set
            {
                var elem = GetElement(power);
                elem.Coefficient = value;
            }
        }

        /// <summary>
        /// Metodas skirtas sutvarkyti polinomo narius, pašalinant tuos, kurių koeficientai lygūs nuliui ir
        /// taip sutaupant laiko elemento paieškai bei atminties.
        /// </summary>
        public void OptimizeElements()
        {
            if (_elements != null)
            {
                _elements = (from elem in _elements where elem.Coefficient != 0 select elem).ToList();
            }
        }

        /// <summary>
        /// Grąžina nurodyto laipsnio polinomo narį
        /// </summary>
        /// <param name="power">Laipsnis polinomo nario, kurį norime gauti</param>
        /// <returns>grąžinamas pageidaujamas polinomo narys</returns>
        public PolynomialElement GetElement(int power)
        {
            if (_elements == null)
            {
                _elements = new List<PolynomialElement>();
            }
            OptimizeElements();
            var element = (from elem in _elements where elem.Power == power select elem).FirstOrDefault();
            if (element == null)
            {
                element = new PolynomialElement { Power = power, Coefficient = 0 };
                _elements.Add(element);
            }
            return element;
        }

        /// <summary>
        /// Metodas iš naujo apskaičiuojantis su polonomu susijusių ciklinių kosetų (angl. Cyclotomic Coset) aibę
        /// </summary>
        public void UpdateCosets()
        {
            _cosetsSet = CyclotomicCoset.GetCyclotomicCosetsSet(PolynomialDegree, Q);
        }

        /// <summary>
        /// Atlieka dviejų baigtinio kūno elementų daugybą
        /// </summary>
        /// <param name="a">Pirmas baigtinio kūno elementas</param>
        /// <param name="b">Antras baigtinio kūno elementas</param>
        /// <param name="q">Baigtinio kūno parametras q</param>
        /// <returns></returns>
        private static int Mlt(decimal a, decimal b, int q)
        {
            var aI = (int)Math.Abs(a);
            var bI = (int)Math.Abs(b);
            if ((aI == 0) || (bI == 0))
            {
                return 0;
            }
            int newPower = ((aI + bI - 2) % (q - 1)) + 1;
            if (a * b < 0)
            {
                return -newPower;
            }
            return newPower;
        }

        /// <summary>
        /// Apskaičiuoja iš kiek reikia padauginti daliklio pirmo nario koeficientą, kad atėmus
        /// iš dalinio pirmo nario, jie susiprastintų
        /// </summary>
        /// <param name="coeff1">Dalinio pirmo nario koeficientas</param>
        /// <param name="coeff2">Daliklio pirmo nario koeficientas</param>
        /// <param name="q">Baigtinio kūno parametras q.</param>
        /// <returns>Grąžina ieškomą daugiklį</returns>
        private static decimal CalculateCoeff(decimal coeff1, decimal coeff2, int q)
        {
            if (q == 0)
            {
                return -coeff1 / coeff2;
            }
            int k = 1;
            var add = (int)coeff2;
            var c1 = ((int)coeff1) % q;
            if (c1 < 0)
            {
                c1 = c1 + q;
            }
            int c2 = ((int)coeff2) % q;
            if (c2 < 0)
            {
                c2 = c2 + q;
            }
            while ((c1 ^ c2) != 0)
            {
                k++;
                c2 = (c2 + add) % q;
            }
            return k;
        }

        /// <summary>
        /// Dalybos stulpeliu atgoritmą realizuojantis metodas
        /// </summary>
        /// <param name="pol1">Dalinys</param>
        /// <param name="pol2">Daliklis</param>
        /// <returns>grąžinamas dalmuo</returns>
        private static Polynomial Divide(Polynomial pol1, Polynomial pol2)
        {
            // jei dalyba iš nulio arba daliklio laipsnis didenis už dalmens - baigiam 
            // ir grąžiname dalinį kaip liekaną.
            if ((pol1 == 0) || (pol1.PolynomialDegree < pol2.PolynomialDegree))
            {
                _divisionRemainder = pol1;
                return new Polynomial();
            }
            decimal coeff2 = pol2[pol2.PolynomialDegree];
            decimal coeff = pol1[pol1.PolynomialDegree];
            // pasidarome polinomą, iš kurio dauginsime daliklį
            var mult = new Polynomial();
            if ((pol1.Q % coeff2 == 0) && (coeff % coeff2 != 0))
            {
                pol1 = pol1 * coeff2;
                coeff = pol1[pol1.PolynomialDegree];
            }
            mult[pol1.PolynomialDegree - pol2.PolynomialDegree] = CalculateCoeff(coeff, coeff2, pol1.Q);
            // apskaičiuojame, kiek dar liko polinomo neišdalintą
            Polynomial next = pol1 + (pol2 * mult);
            // kartojame rekursiškai
            return mult + Divide(next, pol2);
        }

        /// <summary>
        /// Metodas, kuris patikrina ar visi polinomo narių koeficientai turi reikšmes mod q ir jei ne, tai jas
        /// atnaujina.
        /// </summary>
        /// <param name="q">q parametro, kuriuo atžvilgiu tikrinama, reikšmė</param>
        /// <returns>Gražina sutvarkytą polinomą</returns>
        public Polynomial Modus(int q)
        {
            if (q > 0)
            {
                var result = new Polynomial();
                for (int i = 0; i <= PolynomialDegree; i++)
                {
                    result[i] = (this[i] >= 0) ? (this[i] % q) : q + (this[i] % q);
                }
                result.Q = q;
                return result;
            }
            return this;
        }

        #endregion

        #region <Perrašyti matematiniai operatoriai>


        /// <summary>
        /// Padaugina polinomą iš skaičiaus
        /// </summary>
        /// <param name="pol">Polinomas</param>
        /// <param name="coeff">Skaičius</param>
        /// <returns>Daugybos rezultatas</returns>
        public static Polynomial operator *(Polynomial pol, decimal coeff)
        {
            return pol * new Polynomial(coeff);
        }

        /// <summary>
        /// Padaugina polinomą iš kito polinomą
        /// </summary>
        /// <param name="pol1">Pirmas polinomas</param>
        /// <param name="pol2">Antras polinomas</param>
        /// <returns>Daugybos rezultatas</returns>
        public static Polynomial operator *(Polynomial pol1, Polynomial pol2)
        {
            // imamas kiekvienas elementas iš pirmo polinomo ir dauginamas iš kiekvieno antro polinomo nario
            var result = new Polynomial();
            for (int i = 0; i <= pol1.PolynomialDegree; i++)
            {
                var item = new Polynomial();
                for (int j = 0; j <= pol2.PolynomialDegree; j++)
                {
                    if (pol1.Q == 0)
                    {
                        item[i + j] = pol2[j] * pol1[i];
                    }
                    else
                    {
                        //jei polinomas virš baigtinio kūno - atlieka daugyba pagal b.kūno taisykles
                        item[i + j] = Mlt(pol1[i], pol2[j], pol1.Q);
                    }
                }
                result += item;
            }
            result.Q = pol1.Q;
            result.M = pol1.M;
            result.P = pol1.P;
            return result.Modus(pol1.Q);
        }

        /// <summary>
        /// Sudeda du polinomus
        /// </summary>
        /// <param name="pol1">Pirmas polinomas</param>
        /// <param name="pol2">Antras polinomas</param>
        /// <returns>Sudėties rezultatas</returns>
        public static Polynomial operator +(Polynomial pol1, Polynomial pol2)
        {
            var result = new Polynomial();
            int last = Math.Max(pol1.PolynomialDegree, pol2.PolynomialDegree);
            // prie kiekvieno pirmo nario elemento prideda atitinkamos pozicijos antro nario elementą
            for (int i = 0; i <= last; i++)
            {
                if (pol1.Q == 0)
                {
                    result[i] = pol1[i] + pol2[i];
                }
                else
                {
                    // sutikrinamos b.kūno taisyklės
                    int p1 = ((pol1[i] >= 0) ? (int)pol1[i] : pol1.Q - (int)pol1[i]) % pol1.Q;
                    int p2 = ((pol2[i] >= 0) ? (int)pol2[i] : pol1.Q - (int)pol2[i]) % pol1.Q;
                    // xor sudėties taisyklė
                    result[i] = p1 ^ p2;
                }
            }
            result.Q = pol1.Q;
            result.M = pol1.M;
            result.P = pol1.P;
            return result.Modus(pol1.Q);
        }

        /// <summary>
        /// Sudeda polinomą su skaičiumi
        /// </summary>
        /// <param name="pol">Polinomas</param>
        /// <param name="num">Skaičius</param>
        /// <returns>Sudėties rezultatas</returns>
        public static Polynomial operator +(Polynomial pol, decimal num)
        {
            return pol + new Polynomial(num);
        }

        /// <summary>
        /// Iš vieno polinomo atima kitą
        /// </summary>
        /// <param name="pol1">Pirmas polinomas</param>
        /// <param name="pol2">Antras polinomas</param>
        /// <returns>Atimties rezultatas</returns>
        public static Polynomial operator -(Polynomial pol1, Polynomial pol2)
        {
            return pol1 + (pol2 * (-1));
        }

        /// <summary>
        /// Atima skaičių iš polinomo
        /// </summary>
        /// <param name="pol1">Polinomas</param>
        /// <param name="num">Skaičius</param>
        /// <returns>Atimties rezultatas</returns>
        public static Polynomial operator -(Polynomial pol1, decimal num)
        {
            return pol1 - new Polynomial(num);
        }

        private static Polynomial _divisionRemainder;

        /// <summary>
        /// Padalija vieną polinomą iš kito
        /// </summary>
        /// <param name="pol1">Pirmas polinomas</param>
        /// <param name="pol2">Antras polinomas</param>
        /// <returns>Dalybos rezultatas</returns>
        public static Polynomial operator /(Polynomial pol1, Polynomial pol2)
        {
            var result = Divide(pol1, pol2);
            result.Q = pol1.Q;
            result.M = pol1.M;
            result.P = pol1.P;
            return result.Modus(pol1.Q);
        }

        /// <summary>
        /// Randa dalybos liekaną
        /// </summary>
        /// <param name="pol1">Dalinys</param>
        /// <param name="pol2">Daliklis</param>
        /// <returns>Liekana</returns>
        public static Polynomial operator %(Polynomial pol1, Polynomial pol2)
        {
            Divide(pol1, pol2);
            _divisionRemainder.Q = pol1.Q;
            _divisionRemainder.M = pol1.M;
            _divisionRemainder.P = pol1.P;
            return _divisionRemainder.Modus(pol1.Q);
        }


        /// <summary>
        /// Palygina du polinomus.
        /// </summary>
        /// <param name="pol1">Pirmas polinomas</param>
        /// <param name="pol2">Antras polinomas</param>
        /// <returns>Gražiną ar jie lygūs</returns>
        public static bool operator ==(Polynomial pol1, Polynomial pol2)
        {
            Polynomial pol = pol1 - pol2;
            return ((pol.PolynomialDegree == 0) && (pol[0] == 0));
        }

        /// <summary>
        /// Palygina polinomą su polinoma tekstine išraiška.
        /// </summary>
        /// <param name="pol">Polinomas</param>
        /// <param name="text">Polinomo tekstinė išraiška</param>
        /// <returns>Gražiną ar jie lygūs</returns>
        public static bool operator ==(Polynomial pol, string text)
        {
            return pol == new Polynomial(text);
        }

        /// <summary>
        /// Palygina ar polinomas lygus skaičiui, t.y. polinomo laipsnis yra 0 ir koeficientas lygus duotam skaičiui
        /// </summary>
        /// <param name="pol1">Polinomas</param>
        /// <param name="num">Skaičius</param>
        /// <returns>Gražiną ar jie lygūs</returns>
        public static bool operator ==(Polynomial pol1, decimal num)
        {
            var temp = new Polynomial();
            temp[0] = num;
            return pol1 == temp;
        }

        /// <summary>
        /// Palygina polinomą su polinoma tekstine išraiška.
        /// </summary>
        /// <param name="pol">Polinomas</param>
        /// <param name="text">Polinomo tekstinė išraiška</param>
        /// <returns>Gražiną ar jie nelygūs</returns>
        public static bool operator !=(Polynomial pol, string text)
        {
            return pol != new Polynomial(text);
        }

        /// <summary>
        /// Palygina ar polinomas nelygus skaičiui
        /// </summary>
        /// <param name="pol1">Polinomas</param>
        /// <param name="num">Skaičius</param>
        /// <returns>Gražiną ar jie nelygūs</returns>
        public static bool operator !=(Polynomial pol1, decimal num)
        {
            return !(pol1 == num);
        }

        /// <summary>
        /// Palygina du polinomus.
        /// </summary>
        /// <param name="pol1">Pirmas polinomas</param>
        /// <param name="pol2">Antras polinomas</param>
        /// <returns>Gražiną ar jie nelygūs</returns>
        public static bool operator !=(Polynomial pol1, Polynomial pol2)
        {
            return !(pol1 == pol2);
        }

        /// <summary>
        /// Palygina du polinomus. Polinomas didesnis už kitą, jei jo laipsnis didesnis.
        /// </summary>
        /// <param name="pol1">Pirmas polinomas</param>
        /// <param name="pol2">Antras polinomas</param>
        /// <returns>Gražiną ar pirmas didesnis</returns>
        public static bool operator >(Polynomial pol1, Polynomial pol2)
        {
            if (pol1.PolynomialDegree < pol2.PolynomialDegree)
            {
                return false;
            }
            if (pol1.PolynomialDegree > pol2.PolynomialDegree)
            {
                return true;
            }
            Polynomial pol = pol1 - pol2;
            return pol[pol.PolynomialDegree] > 0;
        }

        /// <summary>
        /// Palygina polinomą ir skaičių
        /// </summary>
        /// <param name="pol1">Polinomas</param>
        /// <param name="num">Skaičius</param>
        /// <returns>Gražiną ar polinomas didesnis</returns>
        public static bool operator >(Polynomial pol1, decimal num)
        {
            return pol1 > new Polynomial(num);
        }

        /// <summary>
        /// Palygina polinomą ir skaičių
        /// </summary>
        /// <param name="pol1">Polinomas</param>
        /// <param name="num">Skaičius</param>
        /// <returns>Gražiną ar polinomas mažesnis arba lygus</returns>
        public static bool operator <=(Polynomial pol1, decimal num)
        {
            return pol1 <= new Polynomial(num);
        }

        /// <summary>
        /// Palygina du polinomus.
        /// </summary>
        /// <param name="pol1">Pirmas polinomas</param>
        /// <param name="pol2">Antras polinomas</param>
        /// <returns>Gražiną ar pirmas mažesnis arba lygus</returns>
        public static bool operator <=(Polynomial pol1, Polynomial pol2)
        {
            return (pol1 < pol2) || (pol1 == pol2);
        }

        /// <summary>
        /// Palygina du polinomus.
        /// </summary>
        /// <param name="pol1">Pirmas polinomas</param>
        /// <param name="pol2">Antras polinomas</param>
        /// <returns>Gražiną ar pirmas didesnis arba lygus</returns>
        public static bool operator >=(Polynomial pol1, Polynomial pol2)
        {
            return pol2 <= pol1;
        }

        /// <summary>
        /// Palygina polinomą ir skaičių
        /// </summary>
        /// <param name="pol1">Polinomas</param>
        /// <param name="num">Skaičius</param>
        /// <returns>Gražiną ar polinomas didesnis arba lygus</returns>
        public static bool operator >=(Polynomial pol1, decimal num)
        {
            return pol1 >= new Polynomial(num);
        }

        /// <summary>
        /// Palygina du polinomus.
        /// </summary>
        /// <param name="pol1">Pirmas polinomas</param>
        /// <param name="pol2">Antras polinomas</param>
        /// <returns>Gražiną ar pirmas mažesnis</returns>
        public static bool operator <(Polynomial pol1, Polynomial pol2)
        {
            return pol2 > pol1;
        }

        /// <summary>
        /// Palygina polinomą ir skaičių
        /// </summary>
        /// <param name="pol1">Polinomas</param>
        /// <param name="num">Skaičius</param>
        /// <returns>Gražiną ar polinomas mažesnis</returns>
        public static bool operator <(Polynomial pol1, decimal num)
        {
            return pol1 < new Polynomial(num);
        }

        #endregion

        #region <Įvarios klasės savybės>

        /// <summary>
        /// Baigtinį kūną, virš kurio vyksta polinomo operacijos apibrėžiantis parametras p. Visada pirminis
        /// </summary>
        public int P { get; set; }
        /// <summary>
        /// Baigtinį kūną, virš kurio vyksta polinomo operacijos apibrėžiantis parametras m. 
        /// </summary>
        public int M { get; set; }
        /// <summary>
        /// Baigtinį kūną, virš kurio vyksta polinomo operacijos apibrėžiantis parametras q = p^m.
        /// </summary>
        public int Q { get; set; }

        /// <summary>
        /// Polinomo laipsnį grąžinanti klasės savybė
        /// </summary>
        public int PolynomialDegree
        {
            get
            {
                if (_elements == null)
                {
                    return 0;
                }
                if (_elements.Count > 0)
                {
                    // surikiuojame narius pagal jų laipsnius didejimo tvarka
                    var sorted = _elements.Where(e => e.Coefficient != 0).OrderBy(elem => elem.Power);
                    if (sorted.Count() > 0)
                    {
                        // imame paskutinį narį, jo laipsnis ir bus polinomo laipsnis
                        return sorted.Last().Power;
                    }
                    return 0;
                }
                return 0;
            }
        }

        /// <summary>
        /// Klasės savybė sauganti su polinomu susijusių ciklinių kosetų (angl. Cyclotomic Coset) aibę
        /// </summary>
        private List<CyclotomicCoset> _cosetsSet;
        public List<CyclotomicCoset> CosetsSet
        {
            get
            {
                if (_cosetsSet == null)
                {
                    UpdateCosets();
                }
                return _cosetsSet;
            }
            set
            {
                _cosetsSet = value;
            }
        }

        /// <summary>
        /// Savybė grąžinanti į kiek nebeišskaidomų dauginamųjų galima išskaidyti polinomą
        /// </summary>
        public int IrreducibleFactorsCount
        {
            get
            {
                return CosetsSet.Count;
            }
        }

        #endregion

        #region <Sisteminiai palyginimo metodai>

        /// <summary>
        /// Palygina ši polinomą su duotu.
        /// </summary>
        /// <param name="other">Duotas polinomas</param>
        /// <returns>Grąžina ar lygus</returns>
        public bool Equals(Polynomial other)
        {
            return this == other;
        }

        /// <summary>
        /// Palygina šį polinomą su bet kuriuo kitu duotu objektu.
        /// </summary>
        /// <param name="obj">objektą, su kuriuo lyginama</param>
        /// <returns>Grąžina ar lygu</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(Polynomial)) return false;
            return Equals((Polynomial)obj);
        }

        /// <summary>
        /// Generuoja hash kodą
        /// </summary>
        /// <returns>Hash kodas</returns>
        public override int GetHashCode()
        {
            return (_elements != null ? _elements.GetHashCode() : 0);
        }

        #endregion

        /// <summary>
        /// Pagalbinė klasė paversti tesktinį užrašą į polinomą
        /// </summary>
        public static class PolynomialParser
        {
            /// <summary>
            /// Verčia tekstinį užrašą į polinomą
            /// </summary>
            /// <param name="s">Tekstinis polinomo užrašas</param>
            /// <returns>Polinomas</returns>
            public static Polynomial Parse(string s)
            {
                var result = new Polynomial();
                // Pašalinami nereikalingi simboliai
                string checkedS = s.Replace(" ", "").Replace("+-", "-").Replace("-+", "-").Replace("f(x)=", "") + "+";
                int mode = -1;
                int calculatedCoeff = 0;
                int currentPower = 0;
                int value = 0;
                int sign = 1;
                // bėgama per visus eilutės simbolius
                for (int i = 0; i < checkedS.Length; i++)
                {
                    // tikrinama ką einamasis simbolis gali reikšti
                    switch (checkedS[i])
                    {
                        case 'x': //sutiktas x, baigiam koeficiento rinkimo rėžimą
                            currentPower = 1;
                            if (mode == 0)
                            {
                                calculatedCoeff = value;
                            }
                            else if (mode == 2)
                            {
                                calculatedCoeff = value + 2;
                            }
                            else if (mode == 4)
                            {
                                calculatedCoeff = 1;
                            }
                            else
                            {
                                calculatedCoeff = 1;
                            }
                            mode = 1;
                            value = 0;
                            break;
                        case '-': // operacijos ženklas, reiks keisti toliau einantį koeficientą į minusą
                            if ((mode == 1) || (mode == 3))
                            {
                                result[currentPower] += sign * calculatedCoeff;
                            }
                            else if (mode == 0)
                            {
                                result[0] += sign * value;
                            }
                            sign = -1;
                            mode = -1;
                            value = 0;
                            break;
                        case '+':
                            if ((mode == 1) || (mode == 3))
                            {
                                result[currentPower] += sign * calculatedCoeff;
                            }
                            else if (mode == 0)
                            {
                                result[0] += sign * value;
                            }
                            sign = 1;
                            mode = -1;
                            value = 0;
                            break;
                        case '^': // kėlimas laipsniu, pereinama į laipsnio rodiklio rinkimo rėžimą
                            if (mode == 4)
                            {
                                mode = 2;
                                value = 0;
                            }
                            else
                            {
                                mode = 3;
                                value = 0;
                                currentPower = 0;
                            }
                            break;
                        case 'a': // primityviojo elemento simbolis, pereinant į jo rinkimo rėžimą
                            value = 0;
                            mode = 4;
                            break;
                        case '0': // skaičiai, renkam koeficiento rėikšmę
                        case '1':
                        case '2':
                        case '3':
                        case '4':
                        case '5':
                        case '6':
                        case '7':
                        case '8':
                        case '9':
                            if (mode == -1)
                            {
                                mode = 0;
                                calculatedCoeff = 0;
                                value = 0;
                            }
                            value = value * 10 + int.Parse(checkedS[i].ToString());
                            if (mode == 3)
                            {
                                currentPower = currentPower * 10 + int.Parse(checkedS[i].ToString());
                            }
                            break;
                    }
                }
                return result;
            }
        }
    }

    /// <summary>
    /// Vieną polinomo elementą apibrėžianti klasė
    /// </summary>
    public class PolynomialElement
    {
        /// <summary>
        /// Koeficientas prie polinomo nario
        /// </summary>
        public decimal Coefficient { get; set; }

        /// <summary>
        /// Nario laipsnis
        /// </summary>
        public int Power { get; set; }

        /// <summary>
        /// Sugeneruoja koeficiento tekstinį pavidalą
        /// </summary>
        /// <param name="useSeparator">Ar rodyti priekyje pliusą</param>
        /// <param name="q">q parametro reikšmė</param>
        /// <returns>Tekstinis pavidalas</returns>
        private string CoefficientToString(bool useSeparator, int q)
        {
            if (Coefficient == 0)
            {
                return "";
            }
            string start = "";
            if (Coefficient < 0)
            {
                start = " - ";
            }
            else if (useSeparator)
            {
                start = " + ";
            }

            string number;
            var nr = ((int)Math.Abs(Coefficient));
            if (q == 0)
            {
                if ((Power == 0) && (nr == 1))
                {
                    number = "1";
                }
                else if ((Power > 0) && (nr == 1))
                {
                    number = "";
                }
                else
                {
                    number = nr.ToString();
                }

            }
            else
            {
                if ((nr == 1) && (Power == 0))
                {
                    number = "1";
                }
                else if (nr == 1)
                {
                    number = "";
                }
                else if (nr == 2)
                {
                    number = "a";
                }
                else
                {
                    number = "a^" + (nr - 1);
                }
            }
            return start + number;
        }

        /// <summary>
        /// Sugeneruoja elemento tekstinį pavidalą
        /// </summary>
        /// <param name="useSeparator">Ar rodyti priekyje pliusą</param>
        /// <param name="q">q parametro reikšmė</param>
        /// <returns>Tekstinis pavidalas</returns>
        public string ToString(bool useSeparator, int q)
        {
            if (Coefficient == 0)
            {
                return "";
            }
            string coeff = CoefficientToString(useSeparator, q);
            if (Power == 0)
            {
                return coeff;
            }
            if (Power == 1)
            {
                return coeff + "x";
            }
            return coeff + "x^" + Power;
        }

        /// <summary>
        /// Sugeneruoja elemento tekstinį pavidalą, kai nerodomas priekyje pliusas ir q - nenurodytas
        /// </summary>
        /// <returns>Tesktinis pavidalas</returns>
        public override string ToString()
        {
            return ToString(false, 0);
        }
    }
}
