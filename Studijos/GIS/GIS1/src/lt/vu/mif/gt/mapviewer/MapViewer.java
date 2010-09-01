package lt.vu.mif.gt.mapviewer;

import java.awt.Container;

import javax.swing.JFrame;

import lt.vu.mif.gt.mapviewer.utils.ProgressBarDialog;

public class MapViewer
{

    /**
     * @param args
     */
    public static void main(String[] args)
    {
        JFrame frame = new JFrame();
        frame.setBounds(100, 100, 608, 560);
        frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        Container c = frame.getContentPane();
        // c.add(new MapPanel());
        ProgressBarDialog dialog = new ProgressBarDialog(frame);
        dialog.setVisible(false);
        frame.setVisible(true);
    }

}
