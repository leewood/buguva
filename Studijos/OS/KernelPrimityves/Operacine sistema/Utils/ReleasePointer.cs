using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Operacine_sistema.Utils
{
    public class ReleasePointer
    {
        public Mutex ChangerMutex { get; set; }
        public Mutex GetterMutex { get; set; }
        public bool CanContinue { get; set; }

        public static ReleasePointer Create()
        {
            var result = new ReleasePointer();
            result.CanContinue = true;
            result.ChangerMutex = new Mutex();
            result.GetterMutex = new Mutex();
            return result;
        }
    }
}
