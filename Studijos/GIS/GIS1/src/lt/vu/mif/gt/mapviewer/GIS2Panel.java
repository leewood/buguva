package lt.vu.mif.gt.mapviewer;

import java.awt.event.MouseAdapter;
import java.awt.event.MouseEvent;

import javax.swing.JButton;
import javax.swing.JFrame;
import javax.swing.JLabel;
import javax.swing.JOptionPane;
import javax.swing.JTextField;
import javax.swing.WindowConstants;

import org.dyno.visual.swing.layouts.Constraints;
import org.dyno.visual.swing.layouts.GroupLayout;
import org.dyno.visual.swing.layouts.Leading;
import org.dyno.visual.swing.layouts.Trailing;

//VS4E -- DO NOT REMOVE THIS LINE!
public class GIS2Panel extends JFrame
{

    private static final long serialVersionUID = 1L;
    private JLabel jLabel4;
    private JTextField jTextField4;
    private JTextField jTextField5;
    private JTextField jTextField6;
    private JTextField jTextField3;
    private JLabel jLabel3;
    private JLabel jLabel9;
    private JLabel jLabel5;
    private JLabel jLabel6;
    private JTextField jTextField7;
    private JTextField jTextField8;
    private JTextField jTextField2;
    private JLabel jLabel2;
    private JLabel jLabel0;
    private JTextField jTextField0;
    private JLabel jLabel1;
    private JTextField jTextField1;
    private JLabel jLabel10;
    private JLabel jLabel8;
    private JButton jButton0;
    private JLabel jLabel7;
    private JTextField jTextField9;
    private JLabel jLabel11;

    private MapPanel map = null;

    public GIS2Panel(MapPanel map)
    {
        this.map = map;
        initComponents();
    }

    private void initComponents()
    {
        setTitle("GIS2");
        setDefaultCloseOperation(WindowConstants.DO_NOTHING_ON_CLOSE);
        setResizable(false);
        setLayout(new GroupLayout());
        add(getJLabel4(), new Constraints(new Leading(12, 12, 12), new Leading(
                159, 10, 10)));
        add(getJTextField4(), new Constraints(new Leading(12, 28, 10, 10),
                new Leading(187, 12, 12)));
        add(getJTextField5(), new Constraints(new Leading(44, 26, 12, 12),
                new Leading(187, 12, 12)));
        add(getJTextField6(), new Constraints(new Leading(76, 26, 12, 12),
                new Leading(187, 12, 12)));
        add(getJTextField3(), new Constraints(new Leading(83, 112, 12, 12),
                new Leading(137, 10, 10)));
        add(getJLabel3(), new Constraints(new Leading(12, 12, 12), new Leading(
                139, 10, 10)));
        add(getJLabel9(), new Constraints(new Leading(12, 12, 12), new Leading(
                210, 10, 10)));
        add(getJLabel5(), new Constraints(new Leading(12, 12, 12), new Leading(
                232, 12, 12)));
        add(getJLabel6(), new Constraints(new Leading(12, 12, 12), new Leading(
                265, 10, 10)));
        add(getJTextField7(), new Constraints(new Leading(83, 112, 12, 12),
                new Leading(229, 10, 10)));
        add(getJTextField8(), new Constraints(new Leading(83, 112, 12, 12),
                new Leading(261, 10, 10)));
        add(getJTextField2(), new Constraints(new Leading(83, 112, 12, 12),
                new Leading(107, 12, 12)));
        add(getJLabel2(), new Constraints(new Leading(12, 12, 12), new Leading(
                107, 10, 10)));
        add(getJLabel0(), new Constraints(new Leading(12, 12, 12), new Leading(
                28, 10, 10)));
        add(getJTextField0(), new Constraints(new Leading(83, 112, 12, 12),
                new Leading(26, 10, 10)));
        add(getJLabel1(), new Constraints(new Leading(12, 12, 12), new Leading(
                65, 12, 12)));
        add(getJTextField1(), new Constraints(new Leading(83, 112, 12, 12),
                new Leading(61, 12, 12)));
        add(getJLabel10(), new Constraints(new Leading(12, 12, 12),
                new Leading(6, 12, 12)));
        add(getJLabel8(), new Constraints(new Leading(12, 12, 12), new Leading(
                87, 12, 12)));
        add(getJButton0(), new Constraints(new Leading(73, 10, 10),
                new Trailing(12, 293, 293)));
        add(getJLabel7(), new Constraints(new Leading(12, 12, 12), new Leading(
                313, 10, 10)));
        add(getJTextField9(), new Constraints(new Leading(83, 112, 12, 12),
                new Leading(309, 10, 10)));
        add(getJLabel11(), new Constraints(new Leading(12, 12, 12),
                new Leading(287, 12, 12)));
        setSize(264, 379);
    }

