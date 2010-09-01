using System;
using System.Collections.Generic;
using System.Text;

namespace PSI3.FuturisticCar
{
    public class FuturisticCar : PSI3.MachineInterface
    {
        #region MachineInterface Members

        private BlackBoxInterface box = AbstractFactory.FuturisticCarFactory.CreateBlackBox(0);
        private MachineBirthDocument document = AbstractFactory.FuturisticCarFactory.CreateDocument("");

        public FuturisticCar()
        {
            
        }

        public void Use(string reason)
        {
            box.AddDataToLog("Tried to use for " + reason + " with no success on " + DateTime.Now.ToString() + ", because it it still being designed");
        }

        public void Crash(string reason)
        {
            box.AddDataToLog("It was still designed, but somehow was destroyed because of " + reason);
        }

        #endregion

        #region MachineInterface Members


        public void SetBlackBox(BlackBoxInterface box)
        {
            this.box = box;
        }

        public void SetDocument(MachineBirthDocument document)
        {
            this.document = document;
            box.AddDataToLog(this.document.GetInfo());

        }

        #endregion
    }
}
