using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KTUzd.Models
{
    public class MathTools
    {
        public static int FirstPrimaryDivider(int num)
        {
            for (int i = 2; i <= num / 2; i++)
            {
                if (num % i == 0)
                {
                    return i;
                }
            }
            return num;
        }

        public static bool IsPrimary(int num)
        {
            return FirstPrimaryDivider(num) == num;
        }

        private static int _lastPower;
        private static int _lastPrimary;
        public static int LastPower
        {
            get
            {
                return _lastPower;
            }
        }

        public static int LastPrimary
        {
            get
            {
                return _lastPrimary;
            }
        }

        public static bool IsPrimaryPower(int num)
        {
            _lastPrimary = FirstPrimaryDivider(num);
            _lastPower = 1;
            int value = _lastPrimary;
            while (value < num)
            {
                value *= _lastPrimary;
                _lastPower++;
            }
            if (value == num)
            {
                return true;
            }
            return false;
        }
    }
}
