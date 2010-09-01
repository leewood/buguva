unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, Grids, DBGrids, DB, DBTables, StdCtrls, ComCtrls, DBCtrls,
  ExtCtrls, dbcgrids, Mask;

type
  TForm1 = class(TForm)
    Button2: TButton;
    Button3: TButton;
    PageControl1: TPageControl;
    TabSheet1: TTabSheet;
    TabSheet2: TTabSheet;
    TabSheet3: TTabSheet;
    TabSheet4: TTabSheet;
    Label2: TLabel;
    Edit1: TEdit;
    Label3: TLabel;
    Edit2: TEdit;
    Button1: TButton;
    DBGrid1: TDBGrid;
    Label4: TLabel;
    Button4: TButton;
    DBText1: TDBText;
    DBText2: TDBText;
    DBText3: TDBText;
    DBText4: TDBText;
    DBEdit1: TDBEdit;
    DBText5: TDBText;
    Label5: TLabel;
    Label6: TLabel;
    Label7: TLabel;
    Label8: TLabel;
    Label9: TLabel;
    Label10: TLabel;
    Label11: TLabel;
    DBText6: TDBText;
    DBText7: TDBText;
    GroupBox1: TGroupBox;
    Edit3: TEdit;
    Edit4: TEdit;
    Edit5: TEdit;
    Button5: TButton;
    Label12: TLabel;
    Label13: TLabel;
    Label14: TLabel;
    Button6: TButton;
    Button7: TButton;
    CheckBox1: TCheckBox;
    CheckBox2: TCheckBox;
    DBText8: TDBText;
    DBGrid2: TDBGrid;
    Button8: TButton;
    Label1: TLabel;
    DBGrid3: TDBGrid;
    Label15: TLabel;
    Button9: TButton;
    Button10: TButton;
    Edit6: TEdit;
    StatusBar1: TStatusBar;
    Timer1: TTimer;
    TabSheet5: TTabSheet;
    Button11: TButton;
    DBGrid4: TDBGrid;
    Button12: TButton;
    Button13: TButton;
    Button14: TButton;
    DBGrid5: TDBGrid;
    DBGrid6: TDBGrid;
    Button15: TButton;
    Button16: TButton;
    DBEdit2: TDBEdit;
    Label16: TLabel;
    Label17: TLabel;
    Label18: TLabel;
    Button17: TButton;
    DBEdit3: TDBEdit;
    Button18: TButton;
    procedure Button1Click(Sender: TObject);

    procedure Button2Click(Sender: TObject);
    procedure FormCreate(Sender: TObject);
    procedure Button3Click(Sender: TObject);
    procedure FormShow(Sender: TObject);
    procedure Button7Click(Sender: TObject);
    procedure Button6Click(Sender: TObject);
    procedure Button8Click(Sender: TObject);
    procedure Button5Click(Sender: TObject);
    procedure Edit6Change(Sender: TObject);
    procedure Button10Click(Sender: TObject);
    procedure Button4Click(Sender: TObject);
    procedure Button9Click(Sender: TObject);
    procedure Timer1Timer(Sender: TObject);
    procedure FormClose(Sender: TObject; var Action: TCloseAction);
    procedure DBGrid4MouseDown(Sender: TObject; Button: TMouseButton;
      Shift: TShiftState; X, Y: Integer);
    procedure DBGrid4CellClick(Column: TColumn);
    procedure Button13Click(Sender: TObject);
    procedure Button14Click(Sender: TObject);
    procedure Button12Click(Sender: TObject);
    procedure Button15Click(Sender: TObject);
    procedure Button16Click(Sender: TObject);
    procedure Button11Click(Sender: TObject);
    procedure Button17Click(Sender: TObject);
    procedure Button18Click(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  Form1: TForm1;
  login: boolean;
  name, pass: string;
  time: longint;
  data: string;
   kiek: integer;

implementation

uses Unit4, Unit2, Unit6, Unit7, Unit5, enterdate, Unit8, Unit11, Unit12;

{$R *.dfm}

procedure userUpdate;
  begin
    datamodule2.Query17.close;
    datamodule2.Query18.Close;
    datamodule2.Query17.Params[0].AsInteger := form1.DBGrid4.Fields[0].AsInteger;
    datamodule2.Query18.Params[0].AsInteger := form1.DBGrid4.Fields[0].AsInteger;
    datamodule2.Query17.open;
    datamodule2.Query18.Open;
end;

procedure TForm1.Button1Click(Sender: TObject);
  var s, s2, s3, s4: string;
  begin
    s := '';
    s2 := '';
    s3 := '';
    s4 := '';
    s2 := 'SELECT egzemplioriai.tipas, egzemplioriai.isbn, egzemplioriai.pavadinimas, egzemplioriai.viso, egzemplioriai.paimta, egzemplioriai.nepaimta, egzemplioriai.rezervuota FROM biblio.egzemplioriai';
    if form1.Edit1.Text <> '' then
      begin
        if s <> '' then s := s + ' and';
        s := s + ' egzemplioriai.pavadinimas LIKE ' + #39 + '%' + form1.Edit1.Text  + '%' + #39;
      end;
    if not(form1.CheckBox1.Checked) then
      begin
        if s <> '' then s := s + ' and';
        s := s + ' egzemplioriai.tipas <> ' + #39 + 'CD' + #39;
      end;
    if not(form1.CheckBox2.Checked) then
      begin
        if s <> '' then s := s + ' and';
        s := s + ' egzemplioriai.tipas <> ' + #39 + 'Knyga' + #39;
      end;
    if form1.Edit2.Text <> '' then
      begin
        if s <> '' then s := s + ' and';
        s := s + ' egzemplioriai.isbn = ' + #39 + form1.Edit2.Text  + #39;
      end;
    if s <> '' then s4 := s4 + 'WHERE' + s;
    datamodule2.Query1.SQL.Clear;
    datamodule2.Query1.SQL.Add(s2);
    datamodule2.Query1.SQL.Add(s3);
    datamodule2.Query1.SQL.Add(s4);
    datamodule2.Query1.Open;
    datamodule2.Query2.Open;
  end;


procedure TForm1.Button2Click(Sender: TObject);
  begin
    form4.showmodal;
    form1.Button2.Visible := not(login);
    form1.Button3.Visible := login;
    form1.TabSheet1.TabVisible := not(login);
    form1.TabSheet2.TabVisible := login;
    form1.TabSheet3.TabVisible := login;
    form1.TabSheet4.TabVisible := login;
    form1.TabSheet1.Enabled := not(login);
    form1.TabSheet2.Enabled := login;
    form1.TabSheet3.Enabled := login;
    form1.TabSheet4.Enabled := login;
    if login then begin
      datamodule2.Query5.close;
      datamodule2.Query5.SQL.Clear;
      datamodule2.Query5.SQL.Add('select nr, vardas, pavarde, ak, gimimas, adresas, leidimai, slaptazodis from biblio.skaitytojas where nr = ' + unit1.name);
      datamodule2.Query5.Open;
      form1.Edit6.Text := form1.DBEdit1.Text;
      form1.Button1.Click;
      datamodule2.Query8.Close;
      datamodule2.Query8.Params[0].AsInteger := strtoint(unit1.name);
      datamodule2.Query8.Open;
      datamodule2.Query10.Close;
      datamodule2.Query10.Params[0].AsInteger := strtoint(unit1.name);
      datamodule2.Query10.Open;
      time := 0;
      form1.Timer1.Enabled := true;
      form1.StatusBar1.Panels[0].Text := 'Jûs prisijungæs prie sistemos 0 sec.';
      datamodule2.Query16.Close;
      datamodule2.Query16.Open;
      userUpdate();
      form1.button11.Visible := (form1.DBText5.Field.AsInteger = 1) and login;
      form1.button17.Visible := (form1.DBText5.Field.AsInteger = 1) and login;
      form1.button18.Visible := (form1.DBText5.Field.AsInteger = 1) and login;
      form1.TabSheet5.TabVisible := login and (form1.DBText5.Field.AsInteger = 1);
    end;
  end;

procedure TForm1.FormCreate(Sender: TObject);
  begin
    login := false;
    form1.Timer1.Enabled := false;
    form1.StatusBar1.Panels[0].Text := 'Jûs neprisijungæs';
    form1.Button3.Visible := false;
    form1.Button2.Visible := true;
    form1.TabSheet1.TabVisible := not(login);
    form1.TabSheet2.TabVisible := login;
    form1.TabSheet3.TabVisible := login;
    form1.TabSheet4.TabVisible := login;
    form1.TabSheet1.Enabled := not(login);
    form1.TabSheet2.Enabled := login;
    form1.TabSheet3.Enabled := login;
    form1.TabSheet4.Enabled := login;
    form1.button11.Visible := false ;
    form1.button17.Visible := false ;
    form1.button18.Visible := false;
    form1.TabSheet5.TabVisible := false;
    form1.TabSheet2.Hide;
    form1.TabSheet3.Hide;
    form1.TabSheet1.Show;
  end;

procedure TForm1.Button3Click(Sender: TObject);
  begin
    login := false;
    form1.Timer1.Enabled := false;
    form1.StatusBar1.Panels[0].Text := 'Jûs neprisijungæs';
    form1.Button2.Visible := not(login);
    form1.Button3.Visible := login;
    form1.TabSheet1.TabVisible := not(login);
    form1.TabSheet2.TabVisible := login;
    form1.TabSheet3.TabVisible := login;
    form1.TabSheet4.TabVisible := login;
    form1.TabSheet1.Enabled := not(login);
    form1.TabSheet2.Enabled := login;
    form1.TabSheet3.Enabled := login;
    form1.TabSheet4.Enabled := login;
    form1.button11.Visible := login;
    form1.button17.Visible := login;
    form1.button18.Visible := login;
    form1.TabSheet5.TabVisible := login;
    form1.TabSheet2.Hide;
    form1.TabSheet3.Hide;
    form1.TabSheet1.Show;
  end;

procedure TForm1.FormShow(Sender: TObject);
  begin
    datamodule2.Database1.Open;
  end;

procedure TForm1.Button7Click(Sender: TObject);
  begin
    if (form1.DBGrid1.Fields[0].AsString = 'Knyga') then begin
      datamodule2.Query6.close;
      datamodule2.Query6.Params[0].AsString := form1.DBGrid1.Fields[1].AsString;
      datamodule2.Query6.Open;
      form6.show;
    end else begin
      datamodule2.Query7.close;
      datamodule2.Query7.Params[0].AsString := form1.DBGrid1.Fields[1].AsString;
      datamodule2.Query7.Open;
      form7.show;
    end;
  end;

procedure TForm1.Button6Click(Sender: TObject);
  begin
    if (form1.DBGrid1.Fields[6].AsInteger  +  form1.DBGrid1.Fields[4].AsInteger < form1.DBGrid1.Fields[3].AsInteger)
    then begin
      datamodule2.Query9.Close;
      datamodule2.Query9.Params[0].AsInteger := form1.DBText6.Field.AsInteger;
      datamodule2.Query9.Params[1].AsString := form1.DBGrid1.Fields[1].AsString;
      datamodule2.Query9.ExecSQL;
      form1.Button1.Click;
      showmessage('Knyga sëkmingai rezervuota');
    end else showmessage('Nebëra knygø, kurias bûtø galima rezervuoti');
  end;

procedure TForm1.Button8Click(Sender: TObject);
  begin
    if (form1.DBGrid2.Fields[3].AsString = 'Knyga') then begin
      datamodule2.Query6.close;
      datamodule2.Query6.Params[0].AsString := form1.DBGrid2.Fields[1].AsString;
      datamodule2.Query6.Open;
      form6.show;
    end else begin
      datamodule2.Query7.close;
      datamodule2.Query7.Params[0].AsString := form1.DBGrid2.Fields[1].AsString;
      datamodule2.Query7.Open;
      form7.show;
    end;
  end;

procedure TForm1.Button5Click(Sender: TObject);
  begin
    if (form1.DBText7.Field.AsString = form1.Edit3.Text) then
      if (form1.Edit4.Text = form1.Edit5.Text) then begin
        datamodule2.Query11.close;
        datamodule2.query11.params[0].asstring := form1.Edit5.Text;
        datamodule2.query11.params[1].asinteger := strtoint(unit1.name);
        datamodule2.Query11.execsql;
        showMessage('Slaptaþodis sëkmingai pakeistas');
      end else showMessage('Naujasis slaptaþodis ir pakartotas nesutampa')
    else showmessage('Neteisingas senasis slaptaþodis');
  end;

procedure TForm1.Edit6Change(Sender: TObject);
  begin
    datamodule2.Query12.close;
    datamodule2.query12.params[0].asstring := form1.Edit6.Text;
    datamodule2.query12.params[1].asinteger := strtoint(unit1.name);
    datamodule2.Query12.execsql;
  end;

procedure TForm1.Button10Click(Sender: TObject);
  begin
    datamodule2.Query13.Close;
    datamodule2.Query13.Params[0].AsInteger := form1.DBGrid3.Fields[0].AsInteger;
    datamodule2.Query13.execsql;
    datamodule2.Query10.Close;
    datamodule2.Query10.Params[0].AsInteger := StrToInt(unit1.name);
    datamodule2.Query10.Open;
  end;

procedure TForm1.Button4Click(Sender: TObject);
  begin
    form5.show;
  end;

procedure TForm1.Button9Click(Sender: TObject);
  begin
    if (form1.DBGrid3.Fields[3].AsString = 'Knyga') then begin
      datamodule2.Query6.close;
      datamodule2.Query6.Params[0].AsString := form1.DBGrid3.Fields[1].AsString;
      datamodule2.Query6.Open;
      form6.show;
    end else begin
      datamodule2.Query7.close;
      datamodule2.Query7.Params[0].AsString := form1.DBGrid3.Fields[1].AsString;
      datamodule2.Query7.Open;
      form7.show;
    end;
  end;

procedure TForm1.Timer1Timer(Sender: TObject);
  begin
    time := time + 1;
    form1.StatusBar1.Panels[0].Text := 'Jûs prisijungæs prie sistemos ' + IntToStr(time) + ' sec.';
  end;

procedure TForm1.FormClose(Sender: TObject; var Action: TCloseAction);
  begin
    form1.button3.Click;
  end;



procedure TForm1.DBGrid4MouseDown(Sender: TObject; Button: TMouseButton;
  Shift: TShiftState; X, Y: Integer);
  begin
    userUpdate();
  end;

procedure TForm1.DBGrid4CellClick(Column: TColumn);
  begin
    userUpdate();
  end;

procedure TForm1.Button13Click(Sender: TObject);
  begin
    datamodule2.Query19.close;
    datamodule2.Query19.Params[0].AsInteger := 1;
    datamodule2.Query19.Params[1].AsInteger := form1.DBGrid4.Fields[0].AsInteger;
    datamodule2.Query19.ExecSQL;
    datamodule2.Query16.close;
    datamodule2.Query16.open;
    userUpdate();
  end;

procedure TForm1.Button14Click(Sender: TObject);
  begin
    datamodule2.Query19.close;
    datamodule2.Query19.Params[0].AsInteger := 0;
    datamodule2.Query19.Params[1].AsInteger := form1.DBGrid4.Fields[0].AsInteger;
    datamodule2.Query19.ExecSQL;
    datamodule2.Query16.close;
    datamodule2.Query16.open;
    userUpdate();
  end;

procedure TForm1.Button12Click(Sender: TObject);
  begin
    datamodule2.Query20.close;
    datamodule2.Query20.Params[0].AsInteger := form1.DBGrid4.Fields[0].AsInteger;
    datamodule2.Query20.ExecSQL;
    datamodule2.Query16.close;
    datamodule2.Query16.open;
    userUpdate();
  end;

procedure TForm1.Button15Click(Sender: TObject);
  begin
    datamodule2.Query21.Close;
    datamodule2.Query21.Params[0].AsInteger := form1.DBGrid5.Fields[0].AsInteger;
    datamodule2.Query21.ExecSQL;
    datamodule2.Query17.Close;
    datamodule2.Query17.open;
    datamodule2.Query1.Close;
    datamodule2.Query1.open;
  end;

procedure TForm1.Button16Click(Sender: TObject);
  begin
    datamodule2.Query22.Close;
    datamodule2.Query22.Params[0].AsString := form1.DBGrid6.Fields[1].AsString;
    datamodule2.Query22.Open;
    datamodule2.query24.close;
    Form3.showmodal;
    datamodule2.Query24.Params[0].AsInteger := form1.DBGrid4.fields[0].AsInteger;
    datamodule2.Query24.Params[1].AsString := Unit1.data;
    datamodule2.Query24.Params[2].AsInteger := form1.dbedit2.field.AsInteger;
    datamodule2.Query24.ExecSQL;
    datamodule2.Query23.Close;
    datamodule2.Query23.Params[0].AsInteger := form1.DBGrid6.Fields[0].AsInteger;
    datamodule2.Query23.ExecSQL;
    datamodule2.Query17.Close;
    datamodule2.Query17.Open;
    datamodule2.Query18.Close;
    datamodule2.Query18.Open;
    datamodule2.Query1.Close;
    datamodule2.Query1.Open;
  end;

procedure TForm1.Button11Click(Sender: TObject);
  begin
    datamodule2.Query22.Close;
    datamodule2.Query22.Params[0].AsString := form1.DBGrid1.Fields[1].AsString;
    datamodule2.Query22.Open;
    Form3.Showmodal;
    datamodule2.Query24.Close;
    datamodule2.Query24.Params[0].asinteger := form1.DBGrid4.fields[0].asinteger;
    datamodule2.Query24.Params[1].AsString := unit1.data;
    datamodule2.Query24.Params[2].asinteger := form1.dbedit2.field.asinteger;
    datamodule2.query24.execsql;
    datamodule2.Query17.Close;
    datamodule2.Query17.open;
    datamodule2.Query18.Close;
    datamodule2.Query18.open;
    datamodule2.Query1.Close;
    datamodule2.Query1.open;
  end;

procedure TForm1.Button17Click(Sender: TObject);
  begin
    form8.show;
  end;

procedure TForm1.Button18Click(Sender: TObject);
  var st, x: longint;
  begin
    form12.showmodal;
    datamodule2.query32.Close;
    datamodule2.query32.open;
    st := form1.DBEdit3.Field.AsInteger;
    datamodule2.Query33.Close;
    for x := 1 to unit1.kiek do
      begin
        datamodule2.Query33.close;
        datamodule2.Query33.params[0].AsInteger := st + x;
        datamodule2.Query33.params[1].AsString := form1.DBGrid1.Fields[1].AsString;
        datamodule2.Query33.ExecSQL;
      end;
    datamodule2.Query1.Close;
    datamodule2.Query1.open;
  end;

end.
