asmuo = symbol                   /*                   jonas                */
tevas(jonas,antanas).            /******************************************/
tevas(jonas,petras).  
tevas(antanas,juozas).
tevas(antanas,algis).            
tevas(antanas,stasys).
tevas(algis, dainius).
tevas(stasys,saulius).
tevas(petras,pranas).
tevas(petras,kazys).

sunus(Sunus,Tevas):-
     tevas(Tevas,Sunus).
broliai(A,B):-
    sunus(A,S), sunus(B,S).

protevis(P,A):- tevas(P,A).
protevis(P,A):- tevas(P,A1), protevis(A1,A).
