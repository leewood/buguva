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
Output lange gauname lentele �Test of H0: Proportion = 0.55�. �iurime i �Exact tests�
(tikslus kriterijus). Eilutje �One-sided� spaudinama vienpus P-reik�m, eilutje �Twosided�
spaudinama dvipus P-reik�m.
Jei reikia patikrinti hipoteze su dvipuse alternatyva, dvipuse P-reik�me 0.7732
lyginame su reik�mingumo lygmeniu 0.05, gauname, kad hipotez neatmetama
*/
