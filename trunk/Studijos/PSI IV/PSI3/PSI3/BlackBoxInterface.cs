using System;
using System.Collections.Generic;
using System.Text;

namespace PSI3
{
    public interface BlackBoxInterface
    {
        string GetLog();
        void AddDataToLog(string data);
    }
}
