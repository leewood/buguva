unit Unit6;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, ExtCtrls;

type
  TForm6 = class(TForm)
    Image1: TImage;
    Timer1: TTimer;
    procedure FormKeyPress(Sender: TObject; var Key: Char);
    procedure FormShow(Sender: TObject);
    procedure Timer1Timer(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  Form6: TForm6;
  mas: array[1..100] of string;
  cur, x, y: integer;
implementation

uses Unit3;

{$R *.dfm}
procedure ateinupr(name: string; var x, y: integer);
begin
form6.Image1.Picture.LoadFromFile(name);
x := -form6.Width;
y := -form6.Height;
form6.Image1.top:=0;
form6.Image1.left:=x;
form6.Image1.Width:=-x;
form6.Image1.Height:=-y;
end;

procedure judek(x, y: integer);
begin
form6.Image1.top:=0;
form6.Image1.left:=x;
end;

procedure readf(name: string);
var t: textfile;
    x: integer;
begin
assignfile(t, name);
reset(t);
x := 1;
while not(eof(t)) do
begin
  readln(t, mas[x]);
  x := x + 1;
end;
cur := 0;
closefile(t);
end;
procedure pictnext;
  begin
    cur := cur + 1;
ateinupr(mas[cur], x, y);
form6.Timer1.Enabled:=true;
  end;
procedure pictlast;
  begin
    cur := cur - 1;
    form6.Image1.Picture.LoadFromFile(mas[cur]);
  end;
procedure TForm6.FormKeyPress(Sender: TObject; var Key: Char);
begin
if key = 's' then pictnext;
if key = 'a' then pictlast;
if key = #27 then begin form6.Hide; form3.showmodal; end;
end;

procedure TForm6.FormShow(Sender: TObject);
begin
readf('config.ini');
end;

procedure TForm6.Timer1Timer(Sender: TObject);
begin
x := x + 100;
judek(x, y);
if x >= 0 then begin form6.Timer1.Enabled:=false;
judek(x - 100, y);
end;
end;

end.
