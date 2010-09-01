data uzd7.svoriai71;
input svoris @@;
datalines;
9.87  9.55 9.95 10.02 10.25  9.78 9.76  9.52 10.79 10.17
10.18 10.10 9.82 10.47  9.72 10.08 9.31 10.31 10.51  9.63
;
proc means data=uzd7.svoriai71 mean var;
output out=uzd7.svoriai71_1 mean=vid var=disp;
/*sudarom hipoteze 
H0 vid = 10
alternatyva
h1 vid != 10*/
DATA uzd7.svoriai71_1;
SET uzd7.svoriai71_1;
Z=sqrt(20)*(vid-10)/sqrt(disp);	/*150psl murausko*/
w=tinv(1-0.05/2, 19);	/*stjudento skirst alpha pasirenkam 0.05, n-1 laisves laipsnis*/
keep Z w vid disp;
/* |Z| < w H0 neatmetama*/
/*************************************************/
/*
sudarom hipoteze apie dispersija
H0 disp <= 0.1
*/
DATA uzd7.svoriai71_1;
SET uzd7.svoriai71_1;
Tkv=disp *19/0.1;
chi=cinv(1-0.05, 19);
keep Z w Tkv chi;
/*Tkv < chi - neatmetam*/
run;

