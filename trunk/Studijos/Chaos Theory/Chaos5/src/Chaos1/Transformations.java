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
public class Transformations {

    private ArrayList<Transformation> list = new ArrayList<Transformation>();

    public void Add(Transformation trans)
    {
        list.add(trans);
    }

    public Transformation Get(int index)
    {
        if ((index >= 0) && (index < list.size()))
        {
            return list.get(index);
        }
        else
        {
           return new Transformation(0, 0, 0, 0);
        }
    }

    public int Count()
    {
        return list.size();

    }

    public void Clear()
    {
        list.clear();
    }

}
