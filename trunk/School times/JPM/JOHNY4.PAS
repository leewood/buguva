program johny;
var t, n: text;
    g: integer;
    s: string;
begin
  Assign(t, 'file2.txt');
  ReWrite(t);
  Assign(n, 'file.txt');
  Reset(n);
  g := 0;
  while g <= 6 do
  begin
    g := g + 1;
    ReadLn(n, s);
    Delete(s, g, 1);
    WriteLn(t, s);
  end;
  Close(t);
  Close(n);
end.