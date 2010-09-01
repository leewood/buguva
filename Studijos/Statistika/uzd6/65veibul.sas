data uzd6.veibulU5;
do i=1 to 100;
	call streaminit(1);
x=rand('weibull',3,4); /* veibul eta sigma */
output;
end;
run;
PROC MEANS DATA=uzd6.veibulU5;
VAR x; /*analizuojamas kintamasis*/
OUTPUT OUT=uzd6.Param MEAN(X)=vid VAR(X)=disp; /*apskaiciuojame vidurki ir*/
PROC NLP DATA=uzd6.veibulU5;
max loglik;
parms eta=3, sigma=4;
bounds eta>1e-12, sigma>1e-12;
loglik=log(eta) - log(sigma) + (eta-1)*log((x/sigma))-((x/sigma)**eta);
run;

/* 1 - exp (- (x/sigma)^eta)    ' =  - exp (- (x/sigma)^eta) * - eta * (x/sigma)^(eta-1) = 
=  exp (- (x/sigma)^eta) * (eta /sigma)* (x/sigma)^(eta-1)

ln! 

ln exp (- (x/sigma)^eta) + ln (eta/sigma) * (x/sigma)^(eta-1)= 
___________________________________________
ln eta - ln sigma + (eta-1)*(x/sigma) - ((x/sigma)**eta) = 
___________________________________________

*/
/* RESULTING PARAMETERS !!! convergence status */
