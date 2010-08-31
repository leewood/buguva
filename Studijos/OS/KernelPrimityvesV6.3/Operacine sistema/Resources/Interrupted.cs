using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApplication1
{
    public class Interrupted : ResourceElement
    {
        public int type;
        public int additionalArgument;

        public InterruptedType Type
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


        private InterruptedType toIt(int type)
        {
            switch (type)
            {
                case TI: return InterruptedType.TI;
                case SI_CALL: return InterruptedType.SI_CALL;
                case SI_HALT: return InterruptedType.SI_HALT;
                case SI_IN: return InterruptedType.SI_IN;
                case SI_OUT: return InterruptedType.SI_OUT;
                case SI_SWAP: return InterruptedType.SI_SWAP;
                default: return InterruptedType.TI;
            }
        }

        private int fromIt(InterruptedType type)
        {
            switch (type)
            {
                case InterruptedType.TI: return TI;
                case InterruptedType.SI_SWAP: return SI_SWAP;
                case InterruptedType.SI_OUT: return SI_OUT;
                case InterruptedType.SI_IN: return SI_IN;
                case InterruptedType.SI_HALT: return SI_HALT;
                case InterruptedType.SI_CALL: return SI_CALL;
                default: return TI;
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


        public const int SI_HALT = 1;
        public const int SI_CALL = 3;
        public const int SI_IN = 2;
        public const int TI = 4;
        public const int SI_OUT = 5;
        public const int SI_SWAP = 6;
    }

    public enum InterruptedType
    {
        SI_HALT,
        SI_IN,
        SI_CALL,
        TI,
        SI_OUT,
        SI_SWAP
    }
}
