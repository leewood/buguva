using System;
using System.Collections.Generic;

using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Data.SqlClient;
using System.Diagnostics;

namespace MainServer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            killMyBrothers();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
            killMySelf();
        }

        public static void killMyBrothers()
        {
            Process[] ProcList;
            Process curProc = Process.GetCurrentProcess();
            ProcList = Process.GetProcessesByName(curProc.ProcessName);
            for (int b = 0; b <= ProcList.Length - 1; b++)
            {
                if (ProcList[b].Id != curProc.Id) 
                  ProcList[b].Kill();
            }
        }

        public static void killMySelf()
        {
            Process[] ProcList;
            Process curProc = Process.GetCurrentProcess();
            curProc.Kill();
        }
    }


 
}
