using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    public class RAMResourceElement: ResourceElement
    {
        public int blockAddress;
        public Block block;
        public bool useSupervisorMemory = false;

        public int BlockAddress
        {
            get
            {
                return blockAddress;
            }
            set
            {
                blockAddress = value;
            }
        }
        public bool AsSuvervisor
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

        public override bool HasName
        {
            get
            {
                return true;
            }
        }

        public override string ToString()
        {
            return String.Format("{0}:{1} memory block", AsSuvervisor?"ROM":"RAM", BlockAddress.ToString("X2"));
        }

        public override bool isEqual(ResourceElement res)
        {
            RAMResourceElement elem2 = (RAMResourceElement)res;
            if (elem2.useSupervisorMemory == this.useSupervisorMemory)
            {
                if (elem2.blockAddress == this.blockAddress)
                {
                    return true;
                }
                else if ((elem2.blockAddress == -1) || (this.blockAddress == -1))
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            else if (((elem2.blockAddress == -1) || (this.blockAddress == -1)) && (this.useSupervisorMemory == false))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
