program johny;
var p, n: longint;
begin
  Read(p);
  n := 1;
  while (p <> 1) and (n <= 300) do
  begin
    n := n + 1;
    if p mod 2 = 0 
      then p := p div 2 
      else p := 3 * p + 1;
  end;
  if n > 300 
    then WriteLn('seka nepasiekta') 
    else WriteLn(n);
end.
