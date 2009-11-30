/*
 * BioInfoView.java
 */

package bioinfo;

import java.awt.FlowLayout;
import org.jdesktop.application.Action;
import org.jdesktop.application.ResourceMap;
import org.jdesktop.application.SingleFrameApplication;
import org.jdesktop.application.FrameView;
import org.jdesktop.application.TaskMonitor;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.util.Iterator;
import java.util.Set;
import javax.swing.Timer;
import javax.swing.Icon;
import javax.swing.JDialog;
import javax.swing.JFrame;
import org.biojava.bio.BioException;
import org.biojava.bio.gui.glyph.TurnGlyph;
import org.biojava.bio.gui.sequence.FeatureBlockSequenceRenderer;
import org.biojava.bio.gui.sequence.GlyphFeatureRenderer;
import org.biojava.bio.gui.sequence.MultiLineRenderer;
import org.biojava.bio.gui.sequence.SequencePanelWrapper;
import org.biojava.bio.gui.sequence.SequenceRenderer;
import org.biojava.bio.gui.sequence.SymbolSequenceRenderer;
import org.biojava.bio.program.gff.GFFRecordFilter.FeatureFilter;
import org.biojava.bio.proteomics.Digest;
import org.biojava.bio.seq.Feature;
import org.biojava.bio.seq.FeatureHolder;
import org.biojava.bio.seq.Sequence;
import org.biojava.bio.seq.impl.ViewSequence;
import org.biojava.bio.symbol.SymbolList;
import org.biojava.utils.ChangeVetoException;
import org.biojavax.Note;
import org.biojavax.bio.BioEntry;
import org.biojavax.bio.db.ncbi.GenbankRichSequenceDB;
import org.biojavax.bio.seq.RichFeature;
import org.biojavax.bio.seq.RichSequence;
import org.biojavax.bio.taxa.NCBITaxon;

/**
 * The application's main frame.
 */
public class BioInfoView extends FrameView {

    public BioInfoView(SingleFrameApplication app) {
        super(app);

        initComponents();

        // status bar initialization - message timeout, idle icon and busy animation, etc
        ResourceMap resourceMap = getResourceMap();
        int messageTimeout = resourceMap.getInteger("StatusBar.messageTimeout");
        messageTimer = new Timer(messageTimeout, new ActionListener() {
            public void actionPerformed(ActionEvent e) {
                statusMessageLabel.setText("");
            }
        });
        messageTimer.setRepeats(false);
        int busyAnimationRate = resourceMap.getInteger("StatusBar.busyAnimationRate");
        for (int i = 0; i < busyIcons.length; i++) {
            busyIcons[i] = resourceMap.getIcon("StatusBar.busyIcons[" + i + "]");
        }
        busyIconTimer = new Timer(busyAnimationRate, new ActionListener() {
            public void actionPerformed(ActionEvent e) {
                busyIconIndex = (busyIconIndex + 1) % busyIcons.length;
                statusAnimationLabel.setIcon(busyIcons[busyIconIndex]);
            }
        });
        idleIcon = resourceMap.getIcon("StatusBar.idleIcon");
        statusAnimationLabel.setIcon(idleIcon);
        progressBar.setVisible(false);

        // connecting action tasks to status bar via TaskMonitor
        TaskMonitor taskMonitor = new TaskMonitor(getApplication().getContext());
        taskMonitor.addPropertyChangeListener(new java.beans.PropertyChangeListener() {
            public void propertyChange(java.beans.PropertyChangeEvent evt) {
                String propertyName = evt.getPropertyName();
                if ("started".equals(propertyName)) {
                    if (!busyIconTimer.isRunning()) {
                        statusAnimationLabel.setIcon(busyIcons[0]);
                        busyIconIndex = 0;
                        busyIconTimer.start();
                    }
                    progressBar.setVisible(true);
                    progressBar.setIndeterminate(true);
                } else if ("done".equals(propertyName)) {
                    busyIconTimer.stop();
                    statusAnimationLabel.setIcon(idleIcon);
                    progressBar.setVisible(false);
                    progressBar.setValue(0);
                } else if ("message".equals(propertyName)) {
                    String text = (String)(evt.getNewValue());
                    statusMessageLabel.setText((text == null) ? "" : text);
                    messageTimer.restart();
                } else if ("progress".equals(propertyName)) {
                    int value = (Integer)(evt.getNewValue());
                    progressBar.setVisible(true);
                    progressBar.setIndeterminate(false);
                    progressBar.setValue(value);
                }
            }
        });
    }

