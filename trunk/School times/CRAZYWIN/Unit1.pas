unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, WinAccess, StdCtrls;

type
  TForm1 = class(TForm)
    WinAccess1: TWinAccess;
    Button1: TButton;
    Button2: TButton;
    Button3: TButton;
    Button4: TButton;
    Button5: TButton;
    Button6: TButton;
    Button7: TButton;
    Edit1: TEdit;
    procedure Button1Click(Sender: TObject);
    procedure Button2Click(Sender: TObject);
    procedure Button3Click(Sender: TObject);
    procedure Button4Click(Sender: TObject);
    procedure Button5Click(Sender: TObject);
    procedure Button6Click(Sender: TObject);
    procedure Button7Click(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  Form1: TForm1;

implementation

{$R *.dfm}

procedure TForm1.Button1Click(Sender: TObject);
begin
form1.WinAccess1.StartButton := ashiden;
end;

procedure TForm1.Button2Click(Sender: TObject);
begin
form1.WinAccess1.StartButton := asvisible;
end;

procedure TForm1.Button3Click(Sender: TObject);
begin
form1.WinAccess1.TrayBar := ashiden;
end;

procedure TForm1.Button4Click(Sender: TObject);
begin
form1.WinAccess1.TrayNotify := ashiden;
end;

procedure TForm1.Button5Click(Sender: TObject);
begin
form1.WinAccess1.TrayBar := asvisible;
end;

procedure TForm1.Button6Click(Sender: TObject);
begin
form1.WinAccess1.TrayNotify := asvisible;
end;

procedure finff;
const
iTRAYBAR = 0;
 iTRAYNOTIFY = 1;
 iSTARTBUTTON = 2;
 iAPPSWITCHBAR = 3;
var
 wnd : THandle;
 s: string;
 SysClasses : array [0..3] of PChar;
 c: pchar;
begin
SysClasses[0] := 'Shell_TrayWnd';
 SysClasses[1] := 'TrayNotifyWnd';
 SysClasses[2] := 'Button';
 SysClasses[3] := 'MSTaskSwWClass';
 wnd := FindWindow(SysClasses[iTRAYBAR],nil);
                                                            

 SetWindowPos(wnd, HWND_TOP,0,-10,10,50,SWP_SHOWWINDOW);
 wnd := FindWindow(SysClasses[iTRAYBAR],nil);
 wnd := FindWindowEx(wnd,HWND(0),SysClasses[iSTARTBUTTON],nil);
{ case v of
  asVisible :
   }SetWindowPos(wnd, HWND_TOP,0,0,100,40,SWP_NOZORDER+SWP_SHOWWINDOW);
 { asHiden:
   SetWindowPos(wnd, HWND_TOP,0,0,0,0,SWP_NOZORDER+SWP_NOMOVE+SWP_NOSIZE+SWP_HIDEWINDOW);}
s := form1.edit1.text;
c := pchar(s);
   setwindowtext(wnd, c);
 end;
procedure TForm1.Button7Click(Sender: TObject);
begin
finff;
end;

end.