    private JLabel getJLabel11()
    {
        if (jLabel11 == null)
        {
            jLabel11 = new JLabel();
            jLabel11.setText("River incline:");
        }
        return jLabel11;
    }

    private JTextField getJTextField9()
    {
        if (jTextField9 == null)
        {
            jTextField9 = new JTextField();
            jTextField9.setText("0.0001");
        }
        return jTextField9;
    }

    private JLabel getJLabel7()
    {
        if (jLabel7 == null)
        {
            jLabel7 = new JLabel();
            jLabel7.setText("h:");
        }
        return jLabel7;
    }

    private JButton getJButton0()
    {
        if (jButton0 == null)
        {
            jButton0 = new JButton();
            jButton0.setText("Search");
            jButton0.addMouseListener(new MouseAdapter()
            {
                @Override
                public void mouseClicked(MouseEvent event)
                {
                    initStart();
                }
            });
        }
        return jButton0;
    }

    private JLabel getJLabel8()
    {
        if (jLabel8 == null)
        {
            jLabel8 = new JLabel();
            jLabel8.setText("Nearest city is between distance between:");
        }
        return jLabel8;
    }

    private JLabel getJLabel10()
    {
        if (jLabel10 == null)
        {
            jLabel10 = new JLabel();
            jLabel10.setText("Area size:");
        }
        return jLabel10;
    }

    private JTextField getJTextField1()
    {
        if (jTextField1 == null)
        {
            jTextField1 = new JTextField();
            jTextField1.setText("250");
        }
        return jTextField1;
    }

    private JLabel getJLabel1()
    {
        if (jLabel1 == null)
        {
            jLabel1 = new JLabel();
            jLabel1.setText("Y:");
        }
        return jLabel1;
    }

    private JTextField getJTextField0()
    {
        if (jTextField0 == null)
        {
            jTextField0 = new JTextField();
            jTextField0.setText("250");
        }
        return jTextField0;
    }

    private JLabel getJLabel0()
    {
        if (jLabel0 == null)
        {
            jLabel0 = new JLabel();
            jLabel0.setText("X:");
        }
        return jLabel0;
    }

    private JLabel getJLabel2()
    {
        if (jLabel2 == null)
        {
            jLabel2 = new JLabel();
            jLabel2.setText("m:");
        }
        return jLabel2;
    }

    private JTextField getJTextField2()
    {
        if (jTextField2 == null)
        {
            jTextField2 = new JTextField();
            jTextField2.setText("1000");
        }
        return jTextField2;
    }

    private JTextField getJTextField8()
    {
        if (jTextField8 == null)
        {
            jTextField8 = new JTextField();
            jTextField8.setText("1000000");
        }
        return jTextField8;
    }

    private JTextField getJTextField7()
    {
        if (jTextField7 == null)
        {
            jTextField7 = new JTextField();
            jTextField7.setText("10000");
        }
        return jTextField7;
    }

    private JLabel getJLabel6()
    {
        if (jLabel6 == null)
        {
            jLabel6 = new JLabel();
            jLabel6.setText("k:");
        }
        return jLabel6;
    }

    private JLabel getJLabel5()
    {
        if (jLabel5 == null)
        {
            jLabel5 = new JLabel();
            jLabel5.setText("z:");
        }
        return jLabel5;
    }

