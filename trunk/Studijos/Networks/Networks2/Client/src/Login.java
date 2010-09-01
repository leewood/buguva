/**
 * @(#)Login.java
 *
 *
 * @author 
 * @version 1.00 2008/11/9
 */
import java.awt.Color;
import java.awt.event.KeyEvent;

public class Login 
{

    TextConsole console;
    
    public Login(TextConsole console)
    {
    	screen = new Screen(console);
    	this.console = console;
    }
    
    Screen screen;
      
    public void paintLoginScreen()
    {
    	console.clear();
    	console.setForeground(Color.BLUE);
    	console.gotoPosition(30, 2);
        console.write("Welcome To ZZT RPG");
        screen.window(20, 10, 60, 16, Color.RED, Color.WHITE, "Login:");
    	console.gotoPosition(25, 12);
        console.write("Login name:");
    	console.gotoPosition(25, 15);
        console.write("[F1 - CANCEL]  [Enter - OK]");     
    	console.gotoPosition(25, 13);     
        console.setLocalBackground(Color.BLACK);        
        console.write("                                ");
    	console.gotoPosition(25, 13);    	
    }
    
    public String loginModeResult()
    {
        String current = "";
        int res = 0;
        while ((res != KeyEvent.VK_ENTER) && (res != 315))
        {
            res = screen.readKey();
            if (res == 8)
            {
            	if (current.length() >= 1)
            	{            	
                    current = current.substring(0, current.length() - 1);
            	}
            	else
            	{
            		current = "";
            	}
                console.gotoPosition(25, 13);
                console.setLocalBackground(Color.BLACK);                
                console.write("                                ");
                console.gotoPosition(25, 13);
                console.write(current);
            }
            else if ((res > 30) && (res < 256))
            {
                current = current + Utils.chr(res);
                console.write("" + Utils.chr(res));
            }
            else if (res > 0)
            {
            	System.out.println("KBHIT:" + res);
            }            
             
        }       
        if (res == 315)
        {
            return "DISCON:";
        } 
        else 
        {
            return current;
        }    	
    }
    
    public void paintLoginError(String text)
    {
    	console.gotoPosition(25, 17);
    	console.setForeground(Color.RED);
    	console.write(text);
    	console.gotoPosition(25, 13);
        console.setForeground(Color.WHITE);
    }
    
    public int loginMode(NetworkClient client, Inventory inventory, TMap map, Me me, SystemWindow sys)
    {
        String data = "";
        int result = 0;
        paintLoginScreen();
        while (result == 0)
        {
            String loginName = loginModeResult();    
            if ((!loginName.equals("DISCON:")) && (!loginName.equals("")))
            {           
                client.sendToServer("LOGIN:");            
                data = client.receive();  
                client.sendToServer(loginName);            
                data = client.receive();  
                if (data.equals("ERROR2"))
                {
                    paintLoginError("Such login name already exists");          
                }
                else if (data.equals("ERROR1"))
                {
                    paintLoginError("Server is full");
                    client.sendToServer("DISCON:");   
                    data = client.receive();         
                    result = -1;
                } 
                else 
                {
                    result = 1;
                    console.setBackground(Color.BLACK);
                    console.clear();                     
                    sys.initSystemWindow(); 
                    client.sendToServer("MAPSEND:");
                    client.receive();  
                    client.sendToServer("1.map");    
                    data = client.receive();
                    map.loadInicialData(data);
                    me.myID = map.objectsCount;
                    for (int i = 0; i < map.objectsCount; i++)
                    {
                        if (map.objects[i].type == Utils.OBJ_ME)
                        {
                            me.myID = i;
                        }
                    }
                    me.myX = map.objects[me.myID].x;
                    me.myY = map.objects[me.myID].y;
                    me.myAttack = map.objects[me.myID].atk;
                    me.myDefence = map.objects[me.myID].def;
                    me.myMagic = map.objects[me.myID].magic;
                    me.myHealth = map.objects[me.myID].health;            
                    //map.paintObject(me.myX, me.myY, OBJ_ME);            
                    inventory.initInventory(me, sys, client, map, console);            
            
                }
            }
            else 
            {
            
                if (loginName.equals(""))
                {
                    paintLoginError("Login name can't be blank");          
                }
                else 
                {
                    client.sendToServer("DISCON:");
                    data = client.receive();
                    result = -1;
                }
            }
        }
      
        return result;     	
    }
    
}