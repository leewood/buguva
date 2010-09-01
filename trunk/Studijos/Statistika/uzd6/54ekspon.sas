data uzd6.ekspo54;
INPUT rezis daznis;
DATALINES;
150     53  
450     41
750     30
1050    22
1350    16
1650    12
1950    9
2250    7
2550   5
2850    3
3150    2
;
Proc means data=uzd6.ekspo54 mean N sum;  
Var  rezis;
Freq daznis;
output out=uzd6.ekspo54_1 mean=vid N=imties_dyd sum=suma;
DATA uzd6.ekspo54_2;
SET uzd6.ekspo54_1;
low=CINV(1-0.005, 2*imties_dyd)/2/suma;
up=CINV(0.005, 2*imties_dyd)/2/suma;
lambda_t=1/vid;
lambda_nep=(imties_dyd-1)/suma;
RUN;
