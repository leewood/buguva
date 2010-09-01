using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using System.IO;

namespace Kartuves
{

	public class Network
	{

		#region Variables 
 
		//_____________________________________________________________________________________________
		//
		// Reference for the TicTacToe main screen
		//_____________________________________________________________________________________________

		volatile Kartuves.Form1 objForm=null;
					
		//_____________________________________________________________________________________________
		//
		// Thread for client and server
		//_____________________________________________________________________________________________

		public Thread thread_receive_client;
		public Thread thread_receive_server;

		//_____________________________________________________________________________________________
		//
		// Server IP and port
		//_____________________________________________________________________________________________

		private string wServerIP;
		const int SERVERPORT=50000;

		//_____________________________________________________________________________________________
		//
		// Loop control variables for client and server threads
		//_____________________________________________________________________________________________

		public volatile bool wReceivingServer=true;
		public volatile bool wReceivingClient=true;

		//_____________________________________________________________________________________________
		//
		// TCP e NetworkStream objects for client and server
		//_____________________________________________________________________________________________

		NetworkStream clientSockStream;
		NetworkStream serverSockStream; 

		TcpClient tcpClient;
		TcpListener tcpListener;
		Socket soTcpServer;
		
		#endregion

		#region Constructor 

        public Network(Kartuves.Form1 pThis)
		{
			
			//_____________________________________________________________________________________________
			//
			// References screen game
			//_____________________________________________________________________________________________

		   	objForm=pThis;
			 
		}

		#endregion

		#region Client 

		public void ConnectServer(string pIP)
		{

			//_____________________________________________________________________________________________
			//
			// Connect to a game server
			//_____________________________________________________________________________________________

			wServerIP=pIP;
			byte[] buf = new byte[1];
			
			thread_receive_client = new Thread(new ThreadStart(ThreadReceivingClient));
			thread_receive_client.Start();
			
		}


		private void ThreadReceivingClient()
		{
			//_____________________________________________________________________________________________
			//
			// Thread for receiving packets from server
			//_____________________________________________________________________________________________

			try
			{

				byte[] buf = new byte[512];
				int bytesReceived=0;
                int size;
				tcpClient = new TcpClient(wServerIP,SERVERPORT);
				clientSockStream = tcpClient.GetStream();

                lock (objForm)
                {
                    objForm.RestartGame();
                    objForm.SetStatusMessage("Connected!");
                }

				wReceivingClient=true;

				while (wReceivingClient)
				{
                    
                    size = tcpClient.Available;
                    buf = new byte[size + 2];
					//_____________________________________________________________________________________________
					//
					// Thread is blocked until receives data
					//_____________________________________________________________________________________________
			
					try
					{
						bytesReceived = clientSockStream.Read(buf, 0, size);
					}
					catch
					{
						return;
					}
									
					//_____________________________________________________________________________________________
					//
					// Processes network packet
					//_____________________________________________________________________________________________
				
					if (bytesReceived>0) 
					{
						//_____________________________________________________________________________________________
						//
						// Control packet for game restart
						//_____________________________________________________________________________________________

						if (buf[0]==byte.Parse(Asc("R").ToString()))
						{
                            lock (objForm)
                            {
                                objForm.RestartGame();
                            }
							continue;
						}
                        if (buf[0] == byte.Parse(Asc("W").ToString()))
                        {
                            byte b = buf[1];
                            int i = 1;
                            byte[] buf2 = new byte[1];
                            string s = "";

                            while (i < size)
                            {
                                if (b != 0)
                                    s += char.ConvertFromUtf32(b);
                                i++;
                                b = buf[i];
                            }
                            objForm.realWord = s;
                            
                            continue;
                        }

						//_____________________________________________________________________________________________
						//
						// Packet indicating a game move
						//_____________________________________________________________________________________________
			
						int wRow=int.Parse(Convert.ToChar(buf[0]).ToString());
						int wColumn=int.Parse(Convert.ToChar(buf[1]).ToString());

						
                            lock (objForm)
                            {
                                objForm.wNetworkPlay = true;
                                objForm.makeMove(wRow, wColumn);
                                //objTicTacToe.wNetworkPlay=true;
                                //objTicTacToe.MakeMove(wRow,wColumn);
                            }
						

					} //if (bytesReceived>0) 
			
				} //while (wReceivingClient)

			}
			catch (ThreadAbortException ) {}
			catch (Exception ex)
			{
				MessageBox.Show("An error ocurred: " + ex.Message + "\n" + ex.StackTrace);
                lock (objForm)
                {
                   
                    //objTicTacToe.mnDisconnect_Click(null,null);
                }
				return;
			}
		}

