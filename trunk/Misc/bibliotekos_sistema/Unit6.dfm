object Form6: TForm6
  Left = 233
  Top = 185
  Width = 403
  Height = 500
  Caption = 'Apie knyg'#261':'
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
    DataSource = DataModule2.DataSource6
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
    DataSource = DataModule2.DataSource6
  end
  object Label2: TLabel
    Left = 72
    Top = 64
    Width = 100
    Height = 13
    Caption = 'Knygos pavadinimas:'
  end
  object DBText3: TDBText
    Left = 16
    Top = 128
    Width = 42
    Height = 13
    AutoSize = True
    DataField = 'leidykla'
    DataSource = DataModule2.DataSource6
  end
  object DBText4: TDBText
    Left = 152
    Top = 128
    Width = 42
    Height = 13
    AutoSize = True
    DataField = 'metai'
    DataSource = DataModule2.DataSource6
  end
  object DBText5: TDBText
    Left = 288
    Top = 128
    Width = 42
    Height = 13
    AutoSize = True
    DataField = 'kalba'
    DataSource = DataModule2.DataSource6
  end
  object DBText6: TDBText
    Left = 16
    Top = 192
    Width = 42
    Height = 13
    AutoSize = True
    DataField = 'sritis'
    DataSource = DataModule2.DataSource6
  end
  object DBText7: TDBText
    Left = 16
    Top = 232
    Width = 42
    Height = 13
    AutoSize = True
    DataField = 'vardas'
    DataSource = DataModule2.DataSource6
  end
  object DBText8: TDBText
    Left = 120
    Top = 232
    Width = 42
    Height = 13
    AutoSize = True
    DataField = 'pavarde'
    DataSource = DataModule2.DataSource6
  end
  object DBText9: TDBText
    Left = 16
    Top = 288
    Width = 65
    Height = 17
    DataField = 'Viso'
    DataSource = DataModule2.DataSource6
  end
  object DBText10: TDBText
    Left = 16
    Top = 336
    Width = 65
    Height = 17
    DataField = 'Paimta'
    DataSource = DataModule2.DataSource6
  end
  object DBText11: TDBText
    Left = 152
    Top = 336
    Width = 65
    Height = 17
    DataField = 'Rezervuota'
    DataSource = DataModule2.DataSource6
  end
  object DBText12: TDBText
    Left = 16
    Top = 384
    Width = 65
    Height = 17
    DataField = 'Nepaimta'
    DataSource = DataModule2.DataSource6
  end
  object Label3: TLabel
    Left = 16
    Top = 112
    Width = 42
    Height = 13
    Caption = 'Leidykla:'
  end
  object Label4: TLabel
    Left = 152
    Top = 112
    Width = 71
    Height = 13
    Caption = 'I'#353'leidimo metai:'
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
  object DBText13: TDBText
    Left = 152
    Top = 192
    Width = 65
    Height = 17
    DataField = 'verte'
    DataSource = DataModule2.DataSource6
  end
  object Label12: TLabel
    Left = 152
    Top = 176
    Width = 65
    Height = 13
    Caption = 'Knygos vert'#279':'
  end
  object DBText14: TDBText
    Left = 288
    Top = 192
    Width = 65
    Height = 17
    DataField = 'puslapiai'
    DataSource = DataModule2.DataSource6
  end
  object Label13: TLabel
    Left = 288
    Top = 176
    Width = 84
    Height = 13
    Caption = 'Puslapi'#371' skai'#269'ius:'
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
