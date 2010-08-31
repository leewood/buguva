using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace Operacine_sistema
{
    public partial class Dialog : Form
    {
        private Boolean mode;  // true - SAVE, false - LOAD
        private ConsoleApplication1.HardDiskDriveUtils hddu = new ConsoleApplication1.HardDiskDriveUtils();
        private ConsoleApplication1.Processor proc;
        private Form_FilesEditor fe;
        private String s = "";

        public Dialog( Boolean mode, ConsoleApplication1.Processor proc, Form_FilesEditor fe )
        {
            InitializeComponent();

            this.mode = mode;
            this.proc = proc;
            this.fe = fe;
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            Visible = false;
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            if (mode)
            {
                ConsoleApplication1.FileDescriptor fd = hddu.createFile(textBox_fileName.Text, proc.hdd);
                int i = 0;
                s = fe.textBox_filesEditor.Text;
                while (i < s.Length)
                {
                    if (s.IndexOf("$ASM", i) == i)
                    {
                        i += 24;
                    }
                    else if (s.IndexOf("$END", i) == i)
                    {
                        i += 4;
                    }
                    else if (s[i] == '$')
                    {
                        hddu.writeByte(proc.hdd, fd, (byte)s[i]);
                        hddu.writeByte(proc.hdd, fd, (byte)s[i + 1]);
                        hddu.writeByte(proc.hdd, fd, (byte)s[i + 2]);
                        i += 3;
                        if ((s[i] == '\n') || (s[i] == '\r'))
                        {
                            i += 2;
                        }
                    }
                    else
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            if ((s[i] != '\n') && (s[i] != '\r'))
                            {
                                hddu.writeByte(proc.hdd, fd, (byte)s[i]);
                                i++;
                            }
                            else
                            {
                                hddu.writeByte(proc.hdd, fd, (byte)' ');
                            }
                        }
                        if ((s[i] == '\n') || (s[i] == '\r'))
                        {
                            i += 2;
                        }
                    }
                }
                hddu.closeFile(proc.hdd, fd);
            }
            else
            {
                ConsoleApplication1.FileDescriptor fd = hddu.createFile(textBox_fileName.Text, proc.hdd);
                Boolean end = false;
                while (!end)
                {
                    byte b = hddu.readByte(proc.hdd, fd);
                    s += (char)b;
                    if (s.IndexOf("$END") >= 0)
                        end = true;
                }

                //fe.textBox_filesEditor.Text = s.Substring(0, 24);
                //fe.textBox_filesEditor.Text += "\n";

                int i = 0;
                fe.textBox_filesEditor.Text = "";
                while (!(s.IndexOf("$END", i) == i) && (i < s.Length))
                {
                    if (s[i] == '$')
                    {
                        if (s.IndexOf("$END", i) == i)
                        {
                            fe.textBox_filesEditor.Text += s.Substring(i, 4);
                            fe.textBox_filesEditor.Text += "\r\n";
                            i += 4;
                        }
                        else
                        {
                            fe.textBox_filesEditor.Text += s.Substring(i, 3);
                            fe.textBox_filesEditor.Text += "\r\n";
                            i += 3;
                        }
                    }
                    else
                    {
                        fe.textBox_filesEditor.Text += s.Substring(i, 8);
                        fe.textBox_filesEditor.Text += "\r\n";
                        i += 8;
                    }
                }
            }
            Visible = false;
        }
    }
}
