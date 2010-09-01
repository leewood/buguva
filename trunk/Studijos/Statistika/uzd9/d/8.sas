DATA dagr9.u8;
DO i=1 TO 100;
 X=RAND('EXPONENTIAL'); 
 OUTPUT;
END;
RUN;
proc univariate data=dagr9.u8 NOPRINT;
var X;
HISTOGRAM X / EXP;
RUN;
proc univariate data=dagr9.u8 NOPRINT;
var X;
HISTOGRAM X / NORMAL;
RUN;




