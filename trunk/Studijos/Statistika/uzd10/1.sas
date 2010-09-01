DATA dagr10.u1;
INPUT grupe $ NKiekis @@;
DATALINES;
D1 4.079 D1 4.859 D1 3.540 D1 5.047 D1 3.298 D1 4.679 D1 2.870 D1 4.648 D1 3.847
D2 4.368 D2 5.668 D2 3.752 D2 5.848 D2 3.802 D2 4.844 D2 3.578 D2 5.393 D2 4.374
D3 4.169 D3 5.709 D3 4.416 D3 5.666 D3 4.123 D3 5.059 D3 4.403 D3 4.496 D3 4.688
D4 4.928 D4 5.608 D4 4.940 D4 5.291 D4 4.674 D4 5.038 D4 4.905 D4 5.208 D4 4.806
;
RUN;
proc GLM data=dagr10.u1;
class grupe;
model NKiekis=grupe;
means grupe /TUKEY BON HOVTEST=LEVENE WELCH;  /* BON  - daugkartiniai palyginimai */
means grupe; 
run;
Symbol1 i=stdtmj v=none l=1 w=1 c=black;
Axis1 label=(a=90 r=0 h=1.5) value=(h=1.5) offset=(4) order=(20 to 40 by 5);
Axis2 label=(h=1.5) value=(h=1.5) offset=(4);
Proc gplot data=dagr10.u1;
Plot NKiekis*grupe / frame hminor=0 vminor=4 vaxis=axis1 haxis=axis2; 
Run; Quit;
/*
a) Patikrinkite atsitiktiniø paklaidø dispersijø lygybæ. 
su reiksmingumo lygmeniu 0,05 atmetam, nes PR > F 0,0208, todel dispersija tikrinsim su Welch Anova.
Su Welch Anova Pr > F 0,0220 tai vis tiek atmetam - nelygios dispersijos

b) Ávertinkite neþinomus parametrus. 
c) Nubraiþykite vidurkiø trendø grafikà. 
d) Ar dietos tipas turi átakos iðpuèiamo azoto kiekiui?
Kazkodel D4 ir D1 grupes issiskiria is kitu. Kuo daugiau vartojama baltymø, tuo daugiau iðkvepiama N.
e) Jei reikia atlikite daugkartinius palyginimus.
f) Apskaièiuokite rodiklius (jeigu ðiuo atveju jie turi prasmës), 
parodanèius faktoriaus átakà priklausomo kintamojo reikðmëms.  
*/
