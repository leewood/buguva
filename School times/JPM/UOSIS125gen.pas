program uzdgen5;
var n, zodzioIlgis, zodziai, zodzioRaides: integer;
    failas: text;
    zodis: string;
begin
  Assign(failas, 'duom.txt');
  Rewrite(failas);
  {Susigeneruojame �od�i� skai�i�}
  n := Random(18) + 2;
  Writeln(failas, n);
  {Susigeneruojame �od�i� ilg�}
  zodzioIlgis := Random(98) + 2;
  for zodziai := 1 to n do
  begin
    zodis := '';
	{Generuojame  �od�io raides}
    for zodzioRaides := 1 to zodzioIlgis do
      zodis := zodis + Chr(Random(Ord('z') - Ord('a')) + Ord('a'));
    Writeln(failas, zodis);
  end;
  Close(failas);
end.
