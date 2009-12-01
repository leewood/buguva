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
import java.util.Iterator;
import java.util.List;
import javax.swing.JPanel;
import org.biojava.bio.BioException;
import org.biojava.bio.seq.FeatureHolder;
import org.biojavax.Note;
import org.biojavax.bio.db.ncbi.GenpeptRichSequenceDB;
import org.biojavax.bio.seq.RichFeature;
import org.biojavax.bio.seq.RichSequence;

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

  private boolean outputLog = false;

  public void SetOutputLog(boolean outLog)
  {
    outputLog = outLog;
  }

  @Override
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

  public String loadData(String accessionID)
  {
    String result = "";
    RichSequence rs = null;    
    FeatureHolder holder;
    GenpeptRichSequenceDB db = new GenpeptRichSequenceDB();
      try
      {
        rs = db.getRichSequence(accessionID);
        result = "Description: " + rs.getDescription();
        String featureType = "Region";
        holder = rs.filter(new org.biojava.bio.seq.FeatureFilter.ByType(featureType));
        Iterator<org.biojava.bio.seq.Feature> features = holder.features();                
        String seqLoc = rs.seqString();
        while(features.hasNext())
        {
           RichFeature feature = (RichFeature)features.next();
           Iterator notes = feature.getNoteSet().iterator();
           int f2s = (feature != null)? feature.getLocation().getMin(): Integer.MAX_VALUE;
           int f2e = (feature != null)? feature.getLocation().getMax(): Integer.MAX_VALUE;
           while (notes.hasNext())
           {              
              Note note = (Note)notes.next();
              String name = note.getTerm().getName();
              String value = note.getValue();
              if ((name.equals("region_name")) && (value.equals("Domain")))
              {
                 if (outputLog) System.out.println("Note: " + name + " " + value);
                 if (outputLog) System.out.println("St: " + f2s + " En: " + f2e + " Len: " + seqLoc.length() + " Diff: " + (f2e - f2s + 1));
                 String s = seqLoc.substring(f2s - 1, f2e - 1);
                 writeSeq(s, true, f2s - 1, f2e - 1);
              }
            }
         }
         setSeq(seqLoc);
         repaint();
      }
      catch(BioException be)
      {
	       be.printStackTrace();
	       result = "Error: " + be.toString();
      }
      return result;
  }

    public void writeSeq(String seq, Boolean colorize, int x, int y)
    {
       if (outputLog) System.out.println("Seq: " + seq + " Color: " + colorize + "Start: " + x + "End: " + y);
       addSeq(x, y, colorize);
    }

}
