/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package Chaos1;
import java.awt.*;
import java.util.Random;
import java.util.ArrayList;
import jv.number.PuComplex;
/**
 *
 * @author nku
 */

public class PaintingArea extends java.awt.Canvas{
   private ArrayList generatedSource = new ArrayList();
   private ArrayList generatedDestination = new ArrayList();
   public int tr1 = 1;
   public int tr2 = 2;
   public int tr3 = 3;

   

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
   public int untilStep = -1;
   public int pauses = 10;
   public boolean changeColors = true;
   public PuComplex lambda = new PuComplex();
   public double alpha = 0.0;
   public double beta = 0.0;
   public int length;
   public Poly stdPoly = new Poly();
   private Graphics innerG = null;
   public int max = 11;
   public ArrayList<Transformation> transformations = new ArrayList<Transformation>();

   public void iterate(Poly input, int depth, Color clr)
   {
       if (depth < max)
       {
           for (int i = 0; i < transformations.size(); i++)
           {
               Poly tr = transformations.get(i).Transform(input, depth);
               iterate(tr, depth + 1, Color.BLACK);
           }
       }
       else
       {
           input.PaintPoly(innerG, length / 2, clr);
       }
   }

   public void iterate2(Pixel input, int depth)
   {
       if (depth < max)
       {
           for (int i = 0; i < transformations.size(); i++)
           {
               Pixel tr = transformations.get(i).PointTransform(input, depth);
               innerG.drawLine(tr.getXo(length),  tr.getYo(length), tr.getXo(length), tr.getYo(length));
               iterate2(tr, depth + 1);
           }
       }
   }


   public void initPoly(int length)
   {
       stdPoly = new Poly();
       stdPoly.Add(new Pixel(-0.1, 0.1));
       stdPoly.Add(new Pixel(-0.1, -0.1));
       stdPoly.Add(new Pixel(0.1, -0.1));
       stdPoly.Add(new Pixel(0.1, 0.1));
       
       
   }

   public void initTransformations(PuComplex pu)
   {
       if (transformations == null)
       {
           transformations = new ArrayList<Transformation>();
       }
       transformations.clear();
       transformations.add(new Transformation(pu, -1));
       transformations.add(new Transformation(pu, 1));
   }

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
       if (untilStep >= 0)
       {
         max = untilStep;
         if (untilStep == 0)
         {
             max = 10;
         }
         int width = this.getWidth();
         int height = this.getHeight();
         length =  height;
         innerG = g;
         initPoly(height);
         Pixel pix = new Pixel(0, 0);         
       //for (int i = 0; i < 1; i++)
       //{
         //iterate2(pix, 0);
       //}
       iterate(stdPoly, 0, Color.BLACK);
       }
   }

}
