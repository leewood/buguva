unit Unit12;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, ComCtrls, unit1;

type
  TForm12 = class(TForm)
    Edit1: TEdit;
    UpDown1: TUpDown;
    Button1: TButton;
    procedure Button1Click(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  Form12: TForm12;

implementation

{$R *.dfm}

procedure TForm12.Button1Click(Sender: TObject);
  begin
    unit1.kiek := form12.updown1.position;
    form12.Close;
  end;

end.
