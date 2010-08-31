using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    public class InterruptProcess: Process
    {
        private ElementList returnArray = new ElementList();
        private InterruptResourceElement curInterrupt;

        public InterruptProcess()
        {
            step.Add(stepBlockOnInterruptResource);
            step.Add(stepFindJobControlToUse);
            step.Add(stepCheckWhatToDoNext);
            step.Add(stepKillSender);
            step.Add(stepSendInterruptedResource);
            step.Add(stepSendReadStatusResource);
        }

        int cycleCount = 0;

        private int stepBlockOnInterruptResource()
        {
            ElementList query = new ElementList();
            ResourceElement queryElem = new ResourceElement();
            queryElem.receiver = this;
            queryElem.sender = null;
            query[0] = queryElem;
            kernel.askForResource("Interrupt", query, returnArray);
            cycleCount = 1;
            return cycleCount;
        }

        private JobControl curJonCont;

        private int stepFindJobControlToUse()
        {
            curInterrupt = (InterruptResourceElement)returnArray[0];
            if ((curInterrupt != null) && (curInterrupt.type > 0))
            if ((curInterrupt.sender != null) && (curInterrupt.sender.GetType() == typeof(VirtualMachineRunner)))
            {
                curJonCont = (JobControl)(curInterrupt.sender.parent);
            }
            else
            {
                curJonCont = null;
            }
            cycleCount++;
            return cycleCount;
        }


        private int stepCheckWhatToDoNext()
        {
            switch (curInterrupt.type)
            {
                case 1:
                    cycleCount++;
                    return cycleCount;
                case 2:
                    cycleCount++;
                    return cycleCount;
                case 3:
                    cycleCount++;
                    return cycleCount;
                case 4:
                    cycleCount += 2;
                    return cycleCount;
                case 5:
                    cycleCount += 2;
                    return cycleCount;
                case 6:
                    cycleCount += 2;
                    return cycleCount;
                case 7:
                    cycleCount += 2;
                    return cycleCount;
                case 8:
                    cycleCount += 2;
                    return cycleCount;
                case 9:
                    cycleCount += 2;
                    return cycleCount;
                case 10:
                    cycleCount += 3;
                    return cycleCount;
                case 11:
                    cycleCount += 3;
                    return cycleCount;
                case 0:
                    cycleCount = 0;
                    return cycleCount;
                default:
                    cycleCount++;
                    return cycleCount;
            }
        }

        private int stepKillSender()
        {
            ElementList query = new ElementList();
            ProcEvent queryElem = new ProcEvent();
            queryElem.sender = this;
            queryElem.receiver = kernel.getProcessPointer("ProcessManager");
            queryElem.Event = ProcEvent.EVENT_STOP;
            queryElem.ProcName = curJonCont.name;
            query[0] = queryElem;
            kernel.createResource("ProcEvent", this);
            kernel.freeResouce("ProcEvent", query);
            cycleCount = 0;
            return cycleCount;
        }

        private int stepSendInterruptedResource()
        {
            ElementList query = new ElementList();
            Interrupted queryElem = new Interrupted();
            queryElem.sender = this;
            queryElem.receiver = curJonCont;
            if (curInterrupt.type == 4)
            {
                queryElem.type = Interrupted.SI_IN;
                queryElem.additionalArgument = curInterrupt.additionalArgument;
            }
            else if (curInterrupt.type == 5)
            {
                queryElem.type = Interrupted.SI_OUT;
                queryElem.additionalArgument = curInterrupt.additionalArgument;
            }
            else if (curInterrupt.type == 6)
            {
                queryElem.type = Interrupted.SI_HALT;
                queryElem.additionalArgument = curInterrupt.additionalArgument;
            }
            else if (curInterrupt.type == 7)
            {
                queryElem.type = Interrupted.SI_SWAP;
                queryElem.additionalArgument = curInterrupt.additionalArgument;
            }
            else if (curInterrupt.type == 8)
            {
                queryElem.type = Interrupted.SI_CALL;
                queryElem.additionalArgument = curInterrupt.additionalArgument;
            }
            else if (curInterrupt.type == 9)
            {
                queryElem.type = Interrupted.TI;
            }
            query[0] = queryElem;
            kernel.createResource("Interrupted", this);
            kernel.freeResouce("Interrupted", query);
            cycleCount = 0;
            return cycleCount;
        }

        private int stepSendReadStatusResource()
        {
            ElementList query = new ElementList();
            readStatus queryElem = new readStatus();
            queryElem.sender = this;
            queryElem.receiver = null;
            queryElem.onChannel = curInterrupt.additionalArgument;
            if (curInterrupt.type == 10)
            {
                queryElem.status = readStatus.STATUS_SUCCESS;
            }
            else
            {
                queryElem.status = readStatus.STATUS_ERROR;
            }
            queryElem.onChannel = curInterrupt.additionalArgument;
            query[0] = queryElem;
            kernel.createResource("readStatus", this);
            kernel.freeResouce("readStatus", query);
            cycleCount = 0;
            return cycleCount;
        }

    }
}
