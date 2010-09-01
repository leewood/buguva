data uzd8.impuls83;
input band y @@;
datalines;
1 9 2 15
1 9 2 16
1 8 2 17
1 10 2 23
1 12 2 22
1 13 2 20
1 10 2 21
1 10 2 24
1 . 2 27
;
proc ttest data=uzd8.impuls83;
class band;
var y;
run;
