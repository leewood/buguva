using System;
using System.Collections.Generic;
using System.Text;

namespace PSI3
{
    public interface MachineInterface
    {
        void Use(string reason);
        void Crash(string reason);
        void SetBlackBox(BlackBoxInterface box);
        void SetDocument(MachineBirthDocument document);
    }
}
