using System;
using System.Collections.Generic;
using System.Text;

namespace PSI3.FuturisticCar
{
    public class FuturisticCarDocument : PSI3.MachineBirthDocument
    {
        #region MachineBirthDocument Members

        string title = "X98";

        public FuturisticCarDocument(string owner)
        {
            if (title != "")
            {
                title = owner;
            }
        }

        public string GetInfo()
        {
            return String.Format("No document for still being designed super {0} futuristic car", title);
        }

        #endregion
    }
}
