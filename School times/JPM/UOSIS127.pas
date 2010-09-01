failprogram test;

type TLangelis = record
        x, y: integer;
      end;
	  
var paimti: array[1..10] of TLangelis;
    k,
	paimtos,
	viso,
	i,
	j,
    n: integer;	
    panaudoti: array [1..10, 1..10] of boolean;
    duomin, duomout: text;

{Paþymi visus langelius, kuriuos kerta figûra padëta á langelá (x, y)}
procedure UzpildytiFigura(x, y: integer);
var i, j: integer;
    skirt, skirt2, tmpy: integer;
begin
  skirt := y - x;
  skirt2 := n + 1 - x - y;
  for i := 1 to n do
  begin
    panaudoti[i, y] := true;
    panaudoti[x, i] := true;
    tmpy := i + skirt;
    if (tmpy > 0) and (tmpy <= n) then panaudoti[i, tmpy] := true;
    tmpy := n - i + 1 - skirt2;
    if (tmpy > 0) and (tmpy <= n) then panaudoti[i, tmpy] := true;
  end;
  for i := x - 2 to x + 2 do
    for j := y - 2 to y + 2 do
	begin
	  if (i > 0) and (i <= n) and (j > 0) and (j <= n) then panaudoti[i, j] := true;
	end;
end;

{Paþymi visus langelius, kurie kertami paimtø figûrø}
procedure UzpildytiVisas;
var i: integer;
begin
  for i := 1 to paimtos do
    UzpildytiFigura(paimti[i].x, paimti[i].y);
end;

{Padeda nurodytà figûra}
procedure Prideti(x, y: integer);
var lang: TLangelis;
begin 
  lang.x := x;
  lang.y := y;
  paimtos := paimtos + 1;
  paimti[paimtos] := lang;
  UzpildytiVisas;
end;

{Paðalina paskutinæ padëtà figûrà nuo lentos}
procedure Isimti;
var i, j: integer;
begin
  for i := 1 to n do
    for j := 1 to n do
      panaudoti[i, j] := false;
  if paimtos > 0 then paimtos := paimtos - 1;
  UzpildytiVisas;
end;

{Deda figûra á langelá (x, y)}
procedure Deti (x, y, liko: integer);
var i, j: integer;
begin
  {patkrina ar figûra nebus kertama}
  if not panaudoti[x, y] 
  then begin
    Prideti(x,y);
	{Patikrina ar dar liko nepadëtø figûrø}	
    if liko = 1 
	then begin
      viso := viso + 1;
    end
    else begin
	  {Bando dëti sekanèià figûrà}
      for i := 1 to n do
        for j := 1 to n do
          Deti(i, j, liko - 1);
    end;
    Isimti;
  end;
end;

begin
  Assign(duomin, 'duom.txt');
  Reset(duomin);
  Read(duomin, n, k);
  Close(duomin);
  for i := 1 to n do
    for j := 1 to n do
      Deti(i, j, k);
  Assign(duomout, 'rez.txt');
  Rewrite(duomout);
  Write(duomout, viso div k);
  Close(duomout);
end.
