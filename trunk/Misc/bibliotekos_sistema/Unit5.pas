unit Unit5;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, ComCtrls, DBCtrls;

type
  TForm5 = class(TForm)
    Edit1: TEdit;
    Label1: TLabel;
    Edit2: TEdit;
    Label2: TLabel;
    Label3: TLabel;
    Edit3: TEdit;
    Edit4: TEdit;
    Label4: TLabel;
    Edit5: TEdit;
    Label5: TLabel;
    Edit6: TEdit;
    Label6: TLabel;
    DateTimePicker1: TDateTimePicker;
    Label7: TLabel;
    Button1: TButton;
    Button2: TButton;
    DBText1: TDBText;
    procedure Button1Click(Sender: TObject);
    procedure Button2Click(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  Form5: TForm5;

implementation

uses Unit2;

{$R *.dfm}

procedure TForm5.Button1Click(Sender: TObject);
  var s, s2, s3: string;
  begin
    if form5.Edit6.Text = form5.Edit5.Text
      then begin
        datamodule2.Query14.Close;
        datamodule2.Query14.Params[0].AsString := form5.Edit3.Text;
        datamodule2.Query14.Params[1].AsString := form5.Edit1.Text;
        datamodule2.Query14.Params[2].AsString := form5.Edit2.Text;
        datamodule2.Query14.Params[4].AsString := form5.edit4.Text;
        datamodule2.Query14.Params[3].AsDateTime := form5.DateTimePicker1.DateTime;
        datamodule2.Query14.Params[5].AsString := form5.Edit5.Text;
        s2 := 'insert into biblio.skaitytojas values (0,' + #39;
        s2 := s2 + form5.Edit3.Text + #39 + ' ,' + #39+ form5.Edit1.Text + #39+ ',' + #39+ form5.Edit2.Text+ #39;
        s3 := s3 + ',' +#39 + datamodule2.Query14.Params[3].AsString + #39 + ',' + #39+ form5.edit4.Text + #39+', 0,'+ #39+ form5.edit5.Text + #39+ ')';
        datamodule2.Query14.SQL.Clear;
        datamodule2.Query14.SQL.Add(s2);
        datamodule2.Query14.SQL.Add(s3);
        datamodule2.Query14.ExecSQL;
        datamodule2.Query15.Close;
        datamodule2.Query15.Params[0].AsString := form5.Edit3.Text;
        datamodule2.Query15.open;
        s := 'Vartotojas sëkmingai sukurtas, Vartotojo ID:' + form5.DBText1.Field.AsString;
        showmessage(s);
        form5.Close;
      end else showmessage('Slaptaþodþiai nesutampa');
  end;

procedure TForm5.Button2Click(Sender: TObject);
  begin
    form5.Close;
  end;

end.
