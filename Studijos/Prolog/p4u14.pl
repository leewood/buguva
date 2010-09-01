% Karolis Uosis
%  Programu sistemos, 4 kursas 3 grupe
%  MIF, 2009
%  4 praktinis darbas 14 uþduotis

:- dynamic board/2.
:- dynamic size/2.
:- dynamic generated_board/2.
board(1, 1).
set_size(X, Y) :-
	not(size(_, _)),
	clear_board,
	assert(size(X, Y)),
	init_board(1, 1).
set_size(X, Y) :-
	size(X2, Y2),
	clear_board,
	retract(size(X2, Y2)),
	assert(size(X, Y)),
	init_board(1, 1).
clear_board :-
	not(board(_, _)).
clear_board :-
	board(X, Y),
	retract(board(X, Y)),
	clear_board.
init_board(X, Y) :- not(follows_bounds(X, Y)).
init_board(X, Y) :-
	board(X, Y),
	next_init(X, Y, X2, Y2),
	init_board(X2, Y2).
init_board(X, Y) :-
	follows_bounds(X, Y),
	not(board(X, Y)),
	not(next_init(X, Y, _, _)),
	assert(board(X, Y)).
init_board(X, Y) :-
	follows_bounds(X, Y),
	not(board(X, Y)),
	next_init(X, Y, X2, Y2),
	assert(board(X, Y)),
	init_board(X2, Y2).
follows_bounds(X, Y) :- size(X2, Y2), X > 0, Y > 0, X =< X2, Y =< Y2.
has(X, Y, [knight(X, Y)|_]).
has(X, Y, [H|T]) :- H \= knight(X, Y), has(X, Y, T).
controlled(X, Y, L) :- has(X, Y, L).
controlled(X, Y, L) :- X2 is X + 1, Y2 is Y - 2, has(X2, Y2, L).
controlled(X, Y, L) :- X2 is X + 2, Y2 is Y - 1, has(X2, Y2, L).
controlled(X, Y, L) :- X2 is X + 2, Y2 is Y + 1, has(X2, Y2, L).
controlled(X, Y, L) :- X2 is X + 1, Y2 is Y + 2, has(X2, Y2, L).
controlled(X, Y, L) :- X2 is X - 1, Y2 is Y - 2, has(X2, Y2, L).
controlled(X, Y, L) :- X2 is X - 1, Y2 is Y + 2, has(X2, Y2, L).
controlled(X, Y, L) :- X2 is X - 2, Y2 is Y - 1, has(X2, Y2, L).
controlled(X, Y, L) :- X2 is X - 2, Y2 is Y + 1, has(X2, Y2, L).
can_put(L, X, Y) :-
	follows_bounds(X, Y),
	not(controlled(X, Y, L)).
try_put(Start, Result, X, Y) :-
	can_put(Start, X, Y),
	Result = [knight(X, Y)|Start].
try_put(Start, Result, X, Y) :-
	not(can_put(Start, X, Y)),
	next(X, Y, X2, Y2),
        try_put(Start, R2, X2, Y2),
	Result = R2.
next_init(X, Y, X2, Y2) :-
	X2 is X + 1,
	Y2 is Y,
	follows_bounds(X2, Y2).
next_init(X, Y, X2, Y2) :-
	X3 is X + 1,
	Y3 is Y,
	not(follows_bounds(X3, Y3)),
	X2 is 1,
	Y2 is Y + 1,
	follows_bounds(X2, Y2).
next(X, Y, X2, Y2) :-
	board(X2, Y2),
	X2 > X,
	Y2 = Y.
next(_, Y, X2, Y2) :-
	board(X2, Y2),
	Y2 > Y.
put(Start, End) :- try_put(Start, End, 1, 1).
put_count(X, X, 0).
put_count(Start, Result, X) :-
	X > 0,
	X2 is X - 1,
        put(Start, R2),
	put_count(R2, Result, X2).
minimum(C) :-
	size(W, H),
	min(C, W, H).
min(C, W, H) :-
       	Diff is (C * 9) - (W * H),
	Diff >= 0.
max(C, W, H) :- C =< W * H.
all_controlled(X, Y, _) :-
	not(follows_bounds(X, Y)).
all_controlled(X, Y, L) :-
	controlled(X, Y, L),
	not(next_init(X, Y, _, _)).
all_controlled(X, Y, L) :-
	controlled(X, Y, L),
	next_init(X, Y, X2, Y2),
	all_controlled(X2, Y2, L).
