using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApplication1
{
    public class Init : Process
    {
        public Init()
        {
            step.Add(stepStartChildProcesses);
            step.Add(stepCreateSystemResources);
            step.Add(stepBlockOnShutDown);
            step.Add(stepStopChildProcesses);
            step.Add(stepRemoveSystemResources);
            step.Add(stepEternalBlock);
        }


        private int stepStartChildProcesses()
        {
            kernel.createProcess(this, null, this.priority + 1, null, "FileSystem");
            kernel.createProcess(this, null, this.priority + 1, null, "Interrupt");
            kernel.createProcess(this, null, this.priority + 1, null, "ProcessManager");
            return 1;
        }


        private int stepCreateSystemResources()
        {
            kernel.createResource("RAMResource", this);
            ElementList query = new ElementList();
            if (this.processor != null)
            {
                for (int i = 0; i < this.processor.user_memory.Size; i++)
                {
                    RAMResourceElement ramRS = new RAMResourceElement();
                    ramRS.block = this.processor.user_memory[i];
                    ramRS.blockAddress = i;
                    ramRS.useSupervisorMemory = false;
                    ramRS.sender = this;
                    ramRS.receiver = null;
                    ramRS.resourceName = "RAMResource";
                    query.add(ramRS);
                }
                for (int i = 0; i < this.processor.supervisor_memory.Size; i++)
                {
                    RAMResourceElement ramRS = new RAMResourceElement();
                    ramRS.block = this.processor.supervisor_memory[i];
                    ramRS.blockAddress = i;
                    ramRS.useSupervisorMemory = true;
                    ramRS.sender = this;
                    ramRS.receiver = null;
                    ramRS.resourceName = "RAMResource";
                    query.add(ramRS);
                }
                kernel.freeResouce("RAMResource", query);
                kernel.createResource("InputDevice", this);
                query = new ElementList();
                ResourceElement resElem = new ResourceElement();
                resElem.receiver = null;
                resElem.sender = this;
                query.add(resElem);
                kernel.freeResouce("InputDevice", query);
                kernel.createResource("OutputDevice", this);
                query = new ElementList();
                resElem = new ResourceElement();
                resElem.receiver = null;
                resElem.sender = this;
                query.add(resElem);
                kernel.freeResouce("OutputDevice", query);
                kernel.createResource("HardDisk", this);
                query = new ElementList();
                resElem = new ResourceElement();
                resElem.receiver = null;
                resElem.sender = this;
                query.add(resElem);
                kernel.freeResouce("HardDisk", query);
            }
            else
            {
                if (this.kernel.processor != null)
                {
                    this.processor = this.kernel.processor;
                }
                return 1;
            }
            return 2;
        }

        private int stepBlockOnShutDown()
        {
            ElementList request = new ElementList();
            request.add(new ResourceElement());
            ElementList returnArray = new ElementList();
            kernel.askForResource("Shutdown", request, returnArray);
            return 3;
        }




        private int stepRemoveSystemResources()
        {
            kernel.destroyResource("HardDisk");
            kernel.destroyResource("InputDevice");
            kernel.destroyResource("OutputDevice");
            kernel.destroyResource("RAMResource");
            return 5;
        }

        private int stepStopChildProcesses()
        {
            for (int i = children.Count - 1; i >= 0; i--)
            {
                kernel.destroyProcess(children.getByPos(i).name);
            }
            kernel.destroyProcess("Init");
            return 4;
        }
    }
}
