using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    public class ElementList
    {
        public Resource parent;
        private List<ResourceElement> element = new List<ResourceElement>();
        public ElementList()
        {
        }

        public ResourceElement this[int i]
        {
            get
            {
                return get(i);
            }
            set
            {
                if (i < Count)
                {
                    element[i] = value;
                    value.elementList = this;
                }
                else
                {
                    add(value);
                }
            }
        }
        public int Count
        {
            get
            {
                return element.Count;
            }
        }

        public ElementList(Resource parent)
        {
            this.parent = parent;
        }

        public void clear()
        {
            this.element.Clear();
        }

        public void add(ResourceElement element)
        {
            if (element != null)
            {
                this.element.Add(element);
                element.elementList = this;
            }
        }

        public ResourceElement remove(int i)
        {
            ResourceElement tmp = element[i];
            element.Remove(tmp);
            tmp.elementList = null;
            return tmp;
        }

        public int IndexOf(ResourceElement elem)
        {
            return element.IndexOf(elem);
        }

        public void removeElement(ResourceElement elems)
        {
            if (this.element.IndexOf(elems) >= 0)
            {
                element.Remove(elems);
            }
        }

        public ResourceElement get(int i)
        {
            if ((i >= 0) && (i < this.element.Count))
            {
                return this.element[i];
            }
            else
            {
                return null;
            }
        }

    }
}
