using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApplication1
{
    public enum ProcessEventEnum
    {
        NewProcess,
        ResumeProcess,
        StopProcess,
        SuspendProcess
    }

    public class ProcEvent : ResourceElement
    {
        public int Event;
        public string ProcName;
        public string ProcessName
        {
            get
            {
                return ProcName;
            }
            set
            {
                ProcName = value;
            }
        }

        public string EventName
        {
            get
            {
                switch (Event)
                {
                    case EVENT_NEW: return "NEW";
                    case EVENT_RESUME: return "RESUME";
                    case EVENT_STOP: return "STOP";
                    case EVENT_SUSPEND: return "SUSPEND";
                    default: return "any event";
                }
            }
        }

        public ProcessEventEnum ProcessEvent
        {
            get
            {
                return toEnum(Event);
            }
            set
            {
                Event = toInt(value);
            }
        }


        private ProcessEventEnum toEnum(int int_Event)
        {
            switch (int_Event)
            {
                case EVENT_NEW: return ProcessEventEnum.NewProcess;
                case EVENT_RESUME: return ProcessEventEnum.ResumeProcess;
                case EVENT_STOP: return ProcessEventEnum.StopProcess;
                case EVENT_SUSPEND: return ProcessEventEnum.SuspendProcess;
                default: return ProcessEventEnum.ResumeProcess;
            }
        }

        private int toInt(ProcessEventEnum enum_Event)
        {
            switch (enum_Event)
            {
                case ProcessEventEnum.NewProcess: return EVENT_NEW;
                case ProcessEventEnum.ResumeProcess: return EVENT_RESUME;
                case ProcessEventEnum.StopProcess: return EVENT_STOP;
                case ProcessEventEnum.SuspendProcess: return EVENT_SUSPEND;
                default: return EVENT_RESUME;
            }
        }

        public override bool HasName
        {
            get
            {
                return true;
            }
        }

        public override string ToString()
        {
            if (ProcessName != "")
            {
                return String.Format("{0} {1}", EventName, ProcessName);
            }
            return "Any ProcEvent resource";
        }

        public const int EVENT_NEW = 1;
        public const int EVENT_STOP = 3;
        public const int EVENT_RESUME = 2;
        public const int EVENT_SUSPEND = 4;
    }
}
