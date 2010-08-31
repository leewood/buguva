using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Operacine_sistema
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>

        public static bool goToDebugMode = false;

        [STAThread]
        static void Main()
        {
            //ConsoleApplication1.HardDiskDriveUtils.installOS("hdd");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form_OS());
        }
    }
}
