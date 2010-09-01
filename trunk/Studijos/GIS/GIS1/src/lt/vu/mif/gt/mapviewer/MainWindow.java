package lt.vu.mif.gt.mapviewer;

import java.awt.Dimension;
import java.awt.Frame;
import java.awt.Insets;
import java.awt.Rectangle;
import java.awt.event.ComponentEvent;
import java.awt.event.ComponentListener;
import java.awt.event.MouseAdapter;
import java.awt.event.MouseEvent;
import java.awt.event.WindowEvent;
import java.awt.event.WindowListener;
import java.awt.event.WindowStateListener;

import javax.swing.ImageIcon;
import javax.swing.JButton;
import javax.swing.JFrame;
import javax.swing.JLabel;
import javax.swing.JMenu;
import javax.swing.JMenuBar;
import javax.swing.JMenuItem;
import javax.swing.JPanel;
import javax.swing.JScrollPane;
import javax.swing.JSplitPane;
import javax.swing.JTable;
import javax.swing.JTextField;
import javax.swing.JToolBar;
import javax.swing.SpringLayout;
import javax.swing.SwingConstants;
import javax.swing.SwingUtilities;
import javax.swing.UIManager;
import javax.swing.table.DefaultTableModel;

import lt.vu.mif.gt.mapviewer.utils.ProgressBarDialog;

import org.dyno.visual.swing.layouts.Bilateral;
import org.dyno.visual.swing.layouts.Constraints;
import org.dyno.visual.swing.layouts.GroupLayout;
import org.dyno.visual.swing.layouts.Leading;

//VS4E -- DO NOT REMOVE THIS LINE!
public class MainWindow extends JFrame
{

    private static final long serialVersionUID = 1L;
    private JMenuItem jMenuItem0;
    private JMenu jMenu0;
    private JMenuBar jMenuBar0;
    private JToolBar jToolBar0;
    private JButton jButton1;
    public JButton jButton0;
    private JPanel jPanel0;
    private JScrollPane jScrollPane0;
    private JScrollPane jScrollPane1;
    private JSplitPane jSplitPane0;
    private MapPanel jPanel1;

    /*
     * public boolean isFrameStateSupported(int state) throws HeadlessException
     * { if (state == Frame.MAXIMIZED_VERT) { return false; } else
     * 
     * { return super.isFrameStateSupported(state); } }
     */

    ProgressBarDialog dialog;

    public MainWindow(ProgressBarDialog dialog)
    {
        this.dialog = dialog;
        initComponents();

        this.addComponentListener(new ComponentListener()
        {

            public void componentHidden(ComponentEvent e)
            {
                getAttrWindow().setVisible(false);
                getGIS2Panel().setVisible(false);
            }

            public void componentMoved(ComponentEvent e)
            {
                updateAttributesSize();
            }

            public void componentResized(ComponentEvent e)
            {
                updateAttributesSize();
            }

            public void componentShown(ComponentEvent e)
            {
                getAttrWindow().setVisible(true);
                getGIS2Panel().setVisible(true);
            }
        });

        this.addWindowListener(new WindowListener()
        {

            @Override
            public void windowActivated(WindowEvent arg0)
            {
                // TODO Auto-generated method stub

            }

            @Override
            public void windowClosed(WindowEvent arg0)
            {
                // TODO Auto-generated method stub

            }

            @Override
            public void windowClosing(WindowEvent arg0)
            {
                // TODO Auto-generated method stub

            }

            @Override
            public void windowDeactivated(WindowEvent arg0)
            {
                // TODO Auto-generated method stub

            }

            @Override
            public void windowDeiconified(WindowEvent arg0)
            {
                // TODO Auto-generated method stub
                getAttrWindow().setVisible(true);
                getGIS2Panel().setVisible(true);
            }

            @Override
            public void windowIconified(WindowEvent arg0)
            {
                // TODO Auto-generated method stub
                getAttrWindow().setVisible(false);
                getGIS2Panel().setVisible(false);
            }

            @Override
            public void windowOpened(WindowEvent arg0)
            {
                // TODO Auto-generated method stub

            }
        });

        this.addWindowStateListener(new WindowStateListener()
        {

            @Override
            public void windowStateChanged(WindowEvent arg0)
            {
                if (arg0.getNewState() == Frame.MAXIMIZED_BOTH)
                {
                    System.out.println("Maximized");
                    initBound();
                }
            }

        }

        );
    }

