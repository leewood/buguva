/**
 * @(#)Inventory.java
 *
 *
 * @author 
 * @version 1.00 2008/11/9
 */
import java.awt.Color;

public class Inventory 
{
    public static final int INVENTORY_SIZE = 10;

    public Inventory() 
    {
    	inventory = new ObjectDef[INVENTORY_SIZE];
    	inventoryItemsCount = 0;    
    	inventoryUseKeys = 	new int[INVENTORY_SIZE];
    }
        
    ObjectDef[] inventory;
    int inventoryItemsCount;
    Screen screen;
    Me me;
    SystemWindow sys;
    NetworkClient client;
    TMap map;
    TextConsole console;
    
    public int[] inventoryUseKeys;
    public int useEvent(int key)
    {
        for (int i = 0; i < 10; i ++)
        {
            if (inventoryUseKeys[i] == key)
            {
                return i;
            }
        }
        return -1;    	
    }
    
    public void inventoryDataLine(String data, int line)
    {
     
         console.setLocalBackground(Color.BLUE);
         console.setForeground(Color.WHITE);
         console.gotoPosition(75, line + 1);
         console.write(data);     
    }
    
    public void addToInventory(ObjectDef objType)
    {
        if (inventoryItemsCount >= 10)
        {
            sys.outputToSystemMessages("NO MORE SLOTS IN INVENTORY LEFT");        
        } 
        else 
        {
            inventoryItemsCount++;
            inventory[inventoryItemsCount - 1] = objType;
            String caption = "";    	        
            caption = objType.name;
            if (caption.length() > 15)
            {
               caption = caption.substring(0, 15);
            }
            inventoryCaptionLine(Utils.intToStr(inventoryItemsCount) + ". " + caption, inventoryItemsCount + 8);        
        }        
    }
    
    public void useInventoryItem(int itemNr)
    {
        if (itemNr < inventoryItemsCount)
        {
            ObjectDef obj = inventory[itemNr];
            switch (obj.type)
            {
                case Utils.OBJ_SWORD:
                   if (me.hasGun)
                   {
                       addToInventory(me.gun);
                   }
                   me.hasGun = true;
                   me.gun = obj;
                   me.myAttack += obj.atk;                   
                   break;
                case Utils.OBJ_SHIELD:
                   if (me.hasShield)
                   {
                       addToInventory(me.shield);
                   }
                   me.hasShield = true;
                   me.shield = obj;
                   me.myDefence += obj.def;
                   break;
                case Utils.OBJ_MAGIC_WAND:
                   if (me.hasMagicWand)
                   {
                       addToInventory(me.wand);
                   }
                   me.hasMagicWand = true;
                   me.wand = obj;
                   me.myMagic += obj.magic;
                   break;
                case Utils.OBJ_HEALTH_POINT:
                   me.myHealth += obj.health;
                   break;
            }
            client.sendToServer("OBJUPDATE:");
            client.receive();
            client.sendToServer(Utils.intToStr(me.myID) + " " + Utils.intToStr(me.myAttack) + " " + Utils.intToStr(me.myDefence) + " " + Utils.intToStr(me.myMagic) + " " + Utils.intToStr(me.myHealth));
            client.receive();     
            removeInventoryItem(itemNr);
            updateMyInfoInInventory();
        }
        else
        {
            sys.outputToSystemMessages("There is not any item in slot " + Utils.intToStr(itemNr));
        }    	
    }
    
