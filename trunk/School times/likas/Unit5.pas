unit Unit5;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, OleCtrls, SHDocVw, ExtCtrls;

type
  TForm5 = class(TForm)
    WebBrowser1: TWebBrowser;
    Timer1: TTimer;
    procedure Button1Click(Sender: TObject);
    procedure Timer1Timer(Sender: TObject);
    procedure FormKeyDown(Sender: TObject; var Key: Word;
      Shift: TShiftState);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  Form5: TForm5;

implementation

{$R *.dfm}

procedure TForm5.Button1Click(Sender: TObject);
begin
form5.WebBrowser1.Navigate('file://d:\who.swf');
end;

procedure TForm5.Timer1Timer(Sender: TObject);
begin
form5.WebBrowser1.Navigate('file://d:\who.swf');
form5.Timer1.Enabled:=false;
end;

procedure TForm5.FormKeyDown(Sender: TObject; var Key: Word;
  Shift: TShiftState);
begin
if (key = 27) then form5.Hide;
end;

end.
