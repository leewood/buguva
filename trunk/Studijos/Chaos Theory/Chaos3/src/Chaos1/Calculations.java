package Chaos1;
/**
 * @(#)Calculations.java
 *
 *
 * @author 
 * @version 1.00 2008/11/27
 */

import java.util.ArrayList;
public class Calculations {

    public Calculations()
    {
        setStep(10);
    }


    public ArrayList allA = new ArrayList();
    public ArrayList allB = new ArrayList();
    public ArrayList allC = new ArrayList();
    public TrupmenicComplex a = new TrupmenicComplex();
    public TrupmenicComplex b = new TrupmenicComplex();
    public TrupmenicComplex c = new TrupmenicComplex();
    public TrupmenicComplex defaultStep = new TrupmenicComplex();
    private int step = 10;

    public int getStep()
    {
       return step;
    }

    public void setStep(int step)
    {
        Complex comp = new Complex(step, 0.0);
        defaultStep = new TrupmenicComplex(comp);
        this.step = step;
    }

    public void calculate(Complex z1, Complex z2, Complex z3, Complex w1, Complex w2, Complex w3)
    {
        
    	TrupmenicComplex k = new TrupmenicComplex(w1.times(z1).minus(w2.times(z2)), z1.minus(z2));
    	TrupmenicComplex l = new TrupmenicComplex(w1.minus(w2), z1.minus(z2));
    	TrupmenicComplex m = new TrupmenicComplex(z3);
    	m = m.times((new TrupmenicComplex(w3)).minus(k));
    	TrupmenicComplex n = new TrupmenicComplex(w3);
    	n = n.minus(l.times(z3));
        TrupmenicComplex o = k.times(z2).plus(m).minus(z2.times(w2));
        TrupmenicComplex p = (new TrupmenicComplex(w2)).minus(n);
        TrupmenicComplex c = p.div(o);
        TrupmenicComplex b = c.times(m).plus(n);
        TrupmenicComplex a = c.times(k).plus(l);        
        this.a = a;
        this.b = b;
        this.c = c;
        TrupmenicComplex kof1 = new TrupmenicComplex(w1.times(z1).minus(w2.times(z2)), z1.minus(z2));
        TrupmenicComplex kof2 = new TrupmenicComplex(w1.minus(w2), z1.minus(z2));
        TrupmenicComplex kof3 = (new TrupmenicComplex(w3)).minus(kof1).times(z3);
        //TrupmenicComplex kof4 =

    }

    public Pixel calculatPixel(Pixel pix, int stepIndex)
    {
        TrupmenicComplex c_k = c.times((double)stepIndex / getStep());
        TrupmenicComplex b_k = b.times((double)stepIndex / getStep());
        TrupmenicComplex a_k = a.minus(1).times((double)stepIndex / getStep()).plus(1);
        Pixel result = new Pixel();
        TrupmenicComplex high = a_k.times(pix.toComplex()).plus(b_k);
        TrupmenicComplex low = c_k.times(pix.toComplex()).plus(1);
        result = new Pixel(high.div(low).toRoundedComplex());
        return result;
    }

    public Pixel calculatPixel(Pixel pix, int stepIndex, int pixelIndex)
    {
        int arrayIndex = 0;
        if (pixelIndex > 0)
        {
            arrayIndex = (pixelIndex - 1) / 2;
        }
        TrupmenicComplex a = (TrupmenicComplex)allA.get(arrayIndex);
        TrupmenicComplex b = (TrupmenicComplex)allB.get(arrayIndex);
        TrupmenicComplex c = (TrupmenicComplex)allC.get(arrayIndex);
        TrupmenicComplex c_k = c.times((double)stepIndex / getStep());
        TrupmenicComplex b_k = b.times((double)stepIndex / getStep());
        TrupmenicComplex a_k = a.minus(1).times((double)stepIndex / getStep()).plus(1);
        Pixel result = new Pixel();
        TrupmenicComplex high = a_k.times(pix.toComplex()).plus(b_k);
        TrupmenicComplex low = c_k.times(pix.toComplex()).plus(1);
        result = new Pixel(high.div(low).toRoundedComplex());
        return result;
    }


