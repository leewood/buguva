/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package Chaos1;

/**
 *
 * @author kuosis
 */
import java.awt.*;
import java.util.Random;
import java.util.ArrayList;

public class ImagePanel extends javax.swing.JPanel{

    public void paint( Graphics g )
    {
           Toolkit toolkit = Toolkit.getDefaultToolkit();
		   Image image = toolkit.getImage("test.jpg");
           g.drawImage(image, 0, 0, this);
    }
}
