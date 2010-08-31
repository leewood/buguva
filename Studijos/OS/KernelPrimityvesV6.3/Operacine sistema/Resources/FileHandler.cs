using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    public enum FileHandlerConversionMode
    {
        no_conversion,
        int_conversion
    }

    public enum FileHandlerWorkType
    {
        read,
        write,
        append,
        continue_writing,
        close
    }

    public class FileHandler: ResourceElement
    {
        public int currentPosition = 0;
        public int count = 0;
        public int bufferAddress = -1;
        public int destinationAddress = 0;
        public int freeBlocksSection = 0;
        public int startPosition = 0;
        public int type = 1;
        public int mode = 1;
        public Kernel kernel;
        public bool useSupervisorMemory = false;



        public int BufferAddress
        {
            get
            {
                return bufferAddress;
            }
            set
            {
                bufferAddress = value;
            }
        }


        public int FreeBlocksSectionAddress
        {
            get
            {
                return freeBlocksSection;
            }
            set
            {
                freeBlocksSection = value;
            }
        }

        public int WordsCount
        {
            get
            {
                return count;
            }
            set
            {
                count = value;
            }
        }

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
                return destinationAddress;
            }
            set
            {
                destinationAddress = value;
            }
        }

        public int HDDPosition
        {
            get
            {
                return currentPosition;
            }
            set
            {
                currentPosition = value;
            }
        }

        public FileHandlerConversionMode ConversionMode
        {
            get
            {
                return toConvMode(mode);
            }
            set
            {
                mode = fromConvMode(value);
            }
        }

        public string ConversionString
        {
            get
            {
                return (ConversionMode == FileHandlerConversionMode.int_conversion) ? "I" : "H";
            }
        }

        public FileHandlerWorkType ReadWriteMode
        {
            get
            {
                return toWorkType(type);
                
            }
            set
            {
                type = fromWorkType(value);
            }
        }

        public string HandlerWorkTypeString
        {
            get
            {
                switch (ReadWriteMode)
                {
                    case FileHandlerWorkType.append: return "append";
                    case FileHandlerWorkType.close: return "close";
                    case FileHandlerWorkType.continue_writing: return "writenext";
                    case FileHandlerWorkType.read: return "read";
                    case FileHandlerWorkType.write: return "write";
                    default: return "idle";
                }
            }
        }

        public const int TYPE_READ = 1;
        public const int TYPE_WRITE = 2;
        public const int TYPE_APPEND = 3;
        public const int TYPE_CONTINUE_WRITING = 4;
        public const int TYPE_CLOSE = 5;
        public const int MODE_RAW = 1;
        public const int MODE_INT = 2;


        private FileHandlerConversionMode toConvMode(int mode)
        {
            switch (mode)
            {
                case MODE_INT: return FileHandlerConversionMode.int_conversion;
                case MODE_RAW: return FileHandlerConversionMode.no_conversion;
                default: return FileHandlerConversionMode.no_conversion;
            }
        }

        private int fromConvMode(FileHandlerConversionMode mode)
        {
            switch (mode)
            {
                case FileHandlerConversionMode.no_conversion: return MODE_RAW;
                case FileHandlerConversionMode.int_conversion: return MODE_INT;
                default: return MODE_RAW;
            }
        }

        private FileHandlerWorkType toWorkType(int type)
        {
            switch (type)
            {
                case TYPE_READ: return FileHandlerWorkType.read;
                case TYPE_WRITE: return FileHandlerWorkType.write;
                case TYPE_APPEND: return FileHandlerWorkType.append;
                case TYPE_CONTINUE_WRITING: return FileHandlerWorkType.continue_writing;
                case TYPE_CLOSE: return FileHandlerWorkType.close;
                default: return FileHandlerWorkType.read;
            }
        }

        private int fromWorkType(FileHandlerWorkType type)
        {
            switch (type)
            {
                case FileHandlerWorkType.read: return TYPE_READ;
                case FileHandlerWorkType.write: return TYPE_WRITE;
                case FileHandlerWorkType.append: return TYPE_APPEND;
                case FileHandlerWorkType.continue_writing: return TYPE_CONTINUE_WRITING;
                case FileHandlerWorkType.close: return TYPE_CLOSE;
                default: return TYPE_READ;
            }
        }

        public FileHandler()
        {
        }

        public FileHandler(Kernel kernel, int startAddresss, int p_intDestinationAddresss, int type)
        {
            this.kernel = kernel;
            this.currentPosition = startAddresss;
            this.destinationAddress = p_intDestinationAddresss;
            this.type = type;
        }

        override public bool isEqual(ResourceElement e)
        {
            FileHandler fh = (FileHandler)e;
            if ((fh.bufferAddress < 0) || (this.bufferAddress < 0))
            {
                return true;
            } else {
                return ((fh.bufferAddress == this.bufferAddress) && 
                        (fh.useSupervisorMemory == this.useSupervisorMemory));
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
            string ramName= String.Format("{0}:{1}", (UseSupervisorMemory)? "ROM":"RAM", RAMBlockAddress.ToString("X2"));
            switch (ReadWriteMode)
            {
                case FileHandlerWorkType.read: return String.Format("({0}:{2}) -> {3}", ConversionString, startPosition.ToString("X4"), HDDPosition.ToString("X4"), ramName);
                case FileHandlerWorkType.write: return String.Format("{3} -> ({0}:{2})", ConversionString, startPosition.ToString("X4"), HDDPosition.ToString("X4"), ramName);
                case FileHandlerWorkType.continue_writing: return String.Format("{3} ->> ({0}:{2})", ConversionString, startPosition.ToString("X4"), HDDPosition.ToString("X4"), ramName);
                case FileHandlerWorkType.append: return String.Format("({0}:{2}) <-> {3}", ConversionString, startPosition.ToString("X4"), HDDPosition.ToString("X4"), ramName);
                case FileHandlerWorkType.close: return String.Format("({0}:{2}) close", ConversionString, startPosition.ToString("X4"), HDDPosition.ToString("X4"));
                default: return "Any FileHandler";
            }
        }
    }
}
