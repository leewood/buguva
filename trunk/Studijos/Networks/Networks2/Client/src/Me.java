/**
 * @(#)Me.java
 *
 *
 * @author 
 * @version 1.00 2008/11/9
 */


public class Me 
{
     TMap map;
     NetworkClient client = null;      

     public int myAttack;
     public int myDefence;
     public int myMagic;
     public int myHealth;
     public int myX;
     public int myY;
     public int myID;
     public boolean killed;
     public ObjectDef gun, shield, wand;
     public boolean hasGun;
     public boolean hasShield;
     public boolean hasMagicWand;
   
    public Me() 
    {
    }
    
    void makeAMove(int newx, int newy)
    {
        String data = "";
        if ((newx >= 0) && (newx < 40) && (newy < 17) && (newy >= 0))
        {
            int mapType = map.mapType(newx, newy);
            boolean canMove = (mapType == 0) || ((mapType == Utils.MAP_SAND) && (myDefence > 10)) || ((mapType == Utils.MAP_WATER) && (myMagic > 10));
            if (canMove)
            {            
               client.sendToServer("MOVE:");
               data = client.receive();
               client.sendToServer(Utils.intToStr(newx));
               data = client.receive();
               client.sendToServer(Utils.intToStr(newy));    
               data = client.receive();
               //sys.outputToSystemMessages(">" + data);   
               map.objects[myID].x = newx;
               map.objects[myID].y = newy;                    
               map.moveObject(myX, myY, newx, newy, Utils.OBJ_ME);
               myX = newx;
               myY = newy;                        
               //interpretObjects(myX, myY, MODE_MOVE);

            }
        }
        else 
        {        
            if (newx < 0)
            {
                newx = 39;
                client.sendToServer("MAPSEND:");
                client.receive();
                data = client.receive();
                //loadInicialData(data);
                client.sendToServer("ADD:");
                client.receive();
                client.sendToServer(Utils.intToStr(39) + " " + Utils.intToStr(newy));
                client.receive();
                //interpretObjects(myX, myY, MODE_MOVE);
            } 
            else  if (newx >= 40)
            {
                newx = 0;
                client.sendToServer("MAPSEND:");
                client.receive();
  //            client->sendToServer(rightMap);
                data = client.receive();
            //loadInicialData(data);
                client.sendToServer("ADD:");
                client.receive();
                client.sendToServer(Utils.intToStr(newx) + " " + Utils.intToStr(newy));
                client.receive();
              
            }
            else  if (newy >= 17)
            {
               newy = 0;
               client.sendToServer("MAPSEND:");
               client.receive();
 //            client->sendToServer(bottomMap);
               data = client.receive();
               //loadInicialData(data);
               client.sendToServer("ADD:");
               client.receive();
               client.sendToServer(Utils.intToStr(newx) + " " + Utils.intToStr(newy));
               client.receive();
              
            }
            else
            {
               newy = 16;
               client.sendToServer("MAPSEND:");
               client.receive();
 //            client->sendToServer(topMap);
               data = client.receive();
            //loadInicialData(data);
               client.sendToServer("ADD:");
               client.receive();
               client.sendToServer(Utils.intToStr(newx) + " " + Utils.intToStr(newy));
               client.receive();
              
            }
        
        
        
        }    	
    }
    
    public void initMe(TMap map, NetworkClient client)
    {        
        hasGun = false;
        hasShield = false;
        hasMagicWand = false;  
        myAttack = 0;
        myDefence = 0;
        myMagic = 0;
        myHealth = 50;        
        killed = false;
        myID = 0;
        myX = 10;
        myY = 10;
        this.map = map;
        this.client = client;
    }  
    
    
}