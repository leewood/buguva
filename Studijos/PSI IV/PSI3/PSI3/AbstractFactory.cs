using System;
using System.Collections.Generic;
using System.Text;

namespace PSI3
{
    public abstract class AbstractFactory
    {
        public abstract BlackBoxInterface CreateBlackBox(int securityLevel);
        public abstract MachineInterface CreateMachine(bool logAfterDestruction);
        public abstract MachineBirthDocument CreateDocument(string owner);
        public static AbstractFactory CreateFactory(string machineCode)
        {
            switch (machineCode)
            {
                case "Plane": return new Plane.PlaneFactory();
                default: return new FuturisticCar.FuturisticCarFactory();
            }
        }
    }
}
