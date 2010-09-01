public class InputReader
{
    public static final int CHOSE_EXIT = 7;
    public static String readString()
    {
        String tmp = new String();
        int i = 0;
        try
        {
            while (i != 13)
            {    
                i = System.in.read();
                if ((i != 13) && (i != 10))
                {
                    tmp += (char)i;
                }
            }
        } catch (Exception e) {}
        return tmp;
    }
    
    public static int readInteger()
    {
        String tmp = readString();
        try
        {
            return Integer.parseInt(tmp);
        } catch (Exception e)
        {
            return 0;
        }
    }
    
    public static int showMenu()
    {
        int i = 0;
        System.out.println("");
        System.out.println("");
        System.out.println("What do you want to do?: ");
        System.out.println("1. Add employee");
        System.out.println("2. Kick employee out");
        System.out.println("3. See employees list");
        System.out.println("4. Add client");
        System.out.println("5. See clients list");
        System.out.println("6. Service first unserviced client");
        System.out.println("7. Exit");
        System.out.print("Choose: ");
        try
        {
            i = readInteger();
        } catch (Exception e) {}
        return i;
    }
    
    public static int initData(WorksJournal journal)
    {
        int i, chose = 0;
        String tmp, tmp2, tmp3, tmp4;
        RTechnique tech;
        chose = showMenu();
        
            switch (chose)
            {
                case 1:
                    System.out.print("Enter employee name: ");
                    tmp = readString();
                    System.out.print("Enter employee surname: ");
                    tmp2 = readString();
                    System.out.print("Enter employee personal No: ");
                    tmp3 = readString();
                    System.out.print("Enter employee qualification: ");
                    journal.addEmployee(new Employee(tmp, tmp2, tmp3,  readInteger()));
                    break;
                case 2:
                    System.out.println("Choose which to kick out:");
                    journal.printEmployeesList();
                    System.out.print("Choose:");
                    i = readInteger();
                    journal.removeEmployee(i);
                    break;
                case 3:
                    journal.printEmployeesList();
                    break;
                case 4:
                    System.out.print("Enter client name: ");
                    tmp = readString();
                    System.out.print("Enter client surname: ");
                    tmp2 = readString();
                    System.out.print("Enter his technique description: ");
                    tmp3 = readString();
                    System.out.print("Enter requied qualification to repair his technique:");
                    tech = new RTechnique(tmp3, readInteger());
                    journal.addTechnique(tech);
                    journal.addClient(new Client(tmp, tmp2, tech));
                    break;
                case 5:
                    journal.printClientsList();
                    break;
                case 6:
                    journal.serviceFirst();
                    break;
            }
            
        
       return  chose;
    }
}