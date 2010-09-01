program johny;
uses Dos;
var a, b, c, d, e, f, z, v: word;
    i: array [1..12] of integer;
    q, w, l, k, j, h, x: integer;
begin
  i[1]  := 31;
  i[2]  := 29;
  i[3]  := 31;
  i[4]  := 30;
  i[5]  := 31;
  i[6]  := 30;
  i[7]  := 31;
  i[8]  := 31;
  i[9]  := 30;
  i[10] := 31;
  i[11] := 30;
  i[12] := 31;
  GetDate (a, b, c, d);
  GetTime (e, f, v, z);
  x := 1;
  l := 366;
  while x < b do
  begin
    x := x + 1;
    l := l - i[x];
  end;
  l := l - c;
  k := 24 - e;
  j := 60 - f;
  h := 60 - v - 1;
  WriteLn(l, ' dien ', k, ' val ', j, ' min ', h, ' sec');
end.