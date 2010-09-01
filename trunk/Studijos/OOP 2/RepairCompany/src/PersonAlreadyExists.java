/*
 * PersonAlreadyExists.java
 *
 * Created on Sekmadienis, 2007, Gruodþio 9, 13.04
 *
 * To change this template, choose Tools | Template Manager
 * and open the template in the editor.
 */


public class PersonAlreadyExists  extends Exception{
    
    /** Creates a new instance of PersonAlreadyExists */
    public PersonAlreadyExists() {
    }
    
    public String toString()
    {
        return "Error!! Such personal No already exists";
    }
}
