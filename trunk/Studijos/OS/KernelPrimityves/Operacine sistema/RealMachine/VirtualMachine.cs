using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    public class VirtualMachine
    {
        Processor proc;
        public Register4Bytes IC
        {
            get
            {
                return proc.IC;
            }
            set
            {
                proc.IC = value;
                proc.done = true;
            }
        }
        public Register4Bytes PTR
        {
            get
            {
                return proc.PTR;
            }
            set
            {
                proc.PTR = value;
                proc.done = true;
            }
        }

        public Register4Bytes C
        {
            get
            {
                return proc.C;
            }
            set
            {
                proc.C = value;
                proc.done = true;
            }
        }

        public Register4Bytes SP
        {
            get
            {
                return proc.SP;
            }
            set
            {
                proc.SP = value;
                proc.done = true;
            }
        }



        public VirtualMemory memory;

        public VirtualMachine(Processor proc)
        {
            this.proc = proc;
            memory = new VirtualMemory(proc);
        }

    }
}
