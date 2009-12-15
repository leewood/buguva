using System;
using System.Collections.Generic;
using System.Linq;

namespace KTUzd.Models
{
    public class Polynomial
    {
        readonly List<PolynomialElement> _elements = new List<PolynomialElement>();

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

        public bool HasElement(int power)
        {
            var element = (from elem in _elements where elem.Power == power select elem).FirstOrDefault();
            return element != null;
        }

        public PolynomialElement GetElement(int power)
        {
            var element = (from elem in _elements where elem.Power == power select elem).FirstOrDefault();
            if (element == null)
            {
                element = new PolynomialElement {Power = power, Coefficient = 0};
                _elements.Add(element);
            }
            return element;
        }

        public int PolynomialGrade
        {
            get
            {
                if (_elements.Count > 0)
                {
                    var sorted = _elements.Where(e => e.Coefficient != 0).OrderBy(elem => elem.Power);
                    return sorted.Last().Power;
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
            return result;
        }

        public static Polynomial operator *(Polynomial pol, decimal coeff)
        {
            Polynomial result = new Polynomial();
            for (int i = 0; i <= pol.PolynomialGrade; i++)
            {
                result[i] = pol[i]*coeff;
            }
            return result;
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
            Polynomial result = new Polynomial();
            for (int i = 0; i <= pol1.PolynomialGrade; i++)
            {
                Polynomial item = new Polynomial();
                for (int j = 0; j <= pol2.PolynomialGrade; j++)
                {
                    item[i + j] = pol2[j] * pol1[i];
                }
                result += item;
            }
            return result;
        }

        public static Polynomial operator +(Polynomial pol, decimal num)
        {
            Polynomial temp = new Polynomial();
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

        private static Polynomial _divisionRemainder;
        
        public static Polynomial operator  /(Polynomial pol1, Polynomial pol2)
        {
            return Divide(pol1, pol2);
        }

        public static Polynomial operator %(Polynomial pol1, Polynomial pol2)
        {
            Divide(pol1, pol2);
            return _divisionRemainder;
        }

        private static Polynomial Divide(Polynomial pol1, Polynomial pol2)
        {            
            if (pol1.PolynomialGrade < pol2.PolynomialGrade)
            {
                _divisionRemainder = pol1;
                return new Polynomial();
            }
            decimal coeff = pol2[pol2.PolynomialGrade];
            var mult = new Polynomial();
            mult[pol2.PolynomialGrade - pol1.PolynomialGrade] = coeff;
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
            Polynomial temp = new Polynomial();
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
            Polynomial pol = pol1 - pol2;
            return pol[pol.PolynomialGrade] > 0;            
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
    }

    public class PolynomialElement
    {
        public decimal Coefficient { get; set; }
        public int Power { get; set; }
    }
}
