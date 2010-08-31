using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApplication1
{
    public class VirtualMachineResource : ResourceElement
    {
        private string programName;

        public string ProgramName
        {
            get
            {
                return programName;
            }
            set
            {
                programName = value;
            }
        }

        private int _virtualMachineIndex = -1;
        public int VirtualMachineIndex 
        {
            get
            { return _virtualMachineIndex;  }
            set
            {
                _virtualMachineIndex = value;
            }
        }

        public void setProgramName(String p_strProgramName)
        {
            programName = p_strProgramName;
        }

        public String getProgramName()
        {
            return programName;
        }

        public override bool HasName
        {
            get
            {
                return true;
               
            }
        }

        public override string ToString()
        {
            var index = VirtualMachineIndex;
            if (index >= 0)
            {
                return String.Format("VM {0}: {1}", VirtualMachineIndex, ProgramName);
            }
            else
            {
                return String.Format("Any VM with {0} name", (!String.IsNullOrEmpty(ProgramName)) ? ProgramName : "any");
            }
        }
    }
}
