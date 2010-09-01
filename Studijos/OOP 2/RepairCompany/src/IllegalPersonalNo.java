/*
 * IllegalPersonalNo.java
 *
 * Created on Pirmadienis, 2007, Gruodþio 10, 09.27
 *
 */


public class IllegalPersonalNo extends Exception {
    
    /** Creates a new instance of IllegalPersonalNo */
    private String error = "";
    public IllegalPersonalNo(String error) 
    {
        this.error = error;
    }
    
    static public void checkPersonalNo(String personalNo) throws IllegalPersonalNo
    {
        if (personalNo.length() != 11)
        {
            throw new IllegalPersonalNo("Wrong personal no. Incorrect number of symbols");
        } else {
            for (int i = 0; i < personalNo.length(); i++)
                if ((personalNo.charAt(i) > '9') || (personalNo.charAt(i) < '0'))
                    throw new IllegalPersonalNo("Wrong personal no. Must be digits");
        }
    }
    
    public String toString()
    {    
        return error;
    }
}
