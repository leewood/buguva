using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KTUzd.Models
{
    public class Polynomial
    {
        List<PolynomialElement> _elements = new List<PolynomialElement>();

        private decimal _rounding = (decimal)0.000000001;

        public Polynomial()
        {
            
        }

        public override string ToString()
        {
            if (this == 0)
            {
                return "f(x) = 0";
            }
            else
            {

                StringBuilder b = new StringBuilder();
                b.Append("f(x) = ");
                string separ = "";
                foreach (var elem in _elements.OrderByDescending(e => e.Power))
                {
                    if (elem.Coefficient != 0)
                    {

                        if (elem.Power > 0)
                        {
                            if (elem.Coefficient == -1)
                            {
                                b.Append(" - x^" + elem.Power);
                            }
                            else if (elem.Coefficient == 1)
                            {
                                b.Append(separ + "x^" + elem.Power);
                            }
                            else if (elem.Coefficient > 0)
                            {
                                b.Append(separ + elem.Coefficient + "x^" + elem.Power);
                            }
                            else
                            {
                                b.Append(" - " + Math.Abs(elem.Coefficient) + "x^" + elem.Power);
                            }


                        }
                        else
                        {
                            if (elem.Coefficient > 0)
                            {
                                b.Append(separ + elem.Coefficient);
                            }
                            else if (elem.Coefficient < 0)
                            {
                                b.Append(" - " + Math.Abs(elem.Coefficient));
                            }

                        }
                        separ = " + ";
                    }
                }
                return b.ToString();
            }
        }

        public Polynomial(decimal i)
        {
            this[0] = i;
        }

        public Polynomial(CyclotomicCoset coset)
        {
            for (int i = 0; i < coset.Items.Count; i++)
            {
                this[coset.Items[i]] = 1;
            }
        }

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

        public void OptimizeElements()
        {
            if (_elements != null)
            {
                _elements = (from elem in _elements where Math.Abs(elem.Coefficient) > _rounding select elem).ToList();
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

        public int Q { get; set; }

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
                    var sorted = _elements.Where(e => Math.Abs(e.Coefficient) > _rounding).OrderBy(elem => elem.Power);
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
                result[i] = pol1[i] + pol2[i];
            }
            result.Q = pol1.Q;
            return result.Modus(pol1.Q);
        }

        public static Polynomial operator *(Polynomial pol, decimal coeff)
        {
            var result = new Polynomial();
            for (int i = 0; i <= pol.PolynomialGrade; i++)
            {
                result[i] = pol[i]*coeff;
            }
            result.Q = pol.Q;
            return result.Modus(pol.Q);
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
                    item[i + j] = pol2[j] * pol1[i];
                }
                result += item;
            }
            result.Q = pol1.Q;
            return result;
        }

        public static Polynomial operator +(Polynomial pol, decimal num)
        {
            var temp = new Polynomial();
            temp[0] = num;
            return pol + temp;
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
            return result.Modus(pol1.Q);
        }

        public static Polynomial operator %(Polynomial pol1, Polynomial pol2)
        {
            Divide(pol1, pol2);
            _divisionRemainder.Q = pol1.Q;
            return _divisionRemainder.Modus(pol1.Q);
        }

        private static Polynomial Divide(Polynomial pol1, Polynomial pol2)
        {            
            if ((pol1 == 0) || (pol1.PolynomialGrade < pol2.PolynomialGrade))
            {
                _divisionRemainder = pol1;
                return new Polynomial();
            }
            decimal coeff = pol2[pol2.PolynomialGrade];
            decimal coeff2 = pol1[pol1.PolynomialGrade];
            /*
            if (Math.Abs(coeff) > Math.Abs(coeff2))
            {
                _divisionRemainder = pol1;
                return new Polynomial();                
            }
             */
            var mult = new Polynomial();
            mult[pol1.PolynomialGrade - pol2.PolynomialGrade] = coeff2 / coeff;
            Polynomial next = pol1 - (pol2*mult);                
            return mult + Divide(next, pol2);            
        }

        public static bool operator  ==(Polynomial pol1, Polynomial pol2)
        {
            Polynomial pol = pol1 - pol2;
            return ((pol.PolynomialGrade == 0) && (pol[0] == 0));
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
            Polynomial result = new Polynomial();
            for (int i = 0; i <= PolynomialGrade; i++)
            {
                result[i] = (this[i] >= 0) ? (this[i]%q) : q - (this[i]%q);
            }
            result.Q = q;
            return result;
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
    }

    public class PolynomialElement
    {
        public decimal Coefficient { get; set; }
        public int Power { get; set; }
    }
}
