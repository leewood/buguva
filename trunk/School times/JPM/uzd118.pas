{Sprendimo id�ja:
   Vis� pirma surandamas bet kuris vienetas masyve. Toliau veikiama taip. Tas vienetas masyve kei�iamas � 0. Tada per�i�rimi visi langeliai kaimynai. 
   Visuose langeliuose, kuriuose yra vienetas jis kei�iamas 0 ir v�lgi tikrinami toki� langeli� kaimynai analogi�ku b�du. T� langeli�, kuriuose buvo 0 kaimynai netikrinami
   ir jokios operacijos su jais neatliekamos. Kai pasiekiama sutucija kai visi patikrinti langeliai turi 0 ir neb�ra n� vieno su 1, tai rei�kia, jog  ap�jome vien� kr�myn�, tad
   pasi�ymime, ka�kur, kad jau radome viena kr�myn�. ir visk� kartojame nuo prad�i�, t.y. randame bet kuri vienet� masyve ir t.t. Kai jau joki� vienet� neberasime, tai
   busime sirad� visus kr�mynus. Lengva pastebeti, kad kaimyn� tikrinimui labiausiai tinka rekursija.
   
   }
   
  

program uzd8;
var n, {Lysvi� skai�ius}
    m: integer;{Kr�m� lysv�je skai�ius}
    fileOut: Text;	
    map: array[1..100,1..100] of integer; {kr�m� i�sid�stymas pos lysves}
    result: integer; {kr�myn� skai�ius}

	
{Randa kok� nors vienet� masyve. Jei joks nerastas, tai x ir y reik�m�s gr��inamos nepakeistos}
procedure firstOneInArray(var x, y: integer);
var i, j: integer;
begin
  for i := 1 to n do
    for j := 1 to m do
    begin
      if map[i, j] = 1 then
      begin
        x := i;
        y := j;
      end;
    end;
end;

{Atlieka langelio koordinat�je (x, y) patikrinim�}
procedure go(x, y: integer);
begin
  {tikriname ar tokios koordinat�s galimos}
  if (x >= 1) and (y >= 1) and (x <= n) and (y <= m) then
    {tikriname, ar tame langelyje vienetas}
    if map[x, y] <> 0 
	then begin
	  {jei taip, tai pakei�iame � 0 ir tikriname kaimynus}
      map[x, y] := 0;
      go(x + 1, y);
      go(x, y - 1);
      go(x, y + 1);
      go(x - 1, y);
    end;
end;

{Nuskaito pradinius duomenis i� failo}
procedure readData(fileName: string);
var fileIn: Text;
    i, j: integer;
	currentSymbol: char;
begin
  Assign(fileIn, fileName);
  Reset(fileIn);
  ReadLn(fileIn, n, m);
  for i := 1 to n do
  begin
    for j := 1 to m do
    begin
      Read(fileIn, currentSymbol);
      if currentSymbol = '1' 
	  then lm[i, j] := 1 
	  else lm[i, j] := 0;
    end;
    Readln(fileIn);
  end;
  Close(fileIn);  
end;

{randa kr�myn� skai�i�}
function numberOf: integer;
var result: integer;
    x, y: integer;
	stop: boolean;
begin
  result := 0;
  stop := false;
  repeat
    x := 0;
    y := 0;
    {Ie�ko bet kokio vieneto masyve}	
    firstOneInArray(x, y);
	{Jei nerastas stojama}
    if (x = 0) and (y = 0) 
	then stop := true 
	else begin
	  {Kitu  atveju,  kr�mynas pa�alinamas i� masyvo ir padidinamas kr�myn� skai�ius}
      go(x, y);
      result := result + 1;
    end;
  until stop;  
  numberOf := result;
end;

begin
  readData('duom.txt');  
  result := numberOf;
  Assign(fileOut,'rez.txt');
  ReWrite(fileOut);
  Write(fileOut, result);
  Close(fileOut);
end.
