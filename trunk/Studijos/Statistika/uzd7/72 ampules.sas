data uzd7.ampules72;
input amp @@;
datalines;
310   312   298   270   280   300   305   311   290  288   302   330   320   295  289
;
proc ttest data=uzd7.ampules72 H0=300;
var amp;
run;
proc univariate data=uzd7.ampules72 mu0=300;
var amp;
run;
