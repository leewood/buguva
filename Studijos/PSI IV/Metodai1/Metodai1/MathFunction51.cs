using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Metodai1
{
    class MathFunction51: AbstractMathFunction
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
            double xSqr = x * x;
            return (2 * xSqr + 6) / (xSqr - 2 * x + 5);
        }
    }
}
