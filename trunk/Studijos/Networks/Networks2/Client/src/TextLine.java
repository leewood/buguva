/**
 * @(#)TextLine.java
 *
 *
 * @author 
 * @version 1.00 2008/11/9
 */


public class TextLine 
{

    TPixel[] line;
    public TextLine() 
    {
    	line = new TPixel[80];
    	for (int i = 0; i < 80; i++)
    	{
    		line[i] = new TPixel();
    	}
    }
    
    
}