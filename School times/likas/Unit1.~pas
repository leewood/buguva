unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, ComCtrls, ExtCtrls;

type
  TForm1 = class(TForm)
    Timer1: TTimer;
    Timer2: TTimer;
    Image1: TImage;

    procedure Button2Click(Sender: TObject);
    procedure FormKeyDown(Sender: TObject; var Key: Word;
      Shift: TShiftState);
    procedure FormKeyUp(Sender: TObject; var Key: Word;
      Shift: TShiftState);
    procedure Button2KeyDown(Sender: TObject; var Key: Word;
      Shift: TShiftState);
    procedure Button2KeyUp(Sender: TObject; var Key: Word;
      Shift: TShiftState);
    procedure Timer1Timer(Sender: TObject);
    procedure FormCreate(Sender: TObject);
    procedure Timer2Timer(Sender: TObject);
  private
    { Private declarations }
  public
  komanda: integer;
  taskas: array[1..10] of integer;
  vardai: array[1..10] of string;
  kiek:integer;
  reikia: boolean;
    procedure namas(x, y, z, taskai: integer);
    procedure sendkey(key: word);
    function showkey:word;
    procedure usetaskai(i: integer);

    { Public declarations }
  end;

var
  Form1: TForm1;
  taskai: integer;
  busena: boolean;
  keyevent: word;
  reikia: boolean;
implementation

uses Unit2, Unit3, Unit5, Unit6;

{$R *.dfm}

procedure tform1.namas(x, y, z, taskai: integer);
begin
if taskai >= 1 then
begin
form1.Canvas.moveto(x + 95*z, y + 200*z);
form1.Canvas.lineto(x + 305*z, y + 200*z);
end;
if taskai >= 2 then
begin
form1.canvas.MoveTo(x + 305*z, y + 200*z);
form1.Canvas.LineTo(x + 305*z, y + 220*z);
end;
if taskai >= 3 then
begin
form1.canvas.MoveTo(x + 305*z, y + 220*z);
form1.Canvas.lineto(x + 95*z, y + 220*z);
end;
if taskai >= 4 then
begin
form1.canvas.MoveTo(x + 95*z, y + 220*z);
form1.canvas.lineto(x + 95*z, y + 200*z);
end;
if taskai >= 5 then
begin
form1.canvas.MoveTo(x + 100*z, y + 100*z);
form1.Canvas.LineTo(x + 300*z, y + 100*z);
end;
if taskai >= 6 then
begin
form1.canvas.MoveTo(x + 300*z,y + 100*z);
form1.Canvas.lineto(x + 300*z,y + 200*z);
end;
if taskai >= 7 then
begin
form1.canvas.MoveTo(x + 300*z,y + 200*z);
form1.Canvas.LineTo(x + 100*z,y + 200*z);
end;
if taskai >= 8 then
begin
form1.canvas.MoveTo(x + 100*z,y + 200*z);
form1.Canvas.LineTo(x + 100*z,y + 100*z);
end;
if taskai >= 9 then
begin
form1.Canvas.moveto(x + 80*z,y + 100*z);
form1.Canvas.LineTo(x + 320*z,y + 100*z);
end;
if taskai >= 10 then
begin
form1.canvas.MoveTo(x + 320*z,y + 100*z);
form1.Canvas.LineTo(x + 260*z,y + 50*z);
end;
if taskai >= 11 then
begin
form1.canvas.MoveTo(x + 260*z,y + 50*z);
form1.Canvas.LineTo(x + 140*z,y + 50*z);
end;
if taskai >= 12 then
begin
form1.canvas.MoveTo(x + 140*z,y + 50*z);
form1.Canvas.LineTo(x + 80*z,y + 100*z);
end;
if taskai >= 13 then
begin
form1.Canvas.moveto(x + 230*z,y + 200*z);
form1.Canvas.lineTo(x + 230*z,y + 125*z);
end;
if taskai >= 14 then
begin
form1.canvas.MoveTo(x + 230*z,y + 125*z);
form1.canvas.lineto(x + 170*z,y + 125*z);
end;
if taskai >= 15 then
begin
form1.canvas.MoveTo(x + 170*z,y + 125*z);
form1.Canvas.LineTo(x + 170*z,y + 200*z);
end;
if taskai >= 16 then
begin
form1.canvas.moveto(x + 230*z,y + 162*z);
form1.Canvas.LineTo(x + 210*z,y + 162*z);
end;
if taskai >= 17 then
begin
form1.canvas.moveto(x + 110*z,y + 125*z);
form1.Canvas.lineto(x + 160*z,y + 125*z);
end;
if taskai >= 18 then
begin
form1.canvas.MoveTo(x + 160*z,y + 125*z);
form1.Canvas.LineTo(x + 160*z,y + 175*z);
end;
if taskai >= 19 then
begin
form1.canvas.MoveTo(x + 160*z,y + 175*z);
form1.Canvas.LineTo(x + 110*z,y + 175*z);
end;
if taskai >= 20 then
begin
form1.canvas.MoveTo(x + 110*z,y + 175*z);
form1.Canvas.LineTo(x + 110*z,y + 125*z);
end;
if taskai >= 21 then
begin
form1.canvas.moveto(x + 240*z,y + 125*z);
form1.Canvas.lineto(x + 290*z,y + 125*z);
end;
if taskai >= 22 then
begin
form1.canvas.MoveTo(x + 290*z,y + 125*z);
form1.Canvas.LineTo(x + 290*z,y + 175*z);
end;
if taskai >= 23 then
begin
form1.canvas.MoveTo(x + 290*z,y + 175*z);
form1.Canvas.LineTo(x + 240*z,y + 175*z);
end;
if taskai >= 24 then
begin
form1.canvas.MoveTo(x + 240*z,y + 175*z);
form1.Canvas.LineTo(x + 240*z,y + 125*z);
end;
if taskai >= 25 then
begin
form1.Canvas.moveto(x + 190*z,y + 50*z);
form1.Canvas.LineTo(x + 190*z,y + 25*z);
end;
if taskai >= 26 then
begin
form1.canvas.MoveTo(x + 190*z,y + 25*z);
form1.canvas.lineto(x + 210*z,y + 25*z);
end;
if taskai >= 27 then
begin
form1.canvas.MoveTo(x + 210*z,y + 25*z);
form1.Canvas.LineTo(x + 210*z,y + 50*z);
end;
if taskai >= 28 then
begin
form1.canvas.MoveTo(x + 200*z,y + 25*z);
form1.Canvas.LineTo(x + 210*z,y + 15*z);
end;
if taskai >= 29 then
begin
form1.canvas.moveto(x + 180*z,y + 200*z);
form1.Canvas.LineTo(x + 180*z,y + 220*z);
end;
if taskai >= 30 then
begin
form1.Canvas.moveto(x + 220*z,y + 200*z);
form1.Canvas.LineTo(x + 220*z,y + 220*z);
end;
if taskai >= 31 then
begin
form1.canvas.moveto(x + 180*z,y + 207*z);
form1.Canvas.LineTo(x + 220*z,y + 207*z);
end;
if taskai >= 32 then
begin
form1.Canvas.MoveTo(x + 180*z,y + 214*z);
form1.Canvas.LineTo(x + 220*z,y + 214*z);
end;
end;

