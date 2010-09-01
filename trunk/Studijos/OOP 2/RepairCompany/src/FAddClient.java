/*
 * FAddClient.java
 *
 * Created on Pirmadienis, 2007, Gruodþio 3, 10.31
 * Form for adding new client
 */


public class FAddClient extends javax.swing.JFrame 
{
    WorksJournal journal;
    
    public FAddClient(WorksJournal journal) 
    {
        this.journal = journal;
        initComponents();
    }
    
    private void initComponents() 
    {
        tlQualification = new javax.swing.JLabel();
        tlDescription = new javax.swing.JLabel();
        tlSurname = new javax.swing.JLabel();
        tlName = new javax.swing.JLabel();
        tDescriptionEdit = new javax.swing.JScrollPane();
        tDescription = new javax.swing.JTextPane();
        tSurnameEdit = new javax.swing.JScrollPane();
        tSurname = new javax.swing.JTextPane();
        tNameEdit = new javax.swing.JScrollPane();
        tName = new javax.swing.JTextPane();
        tQualificationEdit = new javax.swing.JScrollPane();
        tQualification = new javax.swing.JTextPane();
        bAdd = new javax.swing.JButton();
        tlError = new javax.swing.JLabel();
        tlQualification.setText("Requied Qualification to repair");

        tlDescription.setText("Technique Description");

        tlSurname.setText("Client Surname");

        tlName.setText("Client Name");
        bAdd.setText("Add");
        tDescriptionEdit.setViewportView(tDescription);

        tSurnameEdit.setViewportView(tSurname);

        tNameEdit.setViewportView(tName);

        tQualificationEdit.setViewportView(tQualification);

        bAdd.addMouseListener(new java.awt.event.MouseAdapter() 
	{
            public void mouseClicked(java.awt.event.MouseEvent evt) 
	    {
                addClient();
            }
        });

        tlError.setForeground(new java.awt.Color(255, 0, 51));
        tlError.setText("   ");
      
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
	tlDescription.setBounds(10, 95, 250, 15);
	layout.add(tlDescription);
	tDescriptionEdit.setBounds(10, 110, 250, 25);
	layout.add(tDescriptionEdit);
	tlQualification.setBounds(10, 135, 250, 15);
	layout.add(tlQualification);
	tQualificationEdit.setBounds(10, 150, 250, 25);
	layout.add(tQualificationEdit);
	bAdd.setBounds(10, 175, 250, 40);
	layout.add(bAdd);
	tlError.setBounds(10, 215, 250, 15);
        layout.add(tlError);
	this.setSize(300, 300);
    }

    private void addClient() 
    {
        try
        {
            Repairable tmp = new RTechnique(tDescription.getText(), Integer.parseInt(tQualification.getText()));
            journal.addTechnique(tmp);
            journal.addClient(new Client(tName.getText(), tSurname.getText(), tmp));
            setVisible(false);
        } catch (FailedToAdd e) {
            tlError.setText(e.toString());
        } catch (ClientAlreadyExists e) {
            tlError.setText(e.toString());
        } catch (NumberFormatException e) {
            tlError.setText("Error!!! Qualification must be number!!!");
        }
        
    }
    
 
    private javax.swing.JButton bAdd;
    private javax.swing.JLabel tlName;
    private javax.swing.JLabel tlSurname;
    private javax.swing.JLabel tlDescription;
    private javax.swing.JLabel tlQualification;
    private javax.swing.JLabel tlError;
    private javax.swing.JScrollPane tNameEdit;
    private javax.swing.JScrollPane tSurnameEdit;
    private javax.swing.JScrollPane tDescriptionEdit;
    private javax.swing.JScrollPane tQualificationEdit;
    private javax.swing.JTextPane tName;
    private javax.swing.JTextPane tSurname;
    private javax.swing.JTextPane tDescription;
    private javax.swing.JTextPane tQualification;
  
}
