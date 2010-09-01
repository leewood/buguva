package Chaos1;
/**
 * @(#)TrupmenicComplex.java
 *
 *
 * @author 
 * @version 1.00 2008/11/27
 */


public class TrupmenicComplex {
    private Complex top;
    private Complex bottom;

    public TrupmenicComplex() {

    }
    public TrupmenicComplex(Complex t, Complex b) {
    	top = t;
    	bottom = b;
    }
    
    public TrupmenicComplex(Complex t) 
    {
    	top = t;
    	bottom = new Complex(1.0, 0.0);
    }

    public TrupmenicComplex(double t)
    {
        bottom = new Complex(1.0, 0.0);
        top = new Complex(t, 0.0);
    }

    public Complex high()
    {
    	return top;
    }
    
    public Complex low()
    {
    	return bottom;
    }
    
    public TrupmenicComplex plus(TrupmenicComplex c)
    {
    	return new TrupmenicComplex(top.times(c.low()).plus(bottom.times((c.high()))), bottom.times(c.low()));
    }

    public TrupmenicComplex plus(double temp)
    {
        TrupmenicComplex c = new TrupmenicComplex(temp);
        return this.plus(c);
    }

    public TrupmenicComplex plus(Complex temp)
    {
        TrupmenicComplex c = new TrupmenicComplex(temp);
        return this.plus(c);
    }


    public Complex toRoundedComplex()
    {
        Complex newHigh = high().times(new Complex(low().real(), -low().imag()));
        Complex newLow = low().times(new Complex(low().real(), -low().imag()));
        return newHigh.div(newLow);
    }

    public TrupmenicComplex minus(TrupmenicComplex c)
    {
    	return new TrupmenicComplex(top.times(c.low()).minus(bottom.times((c.high()))), bottom.times(c.low()));
    }

    public TrupmenicComplex minus(double temp)
    {
        TrupmenicComplex c = new TrupmenicComplex(temp);
        return this.minus(c);
    }

    public TrupmenicComplex minus(Complex temp)
    {
        TrupmenicComplex c = new TrupmenicComplex(temp);
        return this.minus(c);
    }


    public TrupmenicComplex times(TrupmenicComplex c)
    {
    	return new TrupmenicComplex(top.times(c.high()), bottom.times(c.low()));
    }


    public TrupmenicComplex times(double temp)
    {
        TrupmenicComplex c = new TrupmenicComplex(temp);
    	return this.times(c);
    }


    public TrupmenicComplex times(Complex temp)
    {
        TrupmenicComplex c = new TrupmenicComplex(temp);
        return this.times(c);
    }
    
    public TrupmenicComplex div(TrupmenicComplex c)
    {
    	return new TrupmenicComplex(top.times(c.low()), bottom.times(c.high()));
    }

    public TrupmenicComplex div(double temp)
    {
        TrupmenicComplex c = new TrupmenicComplex(temp);
        return this.div(c);
    }


    public TrupmenicComplex div(Complex temp)
    {
        TrupmenicComplex c = new TrupmenicComplex(temp);
        return this.div(c);
    }


    public String toString()
    {
        return high().toString() + " / " + low().toString();
    }

}