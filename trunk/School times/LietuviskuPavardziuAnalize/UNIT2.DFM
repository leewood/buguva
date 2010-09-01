object Form2: TForm2
  Left = 317
  Top = 137
  Width = 216
  Height = 324
  Caption = 'Pirmumo eilës keitimas'
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'MS Sans Serif'
  Font.Style = []
  OldCreateOrder = False
  PixelsPerInch = 96
  TextHeight = 13
  object Button1: TButton
    Left = 8
    Top = 8
    Width = 75
    Height = 25
    Caption = 'Keisti'
    TabOrder = 0
    OnClick = Button1Click
  end
  object RadioGroup1: TRadioGroup
    Left = 0
    Top = 48
    Width = 201
    Height = 249
    Caption = 'Pirmumo eilë '
    ItemIndex = 0
    Items.Strings = (
      'Sujungti'
      'Vardas, pavardë, klasë'
      'Vardas, pavardë'
      'Vardas, klasë, pavardë'
      'Vardas, klasë'
      'Vardas'
      'Pavardë, vardas, klasë'
      'Pavardë, vardas'
      'Pavardë, klasë, vardas'
      'Pavardë, klasë'
      'Pavardë'
      'Klasë, vardas, pavardë'
      'Klasë, vardas'
      'Klasë, pavardë, vardas'
      'Klasë, pavardë'
      'Klasë')
    TabOrder = 1
  end
end
