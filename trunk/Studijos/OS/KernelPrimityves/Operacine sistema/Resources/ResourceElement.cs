using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    public class ResourceElement
    {
        public ElementList elementList;
        public Process receiver;
        public Process sender;
        public string resourceName;

        public virtual bool HasName
        {
            get
            {
                return false;
            }
        }

        public string isOfResource
        {
            get
            {
                return resourceName;
            }
        }


        public int ElementIndex
        {
            get
            {
                if (elementList != null)
                {
                    return elementList.IndexOf(this);
                }
                return -1;
            }
        }

        public string Sender
        {
            get
            {
                if (sender != null)
                {
                    return sender.name;
                }
                else
                {
                    return "none";
                }
            }
            set
            {
                Process proc = KernelInterface.kernel.getProcessPointer(value);
                sender = proc;
            }
        }

        public string Receiver
        {
            get
            {
                if (receiver != null)
                {
                    return receiver.name;
                }
                else
                {
                    return "none";
                }
            }
            set
            {
                Process proc = KernelInterface.kernel.getProcessPointer(value);
                receiver = proc;
            }
        }

        public bool elementsReturnedAsList;
        virtual public bool isEqual(ResourceElement res)
        {
            return true;
        }

        public override string ToString()
        {
            return "Element of " + isOfResource;
        }
    }
}
