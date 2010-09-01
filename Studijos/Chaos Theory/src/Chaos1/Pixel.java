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
   public int x;
   public int y;

   public Pixel(int x, int y)
   {
      this.x = x;
      this.y = y;
   }

   public Pixel(Complex z)
   {

       this.x = (int)Math.round(z.real());
       this.y = (int)Math.round(z.imag());
   }

   public Complex toComplex()
   {
       return new Complex(x, y);
   }

   public Pixel()
   {
   }
   

}
