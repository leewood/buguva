unit Unit4;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, Mask, DBCtrls, unit1;

type
  TForm4 = class(TForm)
    Edit1: TEdit;
    Edit2: TEdit;
    Label1: TLabel;
    Label2: TLabel;
    Button1: TButton;
    DBEdit1: TDBEdit;
    Label3: TLabel;
    Button2: TButton;
    procedure Button1Click(Sender: TObject);
    procedure Button2Click(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  Form4: TForm4;

implementation

uses Unit2;

{$R *.dfm}

procedure TForm4.Button1Click(Sender: TObject);
  var s: string;
      x: integer;
  begin
    try
      x := strtoint(form4.Edit1.Text);
      datamodule2.query4.sql.clear;
      datamodule2.query4.sql.Add('SELECT skaitytojas.slaptazodis from biblio.skaitytojas where nr = ' + form4.Edit1.Text);
      datamodule2.Database1.Session.Open;
      datamodule2.Query4.open;
      s := form4.DBEdit1.Text;
      datamodule2.Query4.Close;
      if (s = '') then begin
        form4.Label3.Caption := 'Toks vartotojas neegzistuoja';
        form4.Label3.Visible := true;
      end else
        if (s = form4.Edit2.Text) then begin
          unit1.login := true;
          unit1.name := form4.Edit1.Text;
          unit1.pass := form4.Edit2.Text;
          form4.Edit1.Text := '';
          form4.Edit2.Text := '';
          form4.Label3.Visible := false;
          form4.Close;
        end else begin
          form4.Label3.Caption := 'Neteisingas slaptaþodis';
          form4.Label3.Visible := true;
        end;
    except
      on  EConvertError do
        begin
          form4.Label3.Caption := 'Vartotojo id turi bûti sudarytas ið skaitmenø';
          form4.Label3.Visible := true;
        end;
      else begin
        form4.Label3.Caption := 'Vartotojo id turi bûti sudarytas ið skaitmenø';
        form4.Label3.Visible := true;
      end;
  end;

end;

procedure TForm4.Button2Click(Sender: TObject);
  begin
    form4.Close;
  end;

end.
