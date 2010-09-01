using System;
using System.Collections.Generic;
using System.Text;

namespace PSI3.Plane
{
    public class Plane : PSI3.MachineInterface
    {
        #region MachineInterface Members
        private BlackBoxInterface box = AbstractFactory.PlaneFactory.CreateBlackBox(0);
        private MachineBirthDocument document = AbstractFactory.PlaneFactory.CreateDocument("");
        private bool canBeUsed = true;
        private bool logAfterDestruction = false;


        public Plane(bool logAfterDestruction)
        {
            this.logAfterDestruction = logAfterDestruction;
        }

        public void Use(string reason)
        {
            if ((canBeUsed) || (logAfterDestruction))
            {
                box.AddDataToLog("Plane was used for " + reason);
                if (reason == "Fun")
                {
                    Crash("Plane was used for fun while being drunk");
                }
            }
            else
            {
                box.AddDataToLog("Tried to use unusable plane");
            }
        }

        public void Crash(string reason)
        {
            box.AddDataToLog("Plane suddenly crashed, because " + reason);
            canBeUsed = false;  
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
            box.AddDataToLog("Plane just created. Info: " + this.document.GetInfo());
        }

        #endregion
    }
}
