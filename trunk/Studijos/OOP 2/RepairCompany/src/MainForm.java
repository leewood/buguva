/*
 * MainForm.java
 *
 * Created on Sekmadienis, 2007, Lapkrièio 18, 14.42
 */

import java.util.*;
public class MainForm extends javax.swing.JFrame implements Runnable
{
    boolean auto = false;
    private WorksJournal journal = new WorksJournal();
    
    public MainForm() 
	{
        initComponents();
    }
    
    
    private void initComponents() {
	
        lClientsListScroll = new javax.swing.JScrollPane();
        lClientsList = new javax.swing.JList();
        lEmployeesListScroll = new javax.swing.JScrollPane();
        lEmployeesList = new javax.swing.JList();
        bAddClient = new javax.swing.JButton();
        tLogScroll = new javax.swing.JScrollPane();
        tLog = new javax.swing.JTextArea();
        bAddEmployee = new javax.swing.JButton();
        bServiceFirst = new javax.swing.JButton();
        bStartCompany = new javax.swing.JButton();
        bRemoveClient = new javax.swing.JButton();
        bRemoveEmployee = new javax.swing.JButton();
        tlLog = new javax.swing.JLabel();
        tlEmployees = new javax.swing.JLabel();
        tlClients = new javax.swing.JLabel();
        bStartAutoMode = new javax.swing.JButton();
        bStopAutoMode= new javax.swing.JButton();
        
        
        setDefaultCloseOperation(javax.swing.WindowConstants.EXIT_ON_CLOSE);
        this.setSize(750, 600);      

        lClientsListScroll.setViewportView(lClientsList);
        lEmployeesListScroll.setViewportView(lEmployeesList);

        bAddClient.setText("Add");
	bAddClient.setEnabled(false);
                
        bAddClient.addMouseListener(new java.awt.event.MouseAdapter() 
	{
            public void mouseClicked(java.awt.event.MouseEvent evt) 
	    {
                 if (bAddClient.isEnabled())
                 {
                     FAddClient emp1 = new FAddClient(journal);
                     emp1.setVisible(true);
                 }
            }
        });

        tLog.setColumns(20);
        tLog.setRows(5);
        tLogScroll.setViewportView(tLog);

        bAddEmployee.setText("Add");
        bAddEmployee.setEnabled(false);
        bAddEmployee.addMouseListener(new java.awt.event.MouseAdapter() 
	{
            public void mouseClicked(java.awt.event.MouseEvent evt) 
	    {
               if (bAddEmployee.isEnabled())
               {    
                   FNewEmpl newEmpl = new FNewEmpl(journal);
                   newEmpl.setVisible(true);
               }
            }
        });

        bServiceFirst.setText("Service First Unserviced Client");
        bServiceFirst.setEnabled(false);
        bServiceFirst.addMouseListener(new java.awt.event.MouseAdapter() 
	{
            public void mouseClicked(java.awt.event.MouseEvent evt) 
	    {
                if (bServiceFirst.isEnabled())
                   journal.serviceFirst();
            }
        });

        bStartCompany.setText("Start Company");
        bStartCompany.addMouseListener(new java.awt.event.MouseAdapter() 
	{
            public void mouseClicked(java.awt.event.MouseEvent evt) 
	    {
                if (bStartCompany.isEnabled())
                    start(evt);
            }
        });

        bRemoveClient.setText("Remove");
        bRemoveClient.setEnabled(false);
        bRemoveClient.addMouseListener(new java.awt.event.MouseAdapter() 
	{
            public void mouseClicked(java.awt.event.MouseEvent evt) 
	    {
                if (bRemoveClient.isEnabled())
                {
                    int i = lClientsList.getSelectedIndex();
                    journal.removeClient(i);        
                }
            }
        });

        bRemoveEmployee.setText("Remove");
        bRemoveEmployee.setEnabled(false);
        bRemoveEmployee.addMouseListener(new java.awt.event.MouseAdapter() 
	{
            public void mouseClicked(java.awt.event.MouseEvent evt) 
	    {
                if (bRemoveEmployee.isEnabled())
                {
                    int i = lEmployeesList.getSelectedIndex();
                    journal.removeEmployee(i);
                }
            }
        });

        tlLog.setText("Log:");

        tlEmployees.setText("Employees:");

        tlClients.setText("Clients:");

        bStartAutoMode.setText("Work in auto mode");
        bStartAutoMode.setEnabled(false);
        bStartAutoMode.addMouseListener(new java.awt.event.MouseAdapter() 
	{
            public void mouseClicked(java.awt.event.MouseEvent evt) 
	    {
                if (bStartAutoMode.isEnabled())
                    setAutoMode();
            }
        });

        bStopAutoMode.setText("Stop auto mode");
        bStopAutoMode.setEnabled(false);
        bStopAutoMode.addMouseListener(new java.awt.event.MouseAdapter() 
	{
            public void mouseClicked(java.awt.event.MouseEvent evt) 
	    {
                if (bStopAutoMode.isEnabled())
                    stopAutoMode();
            }
        });

        java.awt.Container layout = this.getContentPane();        
        layout.setLayout(null);
        bAddClient.setBounds(1, 1, 100, 50);
        layout.add(bAddClient);
        bRemoveClient.setBounds(1, 60, 100, 50);
        layout.add(bRemoveClient);
        tlClients.setBounds(110, 1, 100, 15);
        layout.add(tlClients);
        lClientsListScroll.setBounds(110, 20, 350, 200);
        layout.add(lClientsListScroll);
        bAddEmployee.setBounds(1, 250, 100, 50);
        layout.add(bAddEmployee);
        bRemoveEmployee.setBounds(1, 310, 100, 50);
        layout.add(bRemoveEmployee);
        tlEmployees.setBounds(110, 250, 100, 15);
        layout.add(tlEmployees, java.awt.BorderLayout.NORTH);        
        lEmployeesListScroll.setBounds(110, 270, 450, 150);
        layout.add(lEmployeesListScroll, java.awt.BorderLayout.SOUTH);
        bStartCompany.setBounds(480, 20, 250, 50);
        layout.add(bStartCompany);
        bServiceFirst.setBounds(480, 80, 250, 50);
        layout.add(bServiceFirst);
        bStartAutoMode.setBounds(480, 140, 250, 50);
        layout.add(bStartAutoMode);
        bStopAutoMode.setBounds(480, 200, 250, 50);
        layout.add(bStopAutoMode);
        tlLog.setBounds(110, 430, 100, 15);
        layout.add(tlLog, java.awt.BorderLayout.NORTH);        
        tLogScroll.setBounds(110, 450, 450, 120);
        layout.add(tLogScroll, java.awt.BorderLayout.SOUTH);
    
    }

