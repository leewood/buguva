using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    public class Register4Bytes
    {
        byte[] value = new byte[4];

        public Register4Bytes()
        {
            value[0] = 0;
            value[1] = 0;
            value[2] = 0;
            value[3] = 0;
        }

        public Register4Bytes(int val2)
        {
            UInt32 val = Utils.IntToUint(val2);
            for (int i = 0; i < 4; i++)
            {
                value[3 - i] = (byte)(Utils.subByteToChar((byte)(val % 16)));
                val /= 16;
            }
        }



        public byte this[int p_intIndex]
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
            if (offset > 2)
            {
                offset = 2;
            }
            for (int i = 0; i < 2; i++)
            {
                tmp[i] = this.value[i + offset];
            }
            return tmp;
        }

        public void setRegPart(int offset, Register2Bytes reg)
        {
            if (offset < 0)
            {
                offset = 0;
            }
            if (offset > 2)
            {
                offset = 2;
            }
            for (int i = 0; i < 2; i++)
            {
                value[i + offset] = reg[i];
            }
        }


        public Register2Bytes getHighPart()
        {
            return Utils.convert2Bytes(this, 0);
        }

        public Register2Bytes getLowPart()
        {
            return Utils.convert2Bytes(this, 1);
        }

        public byte getByteAt(int p_intIndex)
        {
            if ((p_intIndex >= 0) && (p_intIndex <= 3))
            {
                return value[p_intIndex];
            }
            else
                return value[0];
        }

        public void writeAt(int p_intIndex, byte p_byteValue)
        {
            if ((p_intIndex >= 0) && (p_intIndex <= 3))
            {
                value[p_intIndex] = p_byteValue;
            }
        }

        public static Register4Bytes operator ++(Register4Bytes i)
        {
            int value = Utils.getLongFrom4Bytes(i);
            Register4Bytes tmp = new Register4Bytes();
            value++;
            tmp = Utils.setLongTo4Bytes(value);
            return tmp;
        }


        public static implicit operator Register4Bytes(string val)
        {
            Register4Bytes reg = new Register4Bytes();
            int n = 4 - val.Length;
            for (int i = 0; i < n; i++)
            {
                reg[i] = (byte)'0';
            }
            for (int i = n; i < 4; i++)
            {
                reg[i] = (byte)val[i - n];
            }
            return reg;
        }

        public static implicit operator string(Register4Bytes reg)
        {
            string tmp = "";
            for (int i = 0; i < 4; i++)
            {
                tmp += (char)reg[i];
            }
            return tmp;
        }

        public static implicit operator Register4Bytes(int val)
        {
            Register4Bytes reg = new Register4Bytes(val);
            return reg;
        }

        public static implicit operator int(Register4Bytes reg)
        {
            return reg.toInt();
        }

        public static Register4Bytes operator --(Register4Bytes i)
        {
            int value = Utils.getLongFrom4Bytes(i);
            Register4Bytes tmp = new Register4Bytes();
            value--;
            tmp = Utils.setLongTo4Bytes(value);
            return tmp;
        }

        public static Register4Bytes operator +(Register4Bytes i, Register4Bytes j)
        {
            int value = Utils.getLongFrom4Bytes(i);
            int value2 = Utils.getLongFrom4Bytes(j);
            Register4Bytes tmp = new Register4Bytes();
            value = value + value2;
            tmp = Utils.setLongTo4Bytes(value);
            return tmp;
        }



        public static Register4Bytes operator -(Register4Bytes i, Register4Bytes j)
        {
            int value = Utils.getLongFrom4Bytes(i);
            int value2 = Utils.getLongFrom4Bytes(j);
            Register4Bytes tmp = new Register4Bytes();
            value = value - value2;
            tmp = Utils.setLongTo4Bytes(value);
            return tmp;
        }

        public static Register4Bytes operator *(Register4Bytes i, Register4Bytes j)
        {
            int value = Utils.getLongFrom4Bytes(i);
            int value2 = Utils.getLongFrom4Bytes(j);
            Register4Bytes tmp = new Register4Bytes();
            value = value * value2;
            tmp = Utils.setLongTo4Bytes(value);
            return tmp;
        }

        public static Register4Bytes operator /(Register4Bytes i, Register4Bytes j)
        {
            int value = Utils.getLongFrom4Bytes(i);
            int value2 = Utils.getLongFrom4Bytes(j);
            Register4Bytes tmp = new Register4Bytes();
            value = value / value2;
            tmp = Utils.setLongTo4Bytes(value);
            return tmp;
        }

        public static Register4Bytes operator %(Register4Bytes i, Register4Bytes j)
        {
            int value = Utils.getLongFrom4Bytes(i);
            int value2 = Utils.getLongFrom4Bytes(j);
            Register4Bytes tmp = new Register4Bytes();
            value = value % value2;
            tmp = Utils.setLongTo4Bytes(value);
            return tmp;
        }


        public Register4Bytes clone()
        {
            Register4Bytes tmp = new Register4Bytes();
            for (int i = 0; i < 4; i++)
            {
                tmp[i] = this[i];
            }
            return tmp;
        }

        public int toInt()
        {
            int value = 0;
            for (int i = 0; i < 4; i++)
            {
                value = value * 16 + Utils.getSubByte((char)(this.value[i]));
            }
            return value;
        }

    }
}
