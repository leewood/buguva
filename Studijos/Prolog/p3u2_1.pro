% Karolis Uosis
%  Programu sistemos, 4 kursas 3 grupe
%  MIF, 2009
%  3 praktinis darbas 2.1 užduotis


takeout(X,[X|R],R).
takeout(X,[F|R],[F|S]) :- takeout(X,R,S).

has([Y|_], Y).
has([_,T], Y) :- has(T, Y).
less([X|T], Y) :- X >= Y, T = [].
less([X|T], Y) :- X >= Y, T \= [], less(T, Y).
min(S, M) :- has(S, M), less(S, M).