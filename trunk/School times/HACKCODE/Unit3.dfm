object Form3: TForm3
  Left = 678
  Top = 399
  Width = 257
  Height = 231
  Caption = 'Options'
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'MS Sans Serif'
  Font.Style = []
  OldCreateOrder = False
  OnShow = FormShow
  PixelsPerInch = 96
  TextHeight = 13
  object Label1: TLabel
    Left = 16
    Top = 0
    Width = 24
    Height = 13
    Caption = 'Skin:'
  end
  object Label2: TLabel
    Left = 16
    Top = 16
    Width = 32
    Height = 13
    Caption = 'Label2'
  end
  object Label3: TLabel
    Left = 16
    Top = 88
    Width = 40
    Height = 13
    Caption = 'Assitant:'
  end
  object Label4: TLabel
    Left = 16
    Top = 104
    Width = 32
    Height = 13
    Caption = 'Label4'
  end
  object Label5: TLabel
    Left = 16
    Top = 136
    Width = 92
    Height = 13
    Caption = 'Interface language:'
  end
  object Label6: TLabel
    Left = 16
    Top = 160
    Width = 34
    Height = 13
    Caption = 'English'
  end
  object Label7: TLabel
    Left = 16
    Top = 40
    Width = 73
    Height = 13
    Caption = 'Alpha blending '
  end
  object Button1: TButton
    Left = 72
    Top = 0
    Width = 75
    Height = 25
    Caption = 'Change'
    TabOrder = 0
    OnClick = Button1Click
  end
  object Button2: TButton
    Left = 160
    Top = 96
    Width = 75
    Height = 25
    Caption = 'Change'
    TabOrder = 1
  end
  object Button3: TButton
    Left = 160
    Top = 160
    Width = 75
    Height = 25
    Caption = 'Change'
    TabOrder = 2
  end
  object ScrollBar1: TScrollBar
    Left = 16
    Top = 56
    Width = 217
    Height = 16
    Max = 255
    PageSize = 0
    TabOrder = 3
    OnChange = ScrollBar1Change
  end
  object OpenDialog1: TOpenDialog
    Filter = 'HackCode Skin|*.sini'
    Left = 168
    Top = 24
  end
end
