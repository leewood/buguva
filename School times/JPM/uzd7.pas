program uzd7;
var n, min:integer;
    data: array[1..1000] of integer;


{Nustato  nurodyto sujungimo bendr� ilg�
    Sujungimai koduojami taip:
     Jei tarp paskutin�s ir pri��paskutin�s girliandos yra sujungimas, tai paskutinis bitas duoto skai�iaus x yra 1. Kiekvienas tolimesnis sujungimas atitinka auk�tesn� bit�
}
function length(x: integer): integer;
var len, total: integer;
begin
  total := n - 1;
  len := 0;
  {Begame er visus bitus}
  while total > 0 do
  begin
    {Jei yra sujungimas}
    if x mod 2 = 1 then
	  {Papildome ilg�}
      len := len + (data[total + 1] - data[total]);
    total := total - 1;
    x := x div 2;
  end;
  length := len;
end;

{Randa dvejeto laipsn�}
function degreeOfTwo(x: integer): integer;
begin
  if x > 0 
  then degreeOfTwo := 2 * degreeOfTwo(x - 1)
  else degreeOfTwo := 1;
end;

{Nustato  ar visi �viestumai suporuoti}
function isInPair(x: integer): boolean;
var lastBit, currentBit, total:integer;
    stop: boolean;
begin
  {Jei paskutinio sujungimo n�ra, tai tikrai tokia  kombinacija netinka}
  if x mod 2 = 0 
  then isInPair := false
  else begin
    total := 1;
    lastBit := 1;
    stop := false;
    x := x div 2;
	{Kitu  atveju �i�rime visus sujungimus}
    while (not stop) and (total < n - 1) do
    begin
     currentBit := x mod 2 ;
     x := x div 2;
	 {Jei dabar sujungimo n�ra ir vienu  anks�iau  jo irgi nebuvo, tai vadinasi lieka nesuporuot� viet�}
     if (lastBit = 0) and (currentBit = 0) then stop := true;
     lastBit := currentBit;
     total := total + 1;
   end;
   if (lastBit = 1) 
   then isInPair := true 
   else isInPair := false;
  end;
end;

{Atlieka visi galim� sujungim� per�i�r�, pradedant duotu}
procedure tryPossibility(x: integer);
var len: integer;
begin
  {Jei visos yra poromis}
  if isInPair(x) 
  then begin
     {Skai�iuojame ilg�}
    len := length(x);
	{Jei jis ma�esnis - i�sisaugome}
    if len < min then min := len;
  end;
  {Jei dar nei�m�ginome vis� galimybi�, m�giname kit� galimyb�}
  if x > 0 then
   tryPossibility(x - 1);
end;

{Surikiuoja duomenis didejimo tvarka}
procedure sort;
var min: integer;
    currentIndex, i, j: integer;
begin
  for i := 1 to n do
  begin
    min := data[i];
    currentIndex := i;
    for j := n downto i do
      if data[j] < min 
	  then begin
        min := data[j];
        currentIndex := j;
      end;
    lm[currentIndex] := lm[i];
    lm[i] := min;
  end;
end;

{Nuskaito pradinius duomenis}
procedure readData(fileName: string)
var fileIn: Text;
    i: integer;
begin
  Assign(fileIn, fileName);
  Reset(fileIn);
  ReadLn(fileIn, n);  
  for i := 1 to n do
  begin
    read(fileIn, data[i]);
  end;
  Close(fileIn);
end;

{I�veda rezultatus}
procedure writeData(fileName: string)
var fileOut: Text;
begin
  Assign(fileOut, fileName);
  Rewrite(fileOut);
  Write(fileOut, min);
  Close(fileOut);  
end;

begin
  readData('duom.txt');
  sort;
  {Pirma sujungsime visas girliandas}
  x:= degreeOfTwo(n - 1) - 1;
  min := maxint - 1 ;
  tryPossibility(x);  
  writeData('rez.txt');
end.
