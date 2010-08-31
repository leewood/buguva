using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FilesManager
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            setImagePath();
            button1_Click(this, new EventArgs());
        }

        string leftSidePath = "/";
        string rightSidePath = "/";
        string winLeftSidePath = "C:\\";
        string winRightSidePath = "C:\\";
        bool useRealDriveLeftSide = false;
        bool useRealDriveRightSide = true;
        FileSystem fs = new FileSystem(HDDConfig.Default["HDDPath"].ToString());


        public void setImagePath()
        {
            if (System.IO.File.Exists("config"))
            {
                System.IO.FileStream config = new System.IO.FileStream("config", System.IO.FileMode.Open, System.IO.FileAccess.Read);
                System.IO.StreamReader reader = new System.IO.StreamReader(config);
                string name = reader.ReadLine();
                fs = new FileSystem(name);
            }
            else
            {
                fs = new FileSystem(HDDConfig.Default["HDDPath"].ToString());
            }
        }

        public void loadDrives(ComboBox drives)
        {
            drives.Items.Clear();
            drives.Items.Add("Drive image");
            
            string[] DriveList = Environment.GetLogicalDrives();
            for (int i = 0; i < DriveList.Length; i++)
            {

                drives.Items.Add(DriveList[i]);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            loadDrives(leftSideDrive);
            loadDrives(rightSideDrive);
            leftSideDrive.SelectedIndex = 0;
            rightSideDrive.SelectedItem = "C:\\";
            goToDir("", leftSide);
            goToDir("", rightSide);
            leftSide.Focus();
            if (leftSide.SelectedIndices.Count == 0)
            {
                leftSide.SelectedIndices.Add(0);
            }
        }

        private void paintRealDir(string root, ListView listView)
        {
            try
            {
                ListViewItem lvi;
                ListViewItem.ListViewSubItem lvsi;
                

                System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(root);

                System.IO.DirectoryInfo[] dirs = dir.GetDirectories();
                System.IO.FileInfo[] files = dir.GetFiles();

                listView.Items.Clear();

                listView.BeginUpdate();

                lvi = new ListViewItem();
                lvi.Text = "..";
                lvi.ImageIndex = 0;
                lvi.Tag = "Back";

                lvsi = new ListViewItem.ListViewSubItem();
                lvsi.Text = "Folder";
                lvi.SubItems.Add(lvsi);

                lvsi = new ListViewItem.ListViewSubItem();
                //lvsi.Text = di.LastAccessTime.ToString();
                //lvi.SubItems.Add(lvsi);

                listView.Items.Add(lvi);


                foreach (System.IO.DirectoryInfo di in dirs)
                {
                    lvi = new ListViewItem();
                    lvi.Text = di.Name;
                    lvi.ImageIndex = 0;
                    lvi.Tag = di.FullName;

                    lvsi = new ListViewItem.ListViewSubItem();
                    lvsi.Text = "Folder";
                    lvi.SubItems.Add(lvsi);

                    lvsi = new ListViewItem.ListViewSubItem();
                    //lvsi.Text = di.LastAccessTime.ToString();
                    //lvi.SubItems.Add(lvsi);

                    listView.Items.Add(lvi);
                }
                listView.EndUpdate();
                foreach (System.IO.FileInfo fi in files)
                {
                    lvi = new ListViewItem();
                    lvi.Text = fi.Name;
                    lvi.ImageIndex = 1;
                    lvi.Tag = fi.FullName;

                    lvsi = new ListViewItem.ListViewSubItem();
                    lvsi.Text = "File";
                    lvi.SubItems.Add(lvsi);

                    lvsi = new ListViewItem.ListViewSubItem();
                    //lvsi.Text = fi.LastAccessTime.ToString();
                    //lvi.SubItems.Add(lvsi);

                    listView.Items.Add(lvi);

                }
            }
            catch (System.Exception err)
            {
                MessageBox.Show("Error: " + err.Message);
            }
            
        }



        public void repaintDir(ListView side)
        {
            if (side.Name == "leftSide")
            {
                if (useRealDriveLeftSide)
                {
                    paintRealDir(winLeftSidePath, side);
                }
                else
                {
                    paintDir(leftSidePath, side);
                }
            }
            else
            {
                if (useRealDriveRightSide)
                {
                    paintRealDir(winRightSidePath, side);
                }
                else
                {
                    paintDir(rightSidePath, side);
                }

            }
        }


        void goToDir(string dirName, ListView side)
        {
            bool useReal = (side.Name == "leftSide") ? useRealDriveLeftSide : useRealDriveRightSide;
            string winPath = (side.Name == "leftSide") ? winLeftSidePath : winRightSidePath;
            string path = (side.Name == "leftSide") ? leftSidePath : rightSidePath;

            if (dirName == "..")
            {
                if (useReal)
                {
                    int index = winPath.LastIndexOf("\\");
                    if (index > 2)
                    {
                        winPath = winPath.Substring(0, index);

                    }
                    else
                    {
                        winPath = winPath.Substring(0, 3);
                    }
                }
                else
                {
                    int index = path.LastIndexOf("/");
                    if (index > 0)
                    {
                        path = path.Substring(0, index);

                    }
                    else
                    {
                        path = "/";
                    }

                }
            }
            else
            {
                if (useReal)
                {

                    if (winPath.Length == 3)
                    {
                        winPath += dirName;
                    }
                    else
                    {
                        winPath += "\\" + dirName;
                    }
                }
                else
                {
                    int copyPlace = dirName.IndexOf(" ");
                    if (copyPlace < 0)
                    {
                        copyPlace = dirName.Length;
                    }
                    dirName = dirName.Substring(0, copyPlace);
                    if (path[path.Length - 1] != '/')
                    {
                        path = path + "/" + dirName;
                    }
                    else
                    {
                        path += dirName;
                    }
                }
            }

            if (useReal)
            {
                paintRealDir(winPath, side);

            }
            else
            {
                paintDir(path, side);
            }

            if (side.Name == "leftSide")
            {
                winLeftSidePath = winPath;
                leftSidePath = path;
            }
            else
            {
                winRightSidePath = winPath;
                rightSidePath = path;
            }
            side.SelectedIndices.Clear();
            side.SelectedIndices.Add(0);
        }

        private void paintDir(string path, ListView listView)
        {


            
            // fs.copyToFile(1, 4, "c:\\result");
            //fs.copyFile("/shell.asm", "c:\\result2");
            //fs.copyFileExternal("c:\\result2", "/usr/shell.asm");
            listView.Items.Clear();
            string toAdd = path + (((path == "/") || (path[path.Length - 1] == '/')) ? "" : "/");
            List<string> list = fs.listFiles(path);
            list.Sort();
            List<string> filesList = new List<string>();
            foreach (string name in list)
            {
                if (name != "..")
                {
                    if (fs.isDir(toAdd + name))
                    {
                        ListViewItem item = new ListViewItem();
                        item.Text = name;
                        item.ImageIndex = 0;
                        ListViewItem.ListViewSubItem subitem = new ListViewItem.ListViewSubItem(item, "Folder");
                        item.SubItems.Add(subitem);
                        listView.Items.Add(item);
                        
                    }
                    else
                    {
                        filesList.Add(name);
                    }
                }
                else
                {
                    ListViewItem item = new ListViewItem();
                    item.Text = name;
                    item.ImageIndex = 0;
                    ListViewItem.ListViewSubItem subitem = new ListViewItem.ListViewSubItem(item, "Folder");
                    item.SubItems.Add(subitem);
                    listView.Items.Add(item);
                    

                }
            }
            filesList.Sort();
            foreach (string fileName in filesList)
            {
                ListViewItem item = new ListViewItem();
                item.Text = fileName;
                item.ImageIndex = 1;
                ListViewItem.ListViewSubItem subitem = new ListViewItem.ListViewSubItem(item, "File");
                item.SubItems.Add(subitem);
                listView.Items.Add(item);
            }
            listView.SelectedIndices.Clear();
            listView.SelectedIndices.Add(0);

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView1_ItemActivate(object sender, EventArgs e)
        {
            ListView listView1 = (ListView)sender;
            string name = listView1.SelectedItems[0].Text;
            goToDir(name, listView1);
        }

        private void leftSideDrive_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox place = (ComboBox)sender;
            if (place.Name == "leftSideDrive")
            {
                if (place.SelectedIndex == 0)
                {
                    useRealDriveLeftSide = false;
                    goToDir("", leftSide);
                }
                else
                {
                    useRealDriveLeftSide = true;
                    winLeftSidePath = place.SelectedItem.ToString();
                    goToDir("", leftSide);
                }
            }
            else
            {
                if (place.SelectedIndex == 0)
                {
                    useRealDriveRightSide = false;
                    goToDir("", rightSide);
                }
                else
                {
                    useRealDriveRightSide = true;
                    winRightSidePath = place.SelectedItem.ToString();
                    goToDir("", rightSide);
                }
            }
        }

        private void leftSide_KeyDown(object sender, KeyEventArgs e)
        {
            ListView side = (ListView)sender;
            if (e.KeyCode == Keys.F5)
            {

                if (side.Name == "leftSide")
                {
                    if ((!useRealDriveLeftSide) && (useRealDriveRightSide))
                    {
                        string source = "";
                        if (leftSidePath == "/")
                        {
                            source = leftSidePath + leftSide.SelectedItems[0].Text;
                        }
                        else
                        {
                            source = leftSidePath + "/" + leftSide.SelectedItems[0].Text;
                        }
                        fs.copyFileFromImagToRealHard(source, winRightSidePath);
                    }
                    else if ((!useRealDriveRightSide) && (useRealDriveLeftSide))
                    {
                        string source = winLeftSidePath + ((winLeftSidePath.Length == 3) ? "" : "\\") + leftSide.SelectedItems[0].Text;
                        fs.copyFromRealHardToImage(source, rightSidePath);
                    }
                    goToDir("", rightSide);
                }
                else
                {
                    if ((useRealDriveLeftSide) && (!useRealDriveRightSide))
                    {
                        string source = "";
                        if (rightSidePath == "/")
                        {
                            source = rightSidePath + rightSide.SelectedItems[0].Text;
                        }
                        else
                        {
                            source = rightSidePath + "/" + rightSide.SelectedItems[0].Text;
                        }
                        fs.copyFileFromImagToRealHard(source, winLeftSidePath);
                    }
                    else if ((!useRealDriveLeftSide) && (useRealDriveRightSide))
                    {
                        string source = winRightSidePath + ((winRightSidePath.Length == 3) ? "" : "\\") + rightSide.SelectedItems[0].Text;
                        fs.copyFromRealHardToImage(source, leftSidePath);
                    }
                    goToDir("", leftSide);
                }
            }
            else if (e.KeyCode == Keys.F8)
            {
                if (side.Name == "leftSide")
                {
                    if (!useRealDriveLeftSide)
                    {
                        fs.fullDirRemove(fs.fileInDir(leftSidePath, side.SelectedItems[0].Text));
                        goToDir("", leftSide);
                    }
                }
                else
                {
                    if (!useRealDriveRightSide)
                    {
                        fs.fullDirRemove(fs.fileInDir(rightSidePath, side.SelectedItems[0].Text));
                        goToDir("", rightSide);
                    }
                }
            }
        }

        private void rightSide_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {

        }
    }
}
