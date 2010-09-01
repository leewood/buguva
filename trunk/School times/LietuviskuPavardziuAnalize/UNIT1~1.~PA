unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Classes, Graphics, Controls, Forms, Dialogs,
  StdCtrls, Grids, ComCtrls, ExtCtrls;

type
  TForm1 = class(TForm)
    StringGrid1: TStringGrid;
    Button5: TButton;
    Button6: TButton;
    RadioGroup1: TRadioGroup;
    RadioGroup2: TRadioGroup;
    Button1: TButton;
    Label1: TLabel;
    Label2: TLabel;
    Button2: TButton;
    Label3: TLabel;
    Button3: TButton;
    OpenDialog1: TOpenDialog;
    SaveDialog1: TSaveDialog;
    procedure Button2Click(Sender: TObject);
    procedure Button5Click(Sender: TObject);
    procedure Button6Click(Sender: TObject);
    procedure FormCreate(Sender: TObject);
    procedure Button3Click(Sender: TObject);
    procedure Button1Click(Sender: TObject);
  private
    { Private declarations }
  public
    cur: integer;
    { Public declarations }
  end;
type tskir = array[0..3] of string;
var
  Form1: TForm1;
  tipas, budas: integer;
  galun: array[1..20] of string;
  priesag: array[1..200] of string;
  galkiek, prieskiek, bpr, bgl: integer;
implementation

uses Unit2, Unit3;

{$R *.DFM}

procedure TForm1.Button2Click(Sender: TObject);
begin
form2.show;
end;

function rasti(c: char): integer;
const s = 'Aa¿‡BbCc»ËDdEe∆ÊÀÎFfGgHhIi¡·YyJjKkLlMmNnOoPpRrSs–TtUuÿ¯€˚VvZzﬁ˛ 1234567890';
var x: integer;
begin
x := 1;
while (x <= length(s))and(s[x]<>c) do
x := x + 1;
rasti := x;
end;

function daugiau(a, b: char): boolean;
begin
case tipas of
1:if rasti(a) < rasti(b) then daugiau := true else daugiau := false;
2:if rasti(a) > rasti(b) then daugiau := true else daugiau := false;
end;
end;

function lygu(a, b: char): boolean;
begin
if rasti(a) = rasti(b) then lygu := true else lygu := false;
end;

function apsukti(s: string):string;
var ats: string;
    x: integer;
begin
ats := '';
for x := length(s) downto 1 do
ats := ats + s[x];
apsukti := ats;
end;

function versti(s: tskir): string;
var x1, x2, x3, code: integer;
    ats: string;
begin
if form1.cur <> 0 then begin
x1 := form1.cur div 100;
x2 := form1.cur div 10 mod 10;
x3 := form1.cur mod 10;
ats := s[x1] + ' ' + s[x2] + ' ' + s[x3];
end
else ats := s[1] + s[2] + s[3];
versti := ats;
end;

function didesnis_gl(sa, sa2: tskir):byte;
var ats: byte;
    x, n: integer;
    run: boolean;
    c, c2: char;
    s, s2: string;
begin
ats := 0;
run := true;
sa[1] := apsukti(sa[1]);
sa[2] := apsukti(sa[2]);
sa[3] := apsukti(sa[3]);
sa2[1] := apsukti(sa2[1]);
sa2[2] := apsukti(sa2[2]);
sa2[3] := apsukti(sa2[3]);
s := versti(sa);
s2 := versti(sa2);
if length(s) > length(s2) then n := length(s) else n := length(s2);
x := 1;
while (x <= n)and(run) do
begin
if x <= length(s) then c := s[x] else c := ' ';
if x <= length(s2) then c2 := s2[x] else c2 := ' ';
if not(LYGU(c, c2)) then begin
run := false;
if daugiau(c, c2) then ats := 1 else ats := 2;
end;
x := x + 1;
end;
didesnis_gl := ats;
end;


function didesnis_ilg(sa, sa2: tskir):byte;
var ats: byte;
    x, n: integer;
    run: boolean;
    c, c2: char;
    s, s2: string;
