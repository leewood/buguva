using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ConsoleApplication1
{
    static class Utils
    {
        public static bool overflow = false;
	    public static byte getSubByte(char c)
		{
		    switch (c)
			{
			   case '0': return 0;
			   case '1': return 1;
			   case '2': return 2;
			   case '3': return 3;
			   case '4': return 4;
			   case '5': return 5;
			   case '6': return 6;
			   case '7': return 7;
			   case '8': return 8;
			   case '9': return 9;
			   case 'A': return 10;
			   case 'B': return 11;
               case 'C': return 12;
               case 'D': return 13;
               case 'E': return 14;
               case 'F': return 15;
               
			}
			return (byte)c;
	    }

        static public int StringToBlockAddress(string s)
        {
            int tmp;
            tmp = ((getSubByte(s[0]) * 16 + getSubByte(s[1])) * 16 + getSubByte(s[2])) * 16 + getSubByte(s[3]);
            return tmp;

        }


        static public string BlockAddressToString(int BlockAddress)
        {
            string s = "";
            char c1 = subByteToChar((byte)(BlockAddress % 16));
            BlockAddress /= 16;
            char c2 = subByteToChar((byte)(BlockAddress % 16));
            BlockAddress /= 16;
            char c3 = subByteToChar((byte)(BlockAddress % 16));
            BlockAddress /= 16;
            char c4 = subByteToChar((byte)(BlockAddress));
            s += c4;
            s += c3;
            s += c2;
            s += c1;
            return s;
        }
		
		public static char subByteToChar(byte b)
		{
		    switch (b)
			{
			   case 0: return '0';
			   case 1: return '1';
			   case 2: return '2';
			   case 3: return '3';
			   case 4: return '4';
			   case 5: return '5';
			   case 6: return '6';
			   case 7: return '7';
			   case 8: return '8';
			   case 9: return '9';
			   case 10: return 'A';
			   case 11: return 'B';
               case 12: return 'C';
               case 13: return 'D';
               case 14: return 'E';
               case 15: return 'F';
               
			}
            return '0';
		   
		}
		
	
		
		public static Register2Bytes convert2Bytes(Word input, int which)
		{
		   Register2Bytes local = new Register2Bytes();
		   local[0] = input[which * 2];
		   local[1] = input[which * 2 + 1];
		   return local;
		}
		
		public static Register2Bytes convert2Bytes(Register4Bytes input, int which)
		{
            Register2Bytes local = new Register2Bytes();
		   local[0] = input[which * 2];
		   local[1] = input[which * 2 + 1];
		   return local;
		}
		
		public static Register2Bytes convert2Bytes(Register3Bytes input)
		{
            Register2Bytes local = new Register2Bytes();
		   local[0] = input[1];
		   local[1] = input[2];
		   return local;
		}

		
		public static byte getByteFromReg2Bytes(Register2Bytes p_reg2bytesMain)
		{
		    return (byte)(getSubByte((char)p_reg2bytesMain[0]) * 16 + getSubByte((char)p_reg2bytesMain[1]));
	    }
		
        public static int getLongFromReg2Bytes(Register2Bytes p_reg2bytesMain)
        {
            return p_reg2bytesMain[0] * 256 + p_reg2bytesMain[1];
        }

        public static int getLongFrom3Bytes(Register3Bytes p_reg3bytesMain)
        {
            return (p_reg3bytesMain[0] * 256 + p_reg3bytesMain[1]) * 256 + p_reg3bytesMain[2];
        }


        public static int getAddrFrom2Bytes(Register2Bytes reg2Bytes)
        {
            return (byte)(getSubByte((char)reg2Bytes[0]) * 16 + getSubByte((char)reg2Bytes[1]));
        }

        public static int getLongFrom4Bytes(Register4Bytes p_reg4bytesMain)
        {
            int val = 0;
			for (int i = 0; i < 4; i++)
			{
			   val = val * 16 + getSubByte((char)p_reg4bytesMain[i]);
			}
			return val;
        }

        public static int getLongFromWord(Word p_wordMain)
        {
            UInt32 val = 0;
            int val2 = 0;
			for (int i = 0; i < 8; i++)
			{
			   val = val * 16 + getSubByte((char)p_wordMain[i]);
			}

            if (val > int.MaxValue)
            {
                val2 = -(int)(UInt32.MaxValue - val + 1);
            } else {
                val2 = (int)val;
            }
			return val2;

        }


        public static int uintToInt(UInt32 val)
        {
            int val2;
            if (val > int.MaxValue)
            {
                val2 = -(int)(UInt32.MaxValue - val + 1);
            }
            else
            {
                val2 = (int)val;
            }
            return val2;

        }


        public static UInt32 IntToUint(int value)
        {
            UInt32 value2 = (UInt32)value;
            if (value < 0)
            {
                value2 = (UInt32.MaxValue + (UInt32)value + 1);
            }
            else
            {

            }
            return value2;
        }

        public static Register4Bytes setLongTo4Bytes(int value)
        {
            Register4Bytes tmp = new Register4Bytes();
            for (int i = 0; i < 4; i++)
            {
                tmp[3 - i] = (byte)subByteToChar((byte)(value % 16));
                value = value / 16;
            }
            return tmp;
        }

        public static Word setLongToWord(int value)
        {
            Word tmp = new Word();
            UInt32 value2 = (UInt32)value;
            if (value < 0)
            {
                value2 = (UInt32.MaxValue + (UInt32)value + 1);
            }
            else
            {

            }
            for (int i = 0; i < 8; i++)
            {
                tmp[7 - i] = (byte)subByteToChar((byte)(value2 % 16));
                value2 = value2 / 16;
            }
            if (value2 != 0)
            {
                overflow = true;
            }
            return tmp;
        }

        public static Int64 getInt64FromWord(Word p_WordMain)
		{
            Int64 val = 0;
			for (int i = 0; i < 8; i--)
			{
			   val = val * 256 + getSubByte((char)p_WordMain[i]);
			}
			return val;
		    
		}
		
		public static int extractSector(Register3Bytes p_reg3Bytes)
		{
		    return getSubByte((char)p_reg3Bytes[0]);
	    }
		
		public static int extractBlockAddress(Register3Bytes p_reg3Bytes)
		{
		    return p_reg3Bytes[1] * 256 + p_reg3Bytes[2];
		}
		
		public static string wordPartToString(Word p_Word)
		{
		    string s = "";
			for (int i = 0; i < 4; i++)
			  s += (char)p_Word[i];
			return s;
		}

       
        public static Word convRegister4BytesToWord(Register4Bytes reg)
        {
           byte uzp = (byte)((reg[0] >= (byte)'8')?'F':'0');
           Word tmp = new Word();
           tmp[0] = uzp;
           tmp[1] = uzp;
           tmp[2] = uzp;
           tmp[3] = uzp;
           tmp[4] = reg[0];
           tmp[5] = reg[1];
           tmp[6] = reg[2];
           tmp[7] = reg[3];
           return tmp;
        }
    }
}
