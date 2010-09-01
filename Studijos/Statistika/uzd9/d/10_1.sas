DATA dagr9.u10;
INPUT grupe $ isgyjim;
DATALINES;
I 5
I 4
I 1
I 7
I 4
I 3
I 6
I 7
I 8
I 7
II 5
II 8
II 2
II 8
II 7
II 4
II 5
II 4
II 6
II 4
III 5
III 3
III 7
III 1
III 2
III 4
III 2
III 1
III 4
III 5
III 4
III 5
;
PROC NPAR1WAY EDF DATA=dagr9.u10; 
CLASS grupe;
VAR isgyjim;
RUN; 
