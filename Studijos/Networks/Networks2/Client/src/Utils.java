/**
 * @(#)Utils.java
 *
 *
 * @author 
 * @version 1.00 2008/11/9
 */


public class Utils 
{
    public static final int MAP_WALL = 1;
    public static final int MAP_WATER = 2;
    public static final int MAP_SAND = 3;
    public static final int MAP_DARK_MATTER = 4;


    public static final int OBJ_USER = 1;
    public static final int OBJ_MONSTER = 2;
    public static final int OBJ_HEALTH_POINT = 3;
    public static final int OBJ_SWORD = 4;
    public static final int OBJ_SHIELD = 5;
    public static final int OBJ_MAGIC_WAND = 6;
    public static final int OBJ_NPC = 7;
    public static final int OBJ_ME = 8;

    public static final int MODE_MOVE = 1;
    public static final int MODE_ACTIVATE = 2;
    public static final int MODE_TALK = 3;

    public static final int UPD_MOVE = 0;
    public static final int UPD_ADD = 1;
    public static final int UPD_DEL = 2;
       
    public static String intToStr(int data)
    {
        return "" + data;
    }

    public static char chr(int data)
    {
        return (char)data;
    }


    public static int charToByte(char c)
    {
        switch (c)
        {
            case '0': return 0;
            case '1': return 1;
            case '2': return 2;
            case '3': return 3;
            case '4': return 4;
            case '5': return 5;
            case '6': return 6;
            case '7': return 7;
            case '8': return 8;
            case '9': return 9;
            case 'A': return 10;
            case 'B': return 11;
            case 'C': return 12;
            case 'D': return 13;
            case 'E': return 14;
            case 'F': return 15;
            default: return 0;
        }        
    }

    public static int getByteFromStr(String data, int place)
    {
        char high, low;
        high = data.charAt(place);
        low = data.charAt(place + 1);
        return charToByte(high) * 16 + charToByte(low);
    }    
    
}