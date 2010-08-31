using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ConsoleApplication1
{
    public class HardDiskDriveUtils
    {

        FileDescriptor lastFile;

        public void createHardDiskDrive(string p_strPath)
        {
            FileStream fileHardDiskDrive = new FileStream(p_strPath, FileMode.Create);
            fileHardDiskDrive.Close();
        }

        static public void writeFileDescriptor(FileStream p_hardDiskDrive, string p_strFilename, byte p_intSector, int p_intAddress)
        {
            string nameToWrite;
            if (p_strFilename.Length > 13)
            {
                nameToWrite = p_strFilename.Substring(0, 13);
            }
            else
            {
                nameToWrite = p_strFilename;
                for (int i = 0; nameToWrite.Length < 13; i++)
                {
                    nameToWrite += ' ';
                }
            }
            for (int i = 0; i < 13; i++)
            {
                p_hardDiskDrive.WriteByte((byte)(nameToWrite[i]));
            }
            p_hardDiskDrive.WriteByte(p_intSector);
            p_hardDiskDrive.WriteByte((byte)(p_intAddress / 256));
            p_hardDiskDrive.WriteByte((byte)(p_intAddress % 256));
        }

        public struct FileInfo
        {
            public string fileName;
            public int block;
            public byte sector;
        }

        static public void writeString(FileStream p_hardDisk, string p_strData)
        {
            string toWrite;
            if (p_strData.Length > 8)
            {
                toWrite = p_strData.Substring(0, 8);
            }
            else
            {
                toWrite = p_strData;
            }
            for (int i = 0; toWrite.Length < 8; i++)
            {
                toWrite += ' ';
            }
            for (int i = 0; i < 8; i++)
            {
                p_hardDisk.WriteByte((byte)toWrite[i]);
            }
        }

        static public void writeDirectoryDescriptor(FileStream p_hardDisk, FileInfo[] p_files)
        {
            p_hardDisk.WriteByte((byte)'$');
            p_hardDisk.WriteByte((byte)'D');
            p_hardDisk.WriteByte((byte)'I');
            p_hardDisk.WriteByte((byte)'R');
            p_hardDisk.WriteByte((byte)0);
            p_hardDisk.WriteByte((byte)(p_files.Length / 65536));
            p_hardDisk.WriteByte((byte)((p_files.Length % 65536) / 256));
            p_hardDisk.WriteByte((byte)(p_files.Length % 256));
            for (int i = 0; i < p_files.Length; i++)
            {
                writeFileDescriptor(p_hardDisk, p_files[i].fileName, p_files[i].sector, p_files[i].block);
            }
            for (int i = (p_files.Length + 1) * 2 - 1; i < 255; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    p_hardDisk.WriteByte(0);
                }
            }
            p_hardDisk.WriteByte(0);
            p_hardDisk.WriteByte(0);
            p_hardDisk.WriteByte(0);
            p_hardDisk.WriteByte((byte)(p_files.Length * 2));
            p_hardDisk.WriteByte(0);
            p_hardDisk.WriteByte(0);
            p_hardDisk.WriteByte(0);
            p_hardDisk.WriteByte(0);
        }

        static public void installOS(string p_strHDDPath)
        {
            FileStream hardDiskDrive = new FileStream(p_strHDDPath, FileMode.OpenOrCreate, FileAccess.Write);
            FileInfo[] files;
            for (int i = 0; i < 134217728; i++)
            {
                hardDiskDrive.WriteByte(0);
            }
            hardDiskDrive.WriteByte(255);
            hardDiskDrive.WriteByte(128);
            for (int i = 2; i < 8192; i++)
            {
                hardDiskDrive.WriteByte(0);
            }
            files = new FileInfo[4];
            files[0].fileName = "dev";
            files[0].sector = 1;
            files[0].block = 5;
            files[1].fileName = "sys";
            files[1].sector = 1;
            files[1].block = 6;
            files[2].fileName = "usr";
            files[2].sector = 1;
            files[2].block = 7;
            files[3].fileName = "shell.asm";
            files[3].sector = 1;
            files[3].block = 8;
            writeDirectoryDescriptor(hardDiskDrive, files);
            files = new FileInfo[0];
            writeDirectoryDescriptor(hardDiskDrive, files);
            writeDirectoryDescriptor(hardDiskDrive, files);
            writeDirectoryDescriptor(hardDiskDrive, files);
            writeString(hardDiskDrive, "$ASMshel");
            writeString(hardDiskDrive, "l       ");
            writeString(hardDiskDrive, "        ");
            writeString(hardDiskDrive, "$00     ");
            writeString(hardDiskDrive, "IN  0301");
            writeString(hardDiskDrive, "PUSH0001");
            writeString(hardDiskDrive, "PUSH0002");
            writeString(hardDiskDrive, "ADD     ");
            writeString(hardDiskDrive, "POP 0101");
            writeString(hardDiskDrive, "OUT 0102");
            writeString(hardDiskDrive, "HALT    ");
            writeString(hardDiskDrive, "$01     ");
            writeString(hardDiskDrive, "Test   \n");
            writeString(hardDiskDrive, "$END    ");
            for (int i = 7 * 8; i < 255 * 8; i++)
            {
                hardDiskDrive.WriteByte(0);
            }
            hardDiskDrive.WriteByte(0);
            hardDiskDrive.WriteByte(0);
            hardDiskDrive.WriteByte(0);
            hardDiskDrive.WriteByte(13);
            hardDiskDrive.WriteByte(0);
            hardDiskDrive.WriteByte(0);
            hardDiskDrive.WriteByte(0);
            hardDiskDrive.WriteByte(0);
            hardDiskDrive.Close();
        }

        public FileDescriptor openFile(string fileName, HardDiskDrive hdd)
        {
            const int FILE_NAME_SIZE = 24;
            FileDescriptor fd = new FileDescriptor();
            bool newBlock = true;
            byte curByte = 0;
            bool found = false;
            bool tosum = false;
            int read = 0;
            string tFname = "";
            Block tmp = new Block();
            while ((!fd.eod) && (!found))
            {
                if (newBlock)
                {
                    tmp = hdd.readBlock(fd.sector, fd.block);
                    if (hdd.eob)
                    {
                        fd.eod = hdd.eob;
                    }
                }
                curByte = tmp.getWordAt(fd.word).getByteAt(fd.bytenr);

                if (tosum)
                {
                    read++;
                    tFname += (char)(curByte);
                    if (tFname == "$END")
                    {
                        lastFile = fd;
                    }
                }
                if (curByte == (byte)'$')
                {
                    tFname = "";
                    read = 0;
                    tosum = true;
                    tFname += (char)(curByte);
                    read++;
                }
                if (read >= FILE_NAME_SIZE)
                {
                    if ((tFname.IndexOf(fileName) >= 0) && (tFname.IndexOf("$ASM") >= 0))
                    {
                        found = true;
                    }
                    else
                    {
                        read = 0;
                        tFname = "";
                        tosum = false;
                    }
                }
                newBlock = fd.next();
            }
            fd.temp = tmp;
            return fd;
        }

        public byte readByte(HardDiskDrive hdd, FileDescriptor fd)
        {
            if (fd.eob)
            {
                hdd.readBlock(fd.sector, fd.block);
            }
            fd.next();
            return fd.temp.getWordAt(fd.word).getByteAt(fd.bytenr);
        }


        public void writeByte(HardDiskDrive hdd, FileDescriptor fd, byte b)
        {
            if (fd.eob)
            {
                hdd.writeBlock(fd.oldsector, fd.oldblock, fd.temp);
                
            }
            fd.next();
            fd.temp[fd.word].writeAt(fd.bytenr, b);
        }


        public FileDescriptor createFile(string fileName, HardDiskDrive hdd)
        {
            FileDescriptor fd = openFile(fileName, hdd);
            if (fd.eod)
            {
                fd = lastFile;
                fd.next();
                writeByte(hdd, fd, (byte)'$');
                writeByte(hdd, fd, (byte)'A');
                writeByte(hdd, fd, (byte)'S');
                writeByte(hdd, fd, (byte)'M');
                for (int i = 0; i < 20; i++)
                {
                    if (i + 1 > fileName.Length)
                    {
                        writeByte(hdd, fd, (byte)' ');
                    }
                    else
                    {
                        writeByte(hdd, fd, (byte)fileName[i]);
                    }
                }
            }
            return fd;
        }

        public void closeFile(HardDiskDrive hdd, FileDescriptor fd)
        {
            writeByte(hdd, fd, (byte)'$');
            writeByte(hdd, fd, (byte)'E');
            writeByte(hdd, fd, (byte)'N');
            writeByte(hdd, fd, (byte)'D');
            hdd.writeBlock(fd.sector, fd.block, fd.temp);
        }

        public void loadToMem(string fileName, HardDiskDrive hdd, Memory mem, Memory supervisor_mem, int PagesTable)
        {
            FileDescriptor fd = openFile(fileName, hdd);
            int curPage = supervisor_mem[PagesTable, 0] / 16 / 256;
            int curVPage = 0;
            int curIndex = 0;
            int curWord = 0;
            bool eof = false;
            byte curByte;
            while (!eof)
            {
                curByte = readByte(hdd, fd);
                if (curByte == (byte)'$')
                {
                    curByte = readByte(hdd, fd);
                    if (curByte == (byte)'E')
                    {
                        eof = true;
                    }
                    else
                    {
                        curVPage = (curByte - (byte)'0') * 16;
                        curByte = readByte(hdd, fd);
                        curVPage += curByte - (byte)'0';
                        curPage = supervisor_mem[PagesTable, curVPage] / 16 / 256;
                        curIndex = 0;
                        curWord = 0;
                    }
                }
                else
                {
                    Block blk = mem[curPage];
                    blk[curWord].writeAt(curIndex, curByte);
                    mem[curPage] = blk;
                    curIndex++;
                    if (curIndex > 7)
                    {
                        curIndex = 0;
                        curWord++;
                    }
                    if (curWord > 255)
                    {
                        curWord = 0;
                        curVPage++;
                        curPage = supervisor_mem[PagesTable, curVPage] / 16 / 256;
                    }
                }
            }
        }
    }
}