    private void initBound()
    {
        Rectangle rect = this.getMaximizedBounds();
        rect.height = rect.height - 200;
        this.setMaximizedBounds(rect);
    }

    private void updateAttributesSize()
    {
        Rectangle rect = this.getBounds();
        int x = (int) rect.getX();
        int y = (int) (rect.getY() + rect.getHeight());
        int x2 = (int) (rect.getX() + rect.getWidth());
        int y2 = (int) rect.getY();
        getAttrWindow().setBounds(x, y, this.getWidth(),
                getAttrWindow().getHeight());
        getGIS2Panel().setBounds(x2, y2, getGIS2Panel().getWidth(),
                this.getHeight());
    }

    private void initComponents()
    {
        setLayout(new GroupLayout());
        add(getJPanel0(), new Constraints(new Bilateral(0, 1, 0),
                new Bilateral(21, 0, 0)));
        add(getJToolBar0(), new Constraints(new Bilateral(0, 1, 77),
                new Leading(0, 18, 10, 10)));
        setJMenuBar(getJMenuBar0());
        setSize(725, 446);
    }

    private JLabel getJLabel2()
    {
        if (jLabel2 == null)
        {
            jLabel2 = new JLabel();
            jLabel2.setText("Attributes:");
        }
        return jLabel2;
    }

    private JButton getJButton8()
    {
        if (jButton8 == null)
        {
            jButton8 = new JButton();
            jButton8.setText("Search");
            jButton8.addMouseListener(new MouseAdapter()
            {
                @Override
                public void mouseClicked(MouseEvent event)
                {
                    jPanel1.search(jTextField0.getText());
                }
            });
        }
        return jButton8;
    }

    private JTextField getJTextField0()
    {
        if (jTextField0 == null)
        {
            jTextField0 = new JTextField();
            jTextField0.setHorizontalAlignment(SwingConstants.TRAILING);
        }
        return jTextField0;
    }

    private JLabel getJLabel1()
    {
        if (jLabel1 == null)
        {
            jLabel1 = new JLabel();
            jLabel1.setText("Search Query:");
        }
        return jLabel1;
    }

    private JPanel getJPanel4()
    {
        if (jPanel4 == null)
        {
            jPanel4 = new JPanel();
            jPanel4.setLayout(new GroupLayout());
            // jPanel4.add(getJScrollPane3(), new Constraints(new Bilateral(3,
            // 0, 22), new Trailing(-231, 386, 10, 10)));
            // jPanel4.add(getJLabel1(), new Constraints(new Leading(206, 10,
            // 10), new Leading(5, 11, 166)));
            // jPanel4.add(getJTextField0(), new Constraints(new Leading(280,
            // 365, 10, 10), new Leading(5, 11, 166)));
            // jPanel4.add(getJButton8(), new Constraints(new Leading(655, 10,
            // 10), new Leading(5, 11, 166)));
            // jPanel4.add(getJLabel2(), new Constraints(new Leading(157, 181,
            // 10, 10), new Leading(30, 11, 166)));
            // jPanel4.add(getJScrollPane3(), new Constraints(new Bilateral(3,
            // 0, 22), new Leading(30, 10, 166)));
            jPanel4.add(getJLabel1(), new Constraints(new Leading(206, 10, 10),
                    new Leading(5, 11, 166)));
            jPanel4.add(getJTextField0(), new Constraints(new Leading(280, 365,
                    10, 10), new Leading(5, 11, 166)));
            jPanel4.add(getJButton8(), new Constraints(
                    new Leading(655, 10, 10), new Leading(5, 11, 166)));
            // jPanel4.add(getJLabel2(), new Constraints(new Leading(20, 181,
            // 10, 10), new Leading(17, 11, 166)));
        }
        return jPanel4;
    }

