using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApplication1
{
    public class FileSystem : Process
    {

        File curFile;
        RAMResourceElement buffer;
        Process curSender;
        string curFName;
        string curDir;
        FileHandler tempFH;

        public FileSystem()
        {
            step.Add(stepStartChildProcesses);
            step.Add(stepGetRAMForBuffer);
            step.Add(stepReceiveBufferRAM);
            step.Add(stepBlockOnFileResource);
            step.Add(stepDividePathIntoFolders);
            step.Add(stepCheckWhatToDoNext);
            step.Add(stepCheckIfDirFound);
            step.Add(stepSendRAMToFileReadWrite);
            step.Add(stepCreateFileHandlerForFileReadWrite);
            step.Add(stepBlockOnFileHandlerResource);
            step.Add(stepBlockOnRAM);
            step.Add(stepFindNextDirectoryAddress);
            step.Add(stepCreateFileHandlerForSender);
        }

        int cycleCount = 0;
        ElementList returnArray = new ElementList();

        private int stepStartChildProcesses()
        {
            kernel.createProcess(this, null, this.priority + 1, null, "FileReadWrite");
            cycleCount++;
            return cycleCount;
        }

        private int stepGetRAMForBuffer()
        {
            ElementList query = new ElementList();
            buffer = new RAMResourceElement();
            buffer.receiver = this;
            buffer.sender = null;
            buffer.blockAddress = -1;
            buffer.useSupervisorMemory = true;
            buffer.block = null;
            query[0] = buffer;
            kernel.askForResource("RAMResource", query, returnArray);
            cycleCount++;
            return cycleCount;
        }

        private int stepReceiveBufferRAM()
        {
            buffer = (RAMResourceElement)returnArray[0];
            cycleCount++;
            return cycleCount;
        }

        private int stepBlockOnFileResource()
        {
            ElementList query = new ElementList();
            query.add(new ResourceElement());
            kernel.askForResource("File", query, returnArray);
            cycleCount++;
            wasOneTime = false;
            curStBlock = 65540 * 256;
            toCheck = 0;
            curCheck = -1;
            curStBlock = 65540 * 256;
            checkedSys = false;
            checkedDev = false;
            dirs.Clear();
            return cycleCount;
        }

        string fname = "";
        List<string> dirs = new List<string>();

        bool wasOneTime = false;
        private int stepDividePathIntoFolders()
        {
            
            if (!wasOneTime)
            {
                curFile = (File)returnArray[0];


                curDir = curFile.sender.currentDir;
                curFName = curFile.fileName;
                curSender = curFile.sender;
            }
            if ((curFName[0] != '/') && (curFName != "shell.asm"))
            {
                fname = curDir + curFName;
            }
            else
            {
                fname = curFName;
            }
            wasOneTime = true;
            int i = 0;
            if ((fname == "/dev/conRAW") || (fname == "/sys/conRAW") || (curFName == "conRAW"))
            {
                tempFH = new FileHandler();
                tempFH.mode = FileHandler.MODE_RAW;
                tempFH.currentPosition = -1;
                tempFH.bufferAddress = -1;
                tempFH.useSupervisorMemory = true;
                tempFH.count = 0;
                cycleCount += 8;
                return cycleCount;
            }
            else if ((fname == "/dev/conINT") || (fname == "/sys/conINT") || (curFName == "conINT"))
            {
                tempFH = new FileHandler();
                tempFH.mode = FileHandler.MODE_INT;
                tempFH.currentPosition = -1;
                tempFH.bufferAddress = -1;
                tempFH.useSupervisorMemory = true;
                tempFH.count = 0;
                cycleCount += 8;
                return cycleCount;
            }
            else
            {
                while (i < fname.Length - 1)
                {
                    int j = i;
                    i = fname.IndexOf("/", i + 1);
                    if (i >= 0)
                    {
                        string dirName = fname.Substring(j + 1, i - j - 1);
                        dirs.Add(dirName);
                    }
                    else
                    {
                        i = fname.Length;
                    }
                }
                dirs.Add(curFName);
                cycleCount++;
                toCheck = dirs.Count;
                curCheck = -1;
                checkedSys = false;
                checkedDev = false;

                return cycleCount;
            }
        }

        int toCheck = 0;
        int curCheck = -1;
        int curStBlock = 65540 * 256;
        bool checkedSys = false;
        bool checkedDev = false;
        private int stepCheckWhatToDoNext()
        {
            if (toCheck > 0)
            {
                toCheck--;
                curCheck++;
                dirFound = false;
                tempFH = new FileHandler();
                tempFH.currentPosition = curStBlock;
                tempFH.destinationAddress = buffer.blockAddress;
                tempFH.useSupervisorMemory = true;
                tempFH.bufferAddress = -1;                
                tempFH.count = 0;
                startOrEnd = false;
                cycleCount++;
                return cycleCount;
            }
            else
            {
                tempFH = new FileHandler();
                tempFH.currentPosition = curStBlock;
                tempFH.startPosition = curStBlock;
                tempFH.useSupervisorMemory = true;
                tempFH.bufferAddress = -1;
                tempFH.count = 0;
                cycleCount += 7;
                return cycleCount;
            }
        }

        bool dirFound = false;
        private int stepCheckIfDirFound()
        {
            if (dirFound)
            {
                cycleCount--;
                return cycleCount;
            }
            else
            {
                cycleCount++;
                return cycleCount;
            }

        }

        private int stepSendRAMToFileReadWrite()
        {
            ElementList query = new ElementList();
            query[0] = buffer;
            buffer.sender = this;
            buffer.receiver = kernel.getProcessPointer("FileReadWrite");
            kernel.freeResouce("RAMResource", query);
            cycleCount++;
            return cycleCount;
        }

        private int stepCreateFileHandlerForFileReadWrite()
        {
            ElementList query = new ElementList();
            tempFH.sender = this;
            tempFH.receiver = kernel.getProcessPointer("FileReadWrite");
            tempFH.mode = FileHandler.TYPE_READ;
            tempFH.count = 0;
            tempFH.destinationAddress = buffer.blockAddress;
            tempFH.useSupervisorMemory = buffer.useSupervisorMemory;
            query[0] = tempFH;
            kernel.createResource("FileHandler", this);
            kernel.freeResouce("FileHandler", query);
            cycleCount++;
            return cycleCount;
        }

        private int stepBlockOnFileHandlerResource()
        {
            ElementList query = new ElementList();
            ResourceElement res = new ResourceElement();
            res.sender = kernel.getProcessPointer("FileReadWrite");
            res.receiver = this;
            query[0] = res;
            kernel.askForResource("FileHandler", query, returnArray);
            cycleCount++;
            return cycleCount;
        }

        private int stepBlockOnRAM()
        {
            tempFH = (FileHandler)returnArray[0];
            ElementList query = new ElementList();
            RAMResourceElement tram = new RAMResourceElement();
            tram.sender = kernel.getProcessPointer("FileReadWrite");
            tram.receiver = this;
            tram.useSupervisorMemory = buffer.useSupervisorMemory;
            tram.blockAddress = buffer.blockAddress;
            query[0] = tram;
            kernel.askForResource("RAMResource", query, returnArray);
            cycleCount++;
            return cycleCount;
        }


        public string WordToString(Word source, int count)
        {
            string result = "";
            for (int i = 0; i < count; i++)
            {
                result += (char)source[i];
            }
            return result;
        }


        public string lastString = "";
        public bool startOrEnd = false;
        int dirToCheck = 0;


        public string removeSpaces(string source)
        {
            int i = 0;
            for (i = source.Length - 1; (i >= 0) && (source[i] == ' '); i--)
            {
            }
            return source.Substring(0, i + 1);
        }


        private int stepFindNextDirectoryAddress()
        {
            int i = 0;
            dirToCheck =  1;
            startOrEnd = true;
            while ((i < 256) && (!dirFound) && (dirToCheck > 0))
            {
                string s = "";
                if (startOrEnd)
                {
                    startOrEnd = false;
                    s = WordToString(buffer.block[i], 8);
                    if (s.Substring(0, 4) == "$DIR")
                    {
                        startOrEnd = true;
                        dirToCheck = (buffer.block[i][5] * 256 + buffer.block[i][6]) * 256 + buffer.block[i][7] + 1;
                    }
                    else
                    {
                        lastString = s;
                    }
                }
                else
                {
                    startOrEnd = true;
                    s = WordToString(buffer.block[i], 5);
                    lastString += s;
                }
                if (startOrEnd)
                {
                    if (removeSpaces(lastString) == dirs[curCheck])
                    {
                        curStBlock = ((buffer.block[i].getByteAt(5) * 256 + buffer.block[i].getByteAt(6)) * 256 + buffer.block[i].getByteAt(7)) * 256;
                        dirFound = true;
                    }
                    dirToCheck--;
                    lastString = "";
                }
                i++;
            }
            if ((!dirFound) && (dirToCheck <= 0))
            {
                if (!checkedSys)
                {
                    curDir = "/sys/";
                    cycleCount -= 7;
                    return cycleCount;
                }
                else if (!checkedDev)
                {
                    curDir = "/dev/";
                    cycleCount -= 7;
                    return cycleCount;

                }
                else
                {
                    return 1;
                }

            }
            cycleCount = cycleCount - 5;
            return cycleCount;
        }

        private int stepCreateFileHandlerForSender()
        {
            ElementList query = new ElementList();
            tempFH.sender = this;
            tempFH.receiver = curSender;
            tempFH.useSupervisorMemory = false;
            query[0] = tempFH;
            kernel.freeResouce("FileHandler", query);
            cycleCount = 3;
            return cycleCount;
        }
    }
}
