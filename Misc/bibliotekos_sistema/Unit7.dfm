object Form7: TForm7
  Left = 640
  Top = 185
  Width = 405
  Height = 500
  Caption = 'Apie CD:'
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'MS Sans Serif'
  Font.Style = []
  OldCreateOrder = False
  PixelsPerInch = 96
  TextHeight = 13
  object DBText1: TDBText
    Left = 72
    Top = 40
    Width = 42
    Height = 13
    AutoSize = True
    DataField = 'isbn'
    DataSource = DataModule2.DataSource7
  end
  object Label1: TLabel
    Left = 72
    Top = 16
    Width = 28
    Height = 13
    Caption = 'ISBN:'
  end
  object DBText2: TDBText
    Left = 72
    Top = 88
    Width = 42
    Height = 13
    AutoSize = True
    DataField = 'pavadinimas'
    DataSource = DataModule2.DataSource7
  end
  object Label2: TLabel
    Left = 72
    Top = 64
    Width = 80
    Height = 13
    Caption = 'CD pavadinimas:'
  end
  object DBText4: TDBText
    Left = 16
    Top = 128
    Width = 42
    Height = 13
    AutoSize = True
    DataField = 'data'
    DataSource = DataModule2.DataSource7
  end
  object DBText5: TDBText
    Left = 288
    Top = 128
    Width = 42
    Height = 13
    AutoSize = True
    DataField = 'kalba'
    DataSource = DataModule2.DataSource7
  end
  object DBText6: TDBText
    Left = 16
    Top = 192
    Width = 42
    Height = 13
    AutoSize = True
    DataField = 'sritis'
    DataSource = DataModule2.DataSource7
  end
  object DBText7: TDBText
    Left = 16
    Top = 232
    Width = 42
    Height = 13
    AutoSize = True
    DataField = 'vardas'
    DataSource = DataModule2.DataSource7
  end
  object DBText8: TDBText
    Left = 120
    Top = 232
    Width = 42
    Height = 13
    AutoSize = True
    DataField = 'pavarde'
    DataSource = DataModule2.DataSource7
  end
  object DBText9: TDBText
    Left = 16
    Top = 288
    Width = 65
    Height = 17
    DataField = 'Viso'
    DataSource = DataModule2.DataSource7
  end
  object DBText10: TDBText
    Left = 16
    Top = 336
    Width = 65
    Height = 17
    DataField = 'Paimta'
    DataSource = DataModule2.DataSource7
  end
  object DBText11: TDBText
    Left = 152
    Top = 336
    Width = 65
    Height = 17
    DataField = 'Rezervuota'
    DataSource = DataModule2.DataSource7
  end
  object DBText12: TDBText
    Left = 16
    Top = 384
    Width = 65
    Height = 17
    DataField = 'Nepaimta'
    DataSource = DataModule2.DataSource7
  end
  object Label4: TLabel
    Left = 16
    Top = 112
    Width = 67
    Height = 13
    Caption = 'I'#353'leidimo data:'
  end
  object Label5: TLabel
    Left = 288
    Top = 112
    Width = 30
    Height = 13
    Caption = 'Kalba:'
  end
  object Label6: TLabel
    Left = 16
    Top = 176
    Width = 25
    Height = 13
    Caption = 'Sritis:'
  end
  object Label7: TLabel
    Left = 16
    Top = 216
    Width = 41
    Height = 13
    Caption = 'Autorius:'
  end
  object Label8: TLabel
    Left = 16
    Top = 264
    Width = 150
    Height = 13
    Caption = 'I'#353' viso egzempliori'#371' bibliotekoje:'
  end
  object Label9: TLabel
    Left = 16
    Top = 312
    Width = 56
    Height = 13
    Caption = 'I'#353' j'#371' paimta:'
  end
  object Label10: TLabel
    Left = 16
    Top = 360
    Width = 58
    Height = 13
    Caption = 'Rezervuota:'
  end
  object Label11: TLabel
    Left = 152
    Top = 312
    Width = 48
    Height = 13
    Caption = 'Nepaimta:'
  end
  object Button1: TButton
    Left = 152
    Top = 416
    Width = 75
    Height = 25
    Caption = 'OK'
    TabOrder = 0
    OnClick = Button1Click
  end
end
