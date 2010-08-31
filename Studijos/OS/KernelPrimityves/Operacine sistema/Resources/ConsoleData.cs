using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApplication1
{
    public class ConsoleData : Resource
    {
    }

    public enum DriverReadWriteMode
    {
        read,
        write
    }

    public enum DriverConversion
    {
        no_conversion,
        int_conversion
    }

    public class ConsoleDataElement : ResourceElement
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

        public DriverConversion ConversionType
        {
            get
            {
                return toConvMode(type);
            }
            set
            {
                type = fromConvMode(value);
            }
        }


        private int fromConvMode(DriverConversion type)
        {
            switch (type)
            {
                case DriverConversion.int_conversion: return TYPE_INT;
                case DriverConversion.no_conversion: return TYPE_RAW;
                default: return TYPE_RAW;
            }
        }

        private DriverConversion toConvMode(int type)
        {
            switch (type)
            {
                case TYPE_INT: return DriverConversion.int_conversion;
                case TYPE_RAW: return DriverConversion.no_conversion;
                default: return DriverConversion.no_conversion;
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

        public int mode;
        public int memoryAddress;
        public bool useSupervisorMemory = false;
        public int type;
        public const int MODE_READ = 1;
        public const int MODE_WRITE = 2;
        public const int TYPE_RAW = 1;
        public const int TYPE_INT = 2;

        public override bool HasName
        {
            get
            {
                return true;
            }
        }

        public override string ToString()
        {
            var memName = String.Format("{0}:{1}", UseSupervisorMemory?"ROM":"RAM", memoryAddress.ToString("X2"));
            var consoleName = (ConversionType == DriverConversion.int_conversion)?"C(int)":"C(RAW)";
            if (ReadWriteMode == DriverReadWriteMode.read)
            {
                return String.Format("{0} -> {1}", consoleName, memName);
            }
            else
            {
                return String.Format("{0} -> {1}", memName, consoleName);
            }
        }
    }
}
