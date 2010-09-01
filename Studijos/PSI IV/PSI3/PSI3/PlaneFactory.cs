using System;
using System.Collections.Generic;
using System.Text;

namespace PSI3.Plane
{
    public class PlaneFactory : AbstractFactory
    {
        public override BlackBoxInterface CreateBlackBox(int securityLevel)
        {
            if (securityLevel > 0)
            {
                return new ArmyPlaneBlackBox();
            }
            else
            {
                return new PlaneBlackBox();
            }
        }

        public override MachineInterface CreateMachine(bool logAfterDestruction)
        {
            return new Plane(logAfterDestruction);
        }

        public override MachineBirthDocument CreateDocument(string owner)
        {
            return new PlaneDocument();
        }
    }
}
