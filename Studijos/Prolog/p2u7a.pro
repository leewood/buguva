% Karolis Uosis
%  Programu sistemos, 4 kursas 3 grupe
%  MIF, 2009
%  2 praktinis darbas 7a užduotis

s(nul).

sum(nul, X, X).
sum(s(X), Y, s(Z)) :- sum(X, Y, Z).

dv_laipsnis_t(s(nul), nul).
dv_laipsnis_t(N, s(X)) :- N \= nul, 
                          sum(M, M, N), 
                          dv_laipsnis_t(M, X).
dv_laipsnis(X) :- dv_laipsnis_t(X, _).

