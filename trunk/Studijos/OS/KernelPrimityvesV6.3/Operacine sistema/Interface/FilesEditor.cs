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
    public partial class Form_FilesEditor : Form
    {
        private Dialog loadSaveDialog;
        private ConsoleApplication1.Processor proc;

        public Form_FilesEditor( ConsoleApplication1.Processor proc )
        {
            InitializeComponent();

            this.proc = proc;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Visible = false;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox_filesEditor.Text = "";
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadSaveDialog = new Dialog(false, proc, this);

            loadSaveDialog.Visible = true;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadSaveDialog = new Dialog(true, proc, this);

            loadSaveDialog.Visible = true;
        }
    }
}
