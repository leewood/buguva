using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Metodai1
{
    public abstract class AbstractMathFunction
    {
        public abstract double getMinPossibleValueInInterval(double start, double end);
        public abstract double getMaxPossibleValueInInterval(double start, double end);
        public abstract double getFunctionValue(double x);
        
    }
}
