program johny;
  var k, n, a, b, m: integer;
  l: array [1..50] of integer;
  begin
    WriteLn ('iveskite skaiciu nuo 1 iki 50 ir nuo 2 iki jusu ivesto skaicio');
    Read (n, k);
    m := 1;
    a := k;
    while m < 50 do
      begin
        l[m] := m;
        m := m + 1;    {cia skaiciuojame nesamone}
      end;
    while n > 1 do
      begin
        b := a;
        while b <= n do
          begin
            l[b] := l[b + 1];
            b := b + 1;
          end;
        n := n - 1;
        a := a + k - 1;
        if a > n then a := a - n;
      end;
    WriteLn(l[1]);
  end.