    private JPanel getJPanel3()
    {
        if (jPanel3 == null)
        {
            jPanel3 = new JPanel();
            jPanel3.setLayout(new GroupLayout());
        }
        return jPanel3;
    }

    private JButton getJButton2()
    {
        if (jButton2 == null)
        {
            jButton2 = new JButton();
            jButton2.setText("Zoom Out");
            jButton2
                    .setIcon(new ImageIcon(
                            getClass()
                                    .getResource(
                                            "/lt/vu/mif/gt/mapviewer/data/pics/magifier_zoom_out.png")));
            jButton2.setToolTipText("Zoom Out (Shift + Mouse Right Button)");
            jButton2.setDefaultCapable(false);
            jButton2.setBorder(null);
            jButton2.addMouseListener(new MouseAdapter()
            {
                @Override
                public void mouseClicked(MouseEvent event)
                {
                    jPanel1.setTool(MapPanel.ZOOMOUT);
                }
            });
        }
        return jButton2;
    }

    private JLabel getJLabel0()
    {
        if (jLabel0 == null)
        {
            jLabel0 = new JLabel();
            jLabel0.setText("Layers:");
        }
        return jLabel0;
    }

    private JScrollPane getJScrollPane3()
    {
        if (jScrollPane3 == null)
        {
            jScrollPane3 = new JScrollPane();
            jScrollPane3.setViewportView(getJTable0());
            jScrollPane3.setSize(400, 110);
        }
        return jScrollPane3;
    }

    private JTable getJTable0()
    {
        if (jTable0 == null)
        {
            jTable0 = new JTable();
            jTable0.setSize(400, 110);
            jTable0.setModel(new DefaultTableModel(new Object[][] {},
                    new String[] { "Attribute" })
            {
                private static final long serialVersionUID = 1L;
                Class<?>[] types = new Class<?>[] { Object.class, Object.class, };

                public Class<?> getColumnClass(int columnIndex)
                {
                    return types[columnIndex];
                }
            });
            jTable0.addMouseListener(new MouseAdapter()
            {
                @Override
                public void mouseClicked(MouseEvent event)
                {
                    int row = jTable0.rowAtPoint(event.getPoint());
                    int col = jTable0.columnAtPoint(event.getPoint());
                    if ((row >= 0) && (col >= 0))
                    {
                        String name = jTable0.getModel().getColumnName(col);
                        String value = jTable0.getModel().getValueAt(row, col)
                                .toString();
                        jPanel1.selectByAttribute(name, value, row);
                    }
                }
            });
        }
        return jTable0;
    }

    private LayersPanel getJPanel2()
    {
        if (jPanel2 == null)
        {
            jPanel2 = new LayersPanel();
            // jPanel2.setLayout(new GridLayout(100, 1));
            jPanel2.add(getJLabel0());
            jPanel2.setMapPanel(getJPanel1());
            jPanel1.setLayersPanel(jPanel2);
        }
        return jPanel2;
    }

    AttributesWindow attrWindow;
    GIS2Panel gis2Panel;

    private GIS2Panel getGIS2Panel()
    {
        if (gis2Panel == null)
        {
            gis2Panel = new GIS2Panel(getJPanel1());
            gis2Panel.setVisible(true);
        }
        return gis2Panel;
    }

    private AttributesWindow getAttrWindow()
    {
        if (attrWindow == null)
        {
            attrWindow = new AttributesWindow(getJPanel1());
            attrWindow.setVisible(true);
        }

        return attrWindow;
    }

