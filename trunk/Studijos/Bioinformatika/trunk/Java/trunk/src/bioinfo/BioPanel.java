/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package bioinfo;

import java.awt.Color;
import java.awt.Component;
import java.awt.Font;
import java.awt.Graphics;
import java.util.ArrayList;
import java.util.List;
import javax.swing.JPanel;

/**
 *
 * @author kuosis
 */
public class BioPanel extends JPanel
{

  private String seq = null;
  private List<PaintInfo> list = new ArrayList<PaintInfo>();

  public void setSeq(String seq)
  {
    this.seq = seq;
  }
  public void addSeq(int start, int end, boolean color)
  {
    list.add(new PaintInfo(start, end, color));
  }

  private PaintInfo getInfo(int pos)
  {
    if (list != null)
    for (int i = 0; i < list.size(); i++)
    {
      if (list.get(i).inside(pos))
      {
        return list.get(i);
      }
    }
    return null;
  }

  public void paintComponent(Graphics g)
  {
    Component parent = null;
    if (this.getParent() != null)
    {
        parent = this.getParent();
        if (parent.getParent() != null)
        {
          parent = parent.getParent();
          if (parent.getParent() != null)
          {
            parent = parent.getParent();
          }
        }
    }
    int width = getWidth();
    int height = getHeight();
    if (parent != null)
    {
      width = parent.getWidth();
    }
    g.setColor(Color.WHITE);
    g.fillRect(0, 0, width, height);
    g.setColor(Color.BLACK);
    int tsx = 16;
    int tsy = 16;
    int margin = 16;
    int x = margin;
    int y = margin;
    Font f = new java.awt.Font("Courier New", 0, 12);
    g.setFont(f);
    if (seq != null)
    {
    for (int i = 0; i < seq.length(); i++)
    {
      PaintInfo pi = getInfo(i);
      Color c = (pi == null)? Color.BLACK:Color.RED;
      g.setColor(c);
      String s = "" + seq.charAt(i);
      g.drawString(s, x, y);
      x += tsx;
      if (x + tsx + margin >= width)
      {
        x = margin;
        y += tsy;
      }
    }
    }
    else
    {
       g.drawString("No protein", x, y);
    }
  }

}
