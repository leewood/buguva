Data uzd7.kava76;
input rinka reklama;
datalines;
15.7 7.6
3.9  3.5
10.6 6.1
9.6  6.8
12.3 8.3
26.2 10.1
21.7 7.1
;
Proc corr data=uzd7.kava76 SPEARMAN;
VAR rinka reklama;
run;
/*stipri teigiama koreliacija tarp kintamuju - priklauso*/
