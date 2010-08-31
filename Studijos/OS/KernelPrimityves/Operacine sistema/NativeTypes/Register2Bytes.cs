using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    public class Register2Bytes
    {
        public const int PROCESSOR_MODE = 1;
        public const int CHANNEL_MODE = 0;
        byte[] value = new byte[2];
        public int mode = PROCESSOR_MODE;

        public int LongValue
        {
            get
            {
                return Utils.getLongFromReg2Bytes(this);
            }
        }

        public byte ByteValue
        {
            get
            {
                return (byte)LongValue;
            }
        }

        public Register2Bytes()
        {
            value[0] = 0;
            value[1] = 0;
        }
		
		public byte this [int p_intIndex]
		{
		   get
		   {
		      return getByteAt(p_intIndex);
		   }
		   set
		   {
		      writeAt(p_intIndex, value);
		   }
	    }

        public static implicit operator Register2Bytes(int val)
        {
            Register2Bytes reg = new Register2Bytes();
            reg[1] = (byte)Utils.subByteToChar((byte)(val % 16));
            reg[0] = (byte)Utils.subByteToChar((byte)(val / 16));
            return reg;
        }


        public void fromIntChannelMode(int val)
        {
            value[0] = (byte)(val / 256);
            value[1] = (byte)(val % 256);
        }

        public static implicit operator int(Register2Bytes reg)
        {
            if (reg.mode == PROCESSOR_MODE)
            {
                return Utils.getAddrFrom2Bytes(reg);
            }
            else
            {
                return Utils.getLongFromReg2Bytes(reg);
            }
        }


        public byte getByteAt(int p_intIndex)
        {
            if ((p_intIndex >= 0) && (p_intIndex <= 1))
            {
                return value[p_intIndex];
            }
            else
                return value[0];
        }

        public void writeAt(int p_intIndex, byte p_byteValue)
        {
            if ((p_intIndex >= 0) && (p_intIndex <= 1))
            {
                value[p_intIndex] = p_byteValue;
            }
        }

        public Register2Bytes clone()
        {
            Register2Bytes tmp = new Register2Bytes();
            for (int i = 0; i < 2; i++)
            {
                tmp[i] = this[i];
            }
            return tmp;
        }
    }
}
