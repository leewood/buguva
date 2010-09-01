
//
//  Project : OOP1
//  File Name : Client.java
//  Date : 2007.09.30
//  Author : Karolis Uosis
//
//




public class Client extends Person
{
    public static final int SERVICED = 1;
    public static final int ON_SERVICE = 0;
    public static final int NOT_SERVICED = -1; 
    
    private int status = NOT_SERVICED;
    private Repairable whatToRepair = null;
                
    Client(String name, String surname, Repairable whatToRepair) 
    {
        this.name = name;
        this.surname = surname;  
        this.whatToRepair = whatToRepair;
        if (whatToRepair != null)
        {
            whatToRepair.setOwner(this);
        }
    }
	
    Client(String name, String surname, int status) 
    {
	this.name     = name;
        this.surname  = surname;  
        this.status   = status;
    }

    Client(String name, String surname) 
    {
	this.name     = name;
        this.surname  = surname;  
    }

    Client(Employee emp, int status)
    {
	this.name     = emp.getName();
        this.surname  = emp.getSurname();  
        this.status   = status;
    }
    
	
    public boolean isNotServiced() 
    {
	return (status == NOT_SERVICED);
    }
    
    public boolean isOnService()
    {
        return (status == ON_SERVICE);
    }
    
    public int getStatus()
    {
        return status;
    }
    
    public void failToService()
    {
        System.out.println("Client " + getInfo() + " was not successfully serviced");
        status = NOT_SERVICED;
    }
    
    public void startToService()
    {
        status = ON_SERVICE;
    }
    
    public void doService() 
    {
        System.out.println("Client " + getInfo() + " was successfully serviced");
        status = SERVICED;
    }
    
    public Repairable getWhatToRepair()
    {
        return whatToRepair;
    }
    
    
    public String getInfo()
    {
        return name + " " + surname + " (Status: " + ((status == NOT_SERVICED)? "Not serviced":((status == ON_SERVICE)? "On sevice": "Serviced")) + ")";
    }
    
    
    public boolean equals(Object o)
    {
        Client client = (Client)o;
        if (!getSurname().equals(""))
        {
            return (client.getSurname().equals(surname) && client.getName().equals(name));
        } else {
            return (client.getStatus() == status);
        }
    }    

}