procedure tform1.sendkey(key: word);
begin
keyevent := key;
end;

function tform1.showkey:word;
begin
showkey := keyevent;
end;

{pagrindas
form1.canvas.MoveTo(100,100);
form1.Canvas.LineTo(300,100);
form1.canvas.MoveTo(300,100);
form1.Canvas.lineto(300,200);
form1.canvas.MoveTo(300,200);
form1.Canvas.LineTo(100,200);
form1.canvas.MoveTo(100,200);
form1.Canvas.LineTo(100,100);}
{stogas
form1.Canvas.moveto(80,100);
form1.Canvas.LineTo(320,100);
form1.canvas.MoveTo(320,100);
form1.Canvas.LineTo(260,50);
form1.canvas.MoveTo(260,50);
form1.Canvas.LineTo(140,50);
form1.canvas.MoveTo(140,50);
form1.Canvas.LineTo(80,100);}
{pamatai
form1.Canvas.moveto(95,200);
form1.Canvas.lineto(305, 200);
form1.canvas.MoveTo(305,200);
form1.Canvas.LineTo(305,220);
form1.canvas.MoveTo(305,220);
form1.Canvas.lineto(95,220);
form1.canvas.MoveTo(95,220);
form1.canvas.lineto(95,200);}
{durys
form1.Canvas.moveto(230,200);
form1.Canvas.lineTo(230,125);
form1.canvas.MoveTo(230,125);
form1.canvas.lineto(170,125);
form1.canvas.MoveTo(170,125);
form1.Canvas.LineTo(170,200);
form1.canvas.moveto(230,162);
form1.Canvas.LineTo(210,162);}
{langas1
form1.canvas.moveto(110,125);
form1.Canvas.lineto(160,125);
form1.canvas.MoveTo(160,125);
form1.Canvas.LineTo(160,175);
form1.canvas.MoveTo(160,175);
form1.Canvas.LineTo(110,175);
form1.canvas.MoveTo(110,175);
form1.Canvas.LineTo(110,125);}
{langas2
form1.canvas.moveto(240,125);
form1.Canvas.lineto(290,125);
form1.canvas.MoveTo(290,125);
form1.Canvas.LineTo(290,175);
form1.canvas.MoveTo(290,175);
form1.Canvas.LineTo(240,175);
form1.canvas.MoveTo(240,175);
form1.Canvas.LineTo(240,125);}
{kaminas
form1.Canvas.moveto(190,50);
form1.Canvas.LineTo(190,25);
form1.canvas.MoveTo(190,25);
form1.canvas.lineto(210,25);
form1.canvas.MoveTo(210,25);
form1.Canvas.LineTo(210,50);
form1.canvas.MoveTo(200,25);
form1.Canvas.LineTo(210,15);}
{laiptai
form1.canvas.moveto(180,200);
form1.Canvas.LineTo(180,220);
form1.Canvas.moveto(220,200);
form1.Canvas.LineTo(220,220);
form1.canvas.moveto(180,207);
form1.Canvas.LineTo(220,207);
form1.Canvas.MoveTo(180,214);
form1.Canvas.LineTo(220,214); }


