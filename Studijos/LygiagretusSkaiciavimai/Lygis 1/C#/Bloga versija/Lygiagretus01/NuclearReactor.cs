using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Lygiagretus01
{
    public class NuclearReactor
    {

        public int controlRodsPosition = 50;
        public int sum = 0;
        public int count = 0;
        public static int MINIMUM_ALLOWED = 10;

        public NuclearReactor(int startPosition)
        {
            controlRodsPosition = startPosition;
        }

        /******** KRITINES SEKCIJOS PRADŽIA ********/
        public void ChangeControlRodStatus(int delta)
        {
            //lock (this)
            {
                if ((controlRodsPosition + delta >= MINIMUM_ALLOWED))
                {
                    sum += controlRodsPosition;
                    controlRodsPosition += delta;
                    count++;
                }
            }
        }
        /******** KRITINES SEKCIJOS PABAIGA ********/
    }
}
