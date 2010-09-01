unit Unit2;

interface

uses
  Windows, Messages, SysUtils, Classes, Graphics, Controls, Forms, Dialogs,
  ExtCtrls, StdCtrls, ComCtrls;

type
  TForm2 = class(TForm)
    Image1: TImage;
    Image2: TImage;
    Image3: TImage;
    Label1: TLabel;
    Timer1: TTimer;
    Image4: TImage;
    procedure Timer1Timer(Sender: TObject);
    procedure FormMouseDown(Sender: TObject; Button: TMouseButton;
      Shift: TShiftState; X, Y: Integer);
    procedure Image4MouseDown(Sender: TObject; Button: TMouseButton;
      Shift: TShiftState; X, Y: Integer);
    procedure Image1MouseDown(Sender: TObject; Button: TMouseButton;
      Shift: TShiftState; X, Y: Integer);
    procedure Image2MouseDown(Sender: TObject; Button: TMouseButton;
      Shift: TShiftState; X, Y: Integer);
    procedure Image3MouseDown(Sender: TObject; Button: TMouseButton;
      Shift: TShiftState; X, Y: Integer);
    procedure Label1MouseDown(Sender: TObject; Button: TMouseButton;
      Shift: TShiftState; X, Y: Integer);
  private
    { Private declarations }
  public
    { Public declarations }
    procedure load_as(name: string);
    procedure next_pic;
    procedure prev_pic;
    procedure ch_speed(speed: integer);
    procedure talk(speech: string);
    procedure cleartext;
  end;

var
  Form2: TForm2;
  as_name: string;
  cur: integer;
  talking: string;
implementation
{$R *.DFM}

procedure tform2.load_as(name: string);
begin
form2.Image1.Picture.LoadFromFile('.\assistant\'+name+'\back.bmp');
form2.Image2.Picture.LoadFromFile('.\assistant\'+name+'\pic1.bmp');
form2.Image3.Picture.LoadFromFile('.\assistant\'+name+'\baloon.bmp');
as_name := name;
cur := 1;
end;

procedure tform2.cleartext;
begin
talking := '';
end;

procedure tform2.next_pic;
var s: string;
begin
cur := cur + 1;
if cur > 4 then cur := 1;
str(cur, s);
form2.Image2.Picture.LoadFromFile('.\assistant\'+as_name+'\pic'+s+'.bmp');
end;

procedure tform2.prev_pic;
var s: string;
begin
cur := cur - 1;
str(cur, s);
form2.Image2.Picture.LoadFromFile('.\assistant\'+as_name+'\pic'+s+'.bmp');
end;

procedure tform2.ch_speed(speed: integer);
begin
form2.Timer1.Interval := speed;
end;

procedure tform2.talk(speech: string);
  begin
    talking := talking + speech;
  end;

procedure nupiesk(s: string);
const tsize = 8;
var s2: string;
    size, y: integer;
    r: trect;
begin
r.Left := 0;
r.Top := 0;
r.Right := form2.Image4.Width;
r.Bottom := form2.Image4.Height;
y := 1;
size := form2.Image4.Width;
form2.Image4.Canvas.FillRect(r);
while length(s) > 0 do
begin
if length(s) * tsize >= size then begin
s2 := copy(s, 1, size div tsize);
delete(s, 1, size div tsize);
end
else begin s2 := s; s := ''; end;
form2.Image4.Canvas.TextOut(1, y, s2);
y := y + 12;
end;
end;

procedure TForm2.Timer1Timer(Sender: TObject);
var s: string;
begin
if length(talking) > 0 then begin
s := form2.Label1.Caption;
if length(s) >= 100 then delete(s, 1, 1);
s := s + talking[1];
FORM2.label1.caption := s;
delete(talking, 1, 1);
{form2.Label1.Caption := s;}
next_pic;

nupiesk(s);

end;
end;

procedure TForm2.FormMouseDown(Sender: TObject; Button: TMouseButton;
  Shift: TShiftState; X, Y: Integer);
const sc_dragmove = $f012;
begin
releasecapture;
twincontrol(form2).perform(wm_syscommand, sc_dragmove, 0);
end;

procedure TForm2.Image4MouseDown(Sender: TObject; Button: TMouseButton;
  Shift: TShiftState; X, Y: Integer);
begin
form2.formmousedown(sender, button, shift, x, y);
end;

procedure TForm2.Image1MouseDown(Sender: TObject; Button: TMouseButton;
  Shift: TShiftState; X, Y: Integer);
begin
form2.formmousedown(sender, button, shift, x, y);
end;

procedure TForm2.Image2MouseDown(Sender: TObject; Button: TMouseButton;
  Shift: TShiftState; X, Y: Integer);
begin
form2.formmousedown(sender, button, shift, x, y);
end;

procedure TForm2.Image3MouseDown(Sender: TObject; Button: TMouseButton;
  Shift: TShiftState; X, Y: Integer);
begin
form2.formmousedown(sender, button, shift, x, y);
end;

procedure TForm2.Label1MouseDown(Sender: TObject; Button: TMouseButton;
  Shift: TShiftState; X, Y: Integer);
begin
form2.formmousedown(sender, button, shift, x, y);
end;

end.
