% Karolis Uosis
%  Programu sistemos, 4 kursas 3 grupe
%  MIF, 2009
%  3 praktinis darbas 4.9 užduotis

didejimo([_|[]]).
didejimo([X|[Y|[]]]) :- Y >= X.
didejimo([X|[Y|T]]) :- T \= [], 
                       Y >= X, 
                       didejimo([Y|T]).

ilgis([], 0).
ilgis([_|[]], 1).
ilgis([_|T], R) :- T \= [], 
                   ilgis(T, R2), 
                   R is R2 + 1.

dalis(X, I, Z, Y) :- I = 0, Z = [], Y = X. 
dalis([], _, [], []).
dalis([X|Y], I, R, T) :- 
                         I > 0,
                         J is I - 1,
                         dalis(Y, J, R2, T),
                         R = [X|R2].
                         
divide(X, X, 1) :- X \= 0.
divide(X, Y, 0) :- Y \= 0, 
                   X < Y.
divide(X, Y, Z) :- Y \= 0, 
                   X > Y, 
                   X2 is X - Y,
                   divide(X2, Y, Z2), 
                   Z is Z2 + 1.
                   
module(X, Y, Z) :- divide(X, Y, R), 
                   Z is X - (Y * R).

dalies_ilgis(X, I) :- ilgis(X, I2),
                      module(I2, 3, 0), 
                      divide(I2, 3, I).
dalies_ilgis(X, I) :- ilgis(X, I2),
                      not(module(I2, 3, 0)), 
                      divide(I2, 3, I3),
                      I is I3 + 1.
                      
dalys(X, D1, D2, D3) :- dalies_ilgis(X, J),
                        dalis(X, J, D1, T), 
                        dalis(T, J, D2, D3).

sujungti([X|[]], Y, [X|Y]).
sujungti([X|Y], Z, R) :- Y \= [], 
                         sujungti(Y, Z, R2), 
                         R = [X|R2].

i_prieki(X, Y) :- dalys(X, D1, D2, D3), 
                  sujungti(D2, D1, DD), 
                  sujungti(DD, D3, Y).
i_gala(X, Y) :- dalys(X, D1, D2, D3), 
                sujungti(D1, D3, DD), 
                sujungti(DD, D2, Y).

ismaisyti(X, 0) :- didejimo(X).
ismaisyti(X, Y) :- Y > 0,
                   i_prieki(X, Z),    
                   J is Y - 1,               
                   ismaisyti(Z, J).
ismaisyti(X, Y) :- Y > 0,
                   i_gala(X, Z), 
                   J is Y - 1,
                   ismaisyti(Z, J).                   