    private void stopAutoMode() 
	{
        auto = false;
        bServiceFirst.setEnabled(true);
        bStartAutoMode.setEnabled(true);
        bStopAutoMode.setEnabled(false);        
    }

    private void setAutoMode() 
	{
        auto = true;
        bServiceFirst.setEnabled(false);
        bStartAutoMode.setEnabled(false);
        bStopAutoMode.setEnabled(true);          
    }





    public WorksJournal getJournal()
    {
        return journal;
    }


    private void start(java.awt.event.MouseEvent evt) 
	{
        RTechnique tech;
        journal = new WorksJournal();
        PrintListStream.print("Just now new Repair Company started to work\n");
        
        try
        {
            journal.addEmployee(new Employee("Jonas", "Jonaitis", "38612290310", Employee.NEWBIE));
            journal.addEmployee(new Employee("Giedrius", "Jaurynas", "38712290310", Employee.NEWBIE));
            journal.addEmployee(new Employee("Saulius", "Salcius", "38610290310", Employee.PRO));
            journal.addEmployee(new Employee("Andrius", "Giedra", "38509290310", Employee.FIRST_LEVEL));
            journal.addEmployee(new Employee("Petras", "Undartas", "38612290300", Employee.PRO));
            PrintListStream.println(journal.findEmployee("38509290310").getSurname());
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
        } catch (Throwable e) {
            PrintListStream.println("Error occured!!!" + e.toString());
        } finally {
           bAddClient.setEnabled(true);
           bAddEmployee.setEnabled(true);
           bServiceFirst.setEnabled(true);
           bStartCompany.setEnabled(false);
           bRemoveClient.setEnabled(true);
           bRemoveEmployee.setEnabled(true);
           bStartAutoMode.setEnabled(true);
           th.start();
        }
    }



    Thread th = new Thread(this);
    public void run()
    {
       while (true)
       {
           updateClientsList();
           updateEmployeesList();
           if (auto)
           {
               journal.serviceFirst();
           }
           tLog.setText(PrintListStream.getOutput());
           try
		   {
               th.sleep(100);
           } catch (Exception e){};
       }
    }

    
    private void updateClientsList()
    {
        List<String> lst = journal.returnClientsList();
        int i = lClientsList.getSelectedIndex();
        lClientsList.setListData(lst.toArray());
        lClientsList.setSelectedIndex(i);

    }
    
    private void updateEmployeesList()
    {
        List<String> lst = journal.returnEmployeesList();
        int i = lEmployeesList.getSelectedIndex();
        lEmployeesList.setListData(lst.toArray());
        lEmployeesList.setSelectedIndex(i);
    }    
    
    public static void main(String args[]) 
	{
        java.awt.EventQueue.invokeLater(new Runnable() 
		{
            public void run() 
			{
                new MainForm().setVisible(true);
            }
        });
   
       
    }
    
    
    private javax.swing.JButton bAddClient;
    private javax.swing.JButton bAddEmployee;
    private javax.swing.JButton bServiceFirst;
    private javax.swing.JButton bStartCompany;
    private javax.swing.JButton bRemoveClient;
    private javax.swing.JButton bRemoveEmployee;
    private javax.swing.JButton bStartAutoMode;
    private javax.swing.JButton bStopAutoMode;
    private javax.swing.JLabel tlEmployees;
    private javax.swing.JLabel tlClients;
    private javax.swing.JLabel tlLog;
    private javax.swing.JList lClientsList;
    private javax.swing.JList lEmployeesList;
    private javax.swing.JScrollPane lClientsListScroll;
    private javax.swing.JScrollPane lEmployeesListScroll;
    private javax.swing.JScrollPane tLogScroll;
    private javax.swing.JTextArea tLog;
    
    
}
