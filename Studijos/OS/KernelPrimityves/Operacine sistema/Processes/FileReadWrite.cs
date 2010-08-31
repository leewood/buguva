using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApplication1
{
    public class FileReadWrite : Process
    {
        private FileHandler filePointer = null;
        private ElementList returnArray = new ElementList();
        private Process sender = null;
        private RAMResourceElement buffer;
        private RAMResourceElement destination;
        private RAMResourceElement freeBlocks;
        private int nextFreeBlock = 65536;
        private int toSave = 0;
        private ElementList tmpReturnArray = new ElementList();

        public FileReadWrite(Kernel kernel)
        {
            this.kernel = kernel;
            step.Add(stepStartChildProcesses);
            step.Add(stepBlockOnFileHandler);
            step.Add(stepBlockOnRAM);
            step.Add(stepCheckWhatToDoNext);
            step.Add(stepSendRAMToConsoleDriver);
            step.Add(stepSendRAMToHDDDriver);
            step.Add(stepCreateConsoleDataResource);
            step.Add(stepCreateHDDBlockResource);
            step.Add(stepBlockOnReadOK);
            step.Add(stepProcessData);
            step.Add(stepReturnRAMResultToSender);
            step.Add(stepReturnFileHandlerToSender);
            #region <Add "Find first free block section" (10 steps)>
            // step 12
            step.Add(stepFDSSendRAMToHDDDriver);
            step.Add(stepFDSCreateHDDBlockResource);
            step.Add(stepFDSBlockOnReadOK);
            step.Add(stepFDSReceiveRAMFromDrivers);
            step.Add(stepFDSFindFirstFreeBlock);
            step.Add(stepSFDSendRAMToHDDDriver);
            step.Add(stepSFDCreateHDDBlockResource);
            step.Add(stepSFDBlockOnReadOK);
            step.Add(stepSFDReceiveRAMFromDrivers);
            step.Add(stepSFDCurBlockCH);
            #endregion
            //step 22
            step.Add(stepReceiveRAMFromDrivers);
            #region <Add "Write-Append Start Section" (17 steps)>
            step.Add(stepStartWritingAppending);
            step.Add(stepCheckIfContinue);
            step.Add(stepROBSendRAMToHDDDriver1);
            step.Add(stepROBCreateHDDBlockResource1);
            step.Add(stepROBBlockOnReadOK1);
            step.Add(stepROBReceiveRAMFromDrivers1);
            step.Add(stepROBChFBS);
            step.Add(stepROBSendRAMToHDDDriver2);
            step.Add(stepROBCreateHDDBlockResource2);
            step.Add(stepROBBlockOnReadOK2);
            step.Add(stepROBReceiveRAMFromDrivers2);
            step.Add(stepROBReuseFreeBlocks);
            step.Add(stepROBSendRAMToHDDDriver);
            step.Add(stepROBCreateHDDBlockResource);
            step.Add(stepROBBlockOnReadOK);
            step.Add(stepROBReceiveRAMFromDrivers);
            step.Add(stepSomeTidying);
#endregion
            #region <Add "File Close Section" (10 steps)>
            //step 40
            step.Add(stepFCloseSendRAMToHDDDriver);
            step.Add(stepFCloseCreateHDDBlockResource);
            step.Add(stepFCloseBlockOnReadOK);
            step.Add(stepFCloseReceiveRAMFromDrivers);
            step.Add(stepFCloseChangeCloseBlock);
            step.Add(stepFCloseSendRAMToHDDDriver);
            step.Add(stepFCloseCreateHDDBlockResource);
            step.Add(stepFCloseBlockOnReadOK);
            step.Add(stepFCloseReceiveRAMFromDrivers);
            step.Add(stepFCloseChangeCurPos);
            #endregion
            // step 50
            step.Add(stepBlockOnRAM_2);
            step.Add(stepBlockOnRAM_3);

        }

        private int stepStartChildProcesses()
        {
            kernel.createProcess(this, null, this.priority + 1, null, "ConsoleDriver");
            kernel.createProcess(this, null, this.priority + 1, null, "HDDDriver");
            return 1;
        }

        private int stepBlockOnFileHandler()
        {
            ElementList asking = new ElementList();
            asking.parent = null;
            ResourceElement element = new FileHandler();
            element.receiver = this; //musu klase bus elemento gavejas
            element.sender = null; //mums nesvarbu, kas buvo elemento siuntejas
            element.elementsReturnedAsList = false; 
            asking[0] = element;
            kernel.askForResource("FileHandler", asking, returnArray);
            return 2;
        }


        private int stepBlockOnRAM()
        {
            filePointer = (FileHandler)(returnArray[0]);
            sender = returnArray[0].sender;
            ElementList asking = new ElementList();
            returnArray = new ElementList();
            asking.parent = new RAMResource();
            RAMResourceElement element = new RAMResourceElement();
            element.receiver = this;
            element.sender = null;
            element.elementsReturnedAsList = false;
            element.blockAddress = filePointer.destinationAddress; //norime, kad elemento bloko adresas butinai butu kaip nurodyta
            element.useSupervisorMemory = filePointer.useSupervisorMemory;
            element.block = null;
            asking[0] = element;
            kernel.askForResource("RAMResource", asking, tmpReturnArray);            
            returnArray = new ElementList();
            return 50;
            
        }


        private int stepBlockOnRAM_2()
        {
            returnArray[0] = tmpReturnArray[0];
            ElementList asking = new ElementList();
            RAMResourceElement element = new RAMResourceElement();
            element.receiver = this;
            element.sender = null;
            element.elementsReturnedAsList = false;
            element.useSupervisorMemory = true;
            element.blockAddress = filePointer.bufferAddress;
            element.block = null;
            asking[0] = element;
            kernel.askForResource("RAMResource", asking, tmpReturnArray);
            return 51;
        }


        private int stepBlockOnRAM_3()
        {
            returnArray[1] = tmpReturnArray[0];
            ElementList asking = new ElementList();
            RAMResourceElement element = new RAMResourceElement();
            element = new RAMResourceElement();
            element.receiver = this;
            element.sender = null;
            element.block = null;
            element.useSupervisorMemory = true;
            element.blockAddress = -1; //freeBlocksSection
            asking[2] = element;
            kernel.askForResource("RAMResource", asking, tmpReturnArray);
            return 3;
        }

        private int stepSendRAMToConsoleDriver()
        {
            ElementList elem = new ElementList(null);
            buffer.sender = this;
            buffer.receiver = kernel.getProcessPointer("ConsoleDriver");
            
            elem[0] = buffer;
            kernel.freeResouce("RAMResource", elem);
            return 6;
        }

        private int stepSendRAMToHDDDriver()
        {
            ElementList elem = new ElementList(null);
            if (buffer == null)
            {
                buffer = new RAMResourceElement();
            }
            buffer.receiver = kernel.getProcessPointer("HDDDriver");
            buffer.sender = this;
            elem[0] = buffer;
            kernel.freeResouce("RAMResource", elem);
            return 7;
        }


        private int stepCheckWhatToDoNext()
        {
            destination = (RAMResourceElement)(returnArray[0]);
            buffer = (RAMResourceElement)(returnArray[1]);
            //freeBlocks = (RAMResourceElement)(returnArray[2]);
            freeBlocks = (RAMResourceElement)(tmpReturnArray[0]);
            if (filePointer.type == FileHandler.TYPE_READ)
            {
                if (filePointer.currentPosition < 0)
                {
                    return 4;
                }
                else if (filePointer.currentPosition % 256 != 0)
                {
                    return 9;
                }
                else
                {
                    return 5;
                }
            }
            else if (filePointer.type == FileHandler.TYPE_CONTINUE_WRITING)
            {
                if (filePointer.currentPosition < 0)
                {
                    return 4;
                }
                else if (filePointer.currentPosition % 256 == 255)
                {
                    return 12;
                }
                else
                {
                    return 9;
                }
            }
            else if (filePointer.type != FileHandler.TYPE_CLOSE)
            {
                curSector = 1;
                curBlock = 0;
                return ADDR_WRITE_APPEND_BLOCK;
            }
            else
            {
                return ADDR_CLOSE_BLOCK;
            }
        }


        private int stepCreateConsoleDataResource()
        {
            kernel.createResource("ConsoleData", this);
            ElementList query = new ElementList();
            ConsoleDataElement console = new ConsoleDataElement();
            console.memoryAddress = buffer.blockAddress;
            console.useSupervisorMemory = buffer.useSupervisorMemory;
            console.sender = this;
            console.receiver = kernel.getProcessPointer("ConsoleDriver");
            console.mode = (filePointer.type == FileHandler.TYPE_READ) ? ConsoleDataElement.MODE_READ : ConsoleDataElement.MODE_WRITE;
            console.type = (filePointer.mode == FileHandler.MODE_INT) ? ConsoleDataElement.TYPE_INT : ConsoleDataElement.TYPE_RAW;
            query.add(console);
            kernel.freeResouce("ConsoleData", query);
            return 8;
        }

        private int stepCreateHDDBlockResource()
        {
            kernel.createResource("HDDBlock", this);

            HDDBlockElement hddBlock = new HDDBlockElement();
            
            hddBlock.memoryAddress = buffer.blockAddress;
            hddBlock.useSupervisorMemory = buffer.useSupervisorMemory;
            hddBlock.hddBlockAddress = filePointer.currentPosition / 256;
            hddBlock.mode = (filePointer.type == FileHandler.TYPE_READ) ? HDDBlockElement.MODE_READ : HDDBlockElement.MODE_WRITE;
            hddBlock.sender = this;
            hddBlock.receiver = null;
            ElementList query = new ElementList();
            query.add(hddBlock);
            kernel.freeResouce("HDDBlock", query);
            return 8;
        }

        public bool getBitAt(byte data, int place)
        {
            byte k = 1;
            k = (byte)(k << ((byte)7 - (byte)place));
            if ((data & k) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public byte setBitAt(byte data, bool bit, int place)
        {
            byte k = 1;
            k = (byte)(k << ((byte)7 - (byte)place));
            byte j = (byte)(-k - 1);
            if (bit)
            {
                return (byte)(data | k);
            }
            else
            {
                return (byte)(data & j);
            }
        }


        #region <Find First Free Block (10 steps)>
        int curSector = 1;
        int curBlock = 0;
        const int ADDR_FIND_FREE_BLOCK = 12;
        const int ADDR_USE_HDD_DRIVER = 5;

        private int stepFDSSendRAMToHDDDriver()
        {
            ElementList elem = new ElementList(null);
            cycleCount = ADDR_FIND_FREE_BLOCK;
            freeBlocks.receiver = kernel.getProcessPointer("HDDDriver");
            freeBlocks.sender = this;
            elem[0] = freeBlocks;
            kernel.freeResouce("RAMResource", elem);
            cycleCount++;
            return cycleCount;
        }

        private int stepFDSCreateHDDBlockResource()
        {
            kernel.createResource("HDDBlock", this);

            HDDBlockElement hddBlock = new HDDBlockElement();
            hddBlock.memoryAddress = freeBlocks.blockAddress;
            hddBlock.useSupervisorMemory = freeBlocks.useSupervisorMemory;
            hddBlock.hddBlockAddress = curSector * 65536 + curBlock;
            hddBlock.mode = HDDBlockElement.MODE_READ;
            hddBlock.sender = this;
            hddBlock.receiver = null;
            ElementList query = new ElementList();
            query.add(hddBlock);
            kernel.freeResouce("HDDBlock", query);

            cycleCount++;
            return cycleCount;
        }

        private int stepFDSBlockOnReadOK()
        {

            ElementList asking = new ElementList();
            ElementList returnArray = new ElementList();
            asking.parent = new readOK();
            readOKElement element = new readOKElement();
            element.receiver = this;
            element.sender = null;
            element.onChannel = (filePointer.currentPosition >= 0) ? readStatus.CHANNEL_HARDDISK : ((filePointer.type == FileHandler.TYPE_READ) ? readStatus.CHANNEL_INPUT_DEVICE : readStatus.CHANNEL_OUTPUT_DEVICE);
            element.elementsReturnedAsList = false;
            asking[0] = element;
            kernel.askForResource("readOK", asking, returnArray);
            cycleCount++;
            return cycleCount;

        }

        private int stepFDSReceiveRAMFromDrivers()
        {
            ElementList asking = new ElementList();
            returnArray = new ElementList();
            asking.parent = new RAMResource();
            RAMResourceElement element = new RAMResourceElement();
            element.receiver = this;
            element.sender = null;
            element.elementsReturnedAsList = false;
            element.blockAddress = freeBlocks.blockAddress; //norime, kad elemento bloko adresas butinai butu kaip nurodyta
            element.useSupervisorMemory = freeBlocks.useSupervisorMemory;
            element.block = null;
            asking[0] = element;
            kernel.askForResource("RAMResource", asking, returnArray);
            cycleCount++;
            return cycleCount;

        }

        private int stepFDSFindFirstFreeBlock()
        {
            freeBlocks = (RAMResourceElement)(returnArray[0]);
            for (int i = 0; i < 256; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    for (int k = 0; k < 8; k++)
                    {
                        byte b = freeBlocks.block[i].getByteAt(j);
                        if (getBitAt(b, k))
                        {
                            setBitAt(b, true, k);
                            freeBlocks.block[i].writeAt(j, b);
                            nextFreeBlock = curSector * 65536 + ((curBlock * 4 + i) * 256 + j) * 8 + k;
                            cycleCount++;
                            return cycleCount;
                        }
                    }
                }
            }
            curBlock++;
            if (curBlock > 3)
            {
                curBlock = 0;
                curSector++;
            }
            cycleCount = ADDR_FIND_FREE_BLOCK;
            return cycleCount;
        }

        private int stepSFDSendRAMToHDDDriver()
        {
            ElementList elem = new ElementList(null);
            freeBlocks.receiver = kernel.getProcessPointer("HDDDriver");
            freeBlocks.sender = this;
            elem[0] = freeBlocks;
            kernel.freeResouce("RAMResource", elem);
            cycleCount++;
            return cycleCount;
        }

        private int stepSFDCreateHDDBlockResource()
        {
            kernel.createResource("HDDBlock", this);

            HDDBlockElement hddBlock = new HDDBlockElement();
            hddBlock.memoryAddress = freeBlocks.blockAddress;
            hddBlock.useSupervisorMemory = freeBlocks.useSupervisorMemory;
            hddBlock.hddBlockAddress = curSector * 65536 + curBlock;
            hddBlock.mode = HDDBlockElement.MODE_WRITE;
            hddBlock.sender = this;
            hddBlock.receiver = null;
            ElementList query = new ElementList();
            query.add(hddBlock);
            kernel.freeResouce("HDDBlock", query);

            cycleCount++;
            return cycleCount;
        }

        private int stepSFDBlockOnReadOK()
        {

            ElementList asking = new ElementList();
            ElementList returnArray = new ElementList();
            asking.parent = new readOK();
            readOKElement element = new readOKElement();
            element.receiver = this;
            element.sender = null;
            element.elementsReturnedAsList = false;
            asking[0] = element;
            element.onChannel = (filePointer.currentPosition >= 0) ? readStatus.CHANNEL_HARDDISK : ((filePointer.type == FileHandler.TYPE_READ) ? readStatus.CHANNEL_INPUT_DEVICE : readStatus.CHANNEL_OUTPUT_DEVICE);
            kernel.askForResource("readOK", asking, returnArray);
            cycleCount++;
            return cycleCount;

        }

        private int stepSFDReceiveRAMFromDrivers()
        {
            ElementList asking = new ElementList();
            returnArray = new ElementList();
            asking.parent = new RAMResource();
            RAMResourceElement element = new RAMResourceElement();
            element.receiver = this;
            element.sender = null;
            element.elementsReturnedAsList = false;
            element.blockAddress = freeBlocks.blockAddress; //norime, kad elemento bloko adresas butinai butu kaip nurodyta
            element.useSupervisorMemory = freeBlocks.useSupervisorMemory;
            element.block = null;
            asking[0] = element;
            kernel.askForResource("RAMResource", asking, returnArray);
            cycleCount++;
            return cycleCount;
        }


        private int stepSFDCurBlockCH()
        {
            freeBlocks = (RAMResourceElement)(returnArray[0]);
            buffer.block[255].writeAt(0, (byte)(nextFreeBlock / 65536));
            buffer.block[255].writeAt(1, (byte)((nextFreeBlock % 65536) / 256));
            buffer.block[255].writeAt(2, (byte)(nextFreeBlock % 256));
            return ADDR_USE_HDD_DRIVER;
        }
        
#endregion

        private int stepBlockOnReadOK()
        {            
            ElementList asking = new ElementList();
            ElementList returnArray = new ElementList();
            asking.parent = new readOK();
            readOKElement element = new readOKElement();
            element.receiver = this;
            element.sender = null;
            element.onChannel = (filePointer.currentPosition >= 0) ? readStatus.CHANNEL_HARDDISK : ((filePointer.type == FileHandler.TYPE_READ) ? readStatus.CHANNEL_INPUT_DEVICE : readStatus.CHANNEL_OUTPUT_DEVICE);
            element.elementsReturnedAsList = false;
            asking[0] = element;
            kernel.askForResource("readOK", asking, returnArray);
            return 22;

        }

        private int stepReceiveRAMFromDrivers()
        {
            ElementList asking = new ElementList();
            returnArray = new ElementList();
            asking.parent = new RAMResource();
            RAMResourceElement element = new RAMResourceElement();
            element.receiver = this;
            element.sender = null;
            element.elementsReturnedAsList = false;
            element.blockAddress = buffer.blockAddress; //norime, kad elemento bloko adresas butinai butu kaip nurodyta
            element.useSupervisorMemory = buffer.useSupervisorMemory;
            element.block = null;
            asking[0] = element;
            kernel.askForResource("RAMResource", asking, returnArray);
            return 9;

        }

        private int stepProcessData()
        {
            buffer = (RAMResourceElement)(returnArray[0]);
            if (filePointer.type == FileHandler.TYPE_READ)
            {
                int count = 0;
                if (filePointer.currentPosition < 0)
                {
                    count = filePointer.count;
                    if (count == 0)
                    {
                        count = 256;
                    }
                    for (int i = 0; i < count; i++)
                    {
                        destination.block[i] = buffer.block[i].clone();
                    }
                    return 10;
                }
                else
                {
                    int mainCount = filePointer.count;
                    if (mainCount == 0)
                    {
                        mainCount = 256;
                    }
                    int nextBlock = (int)((buffer.block[255].getByteAt(0) * 256 + buffer.block[255].getByteAt(1)) * 256 + buffer.block[255].getByteAt(2));
                    if (nextBlock == 0)
                    {
                        int end = (int)(buffer.block[255].getByteAt(3));
                        count = end - (filePointer.currentPosition % 256);
                    }
                    else
                    {
                        count = (255 - (filePointer.currentPosition % 256));
                    }
                    int start = filePointer.currentPosition % 256;
                    if (count > mainCount)
                    {
                        count = mainCount;
                    }
                    for (int i = 0; i < mainCount; i++)
                    {
                        destination.block[i] = buffer.block[start + i].clone();
                    }
                    if (count < filePointer.count)
                    {
                        filePointer.count = mainCount - count;
                        filePointer.currentPosition = nextBlock * 256;
                        return 3;
                    }
                    else
                    {
                        filePointer.count = 0;
                        filePointer.currentPosition = filePointer.currentPosition + count;
                        if (filePointer.currentPosition % 256 == 255)
                        {
                            filePointer.currentPosition = nextBlock * 256;
                            return 10;
                        }
                        else
                        {
                            return 10;
                        }
                        
                    }
                }
            }
            else
            {
                if (filePointer.currentPosition < 0)
                {
                    int count = filePointer.count;
                    if (count == 0)
                    {
                        count = 256;
                    }
                    for (int i = 0; i < count; i++)
                    {
                        buffer.block[i] = destination.block[i].clone();
                    }
                    return 10;
                }
                else
                {
                    int mainCount = filePointer.count;
                    if (mainCount == 0)
                    {
                        mainCount = 256;
                    }
                    int count = 255 - (filePointer.currentPosition % 256);
                    int start = filePointer.currentPosition % 256;
                    for (int i = 0; i < count; i++)
                    {
                        buffer.block[start + i] = destination.block[i].clone();
                    }
                    filePointer.currentPosition += count;
                    filePointer.count = filePointer.count - count;
                    if (filePointer.count < 0)
                    {
                        filePointer.count = 0;
                    }
                    if (filePointer.currentPosition % 256 == 255)
                    {
                        if (filePointer.count > 0)
                        {
                            return 3;
                        }
                        else
                        {
                            return 10;
                        }
                    }
                    else
                    {
                        return 10;
                    }
                }

            }
        }

        private int stepReturnRAMResultToSender()
        {
            ElementList elem = new ElementList(null);
            destination.receiver = filePointer.sender;
            destination.sender = this;
            elem.add(destination);
            kernel.freeResouce("RAMResource", elem);
            return 11;
        }

        private int stepReturnFileHandlerToSender()
        {
            ElementList elem = new ElementList(null);
            filePointer.receiver = filePointer.sender;
            filePointer.sender = this;
            elem[0] = filePointer;
            kernel.freeResouce("FileHandler", elem);
            return 1;
        }


        int cycleCount = 23;

        #region <Start Write/Append (17 steps)>

        const int ADDR_WRITE_APPEND_BLOCK = 23;
        const int ADDR_MAIN_CHECK = 3;

        int curCheckBlock = 0;
        int startBlock = 0;

        private int stepStartWritingAppending()
        {
            startBlock = buffer.blockAddress;
            curCheckBlock = filePointer.startPosition / 256;
            cycleCount = ADDR_WRITE_APPEND_BLOCK;
            cycleCount += 12;
            return cycleCount;
        }

        private int stepCheckIfContinue()
        {
            curCheckBlock = (buffer.block[255].getByteAt(0) * 256 + buffer.block[255].getByteAt(1)) * 256 + buffer.block[255].getByteAt(2);
            if (curCheckBlock != 0)
            {
                if (filePointer.type == FileHandler.TYPE_WRITE)
                {
                    cycleCount++;
                    return cycleCount;
                }
                else
                {
                    cycleCount += 11;
                    return cycleCount;
                }
            }
            else
            {
                if ((filePointer.type == FileHandler.TYPE_WRITE) && (curCheckBlock != startBlock))
                {
                    curCheckBlock = startBlock;
                    cycleCount += 11;
                    return cycleCount;
                }
                else
                {
                    filePointer.type = FileHandler.TYPE_CONTINUE_WRITING;
                    int addr = buffer.blockAddress * 256 + buffer.block[255].getByteAt(3);
                    filePointer.currentPosition = addr + 1;
                    return ADDR_MAIN_CHECK;
                }
            }
        }

        #region <Update Free Blocks Section (10 steps)>

        private int stepROBSendRAMToHDDDriver1()
        {
            ElementList elem = new ElementList(null);
            buffer.receiver = kernel.getProcessPointer("HDDDriver");
            buffer.sender = this;
            elem[0] = freeBlocks;
            kernel.freeResouce("RAMResource", elem);
            cycleCount++;
            return cycleCount;
        }

        private int stepROBCreateHDDBlockResource1()
        {
            kernel.createResource("HDDBlock", this);

            HDDBlockElement hddBlock = new HDDBlockElement();
            hddBlock.memoryAddress = freeBlocks.blockAddress;
            hddBlock.useSupervisorMemory = freeBlocks.useSupervisorMemory;
            hddBlock.mode = HDDBlockElement.MODE_READ;
            //int sk = (buffer.block[255].getByteAt(0) * 256 + buffer.block[255].getByteAt(1)) * 256 + buffer.block[255].getByteAt(2);
            int sector = buffer.blockAddress / 65536;
            int fbaddress = (buffer.blockAddress % sector) / (65536 / 4);
            hddBlock.hddBlockAddress = sector * 65536 + fbaddress;
            //hddBlock.hddBlockAddress = buffer.blockAddress / (65536) 
            
            hddBlock.sender = this;
            hddBlock.receiver = null;
            ElementList query = new ElementList();
            query.add(hddBlock);
            kernel.freeResouce("HDDBlock", query);

            cycleCount++;
            return cycleCount;
        }

        private int stepROBBlockOnReadOK1()
        {

            ElementList asking = new ElementList();
            ElementList returnArray = new ElementList();
            asking.parent = new readOK();
            readOKElement element = new readOKElement();
            element.receiver = this;
            element.sender = null;
            element.elementsReturnedAsList = false;
            element.onChannel = (filePointer.currentPosition >= 0) ? readStatus.CHANNEL_HARDDISK : ((filePointer.type == FileHandler.TYPE_READ) ? readStatus.CHANNEL_INPUT_DEVICE : readStatus.CHANNEL_OUTPUT_DEVICE);
            asking[0] = element;
            kernel.askForResource("readOK", asking, returnArray);
            cycleCount++;
            return cycleCount;

        }

        private int stepROBReceiveRAMFromDrivers1()
        {
            ElementList asking = new ElementList();
            returnArray = new ElementList();
            asking.parent = new RAMResource();
            RAMResourceElement element = new RAMResourceElement();
            element.receiver = this;
            element.sender = null;
            element.elementsReturnedAsList = false;
            element.blockAddress = freeBlocks.blockAddress; //norime, kad elemento bloko adresas butinai butu kaip nurodyta
            element.useSupervisorMemory = freeBlocks.useSupervisorMemory;
            element.block = null;
            asking[0] = element;
            kernel.askForResource("RAMResource", asking, returnArray);
            cycleCount++;
            return cycleCount;

        }

        private int stepROBChFBS()
        {
            freeBlocks = (RAMResourceElement)returnArray[0];
            int sector = buffer.blockAddress / 65536;
            int fbaddress = buffer.blockAddress % sector;
            int wrdAddr = fbaddress / 64;
            int byteAddr = (fbaddress % 64) / 8;
            byte curByte = freeBlocks.block[wrdAddr].getByteAt(byteAddr);
            curByte = setBitAt(curByte, false, fbaddress % 8);
            freeBlocks.block[wrdAddr].writeAt(byteAddr, curByte);
            cycleCount++;
            return cycleCount;
        }
        private int stepROBSendRAMToHDDDriver2()
        {
            ElementList elem = new ElementList(null);
            buffer.receiver = kernel.getProcessPointer("HDDDriver");
            buffer.sender = this;
            elem[0] = freeBlocks;
            kernel.freeResouce("RAMResource", elem);
            cycleCount++;
            return cycleCount;
        }

        private int stepROBCreateHDDBlockResource2()
        {
            kernel.createResource("HDDBlock", this);

            HDDBlockElement hddBlock = new HDDBlockElement();
            hddBlock.memoryAddress = freeBlocks.blockAddress;
            hddBlock.useSupervisorMemory = freeBlocks.useSupervisorMemory;
            hddBlock.mode = HDDBlockElement.MODE_WRITE;
            //int sk = (buffer.block[255].getByteAt(0) * 256 + buffer.block[255].getByteAt(1)) * 256 + buffer.block[255].getByteAt(2);
            int sector = buffer.blockAddress / 65536;
            int fbaddress = (buffer.blockAddress % sector) / (65536 / 4);
            hddBlock.hddBlockAddress = sector * 65536 + fbaddress;
            //hddBlock.hddBlockAddress = buffer.blockAddress / (65536) 

            hddBlock.sender = this;
            hddBlock.receiver = null;
            ElementList query = new ElementList();
            query.add(hddBlock);
            kernel.freeResouce("HDDBlock", query);

            cycleCount++;
            return cycleCount;
        }

        private int stepROBBlockOnReadOK2()
        {

            ElementList asking = new ElementList();
            ElementList returnArray = new ElementList();
            asking.parent = new readOK();
            readOKElement element = new readOKElement();
            element.receiver = this;
            element.sender = null;
            element.elementsReturnedAsList = false;
            asking[0] = element;
            element.onChannel = (filePointer.currentPosition >= 0) ? readStatus.CHANNEL_HARDDISK : ((filePointer.type == FileHandler.TYPE_READ) ? readStatus.CHANNEL_INPUT_DEVICE : readStatus.CHANNEL_OUTPUT_DEVICE);
            kernel.askForResource("readOK", asking, returnArray);
            cycleCount++;
            return cycleCount;

        }

        private int stepROBReceiveRAMFromDrivers2()
        {
            ElementList asking = new ElementList();
            returnArray = new ElementList();
            asking.parent = new RAMResource();
            RAMResourceElement element = new RAMResourceElement();
            element.receiver = this;
            element.sender = null;
            element.elementsReturnedAsList = false;
            element.blockAddress = freeBlocks.blockAddress; //norime, kad elemento bloko adresas butinai butu kaip nurodyta
            element.useSupervisorMemory = freeBlocks.useSupervisorMemory;
            element.block = null;
            asking[0] = element;
            kernel.askForResource("RAMResource", asking, returnArray);
            cycleCount++;
            return cycleCount;

        }

        private int stepROBReuseFreeBlocks()
        {
            freeBlocks = (RAMResourceElement)returnArray[0];
            cycleCount++;
            return cycleCount;
        }
        #endregion

        private int stepROBSendRAMToHDDDriver()
        {
            ElementList elem = new ElementList(null);
            buffer.receiver = kernel.getProcessPointer("HDDDriver");
            buffer.sender = this;
            elem[0] = buffer;
            kernel.freeResouce("RAMResource", elem);
            cycleCount++;
            return cycleCount;
        }

        private int stepROBCreateHDDBlockResource()
        {
            kernel.createResource("HDDBlock", this);

            HDDBlockElement hddBlock = new HDDBlockElement();
            hddBlock.memoryAddress = buffer.blockAddress;
            hddBlock.useSupervisorMemory = buffer.useSupervisorMemory;
            hddBlock.mode = HDDBlockElement.MODE_READ;
            hddBlock.hddBlockAddress = curCheckBlock;
            
            hddBlock.sender = this;
            hddBlock.receiver = null;
            ElementList query = new ElementList();
            query.add(hddBlock);
            kernel.freeResouce("HDDBlock", query);

            cycleCount++;
            return cycleCount;
        }

        private int stepROBBlockOnReadOK()
        {

            ElementList asking = new ElementList();
            ElementList returnArray = new ElementList();
            asking.parent = new readOK();
            readOKElement element = new readOKElement();
            element.receiver = this;
            element.sender = null;
            element.elementsReturnedAsList = false;
            element.onChannel = (filePointer.currentPosition >= 0) ? readStatus.CHANNEL_HARDDISK : ((filePointer.type == FileHandler.TYPE_READ) ? readStatus.CHANNEL_INPUT_DEVICE : readStatus.CHANNEL_OUTPUT_DEVICE);
            asking[0] = element;
            kernel.askForResource("readOK", asking, returnArray);
            cycleCount++;
            return cycleCount;

        }

        private int stepROBReceiveRAMFromDrivers()
        {
            ElementList asking = new ElementList();
            returnArray = new ElementList();
            asking.parent = new RAMResource();
            RAMResourceElement element = new RAMResourceElement();
            element.receiver = this;
            element.sender = null;
            element.elementsReturnedAsList = false;
            element.blockAddress = buffer.blockAddress; //norime, kad elemento bloko adresas butinai butu kaip nurodyta
            element.useSupervisorMemory = buffer.useSupervisorMemory;
            element.block = null;
            asking[0] = element;
            kernel.askForResource("RAMResource", asking, returnArray);
            cycleCount++;
            return cycleCount;

        }

        private int stepSomeTidying()
        {
            buffer = (RAMResourceElement)returnArray[0];
            return cycleCount - 15; 
        }

        #endregion

        #region <File close block (10 steps)>
        const int ADDR_CLOSE_BLOCK = 40;
        const int ADDR_RETURN_PLACE = 1;
        bool wasReadedAgain = false;

        private int stepFCloseSendRAMToHDDDriver()
        {
            if (filePointer.currentPosition % 256 == 0)
            {
                ElementList elem = new ElementList(null);
                buffer.receiver = kernel.getProcessPointer("HDDDriver");
                buffer.sender = this;
                elem[0] = buffer;
                kernel.freeResouce("RAMResource", elem);
                wasReadedAgain = true;
                cycleCount = ADDR_CLOSE_BLOCK;
                cycleCount++;
                return cycleCount;
            }
            else
            {
                wasReadedAgain = false;
                cycleCount = ADDR_CLOSE_BLOCK;
                cycleCount += 4;
                return cycleCount;
            }
        }

        private int stepFCloseCreateHDDBlockResource()
        {
            kernel.createResource("HDDBlock", this);

            HDDBlockElement hddBlock = new HDDBlockElement();
            hddBlock.memoryAddress = buffer.blockAddress;
            hddBlock.useSupervisorMemory = buffer.useSupervisorMemory;
            hddBlock.mode = HDDBlockElement.MODE_READ;
            hddBlock.hddBlockAddress = filePointer.currentPosition / 256;
            hddBlock.sender = this;
            hddBlock.receiver = null;
            ElementList query = new ElementList();
            query.add(hddBlock);
            kernel.freeResouce("HDDBlock", query);

            cycleCount++;
            return cycleCount;
        }

        private int stepFCloseBlockOnReadOK()
        {

            ElementList asking = new ElementList();
            ElementList returnArray = new ElementList();
            asking.parent = new readOK();
            readOKElement element = new readOKElement();
            element.receiver = this;
            element.sender = null;
            element.elementsReturnedAsList = false;
            element.onChannel = (filePointer.currentPosition >= 0) ? readStatus.CHANNEL_HARDDISK : ((filePointer.type == FileHandler.TYPE_READ) ? readStatus.CHANNEL_INPUT_DEVICE : readStatus.CHANNEL_OUTPUT_DEVICE);
            asking[0] = element;
            kernel.askForResource("readOK", asking, returnArray);
            cycleCount++;
            return cycleCount;

        }

        private int stepFCloseReceiveRAMFromDrivers()
        {
            ElementList asking = new ElementList();
            returnArray = new ElementList();
            asking.parent = new RAMResource();
            RAMResourceElement element = new RAMResourceElement();
            element.receiver = this;
            element.sender = null;
            element.elementsReturnedAsList = false;
            element.blockAddress = buffer.blockAddress; //norime, kad elemento bloko adresas butinai butu kaip nurodyta
            element.useSupervisorMemory = buffer.useSupervisorMemory;
            element.block = null;
            asking[0] = element;
            kernel.askForResource("RAMResource", asking, returnArray);
            cycleCount++;
            return cycleCount;

        }

        private int stepFCloseChangeCloseBlock()
        {
            buffer = (RAMResourceElement)returnArray[0];
            buffer.block[255].writeAt(0, 0);
            buffer.block[255].writeAt(1, 0);
            buffer.block[255].writeAt(2, 0);
            buffer.block[255].writeAt(3, (byte)(filePointer.currentPosition % 256));
            cycleCount++;
            return cycleCount;
        }

        private int stepFCloseSendRAMToHDDDriver1()
        {
            ElementList elem = new ElementList(null);
            buffer.receiver = kernel.getProcessPointer("HDDDriver");
            buffer.sender = this;
            elem[0] = buffer;
            kernel.freeResouce("RAMResource", elem);
            cycleCount++;
            return cycleCount;
        }

        private int stepFCloseCreateHDDBlockResource1()
        {
            kernel.createResource("HDDBlock", this);

            HDDBlockElement hddBlock = new HDDBlockElement();
            hddBlock.memoryAddress = buffer.blockAddress;
            hddBlock.useSupervisorMemory = buffer.useSupervisorMemory;
            hddBlock.mode = HDDBlockElement.MODE_WRITE;
            hddBlock.hddBlockAddress = filePointer.currentPosition / 256;
            
            hddBlock.sender = this;
            hddBlock.receiver = null;
            ElementList query = new ElementList();
            query.add(hddBlock);
            kernel.freeResouce("HDDBlock", query);
            cycleCount++;
            return cycleCount;
        }

        private int stepFCloseBlockOnReadOK1()
        {

            ElementList asking = new ElementList();
            ElementList returnArray = new ElementList();
            asking.parent = new readOK();
            readOKElement element = new readOKElement();
            element.receiver = this;
            element.sender = null;
            element.elementsReturnedAsList = false;
            element.onChannel = (filePointer.currentPosition >= 0) ? readStatus.CHANNEL_HARDDISK : ((filePointer.type == FileHandler.TYPE_READ) ? readStatus.CHANNEL_INPUT_DEVICE : readStatus.CHANNEL_OUTPUT_DEVICE);
            asking[0] = element;
            kernel.askForResource("readOK", asking, returnArray);
            cycleCount++;
            return cycleCount;

        }

        private int stepFCloseReceiveRAMFromDrivers1()
        {
            ElementList asking = new ElementList();
            returnArray = new ElementList();
            asking.parent = new RAMResource();
            RAMResourceElement element = new RAMResourceElement();
            element.receiver = this;
            element.sender = null;
            element.elementsReturnedAsList = false;
            element.blockAddress = buffer.blockAddress; //norime, kad elemento bloko adresas butinai butu kaip nurodyta
            element.useSupervisorMemory = buffer.useSupervisorMemory;
            element.block = null;
            asking[0] = element;
            kernel.askForResource("RAMResource", asking, returnArray);
            cycleCount++;
            return cycleCount;

        }

        private int stepFCloseChangeCurPos()
        {
            buffer = (RAMResourceElement)returnArray[0];
            filePointer.currentPosition = filePointer.startPosition;
            cycleCount++;
            return ADDR_RETURN_PLACE;
        }

        #endregion 
    }
}
