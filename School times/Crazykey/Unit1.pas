unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, ExtCtrls, StdCtrls, KeyState, Menus, TBNArea;

type
  TForm1 = class(TForm)
    KeyState1: TKeyState;
    Edit1: TEdit;
    Label1: TLabel;
    Button1: TButton;
    Button2: TButton;
    Timer1: TTimer;
    Button3: TButton;
    Button4: TButton;
    Button5: TButton;
    SaveDialog1: TSaveDialog;
    OpenDialog1: TOpenDialog;
    TBNArea1: TTBNArea;
    PopupMenu1: TPopupMenu;
    Show1: TMenuItem;
    Close1: TMenuItem;
    procedure Timer1Timer(Sender: TObject);
    procedure Button1Click(Sender: TObject);
    procedure Button2Click(Sender: TObject);
    procedure FormCreate(Sender: TObject);
    procedure Button3Click(Sender: TObject);
    procedure Button4Click(Sender: TObject);
    procedure Button5Click(Sender: TObject);
    procedure Show1Click(Sender: TObject);
    procedure Close1Click(Sender: TObject);
    procedure FormDeactivate(Sender: TObject);
    procedure FormDblClick(Sender: TObject);
    procedure TBNArea1DblClick(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  Form1: TForm1;
  song: string;
  where, how: integer;
  toplay: boolean;

implementation

{$R *.dfm}
procedure makeit(x: integer);
begin
case x of
1: form1.KeyState1.NumLock := not(form1.KeyState1.NumLock);
2: form1.KeyState1.CapsLock := not(form1.KeyState1.CapsLock);
3: form1.KeyState1.ScrollLock := not(form1.KeyState1.ScrollLock);
4: begin
form1.KeyState1.NumLock := not(form1.KeyState1.NumLock);
form1.KeyState1.CapsLock := not(form1.KeyState1.CapsLock);
end;
5: begin
form1.KeyState1.CapsLock := not(form1.KeyState1.CapsLock);
form1.KeyState1.ScrollLock := not(form1.KeyState1.ScrollLock);
end;
6: begin
form1.KeyState1.NumLock := not(form1.KeyState1.NumLock);
form1.KeyState1.ScrollLock := not(form1.KeyState1.ScrollLock);
end;
7: begin
form1.KeyState1.NumLock := not(form1.KeyState1.NumLock);
form1.KeyState1.CapsLock := not(form1.KeyState1.CapsLock);
form1.KeyState1.ScrollLock := not(form1.KeyState1.ScrollLock);
end;
8: begin
form1.KeyState1.NumLock := false;
form1.KeyState1.CapsLock := false;
form1.KeyState1.ScrollLock := false;
end;
9: begin
form1.KeyState1.NumLock := true;
form1.KeyState1.CapsLock := true;
form1.KeyState1.ScrollLock := true;
end;
10: begin
form1.KeyState1.NumLock := true;
end;
11: begin
form1.KeyState1.NumLock := false;
end;
12: begin
form1.KeyState1.CapsLock := true;
end;
13: begin
form1.KeyState1.CapsLock := false;
end;
14: begin
form1.KeyState1.ScrollLock := true;
end;
15: begin
form1.KeyState1.ScrollLock := false;
end;
end;
end;

procedure TForm1.Timer1Timer(Sender: TObject);
var x, y: integer;
begin
if toplay then begin
val(song[where], x, y);
where := where + 1;
if where > how then where := 1;
makeit(x);
end;
end;

procedure TForm1.Button1Click(Sender: TObject);
begin
song := form1.Edit1.Text;
how := length(song);
where := 1;
toplay := true;
end;

procedure TForm1.Button2Click(Sender: TObject);
begin
toplay := false;
end;

procedure TForm1.FormCreate(Sender: TObject);
begin
toplay := false;
end;

procedure TForm1.Button3Click(Sender: TObject);
begin
form1.KeyState1.CapsLock := false;
form1.KeyState1.ScrollLock := false;
form1.KeyState1.NumLock := false;
end;

procedure TForm1.Button4Click(Sender: TObject);
var t: textfile;
begin
form1.SaveDialog1.Execute;
assignfile(t, form1.SaveDialog1.FileName);
rewrite(t);
write(t, form1.edit1.text);
closefile(t);
end;

procedure TForm1.Button5Click(Sender: TObject);
var t: textfile;
    s: string;
begin
form1.OpenDialog1.Execute;
assignfile(t, form1.OpenDialog1.FileName);
reset(t);
readln(t, s);
form1.Edit1.Text := s;
closefile(t);
end;

procedure TForm1.Show1Click(Sender: TObject);
begin
form1.Show;
form1.TBNArea1.Enabled := false;
end;

procedure TForm1.Close1Click(Sender: TObject);
begin
form1.Close;
end;

procedure TForm1.FormDeactivate(Sender: TObject);
begin
form1.Hide;
form1.TBNArea1.Enabled := true;
end;

procedure TForm1.FormDblClick(Sender: TObject);
begin
 form1.Hide;
form1.TBNArea1.Enabled := true;
end;

procedure TForm1.TBNArea1DblClick(Sender: TObject);
begin
form1.Show;
form1.TBNArea1.Enabled := false;
end;

end.
