using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApplication1
{
    public class ProcessManager : Process
    {
        ElementList returnedElementList = new ElementList();
        VirtualMachineReady vmReady = new VirtualMachineReady();
        int jobControlID = 0;

        public ProcessManager(Kernel p_kernelMain)
        {
            kernel = p_kernelMain;
            step.Add(step0StartSwapAndWirtualMachineLoader);
            step.Add(step1CreateProcEventForShell);
            step.Add(step2BlockForProcEvent);
            step.Add(step3GetProcEventType);
            step.Add(step4CreateVirtualMachine);
            step.Add(step5SendVirtualMachineToLoader);
            step.Add(step6BlockForVirtualMachineReady);
            step.Add(step7GetRam);
            step.Add(step8StartJobControl);
            step.Add(step9SuspendProcess);
            step.Add(step10ResumeProcess);
            step.Add(step11StopProcess);
        }

        private int step0StartSwapAndWirtualMachineLoader()
        {
            SavedRegisters stateNewProcess = new SavedRegisters();
            ElementList elementListNewProcess = new ElementList();
            ElementList elementListNewProcess2 = new ElementList();
            kernel.createProcess(this, stateNewProcess, 2, elementListNewProcess, "SWAP");
            kernel.createProcess(this, stateNewProcess, 2, elementListNewProcess2, "VirtualMachineLoader");
            return 1;
        }

        private int step1CreateProcEventForShell()
        {
            ElementList elementList = new ElementList();
            ProcEvent procEventShell = new ProcEvent();

            procEventShell.Event = ProcEvent.EVENT_NEW;
            procEventShell.ProcName = "shell";
            procEventShell.sender = this;
            procEventShell.receiver = this;

            elementList.parent = null;
            elementList[0] = procEventShell;

            kernel.freeResouce("ProcEvent", elementList);
            return 2;
        }

        private int step2BlockForProcEvent()
        {
            ElementList elementList = new ElementList();
            ProcEvent procEventAsked = new ProcEvent();

            procEventAsked.receiver = this;
            procEventAsked.sender = null;
            elementList.parent = null;
            elementList[0] = procEventAsked;

            kernel.askForResource("ProcEvent", elementList, returnedElementList);
            return 3;
        }

        private int step3GetProcEventType()
        {
            switch (((ProcEvent)returnedElementList[0]).Event)
            {
                case ProcEvent.EVENT_NEW: return 4;

                case ProcEvent.EVENT_SUSPEND: return 9;

                case ProcEvent.EVENT_RESUME: return 10;

                case ProcEvent.EVENT_STOP: return 11;

                default: return 2;
            }
        }

        private int step4CreateVirtualMachine()
        {
            VirtualMachineResource virtualMachineResource = new VirtualMachineResource();
            ElementList elementList = new ElementList();

            virtualMachineResource.setProgramName(((ProcEvent)returnedElementList[0]).ProcName);
            virtualMachineResource.elementsReturnedAsList = false;
            virtualMachineResource.sender = this;
            virtualMachineResource.VirtualMachineIndex = jobControlID;
            elementList.parent = null;
            elementList[0] = virtualMachineResource;

            kernel.freeResouce("VirtualMachine", elementList);
            return 5;
        }

        private int step5SendVirtualMachineToLoader()
        {
            //Do nothing :)
            return 6;
        }

        private int step6BlockForVirtualMachineReady()
        {
            ElementList elementList = new ElementList();
            ResourceElement elem = new ResourceElement();

            elem.receiver = this;
            elementList.parent = null;
            elementList[0] = elem;

            kernel.askForResource("VirtualMachineReady", elementList, returnedElementList);
            return 7;
        }

        private int step7GetRam()
        {
            ElementList elementList = new ElementList();
            RAMResourceElement ramResource = new RAMResourceElement();
            vmReady = (VirtualMachineReady)kernel.getResourcePointer("VirtualMachineReady");
            
            for (int i = 0; i < vmReady.returnElementsCount; i++)
            {
                ramResource = new RAMResourceElement();
                ramResource.elementsReturnedAsList = true;
                ramResource.receiver = this;
                ramResource.useSupervisorMemory = false;
                ramResource.blockAddress = -1;
                elementList.add(ramResource);
            }
            elementList.parent = null;

            kernel.askForResource("RAMResource", elementList, returnedElementList);
            return 8;
        }

        private int step8StartJobControl()
        {
            SavedRegisters savedRegistersNewProcess = new SavedRegisters();

            savedRegistersNewProcess.C = 0;
            savedRegistersNewProcess.IC = 0;
            savedRegistersNewProcess.SP = 120 * 256;
            savedRegistersNewProcess.PTR = vmReady.getMachineAddress();

            kernel.createProcess(this, savedRegistersNewProcess, 4, returnedElementList, "JobControl" + jobControlID.ToString());
            jobControlID++;
            return 2;
        }

        private int step9SuspendProcess()
        {
            kernel.suspendProcess(((ProcEvent)returnedElementList[0]).ProcName);
            return 2;
        }

        private int step10ResumeProcess()
        {
            kernel.activateProcess(((ProcEvent)returnedElementList[0]).ProcName);
            return 2;
        }

        private int step11StopProcess()
        {
            kernel.destroyProcess(((ProcEvent)returnedElementList[0]).ProcName);
            return 2;
        }
    }
}
