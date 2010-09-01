/*
 * FNewEmpl.java
 *
 * Created on Sekmadienis, 2007, Gruodþio 9, 12.14
 */


public class FNewEmpl extends javax.swing.JFrame 
{
    WorksJournal journal;
   
    public FNewEmpl(WorksJournal journal) 
    {
        initComponents();
        this.journal = journal;
    }
    

    private void initComponents() 
    {
        tlPersonalNo = new javax.swing.JLabel();
        tlQualification = new javax.swing.JLabel();
        tlSurname = new javax.swing.JLabel();
        tlName = new javax.swing.JLabel();
        tPersonalNoEdit = new javax.swing.JScrollPane();
        tPersonalNo = new javax.swing.JTextPane();
        tQualificationEdit = new javax.swing.JScrollPane();
        tQualification = new javax.swing.JTextPane();
        tSurnameEdit = new javax.swing.JScrollPane();
        tSurname = new javax.swing.JTextPane();
        tNameEdit = new javax.swing.JScrollPane();
        tName = new javax.swing.JTextPane();
        bAdd = new javax.swing.JButton();
        tlError = new javax.swing.JLabel();

        tlPersonalNo.setText("Personal NO:");

        tlQualification.setText("Qualification:");

        tlSurname.setText("Surname:");

        tlName.setText("Name:");
        tlError.setText(" ");
        tPersonalNoEdit.setViewportView(tPersonalNo);

        tQualificationEdit.setViewportView(tQualification);

        tSurnameEdit.setViewportView(tSurname);

        tNameEdit.setViewportView(tName);

        bAdd.setText("Add");
        bAdd.addMouseListener(new java.awt.event.MouseAdapter() 
	{
            public void mouseClicked(java.awt.event.MouseEvent evt) 
	    {
                addEmployee();
            }
        });

        tlError.setForeground(new java.awt.Color(255, 0, 51));

        java.awt.Container layout = this.getContentPane();        
        layout.setLayout(null);        
	tlName.setBounds(10, 10, 250, 15);
	layout.add(tlName);
	tNameEdit.setBounds(10, 30, 250, 25);
	layout.add(tNameEdit);
	tlSurname.setBounds(10, 55, 250, 15);
	layout.add(tlSurname);
	tSurnameEdit.setBounds(10, 70, 250, 25);
	layout.add(tSurnameEdit);
	tlPersonalNo.setBounds(10, 95, 250, 15);
	layout.add(tlPersonalNo);
	tPersonalNoEdit.setBounds(10, 110, 250, 25);
	layout.add(tPersonalNoEdit);
	tlQualification.setBounds(10, 135, 250, 15);
	layout.add(tlQualification);
	tQualificationEdit.setBounds(10, 150, 250, 25);
	layout.add(tQualificationEdit);
	bAdd.setBounds(10, 175, 250, 40);
	layout.add(bAdd);
	tlError.setBounds(10, 215, 320, 15);
        layout.add(tlError);
	this.setSize(320, 300);
        
    }

    private void addEmployee() 
    {
        try
        {
           IllegalPersonalNo.checkPersonalNo(tPersonalNo.getText()); 
           journal.addEmployee(new Employee(tName.getText(), tSurname.getText(), tPersonalNo.getText(), Integer.parseInt(tQualification.getText())));    
           tlError.setText(" ");
           this.setVisible(false);
           
        } catch (IllegalPersonalNo e) {
           tlError.setText(e.toString());
        } catch (PersonAlreadyExists e) {
           tlError.setText(e.toString());  
        } catch (FailedToAdd e) {
           tlError.setText(e.toString()); 
        } catch (NumberFormatException e) {
           tlError.setText("Error!!! Qualification must be number!!!");
        }  
    }
    
    
    private javax.swing.JButton bAdd;
    private javax.swing.JLabel tlError;
    private javax.swing.JLabel tlName;
    private javax.swing.JLabel tlSurname;
    private javax.swing.JLabel tlPersonalNo;
    private javax.swing.JLabel tlQualification;
    private javax.swing.JScrollPane tPersonalNoEdit;
    private javax.swing.JScrollPane tNameEdit;
    private javax.swing.JScrollPane tQualificationEdit;
    private javax.swing.JScrollPane tSurnameEdit;
    private javax.swing.JTextPane tQualification;
    private javax.swing.JTextPane tSurname;
    private javax.swing.JTextPane tPersonalNo;
    private javax.swing.JTextPane tName;
    
    
}
