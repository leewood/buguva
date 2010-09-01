//
//  Project : OOP1
//  File Name : RTechnika.java
//  Date : 2007.09.30
//  Author : Karolis Uosis
//
// Class RTechnique realization




public class RTechnique implements Repairable
{
        
        
        private boolean repairable = true;
        private boolean repaired = false;
	private String description = "";
	private static int techniquesNumber = 0;
        private int ownNumber = 0;
        private int requiedQualificationToRepair = 0;
        private Client owner = null;
        
        RTechnique()
        {
            initID();
        }
	
	RTechnique(int requiedQualificationToRepair)
	{
            this.requiedQualificationToRepair = requiedQualificationToRepair;
            initID();
        }
	
	RTechnique(boolean repairable, int requiedQualificationToRepair)
	{
	    this.repairable = repairable;
            this.requiedQualificationToRepair = requiedQualificationToRepair;
            initID();
	}
	
	RTechnique(String description, boolean repairable, int requiedQualificationToRepair)
	{
	    this.repairable = repairable;
	    this.description = description;	
            this.requiedQualificationToRepair = requiedQualificationToRepair;
	}
	
	RTechnique(String description, int requiedQualificationToRepair)
	{
	    this.description = description;	
            this.requiedQualificationToRepair = requiedQualificationToRepair;
            initID();
        }
	
	private void initID()
        {
            techniquesNumber++;
            ownNumber = techniquesNumber;                    
        }
        
	public boolean isRepairable() 
	{
	    return repairable;
	}
	
	public void setAsNotRepairable() 
	{
	    repairable = NOT_REPAIRABLE;
	}
	
	public void setAsRepairable() 
	{
	    repairable = REPAIRABLE;
	}
	
        public int getRequiedQualification()
        {
            return requiedQualificationToRepair;
        }
        
        public int getID()
        {
            return ownNumber;
        }
        
	public String getDescription() 
	{
	    return description;
	}
	
	public void setDescription(String description) 
	{
	    this.description = description;
	}
	
        public void doRepair()
        {
            if (isRepairable())
            {
                repaired = true; 
                System.out.println("Technique called " + description + " was repaired");
            }
        }
        
        public boolean isRepaired()
        {
            return repaired;
        }
        
        public Client getOwner()
        {
            return owner;
        }
        public void setOwner(Client owner)
        {
            this.owner = owner;
        }

        public int getRequiedTimeToRepair(int qualification)
        {
            return 2 * getRequiedQualification() * 5 - qualification * 5;
        }
}
