using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{

    public class VirtualMemory
    {
        Processor proc;

        #region <Private methods>

        private Register4Bytes getBlockAddress(Word realAddress)
        {
            Register4Bytes tmp;
            tmp = realAddress[2, true].clone();
            return tmp;
        }

        private Register2Bytes getOffset(Word realAddress)
        {
            Register2Bytes tmp;
            tmp = realAddress[true, 6].clone();
            return tmp;
        }
        
        #endregion

        public VirtualMemory(Processor proc)
        {
            this.proc = proc;
        }

        public Word this[Register4Bytes reg]
        {
            get
            {
                Block tmp;
                Word address = proc.virtualToAbsoluteAddress(reg);
                tmp = proc.user_memory[getBlockAddress(address)];
                return tmp[getOffset(address)].clone();
            }
            set
            {
                Block tmp;
                Word address = proc.virtualToAbsoluteAddress(reg);
                tmp = proc.user_memory[getBlockAddress(address)];
                tmp[getOffset(address)] = value.clone();
            }
        }

        public Word this[int int_virtualMemoryAddress]
        {
            get
            {
                Register4Bytes tmp = new Register4Bytes(int_virtualMemoryAddress);
                return this[tmp];
            }
            set
            {
                Register4Bytes tmp = new Register4Bytes(int_virtualMemoryAddress);
                this[tmp] = value;
            }

        }

    }




}
