using System;
using System.Collections.Generic;
using System.Text;

namespace PSI3.Plane
{
    public class PlaneDocument : PSI3.MachineBirthDocument
    {
        #region MachineBirthDocument Members

        string data = "";

        public PlaneDocument()
        {
            data = "Boing 977 Created at " + DateTime.Now.ToString();
        }

        public string GetInfo()
        {
            return data;
        }

        #endregion
    }
}
