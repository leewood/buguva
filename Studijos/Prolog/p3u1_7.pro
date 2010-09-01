% Karolis Uosis
%  Programu sistemos, 4 kursas 3 grupe
%  MIF, 2009
%  3 praktinis darbas 1.7 užduotis

tolygus([X|[Y|[]]], D) :- X - Y =< D, 
                          X - Y >= -D.
tolygus([X|[Y|T]], D) :- X - Y =< D, 
                         X - Y >= -D, 
                         tolygus([Y|T], D).
