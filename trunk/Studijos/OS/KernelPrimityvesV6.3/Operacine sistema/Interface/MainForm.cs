using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using ConsoleApplication1;
using Testing;

namespace Operacine_sistema
{
    public partial class Form_OS : Form
    {
        private Form_FilesEditor filesEditor;
        private ConsoleApplication1.Processor proc = new ConsoleApplication1.Processor("hdd");
        private ConsoleApplication1.Kernel os;
        private String register = "";
        private Thread th;

        public Form_OS()
        {
            WinConsole.Initialize();
            InitializeComponent();
            os = new Kernel();
			os.setProcessor(proc);
            th = new Thread(specOSRun);
            this.Left = 0;
            this.Top = 0;
            button_debugMode.Enabled = false;
            button_normalMode.Enabled = true;
            button_nextStep.Enabled = false;
            formInit();
            ResourcesList resList = new ResourcesList(os, this);
            resList.Show();
            ProcessesDebuger procDebug = new ProcessesDebuger(os, resList);
            procDebug.Show();
            WinConsole.Initialize();
            Console.SetError(new ConsoleWriter(Console.Error, Testing.ConsoleColor.Red | Testing.ConsoleColor.Intensified | Testing.ConsoleColor.WhiteBG, ConsoleFlashMode.FlashUntilResponse, true));
            WinConsole.Flash(true);
            WinConsole.Visible = true;
            int x;
            int y;
            int width, height;
            WinConsole.GetWindowPosition(out x, out y, out width, out height);
            WinConsole.SetWindowPosition(this.Width, resList.Height, width + 40, height + 40);
            Console.Title = "Console window";            
        }

        private void specOSRun()
        {
            os.run();
        }

        private void formInit()
        {
            if (proc.done)
            {
                proc.done = false;

                
                register = "";
                register = register + (char)proc.C.getByteAt(0);
                register = register + (char)proc.C.getByteAt(1);
                register = register + (char)proc.C.getByteAt(2);
                register = register + (char)proc.C.getByteAt(3);
                textBox_registerC.Text = register;

                register = "";
                register = register + (char)proc.IC.getByteAt(0);
                register = register + (char)proc.IC.getByteAt(1);
                register = register + (char)proc.IC.getByteAt(2);
                register = register + (char)proc.IC.getByteAt(3);
                textBox_registerIC.Text = register;

                register = "";
                register = register + (char)proc.PTR.getByteAt(0);
                register = register + (char)proc.PTR.getByteAt(1);
                register = register + (char)proc.PTR.getByteAt(2);
                register = register + (char)proc.PTR.getByteAt(3);
                textBox_registerPTR.Text = register;

                register = "";
                register = register + (char)proc.SP.getByteAt(0);
                register = register + (char)proc.SP.getByteAt(1);
                register = register + (char)proc.SP.getByteAt(2);
                register = register + (char)proc.SP.getByteAt(3);
                textBox_registerSP.Text = register;

                register = "";
                register = register + proc.IOI.ToString();
                textBox_registerIOI.Text = register;

                register = "";
                register = register + proc.SI.ToString();
                textBox_registerSI.Text = register;

                register = "";
                register = register + proc.TI.ToString();
                textBox_registerTI.Text = register;

                register = "";
                register = register + proc.PI.ToString();
                textBox_registerPI.Text = register;

                register = "";
                register = register + proc.MODE.ToString();
                textBox_registerMODE.Text = register;

                register = "";
                register = register + proc.CH1.ToString();
                textBox_registerCH1.Text = register;

                register = "";
                register = register + proc.CH2.ToString();
                textBox_registerCH2.Text = register;

                register = "";
                register = register + proc.CH3.ToString();
                textBox_registerCH3.Text = register;
            }

            textBox_blockAddress.Text = ConsoleApplication1.Utils.BlockAddressToString(lookingInRam);

            if (((lookingInSupervisor) && (proc.supervisor_memory.DebugChanged)) || ((!lookingInSupervisor) && (proc.user_memory.DebugChanged)))
            {
                for (int i = 0; i <= 255; i++)
                {
                    listBox_memory.Items.Clear();
                    if (!lookingInSupervisor)
                    {
                        listBox_memory.Items.Add((object)(proc.user_memory[lookingInRam].getWordAt(i).clone().toString()));
                    }
                    else
                    {
                        listBox_memory.Items.Add((object)(proc.supervisor_memory[lookingInRam].getWordAt(i).clone().toString()));
                    }
                }
            }
        }

