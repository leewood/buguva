using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace MainServer
{
    public partial class MainForm : Form
    {
        MainServer server;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            server = new MainServer(logArea);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            server.stop();
            Application.Exit();
        }
    }
}
