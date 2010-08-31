using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApplication1
{
    public class SWAPBlock : Resource
    {
    }

    public class SWAPBlockElement : ResourceElement
    {
        public bool UseSupervisorMemory
        {
            get
            {
                return useSupervisorMemory;
            }
            set
            {
                useSupervisorMemory = value;
            }
        }

        public int RAMBlockAddress
        {
            get
            {
                return memoryAddress;
            }
            set
            {
                memoryAddress = value;
            }
        }

        public int HDDBlockAddress
        {
            get
            {
                return hddBlockAddress;
            }
            set
            {
                hddBlockAddress = value;
            }
        }

        public int hddBlockAddress;
        public int memoryAddress;
        public bool useSupervisorMemory = false;

        public override bool HasName
        {
            get
            {
                return true;
            }
        }

        public override string ToString()
        {
            string ramPrefix = (UseSupervisorMemory) ? "ROM" : "RAM";
            string ramName = (memoryAddress < 0)? String.Format("Any {0}", ramPrefix): String.Format("{0}:{1}", ramPrefix, memoryAddress.ToString("X2"));
            return String.Format("SWAP({0}) <-> {1}", HDDBlockAddress.ToString("X6"), ramName);            
        }
    }
}