        private void button_debugMode_Click(object sender, EventArgs e)
        {
            textBox_registerC.ReadOnly    = false;
            textBox_registerIC.ReadOnly   = false;
            textBox_registerPTR.ReadOnly  = false;
            textBox_registerSP.ReadOnly   = false;
            textBox_registerIOI.ReadOnly  = false;
            textBox_registerSI.ReadOnly   = false;
            textBox_registerTI.ReadOnly   = false;
            textBox_registerPI.ReadOnly   = false;
            textBox_registerMODE.ReadOnly = false;
            textBox_registerCH1.ReadOnly  = false;
            textBox_registerCH2.ReadOnly  = false;
            textBox_registerCH3.ReadOnly  = false;
            proc.debugMode();
            button_nextStep.Enabled = true;
            button_debugMode.Enabled = false;
            button_normalMode.Enabled = true;
            useDebuger = true;
        }

        private void button_normalMode_Click(object sender, EventArgs e)
        {
            textBox_registerC.ReadOnly    = true;
            textBox_registerIC.ReadOnly   = true;
            textBox_registerPTR.ReadOnly  = true;
            textBox_registerSP.ReadOnly   = true;
            textBox_registerIOI.ReadOnly  = true;
            textBox_registerSI.ReadOnly   = true;
            textBox_registerTI.ReadOnly   = true;
            textBox_registerPI.ReadOnly   = true;
            textBox_registerMODE.ReadOnly = true;
            textBox_registerCH1.ReadOnly  = true;
            textBox_registerCH2.ReadOnly  = true;
            textBox_registerCH3.ReadOnly  = true;
            proc.normalMode();
            button_nextStep.Enabled = false;
            button_debugMode.Enabled = true;
            button_normalMode.Enabled = false;
            useDebuger = false;
        }

        private void listBox_memory_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox_memoryEdit.ReadOnly = false;
        }

        private void button_memoryEditOK_Click(object sender, EventArgs e)
        {
            int index = listBox_memory.SelectedIndex;

            if (textBox_memoryEdit.Text.Length == 8 && listBox_memory.SelectedIndex >= 0)
            {
                listBox_memory.Items.Insert(listBox_memory.SelectedIndex, ((object)textBox_memoryEdit.Text));
                listBox_memory.Items.RemoveAt(listBox_memory.SelectedIndex);               

                if (radioButton_userMemory.Checked)
                {
                    ConsoleApplication1.Block tempBlock = proc.user_memory[ConsoleApplication1.Utils.StringToBlockAddress(textBox_blockAddress.Text)];
                    
                    tempBlock[index].writeAt(0, (byte)textBox_memoryEdit.Text[0]);
                    tempBlock[index].writeAt(1, (byte)textBox_memoryEdit.Text[1]);
                    tempBlock[index].writeAt(2, (byte)textBox_memoryEdit.Text[2]);
                    tempBlock[index].writeAt(3, (byte)textBox_memoryEdit.Text[3]);
                    tempBlock[index].writeAt(4, (byte)textBox_memoryEdit.Text[4]);
                    tempBlock[index].writeAt(5, (byte)textBox_memoryEdit.Text[5]);
                    tempBlock[index].writeAt(6, (byte)textBox_memoryEdit.Text[6]);
                    tempBlock[index].writeAt(7, (byte)textBox_memoryEdit.Text[7]);
                }
                else
                {
                    ConsoleApplication1.Block tempBlock = proc.supervisor_memory[ConsoleApplication1.Utils.StringToBlockAddress(textBox_blockAddress.Text)];

                    tempBlock[index].writeAt(0, (byte)textBox_memoryEdit.Text[0]);
                    tempBlock[index].writeAt(1, (byte)textBox_memoryEdit.Text[1]);
                    tempBlock[index].writeAt(2, (byte)textBox_memoryEdit.Text[2]);
                    tempBlock[index].writeAt(3, (byte)textBox_memoryEdit.Text[3]);
                    tempBlock[index].writeAt(4, (byte)textBox_memoryEdit.Text[4]);
                    tempBlock[index].writeAt(5, (byte)textBox_memoryEdit.Text[5]);
                    tempBlock[index].writeAt(6, (byte)textBox_memoryEdit.Text[6]);
                    tempBlock[index].writeAt(7, (byte)textBox_memoryEdit.Text[7]);
                }

                textBox_memoryEdit.Text = "";
                textBox_memoryEdit.ReadOnly = true;
            }
        }

