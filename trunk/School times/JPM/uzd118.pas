{Sprendimo idëja:
   Visø pirma surandamas bet kuris vienetas masyve. Toliau veikiama taip. Tas vienetas masyve keièiamas á 0. Tada perþiûrimi visi langeliai kaimynai. 
   Visuose langeliuose, kuriuose yra vienetas jis keièiamas 0 ir vëlgi tikrinami tokiø langeliø kaimynai analogiðku bûdu. Tø langeliø, kuriuose buvo 0 kaimynai netikrinami
   ir jokios operacijos su jais neatliekamos. Kai pasiekiama sutucija kai visi patikrinti langeliai turi 0 ir nebëra në vieno su 1, tai reiðkia, jog  apëjome vienà krûmynà, tad
   pasiþymime, kaþkur, kad jau radome viena krûmynà. ir viskà kartojame nuo pradþiø, t.y. randame bet kuri vienetà masyve ir t.t. Kai jau jokiø vienetø neberasime, tai
   busime siradæ visus krûmynus. Lengva pastebeti, kad kaimynø tikrinimui labiausiai tinka rekursija.
   
   }
   
  

program uzd8;
var n, {Lysviø skaièius}
    m: integer;{Krûmø lysvëje skaièius}
    fileOut: Text;	
    map: array[1..100,1..100] of integer; {krûmø iðsidëstymas pos lysves}
    result: integer; {krûmynø skaièius}

	
{Randa koká nors vienetà masyve. Jei joks nerastas, tai x ir y reikðmës gràþinamos nepakeistos}
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

{Atlieka langelio koordinatëje (x, y) patikrinimà}
procedure go(x, y: integer);
begin
  {tikriname ar tokios koordinatës galimos}
  if (x >= 1) and (y >= 1) and (x <= n) and (y <= m) then
    {tikriname, ar tame langelyje vienetas}
    if map[x, y] <> 0 
	then begin
	  {jei taip, tai pakeièiame á 0 ir tikriname kaimynus}
      map[x, y] := 0;
      go(x + 1, y);
      go(x, y - 1);
      go(x, y + 1);
      go(x - 1, y);
    end;
end;

{Nuskaito pradinius duomenis ið failo}
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

{randa krûmynø skaièiø}
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
    {Ieðko bet kokio vieneto masyve}	
    firstOneInArray(x, y);
	{Jei nerastas stojama}
    if (x = 0) and (y = 0) 
	then stop := true 
	else begin
	  {Kitu  atveju,  krûmynas paðalinamas ið masyvo ir padidinamas krûmynø skaièius}
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
