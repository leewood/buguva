using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    public class Resource
    {
        public int id;
        public Process creator;
        public string name;
        public ElementList elementList = new ElementList();
        public ProcessList waitingProcessList = new ProcessList(true);
        public List<int> waitingCount = new List<int>();
        public List<ElementList> waitingParts = new List<ElementList>();
        protected Kernel kernel;
        public List<Resource> resourceList;
        public List<ElementList> whereToReturn = new List<ElementList>();


        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public string Creator
        {
            get
            {
                if (creator != null)
                {
                    return creator.ToString();
                }
                else
                {
                    return "none";
                }
            }
        }
        
        public int ID
        {
            get
            {
                return id;
            }
        }

        public int ElementsCount
        {
            get
            {
                return elementList.Count;
            }
        }

        public List<Process> planner()
        {
            List<Process> resultList = new List<Process>();
            List<int> resultDelete = new List<int>();
            if (waitingProcessList == null)
            {
                waitingProcessList = new ProcessList(this);
            }
            if (waitingProcessList.resourceOwner == null)
            {
                waitingProcessList.resourceOwner = this;
                waitingProcessList.onAddChangeOwner = true;
            }
            bool stop = false;
            for (int i = elementList.Count - 1; i >= 0; i--)
            {
                for (int j = waitingParts.Count - 1; ((j >= 0) && !stop); j--)
                {
                    for (int k = waitingParts[j].Count - 1; ((k >= 0) && !stop); k--)
                    {
                        ResourceElement res1 = waitingParts[j].get(k);
                        ResourceElement res2 = elementList[i];
                        if ((res1.receiver == null) && (res2.receiver == null))
                        {
                            if ((res1.sender == res2.sender) || (res1.sender == null) || (res2.sender == null))
                            {
                                if (res1.isEqual(res2))
                                {
                                    resultDelete.Add(i);
                                    waitingProcessList.getByPos(j).ownedResources.add(elementList[i]);
                                    whereToReturn[j].add(elementList[i]);
                                    waitingParts[j].remove(k);
                                    waitingCount[j]--;
                                    if (waitingCount[j] <= 0)
                                    {
                                        resultList.Add(waitingProcessList.getByPos(j));
                                        waitingParts.RemoveAt(j);
                                        waitingCount.RemoveAt(j);
                                        waitingProcessList.deleteByPos(j);
                                        whereToReturn.RemoveAt(j);
                                        
                                    }
                                    stop = true;
                                }
                            }
                        }
                        else if (res2.receiver == null)
                        {
                            if ((res1.sender == res2.sender) || (res1.sender == null) || (res2.sender == null))
                            {
                                if (res1.isEqual(res2))
                                {
                                    resultDelete.Add(i);
                                    waitingProcessList.getByPos(j).ownedResources.add(elementList[i]);
                                    whereToReturn[j].add(elementList[i]);
                                    waitingParts[j].remove(k);
                                    waitingCount[j]--;
                                    if (waitingCount[j] <= 0)
                                    {
                                        resultList.Add(waitingProcessList.getByPos(j));
                                        waitingParts.RemoveAt(j);
                                        waitingCount.RemoveAt(j);
                                        waitingProcessList.deleteByPos(j);
                                        whereToReturn.RemoveAt(j);
                                    }
                                    stop = true;
                                }
                            }

                        }
                        else if (res1.receiver == res2.receiver)
                        {
                            if ((res1.sender == res2.sender) || (res1.sender == null) || (res2.sender == null))
                            {
                                if (res1.isEqual(res2))
                                {
                                    
                                    resultDelete.Add(i);
                                    lock (waitingProcessList)
                                    {
                                        var process = waitingProcessList.getByPos(j);
                                        if (process != null)
                                        {
                                            process.ownedResources.add(elementList[i]);
                                            whereToReturn[j].add(elementList[i]);
                                            waitingParts[j].remove(k);
                                            waitingCount[j]--;
                                            if (waitingCount[j] <= 0)
                                            {
                                                resultList.Add(waitingProcessList.getByPos(j));
                                                waitingParts.RemoveAt(j);
                                                waitingCount.RemoveAt(j);
                                                waitingProcessList.deleteByPos(j);
                                                whereToReturn.RemoveAt(j);
                                            }
                                        }
                                        stop = true;
                                    }
                                }
                            }
                        }
                    }            
                }
                stop = false;
            }
            resultDelete.Sort();
            for (int i = resultDelete.Count - 1; i >= 0; i--)
            {
                elementList.remove(resultDelete[i]);
            }
            return resultList;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
