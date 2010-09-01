import java.net.*;
import java.io.*;

public class KKMultiServerThread extends Thread 
{
    private Socket socket = null;
    public int updateID = 0;
	public boolean showInfo = true;
	public String loginName = "";
	public String updateString = "";
	public int clientID = 0;
	private OutputStream outStream = null;
	private InputStream inStream = null;
	public boolean needToStop = false;
	private Server server;
	
    public KKMultiServerThread(Socket socket, Server server) 
	{
	   super("KKMultiServerThread");
	   this.socket = socket;
	   this.clientID = -1;
	   this.server = server;
    }

	public String readFromClient() throws Exception
	{
		boolean foundEnd = false;	
		int MAX_BYTES = 512;
		byte[] buf = new byte[MAX_BYTES];		
		String inputLine = "";
		try
		{
		while (!foundEnd)
		{
		    int totalBytes = 0;
			while (totalBytes < MAX_BYTES)
			{
			   int readBytes = inStream.read(buf, totalBytes, MAX_BYTES - totalBytes);
			   totalBytes += readBytes;			   
		    }
			for (int i = 0; i < MAX_BYTES; i++)
			{
			    if (!foundEnd)
				{
				   if (buf[i] == 0)
				   {
				      foundEnd = true;
				   } 
				   else 
				   {				
				      inputLine += (char)(buf[i]);
				   }
				}
		    }
	    }
		} catch (Exception e)
		{
			inputLine = "DISCON:";
			
	    }
		return inputLine;

	}
	
	
	public void sendToClient(String data) throws Exception
	{
	   	int MAX_BYTES = 512;
		byte[] buf = new byte[MAX_BYTES];	
		int position = 0;
		if (showInfo)
        {
            System.out.println( "Server->" + clientID + ": " + data);
        }  
		try
		{
		while (position < data.length())
		{
		    for (int i = 0; i < MAX_BYTES; i++)
			{
			   if (position < data.length())
			   {
			      buf[i] = (byte)data.charAt(position);				  
			   }
			   else
			   {
			      buf[i] = 0;
			   }
			   position++;
			}
			outStream.write(buf, 0, MAX_BYTES);
		}
		} catch (Exception e) 
		{
			throw e;
		}
	}
	
    public void run() 
    {

	  try 
	  {
	    //PrintWriter out = new PrintWriter(socket.getOutputStream(), true);
	    //BufferedReader in = new BufferedReader(
		//		    new InputStreamReader(
	//			    socket.getInputStream()));
		String inputLine, outputLine;
		synchronized (server)
		{		   
		      clientID = server.getNewClientID();
		      server.reserveClientID(clientID);
		}

        System.out.println("Client " + clientID + " accepted (IP:" + socket.getInetAddress().getHostAddress()   + ")");
		inStream = socket.getInputStream();
		outStream = socket.getOutputStream();
		
		ClientInfo kkp = new ClientInfo(this, server);
		while (!needToStop)
		{		 		   	
		   inputLine = readFromClient();
	       if (!inputLine.equals("UPDATE:"))
           {       
                 System.out.println(clientID + "->Server: " + inputLine);
           }
	       
	    //outputLine = kkp.processInput(null);
	    //out.println(outputLine);
	       updateID = 0;
	      
		   outputLine = kkp.interpretCommand(inputLine);
		   if (!updateString.equals(""))
		   {
		   	    synchronized (server)
		   	    {
		   	    	server.initUpdate(updateString, updateID, clientID);
		   	    }
		   }
		   //out.println(outputLine);
		   
		   sendToClient(outputLine);
        } 

	   } catch (Exception e) {
	       //e.printStackTrace();
	       System.out.println(e.getMessage());
	   }
	   try
	   {
	   	  inStream.close();
	   } catch (IOException io1)
	   {
	   	  System.out.println(io1.getMessage());
	   }
	   try
	   {	   
	      outStream.close();
	   } catch (IOException io2)
	   {
	   	  System.out.println(io2.getMessage());
	   }
	   try
	   {
	      socket.close();
	   } catch (IOException io3)
	   {
	   	  System.out.println(io3.getMessage());
	   }
	   
    }
}