        private void button_filesEditor_Click(object sender, EventArgs e)
        {
            filesEditor = new Form_FilesEditor( proc );
            filesEditor.Visible = true;
        }

        private void button_nextStep_Click(object sender, EventArgs e)
        {
            proc.nextStep();
            formInit();
        }

        private void button_nextBlock_Click(object sender, EventArgs e)
        {
            int g;

            g = ConsoleApplication1.Utils.StringToBlockAddress(textBox_blockAddress.Text) + 1;

            if (g > 65535)
                g = 0;

            lookingInRam = g;
            proc.user_memory.DebuggingMem = g;
            proc.supervisor_memory.DebuggingMem = g;

            textBox_blockAddress.Text = ConsoleApplication1.Utils.BlockAddressToString( g );

            listBox_memory.Items.Clear();

            if (radioButton_userMemory.Checked)
            {
                lookingInSupervisor = false;
                for (int i = 0; i <= 255; i++)
                {
                    listBox_memory.Items.Add((object)(proc.user_memory[g].getWordAt(i).toString()));
                }
            }
            else
            {
                lookingInSupervisor = true;
                for (int i = 0; i <= 255; i++)
                {
                    listBox_memory.Items.Add((object)(proc.supervisor_memory[g].getWordAt(i).toString()));
                }
            }


        }

        private void button_previousBlock_Click(object sender, EventArgs e)
        {
            int g;

            g = ConsoleApplication1.Utils.StringToBlockAddress(textBox_blockAddress.Text) - 1;

            if (g < 0)
                g = 65535;

            lookingInRam = g;
            proc.user_memory.DebuggingMem = g;
            proc.supervisor_memory.DebuggingMem = g;

            textBox_blockAddress.Text = ConsoleApplication1.Utils.BlockAddressToString(g);

            listBox_memory.Items.Clear();

            if (radioButton_userMemory.Checked)
            {
                lookingInSupervisor = false;
                for (int i = 0; i <= 255; i++)
                {
                    listBox_memory.Items.Add((object)(proc.user_memory[g].getWordAt(i).toString()));
                }
            }
            else
            {
                lookingInSupervisor = true;
                for (int i = 0; i <= 255; i++)
                {
                    listBox_memory.Items.Add((object)(proc.supervisor_memory[g].getWordAt(i).toString()));
                }
            }
        }

        private void button_shutDown_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button_restart_Click(object sender, EventArgs e)
        {
            proc.clearRegisters();
            if (useDebuger)
            {
                proc.debugMode();
            }
            else
            {
                proc.normalMode();
            }            
            th.Abort();
            os = new Kernel();
            os.setProcessor(proc);
            proc.terminateHLP();
            formInit();
            //os.loadPC(textBox1.Text);
            proc.startHLP();
            th = new Thread(specOSRun);
            th.Start();
            proc.MODE = ConsoleApplication1.Processor.USER_MODE;

        }

        bool useDebuger = true;
        int lookingInRam = 0;
        bool lookingInSupervisor = false;

        private void button1_Click(object sender, EventArgs e)
        {
            button_nextStep.Enabled = true;
            button_normalMode.Enabled = true;
            button_debugMode.Enabled = false;
            if (useDebuger)
            {
                proc.debugMode();
            }
            else
            {
                proc.normalMode();
            }
            
            th.Start();//os.loadPC(textBox1.Text);
            //proc.MODE = ConsoleApplication1.Processor.USER_MODE;
            formInit();
        }

