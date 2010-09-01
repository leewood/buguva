using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Metodai1
{
    class ApproxValueMathFunction52: AbstractApproxValue
    {
        protected override double getMinPossibleValueInInterval(double start, double end)
        {
            return start;
        }

        protected override double getMaxPossibleValueInInterval(double start, double end)
        {
            return end;
        }

        protected override double getFunctionValue(double x)
        {
            double result = (1 + x) * Math.Sin(x);
            return result;
        }
    }
}
