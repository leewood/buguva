using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    public class OutputDevice
    {
        public void writeBlock(Block p_blockMain, byte p_byteWordsToWrite)
        {
            string strLine = "";
            int intWriteBlocks = p_byteWordsToWrite;
            if (intWriteBlocks == 0)
            {
                intWriteBlocks = 256;
            }
            for (int i = 0; i < intWriteBlocks; i++)
            {
                if (p_blockMain.getWordAt(i) != null)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        byte b = p_blockMain.getWordAt(i).getByteAt(j);
                        if (b > 0)
                        {
                            if ((char)b == '#')
                            {
                                strLine += '\n';
                            }
                            else
                            {
                                strLine += (char)(p_blockMain.getWordAt(i).getByteAt(j));
                            }
                        }
                    }
                }
            }

            Console.Write(strLine);
        }
    }
}
