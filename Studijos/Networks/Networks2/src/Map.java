/**
 * @(#)Map.java
 *
 *
 * @author 
 * @version 1.00 2008/11/8
 */
import java.io.FileInputStream;
import java.io.*;
import java.util.Scanner;

public class Map 
{
    public static final int MAX_MAPS = 10;
    public Map() 
    {
    	mapNames = new String[MAX_MAPS];
    	maps = new MapDef[MAX_MAPS];
    	mapsCount = 0;
    }
    
    private String[] mapNames;
    private int mapsCount;    
    public MapDef[] maps;
    
    public void init()
    {
    	mapsCount = 0;
    }
    
    public int findMap(String fileName)
    {
       for (int i = 0; i < mapsCount; i++)
       {
          if (mapNames[i].equals(fileName))
          {
              return i;
          }
       }
       return -1;    	
    }
    
    void addMap(String fileName)
    {
        if (findMap(fileName) < 0)
        {
           mapsCount++;
           mapNames[mapsCount - 1] = fileName;
           maps[mapsCount - 1] = new MapDef();
           maps[mapsCount - 1].objectsCount = 0;
        }    	
    }
    
    String loadMap(String fileName)
    {
        String newStr = fileName;
        return this.loadMap(newStr, -1);    	
    }
    
    String loadMap(String fileName, int id)
    {
    	FileInputStream file = null;
    	try
    	{    	
            file = new FileInputStream(fileName);
    	} catch (FileNotFoundException e)
    	{
    		return "";
    	}
        Scanner fileStream = new Scanner(file);
        String result = "";              
        String otherMaps;        
        //maps
        otherMaps = fileStream.next();
        otherMaps = fileStream.next();
        otherMaps = fileStream.next();
        otherMaps = fileStream.next();
        for (int y = 0; y < 18; y++)
          for (int x = 0; x < 40; x++)
          {
             int data;
             data = fileStream.nextInt();
             result += Utils.byteToStr(data);
          }
        int count;
        //objects

        count = fileStream.nextInt();     
        int mapPlace = this.findMap(fileName);
        boolean updateMap = false;
        if (mapPlace < 0)
        {
            result += Utils.byteToStr(count);
            this.addMap(fileName);             
            mapPlace = this.findMap(fileName);
            updateMap = true;
            this.maps[mapPlace].objectsCount = count;
            for (int i = 0; i < count; i++)
            {
                int x, y, type, atk, def, magic, health;
                String name;
                x = fileStream.nextInt();
                y = fileStream.nextInt();
                type = fileStream.nextInt();
                atk = fileStream.nextInt();
                def = fileStream.nextInt();
                magic = fileStream.nextInt();
                health = fileStream.nextInt();
                name = fileStream.next();             
                if (i == id) 
                {
                   type = ObjectDef.OBJ_ME;
                }             
                result += Utils.byteToStr(x) + Utils.byteToStr(y) + Utils.byteToStr(type) + Utils.byteToStr(atk) +
                	      Utils.byteToStr(def) + Utils.byteToStr(magic) + Utils.byteToStr(health) + Utils.fixedSizeString(name, 20);
             
                if (updateMap)
                {
                   maps[mapPlace].objects[i] = new ObjectDef();
                   maps[mapPlace].objects[i].x = x;
                   maps[mapPlace].objects[i].y = y;
                   maps[mapPlace].objects[i].type = type;
                   maps[mapPlace].objects[i].atk = atk;
                   maps[mapPlace].objects[i].def = def;
                   maps[mapPlace].objects[i].magic = magic;
                   maps[mapPlace].objects[i].health = health;
                   maps[mapPlace].objects[i].name = name;
                }
            }
        }
        else 
        {
           int tcount = this.maps[mapPlace].objectsCount;
           result += Utils.byteToStr(tcount);
           for (int i = 0; i < tcount; i++)
           {
              int x, y, type, atk, def, magic, health;
              String name;
              x = maps[mapPlace].objects[i].x;
              y = maps[mapPlace].objects[i].y;
              type = maps[mapPlace].objects[i].type;
              if (i == id) 
              {
                 type = ObjectDef.OBJ_ME;
              }                          
              atk = maps[mapPlace].objects[i].atk;
              def = maps[mapPlace].objects[i].def;
              magic = maps[mapPlace].objects[i].magic;
              health = maps[mapPlace].objects[i].health;
              name = maps[mapPlace].objects[i].name;
              result += Utils.byteToStr(x) + Utils.byteToStr(y) + Utils.byteToStr(type) + Utils.byteToStr(atk) + 
              	        Utils.byteToStr(def) + Utils.byteToStr(magic) + Utils.byteToStr(health) + Utils.fixedSizeString(name, 20);
           }
         
        }
        try
        {
        	file.close();
        } catch (IOException ie)
        {
        	
        }
        
        return result;     	
    }
    
}