DATA uzd9.fiseris92;
INPUT X$ Y $ sk;
DATALINES;
A N 2
A R 16
A M 4
B N 64
B R 83
B M 46
;RUN;
PROC FREQ DATA=uzd9.fiseris92;
TABLES X*Y / CHISQ;
WEIGHT sk;
EXACT FISHER;
RUN;
/*Kadangi gauta P-reik�m didesn u� pasirinkta reik�mingumo
lygmeni 0,05, tai hipotez neatmetama, t.y. kintamieji X ir Y nepriklausomi.*/
