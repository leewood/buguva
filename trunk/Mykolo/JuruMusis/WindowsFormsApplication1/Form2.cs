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
    public partial class Form2 : Form
    {
        RichTextBox IPTextBox = new RichTextBox();

        private Client mainClient = new Client();
        private Server mainServer = new Server();
        private bool server = false;
        private bool client = false;

        public void setIPTextBox(RichTextBox p_txtBoxMain)
        {
            IPTextBox = p_txtBoxMain;
        }

        public Form2()
        {
            InitializeComponent();
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (PanelUtils.playerIsReady())
            {
                if (client)
                    mainClient.sendMessage(100);
                if (server)
                    mainServer.sendMessage(100);
            }
            if (PanelUtils.letGameStart())
            {
                PanelUtils.RedrawToBattleField(panel5, panel1);
                if (client)
                    mainClient.sendMessage(102);
                if (server)
                    mainServer.sendMessage(102);
            }
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            PanelUtils.reverseShip();
            RedrawToBattleField();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PanelUtils.cancelShips();
            if (client)
                mainClient.sendMessage(101);
            if (server)
                mainServer.sendMessage(101);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            PanelUtils.removeGreetingPage();

            mainClient.setRedrawButton(button1);
            mainClient.setRemovePanel(panel1);
            mainClient.setEnemyPanel(panel5);
            mainClient.setMainTextBox(richTextBox2);
            PanelUtils.setMainClient(mainClient);
            mainClient.connect("192.168.0.178", "8000");
            client = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            PanelUtils.removeGreetingPage();

            mainServer.setRedrawButton(button1);
            mainServer.setRemovePanel(panel1);
            mainServer.setMainEnemyPanel(panel5);
            mainServer.setMainTextBox(richTextBox2);
            PanelUtils.setMainServer(mainServer);
            mainServer.startServer("8000");
            server = true;
        }

    }
}
