data uzd6.binom6;
DO i=1 TO 50;
	x=ranbin(1,3,0.5); /* (sk,n,p) GAUNU X1...X5  np=X p=x/n kur n=3 nes cia ne intervalinai
kokie nors iverciai, o paprastas taskinis, del to imam is modeliuota, intervaliniuose
imsime tikraji N*/
OUTPUT;
END;
run;
proc means data=uzd6.binom6 mean;
var x;
output out=uzd6.binom6_1 mean=vidurkis;
run;
data uzd6.binom6_1;
set uzd6.binom6_1;
skaicius=vidurkis/3; /* n is salygos */
run;
PROC NLP DATA=uzd6.binom6;
max loglik;
parms p=0.5; /*arba pradini*/
bounds 1>p>0;
loglik=log(FACT(3))-log(FACT(x))-log(FACT(3-x))+x*log(p)+(3-x)*log(1-p);
run;
/* kadangi modeliuojam 0.5 p, tai momentu metodu ir bus kazkur prie 0.5, taip ir yra, taigi
ta 0.5 ir naudojam paskui pradiniam nlp taske. 
jei turetume pvz normaluji su 2 ir 3, tai momentu irgi butu panasiai,
tai pradinius tokius galetume ir paimti ;] 

taip pat paprasta suvokti kad pasikl intervaluose N imam dideli, o binominio apskaicavime
momentu metodu bei kitu tuo imam n mazaji*/
/*

n! / (x! (n-x)!) * p^x * (1-p)^ n-x 

ln!

ln n! - ln x! - ln (n-x)! + x ln p + (n-x) ln (1-p)

