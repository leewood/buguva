object Form9: TForm9
  Left = 208
  Top = 138
  BorderIcons = []
  BorderStyle = bsSingle
  Caption = 'Pasirinkite kalb'#261
  ClientHeight = 295
  ClientWidth = 198
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'MS Sans Serif'
  Font.Style = []
  OldCreateOrder = False
  PixelsPerInch = 96
  TextHeight = 13
  object Edit1: TEdit
    Left = 32
    Top = 216
    Width = 121
    Height = 21
    TabOrder = 1
    Visible = False
  end
  object DBGrid1: TDBGrid
    Left = 48
    Top = 40
    Width = 113
    Height = 120
    DataSource = DataModule2.DataSource15
    TabOrder = 0
    TitleFont.Charset = DEFAULT_CHARSET
    TitleFont.Color = clWindowText
    TitleFont.Height = -11
    TitleFont.Name = 'MS Sans Serif'
    TitleFont.Style = []
    Columns = <
      item
        Expanded = False
        FieldName = 'kalba'
        Visible = True
      end>
  end
  object RadioButton1: TRadioButton
    Left = 8
    Top = 16
    Width = 153
    Height = 17
    Caption = 'Pasirinkti i'#353' egzistuojan'#269'i'#371':'
    Checked = True
    TabOrder = 2
    TabStop = True
    OnClick = RadioButton1Click
  end
  object RadioButton2: TRadioButton
    Left = 8
    Top = 192
    Width = 113
    Height = 17
    Caption = #302'vesti nauj'#261':'
    TabOrder = 3
    OnClick = RadioButton2Click
  end
  object Button1: TButton
    Left = 56
    Top = 256
    Width = 75
    Height = 25
    Caption = 'Gerai'
    TabOrder = 4
    OnClick = Button1Click
  end
end
