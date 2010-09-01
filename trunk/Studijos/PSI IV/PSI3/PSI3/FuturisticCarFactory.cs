using System;
using System.Collections.Generic;
using System.Text;

namespace PSI3.FuturisticCar
{
    public class FuturisticCarFactory : PSI3.AbstractFactory
    {
        public override BlackBoxInterface CreateBlackBox(int securityLevel)
        {
            return new FuturisticCarBlackBox();
        }

        public override MachineInterface CreateMachine(bool logAfterDestruction)
        {
            return new FuturisticCar();
        }

        public override MachineBirthDocument CreateDocument(string owner)
        {
            return new FuturisticCarDocument(owner);
        }
    }
}
