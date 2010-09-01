object Form11: TForm11
  Left = 660
  Top = 142
  BorderIcons = []
  BorderStyle = bsSingle
  Caption = 'Pasirinkite auori'#371
  ClientHeight = 302
  ClientWidth = 322
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
    Left = 16
    Top = 192
    Width = 36
    Height = 13
    Caption = 'Vardas:'
    Visible = False
  end
  object Label2: TLabel
    Left = 144
    Top = 192
    Width = 40
    Height = 13
    Caption = 'Pavard'#279
    Visible = False
  end
  object DBGrid1: TDBGrid
    Left = 16
    Top = 32
    Width = 297
    Height = 120
    DataSource = DataModule2.DataSource17
    TabOrder = 0
    TitleFont.Charset = DEFAULT_CHARSET
    TitleFont.Color = clWindowText
    TitleFont.Height = -11
    TitleFont.Name = 'MS Sans Serif'
    TitleFont.Style = []
    Columns = <
      item
        Expanded = False
        FieldName = 'nr'
        Visible = True
      end
      item
        Expanded = False
        FieldName = 'vardas'
        Visible = True
      end
      item
        Expanded = False
        FieldName = 'pavarde'
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
    TabOrder = 1
    TabStop = True
    OnClick = RadioButton1Click
  end
  object Edit1: TEdit
    Left = 16
    Top = 208
    Width = 113
    Height = 21
    TabOrder = 2
    Visible = False
  end
  object RadioButton2: TRadioButton
    Left = 8
    Top = 168
    Width = 113
    Height = 17
    Caption = #302'vesti nauj'#261':'
    TabOrder = 3
    OnClick = RadioButton2Click
  end
  object Button1: TButton
    Left = 112
    Top = 264
    Width = 75
    Height = 25
    Caption = 'Gerai'
    TabOrder = 4
    OnClick = Button1Click
  end
  object Edit2: TEdit
    Left = 144
    Top = 208
    Width = 121
    Height = 21
    TabOrder = 5
    Visible = False
  end
  object DBEdit1: TDBEdit
    Left = 8
    Top = 232
    Width = 121
    Height = 21
    DataField = 'nr'
    DataSource = DataModule2.DataSource18
    TabOrder = 6
    Visible = False
  end
end
