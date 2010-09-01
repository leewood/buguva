unit Unit4;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, ExtCtrls;

type
  TForm4 = class(TForm)
    Label1: TLabel;
    Edit1: TEdit;
    Image1: TImage;
    procedure Edit1KeyDown(Sender: TObject; var Key: Word;
      Shift: TShiftState);
    procedure FormShow(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  Form4: TForm4;

implementation

uses Unit1;

{$R *.dfm}

procedure TForm4.Edit1KeyDown(Sender: TObject; var Key: Word;
  Shift: TShiftState);
var code: integer;
    s: string;
begin
if key = 13 then begin
s:=form4.edit1.text;
form4.Edit1.Text := '';
form1.vardai[form1.komanda]:=s;
if form1.kiek >= form1.komanda then form1.komanda:=form1.komanda+1;
str(form1.komanda,s);
form4.Label1.Caption:='Iveskite komandos '+s+' varda:';
if form1.komanda > form1.kiek then begin
form1.komanda:=0; form1.Image1.Visible:=false; form1.reikia:=true;
form4.close; end;
end;
end;

procedure TForm4.FormShow(Sender: TObject);
var s: string;
begin
str(form1.komanda,s);
form4.Label1.Caption:='Iveskite komandos '+s+' varda:';
end;


end.
