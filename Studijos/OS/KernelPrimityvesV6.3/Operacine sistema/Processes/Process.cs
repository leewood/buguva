using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Operacine_sistema.Utils;

namespace ConsoleApplication1
{
    public class Process
    {
        public int id;
        public SavedRegisters savedRegisters;
        public Processor processor;
        public ProcessList processList;
        public List<Resource> createResource = new List<Resource>();
        public ElementList ownedResources;
        public State processState;
        public Resource blockedFor = null;
        public int priority;
        public string name;
        public Process parent;
        public ProcessList children = new ProcessList(false);
        public Kernel kernel;
        public String currentDir;
        public List<ReleasePointer> Semophores = new List<ReleasePointer>();
       
        protected List<FileHandler> fileHandlersList = new List<FileHandler>();

        protected List<SingleStep> step = new List<SingleStep>();
        protected int currentStep = 0;

        public string ProcessName
        {
            get
            {
                return name;
            }
        }

        public int Priority
        {
            get
            {
                return priority;
            }
            set
            {
                priority = value;
            }
        }

        public State ProcessState
        {
            get
            {
                return processState;
            }
            set
            {
                processState = value;
            }
        }

        public int CurrentStep
        {
            get
            {
                return currentStep;
            }
            set
            {
                currentStep = value;
            }
        }

        public override string ToString()
        {
            if (processState == State.running)
            { 
                return "*" + name;
            }
            else if ((processState == State.blocked) || (processState == State.blockedSuspended))
            {
                return "-" + name;
            }
            else
            {
                return "+" + name;
            }
        }

        public void nextStep()
        {
            if (currentStep < step.Count)
            {
                currentStep = (step[currentStep])();
            }
        }

        public int stepEternalBlock()
        {
            ElementList elem = new ElementList();
            ElementList returnArray = new ElementList();
            elem.add(new ResourceElement());
            kernel.askForResource("EternalBlock", elem, returnArray);
            return 1;
        }

        public void removeCreatedResources()
        {
            lock (Semophores)
            {
                foreach (var semo in Semophores)
                {
                    semo.CanContinue = false;
                }
            }
        }

        public void AddNewSemophore(ReleasePointer pointer)
        {
            lock (Semophores)
            {
                Semophores.Add(pointer);
                pointer.ChangerMutex.WaitOne();
            }
        }

        public void RemoveSemophore(ReleasePointer pointer)
        {
            lock (Semophores)
            {
                var index = Semophores.IndexOf(pointer);
                if (index != null)
                {
                    Semophores.RemoveAt(index);                   
                }
                pointer.ChangerMutex.ReleaseMutex();
            }
        }

        
    }
}
