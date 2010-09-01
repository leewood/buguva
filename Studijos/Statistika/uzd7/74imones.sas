%LET m=27;
%LET n=99;
Data uzd7.imones74;
DO i=1 TO &m;
	x=1;
	OUTPUT;
END; 
DO i=&m TO &n;
	x=0;
	OUTPUT;
END;
PROC FREQ DATA=uzd7.imones74;
TABLES x / BINOMIAL (P=0.2 LEVEL=1);
/*EXACT BINOMIAL;  uzkomentavom exact, bus approx*/
RUN;
/*
hipotez4 atmetama, nes P reik6m4 labai maþa?
*/

/*
Output lange gauname lentele „Test of H0: Proportion = 0.55”. Žiurime i “Exact tests”
(tikslus kriterijus). Eilutje „One-sided“ spaudinama vienpus P-reikšm, eilutje „Twosided“
spaudinama dvipus P-reikšm.
Jei reikia patikrinti hipoteze su dvipuse alternatyva, dvipuse P-reikšme 0.7732
lyginame su reikšmingumo lygmeniu 0.05, gauname, kad hipotez neatmetama
*/
