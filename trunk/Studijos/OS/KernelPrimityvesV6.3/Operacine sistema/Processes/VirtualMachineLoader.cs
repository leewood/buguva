using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApplication1
{
    public class VirtualMachineLoader : Process
    {
        ElementList returnedElementList = new ElementList();
        VirtualMachineResource virtualMachineResource = new VirtualMachineResource();
        RAMResourceElement ramBuffer = new RAMResourceElement();
        RAMResourceElement ramPTR = new RAMResourceElement();
        ElementList ramResultList = new ElementList();
        FileHandler fileHandler = new FileHandler();
        bool bufferTaken = true;
        bool programStart = false;

        int currentVirtualBlock = 0;
        int currentVirtualWord = 0;
        int neededBlock = -1;
        int currentReadPosition = 0;
        int ptrCount = 0;
        List<int> awailableBlocks = null;

        public VirtualMachineLoader(Kernel p_kernelMain)
        {
            kernel = p_kernelMain;

            step.Add(step0BlockOnVirtualMachine);
            step.Add(step1BlockOnRamForBufferAndPTR);
            step.Add(step2CreateFileResource);
            step.Add(step3BlockOnFileHandler1);
            step.Add(step4SendRAMToFileReadWrite);
            step.Add(step5SendFileHandlerToFileReadWrite);
            step.Add(step6BlockOnFileHandler2);
            step.Add(step7BlockForBufferRAM);
            step.Add(step8MoveDataFromBufferToResult);
            step.Add(step9SendRamToVirtualMachineCreator);
            step.Add(step10CreateVirtualMachineReadyResource);
            step.Add(step11BlocForResultRAM);
            step.Add(setp12WriteRamToPTR);
        }

        private int step0BlockOnVirtualMachine()
        {
            ElementList elementAskForVirtualMachine = new ElementList();
            ResourceElement resourceVirtualMachine = new VirtualMachineResource();

            elementAskForVirtualMachine.parent = null;
            resourceVirtualMachine.receiver = this;
            resourceVirtualMachine.sender = null;
            resourceVirtualMachine.elementsReturnedAsList = false;
            elementAskForVirtualMachine[0] = resourceVirtualMachine;

            kernel.askForResource("VirtualMachine", elementAskForVirtualMachine, returnedElementList);
            return 1;
        }

        private int step1BlockOnRamForBufferAndPTR()
        {
            RAMResourceElement ramResource = new RAMResourceElement();
            ElementList askedElementList = new ElementList();

            virtualMachineResource = (VirtualMachineResource)returnedElementList[0];

            ramResource.sender = null;
            ramResource.receiver = this;
            ramResource.elementsReturnedAsList = false;
            ramResource.blockAddress = -1;
            ramResource.useSupervisorMemory = true;
            askedElementList.parent = null;
            askedElementList[0] = ramResource;

            ramResource = new RAMResourceElement();
            ramResource.sender = null;
            ramResource.receiver = this;
            ramResource.blockAddress = -1;
            ramResource.useSupervisorMemory = true;
            askedElementList[1] = ramResource;

            bufferTaken = true;
            programStart = false;

            currentVirtualBlock = 0;
            currentVirtualWord = 0;
            neededBlock = -1;
            currentReadPosition = 0;
            ptrCount = 0;
            awailableBlocks = null;
            ramResultList = new ElementList();

            kernel.askForResource("RAMResource", askedElementList, returnedElementList);

            return 2;
        }

        private int step2CreateFileResource()
        {
            File fileResource = new File();
            ElementList elementList = new ElementList();

            ramBuffer = (RAMResourceElement)returnedElementList[0];
            ramPTR = (RAMResourceElement)returnedElementList[1];
            for (int i = 0; i < 256; i++)
            {
                Word wrd = new Word();
                wrd = 0;
                wrd[0] = (byte)'1';
                wrd[4] = (byte)'1';
                ramPTR.block[i] = wrd.clone();
            }
            kernel.createResource("File", this);
            fileResource.setFileName(virtualMachineResource.getProgramName()); // prideti .asm nereik, tai padaro pats getProgramName
            fileResource.sender = this;
            fileResource.receiver = null;
            elementList[0] = fileResource;
            kernel.freeResouce("File", elementList);

            return 3;
        }

        private int step3BlockOnFileHandler1()
        {
            ElementList elementAskForFileHandler = new ElementList();
            ResourceElement resourceFileHandler = new FileHandler();

            elementAskForFileHandler.parent = null;
            resourceFileHandler.receiver = this;
            resourceFileHandler.sender = null;
            resourceFileHandler.elementsReturnedAsList = false;
            elementAskForFileHandler[0] = resourceFileHandler;

            kernel.askForResource("FileHandler", elementAskForFileHandler, returnedElementList);
            return 4;
        }

        private int step4SendRAMToFileReadWrite()
        {
            ElementList sendElementListToFileReadWrite = new ElementList();

            fileHandler = (FileHandler)returnedElementList[0];

            ramBuffer.sender = this;
            ramBuffer.receiver = kernel.getProcessPointer("FileReadWrite");
            sendElementListToFileReadWrite[0] = ramBuffer;

            kernel.freeResouce("RAMResource", sendElementListToFileReadWrite);
            return 5;
        }

        private int step5SendFileHandlerToFileReadWrite()
        {
            ElementList elementList = new ElementList(null);

            fileHandler.sender = this;
            fileHandler.receiver = kernel.getProcessPointer("FileReadWrite");
            fileHandler.type = FileHandler.TYPE_READ;
            fileHandler.destinationAddress = ramBuffer.blockAddress;
            fileHandler.useSupervisorMemory = ramBuffer.useSupervisorMemory;
            elementList[0] = fileHandler;
            kernel.freeResouce("FileHandler", elementList);

            return 6; 
        }

        private int step6BlockOnFileHandler2()
        {
            step3BlockOnFileHandler1();
            return 7;
        }

        private int step7BlockForBufferRAM()
        {
            RAMResourceElement ramResourceFromFileReadWrite = new RAMResourceElement();
            ElementList elementListForRamResource = new ElementList();

            fileHandler = (FileHandler)returnedElementList[0];

            ramResourceFromFileReadWrite.sender = kernel.getProcessPointer("FileReadWrite");
            ramResourceFromFileReadWrite.receiver = this;
            ramResourceFromFileReadWrite.elementsReturnedAsList = false;
            ramResourceFromFileReadWrite.blockAddress = ramBuffer.blockAddress;
            ramResourceFromFileReadWrite.useSupervisorMemory = ramBuffer.useSupervisorMemory;
            elementListForRamResource.parent = null;
            elementListForRamResource[0] = ramResourceFromFileReadWrite;

            kernel.askForResource("RAMResource", elementListForRamResource, returnedElementList);
            bufferTaken = true;
            currentReadPosition = 0;
            awailableBlocks = null;
            programStart = false;
            return 8;
        }

        private int step8MoveDataFromBufferToResult()
        {
            RAMResourceElement ramResult = new RAMResourceElement();

            if (bufferTaken)
            {
                ramBuffer = (RAMResourceElement)returnedElementList[0];
                bufferTaken = false;
            }

            if (awailableBlocks != null)
            {
                if (awailableBlocks.Contains(0))
                    ramResult = (RAMResourceElement)returnedElementList[0];
                ramResultList.add(ramResult);
            }
            else
            {
                awailableBlocks = new List<int>();
            }
            ramResultList = processData(ramResultList, ramResult, ramBuffer);

           // ramResultList = processData(ramBuffer, ramResult);

            if (neededBlock < -1)
            {
                neededBlock = -1;
                if (blockEnds(ramResult))
                {
                    return 4;
                }
                else
                {
                    return 9;
                }
            }
            else
            {
                return 11;
            }
        }

        private int step9SendRamToVirtualMachineCreator()
        {
            ramResultList.parent = null;
            ramPTR.receiver = virtualMachineResource.sender;
            ramPTR.sender = this;

            ramResultList.add(ramPTR);
            kernel.freeResouce("RAMResource", ramResultList);
            return 10;
        }

        private int step10CreateVirtualMachineReadyResource()
        {
            ElementList elementList = new ElementList();
            
            kernel.createResource("VirtualMachineReady", this);
            VirtualMachineReady virtualMachineReadyResource = (VirtualMachineReady)kernel.getResourcePointer("VirtualMachineReady");

            virtualMachineReadyResource.returnElementsCount = ramResultList.Count; 
            virtualMachineReadyResource.setMachineAddress(ramPTR.blockAddress);
            ResourceElement resourceElement = new ResourceElement();
            resourceElement.receiver = null;
            resourceElement.sender = this;
            elementList.parent = null;
            elementList[0] = resourceElement;
            kernel.freeResouce("VirtualMachineReady", elementList);
            return 0;
        }

        private int step11BlocForResultRAM()
        {
            RAMResourceElement ramForResults = new RAMResourceElement();
            ElementList elementListForRamResults = new ElementList();

            ramForResults.sender = null;
            ramForResults.receiver = this;
            ramForResults.elementsReturnedAsList = false;
            ramForResults.blockAddress = -1;

            elementListForRamResults.parent = null;
            elementListForRamResults[0] = ramForResults;
            kernel.askForResource("RAMResource", elementListForRamResults, returnedElementList);

            return 12;
        }

        private int setp12WriteRamToPTR()
        {
            ramPTR.block[ptrCount] = 0;
            ramPTR.block[ptrCount][1, true] = ((RAMResourceElement)returnedElementList[0]).blockAddress;
            ptrCount++;
            return 8;
        }

        private bool blockEnds(RAMResourceElement p_ramResourceElementMain)
        {
            bool booBlockEnds = false;

            for (int i = 0; (i < 256) && (!booBlockEnds); i++)
                booBlockEnds = p_ramResourceElementMain.block[i].toString().ToUpper().Equals("$END");

            return booBlockEnds;
        }

        private ElementList processData(ElementList p_elementListCurrentBuffer,
            RAMResourceElement p_ramResourceToProcess,
            RAMResourceElement p_ramResourceBuffer)
        {
            bool booReachedBlockEnd = false;

            p_ramResourceToProcess.receiver = virtualMachineResource.sender;
            p_ramResourceToProcess.sender = this;

            for (int i = currentReadPosition; (i < 256) && (!booReachedBlockEnd); i++)
            {
                if (p_ramResourceBuffer.block[i].toString()[0].Equals('$'))
                {
                    neededBlock = setCurrentPositionVirtualMachineRam(p_ramResourceBuffer.block[i].toString());
                    booReachedBlockEnd = true;
                }
                else
                {
                    ((RAMResourceElement)p_elementListCurrentBuffer[currentVirtualBlock]).block[currentVirtualWord] =
                        p_ramResourceBuffer.block[i].clone();

                    p_elementListCurrentBuffer[currentVirtualBlock].receiver = virtualMachineResource.sender;
                    currentVirtualWord++;
                }
                currentReadPosition = i + 1;
                
                if (programStart)
                {
                    currentReadPosition++;
                    currentReadPosition++;
                    programStart = false;
                }

                if (currentReadPosition == 256)
                {
                    currentReadPosition = 0;
                    currentVirtualWord = 0;
                    currentVirtualBlock++;
                }
            }

            return p_elementListCurrentBuffer;
        }

        private int setCurrentPositionVirtualMachineRam(String p_strMainCommand)
        {
            if (p_strMainCommand[0].Equals('$'))
            {
                switch (p_strMainCommand.Substring(1, 2))
                {
                    case "EN":
                        {
                            neededBlock = -2;
                            break;
                        }
                    case "AS":
                        {
                            programStart = true;
                            awailableBlocks.Add(0);
                            neededBlock = 0;
                            currentVirtualBlock = 0;
                            currentVirtualWord = 0;
                            break;
                        }
                    default:
                        {
                            neededBlock = getBlockFromString(p_strMainCommand);
                            break;
                        }
                }
            }
            return neededBlock;
        }

        private int getBlockFromString(String p_strCommand)
        {
            int i;

            i = getByteFromChar(p_strCommand[1]) * 16 + getByteFromChar(p_strCommand[2]);
            currentVirtualBlock = i;
            currentVirtualWord = 0;

            if (awailableBlocks.Contains(i))
            {
                return -1;
            }
            else
            {
                awailableBlocks.Add(i);
                return i;
            }
        }

        private int getByteFromChar(char p_chrMain)
        {
            switch (p_chrMain)
            {
                case '0': return 0;
                case '1': return 1;
                case '2': return 2;
                case '3': return 3;
                case '4': return 4;
                case '5': return 5;
                case '6': return 6;
                case '7': return 7;
                case '8': return 8;
                case '9': return 9;
                case 'A': return 10;
                case 'B': return 11;
                case 'C': return 12;
                case 'D': return 13;
                case 'E': return 14;
                case 'F': return 15;
                default: return 0;
            }
        }

    }

}
