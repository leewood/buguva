unit Unit2;

interface

uses
  Windows, Messages, SysUtils, Classes, Graphics, Controls, Forms, Dialogs,
  StdCtrls, ExtCtrls;

type
  TForm2 = class(TForm)
    Button1: TButton;
    RadioGroup1: TRadioGroup;
    procedure Button1Click(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  Form2: TForm2;

implementation

uses Unit1;

{$R *.DFM}

procedure TForm2.Button1Click(Sender: TObject);
var s: string;
    x, c: integer;
begin
form1.Label2.Caption :=
form2.RadioGroup1.Items[form2.RadioGroup1.ItemIndex];
case form2.RadioGroup1.ItemIndex of
0: form1.cur := 0;
1: form1.cur := 123;
2: form1.cur := 120;
3: form1.cur := 132;
4: form1.cur := 130;
5: form1.cur := 100;
6: form1.cur := 213;
7: form1.cur := 210;
8: form1.cur := 231;
9: form1.cur := 230;
10: form1.cur := 200;
11: form1.cur := 312;
12: form1.cur := 310;
13: form1.cur := 321;
14: form1.cur := 320;
15: form1.cur := 300;
end;
end;

end.
