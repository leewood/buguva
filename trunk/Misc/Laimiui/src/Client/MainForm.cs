using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace Client
{
    public partial class MainForm : Form
    {
        Client serverConnection;

        public MainForm()
        {
            InitializeComponent();
            //serverConnection = new MineSweeperClient("localHost", 4567);
            System.IO.FileInfo fileInfo = new System.IO.FileInfo("config.ini");
            if (fileInfo.Exists)
            {                
                System.IO.StreamReader fileR = new System.IO.StreamReader("config.ini");
                string server = fileR.ReadLine();
                textBox2.Text = server;

                OnStart();
            }
        }


       public void OnStart()
       {
           string server = textBox2.Text;
           if (connectToServer(server, 4567))
           {
               textBox2.Visible = false;
               label2.Visible = false;
               button2.Visible = false;
               textBox1.Visible = true;
               label1.Visible = true;
               button1.Visible = true;
               useServer("S");
           }
       }

       public void OnStop()
       {
           useServer("E");
       }

       public void OnText()
       {
           if (textBox1.Text != "")
           {
               useServer("T" + textBox1.Text);
               textBox1.Text = "";
           }
       }

        public string useServer(string ats)
        {
            if (serverConnection != null)
            {
                return serverConnection.updateServerTCP(ats);
            }
            else
            {
                return "No connection";
            }
        }




        public bool connectToServer(string serverName, int port)
        {
            serverConnection = new Client(serverName, port);
            string ats = useServer("?");
            if (ats.IndexOf("OK") < ats.Length)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            OnStart();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OnText();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            OnStop();
        }

    }
}
