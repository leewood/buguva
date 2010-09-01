import java.net.*;
import java.io.*;
import java.util.Scanner;

public class ClientInfo 
{
    private static final int WAITING = 0;
    private static final int SENTKNOCKKNOCK = 1;
    private static final int SENTCLUE = 2;
    private static final int ANOTHER = 3;
    
    
    public static final int LOGIN_OK = 0;
    public static final int LOGIN_ERR1 = 1;
    public static final int LOGIN_ERR2 = 2;
    
    public static final int UPD_MOVE = 0;
    public static final int UPD_ADD = 1;
    public static final int UPD_DEL = 2;
    
    
    private KKMultiServerThread thread = null;
    private Server server = null;
    private int state = WAITING;
	private int currentMode = 0;
    private int currentJoke = 0;
    private boolean loggedIn = false;
    
    private String loginName = "";
    private int x, y;
    private int myObjectID;
    int atk, def, magic, health;
    private String mapName = "1.map";
    private String updateData = ""; 
	private String parsing = "";	
    private int clientID = 0;
 
    public ClientInfo(KKMultiServerThread caller, Server server)
	{
	    this.thread = caller;
	    this.server = server;
        loginName = "none";
        loggedIn = false;	 
        clientID = caller.clientID;   
	}
	
    public int loginClient(String name)
    {
    	int place = 0;
    	synchronized (server)
    	{
    		place = server.addClient(name);
    	}
        if (place >= 0)
        {        
           synchronized (server.mapPointer)        	
           {        
        	
               mapName = "1.map";
               x = 10;
               y = 10;
               (server.mapPointer.maps[0].objectsCount)++;
               myObjectID = server.mapPointer.maps[0].objectsCount - 1;
               ObjectDef obj = new ObjectDef();
               obj.x = 10;
               obj.y = 10;
               obj.type = 1;
               obj.ci = clientID;
               atk = 1;
               obj.atk = 1;
               def = 1;
               obj.def = 1;
               magic = 1;
               obj.magic = 1;
               health = 1;
               obj.health = 100;
               obj.name = name;
               server.mapPointer.maps[0].objects[myObjectID] = obj;
           }
           loggedIn = true;
           return LOGIN_OK;
        } 
       	else if (place == -1)
       	{
            return LOGIN_ERR1;
       	}
       	else
       	{
       		return  LOGIN_ERR2;
       	}
    }			
	
	String loadMap(String fileName, int id)
	{
		synchronized (server.mapPointer)
		{		
		    return server.mapPointer.loadMap(fileName, id);
		}
	}

    void logoutClient(String name)
    {    
        
        synchronized (server)
        {
        	if (loggedIn)
        	{
        		server.removeClient(name);
        	} 
            else 
            {
            	server.clientNames[clientID] = "";
            	server.connectedCount--;
        	}
        	
        }
        loggedIn = false;
        synchronized (server.mapPointer)
        {
            int place = server.mapPointer.findMap(mapName);
            server.mapPointer.maps[place].removeObject(myObjectID);        	
        }
        thread.updateID = myObjectID + 1;
    }    
	
	String fixedSizeString(String source, int size)
    {
        return Utils.fixedSizeString(source, size);
    }

    public String subByte(int cbyte)
    {
    	return Utils.subByte(cbyte);
    }


