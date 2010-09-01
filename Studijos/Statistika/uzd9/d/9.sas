DATA uzd;
INPUT gamykla laikas;
DATALINES;
1 41
1 70
1 26
1 89
1 62
1 54
1 46
1 77
1 34
1 51
2 23
2 35
2 29
2 38
2 21
2 53
2 31
2 25
2 36
2 50
2 61
;
PROC NPAR1WAY EDF DATA=uzd; 
CLASS gamykla;   
VAR laikas;
RUN; 
