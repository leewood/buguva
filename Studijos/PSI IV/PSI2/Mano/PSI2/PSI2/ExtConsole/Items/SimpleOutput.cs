using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PSI2.ExtConsole.Items
{
    class SimpleOutput: Printable
    {
        #region Printable Members

        public string Print(string outputData)
        {
            return outputData;
        }

        #endregion
    }
}