		#endregion

		#region Server 
 
		public void StartServer()
		{
			
			//_____________________________________________________________________________________________
			//
			// Starts game server
			//_____________________________________________________________________________________________
			
			thread_receive_server = new Thread(new ThreadStart(ThreadReceivingServer));
			thread_receive_server.Start();
            
		}

	
		private void ThreadReceivingServer()
		{
			//_____________________________________________________________________________________________
			//
			// Thread for receiving packets from client
			//_____________________________________________________________________________________________

			try 
			{
				byte[] buf = new byte[512];
				IPHostEntry localHostEntry = Dns.GetHostEntry(Dns.GetHostName());
                int size;
				int bytesReceived=0;

				//tcpListener = new TcpListener(localHostEntry.AddressList[0],SERVERPORT);
                tcpListener = new TcpListener(SERVERPORT);
				tcpListener.Start();
			
				//_____________________________________________________________________________________________
				//
				// Thread is blocked until it gets a connection from client
				//_____________________________________________________________________________________________

				soTcpServer = tcpListener.AcceptSocket();

				serverSockStream = new NetworkStream(soTcpServer);

                lock (objForm)
                {
                    objForm.RestartGame();
                    objForm.SetStatusMessage("Connected!");
                }
				wReceivingServer=true;

				while (wReceivingServer)
				{
                    size = soTcpServer.Available;
                    buf = new byte[size + 2];
					//_____________________________________________________________________________________________
					//
					// Thread is blocked until receives data
					//_____________________________________________________________________________________________

					try 
					{
                        //soTcpServer.Available
						bytesReceived=serverSockStream.Read(buf,0,size);
					}
					catch
					{
						return;
					}
				
					//_____________________________________________________________________________________________
					//
					// Processes network packet
					//_____________________________________________________________________________________________

					if (bytesReceived>0) 
					{

						//_____________________________________________________________________________________________
						//
						// Control packet for game restart
						//_____________________________________________________________________________________________
                        if (buf[0] == byte.Parse(Asc("L").ToString()))
                        {

                        }

						if (buf[0]==byte.Parse(Asc("R").ToString()))
						{
                            lock (objForm)
                            {
                                objForm.RestartGame();
                                
                            }
							continue;
						}
                        if (buf[0] == byte.Parse(Asc("W").ToString()))
                        {
                            byte b = buf[1];
                            int i = 1;
                            byte[] buf2 = new byte[1];
                            string s = "";
                            
                            while (i < size)
                            {
                                if (b != 0)
                                    s += char.ConvertFromUtf32(b);
                                i++;
                                b = buf[i];
                            }
                            objForm.realWord = s;
                            SendWord(objForm.realWordOpponent);
                            continue;
                        }
						//_____________________________________________________________________________________________
						//
						// Packet indicating a game move
						//_____________________________________________________________________________________________

                        if (size >= 2)
                        {
                            int wRow = int.Parse(Convert.ToChar(buf[0]).ToString());
                            int wColumn = int.Parse(Convert.ToChar(buf[1]).ToString());

                                lock (objForm)
                                {
                                    objForm.wNetworkPlay = true;
                                    objForm.makeMove(wRow, wColumn);
                                    //objTicTacToe.MakeMove(wRow,wColumn);
                                }
                        }

					}	//if (bytesReceived>0) 
			
				}	//while (wReceivingServer)
			}
			catch (ThreadAbortException) {}
			catch (Exception ex)
			{
				MessageBox.Show("An error ocurred: " + ex.Message + "\n" + ex.StackTrace);
                lock (objForm)
                {
                    objForm.disconnectToolStripMenuItem_Click(null, null);
                    //objTicTacToe.mnDisconnect_Click(null,null);
                }
				return;
			}
		}

