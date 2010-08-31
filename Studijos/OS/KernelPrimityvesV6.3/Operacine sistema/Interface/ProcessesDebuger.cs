using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ConsoleApplication1;

namespace Operacine_sistema
{
    public partial class ProcessesDebuger : Form
    {
        private Kernel kernel; 

        public ProcessesDebuger(Kernel kernel, Form parent)
        {
            InitializeComponent();
           
            this.kernel = kernel;
            this.Left = parent.Left + parent.Width;
            this.Top = 0;

        }


        private void generateProcessesList()
        {
            int index = listBox1.SelectedIndex;
            object selObj = listBox1.SelectedItem;
            listBox1.Items.Clear();
            for (int i = 0; i < kernel.processList.Count; i++)
            {
                Process proc = kernel.processList.getByPos(i);
                listBox1.Items.Add(proc);
            }
            if ((selObj != null) && (listBox1.Items.IndexOf(selObj) >= 0))
            {
                listBox1.SelectedItem = selObj;
            }
            else
            {
                if ((listBox1.Items.Count < index) && (index >= 0))
                {
                    listBox1.SelectedIndex = index;
                }
                else
                {
                    if (listBox1.Items.Count > 0)
                    {
                        listBox1.SelectedIndex = 0;
                    }
                }
            }
        }

        private void generateOwnedResources(int i)
        {
            listBox2.Items.Clear();
            ElementList elems = kernel.processList.getByPos(i).ownedResources;
            for (int j = 0; j < elems.Count; j++)
            {
                listBox2.Items.Add(elems[j]);
            }
        }

        private void generateBlockedOn(int i)
        {
            Process proc = kernel.processList.getByPos(i);
            listBox3.Items.Clear();
            if ((proc.processState == State.blocked) || (proc.processState == State.blockedSuspended))
            {
                Resource res = proc.processList.resourceOwner;
                if ((res != null) && (res.waitingProcessList != null))
                {
                    int pos = res.waitingProcessList.getPosByName(proc.name);
                    ElementList elems = res.waitingParts[pos];
                    for (int j = 0; j < elems.Count; j++)
                    {
                        listBox3.Items.Add(elems[j]);
                    }
                }
            }
        }

        public void destroyResourceElement(Process sender, ResourceElement elems)
        {
            if ((sender != null) && (sender.ownedResources != null))
            {
                sender.ownedResources.removeElement(elems);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (kernel.procChanged)
            {
                generateProcessesList();
                kernel.procChanged = false;
            }
            propertyGrid1.Update();
            //generateOwnedResources();
            //generateBlockedOn();
        }

        private void listBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            propertyGrid1.SelectedObject = kernel.processList.getByPos(listBox1.SelectedIndex);
            generateBlockedOn(listBox1.SelectedIndex);
            generateOwnedResources(listBox1.SelectedIndex);
        }

        private void listBox3_SelectedValueChanged(object sender, EventArgs e)
        {
            propertyGrid1.SelectedObject = listBox3.SelectedItem;

        }

        private void listBox2_SelectedValueChanged(object sender, EventArgs e)
        {
            propertyGrid1.SelectedObject = listBox2.SelectedItem;
        }

        private void listBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (listBox1.IndexFromPoint(e.X, e.Y) >= 0)
                {
                    listBox1.SelectedIndex = listBox1.IndexFromPoint(e.X, e.Y);
                    newProcessToolStripMenuItem.Visible = true;
                    stopProcessToolStripMenuItem.Visible = true;
                    suspendProcessToolStripMenuItem.Visible = true;
                    resumeProcessToolStripMenuItem.Visible = true;
                    changePriorityToolStripMenuItem.Visible = true;
                }
                else
                {
                    newProcessToolStripMenuItem.Visible = true;
                    stopProcessToolStripMenuItem.Visible = false;
                    suspendProcessToolStripMenuItem.Visible = false;
                    resumeProcessToolStripMenuItem.Visible = false;
                    changePriorityToolStripMenuItem.Visible = false;
                }
            }
        }

        private void suspendProcessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string name = kernel.processList.getByPos(listBox1.SelectedIndex).name;
            kernel.suspendProcess(name);
            
        }

        private void resumeProcessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string name = kernel.processList.getByPos(listBox1.SelectedIndex).name;
            kernel.activateProcess(name);

        }

        private void stopProcessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string name = kernel.processList.getByPos(listBox1.SelectedIndex).name;
            kernel.destroyProcess(name);
        }

        private void newProcessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewDialog newDial = new NewDialog("Start new process", "Enter process name:");
            if (newDial.ShowDialog() == DialogResult.OK)
            {
                kernel.createProcess(null, null, 4, null, newDial.FormResult);
            }
        }

        private void changePriorityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process proc = kernel.processList.getByPos(listBox1.SelectedIndex);
            ChangePriority chPrior = new ChangePriority(proc.priority);
            if (chPrior.ShowDialog() == DialogResult.OK)
            {
                kernel.changePriority(proc.name, chPrior.Value);
                kernel.procChanged = true;
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

    }
}
