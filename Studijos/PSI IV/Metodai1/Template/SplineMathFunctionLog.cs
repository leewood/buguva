using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Metodai1
{
    class SplineMathFunctionLog: AbstractSpline
    {
        protected override double getMinPossibleValueInInterval(double start, double end)
        {
            if (start <= 0)
            {
                return 0.0000001;
            }
            else
            {
                return start;
            }
        }

        protected override double getMaxPossibleValueInInterval(double start, double end)
        {
            return end;
        }

        protected override double getFunctionValue(double x)
        {
            return Math.Log(x);
        }
    }
}
