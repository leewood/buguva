public class read
{
    public static String readString()
    {
        String tmp = new String();
        int i = 0;
        try
        {
            while (i != 13)
            {    
                i = System.in.read();
                if (i != 13)
                {
                    tmp += (char)i;
                }
            }
        } catch (Exception e) {}
        return tmp;
    }
    
    public static int showMenu()
    {
        int i = 0;
        System.out.println("What do you want to do?: ");
        System.out.println("1. Add employee");
        System.out.println("2. Add client");
        System.out.println("3. Add technique");
        System.out.println("4. Start simulation");
        System.out.print("Choose: ");
        try
        {
            i = System.in.read();
            System.in.read();
            System.in.read();
        } catch (Exception e) {}
        return i;
    }
    
    public static WorksJournal initData()
    {
        int chose = 0;
        String tmp, tmp2, tmp3, tmp4;
        WorksJournal journal = new WorksJournal();
        while ((chose = showMenu() - 48) < 4)
        {
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
                    tmp4 = readString();
                    journal.addEmployee(new Employee(tmp, tmp2, tmp3));//,  Integer.parseInt(tmp4)));
                    break;
                case 2:
                    break;
                case 3:
                    
            }
        }
       return  journal;
    }
}