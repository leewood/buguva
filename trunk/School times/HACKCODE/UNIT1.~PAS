unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Classes, Graphics, Controls, Forms, Dialogs,
  ExtCtrls, StdCtrls, Buttons;

type
  Tkeymap = record
             outk: array[1..256, 1..10] of widestring;
             ink: array[1..256] of widestring;
             sizein: integer;
             sizeout: array[1..256] of byte;
             maxin, maxout: integer;
            end;
  TForm1 = class(TForm)
    skin_body: TImage;
    Image1: TImage;
    Image2: TImage;
    Image3: TImage;
    Memo1: TMemo;
    Memo2: TMemo;
    moving: TImage;
    Image4: TImage;
    Image5: TImage;
    Image6: TImage;
    Image7: TImage;
    Image8: TImage;
    Image9: TImage;
    Image10: TImage;
    Image11: TImage;
    Image12: TImage;
    Image13: TImage;
    OpenDialog1: TOpenDialog;
    SaveDialog1: TSaveDialog;
    Image14: TImage;
    Label1: TLabel;
    Image15: TImage;
    Image16: TImage;
    Image17: TImage;
    Image18: TImage;
  //  procedure createparams(var params: tcreateparams); override;
    procedure Image1Click(Sender: TObject);
    procedure FormCreate(Sender: TObject);
    procedure Image1MouseDown(Sender: TObject; Button: TMouseButton;
      Shift: TShiftState; X, Y: Integer);
    procedure Image1MouseUp(Sender: TObject; Button: TMouseButton;
      Shift: TShiftState; X, Y: Integer);
    procedure Image2MouseDown(Sender: TObject; Button: TMouseButton;
      Shift: TShiftState; X, Y: Integer);
    procedure Image2MouseUp(Sender: TObject; Button: TMouseButton;
      Shift: TShiftState; X, Y: Integer);
    procedure Image3MouseDown(Sender: TObject; Button: TMouseButton;
      Shift: TShiftState; X, Y: Integer);
    procedure Image3MouseUp(Sender: TObject; Button: TMouseButton;
      Shift: TShiftState; X, Y: Integer);
    procedure Image3Click(Sender: TObject);
    procedure movingMouseDown(Sender: TObject; Button: TMouseButton;
      Shift: TShiftState; X, Y: Integer);
    procedure Image4Click(Sender: TObject);
    procedure BitBtn1Click(Sender: TObject);
    procedure SpeedButton1Click(Sender: TObject);
    procedure Image5Click(Sender: TObject);
    procedure Image6Click(Sender: TObject);
    procedure Image7Click(Sender: TObject);
    procedure Image10Click(Sender: TObject);
    procedure Image8Click(Sender: TObject);
    procedure Image11Click(Sender: TObject);
    procedure Image9Click(Sender: TObject);
    procedure Image12Click(Sender: TObject);
    procedure Image14Click(Sender: TObject);
    procedure Image15Click(Sender: TObject);
    procedure Image16Click(Sender: TObject);
    procedure Image17Click(Sender: TObject);
    procedure Image13Click(Sender: TObject);
  private
    { Private declarations }
  public

  skin_name: string;
   my_dir: string;
   my_alpha: extended;
 procedure realpha;
    { Public declarations }
  end;

var
  Form1: TForm1;

  let_upcase: boolean;
  keymap: tkeymap;

implementation

uses Unit2, Unit3;

{$R *.DFM}

//procedure Tform1.createparams(var params: tcreateparams);
//begin
//inherited
//createparams(params);
//params.exStyle:=ws_ex_transparent;
//end;

const
  WS_EX_LAYERED = $80000;
  LWA_COLORKEY = 1;
  LWA_ALPHA = 2;
type TsetLayeredWindowAttributes = function(
hwnd: Hwnd; crkey: Tcolor; balpha: byte; dwflags: dword): Bool; stdcall;

procedure readmap(filename: string; var kmp: tkeymap);
var t: textfile;
    x, y, z, a: integer;
    s: widestring;
    c: widechar;
    s2: pwidechar;
    sk, si, si2: string;
