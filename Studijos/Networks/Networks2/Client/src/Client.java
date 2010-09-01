/**
 * @(#)Client.java
 *
 * Client application
 *
 * @author 
 * @version 1.00 2008/11/9
 */
 
import java.awt.Color;
import java.util.Iterator;
import java.util.Map;
import javax.swing.JFrame; 
 
public class Client 
{
	
     private static int interpretObjects(int x, int y, int mode)
     {
         int[] objectsInPlace = new int[30];
         int fightType = 0;
         int count = map.objectsInXYCount(x, y);
         for (int i = 0; i < count; i++)
         {
             objectsInPlace[i] = map.objectsInXY(x, y, i + 1);
         }
         switch(mode)
         {
             case Utils.MODE_MOVE:
             {
                 boolean isMonster = false;
                 for (int i = 0; i < count; i++)
                 {
                     if (objectsInPlace[i] == Utils.OBJ_MONSTER)
                     {
                         isMonster = true;
                     }
                 }
                 if (isMonster)
                 {
                     sys.outputToSystemMessages("MONSTER FOUND, FIGHT START");
                     mode = 2;
                     fightType = 1;
                 }
             }
             break;
             case Utils.MODE_ACTIVATE:
             {
                 boolean isPlayer = false;
                 for (int i = 0; i < count; i++)
                 {
                     if (objectsInPlace[i] == Utils.OBJ_USER)
                     {
                         isPlayer = true;
                     }
              
                 }
                 int isItem = -1;           
                 for (int i = count - 1; i >= 0; i--)
                 {
                     if ((objectsInPlace[i] >= Utils.OBJ_HEALTH_POINT) && (objectsInPlace[i] <= Utils.OBJ_MAGIC_WAND))
                     {
                         isItem = i;
                         break;
                     }
                 }
                 if (isPlayer)
                 {
                     sys.outputToSystemMessages("PLAYER FOUND, FIGHT START");
                     mode = 2;
                     fightType = 2;
                 } 
                 else if (isItem >= 0)
                 {
                     inventory.takeItem();  
                 }
                      
             }
         }
         return fightType;
     }
    
    private static TMap map = null;
    private static SystemWindow sys = null;
    private static Inventory inventory = null;
    
    public static void main(String[] args) 
    {
    	
    	// TODO, add your application code
    	int inactivity = 0;
    	JFrame frame = new JFrame("ZZT RPG");
		TextConsole console = new TextConsole(80,25, 20, "Courier New");
		frame.getContentPane().add(console);
		frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		frame.setResizable(false);
		frame.pack();
		frame.setVisible(true);		
		console.setExtendedKeyboardMode(); 
	    sys = new SystemWindow(console);
    	NetworkClient client = new NetworkClient(sys);
        inventory = new Inventory();        
        Me me = new Me();
        map = new TMap(me, console);
        Login login = new Login(console);
        //Fight fight;
        int fightType;		
		int mode = 0;
        me.initMe(map, client);
        Screen screen = new Screen(console);
        String server = "localhost";
        if (args.length > 2)
        {
        	server = args[1];
        }
        client.initSocket(server, "27019");    
        if (client.connectToServer() > 0) System.exit(-1);
        String data = "";
        String errorText = "";
        boolean loginEnd = false;
        boolean loginContinue = false;        
        data = "";
        while (!data.equals("DISCONNECT OK"))
        {
            if (mode == 0)
            {
                int result = login.loginMode(client, inventory, map, me, sys);
                mode = 1;
                if (result < 0)
                {
                    data = "DISCONNECT OK"; 
                }
            } 
            else 
            {                 
                int res = screen.readKey();
                if (res > 0)
                {         
                    String s = "" + res;
                    s = "KBHIT: " + s;
                    int inventoryEvent = inventory.useEvent(res);
                    if (inventoryEvent > -1)
                    {
                         inventory.useInventoryItem(inventoryEvent);
                    }                               
                    else if ((res >= 48) && (res <= 57))
                    {
                        inventory.dropItem(res - 48);
                    }            
                    else if (res == 10)
                    {
                        fightType = interpretObjects(me.myX, me.myY, Utils.MODE_ACTIVATE);
                    }
                    if (res == 315)
                    {
                        sys.outputToSystemMessages("?:");
                        String toSend = "";
                        //cin >> toSend;
                        client.sendToServer(toSend);    
                        data = client.receive();
                        if (data != "MAPSEND START") 
                        {
                            sys.outputToSystemMessages(">" + data);
                        } 
                        else 
                        {
                            sys.outputToSystemMessages("?:");
                            String toSend2 = "";
                            //cin >> toSend;
                            client.sendToServer(toSend2);    
                            data = client.receive();
                            map.loadInicialData(data);                 
                        }                  
                    }
                    else if (res == 37)
                    {                    
                        me.makeAMove(me.myX - 1, me.myY);                      
                        fightType = interpretObjects(me.myX, me.myY, Utils.MODE_MOVE);
                    }
                    else if (res == 39)
                    {
                        me.makeAMove(me.myX + 1, me.myY);
                        fightType = interpretObjects(me.myX, me.myY, Utils.MODE_MOVE);              
                    }
                    else if (res == 38)
                    {
                        me.makeAMove(me.myX, me.myY - 1);
                        fightType = interpretObjects(me.myX, me.myY, Utils.MODE_MOVE);              
                    }
                    else if (res == 40)
                    {
                        me.makeAMove(me.myX, me.myY + 1);
                        fightType = interpretObjects(me.myX, me.myY, Utils.MODE_MOVE);              
                    }          
                    else if (res == 113)
                    {
                        client.sendToServer("DISCON:");
                        data = client.receive();
                    }    
                    else if (res == 114)
                    {
                        client.sendToServer("LOGOUT:");
                        data = client.receive();
                        mode = 0;  
                    }             
                    else 
                    {
                        sys.outputToSystemMessages(s);
                    }          
                }
            	else 
            	{
                    inactivity++;
                    if (inactivity > 300)
                    {
                        inactivity = 0;
                        if (client.sendToServer("UPDATE:") > 0)
                        {
                            sys.outputToSystemMessages("SERVER ERROR, DISCONNECTING:\n");                                                            
                            System.exit(-1);
                        }
                        data = client.receive();
                        map.analizeUpdate(data);
 
                    }
            	}
            }
        }
        System.exit(0);
		//sys.initSystemWindow();
		
    }
}
