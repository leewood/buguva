/**
 * @(#)Map.java
 *
 *
 * @author 
 * @version 1.00 2008/11/9
 */
import java.awt.Color;

public class TMap 
{
 
    	
    public static final int MAX_OBJ_COUNT = 800;
    public static final int MAP_MAX_X = 40;
    public static final int MAP_MAX_Y = 18;

    private String topMap;
    private String leftMap;
    private String rightMap;
    private String bottomMap;
    private Me me;    
    private MapColumn[] map;
    private TextConsole console;
    
    public TMap(Me me, TextConsole console) 
    {
    	this.me = me;
    	objectsCount = 0;
    	objects = new ObjectDef[MAX_OBJ_COUNT];
    	map = new MapColumn[MAP_MAX_X];
    	this.console = console;
    	for (int i = 0; i < MAP_MAX_X; i++)
    	{
    		map[i] = new MapColumn();
    	}
    }
    
    public int mapType(int x, int y)
    {
        if ((x < MAP_MAX_X) && (y < MAP_MAX_Y) && (x >= 0) && (y >= 0))
        {
            return map[x].cell[y];
        } 
        else 
        {
            return 0;
        }
    }
    
    public Color mapBackground(int x, int y)
    {
        Color tBackground = Color.BLACK;
        int mapType = map[x].cell[y];
        switch (mapType)
        {
            case Utils.MAP_WALL:
                break;
            case Utils.MAP_WATER:
                tBackground = Color.BLUE;
                break;
            case Utils.MAP_SAND:
                tBackground = Color.YELLOW;
                break;
            case Utils.MAP_DARK_MATTER:
                tBackground = Color.BLACK;
                break;
            default:
                break;
        }
        return tBackground;     	
    }
    
    public void paintMap(int x, int y)
    {
        Color tColor = Color.WHITE;
        Color tBackground = mapBackground(x, y);
        char symbol = ' ';

        int mapType = map[x].cell[y];
        switch (mapType)
        {
            case Utils.MAP_WALL:
                symbol = '*';
                break;
            case Utils.MAP_WATER:
                symbol = ' ';
                break;
            case Utils.MAP_SAND:
                symbol = ' ';
                break;
            case Utils.MAP_DARK_MATTER:
                symbol = '!';
                break;
            default:
                break;
        }                
        console.setForeground(tColor);
        console.setLocalBackground(tBackground);
        console.write("" + symbol, x + 10, y + 1);   
    }
    
    void paintObject(int x, int y, int objectType)
    {
        Color tColor = Color.WHITE;
        Color tBackground = mapBackground(x, y);
        char symbol = ' ';        
        switch (objectType)
        {
            case Utils.OBJ_USER:
               symbol = '#';
               break;
            case Utils.OBJ_MONSTER:
               symbol = '$';
               tColor = Color.RED;
               break;
            case Utils.OBJ_HEALTH_POINT:
               symbol = 'H';
               tBackground = Color.RED;
               break;
            case Utils.OBJ_SWORD:
               symbol = '^';
               break;
            case Utils.OBJ_SHIELD:
               symbol = 'O';
               break;
            case Utils.OBJ_MAGIC_WAND:
               symbol = '\\';
               break;
            case Utils.OBJ_NPC:
               symbol = '#';
               tColor = Color.BLUE;
               break;
            case Utils.OBJ_ME:
               symbol = '#';
               tColor = Color.RED;
               break;
           default:
               symbol = ' ';
               break;
        }
        console.setForeground(tColor);
        console.setLocalBackground(tBackground);
        console.write("" + symbol, x + 10, y + 1);      
    }
    
    void paintObjects()
    {
        for (int i = 0; i < objectsCount; i++)
        {
            paintObject(objects[i].x, objects[i].y, objects[i].type);
        }    	
    }
    
    void paintHighObject(int x, int y)
    {
         for (int i = objectsCount - 1; i >= 0; i--)
         {
             if ((objects[i].x == x) && (objects[i].y == y))
             {
                 paintObject(x, y, objects[i].type);
                 break;
             }
         }       	
    }
    
    void moveObject(int oldx, int oldy, int newx, int newy, int objectType)
    {
        paintMap(oldx, oldy);
        paintHighObject(oldx, oldy);
        paintObject(newx, newy, objectType);    	
    }
    
    void paintFullMap()
    {
        for (int y = 0; y < 17; y++)
           for (int x = 0; x < 40; x++)
           {
               paintMap(x, y);
           }    	
    }
    
    int objectsInXYCount(int x, int y)
    {
        int count = 0;
        for (int i = 0; i < objectsCount; i++)
        {
            if ((objects[i].x == x) && (objects[i].y == y))
            {
                count++;
            }
        }
        return count;    	
    }
    
    int objectsInXY(int x, int y, int objectNr)
    {
        int found = 0;
        int count = 0;
        for (int i = 0; i < objectsCount; i++)
        {
            if ((objects[i].x == x) && (objects[i].y == y))
            {
                count++;
                if (count == objectNr)
                {
                    return objects[i].type;
                }
            }
        }
        return 0; 	    	
    }
    