    private MapPanel getJPanel1()
    {
        if (jPanel1 == null)
        {
            jPanel1 = new MapPanel(dialog, this);
            jPanel1.setLayout(new GroupLayout());
            // jPanel1.setListView(getJList0());

            jPanel1.setTable(getAttrWindow().getJTable0());
            jPanel1.addMouseListener(new MouseAdapter()
            {
                @Override
                public void mouseClicked(MouseEvent event)
                {
                    jPanel1MouseMouseClicked(event);
                }

                public void mouseReleased(MouseEvent e)
                {
                    // updateSize(e);
                    jPanel1MouseReleased(e);
                }

                public void mousePressed(MouseEvent e)
                {
                    jPanel1MousePressed(e);
                }
            });
            jPanel1.addMouseMotionListener(new MouseAdapter()
            {
                @Override
                public void mouseDragged(MouseEvent event)
                {
                    jPanel1MouseMoved(event);
                }

            });

        }
        return jPanel1;
    }

    private JSplitPane getJSplitPane0()
    {
        if (jSplitPane0 == null)
        {
            jSplitPane0 = new JSplitPane();
            jSplitPane0.setDividerLocation(230);
            jSplitPane0.setPreferredSize(new Dimension(30, 5));
            jSplitPane0.setLeftComponent(getJScrollPane0());
            jSplitPane0.setRightComponent(getJScrollPane1());
        }
        return jSplitPane0;
    }

    private JScrollPane getJScrollPane1()
    {
        if (jScrollPane1 == null)
        {
            jScrollPane1 = new JScrollPane();
            jScrollPane1.setViewportView(getJPanel1());
        }
        return jScrollPane1;
    }

    private JScrollPane getJScrollPane0()
    {
        if (jScrollPane0 == null)
        {
            jScrollPane0 = new JScrollPane();
            jScrollPane0.setViewportView(getJPanel2());
        }
        return jScrollPane0;
    }

    private JPanel getJPanel0()
    {
        if (jPanel0 == null)
        {
            jPanel0 = new JPanel();
            // jPanel0.setLayout(new GridLayout(2, 1));
            SpringLayout layout = new SpringLayout();
            jPanel0.setLayout(layout);

            jPanel0.add(getJPanel4());
            jPanel0.add(getJSplitPane0());
            layout.putConstraint(SpringLayout.NORTH, getJSplitPane0(), 30,
                    SpringLayout.NORTH, jPanel0);
            layout.putConstraint(SpringLayout.WEST, getJSplitPane0(), 0,
                    SpringLayout.WEST, jPanel0);

            layout.putConstraint(SpringLayout.NORTH, getJPanel4(), 0,
                    SpringLayout.NORTH, jPanel0);

            layout.putConstraint(SpringLayout.WEST, getJPanel4(), 0,
                    SpringLayout.WEST, jPanel0);
            layout.putConstraint(SpringLayout.EAST, jPanel0, 0,
                    SpringLayout.EAST, getJPanel4());
            layout.putConstraint(SpringLayout.EAST, jPanel0, 0,
                    SpringLayout.EAST, getJSplitPane0());
            layout.putConstraint(SpringLayout.SOUTH, jPanel0, 0,
                    SpringLayout.SOUTH, getJSplitPane0());
            layout.putConstraint(SpringLayout.SOUTH, getJPanel4(), 0,
                    SpringLayout.NORTH, getJSplitPane0());

            layout.putConstraint(SpringLayout.SOUTH, jPanel0, 0,
                    SpringLayout.SOUTH, getJSplitPane0());
        }
        return jPanel0;
    }

    JButton jButton3 = null;
    JButton jButton4 = null;
    JButton jButton5 = null;
    JButton jButton6 = null;
    JButton jButton7 = null;

    private JButton getJButton7()
    {
        if (jButton7 == null)
        {
            jButton7 = new JButton();
            jButton7.setIcon(new ImageIcon(getClass().getResource(
                    "/lt/vu/mif/gt/mapviewer/data/pics/shape_handles.png")));
            jButton7.setText("Selected Area Info");
            jButton7.setToolTipText("Selected Area Info (Mouse Selection)");
            jButton7.setOpaque(false);
            jButton7.setBorder(null);
            jButton7.setDefaultCapable(false);
            jButton7.addMouseListener(new MouseAdapter()
            {
                @Override
                public void mouseClicked(MouseEvent event)
                {
                    jPanel1.setTool(MapPanel.GETAREA);
                }
            });
        }
        return jButton7;
    }

