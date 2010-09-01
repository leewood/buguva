data uzd9.mendel91;
input name$ kiek;
datalines;
ga 315
gr 101
za 108
zr 32 
;
proc freq data=uzd9.mendel91 order=data;
weight kiek;
tables name / nocum testp=(56.25 18.75 18.75 6.25);
run;
