using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    public class Register3Bytes
    {
        byte[] value = new byte[3];

        public Register3Bytes()
        {
            value[0] = 0;
            value[1] = 0;
            value[2] = 0;
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

        public Register2Bytes this[int offset, bool use_offset]
        {
            get
            {
                return getRegPart(offset);
            }
            set
            {
                setRegPart(offset, value);
            }
        }

        public Register2Bytes getRegPart(int offset)
        {
            Register2Bytes tmp = new Register2Bytes();
            if (offset < 0)
            {
                offset = 0;
            }
            if (offset > 1)
            {
                offset = 1;
            }
            for (int i = 0; i < 2; i++)
            {
                tmp[i] = this.value[i + offset];
            }
            return tmp;
        }

        public void fromRealAddress(Word tempRealAddress)
        {
            value[2] = (byte)(Utils.getSubByte((char)tempRealAddress[4]) * 16 + Utils.getSubByte((char)tempRealAddress[5]));
            value[1] = (byte)(Utils.getSubByte((char)tempRealAddress[2]) * 16 + Utils.getSubByte((char)tempRealAddress[3]));
            value[0] = (byte)(Utils.getSubByte((char)tempRealAddress[1]) * 16 + Utils.getSubByte((char)tempRealAddress[0]));

        }


        public int Sector
        {
            get
            {
                return Utils.extractSector(this);
            }
        }        

        public static implicit operator Register3Bytes(Word realAddress)
        {
            Register3Bytes tmp = new Register3Bytes();
            tmp.fromRealAddress(realAddress);
            return tmp;
        }



        public static implicit operator Register3Bytes(int val2)
        {
            Register3Bytes reg = new Register3Bytes();
            UInt32 val = Utils.IntToUint(val2);
            reg[2] = (byte)(val % 256);
            val /= 256;
            reg[1] = (byte)(val % 256);
            val /= 256;
            reg[0] = (byte)(val);
            return reg;
        }

        public static implicit operator int(Register3Bytes reg)
        {
            UInt32 val2 = ((UInt32)reg[0] * 256 + (UInt32)reg[1]) * 256 + (UInt32)reg[2];
            return Utils.uintToInt(val2);
        }



        public void setRegPart(int offset, Register2Bytes reg)
        {
            if (offset < 0)
            {
                offset = 0;
            }
            if (offset > 1)
            {
                offset = 1;
            }
            for (int i = 0; i < 2; i++)
            {
                value[i + offset] = reg[i];
            }
        }

        public byte getByteAt(int p_intIndex)
        {
            if ((p_intIndex >= 0) && (p_intIndex <= 2))
            {
                return value[p_intIndex];
            }
            else
                return value[0];
        }

        public void writeAt(int p_intIndex, byte p_byteValue)
        {
            if ((p_intIndex >= 0) && (p_intIndex <= 2))
            {
                value[p_intIndex] = p_byteValue;
            }
        }

        public Register3Bytes clone()
        {
            Register3Bytes tmp = new Register3Bytes();
            for (int i = 0; i < 3; i++)
            {
                tmp[i] = this[i];
            }
            return tmp;
        }
    }
}
