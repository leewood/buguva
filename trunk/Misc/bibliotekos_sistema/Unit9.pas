unit Unit9;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, unit2, unit8, StdCtrls, DBCtrls, Grids, DBGrids;

type
  TForm9 = class(TForm)
    DBGrid1: TDBGrid;
    Edit1: TEdit;
    RadioButton1: TRadioButton;
    RadioButton2: TRadioButton;
    Button1: TButton;
    procedure RadioButton1Click(Sender: TObject);
    procedure RadioButton2Click(Sender: TObject);
    procedure Button1Click(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  Form9: TForm9;

implementation

{$R *.dfm}

procedure TForm9.RadioButton1Click(Sender: TObject);
  begin
    form9.RadioButton2.Checked := not form9.RadioButton1.Checked;
    form9.DBGrid1.visible := form9.RadioButton1.Checked;
    form9.Edit1.visible := form9.RadioButton2.Checked;
  end;

procedure TForm9.RadioButton2Click(Sender: TObject);
  begin
    form9.RadioButton1.Checked := not form9.RadioButton2.Checked;
    form9.DBGrid1.visible := form9.RadioButton1.Checked;
    form9.Edit1.visible := form9.RadioButton2.Checked;
  end;

procedure TForm9.Button1Click(Sender: TObject);
  begin
    if form9.RadioButton1.Checked then
      unit8.kalba := form9.DBGrid1.Fields[0].AsString
    else
      unit8.kalba := form9.Edit1.Text;
    form9.Close;
  end;

end.
