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

   public void paint( Graphics g )
   {
       this.setBackground(Color.white);
      Calculations calc = new Calculations();
      calc.setStep(stepCount);
      calc.calculate(generatedSource, generatedDestination);
      ArrayList oldPixels = new ArrayList();
      Pixel oldpix1 = null;
      Pixel oldpix2 = null;
      Color oldColor = null;
      if (paintStartEnd)
      {
        for (int i = 0; i < generatedSource.size(); i++)
        {
            Pixel pix11 = (Pixel)generatedSource.get(i);
            Pixel pix21 = (Pixel)generatedDestination.get(i);
            Pixel pix12 = new Pixel();
            Pixel pix22 = new Pixel();
            if (i != generatedSource.size() - 1)
            {
               pix12 = (Pixel)generatedSource.get(i + 1);
               pix22 = (Pixel)generatedDestination.get(i + 1);
            }
            else
            {
               pix12 = (Pixel)generatedSource.get(0);
               pix22 = (Pixel)generatedDestination.get(0);
            }
            g.setColor(Color.BLUE);
            g.drawLine(pix11.x, pix11.y, pix12.x, pix12.y);
            g.setColor(Color.RED);
            g.drawLine(pix21.x, pix21.y, pix22.x, pix22.y);
          }
      }
      g.setColor(Color.GREEN);
      int rgb = Color.GREEN.getRGB();

      for (int k = 0; k <= untilStep; k++)
      {
        
        if ((k == untilStep) || ((k < untilStep) && (paintTraces)))
        {
             if (!showTraces)
             {
                //g.setXORMode(oldColor);

                 g.setColor(Color.WHITE);
                for (int i = 0; i < oldPixels.size(); i++)
                {
                    Pixel pix11 = (Pixel)oldPixels.get(i);
                    Pixel pix12 = new Pixel();

                    if (i != oldPixels.size() - 1)
                    {
                       pix12 = (Pixel)oldPixels.get(i + 1);
                    }
                    else
                    {
                       pix12 = (Pixel)oldPixels.get(0);

                    }
                    g.drawLine(pix11.x, pix11.y, pix12.x, pix12.y);

                }
      if (paintStartEnd)
      {
        for (int i = 0; i < generatedSource.size(); i++)
        {
            Pixel pix11 = (Pixel)generatedSource.get(i);
            Pixel pix21 = (Pixel)generatedDestination.get(i);
            Pixel pix12 = new Pixel();
            Pixel pix22 = new Pixel();
            if (i != generatedSource.size() - 1)
            {
               pix12 = (Pixel)generatedSource.get(i + 1);
               pix22 = (Pixel)generatedDestination.get(i + 1);
            }
            else
            {
               pix12 = (Pixel)generatedSource.get(0);
               pix22 = (Pixel)generatedDestination.get(0);
            }
            g.setColor(Color.BLUE);
            g.drawLine(pix11.x, pix11.y, pix12.x, pix12.y);
            g.setColor(Color.RED);
            g.drawLine(pix21.x, pix21.y, pix22.x, pix22.y);
          }
      }

                //g.setPaintMode();
             }
          oldPixels.clear();
          for (int i = 0; i < generatedSource.size(); i++)
          {
          Pixel pix11 = (Pixel)generatedSource.get(i);

          Pixel pix12 = new Pixel();
          int index2 = 0;          
          if (i != generatedSource.size() - 1)
          {
             pix12 = (Pixel)generatedSource.get(i + 1);
             index2 = i + 1;
          }
          else
          {
             pix12 = (Pixel)generatedSource.get(0);
          
          }
              g.setColor(new Color(rgb));
              Pixel pixk1 = calc.calculatPixel(pix11, k, i);
              Pixel pixk2 = calc.calculatPixel(pix12, k, index2);
              oldPixels.add(pixk1);
              oldColor = g.getColor();
              g.drawLine(pixk1.x, pixk1.y, pixk2.x, pixk2.y);
              if (changeColors)
              {
                 rgb += 10;
              }
          }
        }
             if (usePause)
                try
                {
                  if (pauses > 0)
                  {
                    Thread.sleep(pauses);
                  }
                } catch (Exception e)
                {
                }

      }
   }

}
