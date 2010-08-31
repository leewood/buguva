using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    public class ProcessList
    {
        private Kernel kernel;
        private List<string> idToName = new List<string>();
        private List<Process> processList = new List<Process>();
        private List<int> processIDs = new List<int>();
        public bool onAddChangeOwner = false;
        public Resource resourceOwner = null;

        public void add(int id, string name, Process process)
        {
            idToName.Add(name);
            processList.Add(process);
            processIDs.Add(id);
            if (onAddChangeOwner)
            {
                process.processList = this;
            }
            
        }

        public void add(Process process)
        {
            if (process != null)
            {
                processIDs.Add(process.id);
                processList.Add(process);
                idToName.Add(process.name);
                if (onAddChangeOwner)
                {
                    process.processList = this;
                }
            }

        }

        public Process getByPos(int pos)
        {
            if ((pos < processList.Count) && (pos >= 0))
            {
                return processList[pos];
            }
            else
            {
                return null;
            }
        }

        public void deleteByPos(int pos)
        {
            if (pos < processList.Count)
            {
                processList.RemoveAt(pos);
                processIDs.RemoveAt(pos);
                idToName.RemoveAt(pos);
            }
        }

        public int Count
        {
            get
            {
                return processIDs.Count;
            }
        }

        public Process this[int i]
        {
            get
            {
                return processList[processIDs.IndexOf(i)];
            }
        }

        public Process this[string i]
        {
            get
            {
                int j = idToName.IndexOf(i);
                if ((j >= 0) && (j < idToName.Count))
                {
                    return processList[idToName.IndexOf(i)];
                }
                else
                {
                    return null;
                }
            }
        }

        public int getIdByName(string name)
        {
            return processIDs[idToName.IndexOf(name)];
        }

        public int getPosByName(string name)
        {
            return idToName.IndexOf(name);
        }

        public void remove(int id)
        {
            int place = processIDs.IndexOf(id);
            if (place >= 0)
            {
                processIDs.RemoveAt(place);
                processList.RemoveAt(place);
                idToName.RemoveAt(place);
            }
        }

        public ProcessList(Resource resourceOwner)
        {
            this.resourceOwner = resourceOwner;
            this.onAddChangeOwner = true;
        }

        public ProcessList(bool onAddChangeOwner)
        {
            this.onAddChangeOwner = onAddChangeOwner;
        }

        public ProcessList()
        {
            onAddChangeOwner = false;
        }

    }
}
