using System;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;


namespace WindowsFormsApplication1
{
    public class Client
    {
        byte[] m_dataBuffer = new byte[10];
        IAsyncResult m_result;
        public AsyncCallback m_pfnCallBack;
        public Socket m_clientSocket;

        private RichTextBox textBoxIP = new RichTextBox();
        private RichTextBox mainTextBox = new RichTextBox();
        private Panel enemyPanel = new Panel();
        private Panel removePanel = new Panel();
        private Button redrawButton = new Button();

        public void setRedrawButton(Button p_mainButton)
        {
            redrawButton = p_mainButton;
        }

        public void setRemovePanel(Panel p_mainPanel)
        {
            removePanel = p_mainPanel;
        }

        public void setEnemyPanel(Panel p_mainPanel)
        {
            enemyPanel = p_mainPanel;
        }

        public void setTextBoxIP(RichTextBox p_txtBoxMain)
        {
            textBoxIP = p_txtBoxMain;
        }

        public void setMainTextBox(RichTextBox p_txtBoxMain)
        {
            mainTextBox = p_txtBoxMain;
        }

        public Client()
        {
            textBoxIP.Text = GetIP();
        }

        void ButtonCloseClick(object sender, System.EventArgs e)
        {
            if (m_clientSocket != null)
            {
                m_clientSocket.Close();
                m_clientSocket = null;
            }
        }

        public void connect(string p_strIP, string p_strPort)
        {
            if (p_strIP == "" || p_strPort == "")
            {
                MessageBox.Show("IP Address and Port Number are required to connect to the Server\n");
                return;
            }
            try
            {
                m_clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                IPAddress ip = IPAddress.Parse(p_strIP);
                int iPortNo = System.Convert.ToInt16(p_strPort);
                IPEndPoint ipEnd = new IPEndPoint(ip, iPortNo);
                m_clientSocket.Connect(ipEnd);
                if (m_clientSocket.Connected)
                {
                    WaitForData();
                }
            }
            catch (SocketException se)
            {
                string str;
                str = "\nConnection failed, is the server running?\n" + se.Message;
                MessageBox.Show(str);
            }
        }

        public void sendMessage(byte p_intMessage)
        {
            string message;

            message = ((char)p_intMessage).ToString();
            try
            {
                Object objData = message;
                byte[] byData = System.Text.Encoding.ASCII.GetBytes(objData.ToString());
                if (m_clientSocket != null)
                {
                    m_clientSocket.Send(byData);
                }
            }
            catch (SocketException se)
            {
                MessageBox.Show(se.Message);
            }
        }

        public void WaitForData()
        {
            try
            {
                if (m_pfnCallBack == null)
                {
                    m_pfnCallBack = new AsyncCallback(OnDataReceived);
                }
                SocketPacket theSocPkt = new SocketPacket();
                theSocPkt.thisSocket = m_clientSocket;
                m_result = m_clientSocket.BeginReceive(theSocPkt.dataBuffer,
                                                        0, theSocPkt.dataBuffer.Length,
                                                        SocketFlags.None,
                                                        m_pfnCallBack,
                                                        theSocPkt);
            }
            catch (SocketException se)
            {
                MessageBox.Show(se.Message);
            }

        }
        public class SocketPacket
        {
            public System.Net.Sockets.Socket thisSocket;
            public byte[] dataBuffer = new byte[1];
        }

        public void OnDataReceived(IAsyncResult asyn)
        {
            try
            {
                SocketPacket theSockId = (SocketPacket)asyn.AsyncState;

                string message;
                char first;
                int iRx = theSockId.thisSocket.EndReceive(asyn);
                char[] chars = new char[iRx + 1];
                System.Text.Decoder d = System.Text.Encoding.UTF8.GetDecoder();
                int charLen = d.GetChars(theSockId.dataBuffer, 0, iRx, chars, 0);
                System.String szData = new System.String(chars);

                message = szData.ToString();
                first = message[0];

                if (first == 'd')
                {
                    PanelUtils.setOtherPlayerReady();
                    mainTextBox.Text = "Priesininkas Pasiruoses";
                }
                if (first == 'e')
                {
                    PanelUtils.setOtherPlayerNotRead();
                    mainTextBox.Text = "Priesininkas Nebepasiruoses";
                }
                if (first == 'f')
                    //mainTextBox.Text = "Priesininkas Pasiruoses";
                    redrawButton.PerformClick();
                    //PanelUtils.RedrawToBattleField(enemyPanel, removePanel);
                WaitForData();
            }
            catch (ObjectDisposedException)
            {
                System.Diagnostics.Debugger.Log(0, "1", "\nOnDataReceived: Socket has been closed\n");
            }
            catch (SocketException se)
            {
                MessageBox.Show(se.Message);
            }
        }

       
        void ButtonDisconnectClick(object sender, System.EventArgs e)
        {
            if (m_clientSocket != null)
            {
                m_clientSocket.Close();
                m_clientSocket = null;
            }
        }

        String GetIP()
        {
            /*String strHostName = Dns.GetHostName();

            IPHostEntry iphostentry = Dns.GetHostEntry(strHostName);

            String IPStr = "";
            foreach (IPAddress ipaddress in iphostentry.AddressList)
            {
                IPStr = ipaddress.ToString();
                return IPStr;
            }
            return IPStr;*/
            return "aa";
        }
    }
}