    private JLabel getJLabel9()
    {
        if (jLabel9 == null)
        {
            jLabel9 = new JLabel();
            jLabel9.setText("City has citizens between:");
        }
        return jLabel9;
    }

    private JLabel getJLabel3()
    {
        if (jLabel3 == null)
        {
            jLabel3 = new JLabel();
            jLabel3.setText("n:");
        }
        return jLabel3;
    }

    private JTextField getJTextField3()
    {
        if (jTextField3 == null)
        {
            jTextField3 = new JTextField();
            jTextField3.setText("2000");
        }
        return jTextField3;
    }

    private JTextField getJTextField6()
    {
        if (jTextField6 == null)
        {
            jTextField6 = new JTextField();
            jTextField6.setText("g");
        }
        return jTextField6;
    }

    private JTextField getJTextField5()
    {
        if (jTextField5 == null)
        {
            jTextField5 = new JTextField();
            jTextField5.setText("b");
        }
        return jTextField5;
    }

    private JTextField getJTextField4()
    {
        if (jTextField4 == null)
        {
            jTextField4 = new JTextField();
            jTextField4.setText("i");
        }
        return jTextField4;
    }

    private JLabel getJLabel4()
    {
        if (jLabel4 == null)
        {
            jLabel4 = new JLabel();
            jLabel4.setText("bad characters:");
        }
        return jLabel4;
    }

    public double getGX() throws Exception
    {
        double result = 0;
        try
        {
            result = Double.parseDouble(this.jTextField0.getText());
        } catch (Exception e)
        {
            throw new Exception("Wrong X entered");
        }
        return result;
    }

    public double getGY() throws Exception
    {
        double result = 0;
        try
        {
            result = Double.parseDouble(this.jTextField1.getText());
        } catch (Exception e)
        {
            throw new Exception("Wrong Y entered");
        }
        return result;
    }

    public double getGM() throws Exception
    {
        double result = 0;
        try
        {
            result = Double.parseDouble(this.jTextField2.getText());
        } catch (Exception e)
        {
            throw new Exception("Wrong m entered");
        }
        return result;
    }

    public double getGN() throws Exception
    {
        double result = 0;
        try
        {
            result = Double.parseDouble(this.jTextField3.getText());
        } catch (Exception e)
        {
            throw new Exception("Wrong n entered");
        }
        return result;
    }

    public double getGZ() throws Exception
    {
        double result = 0;
        try
        {
            result = Double.parseDouble(this.jTextField7.getText());
        } catch (Exception e)
        {
            throw new Exception("Wrong z entered");
        }
        return result;
    }

    public double getGK() throws Exception
    {
        double result = 0;
        try
        {
            result = Double.parseDouble(this.jTextField8.getText());
        } catch (Exception e)
        {
            throw new Exception("Wrong k entered");
        }
        return result;
    }

    public double getGH() throws Exception
    {
        double result = 0;
        try
        {
            result = Double.parseDouble(this.jTextField9.getText());
        } catch (Exception e)
        {
            throw new Exception("Wrong h entered");
        }
        return result;
    }

    public void initStart()
    {
        boolean ok = true;
        double x = 0;
        double y = 0;
        double m = 0;
        double n = 0;
        double z = 0;
        double k = 0;
        double h = 0;
        String c1 = this.jTextField4.getText();
        String c2 = this.jTextField5.getText();
        String c3 = this.jTextField6.getText();
        try
        {
            x = this.getGX();
            y = this.getGY();
            m = this.getGM();
            n = this.getGN();
            z = this.getGZ();
            k = this.getGK();
            h = this.getGH();
        } catch (Exception e)
        {
            ok = false;
            JOptionPane.showMessageDialog(null, e.getMessage(), "Error",
                    JOptionPane.ERROR_MESSAGE);
        }
        if (ok)
        {
            map.InitSpecialFunction(x, y, m, n, z, k, h, c1, c2, c3);
        }

    }

}
