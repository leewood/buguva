program uzdgen7;
var n, k: integer;
    failas: text;
begin
  Assign(failas, 'duom.txt');
  Rewrite(failas);
  n := Random(9) + 1;
  k := Random(k) + 1;
  Write(failas, n, ' ', k);
  Close(failas);
end.
