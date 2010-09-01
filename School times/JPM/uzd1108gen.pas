program uzd1108gen;
  var n, {lysvi� skai�ius}
      m: integer; {kr�m� skai�ius lysv�je}
      generated: array [1..100, 1..100] of boolean; {rezultat� masyvas}
	  

  {Sugeneruoja reik�mes}
  procedure generate;
  var i, j, value: integer;
  begin
    n := random(99) + 1;
	m := random(99) + 1;
	for i := 1 to n do
	  for j := 1 to m do
	  begin
	    value := random(100);
		if (value > 50) 
		then generated[j, i] := true
		else generated[j, i] := false;
	  end;
  end;  
  
  {I�veda reik�mes � fail�}
  procedure outputToFile(fileName: string);
  var i, j: integer;
      outFile: Text;
  begin
    Assign(outFile, fileName);
	ReWrite(outFile);
	Writeln(n, ' ', m);
	for i := 1 to n do
	begin
	  for j := 1 to m do
	    if (generated[j, i]) 
		then Write('1')
		else Write('0');
	  Writeln(outFile);
	end;
	Close(ouFile);
  end;
  
  
  begin
    randomize;
	generate;
	outputToFile('duom.txt');
  end.