begin
ats := 0;
if form1.cur = 0 then begin
s := versti(sa);
s2 := versti(sa2);
end else begin
s := sa[form1.cur div 100];
s2 := sa2[form1.cur div 100];
end;
run := true;
if length(s) > length(s2) then n := length(s) else n := length(s2);
x := n;
while (x >= 1)and(run) do
begin
if x <= length(s) then c := s[x] else c := ' ';
if x <= length(s2) then c2 := s2[x] else c2 := ' ';
if not(LYGU(c, c2)) then begin
run := false;
if daugiau(c, c2) then ats := 1 else ats := 2;
end;
x := x - 1;
end;
didesnis_ilg := ats;
end;

function conv(s: string): string;
begin
while length(s) < bpr do
s := s + ' ';
conv := s;
end;

function rastipries(s: string): string;
var ats, gal: string;
    n, kiek, x, rastas: integer;
    ar: boolean;
begin
n := length(s);
kiek := bgl;
x := 1;
ar := true;
gal := copy(s, n - kiek + 1, kiek);
while (ar)and(kiek >= 1) do
begin
x := 1;
while (x <= galkiek)and(ar) do
begin
if gal = galun[x] then begin ar := false; rastas := x; end;
x := x + 1;
end;
delete(gal, 1, 1);
kiek := kiek - 1;
end;
if not(ar) then
delete(s, n - length(galun[rastas]) + 1, length(galun[rastas]));
n := length(s);
kiek := bpr;
x := 1;
ar := true;
gal := copy(s, n - kiek + 1, kiek);
while (ar)and(kiek >= 1) do
begin
x := 1;
while (x <= prieskiek)and(ar) do
begin
if gal = priesag[x] then begin ar := false; rastas := x; end;
x := x + 1;
end;
delete(gal, 1, 1);
kiek := kiek - 1;
end;
if not(ar) then
ats := priesag[rastas]
else ats := '';
ats := conv(ats);
rastipries := ats;
end;

function didesnis_pr(sa, sa2: tskir):byte;
var ats: byte;
    x, n: integer;
    run: boolean;
    c, c2: char;
    s, s2: string;
begin
ats := 0;
s := versti(sa);
s2 := versti(sa2);
run := true;
if length(s) > length(s2) then n := length(s) else n := length(s2);
x := 1;
while (x <= n)and(run) do
begin
if x <= length(s) then c := s[x] else c := ' ';
if x <= length(s2) then c2 := s2[x] else c2 := ' ';
if not(LYGU(c, c2)) then begin
run := false;
if daugiau(c, c2) then ats := 1 else ats := 2;
end;
x := x + 1;
end;
didesnis_pr := ats;
end;

function didesnis_pries(sa, sa2: tskir):byte;
var ats: byte;
    x, n: integer;
    run: boolean;
    c, c2: char;
    s, s2: string;
    ls, ls2: string;
begin
ats := 0;
if form1.cur <> 0 then begin
ls := rastipries(sa[form1.cur div 100]);
ls2 := rastipries(sa2[form1.cur div 100]);
sa[form1.cur div 100] := ls + sa[form1.cur div 100];
sa2[form1.cur div 100] := ls2 +
sa2[form1.cur div 100];
end;
s := versti(sa);
s2 := versti(sa2);
run := true;
if length(s) > length(s2) then n := length(s) else n := length(s2);
x := 1;
while (x <= n)and(run) do
begin
if x <= length(s) then c := s[x] else c := ' ';
if x <= length(s2) then c2 := s2[x] else c2 := ' ';
if not(LYGU(c, c2)) then begin
run := false;
if daugiau(c, c2) then ats := 1 else ats := 2;
end;
x := x + 1;
end;
didesnis_pries := ats;
end;


function didesnis(s, s2: tskir):byte;
begin
case budas of
1:didesnis := didesnis_pr(s, s2);
2:didesnis := didesnis_gl(s, s2);
3:didesnis := didesnis_ilg(s, s2);
4:didesnis := didesnis_pries(s, s2);
end;
end;

