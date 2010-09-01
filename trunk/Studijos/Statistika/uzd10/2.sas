DATA dagr10.u2;
INPUT grupe $ kiekis @@;
DATALINES;
Maz 7.6 Maz 8.2 Maz 6.8 Maz 5.8 Maz 6.9 Maz 6.6 Maz 6.3 Maz 7.7 Maz 6.0
Vid 6.7 Vid 8.1 Vid 9.4 Vid 8.6 Vid 7.8 Vid 7.7 Vid 8.9 Vid 7.9 Vid 8.3 Vid 8.7 Vid 7.1 Vid 8.4
Did 8.5 Did 9.7 Did 10.1 Did 7.8 Did 9.6 Did 9.5
;
RUN;
proc GLM data=dagr10.u2;
class grupe;
model kiekis=grupe;
means grupe /TUKEY BON HOVTEST=LEVENE;  /* BON  - daugkartiniai palyginimai */
means grupe; 
run; quit;

/*
H: darbo nasumo padidejimas vienodas
ats: Pr > F <0.0001 todel H atmetama su reiksmingumo lygmeniu 0.05
*/