    @Action
    public void showAboutBox() {
        if (aboutBox == null) {
            JFrame mainFrame = BioInfoApp.getApplication().getMainFrame();
            aboutBox = new BioInfoAboutBox(mainFrame);
            aboutBox.setLocationRelativeTo(mainFrame);
        }
        BioInfoApp.getApplication().show(aboutBox);
    }

    /** This method is called from within the constructor to
     * initialize the form.
     * WARNING: Do NOT modify this code. The content of this method is
     * always regenerated by the Form Editor.
     */
    @SuppressWarnings("unchecked")
  // <editor-fold defaultstate="collapsed" desc="Generated Code">//GEN-BEGIN:initComponents
  private void initComponents() {

    mainPanel = new javax.swing.JPanel();
    jPanel2 = new javax.swing.JPanel();
    bioPanel1 = new bioinfo.BioPanel();
    jPanel1 = new javax.swing.JPanel();
    jTextField1 = new javax.swing.JTextField();
    jLabel1 = new javax.swing.JLabel();
    jButton1 = new javax.swing.JButton();
    jLabel2 = new javax.swing.JLabel();
    menuBar = new javax.swing.JMenuBar();
    javax.swing.JMenu fileMenu = new javax.swing.JMenu();
    javax.swing.JMenuItem exitMenuItem = new javax.swing.JMenuItem();
    javax.swing.JMenu helpMenu = new javax.swing.JMenu();
    javax.swing.JMenuItem aboutMenuItem = new javax.swing.JMenuItem();
    statusPanel = new javax.swing.JPanel();
    javax.swing.JSeparator statusPanelSeparator = new javax.swing.JSeparator();
    statusMessageLabel = new javax.swing.JLabel();
    statusAnimationLabel = new javax.swing.JLabel();
    progressBar = new javax.swing.JProgressBar();

    mainPanel.setName("mainPanel"); // NOI18N

    jPanel2.setName("jPanel2"); // NOI18N
    jPanel2.setLayout(new java.awt.GridLayout());

    org.jdesktop.application.ResourceMap resourceMap = org.jdesktop.application.Application.getInstance(bioinfo.BioInfoApp.class).getContext().getResourceMap(BioInfoView.class);
    bioPanel1.setBackground(resourceMap.getColor("bioPanel1.background")); // NOI18N
    bioPanel1.setName("bioPanel1"); // NOI18N

    javax.swing.GroupLayout bioPanel1Layout = new javax.swing.GroupLayout(bioPanel1);
    bioPanel1.setLayout(bioPanel1Layout);
    bioPanel1Layout.setHorizontalGroup(
      bioPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
      .addGap(0, 695, Short.MAX_VALUE)
    );
    bioPanel1Layout.setVerticalGroup(
      bioPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
      .addGap(0, 398, Short.MAX_VALUE)
    );

    jPanel2.add(bioPanel1);

    jPanel1.setName("jPanel1"); // NOI18N

    jTextField1.setText(resourceMap.getString("jTextField1.text")); // NOI18N
    jTextField1.setName("jTextField1"); // NOI18N

    jLabel1.setText(resourceMap.getString("jLabel1.text")); // NOI18N
    jLabel1.setName("jLabel1"); // NOI18N

    jButton1.setText(resourceMap.getString("jButton1.text")); // NOI18N
    jButton1.setName("jButton1"); // NOI18N
    jButton1.addMouseListener(new java.awt.event.MouseAdapter() {
      public void mouseClicked(java.awt.event.MouseEvent evt) {
        jButton1MouseClicked(evt);
      }
    });

    jLabel2.setText(resourceMap.getString("jLabel2.text")); // NOI18N
    jLabel2.setName("jLabel2"); // NOI18N

    javax.swing.GroupLayout jPanel1Layout = new javax.swing.GroupLayout(jPanel1);
    jPanel1.setLayout(jPanel1Layout);
    jPanel1Layout.setHorizontalGroup(
      jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
      .addGroup(jPanel1Layout.createSequentialGroup()
        .addContainerGap()
        .addComponent(jLabel1)
        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.UNRELATED)
        .addComponent(jTextField1, javax.swing.GroupLayout.PREFERRED_SIZE, 253, javax.swing.GroupLayout.PREFERRED_SIZE)
        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
        .addComponent(jButton1)
        .addGap(18, 18, 18)
        .addComponent(jLabel2)
        .addContainerGap(252, Short.MAX_VALUE))
    );
    jPanel1Layout.setVerticalGroup(
      jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
      .addGroup(jPanel1Layout.createSequentialGroup()
        .addContainerGap()
        .addGroup(jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
          .addComponent(jLabel1)
          .addComponent(jTextField1, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
          .addComponent(jButton1)
          .addComponent(jLabel2))
        .addContainerGap(javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
    );

    javax.swing.GroupLayout mainPanelLayout = new javax.swing.GroupLayout(mainPanel);
    mainPanel.setLayout(mainPanelLayout);
    mainPanelLayout.setHorizontalGroup(
      mainPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
      .addComponent(jPanel2, javax.swing.GroupLayout.DEFAULT_SIZE, 695, Short.MAX_VALUE)
      .addGroup(mainPanelLayout.createSequentialGroup()
        .addComponent(jPanel1, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
        .addContainerGap())
    );
    mainPanelLayout.setVerticalGroup(
      mainPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
      .addGroup(mainPanelLayout.createSequentialGroup()
        .addComponent(jPanel1, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
        .addComponent(jPanel2, javax.swing.GroupLayout.DEFAULT_SIZE, 398, Short.MAX_VALUE))
    );

    menuBar.setName("menuBar"); // NOI18N

    fileMenu.setText(resourceMap.getString("fileMenu.text")); // NOI18N
    fileMenu.setName("fileMenu"); // NOI18N

    javax.swing.ActionMap actionMap = org.jdesktop.application.Application.getInstance(bioinfo.BioInfoApp.class).getContext().getActionMap(BioInfoView.class, this);
    exitMenuItem.setAction(actionMap.get("quit")); // NOI18N
    exitMenuItem.setName("exitMenuItem"); // NOI18N
    fileMenu.add(exitMenuItem);

    menuBar.add(fileMenu);

    helpMenu.setText(resourceMap.getString("helpMenu.text")); // NOI18N
    helpMenu.setName("helpMenu"); // NOI18N

    aboutMenuItem.setAction(actionMap.get("showAboutBox")); // NOI18N
    aboutMenuItem.setName("aboutMenuItem"); // NOI18N
    helpMenu.add(aboutMenuItem);

    menuBar.add(helpMenu);

    statusPanel.setName("statusPanel"); // NOI18N

    statusPanelSeparator.setName("statusPanelSeparator"); // NOI18N

    statusMessageLabel.setName("statusMessageLabel"); // NOI18N

    statusAnimationLabel.setHorizontalAlignment(javax.swing.SwingConstants.LEFT);
    statusAnimationLabel.setName("statusAnimationLabel"); // NOI18N

    progressBar.setName("progressBar"); // NOI18N

    javax.swing.GroupLayout statusPanelLayout = new javax.swing.GroupLayout(statusPanel);
    statusPanel.setLayout(statusPanelLayout);
    statusPanelLayout.setHorizontalGroup(
      statusPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
      .addComponent(statusPanelSeparator, javax.swing.GroupLayout.DEFAULT_SIZE, 695, Short.MAX_VALUE)
      .addGroup(statusPanelLayout.createSequentialGroup()
        .addContainerGap()
        .addComponent(statusMessageLabel)
        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED, 521, Short.MAX_VALUE)
        .addComponent(progressBar, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
        .addComponent(statusAnimationLabel)
        .addContainerGap())
    );
    statusPanelLayout.setVerticalGroup(
      statusPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
      .addGroup(statusPanelLayout.createSequentialGroup()
        .addComponent(statusPanelSeparator, javax.swing.GroupLayout.PREFERRED_SIZE, 2, javax.swing.GroupLayout.PREFERRED_SIZE)
        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
        .addGroup(statusPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
          .addComponent(statusMessageLabel)
          .addComponent(statusAnimationLabel)
          .addComponent(progressBar, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE))
        .addGap(3, 3, 3))
    );

    setComponent(mainPanel);
    setMenuBar(menuBar);
    setStatusBar(statusPanel);
  }// </editor-fold>//GEN-END:initComponents

    private void jButton1MouseClicked(java.awt.event.MouseEvent evt)//GEN-FIRST:event_jButton1MouseClicked
    {//GEN-HEADEREND:event_jButton1MouseClicked
      rs = null;
      GenbankRichSequenceDB grsdb = new GenbankRichSequenceDB();
      org.biojavax.bio.db.ncbi.GenpeptRichSequenceDB g = new org.biojavax.bio.db.ncbi.GenpeptRichSequenceDB();
      try
      {
        //configureSequencePanel();
        //BioEntry entry = g.getBioEntry(this.jTextField1.getText());
        //Set lst = entry.getRelationships();
        //Set lst2 = entry.getRankedDocRefs();
        //Set lst3 = entry.getNoteSet();
        //NCBITaxon tax = entry.getTaxon();
        //tax.ge
        //System.out.println(lst.size());
        //System.out.println(lst2.size());
        //System.out.println(lst3.size());
        //System.out.println(lst3.iterator().next().toString());
        //System.out.println(lst2.iterator().next().toString());
        rs = g.getRichSequence(this.jTextField1.getText());
        jLabel2.setText("Description: " + rs.getDescription());

         //rs = grsdb.getSequence(this.jTextField1.getText());
         //rs = grsdb.getRichSequence(this.jTextField1.getTex());
        // ViewSequence view = new ViewSequence(rs);
        // Digest digest = new Digest();
        // digest.setSequence( view );
         //digest.addDigestFeatures();
        // setViewSequence(view);
         //this.jPanel2.removeAll();
         //this.jPanel2.add(sequencePanel);
         //this.jPanel2.setLayout(new FlowLayout());
         //this.jPanel2.doLayout();
         //this.jPanel2.repaint();
         //this.jLabel2.setText("Pasikeite");
         String featureType = "Region";
         holder2 = rs.filter(new org.biojava.bio.seq.FeatureFilter.ByType(featureType));
         holder = rs.filter(new org.biojava.bio.seq.FeatureFilter.Not(new org.biojava.bio.seq.FeatureFilter.ByType(featureType)));
         Iterator<org.biojava.bio.seq.Feature> i = holder.features();
         Iterator<org.biojava.bio.seq.Feature> i2 = holder2.features();

         Feature feat1 = null;
         Feature feat2 = null;
         String seq = rs.seqString();
         int current = 1;
         while(i2.hasNext())
         {
            feat2 = i2.next();
            RichFeature rf = (RichFeature)feat2;
            Set ns = rf.getNoteSet();
            Iterator it = ns.iterator();
            boolean domain = false;
            while (it.hasNext())
            {
              Object obj = it.next();
              Note note = (Note)obj;
              String name = note.getTerm().getName();
              String value = note.getValue();
              if (name.equals("region_name"))
              {
                
                 if (value.equals("Domain"))
                 {
                   domain = true;
                   System.out.println("Note: " + name + " " + value);
                 }
              }
            }
            int f2s = (feat2 != null)? feat2.getLocation().getMin(): Integer.MAX_VALUE;
            int f2e = (feat2 != null)? feat2.getLocation().getMax(): Integer.MAX_VALUE;            
            if (domain)
            {
              System.out.println("St: " + f2s + " En: " + f2e + " Len: " + seq.length() + " Diff: " + (f2e - f2s + 1));
              String s = seq.substring(f2s - 1, f2e - 1);
              writeSeq(s, true, f2s - 1, f2e - 1);

            }
            this.bioPanel1.setSeq(seq);
            this.bioPanel1.repaint();
         }
      }
      catch(BioException be)
      {
	       be.printStackTrace();
	       System.exit(-1);
      }

    }//GEN-LAST:event_jButton1MouseClicked

    protected void setViewSequence(ViewSequence seq)
    {
        sequencePanel.setSequence(seq);
    }

    public void writeSeq(String seq, Boolean colorize, int x, int y)
    {
       System.out.println("Seq: " + seq + " Color: " + colorize + "Start: " + x + "End: " + y);
       this.bioPanel1.addSeq(x, y, colorize);
    }
    
    private SequencePanelWrapper sequencePanel;
    protected void configureSequencePanel(){
        sequencePanel = new SequencePanelWrapper();
        sequencePanel.setSequence(rs);
        MultiLineRenderer multi = new MultiLineRenderer();
        sequencePanel.setRenderer(multi);

        try{
            multi.addRenderer( createDomainRenderer() );
            //multi.addRenderer( createSecondaryStructureRenderer() );
            multi.addRenderer(new SymbolSequenceRenderer());
            //multi.addRenderer( offsetRenderer = new OffsetRulerRenderer());
            //multi.addRenderer( createPeptideDigestRenderer() );
        }
        catch(ChangeVetoException ex){
             ex.printStackTrace();
        }
    }

    protected SequenceRenderer createDomainRenderer() throws ChangeVetoException{
        GlyphFeatureRenderer gfr = new GlyphFeatureRenderer();

        gfr.addFilterAndGlyph(new org.biojava.bio.seq.FeatureFilter.ByType("Region"),

                new TurnGlyph(java.awt.Color.GREEN.darker(), new java.awt.BasicStroke(3F))
        );
        FeatureBlockSequenceRenderer block = new FeatureBlockSequenceRenderer();

        block.setFeatureRenderer(gfr);
        return block;
    }

  // Variables declaration - do not modify//GEN-BEGIN:variables
  private bioinfo.BioPanel bioPanel1;
  private javax.swing.JButton jButton1;
  private javax.swing.JLabel jLabel1;
  private javax.swing.JLabel jLabel2;
  private javax.swing.JPanel jPanel1;
  private javax.swing.JPanel jPanel2;
  private javax.swing.JTextField jTextField1;
  private javax.swing.JPanel mainPanel;
  private javax.swing.JMenuBar menuBar;
  private javax.swing.JProgressBar progressBar;
  private javax.swing.JLabel statusAnimationLabel;
  private javax.swing.JLabel statusMessageLabel;
  private javax.swing.JPanel statusPanel;
  // End of variables declaration//GEN-END:variables

    private final Timer messageTimer;
    private final Timer busyIconTimer;
    private final Icon idleIcon;
    private final Icon[] busyIcons = new Icon[15];
    private int busyIconIndex = 0;
    private RichSequence rs;
    private FeatureHolder holder;
    private FeatureHolder holder2;
    private JDialog aboutBox;
}