procedure skirstyti(s: string; var s2: tskir);
var x, y: integer;
begin
s2[1] := '';
s2[2] := '';
s2[3] := '';
y := 1;
for x := 1 to length(s) do
if s[x] <> ' ' then s2[y] := s2[y] + s[x]
               else y := y + 1;
end;

procedure TForm1.Button5Click(Sender: TObject);
var t:textfile;
    s: string;
    s2: tskir;
begin
if form1.OpenDialog1.Execute then begin
assignfile(t, form1.OpenDialog1.FileName);
reset(t);
form1.StringGrid1.Rowcount := 0;
while not(eof(t)) do
begin
readln(t, s);
skirstyti(s, s2);
form1.StringGrid1.Rowcount := form1.StringGrid1.Rowcount + 1;
form1.StringGrid1.Cells[0, form1.StringGrid1.Rowcount - 1] := s2[1];
form1.StringGrid1.Cells[1, form1.StringGrid1.Rowcount - 1] := s2[2];
form1.StringGrid1.Cells[2, form1.StringGrid1.Rowcount - 1] := s2[3];
end;
closefile(t);
form1.StringGrid1.FixedRows := 1;
end;
end;

procedure TForm1.Button6Click(Sender: TObject);
var x, y, what: integer;
    s, s2: tskir;
begin
budas := form1.RadioGroup1.ItemIndex + 1;
tipas := form1.RadioGroup2.ItemIndex + 1;
for x := 1 to form1.stringgrid1.rowcount - 2 do
for y := 1 to form1.stringgrid1.rowcount - x - 1 do
begin
s[1] := form1.StringGrid1.Cells[0, y];
s[2] := form1.StringGrid1.Cells[1, y];
s[3] := form1.StringGrid1.Cells[2, y];
s2[1] := form1.StringGrid1.Cells[0, y + 1];
s2[2] := form1.StringGrid1.Cells[1, y + 1];
s2[3] := form1.StringGrid1.Cells[2, y + 1];
s[0] := ''; s2[0] := '';
what := didesnis(s, s2);
if what = 1 then begin
form1.StringGrid1.Cells[0, y] := form1.StringGrid1.Cells[0, y + 1];
form1.StringGrid1.Cells[1, y] := form1.StringGrid1.Cells[1, y + 1];
form1.StringGrid1.Cells[2, y] := form1.StringGrid1.Cells[2, y + 1];
form1.StringGrid1.Cells[0, y + 1] := s[1];
form1.StringGrid1.Cells[1, y + 1] := s[2];
form1.StringGrid1.Cells[2, y + 1] := s[3];
end;
end;
end;

procedure TForm1.FormCreate(Sender: TObject);
var t: textfile;
    n: integer;
begin
form1.StringGrid1.Cells[0,0] := 'Vardas';
form1.StringGrid1.Cells[1,0] := 'PavardÎ';
form1.StringGrid1.Cells[2,0] := 'KlasÎ';
form1.cur := 0;
assignfile(t, 'galas.txt');
reset(t);
n := 0;
bgl := 0;
while not(eof(t)) do
begin
readln(t, galun[n + 1]);
n := n + 1;
if length(galun[n]) > bgl then bgl := length(galun[n]);
end;
closefile(t);
galkiek := n;
assignfile(t, 'priesaga.txt');
reset(t);
n := 0;
bpr := 0;
while not(eof(t)) do
begin
readln(t, priesag[n + 1]);
n := n + 1;
if length(priesag[n]) > bpr then bpr := length(priesag[n]);
end;
closefile(t);
prieskiek := n;
end;

procedure TForm1.Button3Click(Sender: TObject);
begin
form3.show;
end;

procedure TForm1.Button1Click(Sender: TObject);
var x: integer;
    t: textfile;
begin
if form1.SaveDialog1.Execute then begin
assignfile(t, form1.savedialog1.filename);
rewrite(t);
for x := 1 to form1.StringGrid1.RowCount - 1 do
writeln(t, form1.StringGrid1.cells[0, x], ' ',
           form1.StringGrid1.cells[1, x], ' ',
           form1.StringGrid1.cells[2, x]);
closefile(t);
end;
end;

end.
