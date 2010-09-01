data uzd6.lognorm53;
SET uzd5.Chemija54;
logar=LOG(kiekis);
PROC UNIVARIATE DATA=uzd6.lognorm53 CIBASIC (TYPE=TWOSIDED ALPHA=0.05);
VAR logar;
RUN;
