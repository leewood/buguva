//
//  Project : OOP1
//  File Name : WorksJournal.java
//  Date : 2007.09.30
//  Author : Karolis Uosis
//
//

import java.util.*;

public class WorksJournal
{
    
    
    private boolean wasLastAppointmentSuccessful = true;
    private ArrayList<Repairable> techniques = new ArrayList<Repairable>();  
    private ArrayList<Client> clients = new ArrayList<Client>();
    private ArrayList<Employee> employees = new ArrayList<Employee>();
    
    public void addTechnique(Repairable technique)
    {
        techniques.add(technique);
    }
    
    public Repairable findTechnique(int id)
    {
        return (Repairable)techniques.get(id); 
    }
    
    public void removeTechnique(int id)
    {
        techniques.remove(id);
    }

    
    public Employee findFirstFreeEmployeeOfGivenQualification(int qualification)
    {
        Employee tmp = null;
        for (int x = 0; x < employees.size(); x++)
        {
            tmp = (Employee)employees.get(x);
            if ((tmp.isFree()) && (tmp.getQualification() >= qualification))
                return tmp;
        }
        
        return null;
    }
    
    public void addEmployee(Employee employee) throws PersonAlreadyExists, FailedToAdd
    {
        if (findEmployee(employee.getPersonalNo()) != null)
        {
            PrintListStream.println("Just now new employee failed to start to work");
            throw new PersonAlreadyExists();            
        }
        if (employees.add(employee))
        {
            PrintListStream.println("Just now new employee started to work");
            PrintListStream.println("He is " + employee.getInfo());
        } else {
            PrintListStream.println("Just now new employee failed to start to work");
            throw new FailedToAdd();
        }
    }
    
    public void removeEmployee(String personalNo)
    {
        Employee tmp = findEmployee(personalNo);
        if (tmp != null)
        {
            if (!tmp.isFree())
            {
                tmp.getJob().getOwner().failToService();
            }
            employees.remove(tmp);  
        }      
    }

    public void removeEmployee(int id)
    {
        Employee tmp = (Employee)employees.get(id);
        if (!tmp.isFree())
        {
            tmp.getJob().getOwner().failToService();
        }
        employees.remove(id);        
    }
    
    
    public Employee findEmployee(String personalNo)
    {
        Employee tmp = null;
        for (int x = 0; x < employees.size(); x++)
        {
            tmp = (Employee)employees.get(x);
            if (personalNo.equals(tmp.getPersonalNo()))
                return tmp;
        }
        return null;        
    }
    
    private void printList(List list, String caption)
    {
        PrintListStream.println("");
        PrintListStream.println(caption);
        for (int i = 0; i < list.size(); i++)
        {
            PrintListStream.println("" + i + " " +((Person)list.get(i)).getInfo());
        }

    }

    private List<String> returnList(List list)
    {
        List<String> lst = new ArrayList<String>();
        for (int i = 0; i < list.size(); i++)
        {
            lst.add("" + i + " " +((Person)list.get(i)).getInfo());
        }
        return lst;
    }
    
    public void printEmployeesList()
    {
        printList(employees, "Employees list:");
    }
    
    public void printClientsList()
    {
        printList(clients, "Clients list:");
    }
    
    public List<String> returnClientsList()
    {
        return returnList(clients);
    }

    public List<String> returnEmployeesList()
    {
        return returnList(employees);
    }
    
    public Client findFirstNonServicedClient()
    {
        Client tmp = null;
        for (int x = 0; x < clients.size(); x++)
        {
            tmp = (Client)clients.get(x);
            if (tmp.isNotServiced())
                return tmp;
        }
        return null;
    }
    
    public boolean isAnyBeingServicedClient()
    {
        for (int x = 0; x < clients.size(); x++)
        {
            if (((Client)clients.get(x)).isOnService())
                return true;
        }
        return false;
    }
    
    public Employee findEmployeeByRepairedTechnique(Repairable tech)
    {
        for (int i = 0; i < employees.size(); i++)
        {
            if (((Employee)employees.get(i)).getJob() == tech)
            {
                return (Employee)employees.get(i);
            }
        }
        return null;
    }
    
    public void addClient(Client client) throws ClientAlreadyExists, FailedToAdd
    {
        if (findClient(client.getName(), client.getSurname()) != null)
        {
           throw new ClientAlreadyExists();
        } else {
           if (clients.add(client))
           {
               PrintListStream.println("We have new client: " + client.getInfo());
           } else {
               throw new FailedToAdd();
           }
        }
    }
    
    public void removeClient(String name, String surname)
    {
        Client tmp = findClient(name, surname);
        if (tmp != null)
            removeClient(clients.indexOf(tmp));
    }
    
    public void removeClient(int id)
    {
        Client tmp = clients.get(id);
        Employee tmp2;
        if (tmp != null)
        {
            tmp2 = findEmployeeByRepairedTechnique(tmp.getWhatToRepair());
            if (tmp2 != null)
            {
                tmp2.stopWork();
            }
            techniques.remove(tmp.getWhatToRepair());
            clients.remove(id);
        }
    }
    
    public Client findClient(String name, String surname)
    {
        Client tmp = null;
        for (int x = 0; x < clients.size(); x++)
        {
            tmp = (Client)clients.get(x);
            if ((name.equals(tmp.getName())) && (surname.equals(tmp.getSurname())))
                return tmp;
        }
        return null;
    } 
    
   public boolean serviceFirst()
   {
       Employee emp;
       Client client;
       Repairable technique;
       client = findFirstNonServicedClient();
       if (client != null)
       {
           technique = client.getWhatToRepair();
           emp = findFirstFreeEmployeeOfGivenQualification(technique.getRequiedQualification());
           if ((emp != null))
           {
               emp.setJob(technique);
               client.startToService();
               emp.doWork();
               wasLastAppointmentSuccessful = true;
               return true; 
           } else {
               if (wasLastAppointmentSuccessful)
               {
                   PrintListStream.println("No free employees of requied qualification at the moment");
                   wasLastAppointmentSuccessful = false;
               }
               return false;
           }
       } else {
               if (wasLastAppointmentSuccessful)
               {
                   PrintListStream.println("No non-serviced clients at the moment ");
                   wasLastAppointmentSuccessful = false;
               }
           return false;
       }
   }    
   
   
}