begin
assignfile(t, filename);
reset(t);
readln(t, si);
readln(t, si2);
form1.Label1.Caption := si + ' < - > ' + si2;
form1.image4.Hint := 'Translate to ' + si;
form1.image14.hint := 'Translate to ' + si2;
x := 1;
kmp.maxin := 0;
kmp.maxout := 0;
while not(eof(t)) do
begin
readln(t, sk);
//s := conv(sk);
a := length(sk);
s := sk;
kmp.ink[x] := s;
if length(s) > kmp.maxin then kmp.maxin := length(s);
y := 1;
kmp.outk[x, y] := '';
readln(t, sk);
s := sk;
//s := conv(sk);
z := 1;
while z <= length(s) do
begin
kmp.outk[x, y] := kmp.outk[x, y] + s[z];
z := z + 1;
if (s[z] = ' ')and(z <=length(s)) then begin
if length(kmp.outk[x, y]) > kmp.maxout then kmp.maxout := length(kmp.outk[x, y]);
y := y + 1;
kmp.outk[x, y] := '';
z := z + 1;
end;
end;
kmp.sizeout[x] := y;
x := x + 1;
end;
kmp.sizein := x - 1;
closefile(t);
end;

procedure configurator;
var t: textfile;
    s, s2: string;
begin
if fileexists('config.ini') then begin
assignfile(t, 'config.ini');
reset(t);
end
else begin
assignfile(t, 'config.ini');
rewrite(t);
writeln(t, '[skin]');
writeln(t, 'hack1');
closefile(t);
assignfile(t, 'config.ini');
reset(t);
end;
while not(eof(t)) do
begin
readln(t, s);
readln(t, s2);
if s = '[skin]' then form1.skin_name := s2;
end;
closefile(t);
end;


procedure TForm1.Image1Click(Sender: TObject);
begin
form1.close;
end;

procedure TForm1.realpha;
var info: tosversioninfo;
f: tsetlayeredwindowattributes;
begin
inherited;
info.dwosversioninfosize := sizeof(info);
getversionex(info);
if (info.dwplatformid = ver_platform_win32_nt) and
(info.dwmajorversion >= 5) then begin
f := getprocaddress(getmodulehandle(user32), 'SetLayeredWindowAttributes');
if assigned(f) then begin
setwindowlong(handle, gwl_exstyle, getwindowlong(handle, Gwl_exstyle) or
ws_ex_layered);
f(handle, 0, round(my_alpha), LWA_ALPHA);
end;
end;
end;

