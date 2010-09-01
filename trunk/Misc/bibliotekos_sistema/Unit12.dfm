object Form12: TForm12
  Left = 433
  Top = 310
  BorderIcons = []
  BorderStyle = bsSingle
  Caption = 'Kiek egzempliori'#371
  ClientHeight = 123
  ClientWidth = 192
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
    Top = 40
    Width = 121
    Height = 25
    TabOrder = 0
    Text = '0'
  end
  object UpDown1: TUpDown
    Left = 153
    Top = 40
    Width = 15
    Height = 25
    Associate = Edit1
    TabOrder = 1
  end
  object Button1: TButton
    Left = 56
    Top = 72
    Width = 75
    Height = 25
    Caption = 'Gerai'
    TabOrder = 2
    OnClick = Button1Click
  end
end
