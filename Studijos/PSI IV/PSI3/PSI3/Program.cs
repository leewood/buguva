using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PSI3
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] Args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string arg = "FuturisticCar";
            if (Args.Length > 0)
            {
                arg = Args[0];
            }
            string security = "0";
            if (Args.Length > 1)
            {
                security = Args[1];
            }
            string owner = "";
            if (Args.Length > 2)
            {
                owner = Args[2];
            }
            string useAfter = "false";
            if (Args.Length > 3)
            {
                useAfter = Args[3];
            }
            string[] result = new string[4];
            result[0] = arg;
            result[1] = security;
            result[2] = owner;
            result[3] = useAfter;
            Application.Run(new Form1(result));
        }
    }
}
