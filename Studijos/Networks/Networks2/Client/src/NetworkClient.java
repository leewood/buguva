/**
 * @(#)NetworkClient.java
 *
 *
 * @author 
 * @version 1.00 2008/11/9
 */

import java.net.*;
import java.io.*;

public class NetworkClient 
{

    public NetworkClient() 
    {
    }

    public static final int DEFAULT_BUFLEN = 512;
    public static final String DEFAULT_PORT = "27019";
	private OutputStream outStream = null;
	private InputStream inStream = null;    
    public String port;
    private String server = "";
    public int isError;
    private SystemWindow sys = null;
        
    public NetworkClient(SystemWindow sys)
    {
    	this(DEFAULT_PORT, sys);
    }
        
    public NetworkClient(String port, SystemWindow sys)
    {
    	this.port = port;
    	this.sys = sys;        		
    }
    
    public int initSocket(String server, String stPort)
    {
        isError = 0;          
        this.server = server;        	
        port = stPort;
        recvbuflen = DEFAULT_BUFLEN;  
        connected = false;
        return 0;
    }
    
    public int connectToServer()
    {
        try
        {            
            ConnectSocket = new Socket(server, Integer.parseInt(port));
            connected = true;
            inStream = ConnectSocket.getInputStream();
            outStream = ConnectSocket.getOutputStream();
        } catch (Exception e)
        {
        	sys.outputToSystemMessages(e.getMessage());
        	return 1;
        }
    	return 0;
    }
    
    public String receive()
    {
		boolean foundEnd = false;	
		int MAX_BYTES = DEFAULT_BUFLEN;
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
		} catch (IOException e)
		{			
		   sys.outputToSystemMessages(e.getMessage());
		   isError = 1;
	    }
		return inputLine;    	
    }
    
    public void disconnect()
    {
        if (connected)
        {
           try
           {           
               sendToServer("DISCON:");
               ConnectSocket.close();
           } catch (Exception e)
           {
               sys.outputToSystemMessages(e.getMessage());               
           }
      
        }   	
    }
    
    public int sendToServer(String data)
    {
	   	int MAX_BYTES = DEFAULT_BUFLEN;
		byte[] buf = new byte[MAX_BYTES];	
		int position = 0;
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
		} catch (IOException e) 
		{
			sys.outputToSystemMessages(e.getMessage());
			return 1;
		}    	
		return 0;
    }
    
    private Socket ConnectSocket;
    
    
    private int iResult;
    private int recvbuflen;  
    private boolean connected;      
    
}