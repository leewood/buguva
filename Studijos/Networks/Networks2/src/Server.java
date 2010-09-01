/**
 * @(#)Server.java
 *
 *
 * @author 
 * @version 1.00 2008/11/8
 */


public class Server 
{

    public int SOMAXCON = 10;
    public Map mapPointer = null;
            
    public Server(int maxConnections, Map mapPointer) 
    {
    	SOMAXCON = maxConnections;
    	this.mapPointer = mapPointer;    	
    	clientNames = new String[SOMAXCON];
    	updateStrings = new String[SOMAXCON];
    	myObjectIDs = new int[SOMAXCON];
    }
    
    public String[] clientNames;
    public int clientsCount = 0;
    public int connectedCount = 0;
    public String[] updateStrings;
    public int[] myObjectIDs;
    
    public void initUpdate(String toUpdate, int updateID, int currentID)
    {
    	if (updateID > 0)
        {                  
             System.out.println("Updating IDs " + updateID);
             for (int k = 0; k < clientsCount; k++)
             {
                      
                 if ((myObjectIDs[k] > updateID) && (!clientNames[k].equals("")))
                 {
                      (myObjectIDs[k])--;
                 }                      
              }              
              
         }
         if (toUpdate != "")
         {
             for (int j = 0; j < clientsCount; j++)
             {
                 if ((j != currentID) && (!clientNames[j].equals("")))
                 {
                      updateStrings[j] += toUpdate;
                 }
             }
         }           
         
    }
    
    public void disconnect(int clientID)
    {
    	removeClient(clientNames[clientID]);
    }
    
    public int findClientName(String name)
    {    
        for (int i = 0; i < clientsCount; i++)
        {
            if (clientNames[i].equals(name))
            {            
               return i;
            }
        }    
        return -1;
    } 
    
    
    public int getNewClientID()
    {
    	if (connectedCount == clientsCount)
    	{
    		return  clientsCount;
    	}
    	else 
    	{
    		int place = findClientName("");
    		return place;
    	}    	
    }
    
    
    public void reserveClientID(int id)
    {
    	clientNames[id] = "$reserved\n";    	
    }
    
    public int addClient(String name)
    {
    	if (connectedCount < SOMAXCON - 1)
    	{
    		if (findClientName(name) < 0)
    		{    		
    		   if (connectedCount == clientsCount)
    		   {    		   
    		       clientNames[clientsCount] = name;
    		       updateStrings[clientsCount] = "";
    		       myObjectIDs[clientsCount] = 0;
    		       clientsCount++;
    		       connectedCount++;
    		       return clientsCount - 1;
    		   } 
    		   else
    		   {
    		       int place = findClientName("$reserved\n");
    		       clientNames[place] = name;
    		       updateStrings[place] = "";
    		       myObjectIDs[place] = 0;
    		       connectedCount++;    		       
    		       return place;
    		   }    		       		   
    		} else 
    		{
    			return -2;
    		}
    	} 
    	else 
    	{
    		return -1;
    	}
    }
    
    public void removeClient(String name)
    {
        int place = findClientName(name);     
        if (place >= 0)
        {
        	/*
        	for (int i = place; i < clientsCount - 1; i++)
            {
                clientNames[i] = clientNames[i + 1];
            }
            */
            clientNames[place] = "";
            //clientsCount--;
            connectedCount--;
        }    	
    }
    
}