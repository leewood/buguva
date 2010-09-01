object Form3: TForm3
  Left = 520
  Top = 156
  BorderIcons = []
  BorderStyle = bsSingle
  Caption = 'Pasirinkite, kada knyg'#261' gr'#261#382'inti'
  ClientHeight = 141
  ClientWidth = 299
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'MS Sans Serif'
  Font.Style = []
  OldCreateOrder = False
  PixelsPerInch = 96
  TextHeight = 13
  object Label1: TLabel
    Left = 56
    Top = 32
    Width = 38
    Height = 13
    Caption = 'Gr'#261#382'inti:'
  end
  object Label2: TLabel
    Left = 184
    Top = 16
    Width = 32
    Height = 13
    Caption = 'Label2'
    Visible = False
  end
  object DateTimePicker1: TDateTimePicker
    Left = 56
    Top = 56
    Width = 186
    Height = 21
    Date = 39443.673623946760000000
    Time = 39443.673623946760000000
    TabOrder = 0
    OnChange = DateTimePicker1Change
  end
  object Button1: TButton
    Left = 104
    Top = 96
    Width = 75
    Height = 25
    Caption = 'Gerai'
    TabOrder = 1
    OnClick = Button1Click
  end
end
