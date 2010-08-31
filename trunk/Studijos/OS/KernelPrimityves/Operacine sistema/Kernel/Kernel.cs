using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    public static class KernelInterface
    {
        public static Kernel _kernel = null;
        public static Kernel kernel
        {
            get
            {
                return _kernel;
            }
            set
            {
                _kernel = value;
            }
        }
    }

    public class Kernel: AbstractOS
    {
        public ProcessList processList = new ProcessList(false);
        public List<Resource> resourceList = new List<Resource>();
        private List<string> resourceNames = new List<string>();
        public ProcessList readyProcessList = new ProcessList(true);
        private Process runningProcess = null;
        public Processor processor;
        private ElementList allResourceElements = new ElementList();
        private int lastNotUsedID = 0;
        public bool resChanged = false;
        public bool procChanged = false;

        public bool debugNextStep = false;

        public Kernel()
        {
            KernelInterface.kernel = this;
        }

        public void createProcess(Process p_procParent, 
                                  SavedRegisters p_stateMain,
                                  int p_intPriority,
                                  ElementList p_elementListMain,
                                  String p_strName)
        {
            Process newProc = null;
            procChanged = true;
            switch (p_strName)
            {
                case "Init":
                    newProc = new Init();
                    break;
                case "ProcessManager":
                    newProc = new ProcessManager(this);
                    break;
                case "FileSystem":
                    newProc = new FileSystem();
                    break;
                case "FileReadWrite":
                    newProc = new FileReadWrite(this);
                    break;
                case "Interrupt":
                    newProc = new InterruptProcess();
                    break;
                case "VirtualMachineLoader":
                    newProc = new VirtualMachineLoader(this);
                    break;
                case "SWAP":
                    newProc = new SWAP();
                    break;
                case "HDDDriver":
                    newProc = new HDDDriver();
                    break;
                case "ConsoleDriver":
                    newProc = new ConsoleDriver();
                    break;
                default:
                    if (p_strName.IndexOf("JobControl") >= 0)
                    {
                        newProc = new JobControl();
                    }
                    else
                    {
                        newProc = new VirtualMachineRunner();
                    }
                    break;
            }
            newProc.id = createNewProcessID();
            newProc.name = p_strName;
            processList.add(newProc.id, p_strName, newProc);
            if (p_stateMain != null)
            {
                newProc.savedRegisters = p_stateMain;
            }
            else
            {
                newProc.savedRegisters = new SavedRegisters();
            }
            newProc.priority = p_intPriority;
            newProc.parent = p_procParent;
            if (p_procParent != null)
            {
                newProc.currentDir = p_procParent.currentDir;
            }
            else
            {
                newProc.currentDir = "/usr/";
            }
            if (p_elementListMain != null)
            {
                newProc.ownedResources = new ElementList();
                for (int i = 0; i < p_elementListMain.Count; i++)
                {
                    newProc.ownedResources.add(p_elementListMain[i]);
                   
                }
                p_elementListMain.clear();
            }
            else
            {
                newProc.ownedResources = new ElementList();
            }
            newProc.processState = State.ready;
            newProc.kernel = this;
            newProc.processor = processor;
            readyProcessList.add(newProc.id, p_strName, newProc);
            if (p_procParent != null)
            {
                p_procParent.children.add(newProc.id, p_strName, newProc);
            }
            planner();

        }

        bool wasLastDestroyedProcessRunning = false;

        private void stop(int id)
        {
            lock (processList)
            {
                if (processList[id].processState == State.running)
                {
                    runningProcess = null;
                    wasLastDestroyedProcessRunning = true;
                }
                if (processList[id].processList != null)
                {
                    int pos = processList.getPosByName(processList[id].name);
                    if (processList.resourceOwner != null)
                    {
                        lock (processList.resourceOwner)
                        {
                            processList.resourceOwner.waitingCount.RemoveAt(pos);
                            processList.resourceOwner.waitingParts.RemoveAt(pos);
                            processList.resourceOwner.whereToReturn.RemoveAt(pos);
                        }
                    }
                    processList[id].processList.remove(id);
                }
                int n = processList[id].children.Count;
                for (int p = n - 1; p >= 0; p--)
                {
                    stop(processList[id].children.getByPos(p).id);
                }
                ElementList elem;
                elem = processList[id].ownedResources;
                for (int p = processList[id].ownedResources.Count - 1; p >= 0; p--)
                {

                    ResourceElement res = elem[p];
                    elem.remove(p);
                    allResourceElements.add(res);
                }
                processList[id].removeCreatedResources();
                processList.remove(id);
                this.processor.ioi_devices.TidyThreads();
            }
        }


        public void changePriority(string name, int newPriority)
        {
            int i = processList[name].id;
            int curPriority = processList[i].priority;
            processList[i].priority = newPriority;
            if ((curPriority < newPriority) && (processList[i].processState == State.ready))
            {
                planner();
            }
        }

        public void destroyProcess(String p_strName)
        {
            procChanged = true;
            wasLastDestroyedProcessRunning = false;
            int i = processList[p_strName].id;
            Process parent = processList[i].parent;
            if (parent != null)
            {
                parent.children.remove(i);
            }
            stop(i);
            if (wasLastDestroyedProcessRunning)
            {
                planner();
            }

        }

        public void suspendProcess(String p_strName)
        {
            if (processList[p_strName] != null)
            {
                int i = processList[p_strName].id;
                State state = processList[i].processState;
                if (state == State.running)
                {

                }
                processList[i].processState = ((state == State.blocked) || (state == State.blockedSuspended)) ? State.blockedSuspended : State.readySuspended;
                if (state == State.running)
                {
                    planner();
                }
            }
        }

        public void activateProcess(String p_strName)
        {
            if (processList[p_strName] != null)
            {
                int i = processList[p_strName].id;
                processList[i].processState = ((processList[i].processState == State.blocked) || (processList[i].processState == State.blockedSuspended)) ? State.blocked : State.ready;
                if (processList[i].processState == State.ready)
                {
                    planner();
                }
            }
        }

        private int findResource(string rName)
        {
            int i = resourceNames.IndexOf(rName);
            return i;
        }

        private Resource lastCreatedResourcePointer = null;


        public bool isCH(int chNr)
        {
            Resource res = new Resource();
            switch (chNr)
            {
                case 1:
                    res = getResourcePointer("InputDevice");
                    break;
                case 2:
                    res = getResourcePointer("OutputDevice");
                    break;
                case 3:
                    res = getResourcePointer("HardDisk");
                    break;

            }
            return (res.elementList.Count == 0);
        }

        public bool isChRegUsed(int chNr)
        {
            switch (chNr)
            {
                case 1:
                    return processor.CH1 == 1;
                case 2:
                    return processor.CH2 == 1;
                case 3:
                    return processor.CH3 == 1;
            }
            return false;
        }


        public int findFreeCh()
        {
            if (!isChRegUsed(1) && isCH(1))
            {
                return readStatus.CHANNEL_INPUT_DEVICE;
            }
            else if (!isChRegUsed(2) && isCH(2))
            {
                return readStatus.CHANNEL_OUTPUT_DEVICE;
            }
            else if (!isChRegUsed(3) && isCH(3))
            {
                return readStatus.CHANNEL_HARDDISK;
            }
            return readStatus.CHANNEL_ANY;
        }

        public void createResource(String p_strName, Process p_processCreator)
        {
            resChanged = true;
            if (findResource(p_strName) < 0)
            {
                Resource newRes = null;
                switch (p_strName)
                {
                    case "FileHandler":
                        newRes = new Resource();
                        newRes.name = "FileHandler";
                        break;
                    case "RAMResource":
                        newRes = new RAMResource();
                        break;
                    case "HDDBlock":
                        newRes = new HDDBlock();
                        break;
                    case "readStatus":
                        newRes = new Resource();
                        break;
                    case "Interrupt":
                        newRes = new InterruptResource();
                        break;
                    case "Interrupted":
                        newRes = new Resource();
                        break;
                    case "ProcEvent":
                        newRes = new Resource();
                        break;
                    case "VirtualMachineReady":
                        newRes = new VirtualMachineReady();
                        break;
                    case "readOK":
                        newRes = new readOK();
                        break;
                    default:
                        newRes = new Resource();
                        break;

                }
                newRes.name = p_strName;
                newRes.id = resourceList.Count;
                newRes.creator = p_processCreator;
                newRes.waitingProcessList.resourceOwner = newRes;
                newRes.waitingProcessList.onAddChangeOwner = true;
                resourceList.Add(newRes);
                resourceNames.Add(newRes.name);
                if (p_processCreator != null)
                {
                    p_processCreator.createResource.Add(newRes);
                }
                lastCreatedResourcePointer = newRes;
            }
            else
            {
                lastCreatedResourcePointer = getResourcePointer(p_strName);
            }
        }

        public void destroyResource(String p_strName)
        {
            resChanged = true;
            int i = resourceNames.IndexOf(p_strName);
            for (int j = resourceList[i].waitingProcessList.Count - 1; j >= 0; j--)
            {
                Process proc = resourceList[i].waitingProcessList.getByPos(j);
                proc.processState = (proc.processState == State.blocked) ? State.ready : State.readySuspended;
                readyProcessList.add(proc);
                resourceList[i].whereToReturn[j].add(new ResourceElement());
            }
            resourceList.RemoveAt(i);
            resourceNames.RemoveAt(i);
        }

        public void askForResource(String p_strName, 
            ElementList p_elementListAskedElements,
            ElementList p_elementListPointer)
        {
            resChanged = true;
            int r = findResource(p_strName);
            if (r < 0)
            {
                createResource(p_strName, runningProcess);
                r = findResource(p_strName);
            }
            runningProcess.processState = State.blocked;
            resourceList[r].waitingParts.Add(p_elementListAskedElements);
            for (int i = 0; i < p_elementListAskedElements.Count; i++)
            {
                p_elementListAskedElements[i].resourceName = p_strName;
            }
            resourceList[r].waitingCount.Add(p_elementListAskedElements.Count);
            resourceList[r].waitingProcessList.add(runningProcess);
            p_elementListPointer.clear();
            resourceList[r].whereToReturn.Add(p_elementListPointer);
            readyProcessList.remove(runningProcess.id);
            List<Process> whoGet = resourceList[r].planner();
            for (int i = 0; i < whoGet.Count; i++)
            {
                readyProcessList.add(whoGet[i]);
                whoGet[i].processState = (whoGet[i].processState == State.blocked) ? State.ready : State.readySuspended;
            }
            if (runningProcess.processState == State.blocked)
            {
                readyProcessList.remove(runningProcess.id);
                procChanged = true;
                runningProcess = null;
            }
            planner();
        }

        public Resource lastCreatedResource()
        {
            return lastCreatedResourcePointer;
        }

        public ResourceElement addResourceElement(string resName)
        {
            ResourceElement resElem = new ResourceElement();
            switch (resName)
            {
                case "Shutdown":
                    resElem = new ResourceElement();
                    break;
                case "HardDisk":
                    resElem = new ResourceElement();
                    break;
                case "InputDevice":
                    resElem = new ResourceElement();
                    break;
                case "OutputDevice":
                    resElem = new ResourceElement();
                    break;
                case "RAMResource":
                    resElem = new RAMResourceElement();
                    break;
                case "FileHandler":
                    resElem = new FileHandler();
                    break;
                case "VirtualMachine":
                    resElem = new VirtualMachineResource();
                    break;
                case "HDDBlock":
                    resElem = new HDDBlockElement();
                    break;
                case "ConsoleData":
                    resElem = new ConsoleDataElement();
                    break;
                case "SWAPBlock":
                    resElem = new SWAPBlockElement();
                    break;
                case "File":
                    resElem = new File();
                    break;
                case "ProcEvent":
                    resElem = new ProcEvent();
                    break;
                case "Interrupt":
                    resElem = new InterruptResourceElement();
                    break;
                case "Interrupted":
                    resElem = new Interrupted();
                    break;
                case "ReadOK":
                    resElem = new ResourceElement();
                    break;
                case "readOK":
                    resElem = new readOKElement();
                    break;
                case "readStatus":
                    resElem = new readStatus();
                    break;
                default:
                    resElem = new ResourceElement();
                    break;
            }
            //ElementList query = new ElementList();
            //query.add(resElem);
            //freeResouce(resName, query);
            return resElem;
        }

        public void freeResouce(String p_strName, ElementList p_elementListFreeElements)
        {
            resChanged = true;
            int r = findResource(p_strName);
            if (r < 0)
            {
                this.createResource(p_strName, runningProcess);
                r = findResource(p_strName);
            }
            for (int i = p_elementListFreeElements.Count - 1; i >= 0; i--)
            {
                p_elementListFreeElements[i].resourceName = p_strName;
                resourceList[r].elementList.add(p_elementListFreeElements[i]);
                if (runningProcess != null)
                {
                    runningProcess.ownedResources.removeElement(p_elementListFreeElements[i]);
                }
                p_elementListFreeElements[i].resourceName = p_strName;
            }
            List<Process> tempList = resourceList[r].planner();
            for (int i = tempList.Count - 1; i >= 0; i--)
            {
                Process pr = tempList[i];
                string name = "";
                readyProcessList.add(pr.id, name, pr);
                pr.processState = (pr.processState == State.blocked) ? State.ready : State.readySuspended;
            }
            if (tempList.Count != 0)
            {
                planner();
            }
        }

        private void planner()
        {
            lock (readyProcessList)
            {
                int maxPriority = 0;
                if (readyProcessList.Count > 0)
                {
                    try
                    {
                        maxPriority = readyProcessList.getByPos(0).priority;
                        if (readyProcessList.getByPos(0).processState == State.readySuspended)
                        {
                            maxPriority = 10;
                        }
                    }
                    catch (NullReferenceException)
                    {
                        maxPriority = 0;
                    }
                }
                int curProc = 0;
                for (int i = readyProcessList.Count - 1; i >= 0; i--)
                {
                    if ((readyProcessList.getByPos(i).processState == State.blocked) || (readyProcessList.getByPos(i).processState == State.blockedSuspended))
                    {
                        readyProcessList.deleteByPos(i);
                    }
                }

                for (int i = 0; i < readyProcessList.Count; i++)
                {
                    if ((readyProcessList.getByPos(i).priority < maxPriority) &&
                        ((readyProcessList.getByPos(i).processState == State.ready) || (readyProcessList.getByPos(i).processState == State.running)))
                    {
                        maxPriority = readyProcessList.getByPos(i).priority;
                        curProc = i;
                    }
                }
                Process proc = readyProcessList.getByPos(curProc);
                readyProcessList.deleteByPos(curProc);
                readyProcessList.add(proc);
                if ((proc != runningProcess) && (proc != null) && (proc.processState != State.readySuspended))
                {
                    procChanged = true;
                    if (runningProcess != null)
                    {
                        runningProcess.processState = State.ready;
                    }
                    runningProcess = proc;
                    if (proc != null)
                    {
                        proc.processState = State.running;
                    }

                }

            }
        }


        public int createNewProcessID()
        {
            lastNotUsedID++;
            return lastNotUsedID - 1;
        }

        public Process getProcessPointer(String p_strProcessName)
        {
            return processList[p_strProcessName];
        }



        public Resource getResourcePointer(string p_strResourceName)
        {
            int i = findResource(p_strResourceName);
            if (i >= 0)
            {
                return resourceList[i];
            }
            else
            {
                return null;
            }
        }

        public int getResourceIndex(string p_strResourceName)
        {
            int i = findResource(p_strResourceName);
            if (i >= 0)
            {
                return i;
            }
            else
            {
                return -1;
            }

        }


        public void testCase()
        {
            
            createResource("FileHandler", getProcessPointer("FileReadWrite"));
            Resource res = getResourcePointer("FileHandler");
            res.elementList.add(new FileHandler(this, 0, 0, FileHandler.TYPE_READ));
            createResource("RAMResource", getProcessPointer("FileReadWrite"));
            res = getResourcePointer("RAMResource");
            res.elementList.add(new RAMResourceElement());
            ((RAMResourceElement)res.elementList[0]).blockAddress = 0;
            ((RAMResourceElement)res.elementList[0]).block = new Block();
            res.elementList.add(new RAMResourceElement());
            ((RAMResourceElement)res.elementList[1]).blockAddress = 1;
            ((RAMResourceElement)res.elementList[1]).block = new Block();
            res.elementList.add(new RAMResourceElement());
            ((RAMResourceElement)res.elementList[2]).blockAddress = 2;
            ((RAMResourceElement)res.elementList[2]).block = new Block();
            res.elementList.add(new RAMResourceElement());
            ((RAMResourceElement)res.elementList[3]).blockAddress = 2;
            ((RAMResourceElement)res.elementList[3]).receiver = runningProcess;
            ((RAMResourceElement)res.elementList[3]).block = new Block();

            createResource("readOK", runningProcess);
            res = getResourcePointer("readOK");
            res.elementList.add(new ResourceElement());
        }

        public void systemMessage(string message)
        {
            System.Console.WriteLine(message);
        }

        public override void setProcessor(Processor proc)
        {
            this.processor = proc;
            proc.setOS(this);
        }


        public void createDebugResourceElement(string resName)
        {
            createResource(resName, runningProcess);
            ElementList query = new ElementList();
            System.Console.Write("Sender (Enter for none):");
            string sender = System.Console.ReadLine();
            System.Console.Write("Receiver (Enter for none):");
            string receiver = System.Console.ReadLine();
            ResourceElement resElem = new ResourceElement();
            string temp;
            switch (resName)
            {
                case "Interrupt":
                    resElem = new InterruptResourceElement();
                    System.Console.Write("Interrupt type:");
                    temp = System.Console.ReadLine();
                    if (temp != "")
                    {
                        ((InterruptResourceElement)resElem).type = int.Parse(temp);
                    }
                    else
                    {
                        ((InterruptResourceElement)resElem).type = 0;
                    }
                    System.Console.Write("Additional argument (Enter for none):");
                    temp = System.Console.ReadLine();
                    if (temp != "")
                    {
                        ((InterruptResourceElement)resElem).additionalArgument = int.Parse(temp);
                    }
                    else
                    {
                        ((InterruptResourceElement)resElem).additionalArgument = 0;
                    }

                    break;
                case "FileHandler":
                    resElem = new FileHandler();
                    FileHandler resFH = (FileHandler)resElem;
                    
                    break;
                case "File":
                    resElem = new File();
                    
                    System.Console.Write("Filename:");
                    temp = System.Console.ReadLine();
                    if (temp != "")
                    {
                        ((File)resElem).fileName = temp;
                    }
                    else
                    {
                        ((File)resElem).fileName = "conRAW";
                    }

                    break;

                case "VirtualMachine":
                    resElem = new VirtualMachineResource();
                    VirtualMachineResource resVM = (VirtualMachineResource)resElem;
                    System.Console.Write("Program name:");
                    temp = System.Console.ReadLine();
                    if (temp != "")
                    {
                        resVM.setProgramName(temp);
                    }
                    else
                    {
                        resVM.setProgramName("shell");
                    }


                    
                    break;
            }
            if (receiver != "")
            {
                resElem.receiver = getProcessPointer(receiver);
            }
            else
            {
                resElem.receiver = null;
            }
            if (sender != "")
            {
                resElem.sender = getProcessPointer(sender);
            }
            else
            {
                resElem.sender = null;
            }
            query[0] = resElem;
            freeResouce(resName, query);

        }

        public void debugInfo()
        {
            systemMessage("----------------");
            if (runningProcess != null)
            {
                systemMessage("Running process: " + runningProcess.name);
                systemMessage(" ");
            }
            if (readyProcessList.Count > 0)
            {
                systemMessage("Ready processes: ");
                for (int i = 0; i < readyProcessList.Count; i++)
                {
                    systemMessage(readyProcessList.getByPos(i).name);
                }
                systemMessage(" ");
            }
            if (processList.Count - readyProcessList.Count > 0)
            {
                systemMessage("Blocked processes: ");
                for (int i = 0; i < processList.Count; i++)
                {
                    Process prc = processList.getByPos(i);
                    if ((prc.processState == State.blocked) || (prc.processState == State.blockedSuspended))
                    {
                        systemMessage(prc.name + " blocked waiting for " + prc.processList.resourceOwner.name);
                    }
                }
            }
            systemMessage("----------------");
            systemMessage("");
            systemMessage("Name of resource to create (Enter to cancel):");
            string resName = System.Console.ReadLine();
            if (resName != "")
            {
                createDebugResourceElement(resName);
            }
        }

        public override SavedRegisters getResumeState()
        {
            if (runningProcess.GetType().Name.ToString() == "VirtualMachineRunner")
            {
                return runningProcess.savedRegisters;
            }
            else
            {
                SavedRegisters tmp = new SavedRegisters();
                tmp.CH1 = tmp.CH2 = tmp.CH3 = tmp.SI = tmp.PI = tmp.TI = tmp.IOI = 0;
                tmp.C = 0;
                tmp.IC = 0;
                tmp.SP = 0;
                tmp.PTR = 0;
                return tmp;
            }
        }
        public override bool canGoIntoUserMode()
        {
            Process tmp = runningProcess;
            if (tmp != null)
            {

                if (tmp.GetType().Name.ToString() != "VirtualMachineRunner")
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
                return false;
            }
        }

        public override void call(int callType, int argument)
        {
            if (callType > 0)
            {
                processor.MODE = Processor.SUPERVISOR_MODE;
                Process curProc = runningProcess;
                if (curProc != null)
                {
                    if (curProc.GetType() == typeof(VirtualMachineRunner))
                    {
                       // processor.suspendHLP();
                        if ((curProc.processState == State.ready) || (curProc.processState == State.running))
                        {
                            suspendProcess(curProc.name);
                        }
                        processor.MODE = Processor.SUPERVISOR_MODE;
                        curProc.savedRegisters.TI = processor.TI;
                        curProc.savedRegisters.SI = processor.SI;
                        curProc.savedRegisters.PI = processor.PI;
                        curProc.savedRegisters.IOI = processor.IOI;
                        curProc.savedRegisters.CH1 = processor.CH1;
                        curProc.savedRegisters.CH2 = processor.CH2;
                        curProc.savedRegisters.CH3 = processor.CH3;
                        //curProc.savedRegisters.mode = processor.MODE;
                        curProc.savedRegisters.SP = processor.SP.clone();
                        curProc.savedRegisters.C = processor.C.clone();
                        curProc.savedRegisters.PTR = processor.PTR.clone();
                        curProc.savedRegisters.IC = processor.IC.clone();
                        
                        suspendProcess(curProc.name);
                        processor.MODE = Processor.SUPERVISOR_MODE;
                    }
                }
                createResource("Interrupt", curProc);
                ElementList query = new ElementList();
                InterruptResourceElement resElem = new InterruptResourceElement();
                resElem.sender = curProc;
                resElem.receiver = null;
                resElem.additionalArgument = argument;
                resElem.type = callType;
                resElem.resourceName = "Interrupt";
                query[0] = resElem;
                freeResouce("Interrupt", query);
                if ((curProc != null) && (curProc.processState == State.readySuspended))
                {
                    activateProcess(curProc.name);
                }
            }
        }

        public bool debug = true;

        public override void debugMode()
        {
            debug = true;
        }

        public override void normalMode()
        {
            debug = false;
        }

        public override void debugRunNextStep()
        {
            debugNextStep = true;    
        }

        public void run()
        {
            systemMessage("OS started");
            systemMessage("");
            createProcess(null, null, 0, null, "Init");
            runningProcess = getProcessPointer("Init");
            //testCase();
            
            bool toContinue = true;
            while (toContinue)
            {
                if ((!debug) || ((debug) && (debugNextStep)))
                {
                    if (processor != null)
                    {
                        
                        if ((processor.IOI > 0) && (processor.MODE == Processor.SUPERVISOR_MODE))
                        {
                            lock (processor)
                            {
                                createResource("Interrupt", runningProcess);
                                ElementList query = new ElementList();
                                InterruptResourceElement resElem = new InterruptResourceElement();
                                resElem.sender = runningProcess;
                                resElem.receiver = null;
                                //resElem.additionalArgument = 
                                resElem.type = processor.getInterruptType();
                                if (resElem.type >= 10)
                                {
                                    resElem.additionalArgument = findFreeCh();
                                }
                                resElem.resourceName = "Interrupt";
                                query[0] = resElem;
                                freeResouce("Interrupt", query);
                            }
                        }
                    }
                    if (runningProcess == null)
                    {
                        planner();
                    }
                    if (runningProcess != null)
                    {
                        runningProcess.nextStep();
                    }
                    if (processList.Count == 0)
                    {
                        toContinue = false;
                    }
                    if (debug)
                    {
                        debugNextStep = false;
                        //debugInfo();
                    }
                    if ((readyProcessList.Count == 0) || (!isReadyProcess()))
                    {
                        System.Threading.Thread.Sleep(100);

                    }
                }
              
            }
            processor.terminate();
            processor.terminateHLP();
            systemMessage("");
            systemMessage("OS was shutdown");
        }

        public bool isReadyProcess()
        {
            for (int i = 0; i < readyProcessList.Count; i++)
            {
                if (readyProcessList.getByPos(i).processState != State.readySuspended)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
