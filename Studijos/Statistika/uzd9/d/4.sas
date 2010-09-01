DATA dagr9.u4;
INPUT A $ B $ kiekis;
DATALINES;
Nenormal Nerukant 2
Nenormal Rukantys 16
Nenormal Mete 4
Normalus Nerukant 64
Normalus Rukantys 83
Normalus Mete 46
;
PROC FREQ DATA=dagr9.u4 ORDER=DATA; 
 TABLES A*B / CHISQ;
 WEIGHT kiekis;
 EXACT FISHER;
RUN;
