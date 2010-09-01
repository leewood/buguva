/*
 * FailedToAdd.java
 *
 * Created on Sekmadienis, 2007, Gruodþio 9, 21.47
 *
 */


public class FailedToAdd extends Exception{
    
    /** Creates a new instance of FailedToAdd */
    public FailedToAdd() {
    }
    
    public String toString()
    {
        return "Error!! Failed to add to the list";
    }
}
