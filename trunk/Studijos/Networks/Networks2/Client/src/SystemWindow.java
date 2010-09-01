/**
 * @(#)SystemWindow.java
 *
 *
 * @author 
 * @version 1.00 2008/11/9
 */
import java.awt.Color;

public class SystemWindow 
{

    public SystemWindow(TextConsole console) 
    {
    	this.console = console;
    	screen = new Screen(console);
    	lines = new String[6];
    }
    
    int linesNr;
    String[] lines;
    Screen screen = null;
    TextConsole console = null;
    
    private void delline(int i)
    {
    	if ((i <= 6) && (i > 0))
    	{
    		for (int j = i - 1; j < 5; j++)
    		{
    			lines[j] = lines[j + 1];
    		}
    		lines[5] = "";
    	}
    }
    
    public void initSystemWindow()
    {
        linesNr = 0;
        screen.window(18, 26, Color.RED, Color.WHITE, "[F1 - System messages]-[F2 - Disconnect]-[F3 - Logout]");       	
    }
    
    public void outputToSystemMessages(String data)
    {
         linesNr++;
         console.setLocalBackground(Color.RED);
         console.setForeground(Color.WHITE);   
         if (linesNr > 6)
         {
         	delline(1);
         	for (int i = 0; i < 5; i++)
         	{
         		String outS = lines[i];
         		if (outS.length() > 78)
         		{
         			outS = outS.substring(0, 78);
         		}
         		while (outS.length() < 78)
         		{
         			outS += " ";
         		}
         		
         		console.gotoPosition(2, 19 + i);
         		console.write(outS);
         	}
         	lines[5] = data;
            console.gotoPosition(2, 24);      
         } 
         else 
         {
         	lines[linesNr - 1] = data;
         	console.gotoPosition(2, 18 + linesNr);
            
         }
         if (data.length() > 78)
         {
         	data = data.substring(0, 77);
         }
         if (linesNr > 6)
         {         
             while (data.length() < 78)
             {
         	    data += " ";
             }
         }
         console.write(data);   
    }
    
}