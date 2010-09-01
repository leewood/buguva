unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, ExtCtrls;

type
  TForm1 = class(TForm)
    Image1: TImage;
    Image2: TImage;
    Timer1: TTimer;
    procedure Timer1Timer(Sender: TObject);
    procedure FormCreate(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  Form1: TForm1;
  pos,pos1,pos2:integer;
implementation

{$R *.dfm}

procedure TForm1.Timer1Timer(Sender: TObject);

begin
form1.Image1.left:=form1.Image1.left+pos;
form1.image2.Left:=form1.image2.Left+pos;

if form1.image1.width+form1.Image1.left=0 then begin
form1.Image1.left:=500;

end;
if form1.Image2.Width+form1.Image2.left=0 then begin
form1.Image2.left:=500;

end;

end;

procedure TForm1.FormCreate(Sender: TObject);
begin
pos:=-10;
pos1:=0;
pos2:=10;
end;

end.
