/**
 * @(#)Screen.java
 *
 *
 * @author 
 * @version 1.00 2008/11/9
 */
import java.awt.Color;

public class Screen 
{

    public Screen(TextConsole console) 
    {
    	this.console = console;
    	screenMap = new TextLine[25];
    	for (int i = 0; i < 25; i++)
    	{
    		screenMap[i] = new TextLine();
    	}
    }
    
    TextConsole console = null;
    TextLine[] screenMap;
      
    private void paint()
    {
       for (int i = 10; i < 50; i++)
       {
         for (int j = 0; i < 18; i++)
         {
             paintPixel(i, j, screenMap[j].line[i]);
         }
       }    	
    }
    
    private void paintPixel(int x, int y, TPixel pix)
    {    	    		
    	console.setForeground(pix.bgColor);
    	console.setLocalBackground(pix.color);
    	console.write("" + pix.symbol, x, y);        
    }

    public void window(int x, int y, int x2, int y2, Color color, Color textColor, String caption)
    {
         //_setcursortype(_NOCURSOR);
         
         console.setLocalBackground(color);
         console.setForeground(textColor);
         console.gotoPosition(x, y);
         console.write("+");
         int length = caption.length();
         int cLen;
         String curCapt = caption;
         if (x2 - x + 1 >= length + 4)
         {
             cLen = length;
         } 
         else 
         {
             cLen = x2 - x + 1 - 4;
         }
         
         curCapt = caption.substring(0, cLen);
         console.write("-");
         console.write(curCapt);     
         for (int i = x + cLen + 2; i < x2; i++)
         {             
             console.write("-");
         }
         console.write("+");          
         for (int j = y + 1; j < y2; j++)
         {
         	console.gotoPosition(x, j);
            console.write("|");       
            for (int i = x + 1; i < x2; i++)
            {
            	console.write(" ");           
            }
            console.write("|");                                 
         }
         console.gotoPosition(x, y2);
         console.write("+");                                      
         for (int i = x + 1; i < x2; i++)
         {
         	console.write("-");                                          
         }
         console.write("+");              	
    }
    
    public void window(int y, int y2, Color color, Color textColor, String caption)
    {     
         console.setLocalBackground(color);
         console.setForeground(textColor);
         console.gotoPosition(1, y);     
         console.write("+");     
         int length = caption.length();
         int cLen;
         String curCapt = caption;
         if (80 - 1 + 1 >= length + 4)
         {
             cLen = length;
         } 
         else 
         {
             cLen = 80 - 1 + 1 - 4;
         }
         curCapt = caption.substring(0, cLen);
         console.write("-");
         console.write(curCapt);
         for (int i = 0 + cLen + 2; i < 79; i++)
         {
             console.write("-");
         }     
         console.write("+");         
         for (int j = y + 1; j < y2 - 1; j++)
         {            
            //console.gotoPosition(1, y + 1);
            console.gotoPosition(1, j);
            //insline();
            /*
            console.write("|");
            console.gotoPosition(80, y + 1);       
            */
            console.write("|");
            for (int i = 1; i < 79; i++)
            {
               console.write(" ");
            }       
            console.write("|");
         }         	
         console.gotoPosition(1, y2 - 1);	
         console.write("+");
         for (int i = 1; i < 79; i++)
         {
             console.write("-");
         }
         console.write("+");
     

         //     putchxy(80, y2 - 1, '+');
         //console.write(setxy(2, y + 1);    	
     	 console.gotoPosition(2, y + 1);
    }    
    		
    public int readKey()
    {
      synchronized(console)
      {
      
      int res = console.kbhit();
      if (res > 0)
      {
         res = console.getch();
         if (res == 0)
         {            
            res = console.getch();
            res = res + 256;
                 
         }
         if (res == 224)
         {            
            res = console.getch();
            res = res + 256 + 224;
                 
         }
      }
      return res;    	
      }
      
    }
    
}