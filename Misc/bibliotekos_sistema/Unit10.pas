unit Unit10;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls,unit2,unit8, Grids, DBGrids;

type
  TForm10 = class(TForm)
    DBGrid1: TDBGrid;
    Edit1: TEdit;
    RadioButton1: TRadioButton;
    RadioButton2: TRadioButton;
    Button1: TButton;
    procedure Button1Click(Sender: TObject);
    procedure RadioButton1Click(Sender: TObject);
    procedure RadioButton2Click(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  Form10: TForm10;

implementation

{$R *.dfm}

procedure TForm10.Button1Click(Sender: TObject);
  begin
    if form10.RadioButton1.Checked then
      unit8.sritis := form10.DBGrid1.Fields[0].AsString
    else
      unit8.sritis := form10.Edit1.Text;
    form10.Close;
  end;

procedure TForm10.RadioButton1Click(Sender: TObject);
  begin
    form10.RadioButton2.Checked := not form10.RadioButton1.Checked;
    form10.DBGrid1.visible := form10.RadioButton1.Checked;
    form10.Edit1.visible := form10.RadioButton2.Checked;
  end;

procedure TForm10.RadioButton2Click(Sender: TObject);
  begin
    form10.RadioButton1.Checked := not form10.RadioButton2.Checked;
    form10.DBGrid1.visible := form10.RadioButton1.Checked;
    form10.Edit1.visible := form10.RadioButton2.Checked;
  end;

end.
