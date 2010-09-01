using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Kartuves
{
    public partial class Form1 : Form
    {
        volatile Image[] pics;
        public volatile string realWord;
        public volatile string realWordOpponent;
        string oldWord = "";
        int speti;
        volatile int minus = 0;
        volatile int minus_op = 0;
        volatile Network objNetwork;
        volatile public bool wNetworkPlay = false;
        volatile int wNumberPlayers = 1;
        volatile bool wRestart = false;
        volatile string wServerIP = "";
        public volatile bool winner = false;
        public volatile bool loser = false;

        //_____________________________________________________________________________________________
        //
        // It indicates if it was a client or server turned on for network play
        //_____________________________________________________________________________________________

        volatile public bool wServer = false;

        volatile public bool wClient = false;

        public Form1()
        {
            pics = new Image[7];
            //pics[0] = new Butto
            pics[0] = Image.FromFile("p1.jpg");
            pics[1] = Image.FromFile("p2.jpg");
            pics[2] = Image.FromFile("p3.jpg");
            pics[3] = Image.FromFile("p4.jpg");
            pics[4] = Image.FromFile("p5.jpg");
            pics[5] = Image.FromFile("p6.jpg");
            pics[6] = Image.FromFile("p7.jpg");
            
            InitializeComponent();
            pictureBox1.Image = pics[0];
            pictureBox2.Image = pics[0];
            realWord = "Labas";
            generateWordArea(5);
            isThisLetter('a');
            objNetwork = new Network(this);
        }

        public void generateWordArea(int size)
        {
            size = realWord.Length;
            if (30 * size > 240)
            {
                this.Width = 30 * size;
                wordsArea.Width = 30 * size;
            }
            //wordArea.Controls.
            wordsArea.Controls.Clear();
            for (int i = 0; i < size; i++)
            {
                TextBox tmp = new TextBox();
                tmp.Width = 20;
                tmp.Height = 10;
                //tmp.Enabled = false;
                tmp.ReadOnly = true;
                tmp.TextChanged += new EventHandler(tmp_TextChanged);
                wordsArea.Controls.Add(tmp);
            }
        }


        bool isThisLetter(char letter)
        {
            bool isOk = false;
            for (int i = 0; i < realWord.Length; i++)
            {
                if (realWord[i] == letter)
                {
                    isOk = true;
                    ((TextBox)wordsArea.Controls[i]).Text = "" + letter;
                    speti--;
                }
            }
            return isOk;
        }

        void tmp_TextChanged(object sender, EventArgs e)
        {
            int i = wordsArea.Controls.IndexOf((Control)sender);

           // throw new NotImplementedException();
        }
        
        private void naujasToolStripMenuItem_Click(object sender, EventArgs e)
        {

            wServer = true;
            objNetwork.StartServer();
            SetStatusMessage("Waiting for connection...");
            naujasToolStripMenuItem.Enabled = false;
            connectToolStripMenuItem.Enabled = false;
            disconnectToolStripMenuItem.Enabled = true;
            DialogResult wDialog = InputBox.ShowInputBox("Enter word, that client user has to guess?", "Word", realWordOpponent, ref realWordOpponent);
            while (wDialog == DialogResult.Cancel)
            {
                wDialog = InputBox.ShowInputBox("Enter word, that client user has to guess?", "Word", realWordOpponent, ref realWordOpponent);
            }
            //objNetwork.SendWord(realWordOpponent);

            
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        public void RestartGame()
        {
            wRestart = true;
            timer1.Enabled = true;
        }

        public void NewGame()
        {
            if (wClient)
            {
                DialogResult wDialog = InputBox.ShowInputBox("Enter word, that server user has to guess?", "Word", realWordOpponent, ref realWordOpponent);
                while (wDialog == DialogResult.Cancel)
                {
                    wDialog = InputBox.ShowInputBox("Enter word, that server user has to guess?", "Word", realWordOpponent, ref realWordOpponent);                    
                }
                objNetwork.SendWord(realWordOpponent);
            }
            generateWordArea(20);
            label4.Text = "";
            panel1.Visible = false;
            minus = 0;
        }


        public void Regen(string word)
        {
            realWord = word;
            minus = 0;
            generateWordArea(20);
        }

        public void SetStatusMessage(string mes)
        {
            toolStripStatusLabel1.Text = mes;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            textBox1.Text = "";
            if (e.KeyChar >= 'A')
            {

                if (!isThisLetter(e.KeyChar))
                {
                    minus++;
                    if (minus < 7)
                    {
                        pictureBox1.Image = pics[minus];
                        objNetwork.SendMove(minus, ((speti > 0)?1:0));
                    }
                    else
                    {
                        loser = true;
                        objNetwork.SendMove(minus, ((speti > 0) ? 1 : 0));
                        
                    }


                }
                else
                {
                    objNetwork.SendMove(minus, ((speti > 0) ? 1 : 0));
                }
                if (speti <= 0)
                {
                    winner = true;
                }
                
            }
        }

        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult wDialog = InputBox.ShowInputBox("What is the server Name/IP?", "Server name", wServerIP, ref wServerIP);
            if (wDialog == DialogResult.Cancel)
                return;
            wClient = true;
            objNetwork.ConnectServer(wServerIP);
            SetStatusMessage("Connecting...");
            naujasToolStripMenuItem.Enabled = false;
            connectToolStripMenuItem.Enabled = false;
            disconnectToolStripMenuItem.Enabled = true;
        }

        public void makeMove(int points, int speta)
        {
            minus_op = points;
            if (points < 7)
            {
                pictureBox2.Image = pics[minus_op];
            }
            else
            {
                winner = true;
            }
            if (speta <= 0)
            {
                loser = true;
            }

        }

        public void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            objNetwork.Disconnect();
            wServer = false;
            wClient = false;
            naujasToolStripMenuItem.Enabled = true;
            connectToolStripMenuItem.Enabled = true;
            disconnectToolStripMenuItem.Enabled = false;
            
            //RestartGame();
            panel1.Visible =  true;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if ((wServer == true) || (wClient == true))
                objNetwork.Disconnect();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (wRestart)
            {
                wRestart = false;
                NewGame();
            }
            if (oldWord != realWord)
            {
                oldWord = realWord;
                speti = realWord.Length;
                Regen(realWord);
            }
            if (winner)
            {
                label4.Text = "You win!!!!";
                winner = false;
                disconnectToolStripMenuItem_Click(null, null);
            }
            if (loser)
            {
                label4.Text = "You lose!!!!";
                loser = false;
                disconnectToolStripMenuItem_Click(null, null);
            }

        }

        private void baigtiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
