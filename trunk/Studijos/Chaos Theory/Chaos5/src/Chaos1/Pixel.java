/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package Chaos1;

/**
 *
 * @author nku
 */
public class Pixel {
   public double x;
   public double y;

   public Pixel(int x, int y)
   {
      this.x = x;
      this.y = y;
   }

   public Pixel(Complex z)
   {

       this.x = z.real();
       this.y = z.imag();
   }

   public Complex toComplex()
   {
       return new Complex(x, y);
   }

   public Pixel()
   {
   }

   public Pixel(double x, double y)
   {
       this.x = x;
       this.y = y;
   }


   public double getX()
   {
       return this.x;
   }

   public double getY()
   {
       return this.y;
   }

   public int getXi()
   {
       return (int)getX();

   }

   public int getYi()
   {
       return (int)getY();
   }


   public int getXo(int offset)
   {
       return (int)(getX() * offset + offset / 2);
   }

   public int getYo(int offset)
   {
       return (int)(-getY() * offset + offset);
   }

   public void setX(double x)
   {
       this.x = x;

   }

   public void setY(double y)
   {
       this.y = y;
   }
}
