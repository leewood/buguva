/**
 * @(#)MapDef.java
 *
 *
 * @author 
 * @version 1.00 2008/11/8
 */


public class MapDef 
{
    public static final int MAX_OBJECTS = 20;
    public MapDef() 
    {
    	objects = new ObjectDef[MAX_OBJECTS];
    }
    
    public ObjectDef[] objects;
    public int objectsCount;    
    	
    Enemy calculateMonster(int x, int y)
    {
    	return calculateObjects(x, y, -1);
    }
    
    Enemy calculateObjects(int x, int y, int ignore)
    {
        Enemy result = new Enemy();
        result.attack = 0;
        result.defence = 0;
        result.magic = 0;
        result.health = 0;
        for (int i = 0; i < objectsCount; i++)
        {
            if ((objects[i].type == ObjectDef.OBJ_USER) && (objects[i].x == x) && (objects[i].y == y) && (i != ignore))
            {
                result.attack += objects[i].atk;
                result.defence += objects[i].def;
                result.magic += objects[i].magic;
                result.health += objects[i].health;
            }
        }
        return result;    	
    }
    
    void removeObject(int i)
    {
       if (i < objectsCount)
       {
           for (int j = i; j < objectsCount - 1; j++)
           {             
              objects[j].type = objects[j + 1].type;
              objects[j].x = objects[j + 1].x;
              objects[j].y = objects[j + 1].y;
              objects[j].atk = objects[j + 1].atk;
              objects[j].def = objects[j + 1].def;
              objects[j].magic = objects[j + 1].magic;
              objects[j].health = objects[j + 1].health;
              objects[j].ci = objects[j + 1].ci;
              objects[j].name = objects[j + 1].name;                                                                                                                
           }         
           objectsCount--;           
       }   	
    }    
}