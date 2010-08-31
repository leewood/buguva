using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApplication1
{
    public class InterruptResource : Resource
    {
        private int type;
    }

    public enum InterruptValues
    {
        no_interrupt = 0,
        wrong_operation,
        wrong_address,
        overflow,
        standart_input,
        standart_output,
        stop_program,
        block_in_swap,
        specific_os_call,
        timer,
        channel_device_job_complete,
        channel_device_error
    }

    public class InterruptResourceElement : ResourceElement
    {
        public int type;
        public int additionalArgument;

        public InterruptValues Type
        {
            get
            {
                return toIt(type);
            }
            set
            {
                type = fromIt(value);
            }
        }

        public string TypeName
        {
            get
            {
                switch (Type)
                {
                    case InterruptValues.block_in_swap: return "Block in SWAP";
                    case InterruptValues.channel_device_error: return "Channel error";
                    case InterruptValues.channel_device_job_complete: return "Channel ready";
                    case InterruptValues.no_interrupt: return "No interrupt";
                    case InterruptValues.overflow: return "Overflow";
                    case InterruptValues.specific_os_call: return "OS specific call";
                    case InterruptValues.standart_input: return "Input";
                    case InterruptValues.standart_output: return "Output";
                    case InterruptValues.stop_program: return "Halt";
                    case InterruptValues.timer: return "Timer";
                    case InterruptValues.wrong_address: return "Wrong address";
                    case InterruptValues.wrong_operation: return "Wrong operation";
                    default: return "";
                }
            }
        }

        private int fromIt(InterruptValues type)
        {
            switch (type)
            {
                case InterruptValues.no_interrupt: return 0;
                case InterruptValues.wrong_operation: return 1;
                case InterruptValues.wrong_address: return 2;
                case InterruptValues.overflow: return 3;
                case InterruptValues.standart_input: return 4;
                case InterruptValues.standart_output: return 5;
                case InterruptValues.stop_program: return 6;
                case InterruptValues.block_in_swap: return 7;
                case InterruptValues.specific_os_call: return 8;
                case InterruptValues.timer: return 9;
                case InterruptValues.channel_device_job_complete: return 10;
                case InterruptValues.channel_device_error: return 11;
                default: return 0;
            }
        }


        private InterruptValues toIt(int type)
        {
            switch (type)
            {
                case 0: return InterruptValues.no_interrupt;
                case 1: return InterruptValues.wrong_operation;
                case 2: return InterruptValues.wrong_address;
                case 3: return InterruptValues.overflow;
                case 4: return InterruptValues.standart_input;
                case 5: return InterruptValues.standart_output;
                case 6: return InterruptValues.stop_program;
                case 7: return InterruptValues.block_in_swap;
                case 8: return InterruptValues.specific_os_call;
                case 9: return InterruptValues.timer;
                case 10: return InterruptValues.channel_device_job_complete;
                case 11: return InterruptValues.channel_device_error;
                default: return InterruptValues.no_interrupt;
            }
        }

        public int AdditionalArgument
        {
            get
            {
                return additionalArgument;
            }
            set
            {
                additionalArgument = value;
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
            return String.Format("INT {0}:{2} - {1}", ((int)Type).ToString("X2"), TypeName, AdditionalArgument.ToString("X4"));
        }
    }
}
