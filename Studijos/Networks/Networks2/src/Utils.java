/**
 * @(#)Utils.java
 *
 *
 * @author 
 * @version 1.00 2008/11/8
 */


public class Utils 
{
	public static String fixedSizeString(String source, int size)
    {
        String result;
        if (source.length() > size)
        {
           result = source.substring(0, size);
        } 
		else 
        {
           result = source;
           for (int i = source.length(); i < size; i++)
           {
              result += " ";
           }
        }
        return result;
    }

    public static String subByte(int cbyte)
    {
        switch (cbyte)
        {
            case 0: return "0";
            case 1: return "1";
            case 2: return "2";
            case 3: return "3";
            case 4: return "4";
            case 5: return "5";
            case 6: return "6";
            case 7: return "7";
            case 8: return "8";
            case 9: return "9";
            case 10: return "A";
            case 11: return "B";
            case 12: return "C";
            case 13: return "D";
            case 14: return "E";                                                                                                
            case 15: return "F";        
        }
        return "0";
    }


    public static String byteToStr(int cbyte)
    {
        int lowSubByte = cbyte % 16;
        int highSubByte = cbyte / 16;
        String result = "";
        result += subByte(highSubByte) + subByte(lowSubByte);
        return result;
    }
    
    String intToStr(int data)
    {
        return "" + data;
    }
}