    void addObject(int x, int y, int type, int index, int atk, int def, int magic, int health, String name)
    {
        ObjectDef obj = new ObjectDef();
        obj.x = x;
        obj.y = y;
        obj.type = type;
        obj.atk = atk;
        obj.def = def;
        obj.magic = magic;
        obj.health = health;
        obj.name = name;
        if (objectsCount - 1 < index)
        {
            objectsCount = index + 1;
        }
        objects[index] = obj;
        paintObject(x, y, type);    	
    }
    
    void delObject(int id)
    {
        int x = objects[id].x;
        int y = objects[id].y;
        paintMap(x, y);
        for (int i = id; i < objectsCount - 1; i++)
        {
            objects[id].type = objects[id + 1].type;
            objects[id].x = objects[id + 1].x;        
            objects[id].y = objects[id + 1].y;
            objects[id].atk = objects[id + 1].atk;
            objects[id].def = objects[id + 1].def;
            objects[id].magic = objects[id + 1].magic;
            objects[id].health = objects[id + 1].health;  
            objects[id].name = objects[id + 1].name;                                      
        }

        objectsCount--;
        if (me.myID >= id)
        {
            me.myID--;
        }
        paintHighObject(x, y);
    }
    
    
    void loadInicialData(String data)
    {
        int place = 0;
        for (int y = 0; y < 18; y++)
          for (int x = 0; x < 40; x++)
          {
              int val = Utils.getByteFromStr(data, place);
              place += 2;
              map[x].cell[y] = val;            
          }
        int count = Utils.getByteFromStr(data, place);
        place += 2;
        objectsCount = count;
        for (int i = 0; i < count; i++)
        {
            int x = Utils.getByteFromStr(data, place);
            place += 2;
            int y = Utils.getByteFromStr(data, place);
            place += 2;
            int type = Utils.getByteFromStr(data, place);
            place += 2;
            int atk = Utils.getByteFromStr(data, place);
            place += 2;
            int def = Utils.getByteFromStr(data, place);
            place += 2;
            int magic = Utils.getByteFromStr(data, place);
            place += 2;
            int health = Utils.getByteFromStr(data, place);
            place += 2;
            
            String name = data.substring(place, place + 19);
            place += 20;           
            ObjectDef obj = new ObjectDef();
            obj.x = x;
            obj.y = y;
            obj.type = type;
            obj.atk = atk;
            obj.def = def;
            obj.magic = magic;
            obj.health = health;
            obj.name = name;
            objects[i] = obj;
        }
        paintFullMap();
        paintObjects(); 
    }
    
    void analizeUpdate(String data)
    {
        int x, y, x2, y2, type, id, atk, def, magic, health;
        String name;
        if (!data.equals("N"))
        {
            int pos = 0;
            while (pos < data.length())
            {       
                int opt = Utils.getByteFromStr(data, pos);
       
                switch (opt)
                {
                    case Utils.UPD_MOVE:
                       x = Utils.getByteFromStr(data, pos + 2);
                       y = Utils.getByteFromStr(data, pos + 4);
                       x2 = Utils.getByteFromStr(data, pos + 6);
                       y2 = Utils.getByteFromStr(data, pos + 8);
                       id = Utils.getByteFromStr(data, pos + 10);
                       pos += 12;
                       objects[id].x = x2;
                       objects[id].y = y2;
                       moveObject(x, y, x2, y2, objects[id].type);
                       break;
                    case Utils.UPD_ADD:
                       x = Utils.getByteFromStr(data, pos + 2);
                       y = Utils.getByteFromStr(data, pos + 4);
                       type = Utils.getByteFromStr(data, pos + 6);
                       id = Utils.getByteFromStr(data, pos + 8);
                       atk = Utils.getByteFromStr(data, pos + 10);
                       def = Utils.getByteFromStr(data, pos + 12);
                       magic = Utils.getByteFromStr(data, pos + 14);
                       health = Utils.getByteFromStr(data, pos + 16);
                       name = data.substring(pos + 18, pos + 18 + 19);
                       pos += 38;
                       addObject(x, y, type, id, atk, def, magic, health, name);
                       break;
                    case Utils.UPD_DEL:
                       id = Utils.getByteFromStr(data, pos + 2);
                       delObject(id);
                       pos += 4;
                       break;
                    case 3:
                       id = Utils.getByteFromStr(data, pos + 2);
                       x =  Utils.getByteFromStr(data, pos + 4);
                       y = Utils.getByteFromStr(data, pos + 6);
                       type = Utils.getByteFromStr(data, pos + 8);
                       atk = Utils.getByteFromStr(data, pos + 10);
                       def = Utils.getByteFromStr(data, pos + 12);
                       magic = Utils.getByteFromStr(data, pos + 14);
                       health = Utils.getByteFromStr(data, pos + 16);
                       int oldx;
                       oldx = (objects[id]).x;
                       int oldy; 
                       oldy = (objects[id]).y;
                       objects[id].atk = atk;
                       objects[id].def = def;
                       objects[id].magic = magic;
                       objects[id].health = health;
                       objects[id].x = x;
                       objects[id].y = y;
                       objects[id].type = type;
                       moveObject(oldx, oldy, x, y, type);
                       pos += 18;
                    default: 
                       break;          
                }
       
            }
        }  	
    }
    
    public int objectsCount;
    public ObjectDef[] objects;    
    
}