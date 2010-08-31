using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    public class SavedRegisters
    {
        public Register4Bytes C;
        public Register4Bytes PTR;
        public Register4Bytes IC;
        public Register4Bytes SP;

        public Byte mode;
        public Byte IOI;
        public Byte SI;
        public Byte PI;
        public Byte TI;
        public Byte CH1;
        public Byte CH2;
        public Byte CH3;
    }
}
