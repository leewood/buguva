using System;
using System.Collections.Generic;
using System.Text;

namespace PSI3.Plane
{
    public class PlaneBlackBox : PSI3.BlackBoxInterface
    {
        #region BlackBoxInterface Members

        List<string> log = new List<string>();

        public string GetLog()
        {
            string result = "Plane black box content:\n--Start of data in black box--\n";
            foreach (string item in log)
            {
                result += item + "\n";
            }
            result += "--End of data in black box--\n";
            return result;
        }

        public void AddDataToLog(string data)
        {
            log.Add(DateTime.Now.ToString() + ": " + data);
        }

        #endregion
    }
}
