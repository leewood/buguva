unit Unit3;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, ExtDlgs;

type
  TForm3 = class(TForm)
    Label1: TLabel;
    Button1: TButton;
    Label2: TLabel;
    Label3: TLabel;
    Label4: TLabel;
    Button2: TButton;
    Label5: TLabel;
    Label6: TLabel;
    Button3: TButton;
    OpenDialog1: TOpenDialog;
    ScrollBar1: TScrollBar;
    Label7: TLabel;
    procedure Button1Click(Sender: TObject);
    procedure FormShow(Sender: TObject);
    procedure ScrollBar1Change(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  Form3: TForm3;

implementation

uses Unit1;

{$R *.dfm}

procedure TForm3.Button1Click(Sender: TObject);
var s, rez: string;
    x, y: integer;
begin
form3.OpenDialog1.Execute;
s := form3.opendialog1.filename;
x := length(s);
while (s[x] <> '\')or (x < 1) do
begin
x := x - 1;
end;
y := x - 1;
while (s[y] <> '\')or (y < 1) do
begin
y := y - 1;
end;

rez := copy(s, y + 1, x - y - 1);
form3.label2.caption := rez;
form1.skin_name := rez;
form1.moving.Picture.LoadFromFile(form1.my_dir + '\skins\'+form1.skin_name+'\title.bmp');
form1.skin_body.picture.loadfromfile(form1.my_dir + '\skins\'+form1.skin_name+'\body.bmp');
form1.skin_body.Picture.Bitmap.TransparentColor :=
form1.skin_body.Canvas.Pixels[0, form1.skin_body.Height];
form1.Image1.picture.loadfromfile(form1.my_dir + '\skins\'+form1.skin_name+'\close.bmp');
form1.Image2.picture.loadfromfile(form1.my_dir + '\skins\'+form1.skin_name+'\max.bmp');
form1.image3.Picture.loadfromfile(form1.my_dir + '\skins\'+form1.skin_name+'\min.bmp');
form1.BorderStyle := bsnone;
form1.Memo1.Color := form1.skin_body.Canvas.Pixels[1,1];
form1.Memo2.Color := form1.skin_body.Canvas.Pixels[1,1];
form1.Memo1.font.Color := rgb(255,255,255) - form1.memo1.Color;
form1.Memo2.font.Color := rgb(255,255,255) - form1.memo1.Color;
end;

procedure TForm3.FormShow(Sender: TObject);
begin
form3.Label2.Caption := form1.skin_name;
form3.ScrollBar1.Position := round(form1.my_alpha);
end;

procedure TForm3.ScrollBar1Change(Sender: TObject);
begin
form1.my_alpha := form3.ScrollBar1.Position;
form1.realpha;
end;

end.
