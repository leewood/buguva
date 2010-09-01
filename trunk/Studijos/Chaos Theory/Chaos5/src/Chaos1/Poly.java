/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package Chaos1;

/**
 *
 * @author kuosis
 */
import java.util.ArrayList;
import java.awt.*;
public class Poly {

    private ArrayList<Pixel> list = new ArrayList<Pixel>();

    public Poly()
    {

    }

    public int Count()
    {
       return list.size();
    }

    public void Add(Pixel pix)
    {
        list.add(pix);
    }

    public Pixel Get(int index)
    {
        if ((index >=0) && (index < Count()))
        {
            return list.get(index);
        }
        else
        {
            return new Pixel();
        }
    }


    public void PaintPoly(Graphics g, int offset, Color clr)
    {
        for (int i = 0; i < Count(); i++)
        {
            Pixel pix1 = Get(i);
            Pixel pix2 = Get(0);
            if (i < Count() - 1)
            {
               pix2 = Get(i + 1);
            }
            g.setColor(clr);
            g.drawLine(pix1.getXo(offset), pix1.getYo(offset), pix2.getXo(offset), pix2.getYo(offset));
        }
    }

}
