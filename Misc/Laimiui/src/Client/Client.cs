using System;
using System.Collections.Generic;

using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Client
{
    public class Client
    {
        private const int ANYPORT = 0;
        private bool DONE = false;
        private string serverName;
        private int tcpPort = 4567;

        public Client(string serverName, int tcpPort)
        {
            this.serverName = serverName;
            this.tcpPort = tcpPort;
        }

        public String updateServerTCP(String whatEver)
        {
            String dataReceived = "";
            DONE = false;
            try
            {
                TcpClient tcpClient = new TcpClient(serverName, tcpPort);
                NetworkStream tcpStream = tcpClient.GetStream();
                if (tcpStream.CanWrite)
                {
                    Byte[] inputToBeSent = System.Text.Encoding.ASCII.GetBytes(whatEver.ToCharArray());
                    tcpStream.Write(inputToBeSent, 0, inputToBeSent.Length);
                    tcpStream.Flush();
                }
                while (tcpStream.CanRead && !DONE)
                {
                    if (tcpStream.DataAvailable)
                    {

                        Byte[] received = new Byte[tcpClient.Available + 2];
                        int nBytesReceived = tcpStream.Read(received, 0, received.Length);
                        dataReceived = System.Text.Encoding.ASCII.GetString(received);
                        DONE = true;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("An Exception has occurred.");
                Console.WriteLine(e.ToString());
            }
            return dataReceived;
        }

    }

}
