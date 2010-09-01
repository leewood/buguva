using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Metodai1
{
    class SplineMathFunction51: AbstractSpline
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
            double xSqr = x * x;
            return (2 * xSqr + 6) / (xSqr - 2 * x + 5);
        }
    }
}
