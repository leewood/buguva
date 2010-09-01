Data uzd8.muses82;
input nr laikas;
datalines;
1 3.1 
1 9.4 
1 15.6 
1 21.9 
1 28.1 
1 34.4 
1 40.6 
1 46.9 
1 53.1
1 59.4
1 65.6
1 71.9
1 78.1
1 84.4
1 90.6
1 96.9
2 56.7
2 63.3
2 70.0
2 76.7
2 83.3
2 90.0
2 96.7
2 3.3 
2 10.0 
2 10.7 
2 23.3 
2 30.0 
2 36.7 
2 43.3 
2 50.0 
;
PROC TTEST DATA=uzd8.muses82;
CLASS nr;
VAR laikas;
WHERE nr=1 or nr=2;
RUN;
run;
