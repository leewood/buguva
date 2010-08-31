using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    public class VirtualMachineRunner: Process
    {
        public VirtualMachineRunner()
        {
            step.Add(stepToUserMode);
        }

        private int stepToUserMode()
        {
            //kernel.processor.resumeHLP();
            if (kernel.processor.MODE == Processor.SUPERVISOR_MODE)
            {
                kernel.processor.userMode(savedRegisters.IC, savedRegisters.SP,
                                          savedRegisters.PTR, savedRegisters.C);
            }
            kernel.processor.nextHLP();
            return 0;
        }
    }
}
