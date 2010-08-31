using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    public class FileDescriptor
    {
        public int sector = 0;
        public int oldsector = 0;
        public int oldblock = 0;
        public int block = 0;
        public int word = 0;
        public int bytenr = 0;
        public bool eod = false;
        public bool eob = false;

        public Block temp = new Block();

        public FileDescriptor()
        {
        }
        public bool next()
        {

            bool result = false;
            bytenr++;
            if (bytenr > 7)
            {
                bytenr = 0;
                word++;
            }
            if (word > 255)
            {
                oldblock = block;
                word = 0;
                block++;
                result = true;

            }
            if (block > 65535)
            {
                block = 0;
                oldsector = sector;
                sector++;
            }
            if (sector > 15)
            {
                sector = 0;
                eod = true;
            }
            eob = result;
            return result;
        }
    }
}
