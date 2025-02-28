% Karolis Uosis
%  Programu sistemos, 4 kursas 3 grupe
%  MIF, 2009
%  3 praktinis darbas 4.7 užduotis


yra_keitinys(X, [k(X, Y)| _], Y).
yra_keitinys(X, [_|T], Y) :- yra_keitinys(X, T, Y).
keisti([X|[]], K, R) :- yra_keitinys(X, K, N), R = [N|[]].
keisti([X|[]], K, R) :- not(yra_keitinys(X, K, N)), R = [X|[]].
keisti([X|Y], K, R) :- yra_keitinys(X, K, N), 
                       keisti(Y, K, R2), 
                       R = [N|R2].
keisti([X|Y], K, R) :- not(yra_keitinys(X, K, N)),
                       keisti(Y, K, R2), 
                       R = [X|R2].                       