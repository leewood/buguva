using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    public static class WordExtensions
    {
        public static Word ToOSWord(this byte[] data)
        {
            return ToOSWord(data, 0);
        }

        public static Word ToOSWord(this byte[] data, int start)
        {
            return ToOSWord(data, start, data.Length);
        }

        public static Word ToOSWord(this byte[] data, int start, int limit)
        {
            var result = new Word();
            for (int i = 0; i < 8; i++)
            {
                result[i] = ((data.Length > start + i) && (start + i < limit)) ? data[start + 1] : (byte)0;
            }
            return result;
        }
    }

    public class Word
    {


        private byte[] value = new byte[8];

        public Word()
        {
            for (int i = 0; i <= 7; i++)
                value[i] = 0;
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

        public Register4Bytes this[int offset, bool use_offset]
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

        public Register2Bytes this[bool use_2_byte, int offset]
        {
            get
            {
                if (offset > 6)
                {
                    offset = 6;
                }
                if (offset < 0)
                {
                    offset = 0;
                }
                Register2Bytes tmp = new Register2Bytes();
                tmp[0] = value[offset];
                tmp[1] = value[offset + 1];
                return tmp;
            }
            set
            {
                if (offset > 6)
                {
                    offset = 6;
                }
                if (offset < 0)
                {
                    offset = 0;
                }
                
                this.value[offset] = value[0];
                this.value[offset + 1] = value[1];

            }
        }

        public Word(int val)
        {
            Word tmp = Utils.setLongToWord(val);
            for (int i = 0; i < 8; i++)
            {
                this.value[i] = tmp.value[i];
            }
        }


        public static implicit operator Word(int val)
        {
            Word reg = new Word(val);
            return reg;
        }

        public static implicit operator int(Word reg)
        {
            return reg.toInt();
        }


        public static implicit operator Word(string val)
        {
            Word reg = new Word();
            int n = 8 - val.Length;
            for (int i = 0; i < n; i++)
            {
                reg[i] = (byte)'0';
            }
            for (int i = n; i < 8; i++)
            {
                reg[i] = (byte)val[i - n];
            }
            return reg;
        }

        public static implicit operator string(Word reg)
        {
            string tmp = "";
            for (int i = 0; i < 8; i++)
            {
                tmp += (char)reg[i];
            }
            return tmp;
        }

        public static implicit operator Word(Register4Bytes val)
        {
            return Utils.convRegister4BytesToWord(val.clone());
        }

        public static implicit operator Register4Bytes(Word reg)
        {
            return reg[1, true];
        }


        public byte getByteAt(int p_intIndex)
        {
            if ((p_intIndex >= 0) && (p_intIndex <= 7))
            {
                return value[p_intIndex];
            }
            else
            {
                return value[0];
            }
        }

        public Register4Bytes getHighPart()
        {
            Register4Bytes tmp = new Register4Bytes();
            tmp[0] = this[0];
            tmp[1] = this[1];
            tmp[2] = this[2];
            tmp[3] = this[3];
            return tmp;
        }

        public void setHighPart(Register4Bytes reg)
        {
            for (int i = 0; i < 4; i++)
            {
                this[i] = reg[i];
            }
        }

        public void setLowPart(Register4Bytes reg)
        {
            for (int i = 0; i < 4; i++)
            {
                this[i + 4] = reg[i];
            }
        }

        public Register4Bytes getRegPart(int offset)
        {
            Register4Bytes tmp = new Register4Bytes();
            if (offset < 0)
            {
                offset = 0;
            }
            if (offset > 4)
            {
                offset = 4;
            }
            for (int i = 0; i < 4; i++)
            {
                tmp[i] = this.value[i + offset];
            }
            return tmp;
        }

        public void setRegPart(int offset, Register4Bytes reg)
        {
            if (offset < 0)
            {
                offset = 0;
            }
            if (offset > 4)
            {
                offset = 4;
            }
            for (int i = 0; i < 4; i++)
            {
                value[i + offset] = reg[i];
            }
        }

        public Register4Bytes getLowPart()
        {
            Register4Bytes tmp = new Register4Bytes();
            tmp[0] = this[4];
            tmp[1] = this[5];
            tmp[2] = this[6];
            tmp[3] = this[7];
            return tmp;

        }

        public void writeAt(int p_intIndex, byte p_byteValue)
        {
            if ((p_intIndex >= 0) && (p_intIndex <= 7))
            {
                value[p_intIndex] = p_byteValue;
            }
        }

        public static Word operator %(Word i, Word j)
        {
            int value = Utils.getLongFromWord(i);
            int value2 = Utils.getLongFromWord(j);
            Word tmp = new Word();
            value = value % value2;
            tmp = Utils.setLongToWord(value);
            return tmp;
        }

        public static Word operator +(Word i, Word j)
        {
            int value = Utils.getLongFromWord(i);
            int value2 = Utils.getLongFromWord(j);
            Word tmp = new Word();
            value = value + value2;
            tmp = Utils.setLongToWord(value);
            return tmp;
        }

        public static Word operator -(Word i, Word j)
        {
            int value = Utils.getLongFromWord(i);
            int value2 = Utils.getLongFromWord(j);
            Word tmp = new Word();
            value = value - value2;
            tmp = Utils.setLongToWord(value);
            return tmp;
        }

        public static Word operator *(Word i, Word j)
        {
            int value = Utils.getLongFromWord(i);
            int value2 = Utils.getLongFromWord(j);
            Word tmp = new Word();
            value = value * value2;
            tmp = Utils.setLongToWord(value);
            return tmp;
        }

        public static Word operator /(Word i, Word j)
        {
            int value = Utils.getLongFromWord(i);
            int value2 = Utils.getLongFromWord(j);
            Word tmp = new Word();
            value = value / value2;
            tmp = Utils.setLongToWord(value);
            return tmp;
        }

        public static Word operator &(Word i, Word j)
        {
            int value = Utils.getLongFromWord(i);
            int value2 = Utils.getLongFromWord(j);
            Word tmp = new Word();
            value = value & value2;
            tmp = Utils.setLongToWord(value);
            return tmp;
        }

        public static Word operator |(Word i, Word j)
        {
            int value = Utils.getLongFromWord(i);
            int value2 = Utils.getLongFromWord(j);
            Word tmp = new Word();
            value = value | value2;
            tmp = Utils.setLongToWord(value);
            return tmp;
        }

        public static Word operator ~(Word i)
        {
            int value = Utils.getLongFromWord(i);
            
            Word tmp = new Word();
            value = ~value;
            tmp = Utils.setLongToWord(value);
            return tmp;
        }

        public static Word operator ^(Word i, Word j)
        {
            int value = Utils.getLongFromWord(i);
            int value2 = Utils.getLongFromWord(j);
            Word tmp = new Word();
            value = value ^ value2;
            tmp = Utils.setLongToWord(value);
            return tmp;
        }
        public static bool operator ==(Word i, Word j)
        {
            int value = Utils.getLongFromWord(i);
            int value2 = Utils.getLongFromWord(j);
            return (value == value2);
        }
        
        public static bool operator !=(Word i, Word j)
        {
            if (((Object)j) == null)
            {
                return (Object)i != null;
            }
            else
            {
            int value = Utils.getLongFromWord(i);
            int value2 = Utils.getLongFromWord(j);
            return (value != value2);
            }
        }
        
        public static bool operator >(Word i, Word j)
        {
            int value = Utils.getLongFromWord(i);
            int value2 = Utils.getLongFromWord(j);
            return (value > value2);
        }
        public static bool operator <(Word i, Word j)
        {
            int value = Utils.getLongFromWord(i);
            int value2 = Utils.getLongFromWord(j);
            return (value < value2);
        }

        override public bool Equals(Object o)
        {
            int value = Utils.getLongFromWord(this);
            int value2 = Utils.getLongFromWord((Word)o);
            return (value == value2);

        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public String toString()
        {
            String s = "";

            s = s + (char)getByteAt(0);
            s = s + (char)getByteAt(1);
            s = s + (char)getByteAt(2);
            s = s + (char)getByteAt(3);
            s = s + (char)getByteAt(4);
            s = s + (char)getByteAt(5);
            s = s + (char)getByteAt(6);
            s = s + (char)getByteAt(7);

            return s;
        }

        public Word clone()
        {
            Word tmp = new Word();
            for (int i = 0; i < 8; i++)
            {
                tmp[i] = this[i];
            }
            return tmp;
        }

        public int toInt()
        {
            return Utils.getLongFromWord(this);
        }

        public byte[] ToBytesArray()
        {
            var result = new byte[8];
            value.CopyTo(result, 0);
            return result;
        }

        public override string ToString()
        {
            return Encoding.ASCII.GetString(value);
        }
    }
}
