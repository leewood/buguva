unit enterdate;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, ComCtrls, unit1;

type
  TForm3 = class(TForm)
    DateTimePicker1: TDateTimePicker;
    Label1: TLabel;
    Button1: TButton;
    Label2: TLabel;
    procedure DateTimePicker1Change(Sender: TObject);
    procedure Button1Click(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  Form3: TForm3;

implementation

{$R *.dfm}

procedure TForm3.DateTimePicker1Change(Sender: TObject);
begin
form3.Label2.Caption := datetostr(form3.DateTimePicker1.Date);
end;

procedure TForm3.Button1Click(Sender: TObject);
begin
unit1.data := form3.label2.caption;
form3.Close;
end;

end.