    public void calculate(ArrayList start, ArrayList end)
    {
        allA.clear();
        allB.clear();
        allC.clear();
        if (start.size() >= 3)
        {
          for (int i = 0; i < start.size(); i += 2)
          {
             if (i + 1 < start.size())
             {
             Pixel pix1 = (Pixel)start.get(i);
             Pixel pix2 = (Pixel)start.get(i + 1);
             Pixel pix3 = null;
             if (i + 2 < start.size())
             {
                 pix3 = (Pixel)start.get(i + 2);
             }
             else
             {
                pix3 = (Pixel)start.get(0);
             }

             Pixel pix4 = (Pixel)end.get(i);
             Pixel pix5 = (Pixel)end.get(i + 1);
             Pixel pix6 = null;
             if (i + 2 < end.size())
             {
                 pix6 = (Pixel)end.get(i + 2);
             }
             else
             {
                pix6 = (Pixel)end.get(0);
             }

             calculateNew(pix1.toComplex(), pix2.toComplex(), pix3.toComplex(), pix4.toComplex(), pix5.toComplex(), pix6.toComplex());
             allA.add(this.a);
             allB.add(this.b);
             allC.add(this.c);
             System.out.println("It:" + i);
             System.out.println(this.a.toString());
             System.out.println(this.b.toString());
             System.out.println(this.c.toString());
             }
          }
        }

    }

void calculateNew(Complex z1, Complex z2, Complex z3, Complex w1, Complex w2, Complex w3)
{
     TrupmenicComplex a1, a2, a3, b1, b2, b3, c1, c2, c3, d1, d2, d3,det, detx, dety, detz;
     a1 = new TrupmenicComplex(z1);
     a2 = new TrupmenicComplex(z2);
     a3 = new TrupmenicComplex(z3);
     b1 = new TrupmenicComplex(1);
     b2 = new TrupmenicComplex(1);
     b3 = new TrupmenicComplex(1);
     c1 = new TrupmenicComplex(0).minus(z1.times(w1));
     c2 = new TrupmenicComplex(0).minus(z2.times(w2));
     c3 = new TrupmenicComplex(0).minus(z3.times(w3));
     d1 = new TrupmenicComplex(new Complex(0, 0).minus(w1));
     d2 = new TrupmenicComplex(new Complex(0, 0).minus(w2));
     d3 = new TrupmenicComplex(new Complex(0, 0).minus(w3));

     det=finddet(a1,a2,a3,b1,b2,b3,c1,c2,c3);   /*Find determinants*/
     detx=finddet(d1,d2,d3,b1,b2,b3,c1,c2,c3);
     dety=finddet(a1,a2,a3,d1,d2,d3,c1,c2,c3);
     detz=finddet(a1,a2,a3,b1,b2,b3,d1,d2,d3);
     this.a = new TrupmenicComplex(0).minus(detx.div(det));
     this.b = new TrupmenicComplex(0).minus(dety.div(det));
     this.c = new TrupmenicComplex(0).minus(detz.div(det));
}

public TrupmenicComplex finddet(TrupmenicComplex a1,TrupmenicComplex a2, TrupmenicComplex a3,
               TrupmenicComplex b1, TrupmenicComplex b2, TrupmenicComplex b3,
               TrupmenicComplex c1, TrupmenicComplex c2, TrupmenicComplex c3)
{
    return a1.times(b2.times(c3)).minus((a1.times(b3).times(c2))).minus(a2.times(b1).times(c3)).plus(a3.times(b1).times(c2)).plus(a2.times(b3).times(c1)).minus(a3.times(b2).times(c1));
}



    
    
}