    private JButton getJButton6()
    {
        if (jButton6 == null)
        {
            jButton6 = new JButton();
            jButton6.setIcon(new ImageIcon(getClass().getResource(
                    "/lt/vu/mif/gt/mapviewer/data/pics/information.png")));
            jButton6.setText("Selected Object Info");
            jButton6.setToolTipText("Selected Object Info (Mouse Click)");
            jButton6.setOpaque(false);
            jButton6.setBorder(null);
            jButton6.setDefaultCapable(false);
            jButton6.addMouseListener(new MouseAdapter()
            {
                @Override
                public void mouseClicked(MouseEvent event)
                {
                    jPanel1.setTool(MapPanel.GETFEATURE);
                }
            });

        }
        return jButton6;
    }

    private JButton getJButton5()
    {
        if (jButton5 == null)
        {
            jButton5 = new JButton();
            jButton5.setIcon(new ImageIcon(getClass().getResource(
                    "/lt/vu/mif/gt/mapviewer/data/pics/arrow_out.png")));
            jButton5.setText("Pan");
            jButton5.setToolTipText("Pan (Alt + Mouse Drag)");
            jButton5.setOpaque(false);
            jButton5.setBorder(null);
            jButton5.setDefaultCapable(false);
            jButton5.addMouseListener(new MouseAdapter()
            {
                @Override
                public void mouseClicked(MouseEvent event)
                {
                    jPanel1.setTool(MapPanel.PAN);
                }
            });
        }
        return jButton5;
    }

    private JButton getJButton4()
    {
        if (jButton4 == null)
        {
            jButton4 = new JButton();
            jButton4.setIcon(new ImageIcon(getClass().getResource(
                    "/lt/vu/mif/gt/mapviewer/data/pics/map_magnify.png")));
            jButton4.setText("Zoom To Extent");
            jButton4.setToolTipText("Shift + Mouse Selection");
            jButton4.setOpaque(false);
            jButton4.setBorder(null);
            jButton4.setDefaultCapable(false);
            jButton4.addMouseListener(new MouseAdapter()
            {
                @Override
                public void mouseClicked(MouseEvent event)
                {
                    jPanel1.setTool(MapPanel.ZOOMTOEXTENT);
                }
            });

        }
        return jButton4;
    }

    JButton jButton9 = null;

    private JButton getJButton9()
    {
        if (jButton9 == null)
        {
            jButton9 = new JButton();
            jButton9.setIcon(new ImageIcon(getClass().getResource(
                    "/lt/vu/mif/gt/mapviewer/data/pics/map_magnify.png")));
            jButton9.setText("Zoom Selection");
            jButton9.setToolTipText("Zoom Selection Selection");
            jButton9.setOpaque(false);
            jButton9.setBorder(null);
            jButton9.setDefaultCapable(false);
            jButton9.addMouseListener(new MouseAdapter()
            {
                @Override
                public void mouseClicked(MouseEvent event)
                {
                    jPanel1.zoomSel();
                }
            });

        }
        return jButton9;
    }

    private JButton getJButton3()
    {
        if (jButton3 == null)
        {
            jButton3 = new JButton();
            jButton3.setIcon(new ImageIcon(getClass().getResource(
                    "/lt/vu/mif/gt/mapviewer/data/pics/magnifier.png")));
            jButton3.setText("Full Extent");
            jButton3.setToolTipText("Full Extent");
            jButton3.setOpaque(false);
            jButton3.setBorder(null);
            jButton3.setDefaultCapable(false);
            jButton3.addMouseListener(new MouseAdapter()
            {
                @Override
                public void mouseClicked(MouseEvent event)
                {
                    jPanel1.zoomToExtent();
                }
            });

        }
        return jButton3;
    }

