program uzd1107gen;
var n: integer;  {Element� skai�ius}
    generated: array [1..1000] of integer; {Jau sugeneruotos reik�m�s}
	generatedCount: integer; {Jau sugeneruot� reik�mi� skai�ius}
	
	
{Patikrina ar tokia reik�m� jau buvo sugeneruota}	
function isAlreadyGenerated(value: integer): boolean;
var i: intger;
    found: boolean;
begin
  i := 1;
  found := false;
  {Tikriname sugeneruot� reik�mi� masyv� kol pasiekiame gal� arba randame reik�m�}
  while (i <= generatedCount) and not(found) do
  begin
    if (generated[i] = value) then found := true;
	i := i + 1;
  end;
  isAlreadyGenerated := found;
end;

{Sugeneruoja nauj�, skirting� nei jau buvo sugeneruotos, reik�m�}
function newValue: integer;
var result: integer;
    correct: boolean;
begin
  correct := true;
  {Kartojame, kol sugeneruojame dar nebuvusi� reik�m�}
  repeat
    result := random(1000);
	correct := not(isAlreadyGenerated(result));
  until correct;
  generatedCount := generatedCount + 1;
  generated[generatedCount] := result;
  newValue := result;
end;

{Sugeneruoja visas reik�mes}
procedure generate;
var i: integer;
begin
  n := random(999) + 1;
  for i := 1 to n do
    newValue;
end;

{I�veda visk� � fail�}
procedure outputToFile(fileName: string);
var outFile: Text;
    i: integer;
begin
  Assign(outFile, fileName);
  ReWrite(outFile);
  WriteLn(n);
  for i := 1 to n - 1 do
    Write(generated[i], ' ');
   WriteLn(generated[n]);
  Close(outFile);  
end;


begin
  randomize;
  generate;
  outputToFile('duom.txt');
end;