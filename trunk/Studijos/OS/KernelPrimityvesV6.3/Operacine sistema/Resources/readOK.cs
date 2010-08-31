using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApplication1
{
    public class readOK : Resource
    {
    }

    public class readOKElement : ResourceElement
    {
        public int onChannel = 0;

        public override bool isEqual(ResourceElement res)
        {
            readOKElement elem = (readOKElement)res;
            if ((this.onChannel == elem.onChannel) || (this.onChannel == 0) || (elem.onChannel == 0))
            {
                return true;
            }
            else
            {
                return false;
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
            return String.Format("CH{0} read OK", onChannel);
        }
    }
}
