
//
//  Project : OOP1
//  File Name : RepairCompany.java
//  Date : 2007.09.30
//  Author : Karolis Uosis
//
//
import java.util.*;

public class RepairCompany 
{
    	
    public static void main(String args[])	
    {
        RTechnique tech;
        WorksJournal journal = new WorksJournal();
        System.out.print("Just now new Repair Company started to work\n");
        journal.addEmployee(new Employee("Jonas", "Jonaitis", "38612290310", Employee.NEWBIE));
        journal.addEmployee(new Employee("Giedrius", "Jaurynas", "38712290310", Employee.NEWBIE));
        journal.addEmployee(new Employee("Saulius", "Salcius", "38610290310", Employee.PRO));
        journal.addEmployee(new Employee("Andrius", "Giedra", "38509290310", Employee.FIRST_LEVEL));
        journal.addEmployee(new Employee("Petras", "Undartas", "38612290300", Employee.PRO));
        
        System.out.println(journal.findEmployee("38509290310").getSurname());
        tech = new RTechnique("New computer", RTechnique.REPAIRABLE, Employee.PRO);
        journal.addTechnique(tech);
        journal.addClient(new Client("Jonas", "Petraitis", tech));
        
        tech = new RTechnique("Some useless junk", RTechnique.REPAIRABLE, Employee.NEWBIE);
        journal.addTechnique(tech);
        journal.addClient(new Client("Petras", "Petraitis", tech));	  
        
        tech = new RTechnique("Old TV", RTechnique.REPAIRABLE, Employee.FIRST_LEVEL);
        journal.addTechnique(tech);
        journal.addClient(new Client("Agne", "Pukyte", tech)); 
        
        tech = new RTechnique("Old Radio", RTechnique.REPAIRABLE, Employee.FIRST_LEVEL);
        journal.addTechnique(tech);
        journal.addClient(new Client("Sandra", "Pukyte", tech));         

        while (InputReader.initData(journal) != InputReader.CHOSE_EXIT)
        {
        }
        System.out.println("Finishing uncomplete jobs");
        while (journal.isAnyBeingServicedClient())
        {
        }
        System.out.println("No more work for today left. Let's end it here.");
    }
};
