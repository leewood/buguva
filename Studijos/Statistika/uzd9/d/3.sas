DATA dagr9.u3;
INPUT X $ Y $ kiekis;
DATALINES;
4-5 5 110
4-5 4 70
4-5 3 60
4-5 2 10
3 5 0
3 4 10
3 3 10
3 2 30
;
PROC FREQ DATA=dagr9.u3 ORDER=DATA; 
 TABLES X*Y / CHISQ;
 WEIGHT kiekis; 
RUN;