        private void radioButton_supervisoryMemory_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_supervisoryMemory.Checked)
            {
                listBox_memory.Items.Clear();
                for (int i = 0; i <= 255; i++)
                {
                    listBox_memory.Items.Add((object)(proc.supervisor_memory[0].getWordAt(i).clone().toString()));
                }

                textBox_blockAddress.Text = ConsoleApplication1.Utils.BlockAddressToString(0);
            }
        }

        private void radioButton_userMemory_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_userMemory.Checked)
            {
                listBox_memory.Items.Clear();
                for (int i = 0; i <= 255; i++)
                {
                    listBox_memory.Items.Add((object)(proc.user_memory[0].getWordAt(i).clone().toString()));
                }

                textBox_blockAddress.Text = ConsoleApplication1.Utils.BlockAddressToString(0);
            }
        }

        private void textBox_registerC_TextChanged(object sender, EventArgs e)
        {
            if (textBox_registerC.Text.Length == 4)
            {
                proc.C.writeAt(0, (byte)textBox_registerC.Text[0]);
                proc.C.writeAt(1, (byte)textBox_registerC.Text[1]);
                proc.C.writeAt(2, (byte)textBox_registerC.Text[2]);
                proc.C.writeAt(3, (byte)textBox_registerC.Text[3]);
            }

        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Operacine_sistema.Program.goToDebugMode)
            {
                Operacine_sistema.Program.goToDebugMode = false;
                textBox_registerC.ReadOnly = false;
                textBox_registerIC.ReadOnly = false;
                textBox_registerPTR.ReadOnly = false;
                textBox_registerSP.ReadOnly = false;
                textBox_registerIOI.ReadOnly = false;
                textBox_registerSI.ReadOnly = false;
                textBox_registerTI.ReadOnly = false;
                textBox_registerPI.ReadOnly = false;
                textBox_registerMODE.ReadOnly = false;
                textBox_registerCH1.ReadOnly = false;
                textBox_registerCH2.ReadOnly = false;
                textBox_registerCH3.ReadOnly = false;
                proc.debugMode();
                button_nextStep.Enabled = true;
                button_debugMode.Enabled = false;
                button_normalMode.Enabled = true;
                useDebuger = true;
            }
            formInit();
        }


        private void textBox_registerIC_TextChanged(object sender, EventArgs e)
        {
            if (textBox_registerIC.Text.Length == 4)
            {
                proc.IC.writeAt(0, (byte)textBox_registerIC.Text[0]);
                proc.IC.writeAt(1, (byte)textBox_registerIC.Text[1]);
                proc.IC.writeAt(2, (byte)textBox_registerIC.Text[2]);
                proc.IC.writeAt(3, (byte)textBox_registerIC.Text[3]);
            }

        }

        private void textBox_registerPTR_TextChanged(object sender, EventArgs e)
        {
            if (textBox_registerPTR.Text.Length == 4)
            {
                proc.PTR.writeAt(0, (byte)textBox_registerPTR.Text[0]);
                proc.PTR.writeAt(1, (byte)textBox_registerPTR.Text[1]);
                proc.PTR.writeAt(2, (byte)textBox_registerPTR.Text[2]);
                proc.PTR.writeAt(3, (byte)textBox_registerPTR.Text[3]);
            }

        }

        private void textBox_registerSP_TextChanged(object sender, EventArgs e)
        {
            if (textBox_registerSP.Text.Length == 4)
            {
                proc.SP.writeAt(0, (byte)textBox_registerSP.Text[0]);
                proc.SP.writeAt(1, (byte)textBox_registerSP.Text[1]);
                proc.SP.writeAt(2, (byte)textBox_registerSP.Text[2]);
                proc.SP.writeAt(3, (byte)textBox_registerSP.Text[3]);
            }

        }

        private void textBox_registerIOI_TextChanged(object sender, EventArgs e)
        {
            if (textBox_registerIOI.Text.Length == 1)
                proc.IOI = (byte)((textBox_registerIOI.Text[0] == '0') ? 0 : ((textBox_registerIOI.Text[0] == '1') ? 1: 2));

        }

        private void textBox_registerSI_TextChanged(object sender, EventArgs e)
        {
            if (textBox_registerSI.Text.Length == 1)
                proc.SI = (byte)((textBox_registerSI.Text[0] == '0') ? 0 : ((textBox_registerSI.Text[0] == '1') ? 1 : ((textBox_registerSI.Text[0] == '2') ? 2 : ((textBox_registerSI.Text[0] == '3')? 3: 4))));

        }

        private void textBox_registerTI_TextChanged(object sender, EventArgs e)
        {
            if (textBox_registerTI.Text.Length == 1)
                proc.TI = (byte)((textBox_registerTI.Text[0] == '0') ? 0 : 1);

        }

        private void textBox_registerPI_TextChanged(object sender, EventArgs e)
        {
            if (textBox_registerPI.Text.Length == 1)
                proc.PI = (byte)((textBox_registerPI.Text[0] == '0') ? 0 : ((textBox_registerPI.Text[0] == '1') ? 1 : ((textBox_registerPI.Text[0] == '2') ? 2 : ((textBox_registerPI.Text[0] == '3') ? 3 : 4))));

        }

        private void textBox_registerMODE_TextChanged(object sender, EventArgs e)
        {
            if (textBox_registerMODE.Text.Length == 1)
                proc.MODE = (byte)((textBox_registerMODE.Text[0] == '0') ? 0 : 1);

        }

        private void textBox_registerCH1_TextChanged(object sender, EventArgs e)
        {
            if (textBox_registerCH1.Text.Length == 1)
                proc.CH1 = (byte)((textBox_registerCH1.Text[0] == '0') ? 0 : 1);

        }

        private void textBox_registerCH2_TextChanged(object sender, EventArgs e)
        {
            if (textBox_registerCH2.Text.Length == 1)
                proc.CH2 = (byte)((textBox_registerCH2.Text[0] == '0') ? 0 : 1);

        }

        private void textBox_registerCH3_TextChanged(object sender, EventArgs e)
        {
            if (textBox_registerCH3.Text.Length == 1)
                proc.CH3 = (byte)((textBox_registerCH3.Text[0] == '0') ? 0 : 1);

        }

        private void Form_OS_FormClosed(object sender, FormClosedEventArgs e)
        {
            os.createResource("Shutdown", null);
            ElementList elem = new ElementList();
            elem.add(new ResourceElement());
            os.freeResouce("Shutdown", elem);
            th.Abort();
            WinConsole.Visible = false;
            proc.terminateHLP();
            Win32.FreeConsole();
            Application.Exit();
        }

        private void textBox_blockAddress_TextChanged(object sender, EventArgs e)
        {
        }


        private byte toPusB(char c)
        {
            switch (c)
            {
                case '1': return 1;
                case '0': return 0;
                case '2': return 2;
                case '3': return 3;
                case '4': return 4;
                case '5': return 5;
                case '6': return 6;
                case '7': return 7;
                case '8': return 8;
                case '9': return 9;
                case 'A': return 10;
                case 'B': return 11;
                case 'C': return 12;
                case 'D': return 13;
                case 'E': return 14;
                case 'F': return 15;
                default: return 16;
            }
        }

        private int enteredTOInt(string s)
        {
            int value = 0;
            for (int i = 0; i < s.Length; i++)
            {
                byte b = toPusB(s[i]);
                if ((b < 16) && (value >= 0))
                {
                    value = value * 16 + b;
                }
                else
                {
                    value = -1;
                }
            }
            return value;
        }


        char toC(int b)
        {
            switch (b)
            {
                case 0: return '0';
                case 1: return '1';
                case 2: return '2';
                case 3: return '3';
                case 4: return '4';
                case 5: return '5';
                case 6: return '6';
                case 7: return '7';
                case 8: return '8';
                case 9: return '9';
                case 10: return 'A';
                case 11: return 'B';
                case 12: return 'C';
                case 13: return 'D';
                case 14: return 'E';
                case 15: return 'F';
                default: return '0';
            }
        }

        public string toS(int value)
        {
            string returnS = "";
            while (value > 0)
            {
                returnS = "" + toC(value % 16) + returnS;
                value = value / 16;
            }
            while (returnS.Length < 4)
            {
                returnS = "0" + returnS;
            }
            return returnS;
        }

        private void textBox_blockAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int newValue = 0;
                newValue = enteredTOInt(textBox_blockAddress.Text);
                if (newValue < 0)
                {
                    newValue = lookingInRam;
                }
                string newS = toS(newValue);
                textBox_blockAddress.Text = newS;
                if (newValue != lookingInRam)
                {
                    proc.user_memory.DebuggingMem = newValue;
                    lookingInRam = newValue;
                    proc.supervisor_memory.DebuggingMem = newValue;
                    listBox_memory.Items.Clear();

                    if (radioButton_userMemory.Checked)
                    {
                        lookingInSupervisor = false;
                        for (int i = 0; i <= 255; i++)
                        {
                            listBox_memory.Items.Add((object)(proc.user_memory[newValue].getWordAt(i).toString()));
                        }
                    }
                    else
                    {
                        lookingInSupervisor = true;
                        for (int i = 0; i <= 255; i++)
                        {
                            listBox_memory.Items.Add((object)(proc.supervisor_memory[newValue].getWordAt(i).toString()));
                        }
                    }

                }

            }
        }
    }

    public class Win32
    {
        [DllImport("kernel32.dll")]
        public static extern Boolean AllocConsole();
        [DllImport("kernel32.dll")]
        public static extern Boolean FreeConsole();
    }
}
