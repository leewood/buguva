Data uzd7.stazas75;
input stazas alga @@;
datalines;
2 100 8 500 1.5 300 7 400 
3 400 5 400 10 600 4 250
12 600 2 200 4 300 1 100
2 100 6 350
;
Proc corr data=uzd7.stazas75;
VAR stazas alga;
run;
/*stipri teigiama koreliacija tarp kintamuju - priklauso*/