procedure TForm1.Button2Click(Sender: TObject);
begin
form1.Close;
end;

procedure TForm1.FormKeyDown(Sender: TObject; var Key: Word;  Shift: TShiftState);
begin
sendkey(key);
end;

procedure TForm1.FormKeyUp(Sender: TObject; var Key: Word;
  Shift: TShiftState);
begin
sendkey(0);
end;

procedure TForm1.Button2KeyDown(Sender: TObject; var Key: Word;
  Shift: TShiftState);
begin
sendkey(key);
end;

procedure TForm1.Button2KeyUp(Sender: TObject; var Key: Word;
  Shift: TShiftState);
begin
sendkey(0);
end;

procedure tform1.usetaskai(i: integer);
begin
form1.taskas[form1.komanda] := form1.taskas[form1.komanda] + i;
end;
procedure pies(x,y, taskai, komanda: integer);
var t: trect;
    s: string;
begin
if form1.reikia then begin
t.Left:=x;
t.Top:=y+50;
t.Right:=x+350;
t.Bottom:=y+270;
form1.Canvas.FillRect(t);
form1.namas(x,y,1, taskai);
form1.Canvas.Font.Size:=18;
form1.Canvas.Font.Name:='Times New Roman';
form1.Canvas.TextOut(x + 50, y + 230, 'Komandos pavadinimas: ');
form1.Canvas.Font.Size:=24;
form1.Canvas.TextOut(x + 50, y+270, form1.vardai[komanda]);
str(taskai, s);
form1.Canvas.TextOut(x+50,y+320,'Taskai: ' + s);
end;
end;
procedure TForm1.Timer1Timer(Sender: TObject);
var key: integer;
    s: string;
    t: trect;
begin
key := showkey;
if key = 8 then begin sendkey(0); form5.show;form5.Timer1.Enabled:=true;end;
if key = 13 then begin sendkey(0); form6.show;end;
if (key >= 112)and(key <= 122) then
begin komanda := key - 111;
sendkey(0);
form2.show;
form2.Timer2.Enabled:=true;
form1.reikia := true;
end;
if (key = 27)and(komanda = 0) then form1.close;
if form1.kiek > 0 then pies(0,-15,taskas[1], 1);
if form1.kiek > 1 then pies(350,-15, taskas[2], 2);
if form1.kiek > 2 then pies(700,-15,taskas[3],3);
if form1.kiek > 3 then pies(0,240,taskas[4],4);
if form1.kiek > 4 then pies(350,240, taskas[5], 5);
if form1.kiek > 5 then pies(700,240,taskas[6],6);
if form1.kiek > 6 then pies(150,480, taskas[7], 7);
if form1.kiek > 7 then pies(500,480, taskas[8], 8);
form1.reikia := false;
end;

procedure TForm1.FormCreate(Sender: TObject);
begin
form1.kiek:=0;
reikia := true;
end;

procedure TForm1.Timer2Timer(Sender: TObject);
begin
form6.Show;
form1.Timer2.Enabled:=false;
end;

end.
