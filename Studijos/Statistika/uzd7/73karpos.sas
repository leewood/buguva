Data uzd7.karpos73;
input x$ @@;
datalines;
t   n    t    n   t   t    n    t    n    n    t    n    t    n    t    n    n    t    t    n    t    t   n   t   t
;
PROC FREQ DATA=uzd7.karpos73;
TABLES x / BINOMIAL (P=0.4 LEVEL='t');
EXACT BINOMIAL;
RUN;
run;
/*
Output lange gauname lentele „Test of H0: Proportion = 0.55”. Žiurime i “Exact tests”
(tikslus kriterijus). Eilutje „One-sided“ spaudinama vienpus P-reikšm, eilutje „Twosided“
spaudinama dvipus P-reikšm.
Jei reikia patikrinti hipoteze su dvipuse alternatyva, dvipuse P-reikšme 0.7732
lyginame su reikšmingumo lygmeniu 0.05, gauname, kad hipotez neatmetama
*/
