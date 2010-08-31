using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApplication1
{
    public enum statusEnum
    {
        success,
        error
    }

    public class readStatus : ResourceElement
    {
        public int status;

        public statusEnum OperationStatus
        {
            get
            {
                switch (status)
                {
                    case STATUS_ERROR: return statusEnum.error;
                    case STATUS_SUCCESS: return statusEnum.success;
                    default: return statusEnum.error;
                }
            }
            set
            {
                switch (value)
                {
                    case statusEnum.error: status = STATUS_ERROR; break;
                    case statusEnum.success: status = STATUS_SUCCESS; break;
                    default: status = STATUS_ERROR; break;
                }
            }
        }

        public int OnChannel
        {
            get
            {
                return onChannel;
            }
            set
            {
                onChannel = value;
            }
        }

        public override bool isEqual(ResourceElement res)
        {
            readStatus status = (readStatus)res;
            if ((this.onChannel == status.onChannel) || (this.onChannel == 0) || (status.onChannel == 0))
            {
                return true;
            }
            else
            {
                return false;
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
            return String.Format("IOI{0} Status: {1}", OnChannel, (OperationStatus == statusEnum.error)?"Error": "Success");
        }
        public int onChannel = 0;
        public const int STATUS_SUCCESS = 1;
        public const int STATUS_ERROR = 2;
        public const int CHANNEL_OUTPUT_DEVICE = 1;
        public const int CH1 = 1;
        public const int CHANNEL_INPUT_DEVICE = 2;
        public const int CH2 = 2;
        public const int CHANNEL_HARDDISK = 3;
        public const int CH3 = 3;
        public const int CHANNEL_ANY = 0;
    }
}
