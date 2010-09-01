DATA dagr9.u6;
SET dagr8.u1;
z=krak1-krak2;
RUN;
PROC UNIVARIATE Data=dagr9.u6;
var z;
RUN;

