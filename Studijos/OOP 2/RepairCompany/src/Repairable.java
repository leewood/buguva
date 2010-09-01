//
//  Project : OOP1
//  File Name : RTechnika.java
//  Date : 2007.09.30
//  Author : Karolis Uosis
//
// 




interface Repairable
{

     public static final boolean REPAIRABLE = true;
     public static final boolean NOT_REPAIRABLE = false;        
        
     public boolean isRepairable(); 
     public int getRequiedQualification();   
     public boolean isRepaired();
     public int getRequiedTimeToRepair(int qualification);
     public void doRepair();
     public Client getOwner();
     public void setOwner(Client owner);
     public String getDescription();

}
