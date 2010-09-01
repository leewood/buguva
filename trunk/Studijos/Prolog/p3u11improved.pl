% Karolis Uosis
%  Programu sistemos, 4 kursas 3 grupe
%  MIF, 2009
%  3 praktinis darbas 4.9 uþduotis pagerinta versija


%  Versija, kai leidþiama skaidyti ir á tuðèius sàraðus
%
didejimo([H|[T|[]]]) :- H =< T.
didejimo([H|[H2|T]]) :- T \= [], H =< H2, didejimo([H2|T]).

lconcat([X|Y], Z, [X|W]) :- lconcat(Y, Z, W).
lconcat([], X, X).
lconcat(X, Y, Z, L) :- lconcat(X, Y, L2), lconcat(L2, Z, L).

split(X, [], X).
split(X, X, []).
split([X|T], [X|T1], L2) :- split(T, T1, L2).
split(X, L1, L2, L3) :- split(X, L1, L4), split(L4, L2, L3).

move(L1, L2, L3, L) :- lconcat(L2, L1, L3, L).
move(L1, L2, L3, L) :- lconcat(L1, L3, L2, L).

possible(X, 0) :- didejimo(X).
possible(X, Y) :-
	Y > 0,
	Y2 is Y - 1,
	split(X, L1, L2, L3),
	move(L1, L2, L3, L),
	possible(L, Y2),
	writeln(L).


% Versija kai maþiausias dalies dydis yra vienas elementas
%
split2([X|T], [X], T) :- T \= [].
split2([X|T], [X|T1], L2) :- split2(T, T1, L2).
split2(L, L1, L2, L3) :-
	split2(L, L1, L4),
	split2(L4, L2, L3),
	L1 \= [], L2 \= [], L3 \= [].
possible2(X, 0) :- didejimo(X).
possible2(X, Y) :-
	Y > 0,
	Y2 is Y - 1,
	split2(X, L1, L2, L3),
	move(L1, L2, L3, L),
	possible2(L, Y2),
	writeln(L).
