div(X, X, 1).
div(X, Y, 0) :- X < Y.
div(X, Y, Z) :-
	X >= Y,
	X2 is X - Y,
	div(X2, Y, Z2),
	Z is Z2 + 1.
mod(X, Y, Z) :-
	div(X, Y, Z2),
	Z is X - (Y * Z2).
print_board_cell(L, X, Y) :-
	has(X, Y, L),
	write('K|').
print_board_cell(L, X, Y) :-
	not(has(X, Y, L)),
	print_color(X, Y).
print_color(X, Y) :-
	mod(X, 2, Z1),
	mod(Y, 2, Z2),
	Z3 is Z1 + Z2,
	mod(Z3, 2, Z4),
	Z4 = 0,
	write('*|').
print_color(X, Y) :-
	mod(X, 2, Z1),
	mod(Y, 2, Z2),
	Z3 is Z1 + Z2,
	mod(Z3, 2, Z4),
	Z4 = 1,
	write(' |').
print_line(L, Y, W) :-
	print_line_separ(1, W),
	write('|'),
	print_line_item(L, 1, Y, W).
print_line_item(L, W, Y, W) :-
	print_board_cell(L, W, Y),
        writeln('').
print_line_item(L, X, Y, W) :-
	X < W,
	print_board_cell(L, X, Y),
	X2 is X + 1,
	print_line_item(L, X2, Y, W).
print_line_separ(W, W) :- writeln('+-+').
print_line_separ(X, W) :-
	X < W,
	write('+-'),
	X2 is X + 1,
	print_line_separ(X2, W).
print_row(L, H, W, H) :-
	print_line(L, H, W),
	print_line_separ(1, W).
print_row(L, Y, W, H) :-
	Y < H,
	print_line(L, Y, W),
	Y2 is Y + 1,
	print_row(L, Y2, W, H).
print_board(L) :-
	size(W, H),
	print_row(L, 1, W, H).
solution(W, H, C) :-
	min(C, W, H),
	set_size(W, H),
	put_count([], L, C),
	all_controlled(1, 1, L),
	print_board(L).

start_board(R, X, Y) :-
	next_init(X, Y, X2, Y2),
	start_board(R2, X2, Y2),
	R = [b(X, Y, 0)|R2].
start_board(R, X, Y) :-
	not(next_init(X, Y, _, _)),
	R = [b(X, Y, 0)].
next_board([empty], R) :- start_board(R, 1, 1).
next_board([], []).
next_board([b(X, Y, 0)|T], [b(X, Y, 1)|T]).
next_board([b(X, Y, 1)|T], [b(X, Y, 0)|T2]) :- T \= [], next_board(T, T2).
pieces([], []).
pieces([b(X, Y, 1)|T], [knight(X, Y)|T2]) :- pieces(T, T2).
pieces([b(_, _, 0)|T], T2) :- pieces(T, T2).
knight_count([], 0).
knight_count([_|T], K) :- knight_count(T, K2), K is K2 + 1.

ok(X, Y, L) :-
   X21 is X + 1, Y21 is Y - 2, not(has(X21, Y21, L)),
   X22 is X + 2, Y22 is Y - 1, not(has(X22, Y22, L)),
   X23 is X + 2, Y23 is Y + 1, not(has(X23, Y23, L)),
   X24 is X + 1, Y24 is Y + 2, not(has(X24, Y24, L)),
   X25 is X - 1, Y25 is Y - 2, not(has(X25, Y25, L)),
   X26 is X - 1, Y26 is Y + 2, not(has(X26, Y26, L)),
   X27 is X - 2, Y27 is Y - 1, not(has(X27, Y27, L)),
   X28 is X - 2, Y28 is Y + 1, not(has(X28, Y28, L)).
all_ok(L, [knight(X, Y)|[]]) :- ok(X, Y, L).
all_ok(L, [knight(X, Y)|T]) :- T \= [], ok(X, Y, L), all_ok(L, T).
all_ok(L) :- all_ok(L, L).
check(B, K) :-
	pieces(B, L),
	knight_count(L, K),
	all_ok(L),
	all_controlled(1, 1, L),
	print_board(L).
solution2(W, H, C) :-
	min(C, W, H),
	max(C, W, H),
	set_size(W, H),
	next_board([empty], B),
	find_solution(B, C).
find_solution(B, C) :-
	check(B, C).
find_solution(B, C) :-
	not(check(B, C)),
	next_board(B, B2),
	find_solution(B2, C).

get_elem([_|T], I, E) :- I > 0, I2 is I - 1, get_elem(T, I2, E).
get_elem([H|_], 0, H).















