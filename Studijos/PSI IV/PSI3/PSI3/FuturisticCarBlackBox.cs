using System;
using System.Collections.Generic;
using System.Text;

namespace PSI3.FuturisticCar
{
    public class FuturisticCarBlackBox : PSI3.BlackBoxInterface
    {
        #region BlackBoxInterface Members

        List<string> log = new List<string>();

        public string GetLog()
        {
            string result = "Some log about developement:\n";
            foreach (string item in log)
            {
                result += item + "\n";
            }
            return result;
        }

        public void AddDataToLog(string data)
        {
            log.Add(data);
        }


        #endregion
    }
}
