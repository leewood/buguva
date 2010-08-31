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
    public partial class ResourcesList : Form
    {
        Kernel kernel;
        public ResourcesList(Kernel kernel, Form parent)
        {
            this.kernel = kernel;
            
            InitializeComponent();
            this.Left = parent.Left + parent.Width;
            this.Top = 0;
        }

        TreeNode selNode = null;
        private void generateTree()
        {
           
            treeView1.Nodes.Clear();
            for (int i = 0; i < kernel.resourceList.Count; i++)
            {
                TreeNode node = treeView1.Nodes.Add(kernel.resourceList[i].name);
                node.Name = "Resource_" + kernel.resourceList[i].name;
                for (int j = 0; j < kernel.resourceList[i].elementList.Count; j++)
                {
                    var elem = kernel.resourceList[i].elementList[j];
                    var name = "element" + j.ToString();
                    var childName = "Element" + j.ToString() + "_" + i.ToString();
                    if (elem.HasName)
                    {
                        name = elem.ToString();
                    }
                    TreeNode child = node.Nodes.Add(name);
                    child.Name = childName;
                }
            }
            if (selNode != null)
            {
                TreeNode[] nodes = treeView1.Nodes.Find(selNode.Name, true);
                if (nodes.Length > 0)
                {
                    treeView1.SelectedNode = nodes[0];
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (kernel.resChanged)
            {
                generateTree();
                kernel.resChanged = false;
            }
        }

        string selectedResName = "";
        int index = 0;

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Name.IndexOf("Resource") >= 0)
            {
                propertyGrid1.SelectedObject = kernel.getResourcePointer(e.Node.Text);
                selectedResName = e.Node.Text;
            }
            else
            {
                index = e.Node.Index;               
                Resource res = kernel.getResourcePointer(e.Node.Parent.Text);
                selectedResName = res.name;
                //index = int.Parse(e.Node.Text.Substring(7));
                propertyGrid1.SelectedObject = res.elementList[index];
            }
            selNode = treeView1.SelectedNode;
        }


        private string getResNodeName(string resName)
        {
            int i = kernel.getResourceIndex(resName);
            Resource res = kernel.resourceList[i];
            int j = res.elementList.Count - 1;
            return "Element" + j.ToString() + "_" + i.ToString();
        }

        private void newElementToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (selectedResName != "")
            {
                NewResourceDialog resDialog = new NewResourceDialog();
                resDialog.SelectedObject = kernel.addResourceElement(selectedResName);
                if (resDialog.ShowDialog() == DialogResult.OK)
                {
                    ElementList elems = new ElementList();
                    elems.add((ResourceElement)resDialog.SelectedObject);
                    kernel.freeResouce(selectedResName, elems);
                    generateTree();
                    TreeNode[] nodes = treeView1.Nodes.Find(getResNodeName(selectedResName), true);
                    if (nodes.Length > 0)
                    {
                        selNode = nodes[0];
                    }
                }
            }
        }

        private void deleteElementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            kernel.getResourcePointer(selectedResName).elementList.remove(index);
            TreeNode[] nodes = treeView1.Nodes.Find("Resource_" + selectedResName, true);
            if (nodes.Length > 0)
            {
                selNode = nodes[0];
            }
            kernel.resChanged = true;
        }

        private void deleteElementToolStripMenuItem_VisibleChanged(object sender, EventArgs e)
        {
        }

        private void contextMenuStrip1_VisibleChanged(object sender, EventArgs e)
        {

        }

        private void treeView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                TreeNode node =  (TreeNode)treeView1.GetNodeAt(e.X, e.Y);
                treeView1.SelectedNode = node;
                if (treeView1.SelectedNode == null)
                {
                    deleteElementToolStripMenuItem.Visible = false;
                    deleteResourceToolStripMenuItem.Visible = false;
                    newElementToolStripMenuItem.Visible = false;
                    newResourceToolStripMenuItem.Visible = true;
                }
                else if (treeView1.SelectedNode.Name.IndexOf("Resource") >= 0)
                {
                    deleteElementToolStripMenuItem.Visible = false;
                    deleteResourceToolStripMenuItem.Visible = true;
                    newElementToolStripMenuItem.Visible = true;
                    newResourceToolStripMenuItem.Visible = true;
                }
                else
                {
                    deleteElementToolStripMenuItem.Visible = true;
                    deleteResourceToolStripMenuItem.Visible = false;
                    newElementToolStripMenuItem.Visible = true;
                    newResourceToolStripMenuItem.Visible = false;

                }

            }
        }

        private void deleteResourceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            kernel.destroyResource(selectedResName);
        }

        private void newResourceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewDialog dialog = new NewDialog("Create new resource", "Resource name:");
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                kernel.createResource(dialog.FormResult, kernel.getProcessPointer("Init"));
            }
        }
    }
}
