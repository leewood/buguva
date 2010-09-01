DATA dagr10.u3;
INPUT amzius trukme @@;
DATALINES;
1 2.5 2 3 6 9.5 8 11 3 4 2 2.5 11 15 15 20.5 8 10.5 4 5.5 6 9 12 16.5 9 12.5 10 13.5 3 4.5 4 5.5
;
RUN;
proc reg data=dagr10.u3;
model amzius=trukme / clb alpha=0.1;
plot amzius*trukme;
plot amzius*trukme / pred conf;
plot r.*trukme;
plot r.*npp.;
run; quit;

