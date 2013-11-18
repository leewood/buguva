/*
FileSystem - class form managing VM virtual file system
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace FilesManager
{
    class FileSystem
    {
        public string p_strHDDPath = "";

        public FileSystem(string p_strHDDPath)
        {
            this.p_strHDDPath = p_strHDDPath;
        }

        public bool isBlockFree(int sector, int block)
        {
            FileStream hardDiskDrive = new FileStream(p_strHDDPath, FileMode.Open, FileAccess.Read);
            int curPos = sector * 65536 * 256 * 8 + block / 8;
            if (hardDiskDrive.Length > curPos)
            {
                hardDiskDrive.Position = curPos;
                int b = hardDiskDrive.ReadByte();
                hardDiskDrive.Close();
                int curB = block % 8;
                int Mask = 1 << (7 - curB);
                b = b & Mask;
                if (b > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }

        public void setBlockFull(int sector, int block)
        {
            FileStream hardDiskDrive = new FileStream(p_strHDDPath, FileMode.Open, FileAccess.Write);
            int curPos = sector * 65536 * 256 * 8 + block / 8;
            if (hardDiskDrive.Length > curPos)
            {
                hardDiskDrive.Close();
                hardDiskDrive = new FileStream(p_strHDDPath, FileMode.Open, FileAccess.ReadWrite);
                hardDiskDrive.Position = curPos;
                int b = hardDiskDrive.ReadByte();
                hardDiskDrive.Position = curPos;
                int curB = block % 8;
                int Mask = 1 << (7 - curB);
                b = b | Mask;
                hardDiskDrive.WriteByte((byte)b);
                hardDiskDrive.Close();
            }
            else
            {
                hardDiskDrive.Position = hardDiskDrive.Length - 1;
                while (hardDiskDrive.Position < curPos)
                {
                    hardDiskDrive.WriteByte(0);
                }
                int curB = block % 8;
                int b = 1 << (7 - curB);
                hardDiskDrive.WriteByte((byte)b);
                hardDiskDrive.Close();
            }
        }

        public void setBlockFree(int sector, int block)
        {
            FileStream hardDiskDrive = new FileStream(p_strHDDPath, FileMode.Open, FileAccess.Write);
            int curPos = sector * 65536 * 256 * 8 + block / 8;
            if (hardDiskDrive.Length > curPos)
            {
                hardDiskDrive.Close();
                hardDiskDrive = new FileStream(p_strHDDPath, FileMode.Open, FileAccess.ReadWrite);
                hardDiskDrive.Position = curPos;
                int b = hardDiskDrive.ReadByte();
                hardDiskDrive.Position = curPos;
                int curB = block % 8;
                int Mask = 0 - (1 << (7 - curB)) - 1;
                b = b & Mask;
                hardDiskDrive.WriteByte((byte)b);
                hardDiskDrive.Close();
            }
            else
            {
                hardDiskDrive.Position = hardDiskDrive.Length - 1;
                while (hardDiskDrive.Position <= curPos)
                {
                    hardDiskDrive.WriteByte(0);
                }
                hardDiskDrive.Close();
            }
        }


        public int findFirstFree()
		{		    
			int curSect = 1;
			int curBlock = 4;
			bool found = false;
			while (!found)
		    {
			    curBlock = 4;
			    while ((!found) && (curBlock < 65536))
				{
				    if (isBlockFree(curSect, curBlock))
					{
					   found = true;
					} 
					else 
					{
                        curBlock++;					   
				    }
				}
				if (!found)
				{
				    curSect++;
				}
		    }
			return curSect * 65536 + curBlock;
		}

		
		public int analize(string folderFileName, string fileName)
		{
		    string realName = fileName;
			while (realName.Length < 13)
			{
			    realName += " ";
			}
			if (realName.Length > 13)
			{
			    realName = realName.Substring(0, 13);
			}
			FileStream folder = new FileStream(folderFileName, FileMode.Open, FileAccess.Read);
			folder.Position = 5;
			byte b = (byte)folder.ReadByte();
			byte b2 = (byte)folder.ReadByte();
			byte b3 = (byte)folder.ReadByte();
			int count = (b * 256 + b2) * 256 + b3;
			bool found = false;
			int result = 0;
			for (int i = 0; ((i < count) && (!found)); i++)
			{
			    string name = "";
			    for (int j = 0; j < 13; j++)
				    name += (char)folder.ReadByte();
			    b = (byte)folder.ReadByte();
			    b2 = (byte)folder.ReadByte();
			    b3 = (byte)folder.ReadByte();
                result = (b * 256 + b2) * 256 + b3;
				if (name == realName)
				{
				    found = true;
				}
			}
			folder.Close();
			if (found)
			{
			    return result;
			}
			else
			{
			    return -1;
			}
		}


        public List<string> listFiles(string folderFileName)
        {
            List<string> resultFiles = new List<string>();
            copyFile(folderFileName, "tempDir");
            FileStream folder = new FileStream("tempDir", FileMode.Open, FileAccess.Read);
            folder.Position = 5;
            resultFiles.Add("..");
            byte b = (byte)folder.ReadByte();
            byte b2 = (byte)folder.ReadByte();
            byte b3 = (byte)folder.ReadByte();
            int count = (b * 256 + b2) * 256 + b3;            
            int result = 0;
            for (int i = 0; i < count; i++)
            {
                string name = "";
                for (int j = 0; j < 13; j++)
                    name += (char)folder.ReadByte();
                b = (byte)folder.ReadByte();
                b2 = (byte)folder.ReadByte();
                b3 = (byte)folder.ReadByte();
                result = (b * 256 + b2) * 256 + b3;
                resultFiles.Add(name);
            }
            folder.Close();
            return resultFiles;
        }

		public int findFile(string fileName)
		{
		    if (fileName == "/")
			{
			    return 65540;
			}
			else
			{
                if (fileName[fileName.Length - 1] == '/')
                {
                    fileName = fileName.Remove(fileName.Length - 1);
                }
			    List<string> folders = new List<string>();
				folders.Add("/");
				int i = 1;
				while (i < fileName.Length)
				{
				    int j = fileName.IndexOf("/", i);
                    if (j < 0)
                    {
                        j = fileName.Length;
                    }
					folders.Add(fileName.Substring(i, j - i));
					i = j + 1;
				}
				i = 1;
				int curBlock = 4;
				int curSect = 1;
				while (i < folders.Count)
				{
				    copyToFile(curSect, curBlock, "temp");
					int temp = analize("temp", folders[i]);
					if (temp < 0)
					{
					    return -1;
					} 
					else 
					{
					    curSect = temp / 65536;
						curBlock = temp % 65536;
						i++;
					}
				}
				return curSect * 65536 + curBlock;
			}
		}
		
		
		public void copyFile(string fileName, string destination)
		{
		    int pos = findFile(fileName);
			if (pos > -1)
			{
			    copyToFile(pos / 65536, pos % 65536, destination);
			}
		}


        public bool isDir(string fileName)
        {
            int place = findFile(fileName);
            copyToFile(place / 65536, place % 65536, "tempDirTest");
            FileStream fsCheck = new FileStream("tempDirTest", FileMode.Open, FileAccess.Read);
            fsCheck.Position = 0;
            byte b1 = (byte)fsCheck.ReadByte();
            byte b2 = (byte)fsCheck.ReadByte();
            byte b3 = (byte)fsCheck.ReadByte();
            byte b4 = (byte)fsCheck.ReadByte();
            string s = "" + (char)b1 + (char)b2 + (char)b3 + (char)b4;
            fsCheck.Close();
            if (s == "$DIR")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void copyFileExternal(string source, string destination)
        {
            int place = findFile(destination);
            if (place < 0)
            {
                place = createDirectoryEntry(destination, false);
            }
            else
            {
                freeContent(place / 65536, place % 65536, false);
            }
            copyFromFile(place / 65536, place % 65536, source);
        }


        public void fullDirRemove(string source)
        {
            int place = findFile(source);
            int copyPlace = source.LastIndexOf("/");
            string onlyName = source.Substring(copyPlace + 1);
            int end = onlyName.IndexOf(" ");
            if (end < 0)
            {
                end = onlyName.Length;
            }
            onlyName = onlyName.Substring(0, end);
            if (place >= 0)
            {
                if (isDir(source))
                {
                    List<string> list = listFiles(source);
                    foreach (string name in list)
                    {
                        int nameEnd = name.IndexOf(" ");
                        if (nameEnd < 0)
                        {
                            nameEnd = name.Length;
                        }
                        string newName = name.Substring(0, nameEnd);
                        if (name != "..")
                        {
                            fullDirRemove(fileInDir(source, newName));
                        }
                    }
                    removeFile(source);
                }
                else
                {
                    removeFile(source);
                }
            }

        }


        public void copyFileFromImagToRealHard(string source, string destination)
        {
            int place = findFile(source);
            int copyPlace = source.LastIndexOf("/");
            string onlyName = source.Substring(copyPlace + 1);
            int end = onlyName.IndexOf(" ");
            if (end < 0)
            {
                end = onlyName.Length;
            }
            onlyName = onlyName.Substring(0, end);
            if (place >= 0)
            {
                if (isDir(source))
                {
                    List<string> list = listFiles(source);
                    
                    string newDest = "";
                    if (destination.Length == 3)
                    {
                        newDest = destination + onlyName;
                    }
                    else
                    {
                        newDest = destination + "\\" + onlyName;
                    }
                    System.IO.Directory.CreateDirectory(newDest);
                    foreach (string name in list)
                    {
                        string newDestin = "";
                        int nameEnd = name.IndexOf(" ");
                        if (nameEnd < 0)
                        {
                            nameEnd = name.Length;
                        }
                        string newName = name.Substring(0, nameEnd);
                        if (newDest.Length == 3)
                        {
                            newDestin = newDest + newName;
                        }
                        else
                        {
                            newDestin = newDest + "\\" + newName;
                        }
                        if (name != "..")
                        {
                            copyFileFromImagToRealHard(fileInDir(source, newName), newDest);
                        }
                    }
                }
                else
                {
                    string newDestin = "";
                    if (destination.Length == 3)
                    {
                        newDestin = destination + onlyName;
                    }
                    else
                    {
                        newDestin = destination + "\\" + onlyName;
                    }

                    copyFile(source, newDestin);
                }
            }
        }

        public string fileInDir(string dir, string fileName)
        {
            string newDir = dir;
            while ((newDir.Length > 0) && (newDir[newDir.Length - 1] == '/'))
            {
                newDir = newDir.Remove(newDir.Length - 1);
            }
            return newDir + "/" + fileName;
        }

        public void copyFromRealHardToImage(string source, string destination)
        {            
            int place = findFile(destination);
            if (place < 0)
            {
                place = createDirectoryEntry(destination, true);
            }
            FileAttributes attr = System.IO.File.GetAttributes(source);
            if (attr == FileAttributes.Directory)

            {
                DirectoryInfo dirInfo = new DirectoryInfo(source);
                destination = fileInDir(destination, dirInfo.Name);
                if (findFile(destination) < 0)
                {
                    createDirectoryEntry(destination, true);
                }
                string[] files = System.IO.Directory.GetFiles(source);
                string[] dirs = System.IO.Directory.GetDirectories(source);
                foreach (string fullname in files)
                {
                    FileInfo info = new FileInfo(fullname);
                    string name = info.Name;
                    string newDest = fileInDir(destination, name);
                    string newSource = source + ((source.Length == 3) ? "" : "\\") + name;
                    copyFileExternal(newSource, newDest);
                }
                foreach (string fullname in dirs)
                {
                    DirectoryInfo info = new DirectoryInfo(fullname);
                    string name = info.Name;
                    string newDest = fileInDir(destination, name);
                    string newSource = source + ((source.Length == 3) ? "" : "\\") + name;
                    copyFromRealHardToImage(newSource, newDest);
                }
            }
            else
            {
                int newPlace = source.LastIndexOf("\\");
                if (newPlace < 0)
                {
                    newPlace = 0;
                }
                string onlyName = source.Substring(newPlace + 1);
                string newDest = fileInDir(destination, onlyName);
                copyFileExternal(source, newDest);
            }

        }

        

		public void freeContent(int startSector, int startBlock, bool freeStart)
		{
            int[] bytes = new int[8];
            FileStream hardDiskDrive = new FileStream(p_strHDDPath, FileMode.Open, FileAccess.ReadWrite);
            bool end = false;
            int curBlock = startBlock;
            int curSector = startSector;          
			bool toFree = freeStart;
            while (!end)
            {                
                hardDiskDrive.Position = curSector * 65536 * 256 * 8 + curBlock * 256 * 8 + 255 * 8;
                bytes[0] = (byte)hardDiskDrive.ReadByte();
                bytes[1] = (byte)hardDiskDrive.ReadByte();
                bytes[2] = (byte)hardDiskDrive.ReadByte();
                bytes[3] = (byte)hardDiskDrive.ReadByte();
                int next = (bytes[0] * 256 + bytes[1]) * 256 + bytes[2];
                if (next == 0)
                {
                    end = true;                    
                }
                hardDiskDrive.Position = curSector * 65536 * 256 * 8 + curBlock * 256 * 8 + 255 * 8;
				hardDiskDrive.WriteByte(0);
				hardDiskDrive.WriteByte(0);
				hardDiskDrive.WriteByte(0);
				hardDiskDrive.WriteByte(0);
				if (toFree)
				{
				    hardDiskDrive.Close();
					setBlockFree(curSector, curBlock);
                    hardDiskDrive = new FileStream(p_strHDDPath, FileMode.Open, FileAccess.ReadWrite);
				}
				else
				{
				    toFree = true;
				}
                curSector = bytes[0];
                curBlock = bytes[1] * 256 + bytes[2];
                				
            }
            hardDiskDrive.Close();
		}
		
		
        public void create(int startSector, int startBlock, bool isDir)
        {
            FileStream hardDiskDrive = new FileStream(p_strHDDPath, FileMode.Open, FileAccess.Write);
            if (isDir)
            {
                hardDiskDrive.Position = startSector * 65536 * 256 * 8 + startBlock * 256 * 8;
                hardDiskDrive.WriteByte((byte)'$');
                hardDiskDrive.WriteByte((byte)'D');
                hardDiskDrive.WriteByte((byte)'I');
                hardDiskDrive.WriteByte((byte)'R');
                hardDiskDrive.WriteByte(0);
                hardDiskDrive.WriteByte(0);
                hardDiskDrive.WriteByte(0);
                hardDiskDrive.WriteByte(0);
            }
            hardDiskDrive.Position = startSector * 65536 * 256 * 8 + startBlock * 256 * 8 + 255 * 8;
            hardDiskDrive.WriteByte(0);
            hardDiskDrive.WriteByte(0);
            hardDiskDrive.WriteByte(0);
            if (isDir)
            {
                hardDiskDrive.WriteByte(0);
            }
            else
            {
                hardDiskDrive.WriteByte(255);
            }
            hardDiskDrive.Close();
        }


        public int createDirectoryEntry(string fileName, bool isDir)
        {
            int last = fileName.LastIndexOf('/');
            string dir = fileName.Substring(0, last);
            string fileN = fileName.Substring(last + 1);
            int dirPlace = findFile(dir);
            if (dirPlace < 0)
            {
                dirPlace = createDirectoryEntry(dir, true);
            }
            int newPlace = findFirstFree();
            setBlockFull(newPlace / 65536, newPlace % 65536);
            create(newPlace / 65536, newPlace % 65536, isDir);
            copyToFile(dirPlace / 65536, dirPlace % 65536, "tempDir");
            FileStream directory = new FileStream("tempDir", FileMode.Open, FileAccess.ReadWrite);
            directory.Position = 5;
            byte b = (byte)directory.ReadByte();
            byte b2 = (byte)directory.ReadByte();
            byte b3 = (byte)directory.ReadByte();
            int count = (b * 256 + b2) * 256 + b3;
            count++;
            b = (byte)(count / 65536);
            b2 = (byte)((count % 65536) / 256);
            b3 = (byte)(count % 256);
            directory.Position = 5;
            directory.WriteByte(b);
            directory.WriteByte(b2);
            directory.WriteByte(b3);
            directory.Position = directory.Length;
            while (fileN.Length < 13)
            {
                fileN += " ";
            }
            if (fileN.Length > 13)
            {
                fileN = fileN.Substring(0, 13);
            }
            for (int i = 0; i < 13; i++)
            {
                directory.WriteByte((byte)fileN[i]);
            }
            directory.WriteByte((byte)(newPlace / 65536));
            directory.WriteByte((byte)((newPlace % 65536) / 256));
            directory.WriteByte((byte)(newPlace % 256));
            directory.Close();
            copyFromFile(dirPlace / 65536, dirPlace % 65536, "tempDir");
            return newPlace;
        }


        public void addDirectoryEntry(string fileName, int newPlace)
        {
            int last = fileName.LastIndexOf('/');
            string dir = fileName.Substring(0, last);
            string fileN = fileName.Substring(last + 1);
            int dirPlace = findFile(dir);
            if (dirPlace < 0)
            {
                dirPlace = createDirectoryEntry(dir, true);
            }            
            copyToFile(dirPlace / 65536, dirPlace % 65536, "tempDir");
            FileStream directory = new FileStream("tempDir", FileMode.Open, FileAccess.Write);
            directory.Position = directory.Length;
            while (fileN.Length < 13)
            {
                fileN += " ";
            }
            if (fileN.Length > 13)
            {
                fileN = fileN.Substring(0, 13);
            }
            for (int i = 0; i < 13; i++)
            {
                directory.WriteByte((byte)fileN[i]);
            }
            directory.WriteByte((byte)(newPlace / 65536));
            directory.WriteByte((byte)((newPlace % 65536) / 256));
            directory.WriteByte((byte)(newPlace % 256));
            directory.Close();
            
        }

        public void removeDirectoryEntry(string fileName)
        {
            int last = fileName.LastIndexOf('/');
            string dir = fileName.Substring(0, last);
            string fileN = fileName.Substring(last + 1);
            int dirPlace = findFile(dir);
            if (dirPlace < 0)
            {
                dirPlace = createDirectoryEntry(dir, true);
            }
            copyFromFile(dirPlace / 65536, dirPlace % 65536, "tempDir");
            FileStream directory = new FileStream("tempDir", FileMode.Open, FileAccess.ReadWrite);
            directory.Position = 5;
            while (fileN.Length < 13)
            {
                fileN += " ";
            }
            if (fileN.Length > 13)
            {
                fileN = fileN.Substring(0, 13);
            }
            byte b = (byte)directory.ReadByte();
            byte b2 = (byte)directory.ReadByte();
            byte b3 = (byte)directory.ReadByte();
            int count = (b * 256 + b2) * 256 + b3;
            int result = 0;
            int curPos = 0;
            string lastFileName = "";
            for (int i = 0; i < count; i++)
            {
                string name = "";
                for (int j = 0; j < 13; j++)
                    name += (char)directory.ReadByte();
                if (name == fileN)
                {
                    curPos = i;
                }
                if (i == count - 1)
                {
                    lastFileName = name;
                }
                lastFileName = name;
                b = (byte)directory.ReadByte();
                b2 = (byte)directory.ReadByte();
                b3 = (byte)directory.ReadByte();
                result = (b * 256 + b2) * 256 + b3;
            }
            directory.Position = curPos * 16 + 8;
            if (lastFileName.Length == 13)
            {
                for (int i = 0; i < 13; i++)
                {
                    directory.WriteByte((byte)lastFileName[i]);
                }
                directory.WriteByte((byte)b);
                directory.WriteByte((byte)b2);
                directory.WriteByte((byte)b3);
            }
            directory.Position = 5;
            if (count == 0)
            {
                count = 1;
            }
            directory.WriteByte((byte)((count - 1) / 65536));
            directory.WriteByte((byte)(((count - 1) % 65536) / 256));
            directory.WriteByte((byte)((count - 1) % 256));
            if (directory.Length >= 16)
            {
                directory.SetLength(directory.Length - 16);
            }
            directory.Position = 0;
            /*
            FileStream tempDirectory = new FileStream("tempDir2", FileMode.Open, FileAccess.ReadWrite);
            tempDirectory.Position = 0;
            for (int i = 0; i < directory.Length; i++)
            {
                tempDirectory.WriteByte((Byte)directory.ReadByte());
            }
            tempDirectory.Close();
             */
            directory.Close();
            copyFileExternal("tempDir", dir);
        }


        public void moveFile(string source, string destination)
        {
            int place = findFile(source);
            removeDirectoryEntry(source);
            addDirectoryEntry(destination, place);
        }



        public void removeFile(string fileName)
        {
            int place = findFile(fileName);
            freeContent(place / 65536, place % 65536, true);
            removeDirectoryEntry(fileName);
        }



        public void copyFromFile(int startSector, int startBlock, string fileName)
		{
            int[] bytes = new int[8];
            FileStream input = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            FileStream hardDiskDrive = new FileStream(p_strHDDPath, FileMode.Open, FileAccess.Write);
            bool end = false;
            int curBlock = startBlock;
            int curSector = startSector;
            int curByte = 0;
            while (!end)
            {
                int readCount = 255;
                hardDiskDrive.Position = curSector * 65536 * 256 * 8 + curBlock * 256 * 8;
                for (int i = 0; ((i < readCount) && (!end)); i++)
                {
                    for (int j = 0; ((j < 8) && (!end)); j++)
                    {
                        int b = input.ReadByte();
                        hardDiskDrive.WriteByte((byte)b);
						if (input.Position >= input.Length)
						{
						    end = true;
						}
                    }
					curByte = i;
                }
                if (!end)
                {
                    hardDiskDrive.Close();
                    int next = findFirstFree();
                    setBlockFull(next / 65536, next % 65536);
                    hardDiskDrive = new FileStream(p_strHDDPath, FileMode.Open, FileAccess.Write);
                    hardDiskDrive.Position = curSector * 65536 * 256 * 8 + curBlock * 256 * 8 + 255 * 8;
                    bytes[0] = (byte)(next / 65536);
                    bytes[1] = (byte)((next % 65556) / 256);
                    bytes[2] = (byte)(next % 256);
                    hardDiskDrive.WriteByte((byte)bytes[0]);
                    hardDiskDrive.WriteByte((byte)bytes[1]);
                    hardDiskDrive.WriteByte((byte)bytes[2]);
                    curSector = bytes[0];
                    curBlock = bytes[1] * 256 + bytes[2];
                }
                else
                {
                    hardDiskDrive.Position = curSector * 65536 * 256 * 8 + curBlock * 256 * 8 + 255 * 8;
                    hardDiskDrive.WriteByte(0);
                    hardDiskDrive.WriteByte(0);
                    hardDiskDrive.WriteByte(0);
                    hardDiskDrive.WriteByte((byte)(curByte));
                }
            }
            input.Close();
            hardDiskDrive.Close();
		
		}
		

        

        public void copyToFile(int startSector, int startBlock, string fileName)
        {
            int[] bytes = new int[8];
            FileStream output = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            FileStream hardDiskDrive = new FileStream(p_strHDDPath, FileMode.Open, FileAccess.Read);
            bool end = false;
            int curBlock = startBlock;
            int curSector = startSector;
            while (!end)
            {
                int readCount = 255;
                hardDiskDrive.Position = curSector * 65536 * 256 * 8 + curBlock * 256 * 8 + 255 * 8;
                bytes[0] = (byte)hardDiskDrive.ReadByte();
                bytes[1] = (byte)hardDiskDrive.ReadByte();
                bytes[2] = (byte)hardDiskDrive.ReadByte();
                bytes[3] = (byte)hardDiskDrive.ReadByte();
                int next = (bytes[0] * 256 + bytes[1]) * 256 + bytes[2];
                if (next == 0)
                {
                    end = true;
                    readCount = bytes[3] + 1;
                }
                hardDiskDrive.Position = curSector * 65536 * 256 * 8 + curBlock * 256 * 8;
                for (int i = 0; i < readCount; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        int b = hardDiskDrive.ReadByte();
                        output.WriteByte((byte)b);
                    }
                }
                curSector = bytes[0];
                curBlock = bytes[1] * 256 + bytes[2];
            }
            output.Close();
            hardDiskDrive.Close();
        }
    }
}
