DATA dagr10.u5;
INPUT spaud rizik @@;
DATALINES;
150 27 162 40 155 28 173 68 195 43 188 55 155 33 120 43 136 49 100 30 150 37 170 51 135 31 200 62 199 30 118 40 165 30 126 45 118 26 203 52 149 27 121 30
;
RUN;
proc reg data=dagr10.u5;
model rizik=spaud / clb alpha=0.1;
plot rizik*spaud;
plot rizik*spaud / pred conf;
plot r.*spaud;
plot r.*npp.;
run; quit;
/*
Kokia rizika 180?
*/
