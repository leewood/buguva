using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    public class Memory
    {
        const int MAX_MEM = 65536;
        const int STARTING_MEM = 500;
        Block[] value = new Block[MAX_MEM];

        public int Size
        {
            get
            {
                return STARTING_MEM;
            }
        }

        public Memory()
        {
            for (int i = 0; i < STARTING_MEM; i++)
            {
                value[i] = new Block();
            }
        }

        private int _debugMem = 0;

        private bool _debugChanged = false;

        public bool DebugChanged
        {
            get
            {
                bool temp = _debugChanged;
                _debugChanged = false;
                return _debugChanged;
            }
        }

        public int DebuggingMem
        {
            get
            {
                return _debugMem;
            }
            set
            {
                _debugMem = value;
            }
        }

        public Memory(int int_inicialSize)
        {
            if (int_inicialSize < 1)
            {
                int_inicialSize = 1;
            }
            for (int i = 0; i < MAX_MEM; i++)
            {
                value[i] = new Block();
            }

        }


        public void allocate(int block_nr)
        {
            if (value[block_nr] == null)
            {
                value[block_nr] = new Block();
            }
        }

        public void deallocate(int block_nr)
        {
            value[block_nr] = null;
        }

        public Block this[int p_intIndex]
		{
		   get
		   {
		      return getBlockAt(p_intIndex);
		   }
		   set
		   {
		      writeAt(p_intIndex, value);
              if (p_intIndex == _debugMem)
              {
                  _debugChanged = true;
              }
		   }
	    }

        public Word this[int p_intIndex, int int_offset]
        {
            get
            {
                return this[p_intIndex].getWordAt(int_offset);
            }
            set
            {
                this[p_intIndex].writeAt(int_offset, value.clone());
                if (p_intIndex == _debugMem)
                {
                    _debugChanged = true;
                }
            }
        }

        public Word this[Register4Bytes p_Index, Register2Bytes p_offset]
        {
            get
            {
                int block = p_Index;
                int offset = p_offset;
                return this[block].getWordAt(offset);
            }
            set
            {
                int block = p_Index;
                int offset = p_offset;
                this[block].writeAt(offset, value.clone());
                if (block == _debugMem)
                {
                    _debugChanged = true;
                }
            }
        }

        public Word this[int int_Index, Register2Bytes p_offset]
        {
            get
            {
                
                int offset = p_offset;
                return this[int_Index].getWordAt(offset);
            }
            set
            {
                
                int offset = p_offset;
                this[int_Index].writeAt(offset, value.clone());
                if (int_Index == _debugMem)
                {
                    _debugChanged = true;
                }

            }
        }

        public Word this[Register4Bytes p_Index, int int_offset]
        {
            get
            {
                int block = p_Index;
                
                return this[block].getWordAt(int_offset);
            }
            set
            {
                int block = p_Index;
                
                this[block].writeAt(int_offset, value.clone());
                if (block == _debugMem)
                {
                    _debugChanged = true;
                }

            }
        }


        public Block this[Register4Bytes reg]
        {
            get
            {

                int address = reg;
                return getBlockAt(address);
            }
            set
            {
                int address = reg;
                writeAt(address, value);
                if (address == _debugMem)
                {
                    _debugChanged = true;
                }

            }
        }

        public Block getBlockAt(int p_intIndex)
        {
            if ((p_intIndex >= 0) && (p_intIndex < MAX_MEM))
            {
                if (value[p_intIndex] == null)
                {
                    return value[0];
                }
                else
                {
                    return value[p_intIndex];
                }
            }
            else
                return value[0];
        }

        public void writeAt(int p_intIndex, Block p_blockValue)
        {
            if ((p_intIndex >= 0) && (p_intIndex < MAX_MEM))
            {
                if (value[p_intIndex] == null)
                {
                    value[p_intIndex] = p_blockValue;
                }
                else
                {
                    for (int i = 0; i < 256; i++)
                        value[p_intIndex][i] = p_blockValue[i].clone();
                }
            }
            if (p_intIndex == _debugMem)
            {
                _debugChanged = true;
            }

        }
    }
}
