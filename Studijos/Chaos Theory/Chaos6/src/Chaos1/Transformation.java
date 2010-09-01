/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package Chaos1;
import jv.number.PuComplex;
/**
 *
 * @author kuosis
 */
public class Transformation {
    private double a, b, c, d, e, f, koef;
    private PuComplex z;
    private int zadd;

    public Transformation(double a, double b, double c, double d, double e, double f, double koef)
    {
        this.a = a;
        this.b = b;
        this.c = c;
        this.d = d;
        this.e = e;
        this.f = f;
        this.koef = koef;
    }

    public Transformation(PuComplex z, int index)
    {
        this.z = z;
        this.zadd = index;
    }

    public Transformation(double a, double b, double c, double d, double e, double f)
    {
        this.a = a;
        this.b = b;
        this.c = c;
        this.d = d;
        this.e = e;
        this.f = f;
        this.koef = 1;
    }

    public Transformation(double a, double b, double c, double d)
    {
        this.a = a;
        this.b = b;
        this.c = c;
        this.d = d;
        this.e = 0;
        this.f = 0;
        this.koef = 1;

    }

    public Transformation(double a, double b, double c, double d, double koef)
    {
        this.a = a;
        this.b = b;
        this.c = c;
        this.d = d;
        this.e = 0;
        this.f = 0;
        this.koef = koef;
    
    }

    public Transformation(int type)
    {
        switch (type)
        {
            case 1:
                a = 0;
                b = -1;
                c = 1;
                d = 0;
                e = 1;
                f = 0;
                koef = 0.5;
                break;
            case 2:
                a = -1;
                b = 0;
                c = 0;
                d = -1;
                e = 1;
                f = 1;
                koef = 0.5;
                break;
            case 3:
                a = 0;
                b = 1;
                c = -1;
                d = 0;
                e = 0;
                f = 1;
                koef = 0.5;
                break;
            case 4:
                a = 0;
                b = -1;
                c = -1;
                d = 0;
                e = -1;
                f = -1;
                koef = 0.5;
                break;
            case 5:
                a = 0;
                b = 1;
                c = -1;
                d = 0;
                e = -1;
                f = 0;
                koef = 0.5;
                break;
            case 6:
                a = 0;
                b = 1;
                c = 1;
                d = 0;
                e = 0;
                f = 0;
                koef = 0.5;
                break;
            case 7:
                a = -1;
                b = 0;
                c = 0;
                d = 1;
                e = -1;
                f = 0;
                koef = 0.5;
                break;

               
        }
    }

    public Transformation(Transformation trans)
    {
        this.a = trans.a;
        this.b = trans.b;
        this.c = trans.c;
        this.d = trans.d;
        this.e = trans.e;
        this.f = trans.f;
        this.koef = trans.koef;
    }

    public Pixel CalcMovement(int depth)
    {
        Pixel result = new Pixel(this.e, this.f);
        for (int i = 0; i < depth; i++)
        {
            result.x = result.x * a + result.y * b;
            result.y = result.x * c + result.y * d;
        }
        return result;
    }

    public Pixel PointTransform(Pixel pix, int depth)
    {
        Pixel result = new Pixel();
        PuComplex p = new PuComplex();
        p.set(pix.getX(), pix.getY());
        p = p.mult(z);
        p = p.add(zadd);
        result.setX(p.re);
        result.setY(p.im);
        return result;
    }


    public Poly Transform(Poly poly, int depth)
    {
        Poly result = new Poly();
        for (int i = 0; i < poly.Count(); i++)
        {
            result.Add(PointTransform(poly.Get(i), depth));
        }
        return result;
    }
}
