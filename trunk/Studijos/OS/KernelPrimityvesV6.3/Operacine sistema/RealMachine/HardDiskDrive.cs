using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    public class HardDiskDrive
    {
        private string path = "";
        public Boolean eob = false;

        public HardDiskDrive(string p_strPath)
        {
            path = p_strPath;
        }

        public Block readBlock(int p_intSector, int p_intBlock)
        {
            FileStream hardDiskDrive = new FileStream(path, FileMode.Open, FileAccess.Read);
            Block blockMain = new Block();
            Word wordMain = new Word();

            long intReadFrom = p_intSector * 256 * 256 * 256 * 8 + (p_intBlock) * 256 * 8;
            eob = false;
            //if ((p_intSector >= 0) && (p_intSector <= 15) && (p_intBlock >= 0) && (p_intBlock <= 256 * 256 - 1))
            if ((p_intSector >= 0) && (p_intSector <= 15) && (p_intBlock >= 0) && (p_intBlock <= 256 * 256))
            {
                if (hardDiskDrive.Length > intReadFrom)
                {
                    hardDiskDrive.Position = intReadFrom;// - (256 * 8);
                    for (int i = 0; i <= 255; i++)
                    {
                        wordMain = new Word();
                        for (int j = 0; j <= 7; j++)
                            wordMain.writeAt(j, (byte)hardDiskDrive.ReadByte());
                        blockMain.writeAt(i, wordMain);
                    }
                } else {
                    eob = true;
                }
            }

            hardDiskDrive.Close();
            return blockMain;
        }



        public void writeBlock(int p_intSector, int p_intBlock, Block p_blockMain)
        {
            FileStream hardDiskDrive = new FileStream(path, FileMode.Open, FileAccess.Write);
            long intAppendWithSpaces = p_intSector * 256 * 256 * 256 * 8 + p_intBlock * 256 * 8;

            if ((p_intSector >= 0) && (p_intSector <= 15) && (p_intBlock >= 0) && (p_intBlock <= 256 * 256 - 1))
            {
                if (hardDiskDrive.Length < intAppendWithSpaces)
                {
                    intAppendWithSpaces = intAppendWithSpaces - hardDiskDrive.Length;
                    hardDiskDrive.Position = hardDiskDrive.Length;

                    for (int i = 0; i < intAppendWithSpaces; i++)
                        hardDiskDrive.WriteByte(0);

                    for (int i = 0; i <= 255; i++)
                        for (int j = 0; j <= 7; j++)
                            hardDiskDrive.WriteByte(p_blockMain.getWordAt(i).getByteAt(j));
                }
                else
                {
                    hardDiskDrive.Position = intAppendWithSpaces;
                    for (int i = 0; i <= 255; i++)
                        for (int j = 0; j <= 7; j++)
                            hardDiskDrive.WriteByte(p_blockMain.getWordAt(i).getByteAt(j));
                }
            }

            hardDiskDrive.Close();
        }
    }
}
