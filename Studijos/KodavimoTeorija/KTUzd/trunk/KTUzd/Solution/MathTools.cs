using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KTUzd.Models
{
    /// <summary>
    /// Įvairūs matematiniai įrankiai
    /// </summary>
    public class MathTools
    {
        /// <summary>
        /// Randa pirmą pirminį daliklį
        /// </summary>
        /// <param name="num">Skaičiui kuriam ieškome</param>
        /// <returns>Pirmas pirminis daliklis</returns>
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

        /// <summary>
        /// Nustato ar skaičius pirminis
        /// </summary>
        /// <param name="num">Skaičius</param>
        /// <returns>Ar pirminis</returns>
        public static bool IsPrimary(int num)
        {
            return FirstPrimaryDivider(num) == num;
        }

        private static int _lastPower;
        private static int _lastPrimary;

        /// <summary>
        /// Paskutinis naudotas laipsnis
        /// </summary>
        public static int LastPower
        {
            get
            {
                return _lastPower;
            }
        }

        /// <summary>
        /// Paskutinis naudotas pirminis skaičius
        /// </summary>
        public static int LastPrimary
        {
            get
            {
                return _lastPrimary;
            }
        }

        /// <summary>
        /// Nustato ar duotas skaičius yra kokio nors pirminio skaičiaus laipsnis
        /// </summary>
        /// <param name="num">Duotas skaičius</param>
        /// <returns>Ar yra</returns>
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
