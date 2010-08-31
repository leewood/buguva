using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    public class JobControl: Process
    {
        private ElementList returnedElementList = new ElementList();
        private Word wordFromStack = new Word();

        private int virtualMachineID = 0;

        private static int virtualMachineNextID = 0;

        public JobControl()
        {
            step.Add(step0StartVirtualMachineRunner);
            step.Add(step1BlockForInterruptResource);
            step.Add(step2GetInterruptType);
            
            //>>Halt
            step.Add(step3SiHaltCreateProcEvent);
            step.Add(step4SiHaltCreateProcevent);
            //<<Halt
            
            //>>SiInOut
            step.Add(step5SiInOutCreateProcEvent);
            step.Add(step6SiInOutCreateConsoleData);
            step.Add(step7SiInOutSendRAMToDriver);
            step.Add(step8SiInOutGetReadOk);
            step.Add(step9SiInOutReceiveRAMFromDriver);
            step.Add(step10SiInOutCreateProcEvent);
            //<<SiInOut

            step.Add(step11SiCallCreateProcEvent);
            //?????step.Add(step0StartVirtualMachineRunner);

            //>>SiCall
            step.Add(step12SiCallGetDataFromStack);
            step.Add(step13SiCallGetCall);
            step.Add(step14WriteNtoVM);
            step.Add(step15SiCallCreate);
            step.Add(step16SiCallOpenFile);
            step.Add(step17SiCallOpenFileWaitForFileHandler);
            step.Add(step18SiCallOpenFileWriteDataToStack);
            step.Add(step19SiCallGetParam);
            step.Add(step20SiCallReadWriteAskForBufferRAM);
            step.Add(step21SiCallReadWriteFreeFileHandler2);
            step.Add(step22SiCallReadWriteFreeRAMResource);
            step.Add(step23SiCallBlockForFileHandler);
            step.Add(step24SiCallReadWriteWaitForRAMResource);
            step.Add(step25SiCallSaveResultToStack);
            step.Add(step26SiCallShutDown);
            //<<Si_Call

            //>>Si_Swap
            step.Add(step27SiSwapCreateSwapBlock);
            step.Add(step28SiSwapBlockForSwapOK);
            step.Add(step29SiSwapGetRamFromSwap);
            step.Add(step30SiSwapGiveRamToVirtualMachineRunner);
            //<<Si_Swap
        }

        private int step0StartVirtualMachineRunner()
        {
            
            kernel.createProcess(this, 
                savedRegisters, 
                5, 
                this.ownedResources, 
                "VirtualMachine" + virtualMachineNextID.ToString());
            virtualMachineID = virtualMachineNextID;
            virtualMachineNextID++;
            return 1;
        }

        private int step1BlockForInterruptResource()
        {
            ElementList elementList = new ElementList();
            Interrupted interupted = new Interrupted();

            interupted.receiver = this;
            interupted.sender = null;
            interupted.elementsReturnedAsList = false;
            elementList[0] = interupted;
            elementList.parent = null;
            kernel.askForResource("Interrupted", elementList, returnedElementList);
            return 2;
        }


        private Interrupted interrupted = new Interrupted();

        private int step2GetInterruptType()
        {
            interrupted = (Interrupted)returnedElementList[0];

            switch (interrupted.type)
            {
                case Interrupted.SI_HALT: return 3;
                
                case Interrupted.SI_IN:
                case Interrupted.SI_OUT:
                        return 5;
                
                case Interrupted.SI_CALL:
                        return 11;
                case Interrupted.TI: 
                    return 10;
                case Interrupted.SI_SWAP:
                    return 27;
                default: return 1;
            }
        }

        private int step3SiHaltCreateProcEvent()
        {
            createProcEvent(ProcEvent.EVENT_STOP, "VirtualMachine" + virtualMachineID.ToString(), this, null);
            return 4;
        }

        private int step4SiHaltCreateProcevent()
        {
            createProcEvent(ProcEvent.EVENT_STOP, this.name, this, null);
            return 1;
        }

        private int step5SiInOutCreateProcEvent()
        {
            createProcEvent(ProcEvent.EVENT_SUSPEND, 
                "VirtualMachine" + virtualMachineID.ToString(), 
                this, 
                null);
            return 6;
        }

        int memToUse = 0;

        private int step6SiInOutCreateConsoleData()
        {
            kernel.createResource("ConsoleData", this);
            ElementList query = new ElementList();
            ConsoleDataElement elem = new ConsoleDataElement();
            elem.sender = this;
            elem.type = ConsoleDataElement.TYPE_RAW;
            elem.mode = (interrupted.type == Interrupted.SI_IN) ? ConsoleDataElement.MODE_READ : ConsoleDataElement.MODE_WRITE;
            Word fullAddress = kernel.processor.virtualToAbsoluteAddress(interrupted.additionalArgument);
            memToUse = fullAddress[2, true];
            elem.memoryAddress = memToUse;
            query[0] = elem;
            kernel.freeResouce("ConsoleData", query);
            return 7;
        }

        private int step7SiInOutSendRAMToDriver()
        {
            ElementList query = new ElementList();
            RAMResourceElement elem = new RAMResourceElement();
            elem.blockAddress = memToUse;
            elem.block = kernel.processor.user_memory[memToUse];
            elem.receiver = kernel.getProcessPointer("ConsoleDriver");
            elem.sender = this;
            elem.useSupervisorMemory = false;
            query[0] = elem;
            kernel.freeResouce("RAMResource", query);
            return 8;
        }

        private int step8SiInOutGetReadOk()
        {
            ElementList elementList = new ElementList();
            readOKElement rsElem = new readOKElement();
            rsElem.receiver = this;
            rsElem.sender = kernel.getProcessPointer("ConsoleDriver");
            rsElem.onChannel = (interrupted.type == Interrupted.SI_IN) ? readStatus.CHANNEL_INPUT_DEVICE : readStatus.CHANNEL_OUTPUT_DEVICE;
            elementList[0] = rsElem;
            kernel.askForResource("readOK", elementList, returnedElementList);
            return 9;
        }

        private int step9SiInOutReceiveRAMFromDriver()
        {
            ElementList query = new ElementList();
            ElementList returnPlace = new ElementList();
            RAMResourceElement elem = new RAMResourceElement();
            elem.blockAddress = memToUse;
            elem.receiver = this;
            elem.sender = kernel.getProcessPointer("ConsoleData");
            elem.useSupervisorMemory = false;
            query[0] = elem;
            kernel.askForResource("RAMResource", query, returnPlace);            
            return 10;
        }

        private int step10SiInOutCreateProcEvent()
        {
            createProcEvent(ProcEvent.EVENT_RESUME, "VirtualMachine" + virtualMachineID.ToString(), this, null);
            return 1;
        }

        private int step11SiCallCreateProcEvent()
        {
            createProcEvent(ProcEvent.EVENT_SUSPEND, "VirtualMachine" + virtualMachineID.ToString(), this, null);
            return 12;
        }

        private int step12SiCallGetDataFromStack()
        {
            wordFromStack = getDataFromStack();
            return 13;
        }

        private int step13SiCallGetCall()
        {
            int nr = wordFromStack;
            switch(nr)
            {
                case 1: return 14; //TimerData
                case 0: return 15; // Create
                case 2: return 16; //OpenFile
                case 3: return 19; //GetParam
                
                case 4:  //Read
                case 5: //Write
                    return 20;

                case 6: return 23; //Shutdown
                default: return 1;
            }
        }

        private int step14WriteNtoVM()
        {
            putDataToStack(kernel.processor.N);
            return 1;
        }

        private int step15SiCallCreate()
        {
            string name = getDataFromStack().toString();
            int end = name.IndexOf(' ');
            if (end < 0)
            {
                end = name.Length;
            }
            name = name.Substring(0, end);
            if (name != "")
            {
                createProcEvent(ProcEvent.EVENT_NEW, name, this, null);
            }
            return 10;
        }

        private int step16SiCallOpenFile()
        {
            File file = new File();
            ElementList elementList = new ElementList();

            file.elementsReturnedAsList = false;
            file.sender = this;
            file.receiver = null;
            file.fileName = getDataFromStack().ToString();
            elementList.parent = null;
            elementList[0] = file;
            kernel.freeResouce("File", elementList);
            return 17;
        }


        private int step17SiCallOpenFileWaitForFileHandler()
        {
            ElementList elementAskForFileHandler = new ElementList();
            FileHandler resourceFileHandler = new FileHandler();

            elementAskForFileHandler.parent = null;
            resourceFileHandler.receiver = this;
            resourceFileHandler.sender = null;
            resourceFileHandler.elementsReturnedAsList = false;
            elementAskForFileHandler[0] = resourceFileHandler;

            kernel.askForResource("FileHandler", elementAskForFileHandler, returnedElementList);

            return 18;
        }

        private int step18SiCallOpenFileWriteDataToStack()
        {
            fileHandlersList.Add((FileHandler)returnedElementList[0]);

            putDataToStack(fileHandlersList.Count() - 1);
            
            return 1;
        }

        private int step19SiCallGetParam()
        {
            //O jie kartais jau nera paciame steke kaip duom2?
            return 1;
        }

        private ElementList returnArrayCallReadWrite;

        private int step20SiCallReadWriteAskForBufferRAM()
        {
            ElementList query = new ElementList();
            RAMResourceElement ramRes = new RAMResourceElement();
            ramRes.useSupervisorMemory = false;
            ramRes.sender = this;
            ramRes.receiver = kernel.getProcessPointer("FileReadWrite");
            ramRes.blockAddress = -1;
            query[0] = ramRes;
            returnArrayCallReadWrite = new ElementList();
            kernel.askForResource("RAMResource", query, returnArrayCallReadWrite);
            return 21;
        }

        private int step21SiCallReadWriteFreeFileHandler2()
        {
            FileHandler fileHandler = new FileHandler();
            ElementList elementList = new ElementList();
            int fileHandlerNr = getDataFromStack();
            fileHandler = fileHandlersList[fileHandlerNr];
            fileHandler.sender = this;
            fileHandler.count = 1;
            fileHandler.receiver = kernel.getProcessPointer("FileReadWrite");
            fileHandler.type = (wordFromStack == "Read") ? FileHandler.TYPE_READ : FileHandler.TYPE_WRITE;            
            elementList.parent = null;
            elementList.add(fileHandler);
            kernel.freeResouce("FileHandler", elementList);
            return 22;
        }


        private int step22SiCallReadWriteFreeRAMResource()
        {
            ElementList query = new ElementList();
            RAMResourceElement ramRes = (RAMResourceElement)returnArrayCallReadWrite[0];
            ramRes.useSupervisorMemory = false;
            ramRes.sender = this;
            ramRes.receiver = kernel.getProcessPointer("FileReadWrite");
            query[0] = ramRes;
            kernel.freeResouce("RAMResource", query);
            return 23;
        }

        private int step23SiCallBlockForFileHandler()
        {
            step17SiCallOpenFileWaitForFileHandler();
            return 24;
        }

        private int step24SiCallReadWriteWaitForRAMResource()
        {
            ElementList query = new ElementList();
            ElementList returnArray = new ElementList();
            RAMResourceElement ramRes = new RAMResourceElement();
            ramRes.useSupervisorMemory = false;
            ramRes.sender = kernel.getProcessPointer("FileReadWrite");
            ramRes.receiver = this;
            ramRes.blockAddress = -1;
            query[0] = ramRes;
            kernel.askForResource("RAMResource", query, returnArray);
            return 25;
        }

        private int step25SiCallSaveResultToStack()
        {
            putDataToStack(((RAMResourceElement)returnArrayCallReadWrite[0]).block[0]);
            ElementList freeElements = new ElementList();
            ResourceElement elem = returnArrayCallReadWrite[0];
            elem.receiver = null;
            elem.sender = this;
            freeElements[0] = elem;
            kernel.freeResouce("RAMResource", freeElements);
            return 1;
        }

        private int step26SiCallShutDown()
        {
            kernel.createResource("ShutDown", this);
            return 1;
        }

        private int step27SiSwapCreateSwapBlock()
        {
            SWAPBlockElement swapBlockElement = new SWAPBlockElement();
            ElementList elementList = new ElementList();
            createProcEvent(ProcEvent.EVENT_SUSPEND,
                "VirtualMachine" + virtualMachineID.ToString(),
                this,
                null);
            
            //Register4Bytes ptr = new Register4Bytes();
            Register4Bytes virtualAddress = ((Interrupted)returnedElementList[0]).additionalArgument;
            Word realAddress = new Word();
            //Word wordPtrValues = new Word();

            //ptr = kernel.getProcessPointer("VirtualMachine" + virtualMachineID.ToString()).savedRegisters.PTR;
            //wordPtrValues = processor.user_memory[ptr][virtualAddress[0, true]];

            realAddress = processor.virtualToAbsoluteAddress(virtualAddress, kernel.getProcessPointer("VirtualMachine" + virtualMachineID.ToString()).savedRegisters.PTR, false);
            /*realAddress = processor.user_memory[(((((wordPtrValues[1] * 16) +
                    wordPtrValues[2]) * 16) +
                    wordPtrValues[3]) * 16 +
                    wordPtrValues[4]) * 16,
                    virtualAddress[2, true][0] * 16 +
                    virtualAddress[2, true][1]];
            */
            swapBlockElement.elementsReturnedAsList = false;
            swapBlockElement.hddBlockAddress = realAddress[2, true];
            swapBlockElement.memoryAddress = -1;
            swapBlockElement.receiver = null;
            swapBlockElement.sender = this;
            swapBlockElement.useSupervisorMemory = false;
            elementList.parent = null;
            elementList.add(swapBlockElement);

            kernel.freeResouce("SWAPBlock", elementList);
            
            return 28;
        }

        private int step28SiSwapBlockForSwapOK()
        {
            ElementList elementList = new ElementList();
            ResourceElement resourceElement = new ResourceElement();

            resourceElement.elementsReturnedAsList = false;
            resourceElement.receiver = this;
            resourceElement.sender = kernel.getProcessPointer("Swap");

            elementList.parent = null;
            elementList.add(resourceElement);
            kernel.askForResource("SwapOK", elementList, returnedElementList);
            return 29;
        }

        private int step29SiSwapGetRamFromSwap()
        {
            RAMResourceElement ramResourceElement = new RAMResourceElement();
            ElementList elementList = new ElementList();

            ramResourceElement.receiver = this;
            ramResourceElement.sender = kernel.getProcessPointer("SWAP");
            ramResourceElement.blockAddress = -1;
            ramResourceElement.useSupervisorMemory = false;
            ramResourceElement.elementsReturnedAsList = false;

            elementList.parent = null;
            elementList.add(ramResourceElement);

            kernel.askForResource("RAMResource", elementList, returnedElementList);
            return 30;
        }

        private int step30SiSwapGiveRamToVirtualMachineRunner()
        {
            ElementList elementList = new ElementList();
            Register4Bytes ptrPlace = new Register4Bytes();
            RAMResourceElement ramResourceElement = new RAMResourceElement();
            Block blockPTR = new Block();
            int intPtrLine = 0;

            ptrPlace = kernel.getProcessPointer("VirtualMachine" + virtualMachineID.ToString()).savedRegisters.PTR;
            blockPTR = kernel.processor.supervisor_memory[ptrPlace.toInt()];

/*            while (((Interrupted)returnedElementList[0]).additionalArgument != blockPTR[intPtrLine][1, true])
            {
                intPtrLine++;
            }*/
            intPtrLine = interrupted.additionalArgument / 256;
            blockPTR[intPtrLine][1, true] = ((RAMResourceElement)returnedElementList[0]).blockAddress;
            blockPTR[intPtrLine][0] = (byte)'0';
            ramResourceElement = ((RAMResourceElement)returnedElementList[0]);
            ramResourceElement.sender = this;
            ramResourceElement.receiver = kernel.getProcessPointer("VirtualMachine" + virtualMachineID.ToString());
            
            elementList.parent = null;
            elementList.add(ramResourceElement);
            kernel.getProcessPointer("VirtualMachine" + virtualMachineID.ToString()).ownedResources.add(ramResourceElement);
            //kernel.freeResouce("RAMResource", elementList);
            createProcEvent(ProcEvent.EVENT_RESUME, "VirtualMachine" + virtualMachineID.ToString(), this, null);
            return 1;
        }

        private void createProcEvent(int p_intEvent,
            String p_strProcName, 
            Process p_processSender, 
            Process p_processReceiver)
        {
            ProcEvent procEvent = new ProcEvent();
            ElementList elementList = new ElementList();

            procEvent.Event = p_intEvent;
            procEvent.ProcName = p_strProcName;
            procEvent.sender = p_processSender;
            procEvent.receiver = p_processReceiver;

            elementList.parent = null;
            elementList[0] = procEvent;

            kernel.freeResouce("ProcEvent", elementList);
        }

        private void putDataToStack(Word p_wordData)
        {
            Register4Bytes ptr = new Register4Bytes();
            Register4Bytes sp = new Register4Bytes();
            Register2Bytes reg2Bytes = new Register2Bytes();

            Block blockPTR = new Block();
            Word word = new Word();

            ptr = kernel.getProcessPointer("VirtualMachine" + virtualMachineID.ToString()).savedRegisters.PTR;
            kernel.getProcessPointer("VirtualMachine" + virtualMachineID.ToString()).savedRegisters.SP++;
            sp = kernel.getProcessPointer("VirtualMachine" + virtualMachineID.ToString()).savedRegisters.SP;
            

            blockPTR = processor.supervisor_memory[ptr];
            word = blockPTR[sp[0, true]];

            if (word[0] == '1')
            {
            }
            else
            {
                reg2Bytes = sp[2, true];
                int block = word[1, true];
                int pos = reg2Bytes;
                processor.user_memory[block, pos] = p_wordData.clone();
            }

        }
      
        private Word getDataFromStack()
        {
            Register4Bytes ptr = new Register4Bytes();
            Register4Bytes sp = new Register4Bytes();
            Register2Bytes reg2Bytes = new Register2Bytes();

            Block blockPTR = new Block();
            Word word = new Word();
            Word wordResult = new Word();

            ptr = kernel.getProcessPointer("VirtualMachine" + virtualMachineID.ToString()).savedRegisters.PTR;
            sp = kernel.getProcessPointer("VirtualMachine" + virtualMachineID.ToString()).savedRegisters.SP;

            blockPTR = processor.supervisor_memory[ptr];
            word = blockPTR[sp[0, true]];

            if (word[0] == (byte)'1')
            {
            }
            else
            {
                reg2Bytes = sp[2, true];
                int block = word[1, true];
                int pos = reg2Bytes;
                
                wordResult = processor.user_memory[block, pos];
                kernel.getProcessPointer("VirtualMachine" + virtualMachineID.ToString()).savedRegisters.SP--;
            }

            return wordResult;
        }
    }
}
