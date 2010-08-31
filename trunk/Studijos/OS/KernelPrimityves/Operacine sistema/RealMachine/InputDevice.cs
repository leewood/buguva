using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Operacine_sistema.Utils;

namespace ConsoleApplication1
{
    public class InputDevice
    {
        public Block readBlock(byte p_byteWordReadLimit, ReleasePointer pointer)
        {            
            pointer.GetterMutex.WaitOne();
            Block blockMain = new Block();
            Word wordMain = new Word();
            int limit = p_byteWordReadLimit;
            if (limit == 0)
            {
                limit = 256;
            }
            int intWriteBytes = limit * 8;
            int intWriteWordAt = 0;
            bool noMore = false;            
            for (int i = 0; (i < intWriteBytes) && (pointer.CanContinue); i++)
            {
                byte dataByte = (byte)' ';
                if (!noMore)
                {
                    dataByte = (byte)Console.Read();
                }
                if ((dataByte == 13) || (dataByte == 10))
                {
                    noMore = true;
                    dataByte = (byte)' ';
                }
                wordMain.writeAt(i % 8, dataByte);
                if ((i + 1) % 8 == 0)
                {
                    blockMain.writeAt(intWriteWordAt, wordMain);
                    intWriteWordAt++;
                    wordMain = new Word();
                }
            }
            pointer.GetterMutex.ReleaseMutex();
            return blockMain;
        }
    }
}
