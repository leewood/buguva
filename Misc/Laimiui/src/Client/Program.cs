using System;
using System.Collections.Generic;

using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Client
{
    static class Program
    {
        public static MainForm manoForma;
        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            manoForma = new MainForm();
            
            Application.Run(manoForma);
        }
    }
}
