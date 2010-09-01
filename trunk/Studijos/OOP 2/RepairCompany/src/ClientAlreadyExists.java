/*
 * ClientAlreadyExists.java
 *
 * Created on Sekmadienis, 2007, Gruodþio 9, 21.58
 *
 */



public class ClientAlreadyExists  extends Exception{
    
    /** Creates a new instance of PersonAlreadyExists */
    public ClientAlreadyExists() {
    }
    
    public String toString()
    {
        return "Error!! Such client already exists";
    }
}