    String byteToStr(int cbyte)
    {
    	return Utils.byteToStr(cbyte);
    }
	
	
	public String interpretCommand(String data)
	{
       thread.updateID = 0;
       String toUpdate = "";
	   String returnString = "";
       thread.showInfo = true;
       synchronized (server)
       {
       	   myObjectID = server.myObjectIDs[thread.clientID];
       	   clientID = thread.clientID;
       }
       
       if (currentMode < 100)
       {
           if (data.equals("DISCON:"))
           {
               //server.sendToClient(clientID, "DISCONNECT OK");
			   returnString = "DISCONNECT OK";
               //server.clients[clientID].con = false;
            
               if (loggedIn)
               {
                  logoutClient(loginName);            
                  toUpdate = byteToStr(UPD_DEL) + byteToStr(myObjectID);
               }
               loggedIn = false;
               synchronized (server)
               {               
                   server.disconnect(clientID);
               }
               thread.needToStop = true;
               currentMode = 0;
               System.out.println("Client " + thread.clientID + " disconnected\n");                         
			
          }
		
          else if (data.equals("LOGIN:"))
          {
             //server->sendToClient(clientID, "LOGIN START");             
			 returnString = "LOGIN START";
             currentMode = 100;
          }
          else if (data.equals("LOGOUT:"))
          {
             loggedIn = false;
             //server->sendToClient(clientID, "LOGGED OUT");
             logoutClient(loginName);
             toUpdate = byteToStr(UPD_DEL) + byteToStr(myObjectID);
			 returnString = "LOGGED OUT";
             currentMode = 0;
             
           }
           else if (data.equals("LOGINNAME:"))
           {
               if (!loggedIn)
               {
                  //server->sendToClient(clientID, "SERVER ERROR: NOT LOGGED IN");
			      returnString = "SERVER ERROR: NOT LOGGED IN";	  
               } 
               else 
               {
                  //server->sendToClient(clientID, loginName);
				  returnString = loginName;	  
               }
           }
           else if (data.equals("MOVE:"))
           {
                currentMode = 102;
                parsing = byteToStr(UPD_MOVE) + byteToStr(x) + byteToStr(y);
                //server->sendToClient(clientID, "MOVE START, WAIT X:");
				returnString = "MOVE START, WAIT X:";
           }
           else if (data.equals("MAPSEND:"))
           {
               currentMode = 101;
               //server->sendToClient(clientID, "MAPSEND START");
			   returnString = "MAPSEND START";
           }
           else if (data.equals("REMOBJ:"))
           {
               currentMode = 104;
               //server->sendToClient(clientID, "OBJDEL START");
			   returnString = "OBJDEL START";
           }
           else if (data.equals("DROPOBJ:"))
           {
               currentMode = 105;
               //server->sendToClient(clientID, "OBJDROP START");
			   returnString = "OBJDROP START";
           }
           else if (data.equals("OBJUPDATE:"))
           {
               currentMode = 106;
               //server->sendToClient(clientID, "OBJUPD START");
			   returnString = "OBJUPD START";
           }
           else if (data.equals("UPDATE:"))
           {
           	   synchronized (server)
           	   {
           	   	   updateData = server.updateStrings[thread.clientID];
           	   	   server.updateStrings[thread.clientID] = "";
           	   }
           	   
               if (!updateData.equals(""))
               {                 
				  System.out.println(thread.clientID + "->Server: UPDATE:");
                  //server->sendToClient(clientID, updateData);                
				  returnString = updateData;
               } 
               else 
               {
                  thread.showInfo = false;
                  //server->sendToClient(clientID, "N");    
				  returnString = "N";
               } 
               updateData = "";
           }
           else if (data.equals("LOADACCOUNT:"))
           {
               currentMode = 103;
               x = 5;
               y = 5;
             
           }
           else
           {
              //server->sendToClient(clientID, "SERVER ERROR: INCORRECT COMMAND");
			  returnString = "SERVER ERROR: INCORRECT COMMAND";
           }
       } 
       else if (currentMode == 100)
       {
           currentMode = 0;
           int code = loginClient(data);
           if (code == 0)
           {
              loginName = data;
              //server->sendToClient(clientID, "LOGIN OK");
			  returnString = "LOGIN OK";
              loggedIn = true;            
              toUpdate = byteToStr(UPD_ADD) + byteToStr(x) + byteToStr(y) + byteToStr(1) + byteToStr(myObjectID) + byteToStr(atk) + byteToStr(def) + byteToStr(magic) + byteToStr(health) + fixedSizeString(loginName, 20);
           } 
           else 
           {
            //server->sendToClient(clientID, "ERROR" + intToStr(code));
		       returnString = "ERROR" + code;
           }
       }
       else if (currentMode == 101)
       {
          currentMode = 0;
          String result = loadMap(data, myObjectID);
          //server->sendToClient(clientID, result);  
		  return result;
       }
       else if (currentMode == 102)
       {
          currentMode = 103;
          x = Integer.parseInt(data);
          parsing += byteToStr(x);
          //server->sendToClient(clientID, "WAIT Y:");         		 
		  returnString = "WAIT Y:";
       }
       else if (currentMode == 103)
       {
           currentMode = 0;
           Scanner ss = new Scanner(data);
           y = ss.nextInt();
           parsing += byteToStr(y) + byteToStr(myObjectID);
           toUpdate = parsing;
           parsing = "";
           //server->sendToClient(clientID, "MOVE OK");                   
           synchronized (server.mapPointer)
           {           
              int place = server.mapPointer.findMap(mapName);
              server.mapPointer.maps[place].objects[myObjectID].x = x;
              server.mapPointer.maps[place].objects[myObjectID].y = y;
           }
           returnString = "MOVE OK";
       }
       else if (currentMode == 104)
       {
           currentMode = 0;
           Scanner ss = new Scanner(data);                
           int id = ss.nextInt();        
           synchronized (server.mapPointer)
           {                      	
               int place = server.mapPointer.findMap(mapName);
               server.mapPointer.maps[place].removeObject(id);        
           }
           toUpdate = "02" + byteToStr(id);
           thread.updateID = id + 1;
           //server->sendToClient(clientID, "DELOBJ OK");         
           returnString = "DELOBJ OK";
       }
       else if (currentMode == 105)
       {       
           currentMode = 0;
           Scanner ss = new Scanner(data);                
           int id, atk, def, magic, health, x, y, type;
           String name;
           id = ss.nextInt();
           atk = ss.nextInt();
           def = ss.nextInt();
           magic = ss.nextInt();
           health = ss.nextInt();
           type = ss.nextInt();         
           x = ss.nextInt();
           y = ss.nextInt();
           name = ss.next();
           int place = 0;
           int count = 0;
           synchronized (server.mapPointer)
           {           
               place = server.mapPointer.findMap(mapName);
               count = server.mapPointer.maps[place].objectsCount;
           }
           ObjectDef obj = new ObjectDef();
           obj.x = x;
           obj.y = y;
           obj.atk = atk;
           obj.def = def;
           obj.magic = magic;
           obj.health = health;
           obj.type = type;
           obj.name = name;
           synchronized (server.mapPointer)
           {           
               server.mapPointer.maps[place].objects[count] = obj;
               (server.mapPointer.maps[place].objectsCount)++;
           }
           toUpdate = "01" + byteToStr(x) + byteToStr(y) + byteToStr(type) + byteToStr(count) + byteToStr(atk) + byteToStr(def) + byteToStr(magic) + byteToStr(health) + fixedSizeString(name, 20);         
       }
       else if (currentMode == 106)
       {
           currentMode = 0;
           Scanner ss = new Scanner(data);            	
           int id, atk, def, magic, health, x, y, type;
           String name;
           id = ss.nextInt();
           atk = ss.nextInt();
           def = ss.nextInt();
           magic = ss.nextInt();
           health = ss.nextInt();
           type = ss.nextInt();
           x = ss.nextInt();
           y = ss.nextInt();
           name = ss.next();         
           synchronized (server.mapPointer)
           {           
                int place = server.mapPointer.findMap(mapName);
                server.mapPointer.maps[place].objects[id].atk = atk;
                server.mapPointer.maps[place].objects[id].def = def;
                server.mapPointer.maps[place].objects[id].magic = magic;
                server.mapPointer.maps[place].objects[id].health = health;
                x = server.mapPointer.maps[place].objects[id].x;
                y = server.mapPointer.maps[place].objects[id].y;
           }
           toUpdate = "03" + byteToStr(id) + byteToStr(x) + byteToStr(y) + byteToStr(type)  + byteToStr(atk) + byteToStr(def) + byteToStr(magic) + byteToStr(health);
       }
       thread.updateString = toUpdate;
       synchronized (server)
       {
       	   server.myObjectIDs[thread.clientID] = myObjectID;
       	   thread.clientID = clientID;
       }       
       return returnString;	
	}
}
