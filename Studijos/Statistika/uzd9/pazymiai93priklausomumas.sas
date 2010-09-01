data uzd9.pazymiai93;
INPUT X $ Y $ sk;
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
RUN;
PROC FREQ DATA=uzd9.pazymiai93 ORDER=DATA;
TABLES X*Y / CHISQ;
WEIGHT sk;
RUN;
Output
/*Lentels „Statistics for Table of X by Y“ pirmoje eilutje yra spausdinama statistikos
(6.21) reikšm 2 121,2857
1 X = (Value) ir P-reikšm, kuri šiame pavyzdyje yra mažesn už
0,0001, todl hipotez atmetama, taigi, gauname, kad X ir Y yra priklausomi.*/
