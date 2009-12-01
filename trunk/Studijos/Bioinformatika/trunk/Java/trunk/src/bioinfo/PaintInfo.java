/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package bioinfo;

/**
 *
 * @author kuosis
 */
public class PaintInfo
{
  private int start;
  private int end;
  private boolean color;
  public PaintInfo(int start, int end, boolean color)
  {
    this.start = start;
    this.end = end;
    this.color = color;
  }

  public int getStart()
  {
    return start;
  }

  public int getEnd()
  {
    return end;
  }

  public boolean useColor()
  {
    return color;
  }

   public int compareTo(Object o)
   {
       if (o  instanceof PaintInfo)
       {
          PaintInfo pi = (PaintInfo)o;
          if (this.getStart() > pi.getStart())
          {
            return 1;
          }
          else if (this.getStart() < pi.getStart())
          {
            return -1;
          }
          else if (this.getEnd() > pi.getEnd())
          {
            return 1;
          }
          else if (this.getEnd() < pi.getEnd())
          {
            return -1;
          }
          else
          {
            return 0;
          }
       }
       return 1;
   }

   public String getSubSeq(String seq)
   {
     return seq.substring(start, end - start + 1);
   }

   public boolean inside(int x)
   {
     return start <= x && end >= x;
   }
}
