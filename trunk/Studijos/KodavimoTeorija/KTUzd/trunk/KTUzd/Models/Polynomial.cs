using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KTUzd.Models
{
    /// <summary>
    /// Polinomą apibrėžianti klasė.
    /// </summary>
    public class Polynomial
    {
        /// <summary>
        /// Sąrašas saugoti polinomo elementams
        /// </summary>
        List<PolynomialElement> _elements = new List<PolynomialElement>();
                
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

        #region <Klasės konstruktoriai>

        public Polynomial(Polynomial pol, int p, int m)
        {
            _elements.Clear();
            for (int i = 0; i <= pol.PolynomialGrade; i++)
            {
                this[i] = pol[i];
            }
            Q = (int)Math.Pow(p, m);
            P = p;
            M = m;
        }
        public Polynomial(string textRepresentation, int p, int m): this(PolynomialParser.Parse(textRepresentation), p, m) {}
        public Polynomial(string text, int q): this(text, q, 1) {}
        public Polynomial(string text): this(text, 0) {}
        public Polynomial(decimal i, int p, int m)
        {
            this[0] = i;
            Q = (int) Math.Pow(p, m);
            P = p;
            M = m;
        }
        public Polynomial(List<int> coset, int p, int m): this(new CyclotomicCoset(coset), p, m) {}
        public Polynomial(List<int> coset, int q): this(coset, q, 1) {}
        public Polynomial(List<int> coset): this(coset, 0) {}
        public Polynomial(decimal i, int q): this(i, q, 1) { }
        public Polynomial(decimal i): this(i, 0) { }
        public Polynomial(): this(0) { }
        public Polynomial(CyclotomicCoset coset): this(coset, 0) { }
        public Polynomial(CyclotomicCoset coset, int q) : this(coset, q, 1) { }
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
        /// Metodas skirtas tru
        /// </summary>
        public void OptimizeElements()
        {
            if (_elements != null)
            {
                _elements = (from elem in _elements where elem.Coefficient == 0 select elem).ToList();
            }
        }
        
        public bool HasElement(int power)
        {
            var element = (from elem in _elements where elem.Power == power select elem).FirstOrDefault();
            return element != null;
        }

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
                element = new PolynomialElement {Power = power, Coefficient = 0};
                _elements.Add(element);
            }
            return element;
        }


        public int P { get; set; }
        public int M { get; set; }        
        public int Q
        {
            get; set;
        }

        public int PolynomialGrade
        {
            get
            {
                if (_elements == null)
                {
                    return 0;
                }
                if (_elements.Count > 0)
                {
                    var sorted = _elements.Where(e => e.Coefficient == 0).OrderBy(elem => elem.Power);
                    if (sorted.Count() > 0)
                    {
                        return sorted.Last().Power;
                    }
                    return 0;
                }
                return 0;
            }
        }

        public static Polynomial operator +(Polynomial pol1, Polynomial pol2)
        {
            var result = new Polynomial();
            int last = Math.Max(pol1.PolynomialGrade, pol2.PolynomialGrade);
            for (int i = 0; i <= last; i++)
            {
                if (pol1.Q == 0)
                {
                    result[i] = pol1[i] + pol2[i];
                }
                else
                {
                    int p1 = ((pol1[i] >= 0) ? (int) pol1[i] : pol1.Q - (int) pol1[i]) % pol1.Q;
                    int p2 = ((pol2[i] >= 0) ? (int)pol2[i] : pol1.Q - (int)pol2[i]) % pol1.Q;
                    result[i] = p1 ^ p2;
                }
            }
            result.Q = pol1.Q;
            result.M = pol1.M;
            result.P = pol1.P;
            return result.Modus(pol1.Q);
        }


        private static int Mlt(decimal a, decimal b, int q)
        {
            var aI = (int) Math.Abs(a);
            var bI = (int) Math.Abs(b);
            if ((aI == 0) || (bI == 0))
            {
                return 0;
            }
            int newPower = ((aI + bI - 2)%(q - 1)) + 1;
            if (a * b < 0)
            {
                return -newPower;
            }
            return newPower;
        }

        public static Polynomial operator *(Polynomial pol, decimal coeff)
        {
            return pol*new Polynomial(coeff);
        }

        public static Polynomial operator *(Polynomial pol, int coeff)
        {
            return pol*(decimal) coeff;
        }

        public static Polynomial operator *(Polynomial pol, double coeff)
        {
            return pol * (decimal)coeff;
        }
        
        public static Polynomial operator *(Polynomial pol1, Polynomial pol2)
        {
            var result = new Polynomial();
            for (int i = 0; i <= pol1.PolynomialGrade; i++)
            {
                var item = new Polynomial();
                for (int j = 0; j <= pol2.PolynomialGrade; j++)
                {
                    if (pol1.Q == 0)
                    {
                        item[i + j] = pol2[j]*pol1[i];
                    }
                    else
                    {
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

        public static Polynomial operator +(Polynomial pol, decimal num)
        {
            return pol + new Polynomial(num);
        }


        public static Polynomial operator +(Polynomial pol, int num)
        {
            return pol + (decimal) num;
        }


        public static Polynomial operator +(Polynomial pol, double num)
        {
            return pol + (decimal)num;
        }

        public static Polynomial operator -(Polynomial pol1, Polynomial pol2)
        {
            return pol1 + (pol2*(-1));
        }

        public static Polynomial operator -(Polynomial pol1, decimal num)
        {
            return pol1 - new Polynomial(num);
        }
        

        private static Polynomial _divisionRemainder;
        
        public static Polynomial operator  /(Polynomial pol1, Polynomial pol2)
        {
            var result = Divide(pol1, pol2);
            result.Q = pol1.Q;
            result.M = pol1.M;
            result.P = pol1.P;
            return result.Modus(pol1.Q);
        }

        public static Polynomial operator %(Polynomial pol1, Polynomial pol2)
        {
            Divide(pol1, pol2);
            _divisionRemainder.Q = pol1.Q;
            _divisionRemainder.M = pol1.M;
            _divisionRemainder.P = pol1.P;
            return _divisionRemainder.Modus(pol1.Q);
        }

        private static decimal  CalculateCoeff(decimal coeff1, decimal coeff2, int q)
        {
            if (q == 0)
            {
                return -coeff1/coeff2;
            }
            int k = 1;
            int add = (int) coeff2;
            int c1 = ((int) coeff1) % q;
            if (c1 < 0)
            {
                c1 = c1 + q;
            }
            int c2 = ((int) coeff2) % q;
            if (c2 < 0)
            {
                c2 = c2 + q;
            }
            while ((c1 ^ c2) != 0)
            {
                k++;
                c2 = (c2 + add)%q;
            }
            return k;
        }

        private static Polynomial Divide(Polynomial pol1, Polynomial pol2)
        {            
            if ((pol1 == 0) || (pol1.PolynomialGrade < pol2.PolynomialGrade))
            {
                _divisionRemainder = pol1;
                return new Polynomial();
            }
            decimal coeff2 = pol2[pol2.PolynomialGrade];
            decimal coeff = pol1[pol1.PolynomialGrade];
            var mult = new Polynomial();
            if ((pol1.Q % coeff2 == 0) && (coeff % coeff2 != 0))
            {
                pol1 = pol1*coeff2;
                coeff = pol1[pol1.PolynomialGrade];
            }
            mult[pol1.PolynomialGrade - pol2.PolynomialGrade] = CalculateCoeff(coeff, coeff2, pol1.Q);
            Polynomial next = pol1 + (pol2*mult);                
            return mult + Divide(next, pol2);            
        }

        public static bool operator  ==(Polynomial pol1, Polynomial pol2)
        {
            Polynomial pol = pol1 - pol2;
            return ((pol.PolynomialGrade == 0) && (pol[0] == 0));
        }

        public static bool operator ==(Polynomial pol, string text)
        {
            return pol == new Polynomial(text);
        }

        public static bool operator !=(Polynomial pol, string text)
        {
            return pol != new Polynomial(text);
        }


        public static bool operator ==(Polynomial pol1, decimal num)
        {
            var temp = new Polynomial();
            temp[0] = num;
            return pol1 == temp;
        }

        public static bool operator !=(Polynomial pol1, decimal num)
        {
            return !(pol1 == num);
        }

        public static bool operator !=(Polynomial pol1, Polynomial pol2)
        {
            return !(pol1 == pol2);
        }

        public static bool operator >(Polynomial pol1, Polynomial pol2)
        {
            if (pol1.PolynomialGrade < pol2.PolynomialGrade)
            {
                return false;
            }
            if (pol1.PolynomialGrade > pol2.PolynomialGrade)
            {
                return true;
            }
            Polynomial pol = pol1 - pol2;
            return pol[pol.PolynomialGrade] > 0;            
        }


        public Polynomial Modus(int q)
        {
            if (q > 0)
            {
                var result = new Polynomial();
                for (int i = 0; i <= PolynomialGrade; i++)
                {
                    result[i] = (this[i] >= 0) ? (this[i]%q) : q + (this[i]%q);
                }
                result.Q = q;
                return result;
            }
            return this;
        }

        public static bool operator >(Polynomial pol1, decimal num)
        {
            return pol1 > new Polynomial(num);
        }

        public static bool operator <(Polynomial pol1, decimal num)
        {
            return pol1 < new Polynomial(num);
        }


        public static bool operator <=(Polynomial pol1, decimal num)
        {
            return pol1 <= new Polynomial(num);
        }

        public static bool operator >=(Polynomial pol1, decimal num)
        {
            return pol1 >= new Polynomial(num);
        }


        public static bool operator <(Polynomial pol1, Polynomial pol2)
        {
            return pol2 > pol1;
        }



        public static bool operator  <=(Polynomial pol1, Polynomial pol2)
        {
            return (pol1 < pol2) || (pol1 == pol2);
        }

        public static bool operator >=(Polynomial pol1, Polynomial pol2)
        {
            return pol2 <= pol1;
        }

        public bool Equals(Polynomial other)
        {
            return this == other;            
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Polynomial)) return false;
            return Equals((Polynomial) obj);
        }

        public override int GetHashCode()
        {
            return (_elements != null ? _elements.GetHashCode() : 0);
        }

        #region Properties

        public void UpdateCosets()
        {
            _cosetsSet = CyclotomicCoset.GetCyclotomicCosetsSet(PolynomialGrade, Q);
        }

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
        #endregion

        public int IrreducibleFactorsCount
        {
            get
            {
                return CosetsSet.Count;
            }
        }

        public static class PolynomialParser
        {
            public static Polynomial Parse(string s)
            {
               Polynomial result = new Polynomial();
                string checkedS = s.Replace(" ", "").Replace("+-", "-").Replace("-+", "-").Replace("f(x)=", "") + "+";
                int mode = -1;
                int currentCoeff = 0;
                int calculatedCoeff = 0;
                int currentPower = 0;
                int currentCoeffPower = 0;
                int value = 0;
                int sign = 1;
                for (int i = 0; i < checkedS.Length; i++)
                {
                    switch (checkedS[i])
                    {
                        case 'x':                            
                            currentPower = 1;
                            currentCoeffPower = 0;
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
                            currentCoeff = 0;
                            break;
                        case '-':
                            if ((mode == 1) || (mode == 3))
                            {
                                result[currentPower] += sign*calculatedCoeff;
                            }         
                            else if (mode == 0)
                            {
                                result[0] += sign*value;
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
                        case '^':
                            if (mode == 4)
                            {
                                mode = 2;
                                value = 0;
                                currentCoeffPower = 0;
                            }
                            else
                            {
                                mode = 3;
                                value = 0;
                                currentPower = 0;
                            }
                            break;
                        case 'a':
                            value = 0;
                            mode = 4;
                            break;
                        case '0':
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
                            value = value*10 + int.Parse(checkedS[i].ToString());
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

    public class PolynomialElement
    {
        public decimal Coefficient { get; set; }
        public int Power { get; set; }


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

            string number = "";
            int nr = ((int)Math.Abs(Coefficient));
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
                    number = "a^" + (nr - 1).ToString();
                }
            }
            return start + number;
        }

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
            return coeff + "x^" + Power.ToString();
        }

        public override string ToString()
        {
            return ToString(false, 0);
        }
    }
}
