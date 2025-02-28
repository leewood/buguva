% Karolis Uosis
%  Programu sistemos, 4 kursas 3 grupe
%  MIF, 2009
%  3 praktinis darbas 3.11 užduotis

skirtumas([], _, []).
skirtumas([X|T], [], [X|T]).
skirtumas([X|[]], [Y|[]], R) :- X \= Y, R = [X|[]].
skirtumas([X|[]], [X|[]], []).
skirtumas([X|[]], [Y|T], R) :- T \= [], X = Y, R = [].
skirtumas([X|[]], [Y|T], R) :- T \= [], X \= Y, skirtumas([X|[]], T, R2), R = R2.
skirtumas([X|Y], [M|N], R) :- Y \= [], 
                              skirtumas([X|[]], [M|N], R2), 
                              R2 = [], 
                              skirtumas(Y, [M|N], R3), 
                              R = R3.
skirtumas([X|Y], [M|N], R) :- Y \= [], 
                              skirtumas([X|[]], [M|N], R2), 
                              R2 \= [], R2 = [A|[]],
                              skirtumas(Y, [M|N], R3), 
                              R = [A|R3].