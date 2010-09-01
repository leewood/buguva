program uzd1107gen;
var n: integer;  {Elementø skaièius}
    generated: array [1..1000] of integer; {Jau sugeneruotos reikðmës}
	generatedCount: integer; {Jau sugeneruotø reikðmiø skaièius}
	
	
{Patikrina ar tokia reikðmë jau buvo sugeneruota}	
function isAlreadyGenerated(value: integer): boolean;
var i: intger;
    found: boolean;
begin
  i := 1;
  found := false;
  {Tikriname sugeneruotø reikðmiø masyvà kol pasiekiame galà arba randame reikðmæ}
  while (i <= generatedCount) and not(found) do
  begin
    if (generated[i] = value) then found := true;
	i := i + 1;
  end;
  isAlreadyGenerated := found;
end;

{Sugeneruoja naujà, skirtingà nei jau buvo sugeneruotos, reikðmæ}
function newValue: integer;
var result: integer;
    correct: boolean;
begin
  correct := true;
  {Kartojame, kol sugeneruojame dar nebuvusià reikðmæ}
  repeat
    result := random(1000);
	correct := not(isAlreadyGenerated(result));
  until correct;
  generatedCount := generatedCount + 1;
  generated[generatedCount] := result;
  newValue := result;
end;

{Sugeneruoja visas reikðmes}
procedure generate;
var i: integer;
begin
  n := random(999) + 1;
  for i := 1 to n do
    newValue;
end;

{Iðveda viskà á failà}
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