		#endregion

		#region Functions for sending packets/disconnect 
 
        public void SendPacketTCP(Byte[] pDados)
        {
            SendPacketTCP(pDados, 2);
        }

		public void SendPacketTCP(Byte[] pDados, int size)
		{
			//_____________________________________________________________________________________________
			//
			// Sends a packet via TCP
			//_____________________________________________________________________________________________

			try
			{
				if (objForm.wClient==true)
				{
					if (clientSockStream==null)
						return;

					if (clientSockStream.CanWrite)
					{
						clientSockStream.Write(pDados, 0, size);
						clientSockStream.Flush();
					}
				}
				else
				{
					if (serverSockStream==null)
						return;

					if (serverSockStream.CanWrite)
					{
						serverSockStream.Write(pDados,0, size);
						serverSockStream.Flush();
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("An error ocurred: " + ex.Message + "\n" + ex.StackTrace);
                lock (objForm)
                {
                    objForm.disconnectToolStripMenuItem_Click(null, null);
                    //objTicTacToe.mnDisconnect_Click(null,null);
                }
				return;
			}
			
		}

		public void SendMove(int wRow,int wColumn)
		{
			//_____________________________________________________________________________________________
			//
			// Sends packet that shows move position
			//_____________________________________________________________________________________________

			byte[] buf = new byte[2];
			buf[0]=byte.Parse(Asc(wRow.ToString()).ToString());
			buf[1]=byte.Parse(Asc(wColumn.ToString()).ToString());

			SendPacketTCP(buf);
			
		}

        public void SendWord(string word)
        {
            int size = word.Length + 2;
            byte[] buf = new byte[size];
            buf[0] = byte.Parse(Asc("W").ToString());
            for (int i = 0; i < word.Length; i++)
            {
                string s = "";
                s+=word[i];
                buf[i + 1] = byte.Parse(Asc(s).ToString());
            }
            SendPacketTCP(buf, size);

        }

        public void SendsRestartPacket()
		{
			//_____________________________________________________________________________________________
			//
			// Sends packet for the other game restart
			//_____________________________________________________________________________________________

			byte[] buf = new byte[2];
			buf[0]=byte.Parse(Asc("R").ToString());
			buf[1]=0;

			SendPacketTCP(buf);

		}

		public void Disconnect()
		{
			//_____________________________________________________________________________________________
			//
			// Disconnect client and server
			//_____________________________________________________________________________________________
			
			if (objForm.wClient==true)
			{
				thread_receive_client.Abort();

				wReceivingClient=false;

				if (clientSockStream!=null)
					clientSockStream.Close();

				if (tcpClient!=null)
					tcpClient.Close();
		
			}

			if (objForm.wServer==true)
			{
				thread_receive_server.Abort();	

				wReceivingServer=false;

				if (serverSockStream!=null)
					serverSockStream.Close();
				
				if (tcpListener!=null)
					tcpListener.Stop();

				if (soTcpServer!=null)
					soTcpServer.Shutdown(SocketShutdown.Both);
					
			}

		}

		private static int Asc(string character)
		{
			//_____________________________________________________________________________________________
			//
			// VB.NET ASC function
			//_____________________________________________________________________________________________

			if (character.Length == 1)
			{
				System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
				int intAsciiCode = (int)asciiEncoding.GetBytes(character)[0];
				return (intAsciiCode);
			}
			else
			{
				throw new ApplicationException("Character is not valid.");
			}

		}	//private static int Asc(string character)

		#endregion

	}	//public class Network

}	
