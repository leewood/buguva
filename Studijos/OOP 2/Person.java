
//
//  Project : OOP1
//  File Name : Client.java
//  Date : 2007.09.30
//  Author : Karolis Uosis
//
//




public abstract class Person
{
    
    protected String  name = "";
    protected String  surname = "";
                
    
    public String getName() 
    {
	return name;
    }
	
    public String getSurname() 
    {
	return surname;
    }
	
    public void setName(String name) 
    {
	this.name = name;
    }
	
    public void setSurname(String surname) 
    {
	this.surname = surname;
    }
	
    
    public abstract String getInfo();
   
}