    private JButton getJButton1()
    {
        if (jButton1 == null)
        {
            jButton1 = new JButton();
            jButton1
                    .setIcon(new ImageIcon(
                            getClass()
                                    .getResource(
                                            "/lt/vu/mif/gt/mapviewer/data/pics/magnifier_zoom_in.png")));
            jButton1.setText("Zoom In");
            jButton1.setToolTipText("Zoom In (Shift + Mouse Left Button)");
            jButton1.setOpaque(false);
            jButton1.setBorder(null);
            jButton1.setDefaultCapable(false);
            jButton1.addMouseListener(new MouseAdapter()
            {
                @Override
                public void mouseClicked(MouseEvent event)
                {
                    jPanel1.setTool(MapPanel.ZOOMIN);
                }
            });

        }
        return jButton1;
    }

    private JButton getJButton0()
    {
        if (jButton0 == null)
        {
            jButton0 = new JButton();
            jButton0.setIcon(new ImageIcon(getClass().getResource(
                    "/lt/vu/mif/gt/mapviewer/data/pics/bullet.png")));
            jButton0.setText("Add Layer...");
            jButton0.setToolTipText("add layer");
            jButton0.setBorder(null);
            jButton0.setDefaultCapable(false);
            jButton0.addMouseListener(new MouseAdapter()
            {
                @Override
                public void mousePressed(MouseEvent event)
                {
                    jButton0MouseMousePressed(event);
                }
            });
        }
        return jButton0;
    }

    private JButton getJButton10()
    {
        if (jButton10 == null)
        {
            jButton10 = new JButton();
            jButton10.setIcon(new ImageIcon(getClass().getResource(
                    "/lt/vu/mif/gt/mapviewer/data/pics/bullet.png")));
            jButton10.setText("Test");
            jButton10.setToolTipText("Test");
            jButton10.setBorder(null);
            jButton10.setDefaultCapable(false);
            jButton10.addMouseListener(new MouseAdapter()
            {
                @Override
                public void mousePressed(MouseEvent event)
                {
                    jButton10MouseMousePressed(event);
                }
            });
        }
        return jButton10;
    }

    private JButton getJButton11()
    {
        if (jButton11 == null)
        {
            jButton11 = new JButton();
            jButton11.setIcon(new ImageIcon(getClass().getResource(
                    "/lt/vu/mif/gt/mapviewer/data/pics/bullet.png")));
            jButton11.setText("Select All");
            jButton11.setToolTipText("Select All");
            jButton11.setBorder(null);
            jButton11.setDefaultCapable(false);
            jButton11.addMouseListener(new MouseAdapter()
            {
                @Override
                public void mousePressed(MouseEvent event)
                {
                    jButton11MouseMousePressed(event);
                }
            });
        }
        return jButton11;
    }

    private JToolBar getJToolBar0()
    {
        if (jToolBar0 == null)
        {
            jToolBar0 = new JToolBar();
            jToolBar0.setMargin(new Insets(10, 10, 10, 10));
            jToolBar0.add(getJButton0());
            jToolBar0.add(getJButton1());
            jToolBar0.add(getJButton2());
            jToolBar0.add(getJButton3());
            jToolBar0.add(getJButton4());
            jToolBar0.add(getJButton5());
            jToolBar0.add(getJButton6());
            jToolBar0.add(getJButton7());
            jToolBar0.add(getJButton9());
            jToolBar0.add(getJButton11());
            jToolBar0.add(getJButton10());
        }
        return jToolBar0;
    }

    private JMenuBar getJMenuBar0()
    {
        if (jMenuBar0 == null)
        {
            jMenuBar0 = new JMenuBar();
            jMenuBar0.add(getJMenu0());
        }
        return jMenuBar0;
    }

    private JMenu getJMenu0()
    {
        if (jMenu0 == null)
        {
            jMenu0 = new JMenu();
            jMenu0.setText("File");
            jMenu0.add(getJMenuItem0());
        }
        return jMenu0;
    }

