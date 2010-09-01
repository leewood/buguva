/**
 * @(#)MapColumn.java
 *
 *
 * @author 
 * @version 1.00 2008/11/9
 */


public class MapColumn 
{

    public MapColumn() 
    {
    	cell = new int[Map.MAP_MAX_Y];
    	for (int i = 0; i < Map.MAP_MAX_Y; i++)
    	{
    		cell[i] = 0;
    	}
    }

    public int[] cell;    
    
}