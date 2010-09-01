using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Metodai1
{
    class MathFunction52: AbstractMathFunction
    {
        public override double getMinPossibleValueInInterval(double start, double end)
        {
            return start;
        }

        public override double getMaxPossibleValueInInterval(double start, double end)
        {
            return end;
        }

        public override double getFunctionValue(double x)
        {
            double result = (1 + x) * Math.Sin(x);
            return result;
        }
    }
}
