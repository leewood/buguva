/*
 * PrintListStream.java
 *
 * Created on Sekmadienis, 2007, Gruodþio 9, 16.40
 *
 * To change this template, choose Tools | Template Manager
 * and open the template in the editor.
 */


public class PrintListStream {
    static private String str = "";
    /** Creates a new instance of PrintListStream */
    public PrintListStream() {
    }
    
    static public void print(String s)
    {
        str += s;
    }
    
    static public void println(String s)
    {
        print(s + "\n");
    }
    static public String getOutput()
    {
        return str;
    }
}
