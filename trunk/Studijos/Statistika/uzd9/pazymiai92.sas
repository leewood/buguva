data uzd9.pazymiai92;
input grupe paz kiek;
datalines;
1 2 33 
1 3 43 
1 4 80 
1 5 144
2 2 39
2 3 35
2 4 72
2 5 154
;
proc freq data=uzd9.pazymiai92;
tables grupe*paz / CHISQ;
weight kiek;
run;
/*Lentels �Statistics for Table of X by Y� pirmoje eilutje yra spausdinama statistikos
(6.26) reik�m 2 6,5566
1 X = (Value) ir P-reik�m, kuri �iame pavyzdyje yra 0,0875. Gauta Preik�m
 yra didesn u� pasirinkta reik�mingumo lygmeni 0,05, todl hipotez neatmetama.*/
