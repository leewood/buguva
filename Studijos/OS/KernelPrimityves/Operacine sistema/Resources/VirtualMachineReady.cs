using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApplication1
{
    public class VirtualMachineReady : Resource
    {
        private int machineAddress;
        public int returnElementsCount = 0;

        public void setMachineAddress(int p_intMachineAddress)
        {
            machineAddress = p_intMachineAddress;
        }

        public int getMachineAddress()
        {
            return machineAddress;
        }
    }
}
