using System;
using System.Collections.Generic;

using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace MainServer
{
    public class MainServer
    {
        private const int tcpPort = 4567;
        public Thread tcpThread;
        bool listen = true;
        MainForm form;
        RichTextBox logArea;
        
        
        public MainServer(RichTextBox logArea)
        {
            
            this.logArea = logArea;

            
            try
            {
                tcpThread = new Thread(new ThreadStart(StartListen));
                tcpThread.Start();
                logArea.AppendText("Started Server's TCP Listener Thread!\n");
            }
            catch (Exception e)
            {
                logArea.AppendText("An TCP Exception has occurred!" + e.ToString());
                tcpThread.Abort();
            }
        }

        private string interpretClient(string request, string username)
        {
            if (request[0] == 'S')
            {
                logArea.AppendText("Computer " + username + " has sarted the client");
            }
            else if (request[0] == 'E')
            {
                logArea.AppendText("Computer " + username + " has closed the client");
            }
            else if (request[0] == 'P')
            {
                logArea.AppendText("Computer " + username + " has started the software " + request.Substring(1));
            }
            else if (request[0] == 'C')
            {
                logArea.AppendText("Computer " + username + " has closed the software " + request.Substring(1));
            }
            else
            {
                logArea.AppendText("Computer " + username + " performed " + request.Substring(1));
            }
            return "OK";
        }

        public void StartListen()
        {

            TcpListener tcpListener = new TcpListener(tcpPort);
            try
            {
                while (listen)
                {
                    tcpListener.Start();

                    Socket soTcp = tcpListener.AcceptSocket();
                    
                    Byte[] received = new Byte[512];
                    int bytesReceived = soTcp.Receive(received, received.Length, 0);
                    
                    String dataReceived = System.Text.Encoding.ASCII.GetString(received);

                    logArea.AppendText("\n");
                    String returningString = interpretClient(dataReceived, ((IPEndPoint)soTcp.RemoteEndPoint).Address.ToString());
                    Byte[] returningByte = System.Text.Encoding.ASCII.GetBytes
                    (returningString.ToCharArray());
                    soTcp.Send(returningByte, returningByte.Length, 0);
                    tcpListener.Stop();
                }
            }
            catch (SocketException se)
            {
                logArea.AppendText("A Socket Exception has occurred!" + se.ToString());
            }
            catch (ThreadAbortException tae)
            {
            }
            tcpListener.Stop();
        }

        public void stop()
        {
            listen = false;
            tcpThread.Abort();
            
        }
    }
}
