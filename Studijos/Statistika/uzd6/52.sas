/*2) Modeliuoti a.d.   (Puasono skirstinys su parametru  ) didumo n=50 imt?.                                                            
Rasti parametro   taškin? ir intervalin? (Q=0.95) ?ver?ius.*/                                                                           
%LET n=50;
%LET lambda=2;
DATA uzd6.puason52;
DO i=1 TO &n;
	X=ranpoi(10,&lambda);
	OUTPUT;
END;
PROC MEANS DATA=uzd6.puason52 MEAN;
VAR X;
%LET n=50; /* stebejimu skaicius*/
%LET alpha=0.05; /* 0.95 pasikl. intervalas*/
PROC MEANS NOPRINT DATA=uzd6.puason52;
VAR X; /*analizuojamas kintamasis*/
OUTPUT OUT=uzd6.intervalas SUM(X)=suma; /*apskaiciuojame reikšmiu suma*/
RUN; /*ir irašome i lentele intervalas*/
DATA uzd6.intervalas;
SET uzd6.intervalas;
lambda_apat=CINV(&alpha/2,2*suma)/2/&n; /*viršutinis intervalo ržis */
lambda_virsut=CINV(1-&alpha/2,2*suma+2)/2/&n; /*apatinis intervalo ržis*/
KEEP lambda_apat lambda_virsut;
RUN;
