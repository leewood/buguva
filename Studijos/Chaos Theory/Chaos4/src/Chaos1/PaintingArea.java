/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package Chaos1;
import java.awt.*;
import java.util.Random;
import java.util.ArrayList;
/**
 *
 * @author nku
 */

public class PaintingArea extends java.awt.Canvas{
   private ArrayList generatedSource = new ArrayList();
   private ArrayList generatedDestination = new ArrayList();
   
   public void generate(int count)
   {
      Random generator = new Random();
      generatedSource.clear();
      generatedDestination.clear();
      if (count < 3)
      {
         count = 3;
      }
      
      for (int i = 0; i < count; i++)
      {
         int x = generator.nextInt(this.getWidth() / 2) + this.getWidth() / 4;
         int y = generator.nextInt(this.getHeight() / 2) + this.getHeight() / 4;
         Pixel pix = new Pixel(x, y);
         generatedSource.add(pix);
         x = generator.nextInt(this.getWidth() / 2) + this.getWidth() / 4;
         y = generator.nextInt(this.getHeight() / 2) + this.getHeight() / 4;
         pix = new Pixel(x, y);
         generatedDestination.add(pix);         
      }
      /*
      generatedSource.add(new Pixel(26, 35));
      generatedSource.add(new Pixel(10, 404));
      generatedSource.add(new Pixel(162, 454));
      generatedDestination.add(new Pixel(504, 127));
      generatedDestination.add(new Pixel(356, 199));
      generatedDestination.add(new Pixel(424, 367));
       */
   }

   public boolean paintStartEnd = true;
   public boolean showTraces = true;
   public boolean paintTraces = true;
   public boolean usePause = false;
   public int stepCount = 100;
   public int untilStep = 100;
   public int pauses = 10;
   public boolean changeColors = true;

   public double alpha = 0.0;
   public double beta = 0.0;

   public void lineIter(Graphics g, int length, int currentX, int currentY, double degree)
   {

       long newX = Math.round((Math.cos(degree) * length));
       long newY = Math.round((Math.sin(degree) * length));

       long newX2 = Math.round((Math.cos(degree - Math.PI/ 2.0) * length / 2.0));
       long newY2 = Math.round((Math.sin(degree - Math.PI/ 2.0) * length / 2.0));
       if ((length / 2 > 0) && (length / 2 > stopIndex))
       {
          lineIter(g, length / 2, currentX, currentY, degree);
          lineIter(g, length / 2, (int)(currentX + newX), (int)(currentY + newY), degree + Math.PI);
          lineIter(g, length / 2, (int)(currentX + newX), (int)(currentY + newY), degree - Math.PI / 2);
       }
       else
       {
           g.drawLine(currentX, currentY, (int)(currentX + newX), (int)(currentY + newY));
           g.drawLine((int)(currentX + newX), (int)(currentY + newY), (int)(currentX + newX + newX2), (int)(currentY + newY + newY2));
       }
   }

   private int stopIndex = 0;

   public int Iteration = -1;

   public void paint( Graphics g )
   {
       this.setBackground(Color.white);
       int width = this.getWidth();
       int height = this.getHeight();
       int positionX = width / 2;
       int positionY = 0;
       int length = (int)Math.round(height / 1.5);
       if (Iteration >= 0)
       {
       if (Iteration > 0)
       {
          stopIndex = length;
          for (int i = 0; i < Iteration; i++)
          {
             stopIndex /= 2;
          }
       }
       else
       {
           stopIndex = 0;
       }

       //double currentDegree = (double)width / (double)length;
       double currentDegree = Math.PI / 2;
       lineIter(g, length, positionX, positionY, currentDegree);
       }
   }

}
