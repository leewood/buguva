/* An example for calling the sas macro
%include 'logmean.sas';
%logmean(n=20, xbar=0.8, s=0.5, pel=1.005, conflev=0.95,check=1);
run;



/*********************************************************************
NAME: logmean.sas

 This program computes confidence limits and p-values for testing
 about the mean exp(mu + sigma^2/2) of a lognormal distribution
 with parameter mu and sigma^2. The methods are based on the
 generalized p-value and the generalized limit. Let x1,...,xn be a
 sample from a lognormal population.

 Input:      n = sample size
          xbar = mean of ln(x1),...,ln(xn)
             s = std deviation of ln(x1),...,ln(xn)
           pel = value of lognormal mean under H0
       conflev = confidence level of intervals
           num = number of generalized variables. Default: num = 100000
         check = chioce of output (1 or 2)

 Output:
     check = 1:  p-values for left-tail, right-tail and two-tail tests
     check = 2:  one-sided and two-sided confidence limits

 Reference: Krishnamoorthy, K. and Mathew, T. (2002). Inferences on
 the means of  lognormal distributions using generalized p-values and
 generalized confidence intervals. To appear in the Journal of Statistical
 Planning and Inference
***************************************************************************/

%macro logmean(n=, xbar=, s=, pel=, conflev=, check=, num=100000);

options ls = 64 ps = 45 nodate nonumber;
title 'Output of lognormal mean';
proc iml;

check=&check;
pel = &pel;
conflev=&conflev;
n = &n;
s = &s;
xbar = &xbar;
m = &num;

df = n-1.0;
const1 = s*sqrt(df/n);
const2 = 0.5*s*s*df;

pvalleft = 0.0;
pvalright = 0.0;
pvaltwo = 0.0;
seed1 = int(time());
seed2 = int(time()+12345);

gv=j(m,1,0);

do i= 1 to m;
   z = rannor(seed1);
   v = 2.0*rangam(seed2,df/2.0);
   gv[i] = xbar + z*const1/sqrt(v)+const2/v;
   if gv[i] > pel then pvalleft = pvalleft + 1.0;
   if gv[i] < pel then pvalright = pvalright + 1.0;
end;

reset name=noname center=nocenter;

if check=2 then
do;
  L1=int((1.0-conflev)*m);
  L2=int(conflev*m);
  Lt1=int(0.5*(1.0-conflev)*m);
  Lt2=int(0.5*(1.0+conflev)*m);
  gv0=gv;
  gv[rank(gv)]=gv0;

  gvl1=exp(gv[L1]);
  gvl2=exp(gv[L2]);

  gvlt1=exp(gv[Lt1]);
  gvlt2=exp(gv[Lt2]);

  print 'n=' n[format=2.0] ',' 's=' s[format=6.3] ','
   'xbar=' xbar[format=6.3] ',' 'conflel=' conflev[format=4.2];
  print conflev 'one-sided lower limit is' gvl1 [format=10.4];
  print conflev 'one-sided upper limit is' gvl2 [format=10.4];
  print conflev 'confidence interval is (' gvlt1 [format=8.4]',' gvlt2 [format=8.4] ')' ;
end;

reset fw=6;
if check=1 then
do;
  pvalue_left=pvalleft/m;
  pvalue_right=pvalright/m;
  pvalue_two=2.0*min(pvalue_left,pvalue_right);
  print 'n=' n[format=2.0] ',' 's=' s[format=6.3] ','
   'xbar=' xbar[format=6.3] ',' 'pel=' pel[format=6.3];
  print 'The p-value for left-tail test is: ' pvalue_left;
  print 'The p-value for right-tail test is: ' pvalue_right;
  print 'The p-value for two-tail test is: ' pvalue_two;

end;

quit;

%mend logmean;
