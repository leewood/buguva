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
   public double stopEnd = 0.0;
   public int iter = -1;

   public void lineIter(Graphics g, int length, int currentX, int currentY, double degree)
   {
       long newX = Math.round(Math.cos(degree) * length / 2.0);
       long newY = Math.round(Math.sin(degree) * length / 2.0);
       g.drawLine(currentX, currentY, (int)(currentX + newX), (int)(currentY + newY));
       if (length / 2 > stopEnd)
       {           
           lineIter(g, length / 2, (int)(currentX + newX), (int)(currentY + newY), degree + alpha);
           lineIter(g, length / 2, (int)(currentX + newX), (int)(currentY + newY), degree + (Math.PI /2 - alpha));
           lineIter(g, length / 2, (int)(currentX + newX), (int)(currentY + newY), degree - beta);
       }
       else
       {
           g.drawLine(currentX, currentY, (int)(currentX + newX), (int)(currentY + newY));
       }
   }

   public void paint( Graphics g )
   {
       this.setBackground(Color.white);
       int width = 400;
       int height = 400;
       int positionX = 20;
       int positionY = 20;       
       int centerX = (width - positionX) / 2 + positionX;
       int centerY = (height - positionY) / 2 + positionY;
       int length = 200;
       //double currentDegree = (double)width / (double)length;
       if (iter > 0)
       {
          stopEnd = (double)length / (double)Math.pow(2, iter);
       } else if (iter == 0)
       {
           stopEnd = 0.0;
       }
       else
       {
          stopEnd = -1.0;
       }
       if (stopEnd >= 0.0)
       {
          lineIter(g, length, centerX, centerY, -(Math.PI - alpha) / 2);
          lineIter(g, length, centerX, centerY, -(Math.PI + alpha) / 2);
          lineIter(g, length, centerX, centerY, (Math.PI - beta) / 2);
          lineIter(g, length, centerX, centerY, (Math.PI + beta) / 2);
       }
   }

}