    private JMenuItem getJMenuItem0()
    {
        if (jMenuItem0 == null)
        {
            jMenuItem0 = new JMenuItem();
            jMenuItem0.setText("Add layer...");
            jMenuItem0.addMouseListener(new MouseAdapter()
            {
                @Override
                public void mousePressed(MouseEvent event)
                {
                    jButton0MouseMousePressed(event);
                }
            });
        }
        return jMenuItem0;
    }

    private static void installLnF()
    {
        try
        {
            String lnfClassname = PREFERRED_LOOK_AND_FEEL;
            if (lnfClassname == null)
                lnfClassname = UIManager.getCrossPlatformLookAndFeelClassName();
            UIManager.setLookAndFeel(lnfClassname);
        } catch (Exception e)
        {
            System.err.println("Cannot install " + PREFERRED_LOOK_AND_FEEL
                    + " on this platform:" + e.getMessage());
        }
    }

    /**
     * Main entry of the class. Note: This class is only created so that you can
     * easily preview the result at runtime. It is not expected to be managed by
     * the designer. You can modify it as you like.
     */
    public static MainWindow frame = null;
    private LayersPanel jPanel2;
    private JTable jTable0;
    private JScrollPane jScrollPane3;
    private JLabel jLabel0;
    private JButton jButton2;
    private JButton jButton10;
    private JButton jButton11;
    private JPanel jPanel3;
    private JPanel jPanel4;
    private JLabel jLabel1;
    private JTextField jTextField0;
    private JButton jButton8;
    private JLabel jLabel2;
    private static final String PREFERRED_LOOK_AND_FEEL = "com.sun.java.swing.plaf.windows.WindowsLookAndFeel";
    private static ProgressBarDialog progDialog = null;

    public static void main(String[] args)
    {
        installLnF();
        progDialog = new ProgressBarDialog(new JFrame());
        SwingUtilities.invokeLater(new Runnable()
        {
            @Override
            public void run()
            {
                // ProgressBarDialog dialog = new ProgressBarDialog(new
                // JFrame());
                frame = new MainWindow(progDialog);
                frame.setDefaultCloseOperation(MainWindow.EXIT_ON_CLOSE);
                frame.setTitle("GIS Tools");
                frame.getContentPane().setPreferredSize(frame.getSize());
                frame.pack();
                frame.setLocationRelativeTo(null);
                frame.setVisible(true);
                Rectangle rect = frame.getBounds();
                int x = (int) rect.getX();
                int y = (int) (rect.getY() + rect.getHeight());
                System.out.println(x);
                System.out.println(y);
                frame.getAttrWindow().setBounds(x, y, frame.getWidth(), 200);

            }
        });

    }

    private void jButton0MouseMousePressed(MouseEvent event)
    {
        frame.jPanel1.addMapLayerWithDialog();
        // frame.jPanel2.updateLayers();
        frame.jPanel2.repaint();
        // frame.jScrollPane1.repaint();
        int loc = jSplitPane0.getDividerLocation();
        jSplitPane0.setDividerLocation(loc + 1);
        jSplitPane0.setDividerLocation(loc);
    }

    public void upPanel()
    {
        int loc = jSplitPane0.getDividerLocation();
        jSplitPane0.setDividerLocation(loc + 1);
        jSplitPane0.setDividerLocation(loc);

    }

    private void jButton10MouseMousePressed(MouseEvent event)
    {
        // frame.jPanel1.InitSpecialFunction();
    }

    private void jButton11MouseMousePressed(MouseEvent event)
    {
        frame.jPanel1.selectAll();
    }

    private void jPanel1MouseMouseClicked(MouseEvent event)
    {
        frame.jPanel1.mouseClicked(event);
    }

    private void jPanel1MouseReleased(MouseEvent event)
    {
        frame.jPanel1.mouseReleased(event);
    }

    private void jPanel1MousePressed(MouseEvent event)
    {
        frame.jPanel1.mousePressed(event);
    }

    private void jPanel1MouseMoved(MouseEvent event)
    {
        frame.jPanel1.mouseMoved(event);
    }

}
