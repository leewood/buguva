Data uzd7.pakl75;
input pakl @@;
datalines;
0.86  0.06  1.49  1.02  1.39  0.91  1.18  -1.50  -0.69  1.37
;
PROC MEANS DATA=uzd7.pakl75 T PROBT;
VAR pakl;
RUN;
