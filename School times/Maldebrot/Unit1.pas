unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, ExtCtrls, ExtDlgs;

type
  TForm1 = class(TForm)
    Image1: TImage;
    Button1: TButton;
    Shape1: TShape;
    Timer1: TTimer;
    Button2: TButton;
    Button3: TButton;
    Timer2: TTimer;
    Button4: TButton;
    SavePictureDialog1: TSavePictureDialog;
    procedure Button1Click(Sender: TObject);
    procedure Timer1Timer(Sender: TObject);
    procedure Shape1MouseDown(Sender: TObject; Button: TMouseButton;
      Shift: TShiftState; X, Y: Integer);
    procedure Shape1MouseUp(Sender: TObject; Button: TMouseButton;
      Shift: TShiftState; X, Y: Integer);
    procedure Button2Click(Sender: TObject);
    procedure FormCreate(Sender: TObject);
    procedure Button3Click(Sender: TObject);
    procedure Timer2Timer(Sender: TObject);
    procedure Button4Click(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  Form1: TForm1;
  movee: boolean;
  sx, sy, fx, fy: integer;
  did: longint;
  sdx, sdy: double;
implementation

{$R *.dfm}
function get(x, y: double): word;
var a, b, c, sa, sb: double;
    nr: word;
begin
nr := 0;
sa :=x;
sb := y;
a := x;
b := y;
while (sqr(a) + sqr(b) < 4) and (nr < 255) do
begin
c := a;
a := sqr(a) - sqr(b) + sa;
b := 2 * b * c + sb;
nr := nr + 1;
end;
get := nr;
end;

procedure TForm1.Button1Click(Sender: TObject);
var x, y: integer;
    cl: word;
begin
for x := 0 to fx do
for y := 0 to fy do
begin
cl := get(x/did + sdx, y/did + sdy);
form1.Image1.Canvas.Pixels[x, y] := cl * 10000
end;
end;

procedure TForm1.Timer1Timer(Sender: TObject);
var p: tpoint;
    s1, s2: string;
begin
if movee then begin
getcursorpos(p);
str(p.x, s1);
str(p.Y, s2);

form1.Shape1.Left := p.x - form1.Left;
form1.shape1.Top := p.Y - form1.top;
sx := form1.Shape1.Left;
sy := form1.shape1.Top;
end;
end;

procedure TForm1.Shape1MouseDown(Sender: TObject; Button: TMouseButton;
  Shift: TShiftState; X, Y: Integer);
begin
if not(movee) then begin
sx := form1.Shape1.left;
sy := form1.Shape1.top;
movee:=true;
end;

end;

procedure TForm1.Shape1MouseUp(Sender: TObject; Button: TMouseButton;
  Shift: TShiftState; X, Y: Integer);
begin
 movee := false;

end;

procedure TForm1.Button2Click(Sender: TObject);
begin
sdx := sx/did + sdx;
sdy := sy/did + sdy;
did := did * 10;
end;

procedure TForm1.FormCreate(Sender: TObject);
begin
did := 100;
sx := 0;
sy := 0;
sdx := 0;
sdy := 0;
movee := false;
end;

procedure TForm1.Button3Click(Sender: TObject);
begin
form1.close;
end;

procedure pradzia;
begin
did := 100;
sdx := form1.Width/did;
sdy := form1.Height/did;
sdx := -sdx/2;
sdy := -sdy/2;
fx := form1.width;
fy := form1.height;
form1.Shape1.Width := fx div 10;
form1.Shape1.Height := fy div 10;
end;

procedure TForm1.Timer2Timer(Sender: TObject);
begin
form1.Timer2.Enabled:=false;
pradzia;
end;

procedure TForm1.Button4Click(Sender: TObject);
begin
form1.SavePictureDialog1.Execute;
form1.Image1.Picture.SaveToFile(form1.SavePictureDialog1.FileName);
end;

end.
