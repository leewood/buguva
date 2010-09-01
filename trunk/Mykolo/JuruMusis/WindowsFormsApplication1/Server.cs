using System;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace WindowsFormsApplication1
{
    public class Server
    {
        const int MAX_CLIENTS = 10;

        public AsyncCallback pfnWorkerCallBack;
        private Socket m_mainSocket;
        private Socket[] m_workerSocket = new Socket[10];
        private int m_clientCount = 0;

        private RichTextBox mainTextBox = new RichTextBox();
        private RichTextBox textBoxIP = new RichTextBox();
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

        public void setMainTextBox(RichTextBox p_txtBoxMainTextBox)
        {
            mainTextBox = p_txtBoxMainTextBox;
        }

        public void setTextBoxIP(RichTextBox p_txtBoxMainTextBox)
        {
            textBoxIP = p_txtBoxMainTextBox;
        }

        public void setMainEnemyPanel(Panel p_mainPanel)
        {
            enemyPanel = p_mainPanel;
        }

        public Server()
        {
            textBoxIP.Text = GetIP();
        }

        public void startServer(string p_strPortNumber)
        {
            try
            {
                if (p_strPortNumber == "")
                {
                    mainTextBox.Text = "Please enter a Port Number";
                    return;
                }
                int port = System.Convert.ToInt32(p_strPortNumber);
                
                m_mainSocket = new Socket(AddressFamily.InterNetwork,
                                          SocketType.Stream,
                                          ProtocolType.Tcp);
                IPEndPoint ipLocal = new IPEndPoint(IPAddress.Any, port);
                
                m_mainSocket.Bind(ipLocal);                
                m_mainSocket.Listen(4);
                m_mainSocket.BeginAccept(new AsyncCallback(OnClientConnect), null);
            }
            catch (SocketException se)
            {
                mainTextBox.Text = se.Message;
            }

        }

        public void OnClientConnect(IAsyncResult asyn)
        {
            try
            {
                m_workerSocket[m_clientCount] = m_mainSocket.EndAccept(asyn);
                WaitForData(m_workerSocket[m_clientCount]);
                ++m_clientCount;
                String str = String.Format("Client # {0} connected", m_clientCount);
                mainTextBox.Text = str;
                m_mainSocket.BeginAccept(new AsyncCallback(OnClientConnect), null);
            }
            catch (ObjectDisposedException)
            {
                System.Diagnostics.Debugger.Log(0, "1", "\n OnClientConnection: Socket has been closed\n");
            }
            catch (SocketException se)
            {
                MessageBox.Show(se.Message);
            }

        }
        public class SocketPacket
        {
            public System.Net.Sockets.Socket m_currentSocket;
            public byte[] dataBuffer = new byte[1];
        }

        public void WaitForData(System.Net.Sockets.Socket soc)
        {
            try
            {
                if (pfnWorkerCallBack == null)
                {
                    pfnWorkerCallBack = new AsyncCallback(OnDataReceived);
                }
                SocketPacket theSocPkt = new SocketPacket();
                theSocPkt.m_currentSocket = soc;
                soc.BeginReceive(theSocPkt.dataBuffer, 0,
                                   theSocPkt.dataBuffer.Length,
                                   SocketFlags.None,
                                   pfnWorkerCallBack,
                                   theSocPkt);
            }
            catch (SocketException se)
            {
                MessageBox.Show(se.Message);
            }

        }

        public void OnDataReceived(IAsyncResult asyn)
        {
            try
            {
                SocketPacket socketData = (SocketPacket)asyn.AsyncState;

                string message;
                char first;
                int iRx = 0;
                iRx = socketData.m_currentSocket.EndReceive(asyn);
                char[] chars = new char[iRx + 1];
                System.Text.Decoder d = System.Text.Encoding.UTF8.GetDecoder();
                int charLen = d.GetChars(socketData.dataBuffer,
                                         0, iRx, chars, 0);
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
                WaitForData(socketData.m_currentSocket);
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

        public void sendMessage(byte p_intMessage)
        {
            try
            {
                string message;

                message = ((char)p_intMessage).ToString();

                Object objData = message;
                byte[] byData = System.Text.Encoding.ASCII.GetBytes(objData.ToString());
                for (int i = 0; i < m_clientCount; i++)
                {
                    if (m_workerSocket[i] != null)
                    {
                        if (m_workerSocket[i].Connected)
                        {
                            m_workerSocket[i].Send(byData);
                        }
                    }
                }

            }
            catch (SocketException se)
            {
                MessageBox.Show(se.Message);
            }
        }

        void ButtonStopListenClick(object sender, System.EventArgs e)
        {
            CloseSockets();
        }

        String GetIP()
        {
            String strHostName = Dns.GetHostName();

            IPHostEntry iphostentry = Dns.GetHostEntry(strHostName);

            String IPStr = "";
            foreach (IPAddress ipaddress in iphostentry.AddressList)
            {
                IPStr = ipaddress.ToString();
                return IPStr;
            }
            return IPStr;
        }
        void ButtonCloseClick(object sender, System.EventArgs e)
        {
            CloseSockets();
        }
        void CloseSockets()
        {
            if (m_mainSocket != null)
            {
                m_mainSocket.Close();
            }
            for (int i = 0; i < m_clientCount; i++)
            {
                if (m_workerSocket[i] != null)
                {
                    m_workerSocket[i].Close();
                    m_workerSocket[i] = null;
                }
            }
        }
    }
}
