program uzdgen6;
var a, b, c, d, e, r: integer;
    failas: text;
begin
  Assign(failas, 'duom.txt');
  ReWrite(failas);
  {Sugeneruojami visus skai�ius}
  a := Random(2999) + 1;
  b := Random(2999) + 1;
  c := Random(2999) + 1;
  d := Random(2999) + 1;
  e := Random(2999) + 1;
  r := Random(2999) + 1;
  {I�vedame � fail�}
  Write(failas, a, ' ', b, ' ', c, ' ', d, ' ', e, ' ', r);
  Close(failas);
end.