procedure TForm1.FormCreate(Sender: TObject);
var info: tosversioninfo;
f: tsetlayeredwindowattributes;
begin
inherited;
info.dwosversioninfosize := sizeof(info);
getversionex(info);
if (info.dwplatformid = ver_platform_win32_nt) and
(info.dwmajorversion >= 5) then begin
f := getprocaddress(getmodulehandle(user32), 'SetLayeredWindowAttributes');
if assigned(f) then begin
setwindowlong(handle, gwl_exstyle, getwindowlong(handle, Gwl_exstyle) or
ws_ex_layered);
f(handle, 0, round(255 * 80 / 100), LWA_ALPHA);
end;
end;
my_alpha := 255 * 80 / 100;
skin_name := 'hack1';
getdir(0, my_dir);
let_upcase := true;
configurator;
//form1.Brush.Style := bsclear;
form1.BorderStyle := bsnone;
form1.Memo1.Color := form1.skin_body.Canvas.Pixels[1,1];
form1.Memo2.Color := form1.skin_body.Canvas.Pixels[1,1];
form1.Memo1.font.Color := rgb(255,255,255) - form1.memo1.Color;
form1.Memo2.font.Color := rgb(255,255,255) - form1.memo1.Color;
form1.moving.Picture.LoadFromFile(my_dir + '\skins\'+skin_name+'\title.bmp');
form1.skin_body.picture.loadfromfile(my_dir + '\skins\'+skin_name+'\body.bmp');
form1.skin_body.Picture.Bitmap.TransparentColor :=
form1.skin_body.Canvas.Pixels[0, form1.skin_body.Height];
form1.Image1.picture.loadfromfile(my_dir + '\skins\'+skin_name+'\close.bmp');
form1.Image2.picture.loadfromfile(my_dir + '\skins\'+skin_name+'\max.bmp');
form1.image3.Picture.loadfromfile(my_dir + '\skins\'+skin_name+'\min.bmp');
form1.BorderStyle := bsnone;
form1.Memo1.Color := form1.skin_body.Canvas.Pixels[1,1];
form1.Memo2.Color := form1.skin_body.Canvas.Pixels[1,1];
form1.Memo1.font.Color := rgb(255,255,255) - form1.memo1.Color;
form1.Memo2.font.Color := rgb(255,255,255) - form1.memo1.Color;
readmap(my_dir + '\dict\dict1.dct', keymap);
end;

procedure TForm1.Image1MouseDown(Sender: TObject; Button: TMouseButton;
  Shift: TShiftState; X, Y: Integer);
begin
form1.Image1.Picture.LoadFromFile(my_dir + '\skins\'+skin_name+'\close2.bmp');
end;

procedure TForm1.Image1MouseUp(Sender: TObject; Button: TMouseButton;
  Shift: TShiftState; X, Y: Integer);
begin
form1.Image1.Picture.LoadFromFile(my_dir + '\skins\'+skin_name+'\close.bmp');
end;

procedure TForm1.Image2MouseDown(Sender: TObject; Button: TMouseButton;
  Shift: TShiftState; X, Y: Integer);
begin
form1.Image2.Picture.LoadFromFile(my_dir + '\skins\'+skin_name+'\max2.bmp');
end;

procedure TForm1.Image2MouseUp(Sender: TObject; Button: TMouseButton;
  Shift: TShiftState; X, Y: Integer);
begin
form1.Image2.Picture.LoadFromFile(my_dir + '\skins\'+skin_name+'\max.bmp');
end;

procedure TForm1.Image3MouseDown(Sender: TObject; Button: TMouseButton;
  Shift: TShiftState; X, Y: Integer);
begin
form1.Image3.Picture.LoadFromFile(my_dir + '\skins\'+skin_name+'\min2.bmp');
end;

procedure TForm1.Image3MouseUp(Sender: TObject; Button: TMouseButton;
  Shift: TShiftState; X, Y: Integer);
begin
form1.Image3.Picture.LoadFromFile(my_dir + '\skins\'+skin_name+'\min.bmp');
end;

procedure TForm1.Image3Click(Sender: TObject);
begin
application.Minimize;
end;

procedure TForm1.movingMouseDown(Sender: TObject; Button: TMouseButton;
  Shift: TShiftState; X, Y: Integer);
const sc_dragmove = $f012;
begin
releasecapture;
twincontrol(application.MainForm).perform(wm_syscommand, sc_dragmove, 0);
end;

function conv(s: string):widestring;
var x, y: integer;
    t: widestring;
begin
t := '';
for x := 1 to length(s) div 2 do
begin
y := byte(s[x * 2 - 1]) * 256 + byte(s[x * 2]);
t := t + widechar(y);
end;
conv := t;
end;


function downcase(c: widechar):widechar;
begin
if char(c) in ['A'..'Z'] then downcase := widechar(integer(c)+32)
  else if char(c) in ['a'..'z'] then downcase := widechar(integer(c)-32)
  else downcase := c;
  end;

function findmap(c: widestring; keymap: tkeymap; var start: integer): widestring;
var x: integer;
    ar, run: boolean;
    s: widestring;
    pav: widestring;
    fixx, fixy: integer;
begin
x := 1;
ar := false;
pav := c[1];
run := true;
while (not(ar)) and (run) do
begin
if keymap.ink[x] = pav then begin
ar := true; fixx := x; fixy := length(pav); end;
x := x + 1;
if x > keymap.sizein then
if length(pav) = length(c) then run := false
else begin
x := 1;
pav := pav + c[length(pav) + 1];
end;
end;
if (let_upcase)and(not(ar)) then
begin
for x := 1 to length(c) do
c[x]:= downcase(c[x]);
x := 1;
pav := c[1];
run := true;
while (not(ar)) and (run) do
begin
if keymap.ink[x] = pav then begin
ar := true; fixx := x; fixy := length(pav); end;
x := x + 1;
if x > keymap.sizein then
if length(pav) = length(c) then run := false
else begin
x := 1;
pav := pav + c[length(pav) + 1];
end;
end;
end;
if ar then
begin s := keymap.outk[fixx, random(keymap.sizeout[fixx]) + 1];
start := start + fixy;
end
      else begin s := c; start := start + 1; end;
findmap := s;
end;

function unfindmap(c: widestring; keymap: tkeymap; var start: integer): widestring;
var x, y: integer;
    ar, run: boolean;
    s: widestring;
    pav: widestring;
    fixy, fixx, kof: integer;
begin
x := 1;
y := 1;
ar := false;
run := true;
kof := -1;
if kof = 1 then
pav := c[1]
else pav := c;
while (not(ar)) and (run) do
begin
if keymap.outk[x, y] = pav then begin ar := true; fixy:=x; fixx := length(pav); end;
y := y + 1;
if  y > keymap.sizeout[x] then
begin
x := x + 1;
y := 1;
end;
if x > keymap.sizein then
if (length(pav) < length(c)) and (kof = 1) then begin
x := 1;
y := 1;
pav := pav + c[length(pav)+1];
end
else if (length(pav) > 1)and(kof = -1) then begin
x := 1;
y := 1;
delete(pav, length(pav), 1);
end
else
run := false;
end;
if ar then begin s := keymap.ink[fixy]; start := start + fixx; end
      else begin s := c[1]; start := start + 1; end;
unfindmap := s;
end;

procedure translate(how: byte);
var
    y, x, co: integer;
    s, s2, s3: widestring;
begin
form2.show;
//form2.load_as('hack1');
if how = 1 then begin
form1.Memo2.Lines.Clear;
randomize;
for y := 0 to form1.memo1.Lines.Count do
begin
s := '';
s2 := form1.Memo1.Lines.Strings[y];
x := 1;
while x <= length(s2) do
begin
if x + keymap.maxin - 1 <= length(s2) then co := keymap.maxin else
co := length(s2) - x + 1;
s3 := copy(s2, x, co);
s := s + findmap(s3, keymap, x);
end;
form1.Memo2.Lines.Add(s);
//form2.talk(s);
end;
end;
end;


procedure untranslate(how: byte);
var
    y, x: integer;
    co: byte;
    s, s2, s3: widestring;
begin
if how = 1 then begin
form1.Memo1.Lines.Clear;
for y := 0 to form1.memo2.Lines.Count do
begin
s := '';
s2 := form1.Memo2.Lines.Strings[y];
x := 1;
while x <= length(s2) do
begin
if x + keymap.maxout - 1 <= length(s2) then co := keymap.maxout else
co := length(s2) - x + 1;
s3 := copy(s2, x, co);
s := s + unfindmap(s3, keymap, x);
end;

form1.Memo1.Lines.Add(s);

end;
end;
end;

procedure TForm1.Image4Click(Sender: TObject);
begin
translate(1);
end;

procedure TForm1.BitBtn1Click(Sender: TObject);
var t: textfile;
    s: string;
begin
assignfile(t, 'c:\dict1.dct');
reset(t);
readln(t, s);
form1.Memo1.lines.add(s);
readln(t, s);
form1.Memo1.lines.add(s);
readln(t, s);
form1.Memo1.lines.add(s);
readln(t, s);
form1.Memo1.lines.add(s);

closefile(t);
end;

procedure TForm1.SpeedButton1Click(Sender: TObject);
begin
untranslate(1);
end;

procedure TForm1.Image5Click(Sender: TObject);
begin
form1.OpenDialog1.Title := 'Open';
form1.OpenDialog1.Filter := '*.*|*.*|*.txt|*.txt|*.nfo|*.nfo';
form1.OpenDialog1.Execute;
if fileexists(form1.OpenDialog1.FileName) then
form1.Memo1.Lines.LoadFromFile(form1.opendialog1.filename);
end;

procedure TForm1.Image6Click(Sender: TObject);
begin
form1.OpenDialog1.Title := 'Open';
form1.OpenDialog1.Filter := '*.*|*.*|*.txt|*.txt|*.nfo|*.nfo';
form1.OpenDialog1.Execute;
if fileexists(form1.OpenDialog1.FileName) then
form1.Memo2.Lines.LoadFromFile(form1.opendialog1.filename);
end;

procedure TForm1.Image7Click(Sender: TObject);
begin
form1.SaveDialog1.Execute;
//if fileexists(form1.SaveDialog1.FileName) then
if form1.SaveDialog1.FileName <> '' then
form1.Memo1.Lines.SaveToFile(form1.opendialog1.filename);
end;

procedure TForm1.Image10Click(Sender: TObject);
begin
form1.SaveDialog1.Execute;
//if fileexists(form1.SaveDialog1.FileName) then
if form1.SaveDialog1.FileName <> '' then
form1.Memo2.Lines.SaveToFile(form1.opendialog1.filename);
end;

procedure TForm1.Image8Click(Sender: TObject);
begin
form1.Memo1.CopyToClipboard;
end;

procedure TForm1.Image11Click(Sender: TObject);
begin
form1.Memo2.CopyToClipboard;
end;


procedure TForm1.Image9Click(Sender: TObject);
begin
form1.Memo1.PasteFromClipboard;
end;

procedure TForm1.Image12Click(Sender: TObject);
begin
form1.Memo2.PasteFromClipboard;
end;

procedure TForm1.Image14Click(Sender: TObject);
begin
untranslate(1);
end;

procedure TForm1.Image15Click(Sender: TObject);
var w, h: thandle;
    p: pchar;
    s1, s21: string;
    s, s2: widestring;
    x, y: integer;
begin

w := getwindow(getdesktopwindow, gw_child);
new(p);
while w <> 0 do
begin
getmem(p, 260);
getwindowtext(w, p, 255);
s1 := string(p);
freemem(p);
s := widestring(s1);
s2 := '';
for x := 1 to length(s) do
begin
s2 := s2 + findmap(s[x], keymap, y);
end;
s21 := string(s2);
h := createevent(nil, true, false, '');
waitforsingleobject(h, 100);
closehandle(h);
sendmessage(w, WM_SETTEXT, 0, lparam(pchar(s21)));
w := getwindow(w, gw_hwndnext);
h := createevent(nil, true, false, '');
waitforsingleobject(h, 100);
closehandle(h);
end;
end;

procedure TForm1.Image16Click(Sender: TObject);
var w, h: thandle;
    p: pchar;
    s1, s21: string;
    s, s2: widestring;
    s3: widestring;
    x: integer;
    co: byte;
begin

w := getwindow(getdesktopwindow, gw_child);
new(p);
while w <> 0 do
begin
getmem(p, 260);
getwindowtext(w, p, 255);
s1 := string(p);
freemem(p);
s := widestring(s1);
s2 := '';
x := 1;
while x <= length(s) do
begin
if x + 3 <= length(s) then co := 4 else
co := length(s) - x + 1;
s3 := copy(s, x, co);
s2 := s2 + unfindmap(s3, keymap, x);
end;
s21 := string(s2);
h := createevent(nil, true, false, '');
waitforsingleobject(h, 100);
closehandle(h);
sendmessage(w, WM_SETTEXT, 0, lparam(pchar(s21)));
w := getwindow(w, gw_hwndnext);
h := createevent(nil, true, false, '');
waitforsingleobject(h, 100);
closehandle(h);
end;
end;

procedure TForm1.Image17Click(Sender: TObject);
begin
form1.OpenDialog1.Title := 'Choose dictionary';
form1.OpenDialog1.Filter := '*.dct|*.dct';
form1.opendialog1.execute;
if fileexists(form1.opendialog1.filename) then
readmap(form1.OpenDialog1.FileName, keymap);
end;

procedure TForm1.Image13Click(Sender: TObject);
begin
form3.show;
end;

end.
