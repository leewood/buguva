Data uzd8.krakmolas81;
input x y;
datalines;
21.7 21.5  
18.7 18.7  
18.3 18.3  
17.5 17.4  
18.5 18.3  
15.6 15.4  
17.0 16.7  
16.6 16.9  
14.0 13.9
17.2 17.0
21.7 21.4
18.6 18.6
17.9 18.0
17.7 17.6
18.3 18.5
15.6 15.5
;
run;
DATA uzd8.krakmolas81;
SET uzd8.krakmolas81;
skirt=x-y;
PROC univariate data=uzd8.krakmolas81;
var skirt;
RUN;
