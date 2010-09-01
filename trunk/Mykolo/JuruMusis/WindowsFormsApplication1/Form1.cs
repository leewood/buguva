
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1: Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void newGameToolStripMenuItem_Click_1(object sender, EventArgs e)
        {

           
        }

        void buttonEnemyMap_MouseEnter(object sender, EventArgs e)
        {
            /*richTextBox1.Text = this.panel2.Controls.IndexOf((Panel)sender).ToString();
            if (this.panel2.Controls.IndexOf((Panel)sender) > 0)
            {
                ((Panel)sender).BackColor = Color.Tomato;
            }*/
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
