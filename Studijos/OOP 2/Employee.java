
//
//  Project : OOP1
//  File Name : Employee.java
//  Date : 2007.09.30
//  Author : Karolis Uosis
//
//


import java.lang.*;

public class Employee extends Person implements Runnable
{
    public static final int NEWBIE = 0;
    public static final int FIRST_LEVEL = 1;
    public static final int PRO = 10;
    
    private Thread th = new Thread(this);
    
    private int qualification = 0;
    private String personalNo = "";
    private Repairable jobOnProgress = null;
    private int completedJobs = 0;
    private int failedJobs = 0;
    private int workedTimeOnCurrentJob = 0;
    private int requiedTimeToEndCurrentJob = 0; 
    
    Employee(String name, String surname) 
    {
        this.name = name;
        this.surname = surname;
        this.qualification = 0;
        this.personalNo = "";
    }
	
    Employee(String name, String surname, int qualification)
    {
        this.name = name;
        this.surname = surname;
        this.qualification = qualification;	
        this.personalNo = "";
    }
    
    
    Employee(String name, String surname, String pNo)
    {
        this.name = name;
        this.surname = surname;
	this.personalNo = pNo;
	this.qualification = 0;
    }
    
    Employee(String name, String surname, String pNo, int qualification)
    {
        this.name = name;
        this.surname = surname;
        this.personalNo = pNo;
        this.qualification = qualification;
    }
    
    public void run()
    {
        Client owner;
        System.out.println(getInfo() + " tries to repair");
        while (workedTimeOnCurrentJob <= requiedTimeToEndCurrentJob)
        {
            workedTimeOnCurrentJob++;
            try
            {
                th.sleep(100); 
            } catch (InterruptedException e)
            {
            }
        }
        if (jobOnProgress.getRequiedQualification() <= getQualification())
        {
            jobOnProgress.doRepair();
            owner = jobOnProgress.getOwner();
            if (jobOnProgress.isRepaired())
            {
                completedJobs++;
                if (completedJobs % 10 == 0)
                {
                    incQualification();
                }
                owner.doService();
            } else {
                failedJobs++;
                if (completedJobs % 10 == 0)
                {
                    decQualification();
                }
                owner.failToService();
            }
        }
        jobOnProgress = null;
        
    }
    

	
    public void setPersonalNo(String pNo)
    {
	if (personalNo.compareTo("") == 0)
	{
	    if (pNo != null)
	    {
	        personalNo = pNo;
	    }
	} else {
	    System.out.println("Sorry this person already has his personal no");
	}
    }
	
    public String getPersonalNo()
    {
        return personalNo;
    }
	  
	
    public void incQualification() 
    {
	qualification++;
    }
	
    public void decQualification()
    {
	if (qualification > 0)
	{
	    qualification--;
	} else {
	    System.out.println("Sorry employee qualification can't be lower");
	}
	   
    }
	
    public int getQualification() 
    {
        return qualification;
    }
	
	
    public boolean isFree()
    {
        return (jobOnProgress == null);
    }
    
    
    public String getInfo()
    {
        return name + " " + surname + " (personalNo: " + personalNo + "; qualification: " + qualification + ") ";
    }

    public boolean setJob(Repairable job)
    {
        if (isFree())
        {
            jobOnProgress = job;
            if (job != null)
            {
                requiedTimeToEndCurrentJob = jobOnProgress.getRequiedTimeToRepair(qualification);
                workedTimeOnCurrentJob = 0;
            }
            System.out.println("Our employee " + getInfo() + " gets new technique to repair. It's called " + job.getDescription());
            return true;
        } else {
            System.out.println("Our employee" + getInfo() + " already has job to do");
            return false;
        }
    }

    public Repairable getJob()
    {
        return jobOnProgress;
    }
    
    public void doWork()
    {
        if (jobOnProgress != null)
        {
            th = new Thread(this);
            th.start();
        } 
    }
    
    public boolean equals(Object o)
    {
        Employee emp = (Employee)o;
        if (!getSurname().equals(""))
            {
                return (emp.getSurname().equals(surname) && emp.getName().equals(name));
            } 
        else if (!getPersonalNo().equals(""))
            {
                return emp.getPersonalNo().equals(personalNo);
            } else {
                return (emp.isFree() && (qualification <= emp.getQualification()));
            }
    }
}
