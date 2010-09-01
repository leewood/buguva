using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Metodai4
{
    interface IMetodas
    {        
        double calc(FunctionDel function, double a, double b);
        double calcByN(FunctionDel function, double a, double b, int N);
        double calcByEpsilon(FunctionDel function, double a, double b, double eps);
        int tikslumoKlase();

    }
}
