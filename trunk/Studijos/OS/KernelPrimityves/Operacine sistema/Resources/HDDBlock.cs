using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApplication1
{
    public class HDDBlock : Resource
    {
    }

    public class HDDBlockElement : ResourceElement
    {
        public int mode;
        public int hddBlockAddress;
        public int memoryAddress;
        public bool useSupervisorMemory = false;

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

        public DriverReadWriteMode ReadWriteMode
        {
            get
            {
                return fromWorkMode(mode);
            }
            set
            {
                mode = toWorkMode(value);
            }
        }

        private DriverReadWriteMode fromWorkMode(int mode)
        {
            switch (mode)
            {
                case MODE_READ: return DriverReadWriteMode.read;
                case MODE_WRITE: return DriverReadWriteMode.write;
                default: return DriverReadWriteMode.read;
            }
        }

        private int toWorkMode(DriverReadWriteMode mode)
        {
            switch (mode)
            {
                case DriverReadWriteMode.read: return MODE_READ;
                case DriverReadWriteMode.write: return MODE_WRITE;
                default: return MODE_READ;
            }
        }

        public const int MODE_READ = 2;
        public const int MODE_WRITE = 1;
    }
}
