/**
 * @(#)ObjectDef.java
 *
 *
 * @author 
 * @version 1.00 2008/11/8
 */


public class ObjectDef 
{
    public static final int OBJ_USER = 1;
    public static final int OBJ_MONSTER = 2;
    public static final int OBJ_HEALTH_POINT = 3;
    public static final int OBJ_SWORD = 4;
    public static final int OBJ_SHIELD = 5;
    public static final int OBJ_MAGIC_WAND = 6;
    public static final int OBJ_NPC = 7;
    public static final int OBJ_ME = 8;

    public ObjectDef() 
    {
    }
     
    public int x, y, type, realID;
    public int atk, def, magic, health;
    public String name;
    
    
}