    public void dropItem(int itemNr)
    {
        itemNr = itemNr + 1;
        if ((itemNr <= inventoryItemsCount) && (itemNr >= 0))
        {   
            ObjectDef obj = new ObjectDef();
            obj.atk = inventory[itemNr].atk;
            obj.def = inventory[itemNr].def;
            obj.magic = inventory[itemNr].magic;
            obj.health = inventory[itemNr].health;
            obj.x = inventory[itemNr].x;
            obj.y = inventory[itemNr].y;
            obj.type = inventory[itemNr].type;
            obj.name = inventory[itemNr].name;
            removeInventoryItem(itemNr);
            map.objects[map.objectsCount] = obj;
            map.objectsCount++;
            map.paintObject(me.myX, me.myY, obj.type);         
            client.sendToServer("DROPOBJ:");
            client.receive();
            client.sendToServer(Utils.intToStr(obj.atk) + " " + Utils.intToStr(obj.def) + " " + Utils.intToStr(obj.magic) + " " + Utils.intToStr(obj.health) + " " + Utils.intToStr(obj.type) + " " + Utils.intToStr(obj.x) + " " + Utils.intToStr(obj.y) + " " + obj.name);
            client.receive();              
        } 
        else
        {
            sys.outputToSystemMessages("There is not any item in slot " + Utils.intToStr(itemNr));
        }      	
    }
    public void takeItem()
    {
    
        ObjectDef obj = new ObjectDef();
        int result = map.objectsCount;
        for (int i = map.objectsCount - 1; ((i >= 0) && (result == map.objectsCount)); i--)
        {        
            ObjectDef tmp = new ObjectDef();
            tmp.type = map.objects[i].type;
            tmp.x = map.objects[i].x;
            tmp.y = map.objects[i].y;
            tmp.atk = map.objects[i].atk;
            tmp.def = map.objects[i].def;
            tmp.magic = map.objects[i].magic;
            tmp.health = map.objects[i].health;
            tmp.name = map.objects[i].name;
            int type = tmp.type;
            if ((type >= Utils.OBJ_HEALTH_POINT) && (type <= Utils.OBJ_MAGIC_WAND) && (tmp.x == me.myX) && (tmp.y == me.myY))
            {
               obj.x = tmp.x;
               obj.y = tmp.y;
               obj.type = tmp.type;
               obj.name = tmp.name;
               obj.atk = tmp.atk;
               obj.def = tmp.def;
               obj.magic = tmp.magic;
               obj.health = tmp.health;
               result = i;   
            }
        }

        if (result >= 0)
        {        
            map.delObject(result);
            client.sendToServer("REMOBJ:");
            client.receive();
            client.sendToServer(Utils.intToStr(result));
            client.receive();
        } 
        addToInventory(obj);    	
    }
    
    public void paintInventory()
    {
        for (int i = 0; i < 7; i++)
        {
            if (i < inventoryItemsCount)
            {
                String caption = "";
                ObjectDef objType = inventory[i];
                caption = objType.name;
                inventoryCaptionLine(Utils.intToStr(i + 1) + ". " + caption, i + 9);               
            } 
            else 
            {
                inventoryCaptionLine("                  ", i + 9);
            }     
        }
    }
    
    public void removeInventoryItem(int itemNr)
    {
        inventoryItemsCount--;
        for (int i = itemNr - 1; i < inventoryItemsCount; i++)
        {
            inventory[i - 1].x = inventory[i + 1].x;
            inventory[i].y = inventory[i + 1].y;
            inventory[i].type = inventory[i + 1].type;
            inventory[i].atk = inventory[i + 1].atk;
            inventory[i].def = inventory[i + 1].def;
            inventory[i].magic = inventory[i + 1].magic;
            inventory[i].health = inventory[i + 1].health;
            inventory[i].name = inventory[i + 1].name;
        } 
        paintInventory();    	
    }
    
    public void inventoryCaptionLine(String data, int line)
    {
         console.setLocalBackground(Color.BLUE);
         console.setForeground(Color.WHITE);
         console.gotoPosition(62, line + 1);
         console.write(data);     	
    }
    
    public void initInventory(Me me, SystemWindow sys, NetworkClient client, TMap map, TextConsole console)
    {
        inventoryUseKeys[0] = 2048;  
        inventoryUseKeys[1] = 2049;  
        inventoryUseKeys[2] = 2050;  
        inventoryUseKeys[3] = 2051;  
        inventoryUseKeys[4] = 2052;  
        inventoryUseKeys[5] = 2053;  
        inventoryUseKeys[6] = 2054;  
        inventoryUseKeys[7] = 2055;  
        inventoryUseKeys[8] = 2056;  
        inventoryUseKeys[9] = 2057;  
        inventoryItemsCount = 0;
        this.me = me;
        this.sys = sys;
        this.client = client;
        this.map = map;
        this.console = console;
        screen = new Screen(console);
        screen.window(60, 1, 80, 17, Color.BLUE, Color.WHITE, "Inventory");
        inventoryCaptionLine("Level:", 1);
        inventoryCaptionLine("Attack lvl:", 2);
        inventoryCaptionLine("Defence lvl:", 3);
        inventoryCaptionLine("Magic lvl:", 4);
        inventoryCaptionLine("Health:", 5);    
        inventoryCaptionLine("  [Items]", 7);
        inventoryDataLine(Utils.intToStr(me.myAttack + me.myDefence + me.myMagic), 1);
        inventoryDataLine(Utils.intToStr(me.myAttack), 2);
        inventoryDataLine(Utils.intToStr(me.myDefence), 3);
        inventoryDataLine(Utils.intToStr(me.myMagic), 4);
        inventoryDataLine(Utils.intToStr(me.myHealth), 5);    	
    }
    
    public void updateMyInfoInInventory()
    {
        inventoryDataLine(Utils.intToStr(me.myAttack + me.myDefence + me.myMagic), 1);
        inventoryDataLine(Utils.intToStr(me.myAttack), 2);
        inventoryDataLine(Utils.intToStr(me.myDefence), 3);
        inventoryDataLine(Utils.intToStr(me.myMagic), 4);    	
    }  
    
}