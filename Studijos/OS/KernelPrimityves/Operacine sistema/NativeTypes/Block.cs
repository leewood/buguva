using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    public static class BlockExtensions
    {
        public static Block ToBlock(this byte[] data, int start, int limit)
        {
            var result = new Block();
            int j = 0;
            for (int i = start; i < limit; i += 8)
            {
                result[j] = data.ToOSWord(i, limit);
                j++;
            }
            return result;
        }

        public static Block ToBlock(this byte[] data, int start)
        {
            return ToBlock(data, start, data.Length);
        }

        public static Block ToBlock(this byte[] data)
        {
            return ToBlock(data, 0);
        }
    }

    public class Block
    {
        private Word[] value = new Word[256];

        public Block()
        {
            for (int i = 0; i <= 255; i++)
                value[i] = new Word();
        }

	    public Word this [int p_intIndex]
		{
		   get
		   {
		      return getWordAt(p_intIndex);
		   }
		   set
		   {
		      writeAt(p_intIndex, value);
		   }
	    }

        public Word this[Register2Bytes reg]
        {
            get
            {
                int address = Utils.getAddrFrom2Bytes(reg);
                return getWordAt(address);
            }
            set
            {
                int address = Utils.getAddrFrom2Bytes(reg);
                writeAt(address, value);
            }

        }

        public Word getWordAt(int p_intIndex)
        {
            if ((p_intIndex >= 0) && (p_intIndex <= 255))
            {
                return value[p_intIndex];
            }
            else
                return value[0];
        }


        public void writeAt(int p_intIndex, Word p_wordValue)
        {
            if ((p_intIndex >= 0) && (p_intIndex <= 255))
            {
                value[p_intIndex] = p_wordValue.clone();
            }
        }

        public byte[] ToBytesArray()
        {
            var result = new byte[256 * 8];
            var pos = 0;
            for (int i = 0; i < 256; i++)
            {
                var line = value[i];
                for (int j = 0; j < 8; j++)
                {
                    result[pos] = line[j];
                    pos++;
                }
            }
            return result;
        }

        public override string ToString()
        {
            var result = new StringBuilder();
            for (int i = 0; i < 256; i++)
            {
                result.Append(String.Format("{0}: {1}\n", i.ToString("X2"), value[i].ToString()));
            }
            return result.ToString();
        }
    }
}
