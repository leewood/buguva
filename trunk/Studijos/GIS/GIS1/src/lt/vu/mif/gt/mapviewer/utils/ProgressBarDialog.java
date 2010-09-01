package lt.vu.mif.gt.mapviewer.utils;

import java.awt.Dialog;
import java.awt.Frame;
import java.awt.GraphicsConfiguration;
import java.awt.Window;

import javax.swing.JDialog;
import javax.swing.JLabel;
import javax.swing.JProgressBar;
import javax.swing.WindowConstants;

import org.dyno.visual.swing.layouts.Constraints;
import org.dyno.visual.swing.layouts.GroupLayout;
import org.dyno.visual.swing.layouts.Leading;

//VS4E -- DO NOT REMOVE THIS LINE!
public class ProgressBarDialog extends JDialog
{

    private static final long serialVersionUID = 1L;
    private JLabel jLabel0;
    private JProgressBar jProgressBar0;

    public ProgressBarDialog()
    {
        initComponents();
    }

    public ProgressBarDialog(Frame parent)
    {
        super(parent);
        initComponents();
    }

    public ProgressBarDialog(Frame parent, boolean modal)
    {
        super(parent, modal);
        initComponents();
    }

    public ProgressBarDialog(Frame parent, String title)
    {
        super(parent, title);
        initComponents();
    }

    public ProgressBarDialog(Frame parent, String title, boolean modal)
    {
        super(parent, title, modal);
        initComponents();
    }

    public ProgressBarDialog(Frame parent, String title, boolean modal,
            GraphicsConfiguration arg)
    {
        super(parent, title, modal, arg);
        initComponents();
    }

    public ProgressBarDialog(Dialog parent)
    {
        super(parent);
        initComponents();
    }

    public ProgressBarDialog(Dialog parent, boolean modal)
    {
        super(parent, modal);
        initComponents();
    }

    public ProgressBarDialog(Dialog parent, String title)
    {
        super(parent, title);
        initComponents();
    }

    public ProgressBarDialog(Dialog parent, String title, boolean modal)
    {
        super(parent, title, modal);
        initComponents();
    }

    public ProgressBarDialog(Dialog parent, String title, boolean modal,
            GraphicsConfiguration arg)
    {
        super(parent, title, modal, arg);
        initComponents();
    }

    public ProgressBarDialog(Window parent)
    {
        super(parent);
        initComponents();
    }

    public ProgressBarDialog(Window parent, ModalityType modalityType)
    {
        super(parent, modalityType);
        initComponents();
    }

    public ProgressBarDialog(Window parent, String title)
    {
        super(parent, title);
        initComponents();
    }

    public ProgressBarDialog(Window parent, String title,
            ModalityType modalityType)
    {
        super(parent, title, modalityType);
        initComponents();
    }

    public ProgressBarDialog(Window parent, String title,
            ModalityType modalityType, GraphicsConfiguration arg)
    {
        super(parent, title, modalityType, arg);
        initComponents();
    }

    private void initComponents() {
    	setTitle("Status");
    	setDefaultCloseOperation(WindowConstants.DO_NOTHING_ON_CLOSE);
    	setResizable(false);
    	setLayout(new GroupLayout());
    	add(getJLabel0(), new Constraints(new Leading(43, 463, 10, 10), new Leading(15, 10, 10)));
    	add(getJProgressBar0(), new Constraints(new Leading(39, 465, 10, 10), new Leading(43, 26, 12, 12)));
    	setSize(547, 120);
    }

    private JProgressBar getJProgressBar0()
    {
        if (jProgressBar0 == null)
        {
            jProgressBar0 = new JProgressBar();
        }
        return jProgressBar0;
    }

    private JLabel getJLabel0()
    {
        if (jLabel0 == null)
        {
            jLabel0 = new JLabel();
            jLabel0.setText("jLabel0");
        }
        return jLabel0;
    }

    public void setDText(String text)
    {
        getJLabel0().setText(text);
    }

    public void setDTitle(String title)
    {
        this.setTitle(title);
    }

    public void setDValue(int value)
    {
        getJProgressBar0().setValue(value);
        this.repaint();
    }

    public void setDMaxValue(int max)
    {
        getJProgressBar0().setMaximum(max);
    }

    public void reset()
    {
        setDValue(0);
    }
}
